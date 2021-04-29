﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace ASII_Generator
{
    class Program
    {
        private const double WIDTH_OFFSET = 1.5;
        private const int MAX_WIDTH = 350;

        [STAThread]
        static void Main(string[] args)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Image | *.bmp; *.png; *.jpg; *.JPEG"
            };

            Console.WriteLine("U need press Enter for continue...");

            while (true)
            {
                Console.ReadLine();

                if (openFileDialog.ShowDialog() != DialogResult.OK)
                {
                    continue;
                }

                Console.Clear();

                var bitmap = new Bitmap(openFileDialog.FileName);
                bitmap = UpdateBitmap(bitmap);
                bitmap.ToGrayscale();

                var converter = new BitmapConverter(bitmap);
                var rows = converter.Convert();

                foreach (var row in rows)
                {
                    Console.WriteLine(row);
                }

                File.WriteAllLines("image.txt", rows.Select(r => new string(r)));

                Console.SetCursorPosition(0, 0);
                //char[] asciiTable = { '.', ',', ':','+','*','?','%','S','#','@' };
            }
        }

        private static Bitmap UpdateBitmap(Bitmap bitmap)
        {
            var newHeight = bitmap.Height / WIDTH_OFFSET * MAX_WIDTH / bitmap.Width;
            if (bitmap.Width > MAX_WIDTH || bitmap.Height > newHeight)
            {
                bitmap = new Bitmap(bitmap, new Size(MAX_WIDTH, (int)newHeight));
            }
            return bitmap;
        }
    }
}
