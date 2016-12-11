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
        public static int intWaitMin = 59;
        public Task objTask;

        /// <summary>
        /// Form1
        /// </summary>
        public Form1()
        {
            try
            {
                InitializeComponent();

                objTask = asyncTimeSignal();
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
        static async Task asyncTimeSignal()
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
                            showNotify();
                            Thread.Sleep(intWaitMin * 60 * 1000);
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
        private static void showNotify()
        {
            try
            {
                var tmpl = ToastTemplateType.ToastImageAndText02;
                var xml = ToastNotificationManager.GetTemplateContent(tmpl);
                var images = xml.GetElementsByTagName("image");
                var src = images[0].Attributes.GetNamedItem("src");
                var texts = xml.GetElementsByTagName("text");
                var toast = new ToastNotification(xml);

                src.InnerText = "file:///" + System.IO.Path.GetFullPath(@"img\icon.png");
                texts[0].AppendChild(xml.CreateTextNode(DateTime.Now.ToShortTimeString()));
                texts[1].AppendChild(xml.CreateTextNode(DateTime.Now.DayOfWeek.ToString()));
                ToastNotificationManager.CreateToastNotifier("Time Signal").Show(toast);
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
    }
}
