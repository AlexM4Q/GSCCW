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

        private bool _flippedVertically;

        private bool _flippedHorizontally;

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

            List<PointF>[] newSides = null;
            var drawLeftSide = _leftSide;
            var drawRightSide = _rightSide;

            if (_flippedVertically) {
                newSides = MathUtils.FlipVertically(_leftSide.ToPointFs(), _rightSide.ToPointFs());
            }

            if (_flippedHorizontally) {
                newSides = newSides == null
                    ? MathUtils.FlipHorizontally(_leftSide.ToPointFs(), _rightSide.ToPointFs())
                    : MathUtils.FlipHorizontally(newSides);
            }

            if (newSides != null) {
                drawLeftSide = newSides[0].ToPoints();
                drawRightSide = newSides[1].ToPoints();
            }

            for (var i = 0; i < _leftSide.Count; i++) {
                g.DrawLine(new Pen(FillColor), drawLeftSide[i], drawRightSide[i]);
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

        public void FlipVertically() {
            _flippedVertically = !_flippedVertically;
        }

        public void FlipHorizontally() {
            _flippedHorizontally = !_flippedHorizontally;
        }

        private void RecalculateSides() {
            if (_operand1 is TmoObject tmoObject1) {
                tmoObject1.RecalculateSides();
            }

            if (_operand2 is TmoObject tmoObject2) {
                tmoObject2.RecalculateSides();
            }

            var tuple = Tmo.Exe(_operand1, _operand2, _tmo);
            _leftSide = tuple.Item1;
            _rightSide = tuple.Item2;
        }

        public void Bound(List<int> xl, List<int> xr, int y) {
            xl.Clear();
            xr.Clear();

            foreach (var point in _leftSide) {
                if (point.Y == y) {
                    xl.Add(point.X);
                }
            }

            foreach (var point in _rightSide) {
                if (point.Y == y) {
                    xr.Add(point.X);
                }
            }

            if (xl.Count != xr.Count) {
                throw new InvalidOperationException($"Размеры правой и левой границе не совпадают. xrr: {xl.Count}, xrl: {xr.Count}");
            }

            xl.Sort();
            xr.Sort();
        }

    }

}