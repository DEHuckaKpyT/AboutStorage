using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AboutStorage
{
    class Process
    {
        public int TimeTotalExecuting;
        public int CountSegmentsNeed;

        public Color Color;

        public Process(int timeTotalExecuting, int countSegmentsNeed, Color color)
        {
            TimeTotalExecuting = timeTotalExecuting;
            CountSegmentsNeed = countSegmentsNeed;
            Color = color;
        }
    }
}