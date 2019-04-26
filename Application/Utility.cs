using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class Utility
    {
        public static string GetRootCauseOfException(Exception ex)
        {
            string message;
            if (ex.Message.Contains("See the inner exception for details."))
            {
                if (ex.InnerException.Message.Contains("See the inner exception for details."))
                {
                    if (ex.InnerException.InnerException.Message.Contains("See the inner exception for details."))
                        message = ex.InnerException.InnerException.InnerException.Message;
                    else
                        message = ex.InnerException.InnerException.Message;
                }
                else
                    message = ex.InnerException.Message;
            }
            else
                message = ex.Message;
            return message;
        }
    }
}
