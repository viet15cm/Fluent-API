using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demo.Describer
{
    public class AppResult
    {
        public bool Succeeded { get; protected set; }

        private static readonly AppResult _success = new AppResult { Succeeded = true };
        private List<AppError> _errors = new List<AppError>();
        public IEnumerable<AppError> Errors => _errors;

        public static AppResult Success => _success;
        public static AppResult Failed(params AppError[] errors)
        {

            var result = new AppResult { Succeeded = false };
            if (errors != null)
            {
                result._errors.AddRange(errors);
            }
            return result;
        }
        public override string ToString()
        {
            return Succeeded ?
                   "Succeeded" :
                   string.Format("{0} : {1}", "Failed", string.Join(",", Errors.Select(x => x.Code).ToList()));
        }
    }
}
