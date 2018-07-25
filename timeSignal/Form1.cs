using System;
using System.IO;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common;

namespace timeSignal
{
    public partial class Form1 : Form, IDisposable
    {
        private bool blnLangFlg = true;
        private static int intWaitMin = 59;
        private Task objTask;
        private CancellationTokenSource tokenSource = new CancellationTokenSource();

        #region "Process"
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
                runToolStripMenuItem.Text = Properties.Settings.Default.RunToolStripMenuItem;
                languageToolStripMenuItem.Text = Properties.Settings.Default.LanguageToolStripMenuItem;

                // Start
                objTask = AsyncTimeSignal(tokenSource.Token, blnLangFlg);
            }
            catch (Exception ex)
            {
                ExceptionProcess(ex);
            }
        }

        public new void Dispose()
        {
            this.tokenSource.Dispose();
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
                                ci = new CultureInfo(Properties.Settings.Default.CultureInfoJp);
                                Notification.ShowNotify(DateTime.Now.ToString(Properties.Settings.Default.TimeFormatJp, ci),
                                                        DateTime.Now.ToString(Properties.Settings.Default.DateFormatJp),
                                                        Properties.Settings.Default.NotificationAppID,
                                                        Properties.Settings.Default.NotifyIconPath);
                            }
                            else
                            {
                                ci = new CultureInfo(Properties.Settings.Default.CultureInfoEn);
                                Notification.ShowNotify(DateTime.Now.ToString(Properties.Settings.Default.TimeFormatEn, ci),
                                                        DateTime.Now.ToShortDateString() + "(" + DateTime.Now.DayOfWeek.ToString() + ")",
                                                        Properties.Settings.Default.NotificationAppID,
                                                        Properties.Settings.Default.NotifyIconPath);
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
        #endregion

        #region "Common Process"
        /// <summary>
        /// Exception process
        /// </summary>
        /// <param name="ex"></param>
        private static void ExceptionProcess(Exception ex)
        {
            Log.ExceptionOutput(ex, Properties.Settings.Default.ErrLogPath, Properties.Settings.Default.ErrLogFileName);
        }
        #endregion

        #region "ToolStripItem Event"
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
                objInfo = new CultureInfo(Properties.Settings.Default.CultureInfoEn);
                Notification.ShowNotify("Change " + Properties.Settings.Default.CultureInfoEn,
                                        DateTime.Now.ToString(Properties.Settings.Default.TimeFormatEn, objInfo) + "\n" +
                                        DateTime.Now.ToShortDateString() + "(" + DateTime.Now.DayOfWeek.ToString() + ")",
                                        Properties.Settings.Default.NotificationAppID,
                                        Properties.Settings.Default.NotifyIconPath);
            }
            else
            {
                blnLangFlg = true;
                objInfo = new CultureInfo(Properties.Settings.Default.CultureInfoJp);
                Notification.ShowNotify("Change " + Properties.Settings.Default.CultureInfoJp,
                                        DateTime.Now.ToString(Properties.Settings.Default.TimeFormatJp, objInfo) + "\n" +
                                        DateTime.Now.ToString(Properties.Settings.Default.DateFormatJp, objInfo),
                                        Properties.Settings.Default.NotificationAppID,
                                        Properties.Settings.Default.NotifyIconPath);
            }

            tokenSource.Cancel();

            objTask = AsyncTimeSignal(tokenSource.Token, blnLangFlg);

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
                notifyIcon1.Icon = new System.Drawing.Icon(Path.GetFullPath(Properties.Settings.Default.NotifyStopIconPath));
            }
            else
            {
                tokenSource = new CancellationTokenSource();
                objTask = AsyncTimeSignal(tokenSource.Token, blnLangFlg);
                notifyIcon1.Icon = new System.Drawing.Icon(Path.GetFullPath(Properties.Settings.Default.NotifyStopIconPath));
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
                objTask = null;
                notifyIcon1.Visible = false;
                Application.Exit();
            }
            catch (Exception ex)
            {
                ExceptionProcess(ex);
            }
        }
        #endregion
    }
}
