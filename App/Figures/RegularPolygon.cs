using System;
using System.Drawing;

namespace App.Figures {

    public class RegularPolygon : Polygon {

        private float _radius;

        public float Radius {
            get => _radius;
            set {
                _radius = Math.Abs(value);
                RecalculateVertex();
            }
        }

        public RegularPolygon(Color fillColor, Color? borderColor = null, float radius = 1) : base(fillColor, borderColor) {
            _radius = radius;
        }

        public override void Add(Point newVertex) {
            base.Add(newVertex);
            RecalculateVertex();
        }

        private void RecalculateVertex() {
        }

    }

}