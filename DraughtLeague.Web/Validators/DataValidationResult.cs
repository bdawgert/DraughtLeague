using System.Collections.Generic;

namespace DraughtLeague.Web.Validators
{
    public class DataValidationResult
    {
        public bool IsValid { get; set; }
        public List<string> ErrorMessages { get; set; } = new List<string>();
        public List<string> WarningMessages { get; set; } = new List<string>();

    }
}
