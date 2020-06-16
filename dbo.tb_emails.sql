USE [DBEmailScheduler]
GO

/****** Object: Table [dbo].[tb_emails] Script Date: 16-06-2020 23:27:16 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tb_emails] (
    [Id]                    INT            IDENTITY (1, 1) NOT NULL,
    [emailID]               NVARCHAR (MAX) NOT NULL,
    [isOpened]              INT            NULL,
    [isFirstEmailSent]      INT            NULL,
    [remainingReminderDays] INT            NULL
);


