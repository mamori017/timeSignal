using System;
using System.IO;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common;

namespace timeSignal
{
    public partial class Form1 : Form
    {
        private bool blnLangFlg = true;
        private static int intWaitMin = 59;
        private Task objTask;
        private CancellationTokenSource tokenSource = new CancellationTokenSource();
  
        /// <summary>
        /// Form1
        /// </summary>
        public Form1()
        {
            try
            {
                // Initialize
                InitializeComponent();

                // ContextToolStrip setting
                runToolStripMenuItem.Text = Define.RunToolStripMenuItem;
                languageToolStripMenuItem.Text = Define.LanguageToolStripMenuItem;

                // Start
                objTask = AsyncTimeSignal(tokenSource.Token, blnLangFlg);
            }
            catch (Exception ex)
            {
                ExceptionProcess(ex);
            }
        }

        /// <summary>
        /// AsyncTimeSignal
        /// </summary>
        /// <returns></returns>
        private static async Task AsyncTimeSignal(CancellationToken token, bool blnLangFlg)
        {
            CultureInfo ci = null;
            int intInitWaitMin = 0;

            try
            {
                await Task.Run(() => {
                    intInitWaitMin = intWaitMin - DateTime.Now.Minute;
                    Thread.Sleep(intInitWaitMin * 60 * 1000);

                    while (true)
                    {
                        if (DateTime.Now.Minute.ToString() == "0")
                        {
                            if(blnLangFlg == true)
                            {
                                ci = new CultureInfo(Define.CultureInfoJp);
                                Notification.ShowNotify(DateTime.Now.ToString(Define.TimeFormatJp, ci),
                                                        DateTime.Now.ToString(Define.DateFormatJp),
                                                        true);
                            }
                            else
                            {
                                ci = new CultureInfo(Define.CultureInfoEn);
                                Notification.ShowNotify(DateTime.Now.ToString(Define.TimeFormatEn, ci),
                                                        DateTime.Now.ToShortDateString() + "(" + DateTime.Now.DayOfWeek.ToString() + ")",
                                                        true);
                            }

                            Thread.Sleep(intWaitMin * 60 * 1000);
                        }

                        if (token.IsCancellationRequested)
                        {
                            break;
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                ExceptionProcess(ex);
            }
        }

        /// <summary>
        /// Close
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EndToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (objTask != null)
                {
                    objTask = null;
                }

                notifyIcon1.Visible = false;
                Application.Exit();
            }
            catch (Exception ex)
            {
                ExceptionProcess(ex);
            }
        }


        /// <summary>
        /// Language change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LanguageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CultureInfo objInfo = null;

            if (blnLangFlg == true)
            {
                blnLangFlg = false;
                objInfo = new CultureInfo(Define.CultureInfoEn);
                Notification.ShowNotify("Change " + Define.CultureInfoEn, 
                                        DateTime.Now.ToString(Define.TimeFormatEn, objInfo) + "\n" +
                                        DateTime.Now.ToShortDateString() + "(" + DateTime.Now.DayOfWeek.ToString() + ")", 
                                        true);
            }
            else
            {
                blnLangFlg = true;
                objInfo = new CultureInfo(Define.CultureInfoJp);
                Notification.ShowNotify("Change " + Define.CultureInfoJp, 
                                        DateTime.Now.ToString(Define.TimeFormatJp, objInfo) + "\n" +
                                        DateTime.Now.ToString(Define.DateFormatJp, objInfo), 
                                        true);
            }

            tokenSource.Cancel();

            objTask = AsyncTimeSignal(tokenSource.Token, blnLangFlg);

        }

        /// <summary>
        /// Exception process
        /// </summary>
        /// <param name="ex"></param>
        private static void ExceptionProcess(Exception ex)
        {
            Log.ExceptionOutput(ex, Define.ErrLogPath);
        }

        /// <summary>
        /// interrupt/resume
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RunToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tokenSource.IsCancellationRequested != true)
            {
                tokenSource.Cancel();
                notifyIcon1.Icon = new System.Drawing.Icon(Path.GetFullPath(Define.NotifyStopIconPath));
            }
            else
            {
                tokenSource = new CancellationTokenSource();
                objTask = AsyncTimeSignal(tokenSource.Token, blnLangFlg);
                notifyIcon1.Icon = new System.Drawing.Icon(Path.GetFullPath(Define.NotifyStopIconPath));
            }
        }

        /// <summary>
        /// Test Method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            CultureInfo ci = new CultureInfo(Define.CultureInfoJp);
            Notification.ShowNotify(DateTime.Now.ToString(Define.TimeFormatJp, ci), DateTime.Now.ToString(Define.DateFormatJp), true);
        }
    }
}
