﻿using System;

namespace assetsUpdater.Utils
{
    public static class TimeUtil
    {
        public static long GetCurrentUtc1()
        {
            var timeSpan = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0));
            return (long)timeSpan.TotalSeconds;
        }
    }
}
