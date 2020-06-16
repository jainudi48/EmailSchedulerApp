using EmailSchedulerApp;
using System;

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
