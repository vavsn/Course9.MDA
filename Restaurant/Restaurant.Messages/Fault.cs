using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Messages
{
    internal interface Fault<T>
        where T : class
    {
        Guid FaultId { get; }
        Guid? FaultMessageId { get; }
        DateTime Timestamp { get; }
        ExceptionInfo[] Exceptions { get; }
        HostInfo Host { get; }
        T Message { get; }
    }
}
