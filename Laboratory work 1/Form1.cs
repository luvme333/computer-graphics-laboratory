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
            SharpnessFilter filter = new SharpnessFilter();
            Bitmap resultImage = filter.processImage(image);
            pictureBox1.Image = resultImage;
            pictureBox1.Refresh();
        }
    }
}
