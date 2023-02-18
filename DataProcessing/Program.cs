using DataProcessing;

foreach (var f in FileService.GetDirectoryFilesListFiltered(ConfigHelper.FolderAPath))
{
    Console.WriteLine(f.Name);
    var e = FileService.ParseInputFile(@"C:\Users\Artem\source\repos\DataProcessing\DataProcessing\folder_a\1.csv", out int inCount);
    Console.WriteLine(inCount);
    foreach (var d in e)
    {
        Console.WriteLine(d.FirstName);
    }
}




