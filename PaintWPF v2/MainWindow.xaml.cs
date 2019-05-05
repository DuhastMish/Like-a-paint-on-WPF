using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace PaintWPF_v2
{
    public partial class MainWindow
    {
        private int cntClear = 2;
        private int cntClr = 2;
        public Brush ShcolorBrush = Brushes.Black;
        public bool TogglePolygon { get; set; }            //включатель многоугольника

        public MainWindow()
        {
            InitializeComponent();
            InkCanvas1.EditingMode = InkCanvasEditingMode.Ink;
        }
        public PointCollection Points { get; set; }                     //коллекция точек
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
                    "Ink Serialized Format (*.isf)|*.isf|"+"Bitmap Image (.bmp)|*.bmp|Gif Image (.gif)|*.gif|JPEG Image (.jpeg)|*.jpeg|Png Image (.png)|*.png"
            };

            var result = saveimg.ShowDialog();
            if (result == true)
            {
                using (FileStream file = new FileStream(saveimg.FileName,
                    FileMode.Create, FileAccess.Write))
                {
                    if (saveimg.FilterIndex == 1)
                    {
                        this.InkCanvas1.Strokes.Save(file);
                        file.Close();
                    }
                    else
                    {
                        int marg = int.Parse(this.InkCanvas1.Margin.Left.ToString());
                        RenderTargetBitmap rtb = new RenderTargetBitmap((int)this.InkCanvas1.ActualWidth - marg,
                            (int)this.InkCanvas1.ActualHeight - marg, 0, 0, PixelFormats.Default);
                        rtb.Render(this.InkCanvas1);
                        BmpBitmapEncoder encoder = new BmpBitmapEncoder();
                        encoder.Frames.Add(BitmapFrame.Create(rtb));
                        encoder.Save(file);
                        file.Close();
                    }
                }
                //int marg = int.Parse(this.InkCanvas1.Margin.Left.ToString());
                //var filename = saveimg.FileName;
                //FileStream fs = new FileStream(filename, FileMode.Create);
                //RenderTargetBitmap rtb =
                //    new RenderTargetBitmap((int)this.InkCanvas1.ActualWidth - marg,
                //        (int)this.InkCanvas1.ActualHeight - marg, 0, 0,
                //        PixelFormats.Default);
                ////RenderTargetBitmap rtb = new RenderTargetBitmap((int)InkCanvas1.Width, (int)InkCanvas1.Height, 96d, 96d, PixelFormats.Default);
                //rtb.Render(InkCanvas1);
                //JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                //encoder.Frames.Add(BitmapFrame.Create(rtb));

                //encoder.Save(fs);
                //fs.Close();
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
            Shape = "Clear";
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
                "Ink Serialized Format (*.isf)|*.isf|"+"Bitmap Image (.bmp)|*.bmp|Gif Image (.gif)|*.gif|JPEG Image (.jpeg)|*.jpeg|Png Image (.png)|*.png";
            var result = openImg.ShowDialog();
            if (result == true)
            {
                InkCanvas1.Background = Brushes.White;
                InkCanvas1.Strokes.Clear();
                try
                {
                    using (FileStream file = new FileStream(openImg.FileName,
                        FileMode.Open, FileAccess.Read))
                    {
                        if (openImg.FileName.ToLower().EndsWith(".isf"))
                        {
                            this.InkCanvas1.Strokes = new StrokeCollection(file);
                            file.Close();
                        }
                        else
                        {
                            ImageBrush brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri(openImg.FileName, UriKind.Relative));
                            InkCanvas1.Background = brush;
                        }
                    }
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message, Title);
                }
                //var filename = openImg.FileName;
                //ImageBrush brush = new ImageBrush();
                //brush.ImageSource = new BitmapImage(new Uri(filename, UriKind.Relative));
                //InkCanvas1.Background = brush;
            }
        }

        private void ClearingTool_Click(object sender, RoutedEventArgs e)
        {
            Shape = "Clear";
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
                    TogglePolygon = false;
                        Shape = "Rectangle";
                    InkCanvas1.EditingMode = InkCanvasEditingMode.None;
                    break;
                }
                case "Ellipse":
                {
                    TogglePolygon = false;
                        Shape = "Ellipse";
                    InkCanvas1.EditingMode = InkCanvasEditingMode.None;
                    break;
                }
                case "Line":
                    TogglePolygon = false;
                    Shape = "Line";
                    InkCanvas1.EditingMode = InkCanvasEditingMode.None;
                    break;
                case "Circle":
                    TogglePolygon = false;
                    Shape = "Circle";
                    InkCanvas1.EditingMode = InkCanvasEditingMode.None;
                    break;
                case "Polygon":
                    Shape = "Polygon";
                    InkCanvas1.EditingMode = InkCanvasEditingMode.None;
                    break;
            }
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            try
            {
                InkCanvas1.Children.RemoveAt(InkCanvas1.Children.Count - 1);
            }
            catch { };
            try
            {
                InkCanvas1.Strokes.RemoveAt(InkCanvas1.Strokes.Count - 1);
            }
            catch { };
        }
        private void DrawLine(Point p1, Point p2)           //рисует линию
        {
            var myLine = new Line
            {
                Stroke = ShapeColorBrush,
                X1 = p1.X,
                X2 = p2.X,
                Y1 = p1.Y,
                Y2 = p2.Y,
                StrokeThickness = StrokeShape
            };
            InkCanvas1.Children.Add(myLine);
        }

        private void DrawRectangle(double X, double Y)
        {
            var myRectangle = new Rectangle
            {
                Fill = ShapeColorBrush,
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
                Fill = ShapeColorBrush,
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
        private void BuildLine(Point p)                                            //рисование прямой при нажатой левой кнопки мыши
        {
            Line newLine = (Line)InkCanvas1.Children[InkCanvas1.Children.Count - 1];
            newLine.X2 = p.X;
            newLine.Y2 = p.Y;
        }
        private void BuildCircle(Point p)                                          //рисование круга при нажатой левой кнопки мыши
        {
            Ellipse newCircle = (Ellipse)InkCanvas1.Children[InkCanvas1.Children.Count - 1];
            double tempX, tempY, tempW, tempH;

            //определение диаметра круга

            tempW = 2 * Math.Abs((p.X) - Coordinates.X);
            tempH = 2 * Math.Abs((p.Y) - Coordinates.Y);

            if (tempH >= tempW)                                     //определение диаметра круга
            {
                newCircle.Height = tempH;
                newCircle.Width = tempH;
                tempX = Coordinates.X - tempH / 2;
                tempY = Coordinates.Y - tempH / 2;
            }
            else
            {
                newCircle.Width = tempW;
                newCircle.Height = tempW;
                tempX = Coordinates.X - tempW / 2;
                tempY = Coordinates.Y - tempW / 2;
            }
            newCircle.Margin = new Thickness(tempX, tempY, 0, 0);      //определение координат круга, от краёв альбома
        }

        //private void BuildPolyLine()                                               //добавление на рисунок обьекта многолинейника :) 
        //{
        //    Polyline Poly = (Polyline)InkCanvas1.Children[InkCanvas1.Children.Count - 1];
        //    Poly.Points.Add(Coordinates);
        //}
        private void BuildPolygon()                                                //добавление на рисунок обьекта многоугольника
        {
            Polygon Poly = (Polygon)InkCanvas1.Children[InkCanvas1.Children.Count - 1];
            Poly.Points.Add(Coordinates);
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
                case "Line":
                    DrawLine(Coordinates, Coordinates);
                    break;
                case "Circle":
                    DrawEllipse(Coordinates.X, Coordinates.Y);
                    break;
                case "Polygon":
                    if (TogglePolygon)
                    {
                        BuildPolygon();
                    }
                    else DrawPolygon();
                    break;
            }
        }
        private void DrawPolygon()                                          //создание обьекта многоугольника
        {
            TogglePolygon = true;
            Points = new PointCollection
            {
                Coordinates
            };
            Polygon myPolygon = new Polygon
            {
                Fill = ShapeColorBrush,
                Stroke = ShcolorBrush,
                StrokeThickness = StrokeShape
            };
            myPolygon.Points = Points;
            InkCanvas1.Children.Add(myPolygon);
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
            NumCh.Content = InkCanvas1.Children.Count;
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
                case "Circle":
                    BuildCircle(MovePoint);
                    break;
                case "Line":
                    BuildLine(MovePoint);
                    break;
            }

        }

        private void Pen_Click(object sender, RoutedEventArgs e)
        {
            Shape = string.Empty;
            InkCanvas1.EditingMode = InkCanvasEditingMode.Ink;
        }

        private void Cursor_OnClick(object sender, RoutedEventArgs e)
        {
            Shape = "Cursor";
            InkCanvas1.EditingMode = InkCanvasEditingMode.Select;
        }
    }
}