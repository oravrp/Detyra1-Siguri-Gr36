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
        //Hapja e fotografise 
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.ImageLocation = openFileDialog.FileName;
            }
        }

        public static int bit_reverse(int n)
        {
            int rezultati = 0;

            for (int i = 0; i < 8; i++)
            {
                rezultati = rezultati * 2 + n % 2;

                n /= 2;
            }

            return rezultati;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(pictureBox1.ImageLocation);
            pictureBox1.Image = bmp;
            string message = "FIEK-projekti SI";
            bmp = stegano(message, bmp);

            SaveFileDialog svd = new SaveFileDialog();
            if (svd.ShowDialog() == DialogResult.OK)
            {
                bmp.Save(svd.FileName);
                pictureBox2.ImageLocation = svd.FileName;
            }

        }

        //Enum per ruajtjen e gjendjeve
        public enum Situation
        {
            encrypting,
            filled
        };

        //Funksioni per fshehjen e informates ne fotografi
        public static Bitmap stegano(string text, Bitmap bmp)
        {
            //Fshehja e characters ne fotografi
            Situation state = Situation.encrypting;

            int char_index = 0, char_val = 0, zeros = 0;
            long rgb_index = 0;

            int R = 0;
            int G = 0;
            int B = 0;


            for (int i = 0; i < bmp.Height; i++)
            {
                //kalimi ne cdo rresht
                for (int j = 0; j < bmp.Width; j++)
                {
                    //ruajtja e pixelit qe eshte duke u procesuar
                    Color pixel = bmp.GetPixel(j, i);

                    //Largimi i bitit least significant nga te tri ngjyrat e pixelit
                    R = pixel.R - pixel.R % 2;
                    G = pixel.G - pixel.G % 2;
                    B = pixel.B - pixel.B % 2;

                    //per cdo pixel,kalimi ne cdo element (RGB)
                    for (int n = 0; n < 3; n++)
                    {

                        if (rgb_index % 8 == 0)
                        {

                            if (state == Situation.filled && zeros == 8)
                            {

                                if (rgb_index % 3 <= 2)
                                {
                                    bmp.SetPixel(j, i, Color.FromArgb(R, G, B));
                                }

                                return bmp;
                            }

                            if (char_index >= text.Length)
                            {
                                state = Situation.filled;
                            }
                            else
                            {
                                char_val = text[char_index++];
                            }
                        }

                        if (rgb_index % 3 == 0)
                        {
                            if (state == Situation.encrypting)
                            {

                                R += char_val % 2;
                                char_val /= 2;
                            }
                        }
                        else if (rgb_index % 3 == 1)
                        {
                            if (state == Situation.encrypting)
                            {
                                G += char_val % 2;

                                char_val /= 2;
                            }
                        }
                        else if (rgb_index % 3 == 2)
                        {
                            if (state == Situation.encrypting)
                            {
                                B += char_val % 2;

                                char_val /= 2;
                            }

                            bmp.SetPixel(j, i, Color.FromArgb(R, G, B));

                        }

                        rgb_index++;

                        if (state == Situation.filled)
                        {
                            zeros++;
                        }
                    }
                }
            }
            return bmp;
        }

        //Funksioni per leximin e tekstit te fshehur
        public static string get_text(Bitmap bmp)
        {
            int col_index = 0, char_val = 0;
 
            string teksti = String.Empty;
 
            for (int i = 0; i < bmp.Height; i++)
            {
 
                for (int j = 0; j < bmp.Width; j++)
                {
                    Color pixel = bmp.GetPixel(j, i);
 
                    for (int n = 0; n < 3; n++)
                    {
 
                        if (col_index % 3 == 0)
                        {
                            char_val = char_val * 2 + pixel.R % 2;
                        }
                        else if (col_index % 3 == 1)
                        {
                            char_val = char_val * 2 + pixel.G % 2;
                        }
                        else if (col_index % 3 == 2)
                        {
                            char_val = char_val * 2 + pixel.B % 2;
                        }
 
                        col_index++;
 
                        if (col_index % 8 == 0)
                        {
                            char_val = bit_reverse(char_val);
 
                            if (char_val == 0)
                            {
                                return teksti;
                            }
                            char c = (char)char_val;
                            teksti += c.ToString();
                        }
                    }
                }
            }
 
            return teksti;
        }

        //Butoni 3: Read Text
        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            if (opf.ShowDialog() == DialogResult.OK)
            {
                string path = opf.FileName;
                Bitmap bmp = new Bitmap(path);
                string mess = get_text(bmp);
                MessageBox.Show(mess);
            }
        }
    }
}
