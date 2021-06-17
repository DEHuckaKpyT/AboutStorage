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
        public bool Using;
        public Process CurrentProcess;
        public string PhysAdress;

        public Color BrushColor;
        public Point[] Points;

        public Segment(Point[] points, int num)
        {
            Points = points;
            string adress = "0x00000000";
            adress = adress.Remove(adress.Length - (num).ToString().Length - 1, (num).ToString().Length);
            adress += (num).ToString();
            PhysAdress = adress;
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
            g.DrawPolygon(new Pen(Color.Black, 1), Points);
            g.DrawString(PhysAdress, new Font("Times New Roman", 7), new SolidBrush(Color.Black), Points[0]);
        }
    }
}
