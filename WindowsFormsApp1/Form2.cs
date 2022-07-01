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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        List<decimal> tss = new List<decimal>();
        List<decimal> pwf = new List<decimal>();
        bool semi = true;
        public bool semilog {
            get { return semi; } set {

                semi = value;
            } }
        public List<decimal> ts
        {
            get
            {
                return tss;
            }
            set
            {
                tss = value;
            }
        }
        public List<decimal> pwfs
        {
            get
            {
                return pwf;
            }
            set
            {
                pwf = value;
            }
        }
        List<double> pwf1 = new List<double>();
        List<double> ts1 = new List<double>();
        private void setline(double forgivenes,double start)
        {
            pwf1.Clear();
            ts1.Clear();
            double slope = 0.0;
            double err = 0.0;
            for (int i = 0; i < pwf.Count-1; i++)
            {
                double r = Convert.ToDouble(tss[i]);
                double r2 = Convert.ToDouble(tss[i + 1]);
                if (semilog)
                {
                    r = Math.Log10(r);
                    r2 = Math.Log10(r2);
                }
                double tslope =Convert.ToDouble(Convert.ToDouble(pwf[i]-pwf[i+1])/ (r - r2) );
                if (i == 40)
                {

                }
                if (i == 0)
                {
                    slope = tslope;

                }

                {
                    err =Math.Abs((slope - tslope) / slope);
                    if ((err<= forgivenes && Convert.ToDouble(tsm[i])>= start)|| Convert.ToDouble(tsm[i]) == start)
                    {

                        pwf1.Add(Convert.ToDouble(pwf[i]));
                        ts1.Add(r);
                        if (i == pwf.Count - 2) {
                            pwf1.Add(Convert.ToDouble(pwf[i+1]));
                            ts1.Add(r2);
                        }
                    }
                    else if(Convert.ToDouble(tsm[i]) > start){
                        break;
                    }
                    slope = tslope;

                }
            }
        }
        private double calcerr(double b,double m )
        {
            double err = 0.0;
            double y = 0.0;
            double x = 0.0;
            double real = 0.0;
            for(int i = 0; i < pwf1.Count; i++)
            {
                x = Convert.ToDouble(ts1[i]);
                real = Convert.ToDouble(pwf1[i]);
                y = m * x + b;
                err += Math.Abs((real - y) / real);
            }
            return err/pwf1.Count;
        }
        private double linegenerator(double maximumsteps)
        {
            int step = 0;
            double first = -10000;
            double second = 10000;
            double ferr = 0.0;
            double lerr = 0.0;
            double avgerr = 0.0;
            double b = 0.0;
            while (step < maximumsteps)
            {
                double m = first;
                double y = Convert.ToDouble(pwf1[0]);
                double x = Convert.ToDouble(ts1[0]);
                b = y - m * x;
                ferr = calcerr(b, m);
                //
                m = second;
                y = Convert.ToDouble(pwf1[0]);
                x = Convert.ToDouble(ts1[0]);
                b = y - m * x;
                lerr = calcerr(b, m);
                //
                m = (first+second)/2;
                y = Convert.ToDouble(pwf1[0]);
                x = Convert.ToDouble(ts1[0]);
                b = y - m * x;
                avgerr = calcerr(b, m);
                if (ferr > lerr)
                {
                    first = m;
                }
                else
                {
                    second = m;
                }
                step++;
            }
            return (first + second) / 2;
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
        int methodd = 0;
        public int method { get { return methodd; }
            set
            {
                methodd = value;
            }
        }
        private void Form2_Load(object sender, EventArgs e)
        {

            methodcollector();
            chart1.Series.Add("Trend line");
            chart1.Series["Trend line"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart1.Series["Series1"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            if (method == 1)
            {
                chart1.ChartAreas["ChartArea1"].AxisX.Title = "HTR(hr)";
            }
            else if (method == 2)
            {
                chart1.ChartAreas["ChartArea1"].AxisX.Title = "∆te(hr)";
            }
            else if (method == 3)
            {
                chart1.ChartAreas["ChartArea1"].AxisX.Title = "∆t(hr)";
            }
            else if (method == 0)
            {
                chart1.ChartAreas["ChartArea1"].AxisX.Title = "Time(hr)";
            }
            chart1.ChartAreas["ChartArea1"].AxisY.Title = "Pwf(psia)";
            chart1.ChartAreas["ChartArea1"].AxisY.TitleFont = new Font("Arial", 12, FontStyle.Bold);
            chart1.ChartAreas["ChartArea1"].AxisX.TitleFont = new Font("Arial", 12, FontStyle.Bold);
            for (int i = 0; i < pwf.Count; i++)
            {
                double vv = 0.0;
                vv = Convert.ToDouble(tss[i]);
                if (semilog)
                {
                    vv = Math.Log10(vv);
                }
                vv = Math.Round(vv, 4);
                chart1.Series["Series1"].Points.AddXY(vv, pwf[i]);
            }
        }
        double sb = 0.0;
        double sm = 0.0;
        double tp = 0;
        List<double> tsm = new List<double>();
        private void methodcollector()
        {
            if (method == 1)
            {
                tp = sdata[9];
                for(int i = 0; i < ts.Count; i++)
                {
                    tsm.Add(Convert.ToDouble(ts[i]));
                    ts[i] = (Convert.ToDecimal(tp) + ts[i]) / ts[i];

                }
            }
            else if (method == 2)
            {
                tp = sdata[9];
                for(int i = 0; i < ts.Count; i++)
                {
                    tsm.Add(Convert.ToDouble(ts[i]));
                    ts[i] = (Convert.ToDecimal(tp) * ts[i]) / (Convert.ToDecimal(tp) + ts[i]);
                }
            }
            else if (method == 3)
            {
                tp = sdata[9];
                for(int i = 0; i < ts.Count; i++)
                {
                    tsm.Add(Convert.ToDouble(ts[i]));
                    //     ts[i] = (Convert.ToDecimal(tp) * ts[i]) / (Convert.ToDecimal(tp) + ts[i]);
                }
            }
            else if (method == 0)
            {
                tp = sdata[9];
                for (int i = 0; i < ts.Count; i++)
                {
                    tsm.Add(Convert.ToDouble(ts[i]));
                    //     ts[i] = (Convert.ToDecimal(tp) * ts[i]) / (Convert.ToDecimal(tp) + ts[i]);
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                setline(Convert.ToDouble(numericUpDown1.Value), Convert.ToDouble(numericUpDown4.Value));
                double m = linegenerator(1000);
                double b = Convert.ToDouble(pwf1[0]) - m * Convert.ToDouble(Convert.ToDouble(ts1[0]));
                sm = m;
                sb = b;
                label4.Text = "Y = " + Convert.ToString(Math.Round(m, 4)) + " X + " + Convert.ToString(Math.Round(b, 4));
                label5.Text = "\nnumber of points satisfied\nthe conditions : " + pwf1.Count+"\nerror:"+ Math.Round(calcerr(b, m)*100,3)+"%" ;
                //
                chart1.Series["Trend line"].Points.Clear();
                for (int i = 0; i < pwf.Count; i++)
                {
                    double x = Convert.ToDouble(Convert.ToDouble( tss[i]));
                    x = Convert.ToDouble(tss[i]);
                    if (semilog)
                    {
                        x = Math.Log10(x);
                    }
                    double p = m * x + b;
                    x = Math.Round(x, 4);
                    chart1.Series["Trend line"].Points.AddXY(x, p);
                }
                label6.Show();
            }
            catch
            {
                MessageBox.Show("Unexpected error");
            }

        }

        private void label6_MouseEnter(object sender, EventArgs e)
        {
            label6.ForeColor = Color.Yellow;
        }

        private void label6_DragLeave(object sender, EventArgs e)
        {
            label6.ForeColor = Color.Blue;
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            chart1.ChartAreas["ChartArea1"].AxisY.Minimum = Convert.ToDouble( numericUpDown2.Value);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {
            Form3 f = new Form3();
            f.data = this.data;
            f.m = sm;
            f.b = sb;
            f.ShowDialog();
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            chart1.ChartAreas["ChartArea1"].AxisX.Minimum = Convert.ToDouble(numericUpDown3.Value);
        }
    }
}
