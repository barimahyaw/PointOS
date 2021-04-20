namespace PointOS.Common.DTO.Validation
{
    public class ValidationResult
    {
        public bool IsError { get; set; }
        public string Message { get; set; }
        public int ReferenceCode { get; set; }
    }
}
