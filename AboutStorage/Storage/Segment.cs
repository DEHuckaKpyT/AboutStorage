using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AboutStorage
{
    class Segment
    {
        string PhysAdress;
        public bool Using;
        public Process CurrentProcess;

        public Color BrushColor;
        public Point[] Points;

        public Segment(Point[] points)
        {
            Points = points;
            Using = false;
            CurrentProcess = null;
        }

        public void PaintSegment(Graphics g)
        {
            FillSegment(g);
            g.DrawPolygon(new Pen(Color.Black, 1), Points);
        }

        public void FillSegment(Graphics g)
        {
            if (CurrentProcess == null)
                g.FillPolygon(new SolidBrush(Color.White), Points);
            else
                g.FillPolygon(new SolidBrush(CurrentProcess.Color), Points);
        }
    }
}
