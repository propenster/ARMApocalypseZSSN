namespace ARMApocalypseSASAPI.Dtos
{
    public class GenericResponse<T>
    {
        public T Data { get; set; }
        public string Message { get; set; } = string.Empty;
        public bool Status { get; set; }
    }
}
