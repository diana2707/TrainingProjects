
namespace AirportTool.Application.Paging
{
    public static class PagingHelper
    {
        public static PagingParameters Resolve(
            PagingRequest request,
            PagingOptions options)
        {
            var pageNumber = request.PageNumber ?? options.DefaultPage;
            var pageSize = request.PageSize ?? options.DefaultPageSize;

            pageNumber = Math.Max(pageNumber, 1);
            pageSize = Math.Min(Math.Max(pageSize, 1), options.MaxPageSize);

            return new PagingParameters(pageNumber, pageSize);
        }

        //public static IEnumerable<T> Apply<T>(
        //    IEnumerable<T> source,
        //    PagingParameters paging)
        //{
        //    return source
        //        .Skip((paging.PageNumber - 1) * paging.PageSize)
        //        .Take(paging.PageSize);
        //}
    }

}
