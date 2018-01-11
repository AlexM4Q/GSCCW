using System;
using System.Windows.Forms;

namespace App {

    /// <summary>
    /// Вариант 14.
    /// 
    /// Примитивы:
    ///     Кривая Бизье; (BZ)
    ///     Проивольный многоугольник; (FPg)
    ///     Правильный многоугольник. (Pgn)
    /// Геометрические преобразования:
    ///     Поворот вокруг центра фигуры на произвольный угол; (Rf)
    ///     Зеркальное отражение относительно вертикальной прямой; (SV)
    ///     Зеркальное отражение относительно горизонтальной прямой. (SH)
    /// ТМО:
    ///     Объединение;
    ///     Пересечение.
    /// </summary>
    internal static class Program {

        /// <summary>
        /// Главная точка входа приложения
        /// </summary>
        [STAThread]
        public static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

    }

}