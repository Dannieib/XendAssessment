using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xend.Domain
{
    public enum StatusEnum
    {
        Pending=1,
        Processing,
        Processed,
        Completed,
        Failed
    }
}
