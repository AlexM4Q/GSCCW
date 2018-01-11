using System;
using System.Collections.Generic;
using System.Drawing;

namespace App.Figures {

    /// <summary>
    /// Базовый класс фигуры
    /// </summary>
    public abstract class Figure : Drawable {

        public List<PointF> Vertex { get; }

        protected Figure(Color fillColor) : base(fillColor) {
            Vertex = new List<PointF>();
        }

        /// <summary>
        /// Очистка списка вершин фигуры
        /// </summary>
        public override void Clear() {
            Vertex.Clear();
        }

        /// <summary>
        /// Добавление новой вершины к фигуре
        /// </summary>
        /// <param name="newVertex"></param>
        public virtual void Add(Point newVertex) {
            Vertex.Add(newVertex);
        }

        /// <summary>
        /// Перемещение фигуры
        /// </summary>
        /// <param name="dx">Смещение координаты X</param>
        /// <param name="dy">Смещение координаты Y</param>
        public void Move(int dx, int dy) {
            for (var i = 0; i < Vertex.Count; i++) {
                Vertex[i] = new PointF(
                    Vertex[i].X + dx,
                    Vertex[i].Y + dy
                );
            }
        }

        /// <summary>
        /// Вращение фигуры
        /// </summary>
        /// <param name="center">Центр вращения</param>
        /// <param name="f">угл вращения в радианах</param>
        public void Rotate(PointF center, double f) {
            for (var i = 0; i < Vertex.Count; i++) {
                var originX = Vertex[i].X - center.X;
                var originY = Vertex[i].Y - center.Y;
                Vertex[i] = new PointF(
                    center.X + (float) (originX * Math.Cos(f) - originY * Math.Sin(f)),
                    center.Y + (float) (originX * Math.Sin(f) + originY * Math.Cos(f))
                );
            }
        }

        /// <summary>
        /// Масштабирование фигуры
        /// </summary>
        /// <param name="center">Центр масштабирования</param>
        /// <param name="f">Множитель масштабирования</param>
        public void Scale(PointF center, double f) {
            for (var i = 0; i < Vertex.Count; i++) {
                Vertex[i] = new PointF(
                    center.X + (float) ((Vertex[i].X - center.X) * f),
                    center.Y + (float) ((Vertex[i].Y - center.Y) * f)
                );
            }
        }

    }

}