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
        /// <param name="operandA">Полигон A</param>
        /// <param name="operandB">Полигон B</param>
        /// <param name="tmo">Индекс ТМО</param>
        /// <param name="width">Ширина полотна</param>
        /// <param name="height">Высота полотна</param>
        /// <returns>Результат ТМО</returns>
        public static Tuple<List<Point>, List<Point>> Exe(ITmoOperand operandA, ITmoOperand operandB, int tmo) {
            var setQ = CreateTmoQ(tmo);

            var xal = new List<int>();
            var xar = new List<int>();
            var xbl = new List<int>();
            var xbr = new List<int>();

            var ll = new List<Point>();
            var lr = new List<Point>();

            for (var y = 0; y < UiUtils.CanvasHeight; y++) {
                operandA.Bound(xal, xar, y);
                operandB.Bound(xbl, xbr, y);

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