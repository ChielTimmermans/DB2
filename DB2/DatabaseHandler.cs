using System;
using System.Data.SqlClient;

using System.Diagnostics;

namespace ConsoleApp1
{
    class DatabaseHandler
    {
        private string connectionString;
        public DatabaseHandler(string server, string databaseName, string username, string password)
        {
            Console.WriteLine("Starting DatabaseHandler");
            this.connectionString = @"Data Source=" + server + ";Initial Catalog=" + databaseName + ";User id=" + username + ";Password=" + password + ";";
        }

        public void countRows()
        {
            using (var connection = new SqlConnection(this.connectionString))
            {

                string tableCommand = "SELECT count(*) FROM users";
                try
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(tableCommand, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Console.WriteLine("{0}", reader[0]);
                    }
                    connection.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: " + e.Message);
                }
            }
        }

        public string deleteQueries(int count, int id)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            using (var connection = new SqlConnection(this.connectionString))
            {

                string tableCommand;
                int max = 999;
                int index = 0;
                try
                {
                    connection.Open();

                    for (int idx = 0; idx <= (count / 1000); idx++)
                    {
                        tableCommand = "DELETE FROM users WHERE email in (";

                        if (idx == (count / 1000))
                        {
                            max = (count % 1000) - 1;
                        }
                        if (max != -1)
                        {
                            for (int idx2 = 0; idx2 < max; idx2++)
                            {
                                tableCommand += "'" + id + "email" + index + "@stenden.com',";
                                index++;
                            }

                            tableCommand += "'" + id + "email" + index + "@stenden.com');";
                            index++;
                            SqlCommand command = new SqlCommand(tableCommand, connection);
                            command.ExecuteNonQuery();
                        }
                    }

                    connection.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: " + e.Message);
                }
            }

            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                                ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);

            return elapsedTime;
        }

        public string updateQueries(int count, int id)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            using (var connection = new SqlConnection(this.connectionString))
            {

                String tableCommand = "UPDATE users set password = 'pilsbaas' where email = @email";


                try
                {
                    connection.Open();

                    for (int idx = 0; idx < count; idx++)
                    {
                        SqlCommand command = new SqlCommand(tableCommand, connection);

                        command.Parameters.AddWithValue("@email", id + "email" + idx + "@stenden.com");
                        command.ExecuteNonQuery();
                    }

                    connection.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: " + e.Message);
                }
            }

            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                                ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
            return elapsedTime;
        }

        public string selectQueries(int count)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            using (var connection = new SqlConnection(this.connectionString))
            {

                String queryString = "SELECT TOP " + count + " * FROM users;";

                SqlCommand command = new SqlCommand(queryString, connection);

                try
                {
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    string data = "";
                    while (reader.Read())
                    {
                        data = String.Format("{0} {1} {2}", reader[0], reader[1], reader[2]);
                    }
                    connection.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: " + e.Message);
                }
            }

            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                                ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);

            return elapsedTime;
        }

        public string createQueries(int count, int id)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            using (var connection = new SqlConnection(this.connectionString))
            {

                string tableCommand;
                int max = 999;
                int index = 0;
                try
                {
                    connection.Open();

                    for (int idx = 0; idx <= (count / 1000); idx++)
                    {
                        tableCommand = "insert into users(email, [password]) values";

                        if (idx == (count / 1000))
                        {
                            max = (count % 1000) - 1;
                        }

                        if (max != -1)
                        {
                            for (int idx2 = 0; idx2 < max; idx2++)
                            {
                                tableCommand += "('" + id + "email" + index + "@stenden.com','admin1234'),";
                                index++;
                            }

                            tableCommand += "('" + id + "email" + index + "@stenden.com','admin1234');";
                            index++;

                            SqlCommand command = new SqlCommand(tableCommand, connection);
                            command.ExecuteNonQuery();
                        }
                    }

                    connection.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: " + e.Message);
                }
            }

            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                                ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);

            return elapsedTime;
        }

        public void createDatabase()
        {
            using (var connection = new SqlConnection(this.connectionString))
            {

                String tableCommand = "CREATE TABLE " +
                                    "users (" +
                                        "id INT NOT NULL UNIQUE IDENTITY(1,1)," +
                                        "email VARCHAR(255) NOT NULL UNIQUE," +
                                        "[password] VARCHAR(72) NOT NULL," +
                                        "failedLoginAttempts TINYINT NOT NULL DEFAULT 0," +
                                        "emailIsActivated TINYINT NOT NULL DEFAULT 0," +
                                        "created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP," +
                                        "PRIMARY KEY (id)" +
                                    ");";

                try
                {

                    connection.Open();
                    SqlCommand command = new SqlCommand(tableCommand, connection);
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: " + e.Message);
                }

            }
        }

        public void getVersion()
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                connection.Open();

                Console.WriteLine("ServerVersion: {0}", connection.ServerVersion);
                Console.WriteLine("Database: {0}", connection.Database);

            }
        }
    }
}
