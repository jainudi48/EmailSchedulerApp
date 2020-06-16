using System.Collections.Generic;
using MSDSC = Microsoft.Data.SqlClient;
using SD = System.Data;

namespace EmailSchedulerApp
{
	class DBOperations
    {

		// Method to check if the DB is connectable/pingable
		public bool IsConnectable()
		{
			using (var connection = new MSDSC.SqlConnection("Server=(localdb)\\MSSQLLocalDB;Database=DBEmailScheduler;Trusted_Connection=True;"))
			{
				connection.Open();

				if (connection.State.Equals(SD.ConnectionState.Open))
				{
					return true;
				}
			}
			return false;
		}

		// Method to retrieve all the models from the DB
		public List<DbModelEmail> GetAllEmailDataFromDb()
		{
			List<DbModelEmail> list = new List<DbModelEmail>();

			using (var connection = new MSDSC.SqlConnection("Server=(localdb)\\MSSQLLocalDB;Database=DBEmailScheduler;Trusted_Connection=True;"))
			{
				connection.Open();

				if (connection.State.Equals(SD.ConnectionState.Open))
				{
					using (var command = new MSDSC.SqlCommand())
					{
						command.Connection = connection;
						command.CommandType = SD.CommandType.Text;
						command.CommandText = @"
							SELECT * FROM dbo.tb_emails;
							";

						MSDSC.SqlDataReader reader = command.ExecuteReader();

						while (reader.Read())
						{
							bool IsOpened = (reader.GetInt32(2) != 0) ? true : false;
							bool IsFirstEmailSent = (reader.GetInt32(3) != 0) ? true : false;
							DbModelEmail dbModelEmail = new DbModelEmail(reader.GetString(1), IsOpened, IsFirstEmailSent, reader.GetInt32(4));
							list.Add(dbModelEmail);
						}
					}
				}
			}
			return list;
		}

		// Method to update the DB with new model parameteres on the basis of email
		public void UpdateDb(DbModelEmail model)
		{
			using (var connection = new MSDSC.SqlConnection("Server=(localdb)\\MSSQLLocalDB;Database=DBEmailScheduler;Trusted_Connection=True;"))
			{
				connection.Open();

				if (connection.State.Equals(SD.ConnectionState.Open))
				{
					using (var command = new MSDSC.SqlCommand())
					{
						MSDSC.SqlParameter param;

						command.Connection = connection;
						command.CommandType = SD.CommandType.Text;
						command.CommandText = @"
							UPDATE dbo.tb_emails 
							SET isFirstEmailSent = @isFirstEmailSent, isOpened = @isEmailOpened, remainingReminderDays = @remainingReminderDays
							WHERE emailID = @emailID";

						int IsOpened = (model.IsOpened == true) ? 1 : 0;
						int IsFirstEmailSent = (model.IsFirstEmailSent == true) ? 1 : 0;

						param = new MSDSC.SqlParameter("@isFirstEmailSent", SD.SqlDbType.Int);
						param.Value = IsFirstEmailSent;
						command.Parameters.Add(param);

						param = new MSDSC.SqlParameter("@isEmailOpened", SD.SqlDbType.Int);
						param.Value = IsOpened;
						command.Parameters.Add(param);

						param = new MSDSC.SqlParameter("@remainingReminderDays", SD.SqlDbType.Int);
						param.Value = model.RemainingReminderDays;
						command.Parameters.Add(param);

						param = new MSDSC.SqlParameter("@emailID", SD.SqlDbType.NVarChar);
						param.Value = model.Email;
						command.Parameters.Add(param);

						int affectedRows = command.ExecuteNonQuery();

						System.Console.WriteLine("DB Updated! Number of rows affected : " + affectedRows);
					}
				}
			}
		}
	}
}
