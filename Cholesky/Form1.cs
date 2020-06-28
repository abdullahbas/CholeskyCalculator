using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Akifff
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {





        }

        public int denksay;
        public float[,] matrix = new float[100, 100];
        public float[] maty = new float[100];

        private void calis_Click(object sender, EventArgs e)
        {
            // Split on one or more non-digit characters.
            listBox1.Items.Clear();
            richTextBox1.Text = richTextBox1.Text.Replace(".", ",");
            denksay = richTextBox1.Lines.Count();

            for (int kk = 0; kk <= richTextBox1.Lines.Count() - 1; kk++)
            {
                int i;
                string[] input = richTextBox1.Lines[kk].ToString().Split('=');

                string degis = textBox1.Text;
                string[] parca = input[0].Split(degis[0]);


                string buff;
                int y;


                for (i = 0; i < parca.Length; i++)
                {


                    //MessageBox.Show( kk.ToString() + " denklemin" + i.ToString() + ".parçası="+parca[i].ToString());



                    if (i == 0)
                    { if (parca[0] == "" || parca[0] == "-")
                        {
                            parca[0] = parca[0] + "1";
                        }
                        buff = parca[1];
                        matrix[kk, int.Parse(buff[0].ToString()) - 1] = float.Parse(parca[0].ToString());

                    }

                    else
                    {
                        if (parca[i].Length < 3)
                        {
                            parca[i] = parca[i] + "1";
                        }
                        buff = parca[i].ToString();

                        try
                        {
                            string buff2 = parca[i + 1].ToString();
                            matrix[kk, int.Parse(buff2[0].ToString()) - 1] = float.Parse(buff.Substring(1, buff.Length - 1).ToString());

                        }
                        catch
                        {
                            //MessageBox.Show("girdi");
                        }



                    }


                }
                string s = "";
                for (int j = 0; j < richTextBox1.Lines.Count(); j++)
                {
                    if (matrix[kk, j] >= 0)
                    {
                        s = s + "    " + matrix[kk, j].ToString();
                    }
                    else

                    {
                        s = s + "  " + matrix[kk, j].ToString();
                    }


                }

                s = "|" + s + "   |     " + input[1];
                maty[kk] = float.Parse(input[1]);
                listBox1.Items.Add(s);


            }
            listBox1.Items.Add("GİRİLEN DENKLEMLER.");
            for (int ii = 0; ii <= richTextBox1.Lines.Count() - 1; ii++)
            {
                listBox1.Items.Add(richTextBox1.Lines[ii].ToString());
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            richTextBox1.ForeColor = Color.Gray;
        }

        private void richTextBox1_Click(object sender, EventArgs e)
        {
            if (richTextBox1.ForeColor == Color.Gray)
            {
                richTextBox1.ReadOnly = false;
                richTextBox1.Text = "";
                richTextBox1.BackColor = Color.White;
                richTextBox1.ForeColor = Color.Black;
            }
        }
        public float[,] u = new float[100, 100];
        public float[,] L = new float[100, 100];
        public float[] Y = new float [100];
        public float[] x = new float[100];

        private void sonuc_Click(object sender, EventArgs e)
        {
            /*  string[] parca = textBox1.Text.Split(',');
              richTextBox1.AppendText("\r\n" + matrix[int.Parse(parca[0]),int.Parse(parca[1])]);
              richTextBox1.ScrollToCaret();*/
            richTextBox1.Text = "";
       
            for (int j=0;j<denksay;j++)
            {

                if (j==0)
                {
                    Y[j] = maty[j];  
                }
                else
                {
                    float toplam = 0;
                    for (int i=0; i<=j-1;i++)
                    {
                        toplam = toplam + (float)(Y[i] * L[j, i]);
                    }
                    Y[j] = maty[j] - toplam;
                }

                for (int k=0;k<denksay ;k++)
                {
                    if (k==0)
                    {
                        u[k, j] = matrix[k, j];
                    }

                    if (j>=k)
                    {
                        if (k==j)
                        {
                            L[k, k] = 1;
                        }
                        float toplam=0;
                        for (int i=0;i<=k-1;i++)
                        {
                            toplam = toplam +(float)(L[k, i] * u[i, j]);
                        }
                        u[k, j] = matrix[k, j] - toplam;


                    }

                    else
                    {
                        float toplam = 0;
                        for (int i = 0; i <= j - 1; i++)
                        {
                            toplam = toplam + (float)L[k, i] * u[i, j];
                        }
                        L[k, j] = (matrix[k, j]-toplam)/u[j,j];

                    }

                }

            }
            for (int i = denksay-1 ; i >= 0; i--)
            { 
                float toplam = 0;
                for (int j=i+1;j<=denksay;j++)
                {
                    toplam = toplam +(float) (u[i, j] * x[j]);

                }
                x[i] = (Y[i] - toplam) / u[i, i];
               // MessageBox.Show( Y[i].ToString()+"  "+x[i].ToString()+"  "+u[i,i].ToString());
            }

            richTextBox1.AppendText(" U=  ");

            for (int kk=0; kk<denksay; kk++)
            {
                richTextBox1.AppendText("\r");

                for (int kk2=0;kk2<denksay;kk2++)
                {
                    richTextBox1.AppendText("   "+ u[kk,kk2].ToString());

                }
                

            }

            richTextBox1.AppendText("\r\r L=  ");
            for (int kk = 0; kk < denksay; kk++)
            {
                richTextBox1.AppendText("\r");

                for (int kk2 = 0; kk2 < denksay; kk2++)
                {
                    richTextBox1.AppendText("   " + L[kk, kk2].ToString());

                }
              

            }

            richTextBox1.AppendText("\r\r LY=b  \r\r Y=  \r ");

            for (int kk=0; kk<denksay; kk++)
            {
                richTextBox1.AppendText("   " + Y[kk].ToString());

            }
            richTextBox1.AppendText("\r\r U"+textBox1.Text+ "=Y \r\r X=  \r ");

            for (int kk = 0; kk < denksay; kk++)
            {
                richTextBox1.AppendText("   " + x[kk].ToString());

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*richTextBox1.AppendText("\r\n" + x[int.Parse(textBox1.Text)].ToString());
            richTextBox1.ScrollToCaret();*/
            richTextBox1.Text = "";
            listBox1.Items.Clear();
            Form ab = new Form1();
            Form1.ActiveForm.Hide();
            ab.Show();
            








        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
