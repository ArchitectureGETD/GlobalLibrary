using System;
using System.Collections.Generic;
using System.IO;
using iTextSharp.GE.text.pdf.codec;
using iTextSharp.GE.text.exceptions;
using iTextSharp.GE.text.error_messages;
using System.util.collections;

namespace iTextSharp.GE.text.pdf {


    /**
     * Encapsulates filter behavior for PDF streams.  Classes generally interace with this
     * using the static GetDefaultFilterHandlers() method, then obtain the desired {@link IFilterHandler}
     * via a lookup.
     * @since 5.0.4
     */
    // Dev note:  we eventually want to refactor PdfReader so all of the existing filter functionality is moved into this class
    // it may also be better to split the sub-classes out into a separate package
    public sealed class FilterHandlers {
        /**
         * The main interface for creating a new {@link IFilterHandler}
         */
        public interface IFilterHandler {
            byte[] Decode(byte[] b, PdfName filterName, PdfObject decodeParams, PdfDictionary streamDictionary);
        }

        /** The default {@link IFilterHandler}s used by iText */
        private static IDictionary<PdfName, IFilterHandler> defaults;

        static FilterHandlers() {
            Dictionary<PdfName, IFilterHandler> map = new Dictionary<PdfName, IFilterHandler>();

            map[PdfName.FLATEDECODE] = new Filter_FLATEDECODE();
            map[PdfName.FL] = new Filter_FLATEDECODE();
            map[PdfName.ASCIIHEXDECODE] = new Filter_ASCIIHEXDECODE();
            map[PdfName.AHX] = new Filter_ASCIIHEXDECODE();
            map[PdfName.ASCII85DECODE] = new Filter_ASCII85DECODE();
            map[PdfName.A85] = new Filter_ASCII85DECODE();
            map[PdfName.LZWDECODE] = new Filter_LZWDECODE();
            map[PdfName.CCITTFAXDECODE] = new Filter_CCITTFAXDECODE();
            map[PdfName.CRYPT] = new Filter_DoNothing();
            map[PdfName.RUNLENGTHDECODE] = new Filter_RUNLENGTHDECODE();

            defaults = new ReadOnlyDictionary<PdfName, IFilterHandler>(map);
        }

        /**
         * @return the default {@link IFilterHandler}s used by iText
         */
        public static IDictionary<PdfName, IFilterHandler> GetDefaultFilterHandlers() {
            return defaults;
        }

        /**
        * Creates a {@link MemoryLimitsAwareOutputStream} which will be used for decompression of the passed pdf stream.
        *
        * @param streamDictionary the pdf stream which is going to be decompressed.
        * @return the {@link ByteArrayOutputStream} which will be used for decompression of the passed pdf stream
        */
        public static MemoryStream EnableMemoryLimitsAwareHandler(PdfDictionary streamDictionary) {
            MemoryLimitsAwareOutputStream outputStream = new MemoryLimitsAwareOutputStream();
            MemoryLimitsAwareHandler memoryLimitsAwareHandler = null;
            if (streamDictionary is PRStream && null != ((PRStream)streamDictionary).Reader) {
                memoryLimitsAwareHandler = ((PRStream)streamDictionary).Reader.GetMemoryLimitsAwareHandler();
            }
            else {
                // We do not reuse some static instance because one can process pdfs in different threads.
                memoryLimitsAwareHandler = new MemoryLimitsAwareHandler();
            }
            if (null != memoryLimitsAwareHandler && memoryLimitsAwareHandler.ConsiderCurrentPdfStream) {
                outputStream.SetMaxStreamSize(memoryLimitsAwareHandler.GetMaxSizeOfSingleDecompressedPdfStream());
            }
            return outputStream;
        }

        /**
         * Handles FLATEDECODE filter
         */
        private class Filter_FLATEDECODE : IFilterHandler {
            public byte[] Decode(byte[] b, PdfName filterName, PdfObject decodeParams, PdfDictionary streamDictionary) {
                MemoryStream outS = EnableMemoryLimitsAwareHandler(streamDictionary);
                b = PdfReader.FlateDecode(b, outS);
                b = PdfReader.DecodePredictor(b, decodeParams);
                return b;
            }
        }

        /**
         * Handles ASCIIHEXDECODE filter
         */
        private class Filter_ASCIIHEXDECODE : IFilterHandler {
            public byte[] Decode(byte[] b, PdfName filterName, PdfObject decodeParams, PdfDictionary streamDictionary) {
                MemoryStream outS = EnableMemoryLimitsAwareHandler(streamDictionary);
                b = PdfReader.ASCIIHexDecode(b, outS);
                return b;
            }
        }

        /**
         * Handles ASCIIHEXDECODE filter
         */
        private class Filter_ASCII85DECODE : IFilterHandler {
            public byte[] Decode(byte[] b, PdfName filterName, PdfObject decodeParams, PdfDictionary streamDictionary) {
                MemoryStream outS = EnableMemoryLimitsAwareHandler(streamDictionary);
                b = PdfReader.ASCII85Decode(b, outS);
                return b;
            }
        }

