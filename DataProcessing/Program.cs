using DataProcessing;

foreach(var f in FileService.GetDirectoryFilesListFiltered(ConfigHelper.FolderAPath))
{
    Console.WriteLine(f.Name);
}

