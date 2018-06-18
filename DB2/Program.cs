using System;

using System.IO;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("ADO.net programma om 1, 1000, 100000 en 1000000 crud operations te doen op de tabel users\n");
            TextReader tIn = Console.In;
            DatabaseHandler dbhandler = new DatabaseHandler("DESKTOP-OR069KP\\SQLEXPRESS", "DB", "root", "");

            dbhandler.getVersion();
            //dbhandler.createDatabase();

            Console.WriteLine("Create Queries: creating 1 row took: " + dbhandler.createQueries(1, 1) + "ms");
            Console.WriteLine("Create Queries: creating 1000 row took: " + dbhandler.createQueries(1000, 2) + "ms");
            Console.WriteLine("Create Queries: creating 100000 row took: " + dbhandler.createQueries(100000, 3) + "ms");
            Console.WriteLine("Create Queries: creating 1000000 row took: " + dbhandler.createQueries(1000000, 4) + "ms");

            Console.Write("Na deze input moeten er 1101001 rows zijn. Ingevoerde velden: ");
            dbhandler.countRows();
            Console.WriteLine("");
            Console.WriteLine("Select Queries: selecting 1 row took: " + dbhandler.selectQueries(1) + "ms");
            Console.WriteLine("Select Queries: selecting 1000 row took: " + dbhandler.selectQueries(1000) + "ms");
            Console.WriteLine("Select Queries: selecting 100000 row took: " + dbhandler.selectQueries(100000) + "ms");
            Console.WriteLine("Select Queries: selecting 1000000 row took: " + dbhandler.selectQueries(1000000) + "ms");

            //Console.WriteLine("Update Queries: updating 1 row took: " + dbhandler.updateQueries(1, 1) + "ms");
            //Console.WriteLine("Update Queries: updating 1000 row took: " + dbhandler.updateQueries(1000, 2) + "ms");
            //Console.WriteLine("Update Queries: updating 100000 row took: " + dbhandler.updateQueries(100000, 3) + "ms");
            //Console.WriteLine("Update Queries: updating 1000000 row took: " + dbhandler.updateQueries(1000000, 4) + "ms");

            Console.WriteLine("Delete Queries: deleting 1 row took: " + dbhandler.deleteQueries(1, 1) + "ms");
            Console.WriteLine("Delete Queries: deleting 1000 row took: " + dbhandler.deleteQueries(1000, 2) + "ms");
            Console.WriteLine("Delete Queries: deleting 100000 row took: " + dbhandler.deleteQueries(100000, 3) + "ms");
            Console.WriteLine("Delete Queries: deleting 1000000 row took: " + dbhandler.deleteQueries(1000000, 4) + "ms");
            Console.Write("Na deze delete queries moeten er nog 0 rows zijn. Ingevoerde velden: ");
            dbhandler.countRows();
            Console.WriteLine("");

            Console.WriteLine("Done benchmarking");
            while (true)
            {
                String name = tIn.ReadLine();
                if (name == ".")
                {
                    return;
                }
            }

        }
    }
}
