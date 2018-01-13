using System;
using System.Drawing;

namespace App.Figures {

    /// <summary>
    /// Правильный полигон
    /// </summary>
    public class RegularPolygon : Polygon {

        /// <summary>
        /// Радиус полигона
        /// </summary>
        private float _radius;

        public float Radius {
            get => _radius;
            set {
                _radius = Math.Abs(value);
                RecalculateVertex();
            }
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="fillColor">Цвет заливки фигуры</param>
        /// <param name="borderColor">Цвет контура фигуры</param>
        /// <param name="radius">Радиус полигона</param>
        public RegularPolygon(Color fillColor, Color? borderColor = null, float radius = 1) : base(fillColor, borderColor) {
            Radius = radius;
        }

        /// <summary>
        /// Добавление новой вершины к полигону
        /// </summary>
        /// <param name="newVertex"></param>
        public override void Add(Point newVertex) {
            base.Add(newVertex);
            RecalculateVertex();
        }

        /// <summary>
        /// Отрисовка полигона
        /// </summary>
        /// <param name="g">Комопнент отрисовки</param>
        public override void Draw(Graphics g) {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Пересчет координат вершин полигона с учетом радиуса и количества точек
        /// </summary>
        private void RecalculateVertex() {
            if (Vertex.Count < 3) {
                return;
            }

            throw new NotImplementedException();
        }

    }

}