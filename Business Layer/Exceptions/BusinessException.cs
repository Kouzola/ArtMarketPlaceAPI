﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Exceptions
{
    public class BusinessException(string message) : Exception(message)
    {
    }
}
