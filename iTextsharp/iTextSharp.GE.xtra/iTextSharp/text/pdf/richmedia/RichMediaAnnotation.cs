using System;
using System.Collections.Generic;
using iTextSharp.GE.text;
using iTextSharp.GE.text.pdf;
using iTextSharp.GE.text.exceptions;

namespace iTextSharp.GE.text.pdf.richmedia {

    /**
     * Object that is able to create Rich Media Annotations as described
     * in the document "Acrobat Supplement to the ISO 32000", referenced
     * in the code as "ExtensionLevel 3". This annotation is described in
     * section 9.6 entitled "Rich Media" of this document.
     * Extension level 3 introduces rich media PDF constructs that support
     * playing a SWF file and provide enhanced rich media. With rich media
     * annotation, Flash applications, video, audio, and other multimedia
     * can be attached to a PDF with expanded functionality. It improves upon
     * the existing 3D annotation structure to support multiple multimedia
     * file assets, including Flash video and compatible variations on the
     * H.264 format. The new constructs allow a two-way scripting bridge between
     * Flash and a conforming application. There is support for generalized
     * linking of a Flash application state to a comment or view, which enables
     * video commenting. Finally, actions can be linked to video chapter points.
     * @since   5.0.0
     */
    public class RichMediaAnnotation {
        /** The PdfWriter to which the annotation will be added. */
        protected PdfWriter writer;
        /** The annotation object */
        protected PdfAnnotation annot;
        /** the rich media content (can be reused for different annotations) */
        protected PdfDictionary richMediaContent = null;
        /** a reference to the RichMediaContent that can be reused. */
        protected PdfIndirectReference richMediaContentReference = null;
        /** the rich media settings (specific for this annotation) */
        protected PdfDictionary richMediaSettings = new PdfDictionary(PdfName.RICHMEDIASETTINGS);
        /** a map with the assets (will be used to construct a name tree.) */
        protected Dictionary<String, PdfIndirectReference> assetsmap = null;
        /** an array with configurations (will be added to the RichMediaContent). */
        protected PdfArray configurations = null;
        /** an array of views (will be added to the RichMediaContent) */
        protected PdfArray views = null;

        /**
         * Creates a RichMediaAnnotation.
         * @param   writer  the PdfWriter to which the annotation will be added.
         * @param   rect    the rectangle where the annotation will be added.
         */
        public RichMediaAnnotation(PdfWriter writer, Rectangle rect) {
            this.writer = writer;
            annot = writer.CreateAnnotation(rect, PdfName.RICHMEDIA);
            richMediaContent = new PdfDictionary(PdfName.RICHMEDIACONTENT);
            assetsmap = new Dictionary<string,PdfIndirectReference>();
            configurations = new PdfArray();
            views = new PdfArray();
        }

        /**
         * Creates a RichMediaAnnotation using rich media content that has already
         * been added to the writer. Note that assets, configurations, views added
         * to a RichMediaAnnotation created like this will be ignored.
         * @param   writer  the PdfWriter to which the annotation will be added.
         * @param   rect    the rectangle where the annotation will be added.
         * @param   richMediaContentReference   reused rich media content.
         */
        public RichMediaAnnotation(PdfWriter writer, Rectangle rect, PdfIndirectReference richMediaContentReference) {
            this.richMediaContentReference = richMediaContentReference;
            richMediaContent = null;
            this.writer = writer;
            annot = writer.CreateAnnotation(rect, PdfName.RICHMEDIA);
        }

        /**
         * Gets a reference to the RichMediaContent for reuse of the
         * rich media content. Returns null if the content hasn't been
         * added to the Stream yet.
         * @return  a PdfDictionary with RichMediaContent
         */
        virtual public PdfIndirectReference RichMediaContentReference {
            get {
                return richMediaContentReference;
            }
        }

        /**
         * Adds an embedded file.
         * (Part of the RichMediaContent.)
         * @param   name    a name for the name tree
         * @param   fs      a file specification for an embedded file.
         */
        virtual public PdfIndirectReference AddAsset(String name, PdfFileSpecification fs) {
            if (assetsmap == null)
                throw new IllegalPdfSyntaxException("You can't add assets to reused RichMediaContent.");
            PdfIndirectReference refi = writer.AddToBody(fs).IndirectReference;
            assetsmap[name] = refi;
            return refi;
        }

