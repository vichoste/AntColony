using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

using MaterialDesignThemes.Wpf;

using PGMLab.Models.Main;

namespace PGMLab.ViewModels.Main;
/// <summary>
/// View model for the main window
/// </summary>
public class MainViewModel : INotifyPropertyChanged {
	#region Attributes
	private PackIconKind? _MaximizeIcon;
	private State _Status;
	#endregion
	#region Properties
	/// <summary>
	/// Sets the current maximize icon
	/// </summary>
	public PackIconKind? MaximizeIcon {
		get => this._MaximizeIcon;
		set {
			if (this._MaximizeIcon != value) {
				this._MaximizeIcon = value;
				this.OnPropertyChanged("MaximizeIcon");
			}
		}
	}
	/// <summary>
	/// Current program status
	/// </summary>
	public State Status {
		get => this._Status;
		set {
			if (this._Status != value) {
				this._Status = value;
				this.OnPropertyChanged("Status");
			}
		}
	}
	#endregion
	#region Constructors
	/// <summary>
	/// Creates the view model
	/// </summary>
	public MainViewModel() {
		this.TestCommand = new MainCommmand(new Action<object>(ExecuteTestCommand));
		this.MaximizeIcon = PackIconKind.WindowMaximize;
		this.Status = State.Ready;
	}
	#endregion
	#region Events
	/// <summary>
	/// Event
	/// </summary>
	public event PropertyChangedEventHandler? PropertyChanged;
	/// <summary>
	/// When property changes, call this function
	/// </summary>
	/// <param name="value">Property name</param>
	public void OnPropertyChanged(string value) => this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(value));
	#endregion
	#region Command properties
	/// <summary>
	/// Test command
	/// </summary>
	public ICommand TestCommand { get; set; }
	#endregion
	#region Command methods
	/// <summary>
	/// Test command
	/// </summary>
	/// <param name="obj">Object to manipulate</param>
	public static void ExecuteTestCommand(object obj) => _ = MessageBox.Show($"Made by Vicente \"vichoste\" Calderón");
	#endregion
}