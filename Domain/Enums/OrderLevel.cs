using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum OrderLevel
    {
        Open = 1,
        InProgress = 2,
        Completed = 3,
        OnHold = 4,
        AwaitingPayment = 5,
        Canceld = 6,
    }
}
