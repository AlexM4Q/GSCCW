using System.Drawing;

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
            throw new System.NotImplementedException();
        }

    }

}