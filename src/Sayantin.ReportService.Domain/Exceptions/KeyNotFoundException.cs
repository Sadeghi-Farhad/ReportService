namespace ReportService.Domain.Exceptions
{
    public class KeyNotFoundException : BaseException
    {
        public KeyNotFoundException()
            : base()
        {
        }

        public KeyNotFoundException(string message)
            : base(message)
        {
        }
    }
}