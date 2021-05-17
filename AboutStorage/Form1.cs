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
            Processes.Add(new Process(30, 75, Color.Black));
            Processes.Add(new Process(30, 19, Color.SandyBrown));
            Processes.Add(new Process(5, 1, Color.Red));
            Processes.Add(new Process(30, 49, Color.Blue));
            Processes.Add(new Process(30, 17, Color.Yellow));
            Processes.Add(new Process(30, 27, Color.Green));
            Processes.Add(new Process(30, 35, Color.Black));
            Processes.Add(new Process(30, 19, Color.SandyBrown));



        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddresSpace = new AddresSpace(8, int.Parse(textBox1.Text), Processes, pictureBox1, label2, label3,
                listBoxWaitingProcesses, listBoxProcessingProcesses, listBoxExecutedProcesses);
            AddresSpace.PaintSegments();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //new Thread(new ThreadStart(AddresSpace.StartProcesses)).Start();
            //new Thread(new ThreadStart(AddresSpace.DoProcessingProcesses)).Start();
            new Thread(new ThreadStart(AddresSpace.StartProcessing)).Start();
        }


    }
}
