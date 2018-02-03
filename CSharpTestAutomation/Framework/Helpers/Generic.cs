﻿using System;
using System.Reflection;
namespace CSharpAutomationFramework.Framework.Helpers
{
    public static class Generic
    {
        public static String getTimeDifference(long startTime, long endTime)
        {
            //Finding the difference in milliseconds
            long delta = endTime - startTime;
            int days = (int)delta / (24 * 3600 * 1000); //Finding number of days
            delta = (int)delta % (24 * 3600 * 1000); //Finding the remainder
            int hrs = (int)delta / (3600 * 1000); //Finding number of hrs
            delta = (int)delta % (3600 * 1000); //Finding the remainder
            int min = (int)delta / (60 * 1000); //Finding number of minutes
            delta = (int)delta % (60 * 1000); //Finding the remainder
            int sec = (int)delta / 1000; //Finding number of seconds

            //Concatenting to get time difference in the form day:hr:min:sec
            String strTimeDifference = days + ":" + hrs + ":" + min + ":" + sec;
            return strTimeDifference;
        }

        public static String GetBasePath() {
            String solName = Assembly.GetCallingAssembly().GetName().Name;
            String[] delimiters = { solName };
            return System.AppContext.BaseDirectory.Split(delimiters, StringSplitOptions.None)[0] + solName + "/" + solName;
        }
    }
}
