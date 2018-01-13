using System.Drawing;

namespace App.Figures {

    public interface ITmoOperand : IOperable<ITmoOperand> {

        Color FillColor { set; }

    }

}