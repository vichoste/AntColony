using System.Windows.Controls;

using AntColony.Colony;

using MaterialDesignThemes.Wpf;

namespace AntColony.Main;
internal struct MainModel {
	public const int MaximizedBorderMargin = 16;
	public const double MaxZoomFactor = 10;
	public const double MinZoomFactor = 1;
	public const int NormalBorderMargin = 8;
	public const double ZoomFactor = 1;
	public ColonyViewModel? ColonyViewModel { get; set; }
	public int BorderMargin { get; set; }
	public bool CanOperate { get; set; }
	public bool IsControlPressed { get; set; }
	public PackIconKind? MaximizeIcon { get; set; }
	public double PixelsZoom { get; set; }
	public ScrollViewer ScrollViewer { get; set; }
	public Status Status { get; set; }
}