using System;
using System.Collections.Generic;
using System.Text;

namespace timetable_management_system.Config
{
    class DbConfig
    {
        public static string getConnectionString()
        {
            string connectionString = "Server=database-1.cmxg05ztpr3u.us-east-1.rds.amazonaws.com,1433;Database=timetableDb;User Id=admin;Password=test1234;";

            return connectionString;
        }
    }
}
