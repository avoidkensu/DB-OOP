using System;
using MySql.Data.MySqlClient;

namespace GroupActivity
{
    class ManageProduct
    {
        public class InsertNewProduct
        {
            private string server = "localhost";
            private string database = "actdb";
            private string username = "root";
            private string password = "";
            private string connString;

            public InsertNewProduct()
            {
                connString = $"Server={server};Database={database};User ID={username};Password={password};";
            }

            public void InsertData()
            {
                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    try
                    {
                        conn.Open();
                        Console.WriteLine("Connected to MySQL!");

                        while (true) // Infinite loop hanggang iend ng user
                        {
                            // Chinecheck bilang ng row bago mag insert
                            string countQuery = "SELECT COUNT(*) FROM products";
                            using (MySqlCommand countCmd = new MySqlCommand(countQuery, conn))
                            {
                                int rowCount = Convert.ToInt32(countCmd.ExecuteScalar());

                                if (rowCount >= 3)
                                {
                                    Console.WriteLine(@"

 _____  _           ___                            ______                _ 
/  ___|(_)         |_  |                           | ___ \              (_)
\ `--.  _  _ __      | |  ___   _ __    __ _  ___  | |_/ /  ___    __ _  _ 
 `--. \| || '__|     | | / _ \ | '_ \  / _` |/ __| |  __/  / _ \  / _` || |
/\__/ /| || |    /\__/ /| (_) || | | || (_| |\__ \ | |    | (_) || (_| || |
\____/ |_||_|    \____/  \___/ |_| |_| \__,_||___/ \_|     \___/  \__, ||_|
                                                                   __/ |   
                                                                  |___/                                                         
                                                                                            
Database limit reached! Maximum of 3 products allowed.
");
                                    Console.WriteLine("Type 'exit' to quit.");
                                    string exitCommand = Console.ReadLine();
                                    if (exitCommand?.ToLower() == "exit") break; // Exit the loop kapag nag type ng "exit"
                                    else continue; // 
                                }
                            }

                            Console.Write("Enter Product Name (or type 'exit' to quit): ");
                            string productName = Console.ReadLine();
                            if (productName?.ToLower() == "exit") break; //Exit the loop kapag nag type ng "exit"

                            Console.Write("Enter Product Price: ");
                            int productPrice;
                            while (!int.TryParse(Console.ReadLine(), out productPrice))
                            {
                                Console.Write("Invalid input! Enter Product Price (numbers only): ");
                            }

                            Console.Write("Enter Product Description: ");
                            string productDescription = Console.ReadLine();

                            // Insert the new product
                            string query = "INSERT INTO products (productName, productPrice, productDescription) VALUES (@name, @price, @description)";
                            using (MySqlCommand cmd = new MySqlCommand(query, conn))
                            {
                                cmd.Parameters.AddWithValue("@name", productName.Trim());
                                cmd.Parameters.AddWithValue("@price", productPrice);
                                cmd.Parameters.AddWithValue("@description", productDescription.Trim());

                                int rowsAffected = cmd.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    Console.WriteLine(@"
  ____        _          _                     _           _                                    __       _ _       _ 
 |  _ \  __ _| |_ __ _  (_)_ __  ___  ___ _ __| |_ ___  __| |  ___ _   _  ___ ___ ___  ___ ___ / _|_   _| | |_   _| |
 | | | |/ _` | __/ _` | | | '_ \/ __|/ _ \ '__| __/ _ \/ _` | / __| | | |/ __/ __/ _ \/ __/ __| |_| | | | | | | | | |
 | |_| | (_| | || (_| | | | | | \__ \  __/ |  | ||  __/ (_| | \__ \ |_| | (_| (_|  __/\__ \__ \  _| |_| | | | |_| |_|
 |____/ \__,_|\__\__,_| |_|_| |_|___/\___|_|   \__\___|\__,_| |___/\__,_|\___\___\___||___/___/_|  \__,_|_|_|\__, (_)
                                                                                                             |___/  

 _______                                     ___         __ __        
|     __|.--.--.----.----.-----.-----.-----.'  _|.--.--.|  |  |.--.--.
|__     ||  |  |  __|  __|  -__|__ --|__ --|   _||  |  ||  |  ||  |  |
|_______||_____|____|____|_____|_____|_____|__|  |_____||__|__||___  |
                                                               |_____|                                                   
");
                                }
                                else
                                {
                                    Console.WriteLine(@"
  _____     _ _          _   _          _                     _         _       _          
 |  ___|_ _(_) | ___  __| | | |_ ___   (_)_ __  ___  ___ _ __| |_    __| | __ _| |_ __ _   
 | |_ / _` | | |/ _ \/ _` | | __/ _ \  | | '_ \/ __|/ _ \ '__| __|  / _` |/ _` | __/ _` |  
 |  _| (_| | | |  __/ (_| | | || (_) | | | | | \__ \  __/ |  | |_  | (_| | (_| | || (_| |_ 
 |_|  \__,_|_|_|\___|\__,_|  \__\___/  |_|_| |_|___/\___|_|   \__|  \__,_|\__,_|\__\__,_(_)
                                                                                           
");
                                }
                            }
                        } // End of loop
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
            }
        }
    }
}
