using System;
using System.Collections.Generic;
using System.Drawing;
using App.Figures;

namespace App.Utils {

    /// <summary>
    /// Класс выполнения ТМО
    /// </summary>
    public class Tmo {

        /// <summary>
        /// Выполнение ТМО
        /// </summary>
        /// <param name="pgnA">Полигон A</param>
        /// <param name="pgnB">Полигон B</param>
        /// <param name="tmo">Индекс ТМО</param>
        /// <param name="width">Ширина полотна</param>
        /// <param name="height">Высота полотна</param>
        /// <returns>Результат ТМО</returns>
        public static Tuple<List<Point>, List<Point>> Exe(Polygon pgnA, Polygon pgnB, int tmo) {
            var setQ = CreateTmoQ(tmo);

            var xal = new List<int>();
            var xar = new List<int>();
            var xbl = new List<int>();
            var xbr = new List<int>();

            var ll = new List<Point>();
            var lr = new List<Point>();

            for (var y = 0; y < UiUtils.CanvasHeight; y++) {
                Bound(pgnA.Vertex, xal, xar, y);
                Bound(pgnB.Vertex, xbl, xbr, y);

                var totalM = xal.Count + xar.Count + xbl.Count + xbr.Count;
                if (totalM == 0) continue;

                var mx = new List<int>(totalM);
                var mdQ = new List<int>(totalM);

                foreach (var x in xal) {
                    mx.Add(x);
                    mdQ.Add(2);
                }

                foreach (var x in xar) {
                    mx.Add(x);
                    mdQ.Add(-2);
                }

                foreach (var x in xbl) {
                    mx.Add(x);
                    mdQ.Add(1);
                }

                foreach (var x in xbr) {
                    mx.Add(x);
                    mdQ.Add(-1);
                }

                for (var i = 0; i < totalM; i++) {
                    for (var j = i + 1; j < totalM; j++) {
                        if (mx[i] <= mx[j]) continue;

                        var temp1 = mx[i];
                        mx[i] = mx[j];
                        mx[j] = temp1;

                        temp1 = mdQ[i];
                        mdQ[i] = mdQ[j];
                        mdQ[j] = temp1;
                    }
                }

                var q = 0;
                var xEmin = 0;
                var xEmax = UiUtils.CanvasWidth;

                var xrl = new List<int>();
                var xrr = new List<int>();

                if (mx[0] >= xEmin && mdQ[0] < 0) {
                    xrl.Add(xEmin);
                    q = -mdQ[0];
                }

                for (var i = 0; i < totalM; i++) {
                    var x = mx[i];
                    var qNew = q + mdQ[i];

                    if ((q < setQ[0] || q > setQ[1]) && qNew >= setQ[0] && qNew <= setQ[1]) {
                        xrl.Add(x);
                    }

                    if (q >= setQ[0] && q <= setQ[1] && (qNew < setQ[0] || qNew > setQ[1])) {
                        xrr.Add(x);
                    }

                    q = qNew;
                }

                if (q >= setQ[0] && q <= setQ[1]) {
                    xrr.Add(xEmax);
                }

                if (xrl.Count != xrr.Count) {
                    throw new InvalidOperationException($"Размеры правой и левой границе не совпадают. xrr: {xrl.Count}, xrl: {xrr.Count}");
                }

                for (var i = 0; i < xrl.Count; i++) {
                    ll.Add(new Point(xrl[i], y));
                    lr.Add(new Point(xrr[i], y));
                }
            }

            return new Tuple<List<Point>, List<Point>>(ll, lr);
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

            var xb = new List<int>();
            for (var i = 0; i < points.Count; i++) {
                var k = i == points.Count - 1 ? 0 : i + 1;

                var currPoint = points[i];
                var nextPoint = points[k];

                if (currPoint.Y < y && nextPoint.Y >= y || currPoint.Y >= y && nextPoint.Y < y) {
                    xb.Add((int) Cross(currPoint, nextPoint, y));
                }
            }

            xb.Sort();
            for (var i = 0; i < xb.Count; i += 2) {
                xl.Add(xb[i]);
                xr.Add(xb[i + 1]);
            }
        }

        /// <summary>
        /// Определение абсциссы пересечения горизонтальной прямой Y
        /// и прямой образованной точками P1 и P2
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="y"></param>
        /// <returns>абсцисса</returns>
        private static float Cross(PointF p1, PointF p2, float y) {
            if (p1.X == p2.X) {
                return p1.X;
            }

            if (p1.Y == y) {
                return p1.X;
            }

            if (p2.Y == y) {
                return p2.X;
            }

            var k = (p1.Y - p2.Y) / (p1.X - p2.X);
            var b = p1.Y - k * p1.X;

            return (y - b) / k;
        }

        private static int[] CreateTmoQ(int tmo) {
            switch (tmo) {
                case 1: return new[] {1, 3};
                case 2: return new[] {3, 3};
                case 3: return new[] {1, 2};
                case 4: return new[] {2, 2};
                case 5: return new[] {1, 1};
                default:
                    throw new ArgumentException($"Неожиданное значение ТМО: {tmo}");
            }
        }

    }

}