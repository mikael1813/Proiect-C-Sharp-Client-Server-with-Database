using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SQLite;

namespace Proiect_C_Sharp_Client_Server.utils
{
	public class SqliteConnectionFactory : ConnectionFactory
	{
		public override IDbConnection createConnection()
		{
			//String connectionString = ConfigurationManager.ConnectionStrings["inotDB"].ConnectionString;
			String connectionString = "Data Source=C:\\mpp\\databasesJava\\Inot.db; Version = 3;";
			return new SQLiteConnection(connectionString);


		}
	}
}
