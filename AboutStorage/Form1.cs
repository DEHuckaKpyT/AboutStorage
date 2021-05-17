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
            Processes.Add(new Process(30, 4, Color.Red));
            Processes.Add(new Process(30, 3, Color.Blue));
            Processes.Add(new Process(30, 5, Color.Yellow));



        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddresSpace = new AddresSpace(8, int.Parse(textBox1.Text), Processes, pictureBox1);
            AddresSpace.PaintSegments();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new Thread(new ThreadStart(AddresSpace.StartProcesses)).Start();
            new Thread(new ThreadStart(AddresSpace.DoProcessingProcesses)).Start();
        }


    }
}
