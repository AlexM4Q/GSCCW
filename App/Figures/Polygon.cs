using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace App.Figures {

    /// <summary>
    /// Полигон
    /// </summary>
    public class Polygon : Figure, ITmoOperand {

        /// <summary>
        /// Цвет контура фигуры
        /// </summary>
        public Color? BorderColor { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="fillColor">Цвет заливки фигуры</param>
        /// <param name="borderColor">Цвет контура фигуры</param>
        public Polygon(Color fillColor, Color? borderColor = null) : base(fillColor) {
            BorderColor = borderColor;
        }

        /// <summary>
        /// Отрисовка полигона
        /// </summary>
        /// <param name="g">Комопнент отрисовки</param>
        public override void Draw(Graphics g) {
            var pgVertex = new Point[Vertex.Count];
            for (var i = 0; i < Vertex.Count; i++) {
                pgVertex[i].X = (int) Math.Round(Vertex[i].X);
                pgVertex[i].Y = (int) Math.Round(Vertex[i].Y);
            }

            g.FillPolygon(new SolidBrush(FillColor), pgVertex);

            if (BorderColor.HasValue) {
                g.DrawPolygon(new Pen(BorderColor.Value, 3), pgVertex);
            }
        }

        /// <summary>
        /// Проверяет вхождение точки в полигон
        /// </summary>
        /// <param name="point">Точка</param>
        /// <returns>true - если координата попадает в полигон, false - иначе</returns>
        public bool ContainsPoint(PointF point) {
            return ContainsPoint(point.X, point.Y);
        }

        /// <summary>
        /// Проверяет вхождение точки в полигон
        /// </summary>
        /// <param name="x">Координата X</param>
        /// <param name="y">Координата Y</param>
        /// <returns>true - если координата попадает в полигон, false - иначе</returns>
        public bool ContainsPoint(float x, float y) {
            var xb = new List<int>();

            for (var i = 0; i < Vertex.Count; i++) {
                var k = i < Vertex.Count - 1 ? i + 1 : 0;
                var pi = Vertex[i];
                var pk = Vertex[k];
                if (pi.Y < y && pk.Y >= y || pi.Y >= y && pk.Y < y) {
                    xb.Add((int) Math.Round((y - pi.Y) * (pk.X - Vertex[i].X) / (pk.Y - pi.Y) + pi.X));
                }
            }

            if (!xb.Any()) return false;

            xb.Sort();
            for (var i = 0; i < xb.Count; i += 2) {
                if (x >= xb[i] && x <= xb[i + 1]) {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Определяет наложение одного полигона на другой
        /// </summary>
        /// <param name="other">Другая фигура</param>
        /// <returns>true - еслиесть наложение, false - иначе</returns>
        public bool IsOverlaps(ITmoOperand other) {
            switch (other) {
                case Polygon polygon:
                    foreach (var point in Vertex) {
                        if (polygon.ContainsPoint(point)) {
                            return true;
                        }
                    }

                    foreach (var point in polygon.Vertex) {
                        if (ContainsPoint(point)) {
                            return true;
                        }
                    }

                    break;
                case TmoObject tmoObject:
                    return tmoObject.IsOverlaps(this);
            }

            return false;
        }

    }

}