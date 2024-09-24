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
    
    public static string GenerateTimeStampOtp(DateTimeOffset dateTime)
    {
        return CoreHelper.SystemTimeNow.AddMinutes(10).ToUnixTimeSeconds().ToString();
    }
    
    public static string GenerateTimeStamp(DateTimeOffset dateTime)
    {
        return CoreHelper.SystemTimeNow.ToUnixTimeSeconds().ToString();

    }
}