using System;
using System.DirectoryServices.ActiveDirectory;
using System.Threading;
using System.Windows.Forms;

namespace Zadanie_3
{
    public partial class Form1 : Form
    {
        private Thread calculationThread, bitMap, silnia;
        public bool stopCalculate = false;
        private bool stopbitMap = false;
        private bool stopSilnia = false;

        public static int interwal = 0;
        public Form1()
        {
            InitializeComponent();
        }
        private void CalculateThread()
        {
            stopCalculate = false;
            progressBar1.Value = 0;

            int x = int.Parse(textBox1.Text);
            int y = int.Parse(textBox2.Text);

            calculationThread = new Thread(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    if (stopCalculate)
                    {
                        break;
                    }
                    Thread.Sleep(500);
                    int result = x * y * i;

                    int progress = (i + 1) * 10;
                    progressBar1.Invoke(new Action(() =>
                    {
                        progressBar1.Value = progress;
                    }));
                }

                int finalResult = x * y * 10;

                Invoke((MethodInvoker)delegate { label4.Text = $"Wynik w¹tku 1:  {finalResult}"; });
            });

            calculationThread.Start();
        }
        private void btnCalculate_Click(object sender, EventArgs e)
        {
            CalculateThread();

        }

        private void bitMapTread()
        {
            stopbitMap = false;
            bitMap = new Thread(() =>
            {

                int width = 640;
                int height = 480;
                Random rand = new Random();
                Bitmap bmp = new Bitmap(width, height);
                for (int y = 0; y < height; y++)
                {
                    if (stopbitMap)
                    {
                        break;
                    }
                    for (int x = 0; x < width; x++)
                    {
                        Color color = Color.FromArgb(255, rand.Next(256), rand.Next(256), rand.Next(256));
                        bmp.SetPixel(x, y, color);
                    }

                    Invoke(new Action(() => pictureBox.Image = bmp));
                    Thread.Sleep(100);
                }
            });
            bitMap.Start();
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            bitMapTread();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            stopCalculate = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            stopSilnia = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            stopbitMap = true;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            bitMapTread();
            silniaThread();
            CalculateThread();
        }

        private void silniaThread()
        {
            int n = int.Parse(number.Text);


            stopSilnia = false;
            silnia = new Thread(() =>
            {

                int result = 1;

                for (int i = 1; i <= n; i++)
                {

                    result *= i;
                }
                string display = "";
                int silnia = 1;

                for (int i = 1; i <= n; i++)
                {
                    if (stopSilnia)
                    {
                        break;
                    }
                    if (i == 1)
                        display = "1";
                    else
                        display += $" x {i}";
                    silnia = silnia * i;

                    Invoke((MethodInvoker)delegate { label1.Text = $"Silnia liczby:  {n}, wynik: {silnia}"; label2.Text = $"Dzia³anie licz¹ce silnie: {display}"; });
                    Thread.Sleep(500);
                }
            });

            silnia.Start();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            silniaThread();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            stopCalculate = true;
            stopbitMap = true;
            stopSilnia = true;
        }
    }
}
