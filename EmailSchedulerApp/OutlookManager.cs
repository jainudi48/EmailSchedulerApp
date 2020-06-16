using Outlook = Microsoft.Office.Interop.Outlook;

namespace EmailSchedulerApp
{
    class OutlookManager
    {
        // Method that sends email on the basis of passed mail item parameters
        public void SendEmail(string recipientEmail, string subject, string body)
        {
            Outlook.Application outlookApp = new Outlook.Application();
            Outlook.MailItem mailItem = (Outlook.MailItem)
                (outlookApp.CreateItem(Outlook.OlItemType.olMailItem));

            mailItem.Subject = subject;
            mailItem.To = recipientEmail;
            mailItem.Body = body;
            mailItem.Importance = Outlook.OlImportance.olImportanceHigh;
            mailItem.Display(false);
            mailItem.Send();
        }
    }
}
