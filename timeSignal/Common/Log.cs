using System;

namespace Common
{
    public static class Log
    {
        /// <summary>
        /// Output
        /// </summary>
        public static void Output(String outputDetail, String filePath, String fileName)
        {
            if (IO.DirectoryCheck(filePath, true))
            {
                IO.CreateTextFile(filePath, fileName, outputDetail, true, IO.EncodeType.utf8);
            }
        }

        /// <summary>
        /// ExceptionOutput
        /// </summary>
        public static void ExceptionOutput(Exception ex, String filePath, String fileName)
        {
            String outputDetail = "";

            outputDetail = ex.Message + "\n" + 
                           ex.InnerException + "\n" + 
                           ex.StackTrace + "\n" + 
                           ex.Source + "\n";

            if (IO.DirectoryCheck(filePath, true))
            {
                IO.CreateTextFile(filePath, fileName, outputDetail, true, IO.EncodeType.utf8);
            }
        }
    }
}
