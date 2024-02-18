using System;
using System.Collections.Generic;
using System.Text;

namespace Ciot
{
    public enum ErrorCode
    {
        Fail = -1,
        Ok,
        NullArg,
        InvalidId,
        InvalidType,
        Overflow,
        NotImplemented,
        NotSupported,
        Busy,
        InvalidState,
        Serialization,
        Deserialization,
        SendData,
        RecvData,
        InvalidSize,
        Closed,
        NotFound,
        ValidationFailed,
        Connection,
        Disconnection,
        Exception,
        TerminatorMissing,
        InvalidArg,
        NoMemory,
        Timeout
    }
}
