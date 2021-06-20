using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Memorija1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            progressBar1.Visible = false;
            progressBar2.Visible = false;
            button1.Visible = false;
            button2.Visible = false;

        }
        byte process;
        PictureBox previosImage;
        byte remaining = 18;
        byte time = 150;
        void photos()
        {
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox)
                {
                    (x as PictureBox).Image = Properties.Resources._0;
                }
            }
        }

        void tags()
        {
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox)
                {
                    (x as PictureBox).Tag = "0";
                }
            }
        }

        void tagDistribution()
        {
            int[] number = new int[36];
            Random random = new Random();

            byte i = 0;
            while (i < 36)
            {
                int ran = random.Next(1, 37);
                if (Array.IndexOf(number, ran) == -1)
                {
                    number[i] = ran;
                    i++;
                }
            }
            for (byte a = 0; a < 36; a++)
            {
                if (number[a] > 18) number[a] -= 18;
            }
            byte b = 0;
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox)
                {
                    x.Tag = number[b].ToString();
                    b++;
                }
            }
        }
        void timer3_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Value != 0)
            {
                progressBar1.Value--;
            }
            else if (progressBar1.Value == 0)
            {
                timer3.Stop();
                progressBar1.Visible = false;
            }
        }
        void timer2_Tick(object sender, EventArgs e)
        {
            if (progressBar2.Value != 0)
            {
                progressBar2.Value--;
            }
            else if (progressBar2.Value == 0)
            {
                timer2.Stop();
                button1.Enabled = true;
                progressBar2.Visible = false;
                sender = 0;
            }
        }
        void compare(PictureBox previos,PictureBox next)
        {
            if (previos.Tag.ToString() == next.Tag.ToString())
            {
                Application.DoEvents();
                System.Threading.Thread.Sleep(500);
                previos.Visible = false;
                next.Visible = false;
               
                remaining--;
                if (remaining == 0)
                {
                    left.Text = "Честитки, завршивте";
                    button1.Visible = false;
                    button2.Visible = false;
                    panel1.Visible = true;
                    label1.Visible = false;
                    progressBar1.Visible = false;
                    progressBar2.Visible = false;
                }
                else
                    left.Text = "Преостанати фигури = " + remaining;
                timer1.Enabled = false;
                
            }
            else
            {
                Application.DoEvents();
                System.Threading.Thread.Sleep(500);
                previos.Image = Image.FromFile("0.jpg");
                next.Image = Image.FromFile("0.jpg");
               
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Меморија";
            photos();
            tags();
            tagDistribution();
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

            PictureBox nextPicture = (sender as PictureBox);
            nextPicture.Image = Image.FromFile((sender as PictureBox).Tag.ToString() + ".jpg");
            if (process == 0)
            {
                previosImage = nextPicture;
                process++;
            }
            else
            {
                if (previosImage == nextPicture)
                {
                    MessageBox.Show("Отворивте исто поле");
                    process = 0;
                    previosImage.Image = Image.FromFile("0.jpg");

                }
                else
                {
                    compare(previosImage, nextPicture);
                    process = 0;
                }
            }
        }
        void display()
        {
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox)
                {
                    (x as PictureBox).Image = Image.FromFile(x.Tag.ToString() + ".jpg");
                }
            }
        }
        void hide()
        {
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox)
                {
                    (x as PictureBox).Image = Image.FromFile("0.jpg");
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
           
            display();
            Application.DoEvents();
            System.Threading.Thread.Sleep(500);
            hide();
            process = 0;
            button1.Enabled = false;
            timer2.Tick -= new EventHandler(timer2_Tick);
            progressBar2.Visible = true;
            progressBar2.Value = 90;
            timer2.Enabled = true;
            timer2.Stop();
            timer2.Start();
            timer2.Tick += new EventHandler(timer2_Tick);
        }
        void visibleAc()
        {
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox)
                {
                    (x as PictureBox).Visible = true;

                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            photos();
            tags();
            tagDistribution();
            visibleAc();
            remaining = 18;
            process = 0;
            time = 150;
            timer1.Enabled = true;
            progressBar1.Value = 150;
            timer2.Stop();
            progressBar2.Visible = false;
            progressBar2.Value = 90;
            button1.Enabled = true;
        }
        void stop()
        {
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox)
                {
                    (x as PictureBox).Enabled = false;
                }
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            time -= 1;
            label1.Text = "Преостанато Време: " +time;
            if (time == 0)
            {
                timer1.Stop();
                timer2.Stop();
                timer3.Stop();
                label1.Text = "Крај на играта, Времето измина";
                stop();
                this.Enabled = false;
                progressBar1.Visible = false;
                button1.Visible = false;
                button2.Visible = false;
            }
        }

        private void left_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            label1.Visible = true;
            left.Visible = true;
            time = 150;
            remaining = 18;
            process = 0;
            progressBar1.Visible = true;
            progressBar1.Value = 150;
            timer3.Tick -= new EventHandler(timer3_Tick);
            timer1.Enabled = true;
            timer3.Enabled = true;
            timer3.Stop();
            timer3.Start();
            timer3.Tick += new EventHandler(timer3_Tick);
            panel1.Visible = false;
            button1.Visible = true;
            button2.Visible = true;

            

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }
        
    }
}
