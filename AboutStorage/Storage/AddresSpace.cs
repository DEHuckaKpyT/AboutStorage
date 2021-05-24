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
        public List<Process> WaitingProcesses;
        public List<Process> ProcessingProcesses;
        public List<Process> ExecutedProcesses;
        public List<Process> AllProcesses;
        public int CountFreeSegments;

        public int CountSegmentsOnSidePictureBox;
        public int LengthSegmentOnPictureBox;
        public PictureBox PictureBox;
        public Label Label;
        public Label LabelTime;
        ListBox ListBoxWaitingProcesses;
        ListBox ListBoxProcessingProcesses;
        ListBox ListBoxExecutedProcesses;
        ListBox ListBoxAllProcesses;

        public AddresSpace(int timeExecuting, int countOfSegments, List<Process> processes, PictureBox pictureBox, Label label, Label labelTime,
            ListBox listBoxWaitingProcesses, ListBox listBoxProcessingProcesses, ListBox listBoxExecutedProcesses, ListBox listBoxAllProcesses)
        {
            TimeExecuting = timeExecuting;
            Segments = new List<Segment>();
            ProcessingProcesses = new List<Process>();
            ExecutedProcesses = new List<Process>();
            CountSegmentsOnSidePictureBox = (int)Math.Ceiling(Math.Sqrt(countOfSegments));
            LengthSegmentOnPictureBox = pictureBox.Height / CountSegmentsOnSidePictureBox;
            CountFreeSegments = countOfSegments;
            PictureBox = pictureBox;
            WaitingProcesses = processes;
            AllProcesses = new List<Process>(WaitingProcesses);
            Label = label;
            LabelTime = labelTime;
            ListBoxWaitingProcesses = listBoxWaitingProcesses;
            ListBoxProcessingProcesses = listBoxProcessingProcesses;
            ListBoxExecutedProcesses = listBoxExecutedProcesses;
            ListBoxAllProcesses = listBoxAllProcesses;

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
            process.Bias = -1;
            foreach (Segment segment in Segments)
                if (segment.CurrentProcess == process)
                {
                    segment.CurrentProcess = null;
                    segment.Using = false;
                }
        }
        public void SetProcess(Process process, int index)
        {
            process.Bias = index;
            for (int i = index; i < process.CountSegmentsNeed + index; i++)
            {
                if (!Segments[i].Using)
                {
                    Segments[i].Using = true;
                    Segments[i].CurrentProcess = process;
                }
                Segments[i].FillSegment(PictureBox.CreateGraphics());
            }
        }

        int MaxSize(ref int index)
        {
            int maxAnswer = 0;
            int tempMax = 0;
            int indexAnswer = -1;
            int indexTemp = -1;
            for (int i = 0; i < Segments.Count; i++)
            {
                if (!Segments[i].Using)
                {
                    tempMax++;
                    if (indexTemp == -1)
                        indexTemp = i;
                }
                else
                {
                    if (tempMax > maxAnswer)
                    {
                        maxAnswer = tempMax;
                        indexAnswer = indexTemp;
                    }
                    tempMax = 0;
                    indexTemp = -1;
                }
            }
            if (tempMax > maxAnswer)
            {
                maxAnswer = tempMax;
                indexAnswer = indexTemp;
            }
            index = indexAnswer;
            return maxAnswer;
        }
        
        public void StartProcessing()
        {
            int timeLeft = 0;
            while(WaitingProcesses.Count > 0 || ProcessingProcesses.Count > 0)
            {
                int indexStartSegments = 0;
                int countSegmentsNeed = -1;
                for (int i = 0; i < WaitingProcesses.Count; i++)
                {
                    CountFreeSegments = MaxSize(ref indexStartSegments);
                    if (WaitingProcesses[i].CountSegmentsNeed <= CountFreeSegments)
                    {
                        SetProcess(WaitingProcesses[i], indexStartSegments);
                        ProcessingProcesses.Add(WaitingProcesses[i]);
                        WaitingProcesses.Remove(WaitingProcesses[i]);
                        i--;
                    }
                    else
                    {
                        countSegmentsNeed = WaitingProcesses[i].CountSegmentsNeed;
                        break;
                    }
                    CountFreeSegments = MaxSize(ref indexStartSegments);
                }
                Thread.Sleep(100);
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

                indexStartSegments = 0;
                CountFreeSegments = MaxSize(ref indexStartSegments);
                if (timeLeft % TimeExecuting == 0)
                {
                    while (countSegmentsNeed > CountFreeSegments && ProcessingProcesses.Count > 0)
                    {
                        Process process = ProcessingProcesses.OrderBy(x => x.TimeTotalExecuting).ToArray()[0];
                        ProcessingProcesses.Remove(process);
                        RemoveProcess(process);
                        CountFreeSegments = MaxSize(ref indexStartSegments);
                        WaitingProcesses.Add(process);
                        if (countSegmentsNeed > CountFreeSegments)
                        {
                            foreach (Segment segment in Segments)
                                segment.FillSegment(PictureBox.CreateGraphics());
                            UpdateListBoxes();
                            Label.Invoke(new Action(() => Label.Text = CountFreeSegments.ToString() + " - количество свободных сегментов"));
                            MessageBox.Show($"Вытеснен процесс {process.Color}, {process.CountSegmentsNeed}\n" +
                                $"Процесс {WaitingProcesses[0].Color} требует {WaitingProcesses[0].CountSegmentsNeed}, свободных {CountFreeSegments }");
                        }
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

                CountFreeSegments = MaxSize(ref indexStartSegments);
                while (countSegmentsNeed <= CountFreeSegments && WaitingProcesses.Count > 0)
                {
                    Process process = WaitingProcesses[0];
                    WaitingProcesses.Remove(process);
                    ProcessingProcesses.Add(process);
                    CountFreeSegments = MaxSize(ref indexStartSegments);
                    SetProcess(process, indexStartSegments);
                    if (WaitingProcesses.Count > 0)
                        countSegmentsNeed = WaitingProcesses[0].CountSegmentsNeed;
                    CountFreeSegments = MaxSize(ref indexStartSegments);
                }
                Label.Invoke(new Action(() => Label.Text = CountFreeSegments.ToString() + " - количество свободных сегментов"));
                LabelTime.Invoke(new Action(() => LabelTime.Text = (timeLeft % TimeExecuting).ToString() + " - время выполнения"));
                foreach (Segment segment in Segments)
                    segment.FillSegment(PictureBox.CreateGraphics());
                UpdateListBoxes();
            }
        }

        void UpdateListBoxes()
        {
            ListBoxWaitingProcesses.Invoke(new Action(() => ListBoxWaitingProcesses.Items.Clear()));
            foreach (Process process in WaitingProcesses)
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

            ListBoxAllProcesses.Invoke(new Action(() => ListBoxAllProcesses.Items.Clear()));
            foreach (Process process in AllProcesses)
                ListBoxAllProcesses.Invoke(new Action(() => ListBoxAllProcesses.Items
                .Add(process)));

        }
    }
}
