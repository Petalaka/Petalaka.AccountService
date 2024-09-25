namespace Petalaka.Account.Core.Utils;

public static class TimeStampHelper
{
    public static string GenerateTimeStamp()
    {
        return CoreHelper.SystemTimeNow.ToString("yyyyMMddHHss");
    }
    public static string GenerateTimeStampOtp()
    {
        return CoreHelper.SystemTimeNow.AddMinutes(10).ToString("yyyyMMddHHss");
    }
    
    public static string GenerateUnixTimeStampOtp()
    {
        return CoreHelper.SystemTimeNow.AddMinutes(10).ToUnixTimeSeconds().ToString();
    }
    
    public static string GenerateUnixTimeStamp()
    {
        return CoreHelper.SystemTimeNow.ToUnixTimeSeconds().ToString();
    }
    
    public static long GenerateCustomUnixTimeStamp(int? hours, int? minutes, int? seconds)
    {
        return CoreHelper.SystemTimeNow
            .AddHours(hours ?? 0)
            .AddMinutes(minutes ?? 0)
            .AddSeconds(seconds ?? 0)
            .ToUnixTimeSeconds();
    }
}