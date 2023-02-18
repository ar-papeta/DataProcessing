
using DataProcessing.Logger;
using DataProcessing.Models;
using System.Globalization;

namespace DataProcessing;

internal class FileService
{
    public static string? GetNLineFromFile(string filePath, int lineNumber) 
        => File.Exists(filePath) ? File.ReadLines(filePath).Skip(lineNumber).FirstOrDefault() : throw new Exception("File not found");
    public static int GetLinesCount(string filePath) => File.ReadLines(filePath).Count();
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

    public static IEnumerable<InputModel> ParseInputFile(string filePath, out int InvalidLinesCount)
    {
        InvalidLinesCount = 0;
        int lineNumber = 1;
        var inputModelsList = new List<InputModel>();
        string? nextLine;
        do
        {
            nextLine = GetNLineFromFile(filePath, lineNumber++);
            if (TryParseModelFromStringLine(nextLine!, out InputModel? model))
            {
                inputModelsList.Add(model!);
            }
            else
            {
                InvalidLinesCount++;
            }
        }
        while (nextLine is not null);

        return Enumerable.Empty<InputModel>();
    }

    public static bool TryParseModelFromStringLine(string line, out InputModel? model)
    {
        model = new InputModel();
        var notes = line?.Split(",");
        var skip = SkipNCommasInLine(line);
        if (notes is null || notes.Length != 7 + skip)
        {
            Log.LogError($"Line from file was incorrect");
            return false;
        }
        try
        {
            model.FirstName = notes[0];
            model.LastName = notes[1];
            model.Address = notes[2 + skip];
            model.Payment = decimal.Parse(notes[3 + skip]);
            //model.Date = DateTime.Parse(notes[4 + skip]);
            if(DateOnly.TryParseExact(notes[4 + skip].Trim(), "yyyy-dd-MM", null, DateTimeStyles.None, out DateOnly date))
            {
                model.Date = date;
            }
            
            model.AccountNumber = long.Parse(notes[5 + skip]);
            model.Service = notes[6 + skip];

        }
        catch(Exception e)
        {
            Log.LogError(e.Message);
            return false;
        }
        return true;
    }

    private static int SkipNCommasInLine(string line)
    {
        var lines = new List<string>();

        var firstIndex = line.IndexOf('"');
        var lastIndex = line.IndexOf('"', firstIndex);

        return line.Substring(firstIndex, lastIndex).Split(',').Length;
    }
}
