using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;

using MaterialDesignThemes.Wpf;

using PGMLab.ViewModels.Main;

namespace PGMLab.Views.Main;
/// <summary>
/// Main window
/// </summary>
public partial class MainView : Window {
	#region Attributes
	#endregion
	#region Constructors
	/// <summary>
	/// Initializes the window
	/// </summary>
	public MainView() {
		this.InitializeComponent();
		this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
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
		if (e.LeftButton == MouseButtonState.Pressed) {
			if (e.ClickCount == 2 && this.WindowState == WindowState.Normal) {
				this.MaximizeWindow();
			}
			if (e.LeftButton == MouseButtonState.Pressed && this.WindowState == WindowState.Maximized && e.ClickCount < 2) {
				this.WindowState = WindowState.Normal;
				var dataContext = (MainViewModel) this.DataContext;
				dataContext.MaximizeIcon = PackIconKind.WindowMaximize;
				var mousePositionRelativetoWindow = Mouse.GetPosition(this);
				var mousePositionRelativeToScreen = GetMousePositionRelativeToScreen();
				this.Top = mousePositionRelativetoWindow.X - mousePositionRelativeToScreen.X;
				this.Left = mousePositionRelativetoWindow.Y - mousePositionRelativeToScreen.Y;
			}
			this.DragMove();
		}
	}
	#endregion
	#region Methods
	/// <summary>
	/// Maximizes the window
	/// </summary>
	private void MaximizeWindow() {
		this.WindowState = this.WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
		var dataContext = (MainViewModel) this.DataContext;
		dataContext.MaximizeIcon = PackIconKind.WindowMaximize;
		dataContext.MaximizeIcon = this.WindowState == WindowState.Normal ? PackIconKind.WindowMaximize : PackIconKind.WindowRestore;
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
		Win32Point w32Mouse = new();
		_ = GetCursorPos(ref w32Mouse);
		return new Point(w32Mouse.X, w32Mouse.Y);
	}
	#endregion
}
