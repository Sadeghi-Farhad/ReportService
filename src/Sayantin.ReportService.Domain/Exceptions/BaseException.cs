namespace ReportService.Domain.Exceptions
{
    public class BaseException : Exception
    {
        public BaseException()
        : base()
        {
        }

        public BaseException(string message)
            : base(message)
        {
        }
    }
}