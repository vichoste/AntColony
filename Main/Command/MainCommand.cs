using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AntColony.Main.Command {
	/// <summary>
	/// Asynchoronous command
	/// https://docs.microsoft.com/en-us/archive/msdn-magazine/2014/april/async-programming-patterns-for-asynchronous-mvvm-applications-commands
	/// </summary>
	internal abstract class MainCommand : IAsyncCommand {
		#region Methods
		/// <summary>
		/// Executes the command
		/// <param name="parameter">Object to manipulate</param>
		/// </summary>
		public abstract Task ExecuteAsync(object parameter);
		/// <summary>
		/// If this command can be executed
		/// </summary>
		/// <param name="parameter">Object to manipulates</param>
		/// <returns>True if it can be executed</returns>
		public abstract bool CanExecute(object parameter);
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
		/// <summary>
		/// Raise can execute changed
		/// </summary>
		protected void RaiseCanExecuteChanged() => CommandManager.InvalidateRequerySuggested();
		#endregion
	}
}
