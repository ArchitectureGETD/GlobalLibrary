using System;
using iTextSharp.GE.text.pdf;
namespace iTextSharp.GE.text.pdf.spatial {

    /**
     * A projected coordinate system (PROJCS), which includes an embedded GEOGCS,
     * specifies the algorithms and associated parameters used to transform points
     * between geographic coordinates and a two-dimensional (projected) coordinate system.
     * @since 5.1.0
     */
    public class ProjectedCoordinateSystem : CoordinateSystem {

        /**
         * Creates a ProjectedCoordinateSystem.
         */
        public ProjectedCoordinateSystem() : base(PdfName.PROJCS) {
        }
    }
}
