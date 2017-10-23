using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Windows.UI.Notifications;

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
                InitializeComponent();

                runToolStripMenuItem.Text = "Start/Pause";
                languageToolStripMenuItem.Text = "en-US/ja-JP";

                objTask = AsyncTimeSignal(tokenSource.Token, blnLangFlg);
            }
            catch (Exception ex)
            {
                Common.Log.ExceptionOutput(ex,Common.Define.ErrLogPath);
            }
        }

        /// <summary>
        /// asyncTimeSignal
        /// </summary>
        /// <returns></returns>
        private static async Task AsyncTimeSignal(CancellationToken token, bool blnLangFlg)
        {
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
                                System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("ja-JP");
                                ShowNotify(DateTime.Now.ToString(Common.Define.TimeFormatJp, ci), DateTime.Now.ToString(Common.Define.DateFormatJp),true);
                            }
                            else
                            {
                                System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-US");
                                ShowNotify(DateTime.Now.ToString(Common.Define.TimeFormatEn, ci), DateTime.Now.ToShortDateString() + "(" + DateTime.Now.DayOfWeek.ToString() + ")", true);
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
                Common.Log.ExceptionOutput(ex, Common.Define.ErrLogPath);
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
                Common.Log.ExceptionOutput(ex, Common.Define.ErrLogPath);
            }
        }

        /// <summary>
        /// showNotify
        /// </summary>
        private static void ShowNotify(String strLine_1,string strLine_2,Boolean blnExeFlg)
        {
            try
            {
                var tmpl = ToastTemplateType.ToastImageAndText02;
                var xml = ToastNotificationManager.GetTemplateContent(tmpl);
//                var images = xml.GetElementsByTagName("image");
//                var src = images[0].Attributes.GetNamedItem("src");
                var texts = xml.GetElementsByTagName("text");
                var toast = new ToastNotification(xml);

                //if(blnExeFlg == true)
                //{
                //    src.InnerText = "file:///" + Path.GetFullPath(Common.Define.NotifyIconPath);
                //}
                //else
                //{
                //    src.InnerText = "file:///" + Path.GetFullPath(Common.Define.NotifyStopIconPath);
                //}
                
                texts[0].AppendChild(xml.CreateTextNode(strLine_1));
                texts[1].AppendChild(xml.CreateTextNode(strLine_2));

                var notify = ToastNotificationManager.CreateToastNotifier("timeSignal");
                notify.Show(new ToastNotification(xml));
            }
            catch (Exception ex)
            {
                Common.Log.ExceptionOutput(ex, Common.Define.ErrLogPath);
            }
            finally
            {
                GC.Collect();
            }
        }

        /// <summary>
        /// Language change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LanguageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Globalization.CultureInfo objInfo = null;

            if (blnLangFlg == true)
            {
                blnLangFlg = false;

                objInfo = new System.Globalization.CultureInfo("en-US");

                ShowNotify("Change en-US", DateTime.Now.ToString(Common.Define.TimeFormatEn, objInfo) + "\n" +  DateTime.Now.ToShortDateString() + "(" + DateTime.Now.DayOfWeek.ToString() + ")", true);

                tokenSource.Cancel();

                objTask = AsyncTimeSignal(tokenSource.Token, blnLangFlg);
            }
            else
            {
                blnLangFlg = true;

                objInfo = new System.Globalization.CultureInfo("ja-JP");

                ShowNotify("Change ja-JP", DateTime.Now.ToString(Common.Define.TimeFormatJp, objInfo) + "\n" + DateTime.Now.ToString(Common.Define.DateFormatJp, objInfo), true);

                tokenSource.Cancel();

                objTask = AsyncTimeSignal(tokenSource.Token, blnLangFlg);
            }

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
                notifyIcon1.Icon = new System.Drawing.Icon(Path.GetFullPath(Common.Define.NotifyStopIconPath));
            }
            else
            {
                tokenSource = new CancellationTokenSource();
                objTask = AsyncTimeSignal(tokenSource.Token, blnLangFlg);
                notifyIcon1.Icon = new System.Drawing.Icon(Path.GetFullPath(Common.Define.NotifyStopIconPath));
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("ja-JP");
            ShowNotify(DateTime.Now.ToString(Common.Define.TimeFormatJp, ci), DateTime.Now.ToString(Common.Define.DateFormatJp), true);
        }
    }
}
