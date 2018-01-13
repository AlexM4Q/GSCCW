using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using App.Figures;
using App.Utils;

namespace App {

    public partial class Form1 : Form {

        private static readonly Color PrimaryColor = Color.Orange;
        private static readonly Color SecondaryColor = Color.LightSeaGreen;

        private readonly Pen _drawPen = new Pen(Color.Black, 1);
        private readonly Bitmap _image;
        private readonly Graphics _g;
        private readonly List<Point> _vertexList = new List<Point>();
        private readonly List<Drawable> _figureList = new List<Drawable>();
        private ITmoOperand _primaryPgn;
        private ITmoOperand _secondaryPgn;
        private int _operation = 1;
        private Point _mouseStartPosition;

        public Form1() {
            InitializeComponent();
            _image = new Bitmap(PictureBox.Width, PictureBox.Height);
            _g = Graphics.FromImage(_image);

            UiUtils.CanvasWidth = PictureBox.Width;
            UiUtils.CanvasHeight = PictureBox.Height;
        }

        /// <summary>
        /// Перерисовка всех объектов на полотне
        /// </summary>
        private void Redraw() {
            _g.Clear(PictureBox.BackColor);
            _figureList.ForEach(f => f.Draw(_g));
            PictureBox.Image = _image;
        }

        /// <summary>
        /// Ввод списка вершин и рисование
        /// </summary>
        /// <param name="e"></param>
        private void InputPgn(MouseEventArgs e) {
            var newP = new Point(e.X, e.Y);
            _vertexList.Add(newP);

            var k = _vertexList.Count;
            if (k > 1) {
                _g.DrawLine(_drawPen, _vertexList[k - 2], _vertexList[k - 1]);
            } else {
                _g.DrawRectangle(_drawPen, e.X, e.Y, 1, 1);
            }

            // Конец ввода
            if (e.Button == MouseButtons.Right) {
                var pgn = new Polygon(_drawPen.Color);
                _g.DrawLine(new Pen(Color.Blue), _vertexList[k - 1], _vertexList[0]);
                _vertexList.ForEach(pgn.Add);
                _vertexList.Clear();
                _figureList.Add(pgn);
                pgn.Draw(_g);
            }
        }

        private void Clear_Click(object sender, EventArgs e) {
            _g.Clear(PictureBox.BackColor);
            _figureList.ForEach(p => p.Clear());
            _figureList.Clear();
            _operation = 1;
            PictureBox.Image = _image;
        }

        private void Draw_Click(object sender, EventArgs e) {
            _operation = 1; // Задает режим рисования фигуры
        }

        private void Move_Click(object sender, EventArgs e) {
            _operation = 2; // Задает режим перемещения фигуры
        }

        private void Rotate_Click(object sender, EventArgs e) {
            _operation = 3; // Задает режим вращения фигуры
        }

        private void Scale_Click(object sender, EventArgs e) {
            _operation = 4; // Задает режим масштабирования фигуры
        }

        private void TMO_Click(object sender, EventArgs e) {
            var tmo = TmoSelector.SelectedIndex + 1;

            if (_primaryPgn == null || _secondaryPgn == null) {
                UiUtils.ShowInfo("Выберите две фигуры (ЛКМ и ПКМ)");
                return;
            }

            _figureList.Remove(_primaryPgn as Drawable);
            _figureList.Remove(_secondaryPgn as Drawable);
            _figureList.Add(new TmoObject(Color.Red, tmo, _primaryPgn, _secondaryPgn));
            _primaryPgn = null;
            _secondaryPgn = null;

            Redraw();
        }

        private void FlipVertically_Click(object sender, EventArgs e) {
        }

        private void FlipHorizontally_Click(object sender, EventArgs e) {
        }

        private void ColorSelector_SelectedIndexChanged(object sender, EventArgs e) {
            switch (ColorSelector.SelectedIndex) // выбор цвета
            {
                case 0:
                    _drawPen.Color = Color.Black;
                    break;
                case 1:
                    _drawPen.Color = Color.Red;
                    break;
                case 2:
                    _drawPen.Color = Color.Green;
                    break;
                case 3:
                    _drawPen.Color = Color.Blue;
                    break;
                case 4:
                    _drawPen.Color = Color.White;
                    break;
            }
        }

        private void PictureBox_MouseDown(object sender, MouseEventArgs e) {
            var selectedPgn = _figureList.OfType<ITmoOperand>().FirstOrDefault(p => p.ContainsPoint(e.X, e.Y));
            if (selectedPgn != null) {
                switch (e.Button) {
                    case MouseButtons.Left:
                        if (_primaryPgn != null) {
                            _primaryPgn.FillColor = Color.Black;
                        }

                        if (selectedPgn == _secondaryPgn && _primaryPgn != null) {
                            _secondaryPgn = _primaryPgn;
                            _secondaryPgn.FillColor = SecondaryColor;
                        }

                        _primaryPgn = selectedPgn;
                        _primaryPgn.FillColor = PrimaryColor;
                        Redraw();
                        break;
                    case MouseButtons.Right:
                        if (_secondaryPgn != null) {
                            _secondaryPgn.FillColor = Color.Black;
                        }

                        if (selectedPgn == _primaryPgn && _secondaryPgn != null) {
                            _primaryPgn = _secondaryPgn;
                            _primaryPgn.FillColor = PrimaryColor;
                        }

                        _secondaryPgn = selectedPgn;
                        _secondaryPgn.FillColor = SecondaryColor;
                        Redraw();
                        break;
                }
            }

            _mouseStartPosition = e.Location;
            switch (_operation) {
                case 1: // ввод вершин и рисование
                    InputPgn(e);
                    if (e.Button == MouseButtons.Right) _operation = 0;

                    break;
                case 2: // выделение многоугольника
                case 3: // вращение
                case 4: // масштабирование
                    if (_primaryPgn != null) {
                        _g.DrawEllipse(new Pen(Color.Blue), e.X - 2, e.Y - 2, 5, 5);
                    }

                    break;
            }

            PictureBox.Image = _image;
        }

        private void PictureBox_MouseMove(object sender, MouseEventArgs e) {
            if (_primaryPgn != null && e.Button == MouseButtons.Left) {
                switch (_operation) {
                    case 2:
                        _primaryPgn.Move(e.X - _mouseStartPosition.X, e.Y - _mouseStartPosition.Y);
                        Redraw();
                        _mouseStartPosition = e.Location;
                        break;
                    case 3:
                        _primaryPgn.Rotate(_mouseStartPosition, (e.X > _mouseStartPosition.X ? 1 : -1) * (Math.PI / 180.0));
                        Redraw();
                        break;
                    case 4:
                        _primaryPgn.Scale(_mouseStartPosition, e.X > _mouseStartPosition.X ? 1.01 : 0.99);
                        Redraw();
                        break;
                }
            }
        }

    }

}