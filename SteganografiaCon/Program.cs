using System;
using System.Drawing;

namespace SteganografiaCon
{
    class Program
    {
        static void Main(string[] args)
        {
            
            while(true)
            {
                Console.Write("\nEnter image path: ");
                string sourcePath = Console.ReadLine();
                try{
                    var temp=Image.FromFile(sourcePath);
                    var bmp=new Bitmap(temp);

                    Console.Write("Enter message you want to hide: ");
                    string message = Console.ReadLine();

                    Console.Write("Enter path you want to save the image: ");
                    string targetPath = Console.ReadLine();

                    bmp=stegano(message,bmp);
                    bmp.Save(targetPath, System.Drawing.Imaging.ImageFormat.Bmp);
                    
                    Console.Write("Do you want to read the hidden text in an image?? \nPress Y for yes,other for no.\n");
                    string option = Console.ReadLine();
                    if(option=="Y" || option=="y"){
                        ReadText();
                    }
                    break;
                }

                catch(Exception)
                {
                    Console.Write("There was an error opening the bitmap." +
                    "Please check the path.");
                }
            }
        }
        public static void ReadText(){
            Console.Write("Enter image path you want to read the text: ");
            string stagPath = Console.ReadLine();
            var temp2=Image.FromFile(stagPath);
            var bmp1=new Bitmap(temp2);
            string mess = get_text(bmp1);
            Console.WriteLine("The hidden text is: "+ mess);
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
    }
}
