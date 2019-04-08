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
        //double x1, x2, y1, y2;
        private int cntClear = 2;
        private int cntClr = 2;

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

        private void Color_Click(object sender, RoutedEventArgs e)
        {
            switch ((sender as Button).Name)
            {
                case "Black":
                {
                    InkCanvas1.DefaultDrawingAttributes.Color = Colors.Black;
                    break;
                }
                case "Gray":
                {
                    InkCanvas1.DefaultDrawingAttributes.Color = Colors.Gray;
                    break;
                }
                case "White":
                {
                    InkCanvas1.DefaultDrawingAttributes.Color = Colors.White;
                    break;
                }
                case "Red":
                {
                    InkCanvas1.DefaultDrawingAttributes.Color = Colors.Red;

                        break;
                }
                case "Orange":
                {
                    InkCanvas1.DefaultDrawingAttributes.Color = Colors.Orange;

                        break;
                }
                case "Yellow":
                {
                    InkCanvas1.DefaultDrawingAttributes.Color = Colors.Yellow;

                        break;
                }
                case "Pink":
                {
                    InkCanvas1.DefaultDrawingAttributes.Color = Colors.Pink;

                        break;
                }
                case "Green":
                {
                    InkCanvas1.DefaultDrawingAttributes.Color = Colors.Green;
                    break;
                }
                case "Aquamarine":
                {
                    InkCanvas1.DefaultDrawingAttributes.Color = Colors.Aquamarine;
                    break;
                }
                case "SkyBlue":
                {
                    InkCanvas1.DefaultDrawingAttributes.Color = Colors.SkyBlue;
                    break;
                }
                case "Blue":
                {
                    InkCanvas1.DefaultDrawingAttributes.Color = Colors.Blue;
                    break;
                }
                case "Purple":
                {
                    InkCanvas1.DefaultDrawingAttributes.Color = Colors.Purple;
                    break;
                }
            }
        }

        private void Thickness(object sender, RoutedEventArgs e)
        {
            switch ((sender as Button).Name)
            {
                case "ThicknessSmall":
                {
                    InkCanvas1.DefaultDrawingAttributes.Height = 2;
                    InkCanvas1.DefaultDrawingAttributes.Width = 2;
                        break;
                }
                case "ThicknessMedium":
                {
                    InkCanvas1.DefaultDrawingAttributes.Height = 10;
                    InkCanvas1.DefaultDrawingAttributes.Width = 10;
                    break;
                }
                case "ThicknessLarge":
                {
                    InkCanvas1.DefaultDrawingAttributes.Height = 20;
                    InkCanvas1.DefaultDrawingAttributes.Width = 20;
                    break;
                }
            }
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

        private void ClearingToolAll_Click(object sender, RoutedEventArgs e)
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

        private void ClearingTool_Click(object sender, RoutedEventArgs e)
        {
            cntClr++;
            if (cntClr % 2 == 1)
            {
                InkCanvas1.EditingMode = InkCanvasEditingMode.EraseByPoint;
            }
            else
            {
                InkCanvas1.EditingMode = InkCanvasEditingMode.Ink;
            }
        }
    }
}
