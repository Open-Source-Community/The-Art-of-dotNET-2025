namespace Task_Manager.ApiService.DTOs
{
    public class PaginatedResponseDto<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int TotalItems { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
