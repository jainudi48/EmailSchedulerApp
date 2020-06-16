# EmailSchedulerApp
Designed to schedule emails and send follow up emails for 3 days consecutively.

# Development framework/environment/tools
This Console App has been developed in Visual Studio 2019 v16.4.5 against .NET framework 4.7.2. I have used Microsoft SQL Server 2019 as a database and have used SQL Server Management Studio for interactive with the database.

# How to setup?
1. You will need to have a SQL Server. Run the SQL script 'dbo.tb_emails.sql' to create the table schema inside a newly created database. 
2. Fill the table with some dummy values for emailID field only. Rest of the fields will automatically take up the default values.
3. Clone the project and run it against the database. 

# Main Dependencies
1. Hangfire.Core
2. Hangfire.SqlServer
3. Microsoft.Data.SqlCLient
4. System.Data
