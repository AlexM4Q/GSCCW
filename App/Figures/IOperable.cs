using System.Drawing;

namespace App.Figures {

    public interface IOperable<in T> where T : IOperable<T> {

        /// <summary>
        /// Проверяет вхождение точки в полигон
        /// </summary>
        /// <param name="x">Координата X</param>
        /// <param name="y">Координата Y</param>
        /// <returns>true - если координата попадает в полигон, false - иначе</returns>
        bool ContainsPoint(float x, float y);

        /// <summary>
        /// Определяет наложение одного объекта на другой
        /// </summary>
        /// <param name="other">Другая фигура</param>
        /// <returns>true - еслиесть наложение, false - иначе</returns>
        bool IsOverlaps(T other);

        /// <summary>
        /// Перемещение
        /// </summary>
        /// <param name="dx">Смещение координаты X</param>
        /// <param name="dy">Смещение координаты Y</param>
        void Move(int dx, int dy);

        /// <summary>
        /// Вращение
        /// </summary>
        /// <param name="center">Центр вращения</param>
        /// <param name="f">угл вращения в радианах</param>
        void Rotate(PointF center, double f);

        /// <summary>
        /// Масштабирование
        /// </summary>
        /// <param name="center">Центр масштабирования</param>
        /// <param name="f">Множитель масштабирования</param>
        void Scale(PointF center, double f);

    }

}