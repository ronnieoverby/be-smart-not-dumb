<Query Kind="Statements">
  <Reference>&lt;RuntimeDirectory&gt;\System.IO.Compression.dll</Reference>
</Query>

var desktop = Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);
var path = Path.Combine(desktop, "123dcfbe240a4dfda2a0980c7788aadc.zip");

using (var file = File.Open(path, FileMode.Create))
using (var zip = new System.IO.Compression.ZipArchive(file, System.IO.Compression.ZipArchiveMode.Create))
{
	const long byteSize = 1024L * 1024 * 1024 * 50 ; // 50 Gigabytes. Great scott!

	var buffer = new byte[1024 * 1024*25];
	long written = 0;
	var progress = new Util.ProgressBar().Dump();
	while (written < byteSize)
	{
		var entry = zip.CreateEntry(written.ToString(), System.IO.Compression.CompressionLevel.Optimal);
		using (var zstream = entry.Open())
		{
			zstream.Write(buffer, 0, buffer.Length);
			written += buffer.Length;
			progress.Fraction = written / (double)byteSize;
		}
	}
}