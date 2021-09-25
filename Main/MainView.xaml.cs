using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;

using AntColony.Algorithms;
using AntColony.Colony;

using MaterialDesignThemes.Wpf;

namespace AntColony.Main;
public partial class MainView : Window { // TODO optimum route
	private readonly MainViewModel _MainViewModel;
	public MainView() {
		this.InitializeComponent();
		this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
		this.MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth;
		this._MainViewModel = (MainViewModel)this.DataContext;
		this._MainViewModel.ScrollViewer = this.ScrollViewer;
	}
	private void CheckControlPressed(object sender, KeyEventArgs e) => this._MainViewModel.IsControlPressed = e.Key is Key.LeftCtrl or Key.RightCtrl;
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
	private void Exit(object sender, RoutedEventArgs e) => this.Close();
	private void Maximize(object sender, RoutedEventArgs e) => this.MaximizeWindow();
	private void MaximizeWindow() {
		this.WindowState = this.WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
		this._MainViewModel.MaximizeIcon = this.WindowState == WindowState.Normal ? PackIconKind.WindowMaximize : PackIconKind.WindowRestore;
		this._MainViewModel.BorderMargin = this.WindowState == WindowState.Normal ? MainModel.NormalBorderMargin : MainModel.MaximizedBorderMargin;
	}
	private void Minimize(object sender, RoutedEventArgs e) => this.WindowState = WindowState.Minimized;
	private void ReleaseKey(object sender, KeyEventArgs e) => this._MainViewModel.IsControlPressed = false;
	private async void Run(object sender, RoutedEventArgs e) {
		if (this._MainViewModel.ColonyViewModel is not null) {
			this._MainViewModel.CanOperate = false;
			var pathfinder = new Pathfinder(this._MainViewModel.ColonyViewModel);
			var bestPath = await pathfinder.Run(); // TODO something with this result
		}
	}
	private void ZoomPixels(object sender, MouseWheelEventArgs e) {
		if (e.Delta > 0 && this._MainViewModel.IsControlPressed && this._MainViewModel.PixelsZoom + MainModel.ZoomFactor <= MainModel.MaxZoomFactor) {
			this.ScrollViewer.ScrollToBottom();
			this.ScrollViewer.ScrollToLeftEnd();
			this._MainViewModel.PixelsZoom += MainModel.ZoomFactor;
		} else if (e.Delta < 0 && this._MainViewModel.IsControlPressed && this._MainViewModel.PixelsZoom - MainModel.ZoomFactor >= MainModel.MinZoomFactor) {
			this.ScrollViewer.ScrollToBottom();
			this.ScrollViewer.ScrollToLeftEnd();
			this._MainViewModel.PixelsZoom -= MainModel.ZoomFactor;
		}
	}
	#region Win32: https://stackoverflow.com/questions/4226740/how-do-i-get-the-current-mouse-screen-coordinates-in-wpf
	[DllImport("user32.dll")]
	[return: MarshalAs(UnmanagedType.Bool)]
	private static extern bool GetCursorPos(ref Win32Point pt);
	[StructLayout(LayoutKind.Sequential)]
	public struct Win32Point {
		public int X;
		public int Y;
	};
	public static Point GetMousePositionRelativeToScreen() {
		var w32Mouse = new Win32Point();
		_ = GetCursorPos(ref w32Mouse);
		return new(w32Mouse.X, w32Mouse.Y);
	}
	#endregion
}