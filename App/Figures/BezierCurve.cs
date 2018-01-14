using System;
using System.Drawing;
using App.Utils;

namespace App.Figures {

    /// <summary>
    /// Кривая Безье
    /// </summary>
    public class BezierCurve : Figure {

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="fillColor">Цвет заливки фигуры</param>
        public BezierCurve(Color fillColor) : base(fillColor) {
        }

        /// <summary>
        /// Отрисовка кривой Безье
        /// </summary>
        /// <param name="g">Компонент отрисовки</param>
        public override void Draw(Graphics g) {
            var pen = new Pen(FillColor);

            const float dt = 0.01f;
            var n = Vertex.Count - 1;
            var nFact = MathUtils.Factorial(n);

            var xPred = Vertex[0].X;
            var yPred = Vertex[0].Y;
            for (var t = dt; t < 1 + dt / 2; t += dt) {
                if (t > 1) {
                    t = 1;
                }

                float xt = 0;
                float yt = 0;
                for (var i = 0; i < Vertex.Count; i++) {
                    var j = (float) (Math.Pow(t, i)
                                     * Math.Pow(1 - t, n - i)
                                     * nFact / (MathUtils.Factorial(i) * MathUtils.Factorial(n - i)));
                    xt += Vertex[i].X * j;
                    yt += Vertex[i].Y * j;
                }

                g.DrawLine(pen, xPred, yPred, xt, yt);
                xPred = xt;
                yPred = yt;
            }
        }

    }

}