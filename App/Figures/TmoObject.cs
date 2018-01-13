using System;
using System.Collections.Generic;
using System.Drawing;
using App.Utils;

namespace App.Figures {

    public class TmoObject : Drawable, ITmoOperand {

        private readonly int _tmo;

        private readonly ITmoOperand _operand1;

        private readonly ITmoOperand _operand2;

        private List<Point> _leftSide;

        private List<Point> _rightSide;

        public TmoObject(Color fillColor, int tmo, ITmoOperand operand1, ITmoOperand operand2) : base(fillColor) {
            _tmo = tmo;
            _operand1 = operand1;
            _operand2 = operand2;
            RecalculateSides();
        }

        public override void Draw(Graphics g) {
            if (_leftSide.Count != _rightSide.Count) {
                throw new ArgumentException("Количество кординат правой и левой границы не совпадают");
            }

            for (var i = 0; i < _leftSide.Count; i++) {
                g.DrawLine(new Pen(FillColor), _leftSide[i], _rightSide[i]);
            }
        }

        public override void Clear() {
            _leftSide.Clear();
            _rightSide.Clear();
        }

        public bool ContainsPoint(float x, float y) {
            return _operand1.ContainsPoint(x, y) || _operand2.ContainsPoint(x, y);
        }

        public bool IsOverlaps(ITmoOperand other) {
            return _operand1 == other || _operand2 == other || _operand1.IsOverlaps(other) || _operand2.IsOverlaps(other);
        }

        public void Move(int dx, int dy) {
            _operand1.Move(dx, dy);
            _operand2.Move(dx, dy);
            RecalculateSides();
        }

        public void Rotate(PointF center, double f) {
            _operand1.Rotate(center, f);
            _operand2.Rotate(center, f);
            RecalculateSides();
        }

        public void Scale(PointF center, double f) {
            _operand1.Scale(center, f);
            _operand2.Scale(center, f);
            RecalculateSides();
        }

        private void RecalculateSides() {
            switch (_operand1) {
                case Polygon polygon1:
                    switch (_operand2) {
                        case Polygon polygon2:
                            var tuple = Tmo.Exe(polygon1, polygon2, _tmo);
                            _leftSide = tuple.Item1;
                            _rightSide = tuple.Item2;
                            break;
                        case TmoObject tmoObject2:
                            throw new NotImplementedException();
                            break;
                    }

                    break;
                case TmoObject tmoObject1:
                    throw new NotImplementedException();
                    break;
            }
        }

    }

}