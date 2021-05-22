using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AboutStorage
{
    public partial class Form1 : Form
    {
        AddresSpace AddresSpace;
        List<Process> Processes;
        Thread thread;
        bool stopped = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Processes = new List<Process>();
            Processes.Add(new Process(5, 21, Color.Red));
            Processes.Add(new Process(30, 29, Color.Blue));
            Processes.Add(new Process(30, 27, Color.Yellow));
            Processes.Add(new Process(30, 27, Color.Green));
            Processes.Add(new Process(30, 75, Color.Cyan));
            Processes.Add(new Process(30, 19, Color.SandyBrown));
            Processes.Add(new Process(5, 1, Color.DarkGray));
            Processes.Add(new Process(30, 49, Color.GreenYellow));
            Processes.Add(new Process(30, 17, Color.Indigo));
            Processes.Add(new Process(30, 27, Color.Navy));
            Processes.Add(new Process(30, 35, Color.MediumOrchid));
            Processes.Add(new Process(30, 19, Color.Purple));



        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddresSpace = new AddresSpace(8, int.Parse(textBox1.Text), Processes, pictureBox1, label2, label3,
                listBoxWaitingProcesses, listBoxProcessingProcesses, listBoxExecutedProcesses);
            AddresSpace.PaintSegments();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            thread = new Thread(new ThreadStart(AddresSpace.StartProcessing));
            thread.Start();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (stopped)
                thread.Resume();
            else
                thread.Suspend();
            stopped = !stopped;
        }
    }
}
