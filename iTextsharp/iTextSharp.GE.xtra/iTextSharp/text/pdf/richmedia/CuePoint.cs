using System;
using iTextSharp.GE.text.pdf;
using iTextSharp.GE.text.exceptions;

namespace iTextSharp.GE.text.pdf.richmedia {

    /**
     * A video file can contain cue points that are encoded in a video stream
     * or may be created by an associated ActionScript within the Flash content.
     * The CuePoint dictionary contains a state that relates the cue points to
     * an action that may be passed to the conforming application or may be used
     * to change the appearance. Cue points in the Flash content are matched to
     * the cue points declared in the PDF file by the values specified by the
     * Name or Time keys. (See ExtensionLevel 3 p91)
     * @since   5.0.0
     */
    public class CuePoint : PdfDictionary {

        /**
         * Constructs a CuePoint object.
         * A <code>Navigation</code> cue point is an event encoded in a Flash movie (FLV).
         * A chapter stop may be encoded so that when the user requests to go to or skip
         * a chapter, a navigation cue point is used to indicate the location of the chapter.
         * An <code>Event</code> is a generic cue point of no specific significance other
         * than a corresponding action is triggered.
         * @param   subtype possible values: PdfName.NAVIGATION or PdfName.EVENT
         */
        public CuePoint(PdfName subtype) : base(PdfName.CUEPOINT) {
            Put(PdfName.SUBTYPE, subtype);
        }
        
        /**
         * Set the name of the cue point to match against the cue point within
         * Flash content and for display purposes.
         * @param   name    the name of the cue point
         */
        virtual public PdfString Name {
            set {
                Put(PdfName.NAME, value);
            }
        }
        
        /**
         * Sets the time value of the cue point in milliseconds to match against
         * the cue point within Flash content and for display purposes.
         * @param   time    the time value of the cue point
         */
        virtual public int Time {
            set {
                Put(PdfName.TIME, new PdfNumber(value));
            }
        }

        /**
         * Sets an action dictionary defining the action that is executed
         * if this cue point is triggered, meaning that the Flash content
         * reached the matching cue point during its playback.
         * @param   action  an action
         */
        virtual public PdfObject Action {
            set {
                if (value is PdfDictionary || value is PdfIndirectReference)
                    Put(PdfName.A, value);
                else
                    throw new IllegalPdfSyntaxException("An action should be defined as a dictionary");
            }
        }
    }
}
