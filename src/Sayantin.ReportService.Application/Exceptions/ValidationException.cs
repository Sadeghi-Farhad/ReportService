using ReportService.Domain.Exceptions;
using FluentValidation.Results;

namespace ReportService.Application.Exceptions
{
    public class ValidationException : BaseException
    {
        public IDictionary<string, string[]> Errors { get; }

        public ValidationException(IDictionary<string, string[]> errors)
            : base("Validation Failed.")
        {
            Errors = errors;
        }

        public ValidationException(IEnumerable<ValidationFailure> failures)
            : base("Validation Failed.")
        {
            Errors = failures
                .GroupBy(e => e.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(e => e.ErrorMessage).ToArray());
        }
    }
}
