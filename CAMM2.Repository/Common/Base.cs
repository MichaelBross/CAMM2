using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public abstract class Base
    {
        public bool IsObsolete { get; set; }
        public DateTime CreateDate { get; set; }
        public User CreatedBy { get; set; }
        public DateTime UpdateDate { get; set; }
        public User UpdatedBy { get; set; }
    }
}
