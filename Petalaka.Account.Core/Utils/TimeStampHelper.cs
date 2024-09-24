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
}