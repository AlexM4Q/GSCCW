using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace App.Figures {

    /// <summary>
    /// Полигон
    /// </summary>
    public class Polygon : Figure {

        public Color? BorderColor;

        public Polygon(Color fillColor, Color? borderColor = null) : base(fillColor) {
            BorderColor = borderColor;
        }

        public override void Draw(Graphics g) {
            var pgVertex = new Point[Vertex.Count];
            for (var i = 0; i < Vertex.Count; i++) {
                pgVertex[i].X = (int) Math.Round(Vertex[i].X);
                pgVertex[i].Y = (int) Math.Round(Vertex[i].Y);
            }

            g.FillPolygon(new SolidBrush(FillColor), pgVertex);

            if (BorderColor.HasValue)
                g.DrawPolygon(new Pen(BorderColor.Value, 3), pgVertex);
        }

        /// <summary>
        /// Определяет вхождение точки в полигон
        /// </summary>
        /// <param name="mouseX">Координата X</param>
        /// <param name="mouseY">Координата Y</param>
        /// <returns>true - если координата попадает в полигон, false - иначе</returns>
        public bool ThisPgn(int mouseX, int mouseY) {
            var xb = new List<int>();

            for (var i = 0; i < Vertex.Count; i++) {
                var k = i < Vertex.Count - 1 ? i + 1 : 0;
                var pi = Vertex[i];
                var pk = Vertex[k];
                if (pi.Y < mouseY && pk.Y >= mouseY || pi.Y >= mouseY && pk.Y < mouseY) {
                    xb.Add((int) Math.Round((mouseY - pi.Y) * (pk.X - Vertex[i].X) / (pk.Y - pi.Y) + pi.X));
                }
            }

            if (!xb.Any()) return false;

            xb.Sort(); // сортировка по возрастанию
            for (var i = 0; i < xb.Count; i += 2) {
                if (mouseX >= xb[i] && mouseX <= xb[i + 1]) {
                    return true;
                }
            }

            return false;
        }

    }

}