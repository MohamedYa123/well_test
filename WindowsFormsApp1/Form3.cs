using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        double mm = 0.0;
        public double m
        {
            get
            {
                return mm;
            }
            set
            {
                mm = value;
            }
        }
        double bb = 0.0;
        public double b
        {
            get
            {
                return bb;
            }
            set
            {
                bb = value;
            }
        }
        List<double> sdata = new List<double>();
        public List<double> data
        {
            get
            {
                return sdata;
            }
            set
            {
                sdata = value;
            }
        }
        double q = 0;
        double fay = 0;
        double h = 0;
        double rw = 0;
        double pi = 0;
        double mu = 0;
        double ct = 0;
        double beta = 0;
        double t = 0;
        private void Form3_Load(object sender, EventArgs e)
        {
            q = sdata[0];
            fay = sdata[1]/100;
            h = sdata[2];
            rw = sdata[3];
            pi = sdata[4];
            mu = sdata[5];
            ct = sdata[6];
            beta = sdata[7];
            t = sdata[8];
            textBox1.Text += "\r\n";
            textBox1.Text += "Slope (|m|)="+Math.Abs(m);
            textBox1.Text += "\r\n";
            textBox1.Text += "\r\nEquation: Y="+m+"X+"+b;
            textBox1.Text += "\r\n";
            double k = 0;
            k = 162.6 * q * beta * mu / (Math.Abs(m) * h);
            double s = 0;
            double phr1 = 0;
            double ri = 0;
            phr1 = m * 1 + b;
            double j = Math.Log(k / (fay * mu * ct * rw * rw));
            s = 1.151 * ((pi - phr1) / Math.Abs(m) - Math.Log(k / (fay * mu * ct * rw * rw)) + 3.23);
            ri = Math.Sqrt(k*t/(948*fay*mu*ct) );

            textBox1.Text += "\r\n k=" + k+" md";
            textBox1.Text += "\r\n";
            textBox1.Text += "\r\n s=" + s;
            textBox1.Text += "\r\n";
            textBox1.Text += "\r\n P1hr=" + phr1 +" psia";
            textBox1.Text += "\r\n";
            textBox1.Text += "\r\n ri=" + ri+" ft";


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
