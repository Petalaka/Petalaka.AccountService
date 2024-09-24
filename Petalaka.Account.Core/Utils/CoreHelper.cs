using Microsoft.Extensions.Configuration;

namespace Petalaka.Account.Core.Utils;

public class CoreHelper
{
    public static DateTimeOffset SystemTimeNow => DateTimeParsing.ConvertToUtcPlus7(DateTimeOffset.Now);
    public static IConfiguration GetRootAppSettings => ReadConfiguration.ReadAppSettings();
    public static IConfiguration GetDbDesignTimeAppSettings => ReadConfiguration.ReadDbDesignTimeAppSettings();
}