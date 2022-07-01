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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }
        Form1 ff = new Form1();
        List<double> sdata = new List<double>();
        public Form1 f {
            get {
                return ff;
            }
            set {
                ff = value;
            } 
        }
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
        private void Form4_Load(object sender, EventArgs e)
        {
            if (sdata.Count > 0)
            {
                q.Value =Convert.ToDecimal( sdata[0]);
                fay.Value =Convert.ToDecimal( sdata[1]);
                h.Value =Convert.ToDecimal( sdata[2]);
                rw.Value =Convert.ToDecimal( sdata[3]);
                pi.Value =Convert.ToDecimal( sdata[4]);
                mu.Value =Convert.ToDecimal( sdata[5]);
                ct.Value =Convert.ToDecimal( sdata[6]);
                beta.Value= Convert.ToDecimal(sdata[7]);
                t.Value= Convert.ToDecimal(sdata[8]);
                tp.Value= Convert.ToDecimal(sdata[9]);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sdata[0] = Convert.ToDouble(q.Value);
            sdata[1] = Convert.ToDouble(fay.Value);
            sdata[2] = Convert.ToDouble(h.Value);
            sdata[3] = Convert.ToDouble(rw.Value);
            sdata[4] = Convert.ToDouble(pi.Value);
            sdata[5] = Convert.ToDouble(mu.Value);
            sdata[6] = Convert.ToDouble(ct.Value);
            sdata[7] = Convert.ToDouble(beta.Value);
            sdata[8] = Convert.ToDouble(t.Value);
            sdata[9] = Convert.ToDouble(tp.Value);
            ff.data = sdata;
            this.Close();
        }
    }
}
