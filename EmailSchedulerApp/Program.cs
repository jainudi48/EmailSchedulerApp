using EmailSchedulerApp;
using System;
using System.Collections.Generic;
using QC = Microsoft.Data.SqlClient;

namespace EmailScheduler
{
    class Program
    {
        static void Main(string[] args)
        {
            BackgroundRecurringJobs.SendEmailDaily();

            Console.ReadKey();
        }
    }
}
