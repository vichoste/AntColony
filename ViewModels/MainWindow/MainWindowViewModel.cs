using System;
using System.Windows;
using System.Windows.Input;

using PGMLab.Models.MainWindow;

namespace PGMLab.ViewModels.MainWindow;
/// <summary>
/// View model for the main window
/// </summary>
public class MainWindowViewModel {
	#region Attributes
	private ICommand _TestCommand;
	#endregion
	#region Command properties
	/// <summary>
	/// Test command
	/// </summary>
	public ICommand TestCommand {
		get => this._TestCommand;
		set => this._TestCommand = value;
	}
	#endregion
	#region Constructors
	/// <summary>
	/// Creates the view model
	/// </summary>
	public MainWindowViewModel() => this._TestCommand = new RelayCommand(new Action<object>(this.ExecuteTestCommand));
	#endregion
	#region Command methods
	/// <summary>
	/// Test command
	/// </summary>
	/// <param name="obj">Object to manipulate</param>
	public void ExecuteTestCommand(object obj) => _ = MessageBox.Show($"Hecho por Vicente \"vichoste\" Calderón");
	#endregion
}