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
            Processes.Add(new Process(5, 11, Color.Red));
            Processes.Add(new Process(30, 9, Color.Blue));
            Processes.Add(new Process(30, 7, Color.Yellow));
            Processes.Add(new Process(30, 7, Color.Green));
            Processes.Add(new Process(30, 15, Color.Black));
            Processes.Add(new Process(30, 9, Color.SandyBrown));



        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddresSpace = new AddresSpace(8, int.Parse(textBox1.Text), Processes, pictureBox1, label2);
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
