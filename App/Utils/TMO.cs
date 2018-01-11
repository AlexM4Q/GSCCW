using System;
using System.Collections.Generic;
using System.Drawing;
using App.Figures;

namespace App.Utils {

    public class Tmo {

        public static Polygon Exe(Polygon pgnA, Polygon pgnB, int tmo, int width, int height, Graphics g) {
            var setQ = new int[2];
            switch (tmo) {
                case 1:
                    setQ[0] = 1;
                    setQ[1] = 3;
                    break;
                case 2:
                    setQ[0] = 3;
                    setQ[1] = 3;
                    break;
                case 3:
                    setQ[0] = 1;
                    setQ[1] = 2;
                    break;
                case 4:
                    setQ[0] = 2;
                    setQ[1] = 2;
                    break;
                case 5:
                    setQ[0] = 1;
                    setQ[1] = 1;
                    break;
                default:
                    throw new ArgumentException($"Неожиданное значение ТМО: {tmo}");
            }

            var xal = new List<int>();
            var xar = new List<int>();
            var xbl = new List<int>();
            var xbr = new List<int>();

            var pgn = new Polygon(Color.Red);
            var ll = new List<Point>();
            var lr = new List<Point>();

            for (var y = 0; y < height; y++) {
                Bound(pgnA.Vertex, xal, xar, y);
                Bound(pgnB.Vertex, xbl, xbr, y);

                var Mx = new int[1000];
                var MdQ = new int[1000];

                for (int i = 0; i < xal.Count; i++) {
                    Mx[i] = xal[i];
                    MdQ[i] = 2;
                }

                var nM = xal.Count;

                for (int i = 0; i < xar.Count; i++) {
                    Mx[nM + i] = xar[i];
                    MdQ[nM + i] = -2;
                }

                nM = nM + xar.Count;

                for (int i = 0; i < xbl.Count; i++) {
                    Mx[nM + i] = xbl[i];
                    MdQ[nM + i] = 1;
                }

                nM = nM + xbl.Count;

                for (int i = 0; i < xbr.Count; i++) {
                    Mx[nM + i] = xbr[i];
                    MdQ[nM + i] = -1;
                }

                nM = nM + xbr.Count;

                for (var i = 0; i < Mx.Length; i++) {
                    for (var j = i + 1; j < Mx.Length; j++) {
                        if (Mx[i] <= Mx[j]) continue;

                        var temp1 = Mx[i];
                        Mx[i] = Mx[j];
                        Mx[j] = temp1;

                        temp1 = MdQ[i];
                        MdQ[i] = MdQ[j];
                        MdQ[j] = temp1;
                    }
                }

                int k = 0;
                int m = 0;
                int Q = 0;
                int x;
                int Qnew;
                int Xemin = 0;
                int Xemax = width;

                int[] Xrl = new int[1000];
                int[] Xrr = new int[1000];

                if ((Mx[0] >= Xemin) && (MdQ[0] < 0)) {
                    Xrl[0] = Xemin;
                    Q = -MdQ[0];
                    k = 1;
                }

                for (var i = 0; i < nM; i++) {
                    x = Mx[i];
                    Qnew = Q + MdQ[i];

                    if ((Q < setQ[0] || Q > setQ[1]) && Qnew >= setQ[0] && Qnew <= setQ[1]) {
                        Xrl[k] = x;
                        k = k + 1;
                    }

                    if (Q >= setQ[0] && Q <= setQ[1] && (Qnew < setQ[0] || Qnew > setQ[1])) {
                        Xrr[m] = x;
                        m = m + 1;
                    }

                    Q = Qnew;
                }

                if (Q >= setQ[0] && Q <= setQ[1]) {
                    Xrr[m] = Xemax;
                }

                for (var i = 0; i < nM; i++) {
                    g.DrawLine(new Pen(Color.Red), Xrl[i], y, Xrr[i], y);
                    //ll.Add(new Point(Xrl[i], y));
                    //lr.Add(new Point(Xrr[i], y));
                }
            }

            //ll.ForEach(pgn.Add);
            //lr.Reverse();
            //lr.ForEach(pgn.Add);

            //pgn.Draw(g);

            return pgn;
        }

        /// <summary>
        /// Поиск левых и правых границ фигуры
        /// </summary>
        /// <param name="points"></param>
        /// <param name="xl"></param>
        /// <param name="xr"></param>
        /// <param name="y"></param>
        private static void Bound(IList<PointF> points, List<int> xl, List<int> xr, int y) {
            xl.Clear();
            xr.Clear();

            for (var i = 0; i < points.Count; i++) {
                var k = i == points.Count - 1 ? 0 : i + 1;

                if (points[i].Y < y && points[k].Y >= y || points[i].Y >= y && points[k].Y < y) {
                    var p1 = new PointF(points[i].X, points[i].Y);
                    var p2 = new PointF(points[k].X, points[k].Y);
                    var p3 = new PointF(-1000, y);
                    var p4 = new PointF(1000, y);

                    var p5 = Cross(p1, p2, p3, p4);

                    if (p2.Y * p5.Y - p1.Y * p5.Y > 0) {
                        xr.Add((int) p5.X);
                    } else {
                        xl.Add((int) p5.X);
                    }
                }
            }

            xl.Sort();
            xr.Sort();
        }

        private static PointF Cross(PointF p1, PointF p2, PointF p3, PointF p4) {
            if (p3.X == p4.X) {
                var y = p1.Y + ((p2.Y - p1.Y) * (p3.X - p1.X)) / (p2.X - p1.X);
                if (y > Math.Max(p3.Y, p4.Y) || y < Math.Min(p3.Y, p4.Y)
                                             || y > Math.Max(p1.Y, p2.Y) || y < Math.Min(p1.Y, p2.Y))
                    return new Point(0, 0);

                return new PointF(p3.X, y);
            }

            var x = p1.X + ((p2.X - p1.X) * (p3.Y - p1.Y)) / (p2.Y - p1.Y);
            if (x > Math.Max(p3.X, p4.X) || x < Math.Min(p3.X, p4.X)
                                         || x > Math.Max(p1.X, p2.X) || x < Math.Min(p1.X, p2.X))
                return new Point(0, 0);

            return new PointF(x, p3.Y);
        }

    }

}