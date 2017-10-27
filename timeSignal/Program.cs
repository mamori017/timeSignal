using System;
using System.Windows.Forms;
using Common;

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
            Process.CheckMultiple(Application.ProductName);
        }
    }
}
