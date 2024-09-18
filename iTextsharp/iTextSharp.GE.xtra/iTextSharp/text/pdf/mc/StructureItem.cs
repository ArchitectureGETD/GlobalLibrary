namespace iTextSharp.GE.text.pdf.mc {

    /**
     * The abstract StructureItem class is extended by StructureMCID and StructureObject.
     */
    public abstract class StructureItem {

        /** The object number of the page to which this structure item belongs. */
        protected int pageref = -1;

        /**
         * Returns the number of the page object to which the structure item belongs.
         * @return a number of the reference of a page
         */
        virtual public int GetPageref() {
            return pageref;
        }

        /**
         * Checks if an MCID corresponds with the MCID stored in the StructureItem.
         * @param pageref	the page reference that needs to be checked
         * @param mcid		the MCID that needs to be checked
         * @return  0 in case there's no MCID (in case of a StructureObject),
         * 		    1 in case the MCID matches,
         * 		   -1 in case there's no match.
         */
        public virtual int CheckMCID(int pageref, int mcid) {
            return 0;
        }

        /**
         * Checks if a StructParent corresponds with the StructParent stored in the StructureItem.
         * @param pageref	the page reference that needs to be checked
         * @param structParent	the structParent that needs to be checked
         * @return  0 in case there's no StructParent (in case of a StructureMCID)
         *          1 in case the StructParent matches,
         *         -1 in case there's no match.
         */
        public virtual int CheckStructParent(int pageref, int structParent) {
            return 0;
        }
    }
}
