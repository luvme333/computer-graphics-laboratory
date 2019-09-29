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
        public int kernelSize;
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
            SharpnessFilter filter = new SharpnessFilter();
            Bitmap resultImage = filter.processImage(image);
            pictureBox1.Image = resultImage;
            pictureBox1.Refresh();
        }

        private void РасширениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Laboratory_work_1.Form2 form = new Laboratory_work_1.Form2();
            form.ShowDialog();
            Dilation filter = new Dilation();
            filter.kernelSize = form.kernelSize;
            Bitmap resultImage = filter.processImage(image);
            pictureBox1.Image = resultImage;
            pictureBox1.Refresh();
        }

        private void СужениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Laboratory_work_1.Form2 form = new Laboratory_work_1.Form2();
            form.ShowDialog();
            Erosion filter = new Erosion();
            filter.kernelSize = form.kernelSize;
            Bitmap resultImage = filter.processImage(image);
            pictureBox1.Image = resultImage;
            pictureBox1.Refresh();
        }

        private void ОткрытиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Laboratory_work_1.Form2 form = new Laboratory_work_1.Form2();
            form.ShowDialog();
            Erosion filter1 = new Erosion();
            filter1.kernelSize = form.kernelSize;
            Bitmap resultImage1 = filter1.processImage(image);
            Dilation filter2 = new Dilation();
            filter2.kernelSize = form.kernelSize;
            Bitmap resultImage2 = filter2.processImage(resultImage1);
            pictureBox1.Image = resultImage2;
            pictureBox1.Refresh();
        }

        private void ЗакрытиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Laboratory_work_1.Form2 form = new Laboratory_work_1.Form2();
            form.ShowDialog();
            Dilation filter1 = new Dilation();
            filter1.kernelSize = form.kernelSize;
            Bitmap resultImage1 = filter1.processImage(image);
            Erosion filter2 = new Erosion();
            filter2.kernelSize = form.kernelSize;
            Bitmap resultImage2 = filter2.processImage(resultImage1);
            pictureBox1.Image = resultImage2;
            pictureBox1.Refresh();
        }

        private void ГрадиентToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Laboratory_work_1.Form2 form = new Laboratory_work_1.Form2();
            form.ShowDialog();
            Gradient filter = new Gradient();
            filter.kernelSize = form.kernelSize;
            Bitmap resultImage = filter.processImage(image);
            pictureBox1.Image = resultImage;
            pictureBox1.Refresh();
        }

        private void МедианныйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MedianFilter filter = new MedianFilter();
            Bitmap resultImage = filter.processImage(image);
            pictureBox1.Image = resultImage;
            pictureBox1.Refresh();
        }

    }
}
