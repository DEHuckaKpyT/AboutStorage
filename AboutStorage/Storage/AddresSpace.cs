using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AboutStorage
{
    class AddresSpace
    {
        public int TimeExecuting;
        public List<Segment> Segments;
        public List<Process> Processes;
        public List<Process> ProcessingProcesses;
        public int CountFreeSegments;

        public int CountSegmentsOnSidePictureBox;
        public int LengthSegmentOnPictureBox;
        public PictureBox PictureBox;

        public AddresSpace(int timeExecuting, int countOfSegments, List<Process> processes, PictureBox pictureBox)
        {
            TimeExecuting = timeExecuting;
            Segments = new List<Segment>();
            ProcessingProcesses = new List<Process>();
            CountSegmentsOnSidePictureBox = (int)Math.Ceiling(Math.Sqrt(countOfSegments));
            LengthSegmentOnPictureBox = pictureBox.Height / CountSegmentsOnSidePictureBox;
            CountFreeSegments = countOfSegments;
            PictureBox = pictureBox;
            Processes = processes;

            for (int i = 0; i < countOfSegments; i++)
                Segments.Add(new Segment(new Point[] {
                        new Point((i / CountSegmentsOnSidePictureBox) * LengthSegmentOnPictureBox, 
                        (i % CountSegmentsOnSidePictureBox) * LengthSegmentOnPictureBox),
                        new Point((i / CountSegmentsOnSidePictureBox) * LengthSegmentOnPictureBox + LengthSegmentOnPictureBox,
                        (i % CountSegmentsOnSidePictureBox) * LengthSegmentOnPictureBox),
                        new Point((i / CountSegmentsOnSidePictureBox) * LengthSegmentOnPictureBox + LengthSegmentOnPictureBox,
                        (i % CountSegmentsOnSidePictureBox) * LengthSegmentOnPictureBox + LengthSegmentOnPictureBox),
                        new Point((i / CountSegmentsOnSidePictureBox) * LengthSegmentOnPictureBox,
                        (i % CountSegmentsOnSidePictureBox) * LengthSegmentOnPictureBox + LengthSegmentOnPictureBox)}));
        }

        public void PaintSegments()
        {
            Graphics graphics = PictureBox.CreateGraphics();
            graphics.Clear(Color.White);
            foreach (Segment segment in Segments)
                segment.PaintSegment(graphics);
        }

        public void SetProcess(Process process)
        {
            int countSegmentsNeed = process.CountSegmentsNeed;
            CountFreeSegments -= countSegmentsNeed;
            foreach (Segment segment in Segments)
            {
                if (!segment.Using)
                {
                    segment.Using = true;
                    segment.CurrentProcess = process;
                    ProcessingProcesses.Add(process);
                    countSegmentsNeed--;
                }
                segment.FillSegment(PictureBox.CreateGraphics());
                if (countSegmentsNeed == 0) break;
            }
        }
        public void RemoveProcess(Process process)
        {
            foreach (Segment segment in Segments)
                if (segment.CurrentProcess == process)
                {
                    segment.CurrentProcess = null;
                    segment.Using = false;
                }
        }


        public void StartProcesses()
        {
            List<Process> processesForRemove = new List<Process>();
            while (Processes.Count > 0)
            {
                foreach (Process process in Processes)
                {
                    if (CountFreeSegments >= process.CountSegmentsNeed)
                    {
                        SetProcess(process);
                        processesForRemove.Add(process);
                    }
                    else
                        break;
                }
                foreach (Process process in processesForRemove)
                    Processes.Remove(process);
            }
        }

        public void DoProcessingProcesses()
        {
            Thread.Sleep(100 * TimeExecuting);
            foreach (Process process in ProcessingProcesses)
            {
                process.TimeTotalExecuting -= TimeExecuting;
            }

            Process processForMove = ProcessingProcesses.OrderBy(x => x.TimeTotalExecuting).ToArray()[0];
            ProcessingProcesses.Remove(processForMove);
            if (processForMove.TimeTotalExecuting > 0)
                Processes.Add(processForMove);
            else
                RemoveProcess(processForMove);
        }

    }
}
