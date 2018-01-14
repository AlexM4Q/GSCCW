using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace App.Utils {

    public static class MathUtils {

        /// <summary>
        /// Подсчет факториала
        /// </summary>
        /// <param name="value">Число</param>
        /// <returns>Факториал</returns>
        public static long Factorial(int value) {
            long result = 1;
            for (var i = value; i > 1; i--) {
                result *= i;
            }

            return result;
        }

        /// <summary>
        /// Определение абсциссы пересечения горизонтальной прямой Y
        /// и прямой образованной точками P1 и P2
        /// </summary>
        /// <param name="p1">Первая точка отрезка</param>
        /// <param name="p2">Вторая точка отрезка</param>
        /// <param name="y">Уровень горизонтали Y</param>
        /// <returns>абсцисса</returns>
        public static float Cross(PointF p1, PointF p2, float y) {
            if (p1.X == p2.X) {
                return p1.X;
            }

            if (p1.Y == y) {
                return p1.X;
            }

            if (p2.Y == y) {
                return p2.X;
            }

            var k = (p1.Y - p2.Y) / (p1.X - p2.X);
            var b = p1.Y - k * p1.X;

            return (y - b) / k;
        }

        /// <summary>
        /// Отражает наборы точек по общей вертикали
        /// </summary>
        /// <param name="pointsPool">Наборы отражаемых точек</param>
        /// <returns>Новые наборы отраженных точек</returns>
        public static List<PointF>[] FlipVertically(params List<PointF>[] pointsPool) {
            var maxX = float.MinValue;
            var minX = float.MaxValue;

            foreach (var points in pointsPool) {
                foreach (var point in points) {
                    if (point.X > maxX) {
                        maxX = point.X;
                    }

                    if (point.X < minX) {
                        minX = point.X;
                    }
                }
            }

            var middleX = (maxX + minX) / 2;

            var newVertex = new List<PointF>[pointsPool.Length];
            for (var i = 0; i < pointsPool.Length; i++) {
                newVertex[i] = new List<PointF>(pointsPool[i].Count);
                foreach (var point in pointsPool[i]) {
                    newVertex[i].Add(new PointF(2 * middleX - point.X, point.Y));
                }
            }

            return newVertex;
        }

        /// <summary>
        /// Отражает наборы точек по общей горизонтали
        /// </summary>
        /// <param name="pointsPool">Наборы отражаемых точек</param>
        /// <returns>Новые наборы отраженных точек</returns>
        public static List<PointF>[] FlipHorizontally(params List<PointF>[] pointsPool) {
            var maxY = float.MinValue;
            var minY = float.MaxValue;

            foreach (var points in pointsPool) {
                foreach (var point in points) {
                    if (point.Y > maxY) {
                        maxY = point.Y;
                    }

                    if (point.Y < minY) {
                        minY = point.Y;
                    }
                }
            }

            var middleY = (maxY + minY) / 2;

            var newVertex = new List<PointF>[pointsPool.Length];
            for (var i = 0; i < pointsPool.Length; i++) {
                newVertex[i] = new List<PointF>(pointsPool[i].Count);
                foreach (var point in pointsPool[i]) {
                    newVertex[i].Add(new PointF(point.X, 2 * middleY - point.Y));
                }
            }

            return newVertex;
        }

        public static List<Point> ToPoints(this List<PointF> pointFs) {
            return pointFs.Select(p => new Point((int) p.X, (int) p.Y)).ToList();
        }

        public static List<PointF> ToPointFs(this List<Point> points) {
            return points.Select(p => new PointF(p.X, p.Y)).ToList();
        }

    }

}