using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xend.AppService.Helpers
{
    public class RabbitMQEndPointKeys
    {
        public static string HandleIncoming = "ProcessReceivingTxn";
        public static string HandleOutgoing = "ProcessSendingTxn";
    }
}
