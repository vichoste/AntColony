using System;
using System.Windows.Input;

namespace AntColony.Main {
	/// <summary>
	/// Creates a relay command that executes an action
	/// </summary>
	public class MainCommmand : ICommand {
		#region Atrributes
		private readonly Action<object> _Action;
		#endregion
		#region Constructors
		/// <summary>
		/// Create relay command
		/// </summary>
		/// <param name="action">Action to perform</param>
		public MainCommmand(Action<object> action) => this._Action = action;
		#endregion
		#region Methods
		/// <summary>
		/// Executes the command
		/// <param name="parameter">Object to manipulate</param>
		/// </summary>
		public void Execute(object parameter) {
			if (parameter != null) {
				this._Action(parameter);
			} else {
				this._Action("There is no action");
			}
		}
		/// <summary>
		/// If this command can be executed
		/// </summary>
		/// <param name="parameter">Object to manipulates</param>
		/// <returns>True if it can be executed</returns>
		public bool CanExecute(object parameter) => true;
		#endregion
		#region Events
		/// <summary>
		/// Event
		/// </summary>
		public event EventHandler CanExecuteChanged {
			add {
				CommandManager.RequerySuggested += value;
			}
			remove {
				CommandManager.RequerySuggested -= value;
			}
		}
		#endregion
	}
}