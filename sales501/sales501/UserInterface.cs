/*Name: Swap Gupta
 * Finished: 02/16/2018
 * File: Program.cs
 * Description: The main sales501 program
 */

using System;
using System.Collections.Generic;

namespace Sales501
{
    class UserInterface
    {
        /// <summary>
        /// Main Program starts here
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            /* TEST CASES */
            //AllTransactions.test();
            //AllRebates.test();

            while (true) //loop until close
            {
                Console.Write(
                    "Would you like to create a " +
                    "sales transaction (T), " +
                    "return item(s) (I), " +
                    "enter rebate (R), " +
                    "generate rebate check (C), " +
                    "print receipt (P), " +
                    "or close application (X)?" +
                    "\nEnter T, I, R, C, P, or X: ");
                string response = Console.ReadLine().ToUpper();

                bool valid = false;
                while (!valid) //loop until valid response
                {
                    if (response == "T") //sales transaction
                    {
                        Console.WriteLine();
                        CreateTransaction();
                        valid = true;
                    }
                    else if (response == "I") //return item
                    {
                        Console.WriteLine();
                        ReturnItem();
                        valid = true;
                    }
                    else if (response == "R") //create rebate
                    {
                        Console.WriteLine();
                        EnterRebate();
                        valid = true;
                    }
                    else if (response == "C") //generate rebate checks
                    {
                        Console.WriteLine();
                        DateTime date;
                        while (true)
                        {
                            Console.Write("Enter the date(mm/dd/yyyy): ");
                            try
                            {
                                string[] tempDate = Console.ReadLine().Split('/');
                                date = new DateTime(Convert.ToInt32(tempDate[2]), Convert.ToInt32(tempDate[0]), Convert.ToInt32(tempDate[1]));
                                break;
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Invalid Entry");
                            }
                        }

                        DateTime endJuly = new DateTime(2018, 07, 30);

                        if (DateTime.Compare(date, endJuly) >= 0)
                        {
                            AllRebates.GenerateChecks();
                        }
                        else
                        {
                            Console.WriteLine("Rebate refunds checks are not generated till the end of July!");
                        }

                        valid = true;
                        Console.WriteLine();
                    }
                    else if (response == "P")
                    {
                        Console.WriteLine();
                        Console.Write("Enter the transaction ID: ");
                        int ID = Convert.ToInt32(Console.ReadLine());
                        while (!AllTransactions.CheckID(ID, AllTransactions.Transactions))
                        {
                            Console.Write("That ID does not exist, try again: ");
                            ID = Convert.ToInt32(Console.ReadLine());
                        }
                        Console.WriteLine(AllTransactions.TransactionString(ID));
                        valid = true;
                    }
                    else if (response == "X")
                    {
                        Environment.Exit(0);
                    }
                    else
                    {
                        Console.Write("Invalid entry, Enter T, I, R, C, P or X: ");
                        response = Console.ReadLine().ToUpper();
                    }
                }
            }

        }

        /// <summary>
        /// Creates a sales transaction
        /// </summary>
        static public void CreateTransaction()
        {
            string name;
            int ID;
            DateTime date;
            List<string> items = new List<string>();
            List<decimal> costs = new List<decimal>();

            Console.Write("Enter customer's name: ");
            name = Console.ReadLine();

            Console.Write("Enter the first item purchased and the price with a comma(,) seperating them: ");
            string[] item = Console.ReadLine().Split(',');
            try
            {
                items.Add(item[0]);
                costs.Add(Convert.ToDecimal(item[1]));
            }
            catch (Exception ex)
            {
                items.Remove(item[0]);
                Console.WriteLine("Invalid Entry");
            }

            while (true)
            {
                Console.Write("Enter the next item, if there are no more items enter N: ");
                item = Console.ReadLine().Split(',');
                try
                {
                    if (item[0].ToUpper() == "N")
                    {
                        break;
                    }
                    else
                    {
                        items.Add(item[0]);
                        costs.Add(Convert.ToDecimal(item[1]));
                    }
                }
                catch (Exception ex)
                {
                    items.Remove(item[0]);
                    Console.WriteLine("Invalid Entry");
                }
            }

            while (true)
            {
                Console.Write("Enter the transaction ID: ");
                try
                {
                    ID = Convert.ToInt32(Console.ReadLine());
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Invalid Entry");
                }
            }

            while (true)
            {
                Console.Write("Enter the date(mm/dd/yyyy): ");
                try
                {
                    string[] tempDate = Console.ReadLine().Split('/');
                    date = new DateTime(Convert.ToInt32(tempDate[2]), Convert.ToInt32(tempDate[0]), Convert.ToInt32(tempDate[1]));
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Invalid Entry");
                }
            }

            AllTransactions.AddTransaction(name, items, costs, ID, date);
            Console.WriteLine(AllTransactions.TransactionString(ID));
        }

