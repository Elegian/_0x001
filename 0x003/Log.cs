using System;
using System.Diagnostics;

namespace _0x003
{

    public enum LogType
    {
        DEBUG,
        INFO,
        WARNING,
        ERROR,
        CRITICAL,
    }


    public static class Log
    {

        private static TextWriterTraceListener twtl = new TextWriterTraceListener(System.IO.File.CreateText("Logoutput.txt"));

        private static DateTime currentTime()
        {
            return DateTime.Now;
        }
        
        public static void DebugLog(string text, LogType type)
        {
            printMessage(text, type);
        }

        public static void DebugLog(object element, LogType type)
        {
            printMessage(element.ToString(), type);
        }
        

        
        public static void DebugLogIfValueEquals(dynamic yourVal, dynamic otherVal, string message, LogType type)
        {
            if(yourVal.Equals(otherVal))
            {
                printMessage(message, type);
            }
        }
        
        #region Use it with caution
        public static void DebugLogIfValueIsGreaterThan(dynamic yourVal, dynamic otherVal, string message, LogType type)
        {
            try
            {
                if (yourVal > otherVal)
                {
                    printMessage(message, type);
                }
            }
            catch (Exception ex)
            {
                printMessage("Can't perform operation", LogType.INFO);
                printMessage(ex.ToString(), LogType.ERROR);
            }
        }
        

        public static void DebugLogIfValueIsLessThan(dynamic yourVal, dynamic otherVal, string message, LogType type)
        {
            try
            {
                if (yourVal < otherVal)
                {
                    printMessage(message, type);
                }
            }
            catch (Exception ex)
            {
                printMessage("Can't perform operation", LogType.INFO);
                printMessage(ex.ToString(), LogType.ERROR);
            }
        }
        #endregion

        private static void printMessage(string message, LogType type)
        {
            Debug.WriteLine("{0} {1}: {2}", type.ToString(), currentTime().ToString(), message);
        }


        // Gonna implement this in near future
        public static void TraceLog(string text, LogType type)
        {

        }

        public static void TraceLog(object text, LogType type)
        {

        }

        // Maybe for the future: struct Logelement

        // Difference between DebugLog and TraceLog : DebugLog works only in Debugmode
        

    }
}