        /**
         * Adds a reference to an embedded file.
         * (Part of the RichMediaContent.)
         * @param   ref a reference to a PdfFileSpecification
         */
        virtual public PdfIndirectReference AddAsset(String name, PdfIndirectReference refi) {
            if (views == null)
                throw new IllegalPdfSyntaxException("You can't add assets to reused RichMediaContent.");
            assetsmap[name] = refi;
            return refi;
        }

        /**
         * Adds a RichMediaConfiguration.
         * (Part of the RichMediaContent.)
         * @param   configuration   a configuration dictionary
         */
        virtual public PdfIndirectReference AddConfiguration(RichMediaConfiguration configuration) {
            if (configurations == null)
                throw new IllegalPdfSyntaxException("You can't add configurations to reused RichMediaContent.");
            PdfIndirectReference refi = writer.AddToBody(configuration).IndirectReference;
            configurations.Add(refi);
            return refi;
        }

        /**
         * Adds a reference to a RichMediaConfiguration.
         * (Part of the RichMediaContent.)
         * @param   ref     a reference to a RichMediaConfiguration
         */
        virtual public PdfIndirectReference AddConfiguration(PdfIndirectReference refi) {
            if (configurations == null)
                throw new IllegalPdfSyntaxException("You can't add configurations to reused RichMediaContent.");
            configurations.Add(refi);
            return refi;
        }

        /**
         * Adds a view dictionary.
         * (Part of the RichMediaContent.)
         * @param   view    a view dictionary
         */
        virtual public PdfIndirectReference AddView(PdfDictionary view) {
            if (views == null)
                throw new IllegalPdfSyntaxException( "You can't add views to reused RichMediaContent.");
            PdfIndirectReference refi = writer.AddToBody(view).IndirectReference;
            views.Add(refi);
            return refi;
        }

        /**
         * Adds a reference to a view dictionary.
         * (Part of the RichMediaContent.)
         * @param   ref a reference to a view dictionary
         */
        virtual public PdfIndirectReference AddView(PdfIndirectReference refi) {
            if (views == null)
                throw new IllegalPdfSyntaxException("You can't add views to reused RichMediaContent.");
            views.Add(refi);
            return refi;
        }

        /**
         * Sets the RichMediaActivation dictionary specifying the style of
         * presentation, default script behavior, default view information,
         * and animation style when the annotation is activated.
         * (Part of the RichMediaSettings.)
         * @param   richMediaActivation
         */
        virtual public RichMediaActivation Activation {
            set {
                richMediaSettings.Put(PdfName.ACTIVATION, value);
            }
        }

        /**
         * Sets the RichMediaDeactivation dictionary specifying the condition
         * that causes deactivation of the annotation.
         * (Part of the RichMediaSettings.)
         * @param   richMediaDeactivation
         */
        virtual public RichMediaDeactivation Deactivation {
            set {
                richMediaSettings.Put(PdfName.DEACTIVATION, value);
            }
        }

        /**
         * Creates the actual annotation and adds different elements to the
         * PdfWriter while doing so.
         * @return  a PdfAnnotation
         */
        virtual public PdfAnnotation CreateAnnotation() {
            if (richMediaContent != null) {
                if (assetsmap.Count > 0) {
                    PdfDictionary assets = PdfNameTree.WriteTree(assetsmap, writer);
                    richMediaContent.Put(PdfName.ASSETS, writer.AddToBody(assets).IndirectReference);
                }
                if (configurations.Size > 0) {
                    richMediaContent.Put(PdfName.CONFIGURATION, writer.AddToBody(configurations).IndirectReference);
                }
                if (views.Size > 0) {
                    richMediaContent.Put(PdfName.VIEWS, writer.AddToBody(views).IndirectReference);
                }
                richMediaContentReference = writer.AddToBody(richMediaContent).IndirectReference;
            }
            writer.AddDeveloperExtension(PdfDeveloperExtension.ADOBE_1_7_EXTENSIONLEVEL3);
            annot.Put(PdfName.RICHMEDIACONTENT, richMediaContentReference);
            annot.Put(PdfName.RICHMEDIASETTINGS, writer.AddToBody(richMediaSettings).IndirectReference);
            return annot;
        }
    }
}
