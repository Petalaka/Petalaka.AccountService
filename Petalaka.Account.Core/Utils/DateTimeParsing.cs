namespace Petalaka.Account.Core.Utils;

public class DateTimeParsing
{
    public static DateTimeOffset ConvertToUtcPlus7(DateTimeOffset dateTime)
    {
        return dateTime.ToOffset(new TimeSpan(7, 0, 0));
    }
}