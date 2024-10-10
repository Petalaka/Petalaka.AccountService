namespace Petalaka.Account.Contract.Repository.Base;

public class PaginationResponse<T>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public int TotalRecords { get; set; }
    public int CurrentPageRecords { get; set; }
    public IList<T> Data { get; set; }
    public PaginationResponse(IList<T> data,int pageNumber, int pageSize, int totalRecords, int currentPageRecords)
    {
        Data = data;
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalRecords = totalRecords;
        TotalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
        CurrentPageRecords = currentPageRecords;
    }
}