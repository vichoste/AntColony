using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;

using AntColony.Colony;

using MaterialDesignThemes.Wpf;

namespace AntColony.Main;
/// <summary>
/// Main window
/// </summary>
public partial class MainView : Window {
	#region View model
	private readonly MainViewModel _MainViewModel;
	#endregion
	#region Constructors
	/// <summary>
	/// Initializes the window
	/// </summary>
	public MainView() {
		this.InitializeComponent();
		this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
		this.MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth;
		this._MainViewModel = (MainViewModel)this.DataContext;
	}
	#endregion
	#region Event methods
	/// <summary>
	/// Minimizes the application
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	private void Minimize(object sender, RoutedEventArgs e) => this.WindowState = WindowState.Minimized;

	/// <summary>
	/// Maximizes the application
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	private void Maximize(object sender, RoutedEventArgs e) => this.MaximizeWindow();

	/// <summary>
	/// Exits the application
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	private void Exit(object sender, RoutedEventArgs e) => this.Close();

	/// <summary>
	/// Moves the window
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	private void CheckMouseDown(object sender, MouseButtonEventArgs e) {
		if (e.LeftButton is MouseButtonState.Pressed) {
			if (e.ClickCount is 2 && this.WindowState is WindowState.Normal) {
				this.MaximizeWindow();
			}
			if (e.LeftButton is MouseButtonState.Pressed && this.WindowState is WindowState.Maximized && e.ClickCount < 2) {
				this.WindowState = WindowState.Normal;
				var dataContext = (MainViewModel)this.DataContext;
				dataContext.MaximizeIcon = PackIconKind.WindowMaximize;
				var mousePositionRelativetoWindow = Mouse.GetPosition(this);
				var mousePositionRelativeToScreen = GetMousePositionRelativeToScreen();
				this.Top = mousePositionRelativetoWindow.X - mousePositionRelativeToScreen.X;
				this.Left = mousePositionRelativetoWindow.Y - mousePositionRelativeToScreen.Y;
			}
			this.DragMove();
		}
	}
	/// <summary>
	/// This is called when a key is pressed on the window
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	private void CheckControlPressed(object sender, KeyEventArgs e) => this._MainViewModel.IsControlPressed = e.Key is Key.LeftCtrl or Key.RightCtrl;

	/// <summary>
	/// This is called when a key is released on the window
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	private void ReleaseKey(object sender, KeyEventArgs e) => this._MainViewModel.IsControlPressed = false;
	/// <summary>
	/// Zooms the image
	/// Source: https://stackoverflow.com/questions/14729853/wpf-zooming-in-on-an-image-inside-a-scroll-viewer-and-having-the-scrollbars-a
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	private void ZoomPixels(object sender, MouseWheelEventArgs e) {
		System.Diagnostics.Debug.WriteLine($"{e.Source} - {e.OriginalSource}");
		if (e.Delta > 0 && this._MainViewModel.IsControlPressed && this._MainViewModel.PixelsZoom + GraphModel.ZoomFactor <= GraphModel.MaxZoomFactor) {
			this._MainViewModel.PixelsZoom += GraphModel.ZoomFactor;
		} else if (e.Delta < 0 && this._MainViewModel.IsControlPressed && this._MainViewModel.PixelsZoom - GraphModel.ZoomFactor >= GraphModel.MinZoomFactor) {
			this._MainViewModel.PixelsZoom -= GraphModel.ZoomFactor;
		}
	}
	#endregion
	#region Methods
	/// <summary>
	/// Maximizes the window
	/// </summary>
	private void MaximizeWindow() {
		this.WindowState = this.WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
		this._MainViewModel.MaximizeIcon = this.WindowState == WindowState.Normal ? PackIconKind.WindowMaximize : PackIconKind.WindowRestore;
		this._MainViewModel.BorderMargin = this.WindowState == WindowState.Normal ? MainModel.NormalBorderMargin : MainModel.MaximizedBorderMargin;
	}
	#endregion
	#region Win32: https://stackoverflow.com/questions/4226740/how-do-i-get-the-current-mouse-screen-coordinates-in-wpf
	[DllImport("user32.dll")]
	[return: MarshalAs(UnmanagedType.Bool)]
	internal static extern bool GetCursorPos(ref Win32Point pt);
	[StructLayout(LayoutKind.Sequential)]
	internal struct Win32Point {
		public int X;
		public int Y;
	};
	/// <summary>
	/// It does what it says
	/// </summary>
	/// <returns>It returns what it returns</returns>
	public static Point GetMousePositionRelativeToScreen() {
		var w32Mouse = new Win32Point();
		_ = GetCursorPos(ref w32Mouse);
		return new(w32Mouse.X, w32Mouse.Y);
	}
	#endregion
}