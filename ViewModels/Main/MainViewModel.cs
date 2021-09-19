using System;
using System.Windows;
using System.Windows.Input;

using PGMLab.Models.Main;

namespace PGMLab.ViewModels.Main;
/// <summary>
/// View model for the main window
/// </summary>
public class MainViewModel {
	#region Attributes
	#endregion
	#region Command properties
	/// <summary>
	/// Test command
	/// </summary>
	public ICommand TestCommand {
		get; set;
	}
	#endregion
	#region Constructors
	/// <summary>
	/// Creates the view model
	/// </summary>
	public MainViewModel() => this.TestCommand = new MainCommmand(new Action<object>(ExecuteTestCommand));
	#endregion
	#region Command methods
	/// <summary>
	/// Test command
	/// </summary>
	/// <param name="obj">Object to manipulate</param>
	public static void ExecuteTestCommand(object obj) => _ = MessageBox.Show($"Hecho por Vicente \"vichoste\" Calderón");
	#endregion
}