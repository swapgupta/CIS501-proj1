/*Name: Swap Gupta
 * Finished: 02/16/2018
 * File: AllRebates.cs
 * Description: Used to store rebates and generate checks
 */
using System;
using System.Collections.Generic;
using System.Text;

namespace Sales501
{
    class AllRebates
    {
        static private Dictionary<Transaction, Rebate> Rebates = new Dictionary<Transaction, Rebate>(); //holds all rebates

        /// <summary>
        /// test rebates
        /// </summary>
        static public void test()
        {
            Transaction t1 = AllTransactions.Transactions[1234];
            Rebate r1 = new Rebate(t1.Name, "123 Street, Manhanttan, KS", "swapgupta@email.com", new DateTime(2018, 06, 16), (decimal)0.20);
            Rebates.Add(t1, r1);

            Transaction t2 = AllTransactions.Transactions[1235];
            Rebate r2 = new Rebate(t2.Name, "321 Street, Manhanttan, KS", "swapgupta@aol.com", new DateTime(2018, 02, 16), (decimal)0.11);
            Rebates.Add(t2, r2);
        }

        /// <summary>
        /// used to add a rebate
        /// </summary>
        /// <param name="transaction">the transaction to add a rebate to</param>
        /// <param name="rebate">the rebate object</param>
        static public void AddRebate(Transaction transaction, Rebate rebate)
        {
            if (!CheckRebate(transaction.ID))
            {
                Rebates.Add(transaction, rebate);
            }
            else
            {
                Console.WriteLine("That transaction already has a rebate.");
            }
        }

        /// <summary>
        /// Checks if the transaction ID entered is in All Rebates
        /// </summary>
        /// <param name="ID">the transaction ID</param>
        /// <returns>true if ID is in Rebates</returns>
        static public bool CheckRebate(int ID)
        {
            foreach (Transaction t in Rebates.Keys)
            {
                if (t.ID.Equals(ID)) return true;
            }
            return false;
        }

        /// <summary>
        /// Prints the rebate checks
        /// </summary>
        static public void GenerateChecks()
        {
            foreach (var item in Rebates)
            {
                Console.WriteLine("\n");
                DateTime July15 = new DateTime(2018, 07, 15);
                if (DateTime.Compare(item.Value.Date, July15) <= 0)
                {
                    Console.WriteLine(ToString(item.Key, item.Value));
                }
                else
                {
                    Console.WriteLine(item.Key.Name + "'s rebate was mailed in later than July 15th.");
                }

            }
        }

        /// <summary>
        /// Used to make strings for generating checks
        /// </summary>
        /// <param name="t">The transaction that the rebate is used on</param>
        /// <param name="r">The rebate</param>
        /// <returns>a string containing the customer name, amount owed, transaction info and rebate info</returns>
        static public string ToString(Transaction t, Rebate r)
        {
            decimal total = t.Total;
            decimal discount = r.Discount;
            decimal rebatePaid = discount * total;
            string check = "Pay to the Order of " + r.Name + " the amount of " + rebatePaid.ToString("C2") + "\n" +
                "Transaction ID: " + t.ID +
                "\nTotal: " + total.ToString("C2") + " Discount: " + String.Format("{0:P0}\n", discount) +
                "Address: " + r.Address +
                "\nEmail: " + r.Email + "\n";
            return check;
        }
    }
}
