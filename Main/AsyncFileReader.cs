using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AntColony.Main;
// https://stackoverflow.com/questions/13167934/how-to-async-files-readalllines-and-await-for-results
public static class AsyncFileReader {
	private const int _DefaultBufferSize = 4096;
	private const FileOptions _DefaultOptions = FileOptions.Asynchronous | FileOptions.SequentialScan;
	public static Task<string[]> ReadAllLinesAsync(string path) => ReadAllLinesAsync(path, Encoding.UTF8);
	public static async Task<string[]> ReadAllLinesAsync(string path, Encoding encoding) {
		var lines = new List<string>();
		using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, _DefaultBufferSize, _DefaultOptions)) {
			using var reader = new StreamReader(stream, encoding);
			string? line;
			while ((line = await reader.ReadLineAsync()) is not null) {
				lines.Add(line);
			}
		}
		return lines.ToArray();
	}
}