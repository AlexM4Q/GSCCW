using System.Windows.Forms;

namespace App.Utils {

    /// <summary>
    /// Вспомогательный класс интерфейса
    /// </summary>
    public class UiUtils {

        /// <summary>
        /// Ширина полотна
        /// </summary>
        public static int CanvasWidth { get; set; }

        /// <summary>
        /// Высота полотна
        /// </summary>
        public static int CanvasHeight { get; set; }

        /// <summary>
        /// Отображение окна с информацией
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="caption">Заголовок</param>
        public static void ShowInfo(string message, string caption = "") {
            MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

    }

}