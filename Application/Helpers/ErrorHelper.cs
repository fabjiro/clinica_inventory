using Ardalis.Result;
using Microsoft.AspNetCore.Identity;

namespace Application.Helpers
{
    public static class ErrorHelper
    {
        public static string GetValidationErrors(List<ValidationError> errors)
        {
            return string.Join(",", errors.Select(x => x.ErrorMessage));
        }

        public static string GetErrors(List<string> errors)
        {
            return string.Join(",", errors);
        }

        public static string GetExceptionError(Exception ex)
        {
            return ex.InnerException is not null ? ex.Message + " | " + ex.InnerException.Message : ex.Message;
        }

        public static string GetIdentityError(List<IdentityError> identityErros)
        {
            return string.Join(",", identityErros.Select(x => x.Description));
        }

        public static string ImporterError<T>(Exception ex)
        {
            return $"Error in the process of importing seed values for {typeof(T).Name} "
                + $"for the following reason {GetExceptionError(ex)}";
        }

        public static string ImporterError<T>(Exception ex, string stackTrace)
        {
            return $"Error in the process of importing seed values for {typeof(T).Name} "
                + $"for the following reason {GetExceptionError(ex)}"
                + $"ErrorStackTrace: {stackTrace}";
        }
    }
}
