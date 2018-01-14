using System.Collections.Generic;
using System.Drawing;

namespace App.Figures {

    public interface ITmoOperand : IOperable<ITmoOperand> {

        /// <summary>
        /// Цвет операнда
        /// </summary>
        Color FillColor { set; }

        /// <summary>
        /// Поиск левых и правых границ операнда
        /// </summary>
        /// <param name="xl">Контейнер левый границ</param>
        /// <param name="xr">Контейнер правых границ</param>
        /// <param name="y">Уровень горинтали Y</param>
        void Bound(List<int> xl, List<int> xr, int y);

    }

}