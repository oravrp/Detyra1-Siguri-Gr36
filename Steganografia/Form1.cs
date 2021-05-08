using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Steganografia
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.ImageLocation = openFileDialog.FileName;
            }
        }
        public enum State
        {
            Hiding,
            Filling_With_Zeros
        };
        private void button2_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(pictureBox1.ImageLocation);
            pictureBox1.Image = bmp;
            string message = "finally lol";
            bmp = embedText(message, bmp);

            SaveFileDialog svd = new SaveFileDialog();
            if (svd.ShowDialog() == DialogResult.OK)
            {
                bmp.Save(svd.FileName);
                pictureBox2.ImageLocation = svd.FileName;
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            if (opf.ShowDialog() == DialogResult.OK)
            {
                string path = opf.FileName;
                Bitmap bmp = new Bitmap(path);
                string mess = extractText(bmp);
                MessageBox.Show(mess);
            }

        }
    }
}
