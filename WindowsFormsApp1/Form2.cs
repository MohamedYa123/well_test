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
        private void setline(double forgivenes,double start,double end)
        {
            pwf1.Clear();
            ts1.Clear();
            double slope = 0.0;
            double err = 0.0;
            forgivenes = double.MaxValue;
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
                if (slope == 0)
                {

                }
                {
                    err =Math.Abs((slope - tslope) / slope);
                    var d = tsm[i];
                    var pws = pwf[i];
                    if (((Convert.ToDouble(tsm[i])>= start)|| Convert.ToDouble(tsm[i]) == start)&& tsm[i]<=end)
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
                    else
                    {

                    }
                    if (tslope != 0)
                    {
                        slope = tslope;
                    }

                }
            }
        }
        private double calcerr(double b,double m )
        {
            double err = 0.0;
            double y = 0.0;
            double x = 0.0;
            double real = 0.0;
            var gg = pwf1[pwf1.Count - 1];
            for(int i = 0; i < pwf1.Count; i++)
            {
                x = Convert.ToDouble(ts1[i]);
                real = Convert.ToDouble(pwf1[i]);
                y = m * x + b;
                err += Math.Pow((real - y),2 );// real
            }
            return err/pwf1.Count;
        }
        double bb = 0;
        double getb(double m)
        {
            double b = 0.0;
            for (int i = 0; i < pwf1.Count; i++)
            {
                b += pwf1[i] - m * ts1[i];
                //b += (pwf1[i] - pwf1[i + 1]) / (ts1[i] - ts1[i + 1]);

            }
            b /= pwf1.Count;
            bb = b;
            return bb;
        }
        double getslope()
        {
            double m= 0.0;
            double xavg = 0;
            for(int i = 0; i < ts1.Count; i++)
            {
                xavg += ts1[i];
            }
            xavg /= ts1.Count;
            double yavg = 0;
            for(int i = 0; i < pwf1.Count; i++)
            {
                yavg += pwf1[i];
            }
            yavg /= pwf1.Count;
            double topm = 0;
            double downm = 0;
            for(int i=0;i< pwf1.Count; i++)
            {
                double xi = ts1[i];
                double yi = pwf1[i];
                topm += (xi - xavg)*(yi - yavg);
                downm += Math.Pow(xi - xavg, 2);
            }
            m= topm/downm;
            bb = yavg - m * xavg;
            return m;
        }
        private double linegenerator(double maximumsteps)
        {
            int step = 0;
            
            double ferr = 0.0;
            double lerr = 0.0;
            double avgerr = 0.0;
            double b = 0.0;
            var mm=getslope();
            
            ferr= calcerr(bb, mm);
            return mm;
            double minslope = -50000;
            double maxslope = 50000;
            double first = maxslope;
            double second = minslope;
            //return mm;
            while (step < maximumsteps)
            {
                double m = first;
                double y = Convert.ToDouble(pwf1[0]);
                double x = Convert.ToDouble(ts1[0]);
                b = y - m * x;
                b=getb(m);
                ferr = calcerr(b, m);
                //
                m = second;
                y = Convert.ToDouble(pwf1[0]);
                x = Convert.ToDouble(ts1[0]);
                b = y - m * x;
                b = getb(m);
                lerr = calcerr(b, m);
                //
                m = (first+second)/2;
                y = Convert.ToDouble(pwf1[0]);
                x = Convert.ToDouble(ts1[0]);
                b = y - m * x;
                b = getb(m);
                avgerr = calcerr(b, m);
                if (ferr > lerr)
                {
                    if (m <= maxslope && m >= minslope)
                    {
                         first = m;
                    }
                }
                else
                {
                    if (m <= maxslope && m >= minslope)
                    { 
                      second = m;
                    }
                }
                var dr = Math.Max(ferr, lerr);
                var drm=Math.Max(dr, avgerr);
                if (ferr == drm)
                {
                //    first = first;
                    if (lerr < avgerr)
                    {
                        
                    }
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
            var dx = pwf.Count / 1000;
            dx = Math.Max(dx, 1);
            for (int i = 0; i < pwf.Count; i++)
            {
                double vv = 0.0;
                vv = Convert.ToDouble(tss[i]);
                if (semilog)
                {
                    vv = Math.Log10(vv);
                }
                vv = Math.Round(vv, 4);
                if (i % dx == 0)
                {
                    chart1.Series["Series1"].Points.AddXY(vv, pwf[i]);
                }
            }
        }
        double sb = 0.0;
        double sm = 0.0;
        double tp = 0;
        List<double> tsm = new List<double>();
        double t;
        private void methodcollector()
        {
            if (method == 1)
            {
                tp = sdata[9];
                t = sdata[8];
                for(int i = 0; i < ts.Count; i++)
                {
                    tsm.Add(Convert.ToDouble(ts[i]));
                    if (ts[i] > (decimal)38.45)
                    {
                        var g = ts[i];
                    }
                    if (ts[i] > (decimal)t)
                    {
                        var x = (Convert.ToDecimal(tp) + ts[i] - (decimal)t) / (ts[i] - (decimal)t);
                        ts[i] =x;
                        
                        
                    }

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
                setline(Convert.ToDouble(numericUpDown1.Value), Convert.ToDouble(numericUpDown4.Value), Convert.ToDouble(numericUpDown5.Value));
                double m = linegenerator(3000);
                //m = 0.4;
                double b = Convert.ToDouble(pwf1[0]) - m * Convert.ToDouble(Convert.ToDouble(ts1[0]));
                sm = m;
                b=getb(m);
                sb = b;
                label4.Text = "Y = " + Convert.ToString(Math.Round(m, 4)) + " X + " + Convert.ToString(Math.Round(b, 4));
                label5.Text = "\nnumber of points satisfied\nthe conditions : " + pwf1.Count+"\nerror:"+ Math.Round(calcerr(b, m)*100,3)+"%" ;
                //
                chart1.Series["Trend line"].Points.Clear();
                var dx = pwf.Count / 1000;
                dx = Math.Max(dx, 1);
                for (int i = 0; i < pwf.Count; i++)
                {
                    if (i % dx == 0)
                    {
                        double x = Convert.ToDouble(Convert.ToDouble(tss[i]));
                        x = Convert.ToDouble(tss[i]);
                        if (semilog)
                        {
                            x = Math.Log10(x);
                        }

                        double p = m * x + b;
                        x = Math.Round(x, 4);

                        chart1.Series["Trend line"].Points.AddXY(x, p);
                    }
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
