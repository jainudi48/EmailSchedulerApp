CREATE TABLE [dbo].[tb_emails] (
    [Id]                    INT            IDENTITY (1, 1) NOT NULL,
    [emailID]               NVARCHAR (MAX) NOT NULL,
    [isOpened]              INT            DEFAULT ((0)) NULL,
    [isFirstEmailSent]      INT            DEFAULT ((0)) NULL,
    [remainingReminderDays] INT            DEFAULT ((-1)) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

