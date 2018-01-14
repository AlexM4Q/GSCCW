using System;
using System.Drawing;

namespace App.Figures {

    /// <summary>
    /// Правильный полигон
    /// </summary>
    public class RegularPolygon : Polygon {

        /// <summary>
        /// Минимальное количество точек в полигоне
        /// </summary>
        private const int MinPointsCount = 3;

        /// <summary>
        /// Центр полигона (используется только при построение)
        /// </summary>
        public Point Center { get; }

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
        /// Количество точек полигона
        /// </summary>
        private int _pointsCount;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="fillColor">Цвет заливки фигуры</param>
        /// <param name="center">Центр полигона</param>
        /// <param name="radius">Радиус полигона</param>
        public RegularPolygon(Color fillColor, Point center, float radius = 100) : base(fillColor) {
            _pointsCount = MinPointsCount;

            Center = center;
            Radius = radius;
        }

        /// <summary>
        /// Добавление новой вершины к полигону
        /// </summary>
        /// <param name="newVertex">Новая точка полигона (не используется)</param>
        public override void Add(Point? newVertex = null) {
            _pointsCount++;
            RecalculateVertex();
        }

        /// <summary>
        /// Пересчет координат вершин полигона с учетом радиуса и количества точек
        /// </summary>
        private void RecalculateVertex() {
            if (_pointsCount < 3) {
                return;
            }

            Vertex.Clear();

            const double piM2 = 2 * Math.PI;
            const double piD2 = 0.5 * Math.PI;
            for (var i = 0; i < _pointsCount; i++) {
                var angle = piM2 * i / _pointsCount - piD2;
                base.Add(new Point(
                    Center.X + (int) (_radius * Math.Cos(angle)),
                    Center.Y + (int) (_radius * Math.Sin(angle))
                ));
            }
        }

    }

}