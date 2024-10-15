using AutoMapper;
using Petalaka.Account.Contract.Repository.Base;

namespace Petalaka.Account.Contract.Repository.ModelMapping;

public class PaginationMapping : Profile
{
    public PaginationMapping()
    {
        CreateMap(typeof(PaginationResponse<>), typeof(BaseResponsePagination<>));
    }
}