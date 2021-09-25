using AntColony.Colony;

using MaterialDesignThemes.Wpf;

namespace AntColony.Main;
internal struct MainModel {
	public const int MaximizedBorderMargin = 16;
	public const int NormalBorderMargin = 8;
	public ColonyViewModel? ColonyViewModel { get; set; }
	public int BorderMargin { get; set; }
	public bool CanOperate { get; set; }
	public bool IsControlPressed { get; set; }
	public PackIconKind? MaximizeIcon { get; set; }
	public double PixelsZoom { get; set; }
	public Status Status { get; set; }
}