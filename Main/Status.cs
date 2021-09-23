namespace AntColony.Main;
/// <summary>
/// Current program status
/// </summary>
internal enum Status {
	#region Statuses
	/// <summary>
	/// Program is ready
	/// </summary>
	Ready,
	/// <summary>
	/// Program has a warning
	/// </summary>
	Warning,
	/// <summary>
	/// Program has an error
	/// </summary>
	Error,
	/// <summary>
	/// Program is opening
	/// </summary>
	Opening,
	/// <summary>
	/// Program is saving
	/// </summary>
	Saving,
	/// <summary>
	/// Program is dilating the image
	/// </summary>
	Dilating,
	/// <summary>
	/// Program is eroding the image
	/// </summary>
	Eroding
	#endregion
}