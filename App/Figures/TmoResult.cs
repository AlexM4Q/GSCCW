using System;
using System.Collections.Generic;
using System.Drawing;

namespace App.Figures {

    public class TmoResult : Drawable {

        private readonly List<int> _ys;

        private readonly List<int> _leftSide;

        private readonly List<int> _rightSide;

        public TmoResult(Color fillColor, IEnumerable<int> ys, IEnumerable<int> leftSide, IEnumerable<int> rightSide) : base(fillColor) {
            _ys = new List<int>(ys);
            _leftSide = new List<int>(leftSide);
            _rightSide = new List<int>(rightSide);
        }

        public override void Draw(Graphics g) {
            if (_leftSide.Count != _rightSide.Count) {
                throw new ArgumentException("Количество кординат правой и левой границы не совпадают");
            }

            foreach (var y in _ys) {
                for (var i = 0; i < _leftSide.Count; i++) {
                    g.DrawLine(new Pen(Color.Red), _leftSide[i], y, _rightSide[i], y);
                }
            }
        }

        public override void Clear() {
            _ys.Clear();
            _leftSide.Clear();
            _rightSide.Clear();
        }

    }

}