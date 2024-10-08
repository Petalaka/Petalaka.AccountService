using Microsoft.AspNetCore.Http;
using Petalaka.Account.Core.ExceptionCustom;

namespace Petalaka.Account.Contract.Repository.QueryOptions.ExtensionOptions;

public class TimeRange
{
    private DateTime? _from;
    private DateTime? _to;

    public DateTime? From
    {
        get => _from;
        set
        {
            if (value > _to)
            {
                throw new CoreException(StatusCodes.Status400BadRequest, "'From' date cannot be greater than 'To' date.");
            }
            _from = value;
        }
    }

    public DateTime? To
    {
        get => _to;
        set
        {
            if (value < _from)
            {
                throw new CoreException(StatusCodes.Status400BadRequest, "'To' date cannot be less than 'From' date.");
            }
            _to = value;
        }
    }

}