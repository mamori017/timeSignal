
using System;
using System.Threading;
using System.Windows.Forms;

namespace timeSignal
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            new Form1();
            checkMultiple();
        }

        /// <summary>
        /// Multiple start check
        /// </summary>
        static void checkMultiple()
        {
            Mutex objMutex = new System.Threading.Mutex(false, Application.ProductName);

            if (objMutex.WaitOne(0, false))
            {
                Application.Run();
            }

            GC.KeepAlive(objMutex);

            objMutex.Close();
        }

    }
}
