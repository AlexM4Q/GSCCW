using System.Drawing;

namespace App.Figures {

    /// <summary>
    /// Интерфейс отрисовываемых объектов
    /// </summary>
    public abstract class Drawable {

        public Color FillColor;

        protected Drawable(Color fillColor) {
            FillColor = fillColor;
        }

        /// <summary>
        /// Отрисовка
        /// </summary>
        /// <param name="g"></param>
        public abstract void Draw(Graphics g);

        /// <summary>
        /// Очистка состояиня отрисовываемого объекта
        /// </summary>
        public abstract void Clear();

    }

}