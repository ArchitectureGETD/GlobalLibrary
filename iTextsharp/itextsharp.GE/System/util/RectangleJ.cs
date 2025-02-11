using iTextSharp.GE.awt.geom;

namespace System.util {
    public class RectangleJ {
        public const int OUT_LEFT = 1;
        public const int OUT_TOP = 2;
        public const int OUT_RIGHT = 4;
        public const int OUT_BOTTOM = 8;

        private float x;
        private float y;
        private float width;
        private float height;

        public RectangleJ(float x, float y, float width, float height) {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }

        public RectangleJ(iTextSharp.GE.text.Rectangle rect) {
            rect.Normalize();
            x = rect.Left;
            y = rect.Bottom;
            width = rect.Width;
            height = rect.Height;
        }

        virtual public float X {
            get {
                return this.x;
            }
            set {
                this.x = value;
            }
        }
        virtual public float Y {
            get {
                return this.y;
            }
            set {
                this.y = value;
            }
        }
        virtual public float Width {
            get {
                return this.width;
            }
            set {
                this.width = value;
            }
        }
        virtual public float Height {
            get {
                return this.height;
            }
            set {
                this.height = value;
            }
        }

        virtual public void Add(RectangleJ rect) {
            float x1 = Math.Min(Math.Min(x, x + width), Math.Min(rect.x, rect.x + rect.width));
            float x2 = Math.Max(Math.Max(x, x + width), Math.Max(rect.x, rect.x + rect.width));
            float y1 = Math.Min(Math.Min(y, y + height), Math.Min(rect.y, rect.y + rect.height));
            float y2 = Math.Max(Math.Max(y, y + height), Math.Max(rect.y, rect.y + rect.height));
            x = x1;
            y = y1;
            width = x2 - x1;
            height = y2 - y1;
        }

        virtual public int Outcode(double x, double y) {
            int outp = 0;
            if (this.width <= 0) {
                outp |= OUT_LEFT | OUT_RIGHT;
            }
            else if (x < this.x) {
                outp |= OUT_LEFT;
            }
            else if (x > this.x + (double)this.width) {
                outp |= OUT_RIGHT;
            }
            if (this.height <= 0) {
                outp |= OUT_TOP | OUT_BOTTOM;
            }
            else if (y < this.y) {
                outp |= OUT_TOP;
            }
            else if (y > this.y + (double)this.height) {
                outp |= OUT_BOTTOM;
            }
            return outp;
        }

        virtual public bool IntersectsLine(double x1, double y1, double x2, double y2) {
            int out1, out2;
            if ((out2 = Outcode(x2, y2)) == 0) {
                return true;
            }
            while ((out1 = Outcode(x1, y1)) != 0) {
                if ((out1 & out2) != 0) {
                    return false;
                }
                if ((out1 & (OUT_LEFT | OUT_RIGHT)) != 0) {
                    float x = X;
                    if ((out1 & OUT_RIGHT) != 0) {
                        x += Width;
                    }
                    y1 = y1 + (x - x1) * (y2 - y1) / (x2 - x1);
                    x1 = x;
                }
                else {
                    float y = Y;
                    if ((out1 & OUT_BOTTOM) != 0) {
                        y += Height;
                    }
                    x1 = x1 + (y - y1) * (x2 - x1) / (y2 - y1);
                    y1 = y;
                }
            }
            return true;
        }

        virtual public RectangleJ Intersection(RectangleJ r) {
            float x1 = Math.Max(x, r.x);
            float y1 = Math.Max(y, r.y);
            float x2 = Math.Min(x + width, r.x + r.width);
            float y2 = Math.Min(y + height, r.y + r.height);
            return new RectangleJ(x1, y1, x2 - x1, y2 - y1);
        }

        virtual public bool IsEmpty() {
            return width <= 0 || height <= 0;
        }

        public virtual bool Contains(Point2D point) {
            return Contains(point.GetX(), point.GetY());
        }

        public virtual bool Contains(double x, double y) {
            if (IsEmpty()) {
                return false;
            }

            if (x < this.x || y < this.y) {
                return false;
            }

            x -= this.x;
            y -= this.y;

            return x < width && y < height;
        }
    }
}
