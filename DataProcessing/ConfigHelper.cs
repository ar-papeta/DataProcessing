using Microsoft.Extensions.Configuration;

namespace DataProcessing;

public static class ConfigHelper
{
    private static readonly IConfiguration _configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).Build();

    public static string FolderAPath => _configuration.GetSection("FolderAPath").Value!;
    public static string FolderBPath => _configuration.GetSection("FolderBPath").Value!;
    public static string LogType => _configuration.GetSection("LogType").Value!;

}
