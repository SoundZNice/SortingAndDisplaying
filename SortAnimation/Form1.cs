using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using SortAlgorythms;

namespace SortAnimation
{
    public partial class Form1 : Form
    {
        List<Rectangle> _rectangles;
        int panelHeight;
        int panelWidth;
        int elementsNumber;
        Graphics g;
        Thread t1 ;
        Semaphore s1 = new Semaphore(1, 1);


        public Form1()
        {
            InitializeComponent();
            panelHeight = 1000;
            panelWidth = 1000;
            g = this.CreateGraphics();
            _rectangles = new List<Rectangle>();
        }

        private void drawArray()
        {

        }



        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void generateNewArray(int elementsNumber, int maxValue)
        {

        }

        private void Form1_Click(object sender, EventArgs e)
        {





        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = true;
            try
            {
                elementsNumber = Convert.ToInt32(elementsNumberTextBox.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            try
            {
                SolidBrush sb = new SolidBrush(Color.Black);
                g.FillRectangle(sb, 0, 0, panelWidth, panelHeight);
            }
            catch (Exception)
            {
                return;
            }
            int[] a;
            BubbleSort.createArray(out a, elementsNumber);

            _rectangles.Clear();

            foreach (Rectangle rec in convertToRectangleList(a))
            {
                _rectangles.Add(rec);
            }

            drawRectangles();

            ISort sortType;

            if (radioButton1.Checked)
            {
                sortType = (ISort)(new BubbleSort(a, a.Length));
            }
            else
            {
                sortType = (ISort)(new QuickSortClass(a, a.Length));
            }


            IEnumerable<ExchangingPositions> ieep = (sortType).getExchangingPositions();
            t1 = new Thread(drawing);
            t1.Start((object)ieep);
            
            
           

        }
        private void drawing(object o)
        {
            IEnumerable<ExchangingPositions> ieep = (IEnumerable<ExchangingPositions>)o;

            foreach (ExchangingPositions ep in ieep)
            {
                s1.WaitOne();
                s1.Release();
                int x, y;
                ep.getPos(out x, out y);


                swapRectangles(x, y);
            }
            button1.Enabled = true;
        }

        private IEnumerable<Rectangle> convertToRectangleList(int[] array)
        {
            for (int i = 0; i < elementsNumber; i++)
            {
                yield return new Rectangle(i, panelHeight - array[i], panelWidth / elementsNumber, array[i]);
            }
        }

        private void drawRectangles()
        {
            foreach (Rectangle rec in _rectangles)
            {
                g.FillRectangle(new SolidBrush(Color.Green), rec);
            }
        }

        private void swapRectangles(int r1Pos, int r2Pos)
        {
            SolidBrush sb = new SolidBrush(Color.Black);

            g.FillRectangle(sb, _rectangles[r1Pos]);
            g.FillRectangle(sb, _rectangles[r2Pos]);

            Rectangle rec = _rectangles[r2Pos];
            _rectangles[r2Pos] = new Rectangle(_rectangles[r2Pos].X, _rectangles[r1Pos].Y, _rectangles[r2Pos].Width, _rectangles[r1Pos].Height);
            _rectangles[r1Pos] = new Rectangle(_rectangles[r1Pos].X, rec.Y, _rectangles[r1Pos].Width, rec.Height);







            sb.Color = Color.Red;

            g.FillRectangle(sb, _rectangles[r1Pos]);
            g.FillRectangle(sb, _rectangles[r2Pos]);

            sb.Color = Color.Green;

            g.FillRectangle(sb, _rectangles[r1Pos]);
            g.FillRectangle(sb, _rectangles[r2Pos]);

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            radioButton2.Checked = radioButton1.Checked ? false : true;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            radioButton1.Checked = radioButton2.Checked ? false : true;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            t1.Abort();
        }

        private void Stop(object sender, EventArgs e)
        {            
            s1.WaitOne();
            button2.Enabled = false;
            button3.Enabled = true;
        }

        private void Resume(object sender, EventArgs e)
        {
            try
            {
                s1.Release(1);
                button2.Enabled = true;
                button3.Enabled = false;
                
            }
            catch (SemaphoreFullException)
            { }
        }
    }
}
