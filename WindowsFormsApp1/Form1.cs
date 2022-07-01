using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (testtype.SelectedIndex == 1)
            {
                comboBox1.Enabled = true;
            }
            else
            {
                comboBox1.Enabled = false;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
       
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            of.Filter = "(*.csv)|*.csv";
            of.ShowDialog();
            if (of.FileName != "")
            {
                filelocation.Text = of.FileName;

                string text = File.ReadAllText(filelocation.Text, Encoding.UTF8);
                readcsv(text);
            }
        }
        bool show = true;
        List<double> sdata = new List<double>();
        public List<double> data
        {
            get {
                return sdata;
            }
            set {
                sdata = value;
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (show)
            {
                panel7.Height += 20;
            }
            else
            {
                panel7.Height -= 20;
            }
            bool q = false;
            if (button2.Visible == false)
            { button2.Visible = true;q = true; }
            if (panel7.Height>=button2.Location.Y-15 || panel7.Height <= 0)
            {
                timer1.Stop();
                button4.Enabled = true;
                button2.Visible = q;
                
            }
            else
            {
                if (q){
                    button2.Visible = false;
                }
            }
            
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

            show = (panel7.Height <= 0);
            if (panel7.Height >= button2.Location.Y - 15)
            {
                panel7.Height = button2.Location.Y - 15;
            }
            else
            {
                panel7.Height = 0;
            }
            timer1.Enabled = true;
            if (timer1.Enabled == true)
            {
                button4.Enabled = false;
            }
        }
        bool first = true;
        private void Form1_Load(object sender, EventArgs e)
        {
            if (first)
            {
                this.WindowState = FormWindowState.Maximized;
            }
            if (panel7.Height > 0){
                panel7.Height = button2.Location.Y - 15; }
            if (first)
            {
                button4_Click(sender, e);
                first = false;
            }
            for (int i = 0; i <= 9; i++)
            {
                sdata.Add(0.0);
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            //Form1_Load(sender,  e);
        }
        List<decimal> ts = new List<decimal>();
        List<decimal> pwfs = new List<decimal>();

        private void readcsv(string data)
        {
            ts.Clear();
            pwfs.Clear();
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            var d = data.Split('\n');
            for (int i = 0; i < d.Length; i++)
            {
                d[i] = d[i].Replace(",", ";");
            }
            var columns = d[0].Split(';');

            for (int i = 0; i < columns.Length; i++) {
                DataGridViewColumn dg = new DataGridViewColumn();
                dg.HeaderText = columns[i];
                dataGridView1.Columns.Add(dg);
            }
            for (int i = 1; i < d.Length; i++)
            {
                DataGridViewRow dr = new DataGridViewRow();
                var cells = d[i].Split(';');
                if (cells[0] != "")
                {
                    for (int i2 = 0; i2 < cells.Length; i2++)
                {
                    DataGridViewCell dc = new DataGridViewTextBoxCell();
                    dc.Value = cells[i2];
                    dr.Cells.Add(dc);
                    if (i2 == 0)
                    {
                        ts.Add(Convert.ToDecimal(cells[i2]));
                    }
                    else if(i2==1)
                    {
                        pwfs.Add(Convert.ToDecimal(cells[i2]));
                    }
                }
                
                dataGridView1.Rows.Add(dr);
                   }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2();
            readcsv(File.ReadAllText(filelocation.Text, Encoding.UTF8));
            f.ts = ts;
            f.pwfs = pwfs;
            f.data = data;
            f.semilog = checkBox1.Checked;
            f.method = (comboBox1.SelectedIndex+1) * testtype.SelectedIndex;
            if (sdata[9] == 0 && (comboBox1.SelectedIndex + 1) * testtype.SelectedIndex > 0)
            {
                MessageBox.Show("You need to set tp first");
                Form4 f2 = new Form4();
                f2.data = sdata;
                f2.ShowDialog();
            }
            else
            {
                f.Show();
            }
            
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void label7_MouseEnter(object sender, EventArgs e)
        {
            label7.ForeColor = Color.Yellow;
        }

        private void label7_MouseLeave(object sender, EventArgs e)
        {
            label7.ForeColor = Color.Blue;
        }

        private void label7_Click(object sender, EventArgs e)
        {
            label7.ForeColor = Color.Blue;
            Form4 f = new Form4();
            f.f = this;
            f.data = sdata;
            f.ShowDialog();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form5 f = new Form5();
            f.ShowDialog();
        }
    }
}
