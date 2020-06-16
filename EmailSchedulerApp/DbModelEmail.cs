namespace EmailSchedulerApp
{
    class DbModelEmail
    {
        private string email;
        private bool isOpened;
        private bool isFirstEmailSent;
        private int remainingReminderDays;

        public DbModelEmail(string email, bool isOpened, bool isFirstEmailSent, int remainingReminderDays)
        {
            this.Email = email;
            this.IsOpened = isOpened;
            this.IsFirstEmailSent = isFirstEmailSent;
            this.remainingReminderDays = remainingReminderDays;
        }

        public string Email { get => email; set => email = value; }
        public bool IsOpened { get => isOpened; set => isOpened = value; }
        public bool IsFirstEmailSent { get => isFirstEmailSent; set => isFirstEmailSent = value; }
        public int RemainingReminderDays { get => remainingReminderDays; set => remainingReminderDays = value; }
    }
}
