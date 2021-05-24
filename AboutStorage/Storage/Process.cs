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
        public int Bias;

        public Color Color;

        public Process(int timeTotalExecuting, int countSegmentsNeed, Color color)
        {
            TimeTotalExecuting = timeTotalExecuting;
            CountSegmentsNeed = countSegmentsNeed;
            Bias = -1;
            Color = color;
        }

        public override string ToString()
        {
            string adress = "0x00000000";
            if (Bias >= 0)
            {
                adress.Remove(adress.Length - (CountSegmentsNeed + Bias).ToString().Length - 1, (CountSegmentsNeed + Bias).ToString().Length);
                adress += (CountSegmentsNeed + Bias).ToString();
            }
            else
                adress = "-1";
            return $"{Color} {Bias} {CountSegmentsNeed} {adress}";
        }
    }
}