using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Laboratory_work_1
{
    public partial class Form1 : Form
    {
        Bitmap image;
        public Form1()
        {
            InitializeComponent();
        }

        private void ОткрытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image files |*.png;*.jpg;*.bmp|All files (*.*)|*.*";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
            }
            image = new Bitmap(dialog.FileName);
            pictureBox1.Image = image;
            pictureBox1.Refresh();
        }

        private void ИнверсияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InvertFilter filter = new InvertFilter();
            Bitmap resultImage = filter.processImage(image);
            pictureBox1.Image = resultImage;
            pictureBox1.Refresh();
        }

        /*private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Bitmap newImage = ((Filters)e.Argument).processImage(image, backgroundWorker1);
            if (backgroundWorker1.CancellationPending = !true)
                image = newImage;
        }*/

        private void СерыйМирToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GrayWorldFilter filter = new GrayWorldFilter();
            Bitmap resultImage = filter.processImage(image);
            pictureBox1.Image = resultImage;
            pictureBox1.Refresh();
        }
        private void ЛинейноеРастяжениеГистограммыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LinearCorrection filter = new LinearCorrection();
            Bitmap resultImage = filter.processImage(image);
            pictureBox1.Image = resultImage;
            pictureBox1.Refresh();
        }
        private void ВолныToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Waves filter = new Waves();
            Bitmap resultImage = filter.processImage(image);
            pictureBox1.Image = resultImage;
            pictureBox1.Refresh();
        }
        private void РезкостьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MedianFilter filter = new MedianFilter();
            Bitmap resultImage = filter.processImage(image);
            pictureBox1.Image = resultImage;
            pictureBox1.Refresh();
        }

        private void РасширениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dilation filter = new Dilation();
            Bitmap resultImage = filter.processImage(image);
            pictureBox1.Image = resultImage;
            pictureBox1.Refresh();
        }

        private void СужениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Erosion filter = new Erosion();
            Bitmap resultImage = filter.processImage(image);
            pictureBox1.Image = resultImage;
            pictureBox1.Refresh();
        }

        private void ОткрытиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Erosion filter1 = new Erosion();
            Bitmap resultImage1 = filter1.processImage(image);
            Dilation filter2 = new Dilation();
            Bitmap resultImage2 = filter2.processImage(resultImage1);
            pictureBox1.Image = resultImage2;
            pictureBox1.Refresh();
        }

        private void ЗакрытиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dilation filter1 = new Dilation();
            Bitmap resultImage1 = filter1.processImage(image);
            Erosion filter2 = new Erosion();
            Bitmap resultImage2 = filter2.processImage(resultImage1);
            pictureBox1.Image = resultImage2;
            pictureBox1.Refresh();
        }

        private void ГрадиентToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Gradient filter = new Gradient();
            Bitmap resultImage = filter.processImage(image);
            pictureBox1.Image = resultImage;
            pictureBox1.Refresh();
        }
    }
}
