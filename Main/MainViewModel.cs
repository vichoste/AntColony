using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

using AntColony.Colony;

using AsyncAwaitBestPractices.MVVM;

using MaterialDesignThemes.Wpf;

using Microsoft.Win32;

namespace AntColony.Main;
internal class MainViewModel : INotifyPropertyChanged {
	private MainModel _MainModel;
	public AsyncCommand OpenTspFileCommand { get; set; }
	public event PropertyChangedEventHandler? PropertyChanged;
	public int BorderMargin {
		get => this._MainModel.BorderMargin;
		set {
			if (this._MainModel.BorderMargin != value) {
				this._MainModel.BorderMargin = value;
				this.OnPropertyChanged(nameof(this.BorderMargin));
			}
		}
	}
	public bool CanOperate {
		get => this._MainModel.CanOperate;
		set {
			if (this._MainModel.CanOperate != value) {
				this._MainModel.CanOperate = value;
				this.OnPropertyChanged(nameof(this.CanOperate));
			}
		}
	}
	public ColonyViewModel? ColonyViewModel {
		get => this._MainModel.ColonyViewModel;
		set {
			this._MainModel.ColonyViewModel = value;
			this.OnPropertyChanged(nameof(this.ColonyViewModel));
		}
	}
	public bool IsControlPressed {
		get => this._MainModel.IsControlPressed;
		set {
			if (this._MainModel.IsControlPressed != value) {
				this._MainModel.IsControlPressed = value;
				this.OnPropertyChanged(nameof(this.IsControlPressed));
			}
		}
	}
	public PackIconKind? MaximizeIcon {
		get => this._MainModel.MaximizeIcon;
		set {
			if (this._MainModel.MaximizeIcon != value) {
				this._MainModel.MaximizeIcon = value;
				this.OnPropertyChanged(nameof(this.MaximizeIcon));
			}
		}
	}
	public double PixelsZoom {
		get => this._MainModel.PixelsZoom;
		set {
			if (this._MainModel.PixelsZoom != value) {
				this._MainModel.PixelsZoom = value;
				this.OnPropertyChanged(nameof(this.PixelsZoom));
			}
		}
	}
	public ScrollViewer ScrollViewer {
		get => this._MainModel.ScrollViewer;
		set {
			if (this._MainModel.ScrollViewer != value) {
				this._MainModel.ScrollViewer = value;
				this.OnPropertyChanged(nameof(this.ScrollViewer));
			}
		}
	}
	public Status Status {
		get => this._MainModel.Status;
		set {
			if (this._MainModel.Status != value) {
				this._MainModel.Status = value;
				this.OnPropertyChanged(nameof(this.Status));
			}
		}
	}
	public MainViewModel() {
		this.OpenTspFileCommand = new(this.ExecuteOpenTspFile);
		this._MainModel = new() {
			MaximizeIcon = PackIconKind.WindowMaximize,
			Status = Status.Ready,
			BorderMargin = 8,
			PixelsZoom = 1
		};
		this.ScrollViewer = new();
	}
	public async Task ExecuteOpenTspFile() {
		this.Status = Status.Opening;
		var colonyViewModel = new ColonyViewModel();
		var openFileDialog = new OpenFileDialog();
		if (openFileDialog.ShowDialog() is true) {
			// Allow only TSP files
			if (!openFileDialog.FileName.ToLower().EndsWith(".tsp")) {
				_ = MessageBox.Show("Can't open file. Only *.tsp files are allowed!");
				return;
			}
			// Read all lines
			var lines = await AsyncFileReader.ReadAllLinesAsync(openFileDialog.FileName, Encoding.UTF8);
			if (int.Parse(lines[3].Split(':')[1].Trim()) > 300) {
				_ = MessageBox.Show("Can't open file. For performance reasons, only graphs with < 300 nodes are allowed!");
				return;
			}
			if (!lines[4].EndsWith("EUC_2D")) { // Only allow Euclidian 2D, because I don't have time for other graph types
				_ = MessageBox.Show("Can't open file. Only EUC_2D graphs are allowed!");
				return;
			}
			if (!lines[5].Equals("NODE_COORD_SECTION")) {
				_ = MessageBox.Show("Can't open file. Expected \"NODE_COORD_SECTION : \" : at line 6!");
				return;
			}
			for (var i = 6; i < lines.Length - 1; i++) {
				await Task.Run(() => {
					// Parse integers (https://stackoverflow.com/questions/4961675/select-parsed-int-if-string-was-parseable-to-int)
					var splitted = lines[i].Split(' ').Select(str => {
						var success = int.TryParse(str, out var value);
						return (value, success);
					}).Where(pair => pair.success).Select(pair => pair.value).ToList();
					if (splitted.Count is not 3) { // This implies there are not integers at all
						_ = MessageBox.Show("Can't open file. Non-integers were found in the file!");
						this.CanOperate = false;
						this.Status = Status.Ready;
						return;
					}
					colonyViewModel.AddFood(new FoodNode() {
						Id = splitted[0],
						X = splitted[1],
						Y = splitted[2]
					});
					if (colonyViewModel is not null) {
						colonyViewModel.MinCoordinate = splitted[1] > splitted[2] ? splitted[1] : splitted[2];
						colonyViewModel.MaxCoordinate = splitted[1] < splitted[2] ? splitted[1] : splitted[2];
					}
				});
			}
			colonyViewModel.AntCount = AntNode.DefaultAntCount;
			colonyViewModel.PheromoneEvaporationRate = PheromoneNode.DefaultEvaporationRate;
			this.ColonyViewModel = colonyViewModel;
			this.CanOperate = true;
			this.ScrollViewer.ScrollToBottom();
			this.ScrollViewer.ScrollToLeftEnd();
			this.Status = Status.Ready;
		}
	}
	public void OnPropertyChanged(string value) => this.PropertyChanged?.Invoke(this, new(value));
}