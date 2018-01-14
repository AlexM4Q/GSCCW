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

        private ITmoOperand _primaryOperand;
        private ITmoOperand _secondaryOperand;

        private BezierCurve _bezierCurveCache;
        private RegularPolygon _regularPolygonCache;

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

        private void InputBeqierCurve(Point newPoint, bool isEnd) {
            if (_bezierCurveCache == null) {
                _bezierCurveCache = new BezierCurve(Color.Black);
                _figureList.Add(_bezierCurveCache);
            }

            if (isEnd) {
                _bezierCurveCache = null;
            } else {
                _bezierCurveCache.Add(newPoint);
            }
        }

        /// <summary>
        /// Ввод списка вершин и рисование
        /// </summary>
        /// <param name="newPoint"></param>
        /// <param name="isEnd"></param>
        private void InputPolypog(Point newPoint, bool isEnd) {
            _vertexList.Add(newPoint);

            var k = _vertexList.Count;
            if (k > 1) {
                _g.DrawLine(_drawPen, _vertexList[k - 2], _vertexList[k - 1]);
            } else {
                _g.DrawRectangle(_drawPen, newPoint.X, newPoint.Y, 1, 1);
            }

            // Конец ввода
            if (isEnd) {
                var pgn = new Polygon(_drawPen.Color);
                _g.DrawLine(new Pen(Color.Blue), _vertexList[k - 1], _vertexList[0]);
                _vertexList.ForEach(p => pgn.Add(p));
                _vertexList.Clear();
                _figureList.Add(pgn);
                pgn.Draw(_g);
            }
        }

        private void InputRegularPolypog(Point newPoint, bool isEnd) {
            if (_regularPolygonCache == null) {
                _regularPolygonCache = new RegularPolygon(Color.Black, newPoint);
                _figureList.Add(_regularPolygonCache);
            } else if (!isEnd) {
                _regularPolygonCache.Add();
            }

            if (isEnd) {
                _regularPolygonCache = null;
            }
        }

        private void Clear_Click(object sender, EventArgs e) {
            _g.Clear(PictureBox.BackColor);
            _figureList.ForEach(p => p.Clear());
            _figureList.Clear();
            _bezierCurveCache = null;
            _regularPolygonCache = null;
            _primaryOperand = null;
            _secondaryOperand = null;
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

            if (_primaryOperand == null || _secondaryOperand == null) {
                UiUtils.ShowInfo("Выберите две фигуры (ЛКМ и ПКМ)");
                return;
            }

            _figureList.Remove(_primaryOperand as Drawable);
            _figureList.Remove(_secondaryOperand as Drawable);
            _figureList.Add(new TmoObject(Color.Red, tmo, _primaryOperand, _secondaryOperand));
            _primaryOperand = null;
            _secondaryOperand = null;

            Redraw();
        }

        private void FlipVertically_Click(object sender, EventArgs e) {
            if (_primaryOperand == null) {
                UiUtils.ShowInfo("Выберите фигуру (ЛКМ)");
                return;
            }

            _primaryOperand.FlipVertically();
            Redraw();
        }

        private void FlipHorizontally_Click(object sender, EventArgs e) {
            if (_primaryOperand == null) {
                UiUtils.ShowInfo("Выберите фигуру (ЛКМ)");
                return;
            }

            _primaryOperand.FlipHorizontally();
            Redraw();
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
            _mouseStartPosition = e.Location;
            switch (_operation) {
                case 1: // ввод вершин и рисование
                    var isEnd = e.Button == MouseButtons.Right;
                    switch (FigureSelector.SelectedIndex) {
                        case 0:
                            InputBeqierCurve(e.Location, isEnd);
                            Redraw();
                            break;
                        case 1:
                            InputPolypog(e.Location, isEnd);
                            break;
                        case 2:
                            InputRegularPolypog(e.Location, isEnd);
                            Redraw();
                            break;
                    }

                    if (isEnd) _operation = 2;

                    break;
                case 2: // перемещение
                case 3: // вращение
                case 4: // масштабирование
                    RecalculateOperands(e);

                    if (_primaryOperand != null) {
                        _g.DrawEllipse(new Pen(Color.Blue), e.X - 2, e.Y - 2, 5, 5);
                    }

                    break;
            }

            PictureBox.Image = _image;
        }

        /// <summary>
        /// Определяет первичный и вторичный операнды
        /// </summary>
        /// <param name="e">Данные о событии мыши</param>
        private void RecalculateOperands(MouseEventArgs e) {
            var selectedPgn = _figureList.OfType<ITmoOperand>().FirstOrDefault(p => p.ContainsPoint(e.X, e.Y));
            if (selectedPgn == null) return;

            switch (e.Button) {
                case MouseButtons.Left:
                    if (_primaryOperand != null) {
                        _primaryOperand.FillColor = Color.Black;
                    }

                    if (selectedPgn == _secondaryOperand && _primaryOperand != null) {
                        _secondaryOperand = _primaryOperand;
                        _secondaryOperand.FillColor = SecondaryColor;
                    }

                    _primaryOperand = selectedPgn;
                    _primaryOperand.FillColor = PrimaryColor;
                    Redraw();
                    break;
                case MouseButtons.Right:
                    if (_secondaryOperand != null) {
                        _secondaryOperand.FillColor = Color.Black;
                    }

                    if (selectedPgn == _primaryOperand && _secondaryOperand != null) {
                        _primaryOperand = _secondaryOperand;
                        _primaryOperand.FillColor = PrimaryColor;
                    }

                    _secondaryOperand = selectedPgn;
                    _secondaryOperand.FillColor = SecondaryColor;
                    Redraw();
                    break;
            }

            if (_secondaryOperand != null) {
                _figureList.Remove(_secondaryOperand as Drawable);
                _figureList.Add(_secondaryOperand as Drawable);
                Redraw();
            }

            if (_primaryOperand != null) {
                _figureList.Remove(_primaryOperand as Drawable);
                _figureList.Add(_primaryOperand as Drawable);
                Redraw();
            }
        }

        private void PictureBox_MouseMove(object sender, MouseEventArgs e) {
            if (_operation == 1) {
                if (_regularPolygonCache != null) {
                    var center = _regularPolygonCache.Center;
                    var x = center.X - e.X;
                    var y = center.Y - e.Y;
                    _regularPolygonCache.Radius = (float) Math.Sqrt(x * x + y * y);
                    Redraw();
                }

                return;
            }

            if (_primaryOperand != null && e.Button == MouseButtons.Left) {
                switch (_operation) {
                    case 2:
                        _primaryOperand.Move(e.X - _mouseStartPosition.X, e.Y - _mouseStartPosition.Y);
                        Redraw();
                        _mouseStartPosition = e.Location;
                        break;
                    case 3:
                        _primaryOperand.Rotate(_mouseStartPosition, (e.X > _mouseStartPosition.X ? 1 : -1) * (Math.PI / 180.0));
                        Redraw();
                        break;
                    case 4:
                        _primaryOperand.Scale(_mouseStartPosition, e.X > _mouseStartPosition.X ? 1.01 : 0.99);
                        Redraw();
                        break;
                }
            }
        }

    }

}