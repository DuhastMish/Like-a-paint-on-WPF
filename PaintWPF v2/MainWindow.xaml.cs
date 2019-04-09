using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace PaintWPF_v2
{
    public partial class MainWindow
    {
        private int cntClear = 2;
        private int cntClr = 2;
        public Brush ShcolorBrush = Brushes.Black;

        public MainWindow()
        {
            InitializeComponent();
            InkCanvas1.EditingMode = InkCanvasEditingMode.Ink;
        }

        public Point Coordinates { get; set; }
        public double StrokeShape { get; set; } = 2;
        public string Shape { get; set; }
        public bool Toggle { get; set; }

        private Brush ShapeColorBrush
        {
            get => ShcolorBrush;
            set => ShcolorBrush = value;
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            InkCanvas1.Strokes.Clear();
            InkCanvas1.Children.Clear();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            var saveimg = new SaveFileDialog
            {
                FileName = "Изображение",
                Filter =
                    "Bitmap Image (.bmp)|*.bmp|Gif Image (.gif)|*.gif|JPEG Image (.jpeg)|*.jpeg|Png Image (.png)|*.png"
            };

            var result = saveimg.ShowDialog();
            if (result == true)
            {
                var filename = saveimg.FileName;
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
                    ShapeColorBrush = Brushes.Black;
                    break;
                }
                case "Gray":
                {
                    InkCanvas1.DefaultDrawingAttributes.Color = Colors.Gray;
                    ShapeColorBrush = Brushes.Gray;
                    break;
                }
                case "White":
                {
                    InkCanvas1.DefaultDrawingAttributes.Color = Colors.White;
                    ShapeColorBrush = Brushes.White;
                    break;
                }
                case "Red":
                {
                    InkCanvas1.DefaultDrawingAttributes.Color = Colors.Red;
                    ShapeColorBrush = Brushes.Red;
                    break;
                }
                case "Orange":
                {
                    InkCanvas1.DefaultDrawingAttributes.Color = Colors.Orange;
                    ShapeColorBrush = Brushes.Orange;
                    break;
                }
                case "Yellow":
                {
                    InkCanvas1.DefaultDrawingAttributes.Color = Colors.Yellow;
                    ShapeColorBrush = Brushes.Yellow;
                    break;
                }
                case "Pink":
                {
                    InkCanvas1.DefaultDrawingAttributes.Color = Colors.Pink;
                    ShapeColorBrush = Brushes.Pink;
                    break;
                }
                case "Green":
                {
                    InkCanvas1.DefaultDrawingAttributes.Color = Colors.Green;
                    ShapeColorBrush = Brushes.Green;
                    break;
                }
                case "Aquamarine":
                {
                    InkCanvas1.DefaultDrawingAttributes.Color = Colors.Aquamarine;
                    ShapeColorBrush = Brushes.Aquamarine;
                    break;
                }
                case "SkyBlue":
                {
                    InkCanvas1.DefaultDrawingAttributes.Color = Colors.SkyBlue;
                    ShapeColorBrush = Brushes.SkyBlue;
                    break;
                }
                case "Blue":
                {
                    InkCanvas1.DefaultDrawingAttributes.Color = Colors.Blue;
                    ShapeColorBrush = Brushes.Blue;
                    break;
                }
                case "Purple":
                {
                    InkCanvas1.DefaultDrawingAttributes.Color = Colors.Purple;
                    ShapeColorBrush = Brushes.Purple;
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
                    StrokeShape = 2;
                    break;
                }
                case "ThicknessMedium":
                {
                    InkCanvas1.DefaultDrawingAttributes.Height = 10;
                    InkCanvas1.DefaultDrawingAttributes.Width = 10;
                    StrokeShape = 10;
                    break;
                }
                case "ThicknessLarge":
                {
                    InkCanvas1.DefaultDrawingAttributes.Height = 20;
                    InkCanvas1.DefaultDrawingAttributes.Width = 20;
                    StrokeShape = 20;
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
                InkCanvas1.EditingMode = InkCanvasEditingMode.EraseByStroke;
            else
                InkCanvas1.EditingMode = InkCanvasEditingMode.Ink;
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            var openImg = new OpenFileDialog();
            openImg.FileName = "*";
            openImg.Filter =
                "Bitmap Image (.bmp)|*.bmp|Gif Image (.gif)|*.gif|JPEG Image (.jpeg)|*.jpeg|Png Image (.png)|*.png";
            var result = openImg.ShowDialog();
            if (result != true) return;
            var filename = openImg.FileName;
            var fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            InkCanvas1.Strokes = new StrokeCollection(fs);
        }

        private void ClearingTool_Click(object sender, RoutedEventArgs e)
        {
            cntClr++;
            if (cntClr % 2 == 1)
                InkCanvas1.EditingMode = InkCanvasEditingMode.EraseByPoint;
            else
                InkCanvas1.EditingMode = InkCanvasEditingMode.Ink;
        }

        private void ShapeChanged(object sender, RoutedEventArgs e)
        {
            switch ((sender as Button).Name)
            {
                case "Rectangle":
                {
                    Shape = "Rectangle";
                    InkCanvas1.EditingMode = InkCanvasEditingMode.None;
                    break;
                }
                case "Ellipse":
                {
                    Shape = "Ellipse";
                    InkCanvas1.EditingMode = InkCanvasEditingMode.None;
                    break;
                }
            }
        }

        private void DrawRectangle(double X, double Y)
        {
            var myRectangle = new Rectangle
            {
                Stroke = ShapeColorBrush,
                Margin = new Thickness(X, Y, 0, 0),
                StrokeThickness = StrokeShape,
                Height = 1,
                Width = 1
            };
            InkCanvas1.Children.Add(myRectangle);
        }

        private void DrawEllipse(double X, double Y)
        {
            var myEllipse = new Ellipse
            {
                Stroke = ShapeColorBrush,
                Margin = new Thickness(X, Y, 0, 0),
                StrokeThickness = StrokeShape,
                Height = 1,
                Width = 1
            };
            InkCanvas1.Children.Add(myEllipse);
        }

        private void BuildShape(Point p)
        {
            var newShape = (Shape) InkCanvas1.Children[InkCanvas1.Children.Count - 1];
            double tempX, tempY;
            if (p.X - Coordinates.X > 0)
            {
                newShape.Width = p.X - Coordinates.X;
                tempX = Coordinates.X;
            }
            else
            {
                newShape.Width = Coordinates.X - p.X;
                tempX = p.X;
            }

            if (p.Y - Coordinates.Y > 0)
            {
                newShape.Height = p.Y - Coordinates.Y;
                tempY = Coordinates.Y;
            }
            else
            {
                newShape.Height = Coordinates.Y - p.Y;
                tempY = p.Y;
            }

            newShape.Margin = new Thickness(tempX, tempY, 0, 0);
        }

        private void StartDrawFigure()
        {
            switch (Shape)
            {
                case "Rectangle":
                    DrawRectangle(Coordinates.X, Coordinates.Y);
                    break;
                case "Ellipse":
                    DrawEllipse(Coordinates.X, Coordinates.Y);
                    break;
            }
        }

        private void InkCanvas1_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Coordinates = new Point(e.GetPosition(InkCanvas1).X, e.GetPosition(InkCanvas1).Y);
            StartDrawFigure();
            Toggle = true;
        }

        private void InkCanvas1_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Toggle = false;
        }

        private void InkCanvas1_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            Coord.Text = e.GetPosition(InkCanvas1).X + " , " + e.GetPosition(InkCanvas1).Y;
            if (Toggle == false) return;
            var MovePoint = new Point
            {
                X = e.GetPosition(InkCanvas1).X,
                Y = e.GetPosition(InkCanvas1).Y
            };
            switch (Shape)
            {
                case "Rectangle":
                    BuildShape(MovePoint);
                    break;
                case "Ellipse":
                    BuildShape(MovePoint);
                    break;
            }
        }

        private void Pen_Click(object sender, RoutedEventArgs e)
        {
            Shape = string.Empty;
            InkCanvas1.EditingMode = InkCanvasEditingMode.Ink;
        }
    }
}