namespace Petalaka.Account.Core.Utils;

public class StringConverterHelper
{
    public static string NullOrWhiteSpaceStringHandle(string inputString)
    {
        if (string.IsNullOrWhiteSpace(inputString))
        {
            throw new ArgumentException("String is null or white space");
        }

        return inputString;
    }
    public static string CapitalizeString(string inputString)
    {
        NullOrWhiteSpaceStringHandle(inputString);
        return inputString.Trim().ToUpper();
    }

    public static string NormalizeString(string inputString)
    {
        NullOrWhiteSpaceStringHandle(inputString);
        return inputString.Trim().ToLower();
    }
}