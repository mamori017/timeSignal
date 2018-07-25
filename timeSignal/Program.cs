using System;
using System.Windows.Forms;
using System.Threading;

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
            Common.Process.CheckMultiple(Application.ProductName);
        }
    }
}
