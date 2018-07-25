using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using timeSignalTests.Properties;

namespace Common.Tests
{
    [TestClass()]
    public class NotificationTests
    {
        [TestMethod()]
        public void ShowNotifyTest()
        {
            try
            {
                Notification.ShowNotify("test1",
                                        "test2",
                                        timeSignalTests.Properties.Settings.Default.NotificationAppID,
                                        timeSignalTests.Properties.Settings.Default.NotificationIconPath);

                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                Assert.IsTrue(false);
                throw;
            }

        }
    }
}