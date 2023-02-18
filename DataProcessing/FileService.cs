
using DataProcessing.Logger;

namespace DataProcessing;

internal class FileService
{
    public static string GetNLineFromFile(string filePath, int lineNumber) 
        => File.Exists(filePath) ? File.ReadLines(filePath).Skip(lineNumber - 1).First() : throw new Exception("File not found");

    public static FileInfo[] GetDirectoryFilesListFiltered(string directoryPath)
    {
        if (!Directory.Exists(directoryPath))
        {
            Log.LogError($"Input folder does not exist here {directoryPath}");
            return Array.Empty<FileInfo>();
        }
        DirectoryInfo dir = new(directoryPath);

        return dir.GetFiles().Where(f => f.Extension.ToLower() is ".txt" or ".csv").ToArray();
    }
}
