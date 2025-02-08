﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Domain.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime RoundToNearest10Minutes(this DateTime dt)
        {
            return new DateTime(
                dt.Year,
                dt.Month,
                dt.Day,
                dt.Hour,
                (dt.Minute / 10) * 10,
                0
            );
        }
    }
}
