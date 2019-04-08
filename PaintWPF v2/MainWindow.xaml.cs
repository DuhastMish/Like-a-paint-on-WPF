using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;
using System.Threading;

namespace PaintWPF_v2
{
    public partial class MainWindow : Window
    {
        double x1, x2, y1, y2;
        public Brush DrawColor;
        int cntClear = 2;
        public MainWindow()
        {
            InitializeComponent();
            InkCanvas1.EditingMode = InkCanvasEditingMode.Ink;
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            InkCanvas1.Strokes.Clear();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveimg = new SaveFileDialog();
            saveimg.FileName = "Изображение";
            saveimg.Filter = "Bitmap Image (.bmp)|*.bmp|Gif Image (.gif)|*.gif|JPEG Image (.jpeg)|*.jpeg|Png Image (.png)|*.png";

            bool? result = saveimg.ShowDialog();
            if (result == true)
            {
                string filename = saveimg.FileName;
                var fs = new FileStream(filename, FileMode.Create);
                InkCanvas1.Strokes.Save(fs);
            }
        }

        private void Black_Click(object sender, RoutedEventArgs e)
        {
            InkCanvas1.DefaultDrawingAttributes.Color = Colors.Black;
        }

        private void Gray_Click(object sender, RoutedEventArgs e)
        {
            InkCanvas1.DefaultDrawingAttributes.Color = Colors.Gray;
        }

        private void White_Click(object sender, RoutedEventArgs e)
        {
            InkCanvas1.DefaultDrawingAttributes.Color = Colors.White;
        }

        private void Red_Click(object sender, RoutedEventArgs e)
        {
            InkCanvas1.DefaultDrawingAttributes.Color = Colors.Red;
        }

        private void Orange_Click(object sender, RoutedEventArgs e)
        {
            InkCanvas1.DefaultDrawingAttributes.Color = Colors.Orange;
        }

        private void Yellow_Click(object sender, RoutedEventArgs e)
        {
            InkCanvas1.DefaultDrawingAttributes.Color = Colors.Yellow;
        }

        private void Pink_Click(object sender, RoutedEventArgs e)
        {
            InkCanvas1.DefaultDrawingAttributes.Color = Colors.Pink;
        }

        private void Green_Click(object sender, RoutedEventArgs e)
        {
            InkCanvas1.DefaultDrawingAttributes.Color = Colors.Green;
        }

        private void Aquamarine_Click(object sender, RoutedEventArgs e)
        {
            InkCanvas1.DefaultDrawingAttributes.Color = Colors.Aquamarine;
        }

        private void SkyBlue_Click(object sender, RoutedEventArgs e)
        {
            InkCanvas1.DefaultDrawingAttributes.Color = Colors.SkyBlue;
        }

        private void Blue_Click(object sender, RoutedEventArgs e)
        {
            InkCanvas1.DefaultDrawingAttributes.Color = Colors.Blue;
        }

        private void Resize_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int tempHeight = Convert.ToInt16(HeightCanvas.Text);
                int tempWidth = Convert.ToInt16(WidthCanvas.Text);
                InkCanvas1.Height = tempHeight;
                InkCanvas1.Width = tempWidth;
            }
            catch
            {
                MessageBox.Show("Введите целые числа ниже(H-высоту и W-ширину)");
            }
        }

        private void Purple_Click(object sender, RoutedEventArgs e)
        {
            InkCanvas1.DefaultDrawingAttributes.Color = Colors.Purple;
        }

        private void ClearingTool_Click(object sender, RoutedEventArgs e)
        {
            cntClear++;
            if (cntClear % 2 == 1)
            {
                InkCanvas1.EditingMode = InkCanvasEditingMode.EraseByStroke;
            }
            else
            {
                InkCanvas1.EditingMode = InkCanvasEditingMode.Ink;
            }
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openImg = new OpenFileDialog();
            openImg.FileName = "*";
            openImg.Filter = "Bitmap Image (.bmp)|*.bmp|Gif Image (.gif)|*.gif|JPEG Image (.jpeg)|*.jpeg|Png Image (.png)|*.png";
            var result = openImg.ShowDialog();
            if (result != true) return;
            string filename = openImg.FileName;
            var fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            InkCanvas1.Strokes = new StrokeCollection(fs);
        }
    }
}