        /// <summary>
        /// Returns an item
        /// </summary>
        static public void ReturnItem()
        {
            AllTransactions.ReturnItems = new List<string>();
            AllTransactions.ReturnCosts = new List<decimal>();

            DateTime date;
            while (true)
            {
                Console.Write("Enter the date(mm/dd/yyyy): ");
                try
                {
                    string[] tempDate = Console.ReadLine().Split('/');
                    date = new DateTime(Convert.ToInt32(tempDate[2]), Convert.ToInt32(tempDate[0]), Convert.ToInt32(tempDate[1]));
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Invalid Entry");
                }
            }

            int ID;
            while (true)
            {
                Console.Write("Enter the transaction ID: ");
                try
                {
                    ID = Convert.ToInt32(Console.ReadLine());
                    while (!AllTransactions.CheckID(ID, AllTransactions.Transactions))
                    {
                        Console.Write("That ID does not exist, try again: ");
                        ID = Convert.ToInt32(Console.ReadLine());
                    }
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Invalid Entry");
                }
            }

            string name = AllTransactions.Transactions[ID].Name;
            AllTransactions.ReturnItem(ID);

            bool moreReturn = true;
            while (moreReturn)
            {
                Console.Write("Would you like to return another item? (Y or N)");
                string response = Console.ReadLine().ToUpper();

                if (response == "Y") AllTransactions.ReturnItem(ID);
                else if (response == "N") moreReturn = false;
                else Console.WriteLine("Invalid Entry");
            }
            AllTransactions.ReturnTransactions.Add(ID, new Transaction(name, AllTransactions.ReturnItems, AllTransactions.ReturnCosts, ID, date));
            Console.WriteLine(AllTransactions.ReturnString(ID));
        }

        /// <summary>
        /// Used to enter a rebate
        /// </summary>
        static public void EnterRebate()
        {
            DateTime date;
            while (true)
            {
                Console.Write("Enter the date(mm/dd/yyyy): ");
                try
                {
                    string[] tempDate = Console.ReadLine().Split('/');
                    date = new DateTime(Convert.ToInt32(tempDate[2]), Convert.ToInt32(tempDate[0]), Convert.ToInt32(tempDate[1]));
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Invalid Entry");
                }
            }

            Console.Write("Enter the transaction ID: ");
            int ID = Convert.ToInt32(Console.ReadLine());
            while (!AllTransactions.CheckID(ID, AllTransactions.Transactions))
            {
                Console.Write("That ID does not exist, try again: ");
                ID = Convert.ToInt32(Console.ReadLine());
            }

                string name = AllTransactions.Transactions[ID].Name;
                Transaction rebateTransaction = AllTransactions.Transactions[ID];

                Console.Write("Enter customer's address: ");
                string address = Console.ReadLine();

                Console.Write("Enter customer's email: ");
                string email = Console.ReadLine();

                decimal discount;
                Console.Write("Enter the discount precent: ");
                discount = Convert.ToDecimal(Console.ReadLine());
                discount = discount / 100;
                Rebate rebate = new Rebate(name, address, email, date, discount);
                AllRebates.AddRebate(rebateTransaction, rebate);
            Console.WriteLine();
        }
    }
}
