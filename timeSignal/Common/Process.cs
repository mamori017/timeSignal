using System;
using System.Threading;
using System.Windows.Forms;

namespace Common
{
    public static class Process
    {
        /// <summary>
        /// Multiple start check
        /// </summary>
        public static void CheckMultiple(string strProductName)
        {
            Mutex objMutex = new Mutex(false, strProductName);

            if (objMutex.WaitOne(0, false))
            {
                Application.Run();
            }

            GC.KeepAlive(objMutex);

            objMutex.Close();
        }
    }
}
