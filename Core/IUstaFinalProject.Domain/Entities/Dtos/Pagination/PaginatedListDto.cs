namespace IUstaFinalProject.Domain.Entities.Dtos.Pagination
{
    public class PaginatedListDto<TModel>
    {
        public PaginatedListDto(IEnumerable<TModel> items, PaginationMeta meta)
        {
            Items = items;
            Meta = meta;
        }

        public IEnumerable<TModel> Items { get; set; }
        public PaginationMeta Meta { get; set; }
    }
}
