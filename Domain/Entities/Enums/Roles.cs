﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Enums
{
    [Flags]
    public enum Roles
    {
        None = 0,
        User = 1,
        Admin = 2,
    }
}
