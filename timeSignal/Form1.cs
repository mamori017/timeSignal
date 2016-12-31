using System;
using System.IO;
using System.Text;
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
        private static string strTimeFormatJp = "tt hh時mm分";
        private static string strDateFormatJp = "yyyy年MM月dd日(dddd)";
        private static string strTimeFormatEn = "hh:mm tt";

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

                objTask = asyncTimeSignal(tokenSource.Token, blnLangFlg);
            }
            catch (Exception ex)
            {
                outputErrLog(ex);
            }
        }

        /// <summary>
        /// asyncTimeSignal
        /// </summary>
        /// <returns></returns>
        private static async Task asyncTimeSignal(CancellationToken token, bool blnLangFlg)
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
                                showNotify(DateTime.Now.ToString(strTimeFormatJp, ci), DateTime.Now.ToString(strDateFormatJp),true);
                            }
                            else
                            {
                                System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-US");
                                showNotify(DateTime.Now.ToString(strTimeFormatEn, ci), DateTime.Now.ToShortDateString() + "(" + DateTime.Now.DayOfWeek.ToString() + ")", true);
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
                outputErrLog(ex);
            }
        }

        /// <summary>
        /// Close
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void endToolStripMenuItem_Click(object sender, EventArgs e)
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
                outputErrLog(ex);
            }
        }

        /// <summary>
        /// showNotify
        /// </summary>
        private static void showNotify(String strLine_1,string strLine_2,Boolean blnExeFlg)
        {
            try
            {
                var tmpl = ToastTemplateType.ToastImageAndText02;
                var xml = ToastNotificationManager.GetTemplateContent(tmpl);
                var images = xml.GetElementsByTagName("image");
                var src = images[0].Attributes.GetNamedItem("src");
                var texts = xml.GetElementsByTagName("text");
                var toast = new ToastNotification(xml);

                if(blnExeFlg == true)
                {
                    src.InnerText = "file:///" + Path.GetFullPath(@"img\icon.png");
                }
                else
                {
                    src.InnerText = "file:///" + Path.GetFullPath(@"img\icon_stop.png");
                }
                
                texts[0].AppendChild(xml.CreateTextNode(strLine_1));
                texts[1].AppendChild(xml.CreateTextNode(strLine_2));

                ToastNotificationManager.CreateToastNotifier("timeSignal").Show(toast);
            }
            catch (Exception ex)
            {
                outputErrLog(ex);
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
        private void languageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Globalization.CultureInfo objInfo = null;

            if (blnLangFlg == true)
            {
                blnLangFlg = false;

                objInfo = new System.Globalization.CultureInfo("en-US");

                showNotify("Change en-US", DateTime.Now.ToString(strTimeFormatEn, objInfo) + "\n" +  DateTime.Now.ToShortDateString() + "(" + DateTime.Now.DayOfWeek.ToString() + ")", true);

                tokenSource.Cancel();

                objTask = asyncTimeSignal(tokenSource.Token, blnLangFlg);
            }
            else
            {
                blnLangFlg = true;

                objInfo = new System.Globalization.CultureInfo("ja-JP");

                showNotify("Change ja-JP", DateTime.Now.ToString(strTimeFormatJp, objInfo) + "\n" + DateTime.Now.ToString(strDateFormatJp, objInfo), true);

                tokenSource.Cancel();

                objTask = asyncTimeSignal(tokenSource.Token, blnLangFlg);
            }

        }

        /// <summary>
        /// outputErrLog
        /// </summary>
        private static void outputErrLog(Exception ex)
        {
            Encoding objEncoding = new UTF8Encoding(false);
            StreamWriter objWriter = new StreamWriter(@".\errorLog.txt", true, objEncoding);

            try
            {
                objWriter.WriteLine(ex);
                objWriter.Close();
            }
            finally
            {
                if (objEncoding != null)
                {
                    objEncoding = null;
                }

                if (objWriter != null)
                {
                    objWriter = null;
                }
            }
        }

        /// <summary>
        /// interrupt/resume
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tokenSource.IsCancellationRequested != true)
            {
                tokenSource.Cancel();
                notifyIcon1.Icon = new System.Drawing.Icon(Path.GetFullPath(@"img\notify_stop.ico"));
            }
            else
            {
                tokenSource = new CancellationTokenSource();
                objTask = asyncTimeSignal(tokenSource.Token, blnLangFlg);
                notifyIcon1.Icon = new System.Drawing.Icon(Path.GetFullPath(@"img\notify.ico"));
            }
        }

        /// <summary>
        /// Test method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        //{
        //    System.Globalization.CultureInfo objInfo = null;

        //    if (blnLangFlg == true)
        //    {
        //        objInfo = new System.Globalization.CultureInfo("ja-JP");
        //        showNotify(DateTime.Now.ToString(strTimeFormatJp, objInfo), DateTime.Now.ToString(strDateFormatJp), true);
        //    }
        //    else
        //    {
        //        objInfo = new System.Globalization.CultureInfo("en-US");
        //        showNotify(DateTime.Now.ToString(strTimeFormatEn, objInfo), DateTime.Now.ToShortDateString() + "(" + DateTime.Now.DayOfWeek.ToString() + ")", true);
        //    }
        //}
    }
}
