using System.Drawing;

namespace App.Figures {

    /// <summary>
    /// Базовый класс отрисовываемых объектов
    /// </summary>
    public abstract class Drawable {

        /// <summary>
        /// Цвет заливки фигуры
        /// </summary>
        public Color FillColor { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="fillColor">Цвет заливки фигуры</param>
        protected Drawable(Color fillColor) {
            FillColor = fillColor;
        }

        /// <summary>
        /// Отрисовка
        /// </summary>
        /// <param name="g">Компонент отрисовки</param>
        public abstract void Draw(Graphics g);

        /// <summary>
        /// Очистка состояиня отрисовываемого объекта
        /// </summary>
        public abstract void Clear();

    }

}