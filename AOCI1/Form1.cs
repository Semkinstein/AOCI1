using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AOCI1
{
    public partial class Form1 : Form
    {
        ImageEditor imgEditor = new ImageEditor();
        Image<Bgr, byte> image, defaultImage;
        public Form1()
        {
            InitializeComponent();
            button1.Click += new EventHandler(button1_Click);
            timer1.Enabled = false;

        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            timer1.Enabled = false;
            defaultImage = imgEditor.OpenImage(OpenImageFile());
            imgEditor.ShowImage(imageBox1, defaultImage);
            FillImage2();
        }

        private string OpenImageFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Файлы изображений (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif*.png";
            var result = openFileDialog.ShowDialog(); // открытие диалога выбора файла
            if (result == DialogResult.OK) // открытие выбранного файла
            {
                string fileName = openFileDialog.FileName;
                return fileName;
            }
            return null;
        }

        private string OpenVideoFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Media Files|*.mpg;*.avi;*.wma;*.mov;*.wav;*.mp2;*.mp3;*.mp4";
            var result = openFileDialog.ShowDialog(); // открытие диалога выбора файла
            if (result == DialogResult.OK) // открытие выбранного файла
            {
                string fileName = openFileDialog.FileName;
                return fileName;
            }
            return null;
        }

        private void imageBox2_Click(object sender, EventArgs e)
        {

        }

        private void imageBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void FillImage2()
        {
            image = imgEditor.EditImage(defaultImage, trackBar1.Value * 10, trackBar2.Value * 10, trackBar3.Value * 25, checkBox1.Checked, checkBox2.Checked);
            imgEditor.ShowImage(imageBox2, image);
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            if (image != null)
            {
                FillImage2();
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (image != null)
            {
                FillImage2();
            }
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            if (image != null)
            {
                FillImage2();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            defaultImage = imgEditor.GetVideoFrame();
            if (defaultImage != null)
            {
                imgEditor.ShowImage(imageBox1, defaultImage);
                FillImage2();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (image != null)
            {
                FillImage2();
                checkBox2.Checked = false;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (image != null)
            {
                FillImage2();
                checkBox1.Checked = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            imgEditor.OpenVideo(OpenVideoFile());
            timer1.Enabled = true;
        }
    }
}
