namespace WebAPI_MXH.DTO
{
    public class ApiResult<T>
    {
        public T? data { get; set; }
        public string Errormessge { get; set; }
        public bool IsSussces { get; set; }
        public DateTime Respontime = DateTime.Now;
    }
}