        /**
         * Handles LZWDECODE filter
         */
        private class Filter_LZWDECODE : IFilterHandler {
            public byte[] Decode(byte[] b, PdfName filterName, PdfObject decodeParams, PdfDictionary streamDictionary) {
                MemoryStream outS = EnableMemoryLimitsAwareHandler(streamDictionary);
                b = PdfReader.LZWDecode(b, outS);
                b = PdfReader.DecodePredictor(b, decodeParams);
                return b;
            }
        }

        /**
         * Handles CCITTFAXDECODE filter
         */
        private class Filter_CCITTFAXDECODE : IFilterHandler {
            public byte[] Decode(byte[] b, PdfName filterName, PdfObject decodeParams, PdfDictionary streamDictionary) {
                PdfNumber wn = (PdfNumber)PdfReader.GetPdfObjectRelease(streamDictionary.Get(PdfName.WIDTH));
                PdfNumber hn = (PdfNumber)PdfReader.GetPdfObjectRelease(streamDictionary.Get(PdfName.HEIGHT));
                if (wn == null || hn == null)
                    throw new UnsupportedPdfException(MessageLocalization.GetComposedMessage("filter.ccittfaxdecode.is.only.supported.for.images"));
                int width = wn.IntValue;
                int height = hn.IntValue;

                PdfDictionary param = decodeParams is PdfDictionary ? (PdfDictionary)decodeParams : null;
                int k = 0;
                bool blackIs1 = false;
                bool byteAlign = false;
                if (param != null) {
                    PdfNumber kn = param.GetAsNumber(PdfName.K);
                    if (kn != null)
                        k = kn.IntValue;
                    PdfBoolean bo = param.GetAsBoolean(PdfName.BLACKIS1);
                    if (bo != null)
                        blackIs1 = bo.BooleanValue;
                    bo = param.GetAsBoolean(PdfName.ENCODEDBYTEALIGN);
                    if (bo != null)
                        byteAlign = bo.BooleanValue;
                }
                byte[] outBuf = new byte[(width + 7) / 8 * height];
                TIFFFaxDecompressor decoder = new TIFFFaxDecompressor();
                if (k == 0 || k > 0) {
                    int tiffT4Options = k > 0 ? TIFFConstants.GROUP3OPT_2DENCODING : 0;
                    tiffT4Options |= byteAlign ? TIFFConstants.GROUP3OPT_FILLBITS : 0;
                    decoder.SetOptions(1, TIFFConstants.COMPRESSION_CCITTFAX3, tiffT4Options, 0);
                    decoder.DecodeRaw(outBuf, b, width, height);
                    if (decoder.fails > 0) {
                        byte[] outBuf2 = new byte[(width + 7) / 8 * height];
                        int oldFails = decoder.fails;
                        decoder.SetOptions(1, TIFFConstants.COMPRESSION_CCITTRLE, tiffT4Options, 0);
                        decoder.DecodeRaw(outBuf2, b, width, height);
                        if (decoder.fails < oldFails) {
                            outBuf = outBuf2;
                        }
                    }
                }
                else {
                    TIFFFaxDecoder deca = new TIFFFaxDecoder(1, width, height);
                    deca.DecodeT6(outBuf, b, 0, height, 0);
                }
                if (!blackIs1) {
                    int len = outBuf.Length;
                    for (int t = 0; t < len; ++t) {
                        outBuf[t] ^= 0xff;
                    }
                }
                b = outBuf;
                return b;
            }
        }

        /**
         * A filter that doesn't modify the stream at all
         */
        private class Filter_DoNothing : IFilterHandler {
            public byte[] Decode(byte[] b, PdfName filterName, PdfObject decodeParams, PdfDictionary streamDictionary) {
                return b;
            }
        }

        /**
         * Handles RUNLENGTHDECODE filter
         */
        private class Filter_RUNLENGTHDECODE : IFilterHandler {
            public byte[] Decode(byte[] b, PdfName filterName, PdfObject decodeParams, PdfDictionary streamDictionary) {
                // allocate the output buffer
                MemoryStream baos = EnableMemoryLimitsAwareHandler(streamDictionary);
                sbyte dupCount = -1;
                for (int i = 0; i < b.Length; i++) {
                    dupCount = (sbyte)b[i];
                    if (dupCount == -128) break; // this is implicit end of data

                    if (dupCount >= 0 && dupCount <= 127) {
                        int bytesToCopy = dupCount + 1;
                        baos.Write(b, i, bytesToCopy);
                        i += bytesToCopy;
                    }
                    else {
                        // make dupcount copies of the next byte
                        i++;
                        for (int j = 0; j < 1 - (int)(dupCount); j++) {
                            baos.WriteByte(b[i]);
                        }
                    }
                }
                return baos.ToArray();
            }
        }
    }
}
