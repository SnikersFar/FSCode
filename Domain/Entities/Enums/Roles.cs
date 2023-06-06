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
        None = 1,
        User = 2,
        Admin = 4,
    }
}
