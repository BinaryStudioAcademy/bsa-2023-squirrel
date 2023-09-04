﻿using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Squirrel.Core.BLL.Interfaces;
public interface IUserIdGetter
{
    int GetCurrentUserId();
}
