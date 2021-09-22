using System.Threading.Tasks;

namespace AntColony.Main.Command {
	/// <summary>
	/// Asynchronous command
	/// https://docs.microsoft.com/en-us/archive/msdn-magazine/2014/april/async-programming-patterns-for-asynchronous-mvvm-applications-commands
	/// </summary>
	internal interface IAsyncCommand {
		#region Methods
		/// <summary>
		/// Executes the command
		/// <param name="parameter">Object to manipulate</param>
		/// </summary>
		Task ExecuteAsync(object parameter);
		#endregion
	}
}
