using System.Collections.Generic;
using System.Globalization;

namespace System.util {
    /// <summary>
    /// Summary description for Util.
    /// </summary>
    public static class Util {
        public static int USR(int op1, int op2) {        
            if (op2 < 1) {
                return op1;
            } else {
                return unchecked((int)((uint)op1 >> op2));
            }
        }

        public static bool EqualsIgnoreCase(string s1, string s2) {
            return CultureInfo.InvariantCulture.CompareInfo.Compare(s1, s2, CompareOptions.IgnoreCase) == 0;
        }

        public static int CompareToIgnoreCase(string s1, string s2) {
            return CultureInfo.InvariantCulture.CompareInfo.Compare(s1, s2, CompareOptions.IgnoreCase);
        }

        public static CultureInfo GetStandartEnUSLocale() {
            CultureInfo locale = (CultureInfo)CultureInfo.InvariantCulture.Clone();
            //                          en-US                        Invariant
            //=====================     ==================           ==================
            //Currency Symbol           $                            �
            //Currency                  $123456.78                   �123456.78
            //Short Date                1/11/2012                    01/11/2012
            //Time                      10:36:52 PM                  22:36:52
            //Metric                    No                           Yes
            //Long Date                 Wednesday, January 11, 2012  Wednesday, 11 January, 2012
            //Year Month                January, 2012                2012 January
            locale.NumberFormat.CurrencySymbol = "$";
            locale.DateTimeFormat.ShortDatePattern = "M/d/yyyy";
            locale.DateTimeFormat.ShortTimePattern = "h:mm tt";
            locale.DateTimeFormat.LongDatePattern = "dddd, MMMM dd, yyyy";
            locale.DateTimeFormat.YearMonthPattern = "MMMM, yyyy";
            return locale;
        }

        public static int GetArrayHashCode<T>(T[] a) {
            if (a == null)
                return 0;

            int result = 1;
            
            foreach (T element in a) {
                result = 31*result + (element == null ? 0 : element.GetHashCode());
            }

            return result;
        }

        public static int Compare(float f1, float f2) {
            if (f1 < f2) {
                return -1;
            }

            if (f1 > f2) {
                return 1;
            }

            int f1Bits = BitConverter.ToInt32(BitConverter.GetBytes(f1), 0);
            int f2Bits = BitConverter.ToInt32(BitConverter.GetBytes(f2), 0);

            return (f1Bits == f2Bits ? 0 : (f1Bits < f2Bits ? -1 : 1));                         
        }

        public static bool ArraysAreEqual<T>(T[] a, T[] b) {
            if (a == b)
                return true;

            if (a == null || b == null)
                return false;

            int length = a.Length;
            if (b.Length != length)
                return false;

            for (int i = 0; i < length; i++) {
                Object o1 = a[i];
                Object o2 = b[i];
                if (!(o1 == null ? o2 == null : o1.Equals(o2)))
                    return false;
            }

            return true;
        }

        public static bool AreEqual<T>(Stack<T> s1, Stack<T> s2) {
            if (s1.Count != s2.Count) {
                return false;
            }

            IEnumerator<T> e1 = s1.GetEnumerator();
            IEnumerator<T> e2 = s2.GetEnumerator();

            while (e1.MoveNext() && e2.MoveNext()) {
                if (e1.Current == null && e2.Current == null) {
                    continue;
                }

                if (e1 == null || !e1.Current.Equals(e2.Current)) {
                    return false;
                }
            }

            return true;
        }

        public static T Min<T>(T[] array) {
            if (array.Length == 0) {
                throw new InvalidOperationException();
            }

            T result = array[0];

            for (int i = 1; i < array.Length; ++i) {
                if (Comparer<T>.Default.Compare(result, array[i]) > 0) {
                    result = array[i];
                }
            }

            return result;
        }

        public static T Max<T>(T[] array) {
            if (array.Length == 0) {
                throw new InvalidOperationException();
            }

            T result = array[0];

            for (int i = 1; i < array.Length; ++i) {
                if (Comparer<T>.Default.Compare(result, array[i]) < 0) {
                    result = array[i];
                }
            }

            return result;
        }

        public static void AddAll<T>(ICollection<T> dest, IEnumerable<T> source, int srcStartFrom) {
            IEnumerator<T> enumerator = source.GetEnumerator();

            while (srcStartFrom != 0 && enumerator.MoveNext()) {
                --srcStartFrom;
            }

            while (enumerator.MoveNext()) {
                dest.Add(enumerator.Current);
            }
        }

        public static void AddAll<T>(ICollection<T> dest, IEnumerable<T> source) {
            AddAll(dest, source, 0);
        }

        public static void AddAll<T>(Queue<T> to, IEnumerable<T> from) {
            foreach (T elem in from) {
                to.Enqueue(elem);
            }
        }
    }
}
