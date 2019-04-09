using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MarchingAnts
{
	/// <summary>
	/// MainWindow.xaml の相互作用ロジック
	/// </summary>
	public partial class MainWindow : Window
	{
		private Point startDrag;

		public MainWindow()
		{
			InitializeComponent();

			canvas.MouseDown += new MouseButtonEventHandler(canvas_MouseDown);
			canvas.MouseUp += new MouseButtonEventHandler(canvas_MouseUp);
			canvas.MouseMove += new MouseEventHandler(canvas_MouseMove);

		}

		//private void ContentPanel_ManipulationStarted(object sender, ManipulationStartedEventArgs e)
		//{
		//	rectangle1.Margin = new Thickness(e.ManipulationOrigin.X, e.ManipulationOrigin.Y, 0, 0);
		//	rectangle1.Width = 0;
		//	rectangle1.Height = 0;
		//	rectangle1.Visibility = System.Windows.Visibility.Visible;
		//}
		//private void ContentPanel_ManipulationDelta(object sender, ManipulationDeltaEventArgs e)
		//{
		//	rectangle1.Width = e.CumulativeManipulation.Translation.X;
		//	rectangle1.Height = e.CumulativeManipulation.Translation.Y;
		//}

		private void canvas_MouseDown(object sender, MouseButtonEventArgs e)
		{
			//Set the start point
			startDrag = e.GetPosition(canvas);
			//Move the selection marquee on top of all other objects in canvas
			Canvas.SetZIndex(rectangle, canvas.Children.Count);
			//Capture the mouse
			if (!canvas.IsMouseCaptured)
				canvas.CaptureMouse();
			canvas.Cursor = Cursors.Cross;
		}

		private void canvas_MouseUp(object sender, MouseButtonEventArgs e)
		{
			//Release the mouse
			if (canvas.IsMouseCaptured)
				canvas.ReleaseMouseCapture();
			canvas.Cursor = Cursors.Arrow;
		}

		private void canvas_MouseMove(object sender, MouseEventArgs e)
		{
			if (canvas.IsMouseCaptured)
			{
				Point currentPoint = e.GetPosition(canvas);

				//Calculate the top left corner of the rectangle regardless of drag direction
				double x = startDrag.X < currentPoint.X ? startDrag.X : currentPoint.X;
				double y = startDrag.Y < currentPoint.Y ? startDrag.Y : currentPoint.Y;

				if (rectangle.Visibility == Visibility.Hidden)
					rectangle.Visibility = Visibility.Visible;

				//Move the rectangle to proper place
				rectangle.RenderTransform = new TranslateTransform(x, y);
				//Set its size
				rectangle.Width = Math.Abs(e.GetPosition(canvas).X - startDrag.X);
				rectangle.Height = Math.Abs(e.GetPosition(canvas).Y - startDrag.Y);
			}
		}

		private void button_Click(object sender, RoutedEventArgs e)
		{
			rectangle.RenderTransform = new TranslateTransform(100, 100);
			rectangle.Width = 200;
			rectangle.Height = 200;
		}
	}
}
