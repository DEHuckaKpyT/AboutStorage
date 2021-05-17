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
        public List<Process> ExecutedProcesses;
        public int CountFreeSegments;

        public int CountSegmentsOnSidePictureBox;
        public int LengthSegmentOnPictureBox;
        public PictureBox PictureBox;
        public Label Label;
        ListBox ListBoxWaitingProcesses;
        ListBox ListBoxProcessingProcesses;
        ListBox ListBoxExecutedProcesses;

        public AddresSpace(int timeExecuting, int countOfSegments, List<Process> processes, PictureBox pictureBox, Label label,
            ListBox listBoxWaitingProcesses, ListBox listBoxProcessingProcesses, ListBox listBoxExecutedProcesses)
        {
            TimeExecuting = timeExecuting;
            Segments = new List<Segment>();
            ProcessingProcesses = new List<Process>();
            ExecutedProcesses = new List<Process>();
            CountSegmentsOnSidePictureBox = (int)Math.Ceiling(Math.Sqrt(countOfSegments));
            LengthSegmentOnPictureBox = pictureBox.Height / CountSegmentsOnSidePictureBox;
            CountFreeSegments = countOfSegments;
            PictureBox = pictureBox;
            Processes = processes;
            Label = label;
            ListBoxWaitingProcesses = listBoxWaitingProcesses;
            ListBoxProcessingProcesses = listBoxProcessingProcesses;
            ListBoxExecutedProcesses = listBoxExecutedProcesses;


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
        public void StartProcessing()
        {
            int timeLeft = 0;
            while(Processes.Count > 0 || ProcessingProcesses.Count > 0)
            {
                int countSegmentsNeed = -1;
                for (int i = 0; i < Processes.Count; i++)
                {
                    if (Processes[i].CountSegmentsNeed <= CountFreeSegments)
                    {
                        SetProcess(Processes[i]);
                        ProcessingProcesses.Add(Processes[i]);
                        Processes.Remove(Processes[i]);
                        i--;
                    }
                    else
                    {
                        countSegmentsNeed = Processes[i].CountSegmentsNeed;
                        break;
                    }
                }
                Thread.Sleep(1000);
                timeLeft += 1;
                foreach (Process prProcess in ProcessingProcesses)
                {
                    prProcess.TimeTotalExecuting -= 1;
                    if (prProcess.TimeTotalExecuting == 0)
                    {
                        ExecutedProcesses.Add(prProcess);
                        RemoveProcess(prProcess);
                        CountFreeSegments += prProcess.CountSegmentsNeed;
                    }
                }
                
                if (timeLeft % TimeExecuting == 0)
                {
                    while (countSegmentsNeed > CountFreeSegments && ProcessingProcesses.Count > 0)
                    {
                        Process process = ProcessingProcesses.OrderBy(x => x.TimeTotalExecuting).ToArray()[0];
                        ProcessingProcesses.Remove(process);
                        RemoveProcess(process);
                        CountFreeSegments += process.CountSegmentsNeed;
                        Processes.Add(process);
                    }

                }
                foreach (var remProc in ExecutedProcesses)
                {
                    if (ProcessingProcesses.Contains(remProc))
                    {
                        ProcessingProcesses.Remove(remProc);
                        RemoveProcess(remProc);
                    }
                }
                while (countSegmentsNeed <= CountFreeSegments && Processes.Count > 0)
                {
                    Process process = Processes[0];
                    Processes.Remove(process);
                    ProcessingProcesses.Add(process);
                    SetProcess(process);
                    if (Processes.Count > 0)
                        countSegmentsNeed = Processes[0].CountSegmentsNeed;
                }
                Label.Invoke(new Action(() => Label.Text = CountFreeSegments.ToString() + " - количество свободных сегментов"));
                foreach (Segment segment in Segments)
                    segment.FillSegment(PictureBox.CreateGraphics());
                UpdateListBoxes();
            }
        }

        void UpdateListBoxes()
        {
            ListBoxWaitingProcesses.Invoke(new Action(() => ListBoxWaitingProcesses.Items.Clear()));
            foreach (Process process in Processes)
                ListBoxWaitingProcesses.Invoke(new Action(() => ListBoxWaitingProcesses.Items
                .Add($"{process.Color} {process.CountSegmentsNeed} {process.TimeTotalExecuting}")));
            ListBoxProcessingProcesses.Invoke(new Action(() => ListBoxProcessingProcesses.Items.Clear()));
            foreach (Process process in ProcessingProcesses)
                ListBoxProcessingProcesses.Invoke(new Action(() => ListBoxProcessingProcesses.Items
                .Add($"{process.Color} {process.CountSegmentsNeed} {process.TimeTotalExecuting}")));
            ListBoxExecutedProcesses.Invoke(new Action(() => ListBoxExecutedProcesses.Items.Clear()));
            foreach (Process process in ExecutedProcesses)
                ListBoxExecutedProcesses.Invoke(new Action(() => ListBoxExecutedProcesses.Items
                .Add($"{process.Color} {process.CountSegmentsNeed} {process.TimeTotalExecuting}")));

        }
    }
}
