using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Describer
{
    public class AppErrorDescriber 
    {
        public virtual AppError DuplicateStudent()
        {
            return new AppError()
            {
                Code = nameof(DuplicateStudent),
                Message = "LOI BI TRUNG LAP"
            };
             
        }

        


    }
}
