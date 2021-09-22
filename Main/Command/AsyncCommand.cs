using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AntColony.Main.Command {
	/// <summary>
	/// Opens a TSP command
	/// </summary>
	internal class AsyncCommand : MainCommand {
		#region Atrributes
		private readonly Func<Task> _Command;
		#endregion
		#region Constructors
		/// <summary>
		/// Create relay command
		/// </summary>
		/// <param name="action">Action to perform</param>
		public AsyncCommand(Func<Task> command) => this._Command = command;
		#endregion
		#region Methods
		/// <summary>
		/// Executes the command
		/// <param name="parameter">Object to manipulate</param>
		/// </summary>
		public override Task ExecuteAsync(object parameter) => this._Command();
		/// <summary>
		/// If this command can be executed
		/// </summary>
		/// <param name="parameter">Object to manipulates</param>
		/// <returns>True if it can be executed</returns>
		public override bool CanExecute(object parameter) => true;
		#endregion
	}
}