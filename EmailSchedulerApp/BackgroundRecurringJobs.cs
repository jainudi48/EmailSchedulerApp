using Hangfire;
using Hangfire.Common;
using Hangfire.SqlServer;
using System;
using System.Collections.Generic;

namespace EmailSchedulerApp
{
    class BackgroundRecurringJobs
    {
        // Method creating the time-bound scheduler jobs
        public static void SendEmailDaily()
        {
            GlobalConfiguration.Configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseColouredConsoleLogProvider()
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage("Server=(localdb)\\MSSQLLocalDB;Database=DBEmailScheduler;Trusted_Connection=True;", new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    UsePageLocksOnDequeue = true,
                    DisableGlobalLocks = true
                });

            int hourDiff = DateTimeOffset.Now.Offset.Hours;
            int minDiff = DateTimeOffset.Now.Offset.Minutes;

            var manager = new RecurringJobManager();

            manager.AddOrUpdate("EmailSchedulerDaily", Job.FromExpression(() => EmailSchedulerDailyJob()), Cron.Daily(hourDiff, minDiff));

            using (var server = new BackgroundJobServer())
            {
                Console.ReadLine();
            }
        }

        // Method that will be scheduled to be run at mid-night
        public static void EmailSchedulerDailyJob()
        {
            DBOperations db = new DBOperations();
            List<DbModelEmail> list = db.GetAllEmailDataFromDb();
            foreach (var item in list)
            {
                if (!item.IsOpened)
                {
                    if(item.RemainingReminderDays > 0)
                    {
                        string subject = "Reminder Email";
                        string emailBody = "You have not opened the email with subject Test Link";
                        OutlookManager outlookManager = new OutlookManager();
                        outlookManager.SendEmail(item.Email, subject, emailBody);
                        item.RemainingReminderDays -= 1;
                    }
                }
                else
                {
                    string subject = "Thanks";
                    string emailBody = "You have opened the email. Thank you so much!";
                    OutlookManager outlookManager = new OutlookManager();
                    outlookManager.SendEmail(item.Email, subject, emailBody);
                    item.RemainingReminderDays = -1;
                }

                if (!item.IsFirstEmailSent)
                {
                    string subject = "Test Link";
                    string emailBody = "www.microsoft.com";
                    OutlookManager outlookManager = new OutlookManager();
                    outlookManager.SendEmail(item.Email, subject, emailBody);
                    item.IsFirstEmailSent = true;
                    item.RemainingReminderDays = 3;
                }
                
                db.UpdateDb(item);
            }
        }
    }
}
