using DataProcessing;

string folderPath = ConfigHelper.FolderAPath;
System.IO.Directory.CreateDirectory(folderPath);

string workingDirectory = Environment.CurrentDirectory;
Console.WriteLine(workingDirectory);