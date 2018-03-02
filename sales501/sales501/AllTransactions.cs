/*Name: Swap Gupta
 * Finished: 02/16/2018
 * File: AllTransactions.cs
 * Description: Used to store transactions, return items and print receipts
 */
using System;
using System.Collections.Generic;
using System.Text;

namespace Sales501
{
    class AllTransactions
    {
        static public Dictionary<int, Transaction> Transactions = new Dictionary<int, Transaction>(); //holds all transactions
        static public Dictionary<int, Transaction> ReturnTransactions = new Dictionary<int, Transaction>(); //holds return transactions

        static public List<string> ReturnItems; //list of returned items
        static public List<decimal> ReturnCosts; //list of returned costs

        /// <summary>
        /// test transactions
        /// </summary>
        static public void test()
        {
            string name = "Swap Gupta";
            List<string> items1 = new List<string>();
            List<decimal> costs1 = new List<decimal>();
            items1.Add("hammer");
            costs1.Add(10);
            items1.Add("saw");
            costs1.Add(5);
            int id = 1234;
            DateTime date = new DateTime(2018, 02, 16);
            Transaction t1 = new Transaction(name, items1, costs1, id, date);

            string name2 = "Adam Johns";
            List<string> items2 = new List<string>();
            List<decimal> costs2 = new List<decimal>();
            items2.Add("screwdriver");
            costs2.Add(4);
            items2.Add("candy");
            costs2.Add((decimal)1.5);
            int id2 = 1235;
            DateTime date2 = new DateTime(2018, 02, 16);
            Transaction t2 = new Transaction(name2, items2, costs2, id2, date2);

            Transactions.Add(t1.ID, t1);
            Transactions.Add(t2.ID, t2);
        }

        /// <summary>
        /// Used to add a transaction, also checks if ID already exists
        /// </summary>
        /// <param name="name">customer name</param>
        /// <param name="items">items purchased</param>
        /// <param name="costs">item costs</param>
        /// <param name="id">transaction ID</param>
        /// <param name="date">transaction date</param>
        static public void AddTransaction(string name, List<string> items, List<decimal> costs, int id, DateTime date)
        {
            if (!CheckID(id, Transactions))
            {
                Transactions.Add(id, new Transaction(name, items, costs, id, date));
            }
            else
            {
                Console.WriteLine("ID already exists!");
            }
        }

        /// <summary>
        /// Used to check if transaction ID already exists
        /// </summary>
        /// <param name="id">The ID to be checked</param>
        /// <param name="t">The transactions to check in</param>
        /// <returns>true if ID already exists</returns>
        static public bool CheckID(int id, Dictionary<int, Transaction> t)
        {
            foreach (int s in t.Keys)
            {
                if (s == id)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Checks if a sales transactions contains the item entered
        /// </summary>
        /// <param name="ID">Transaction to check</param>
        /// <param name="item">the item entered</param>
        /// <returns>true if item is in the transaction</returns>
        static private bool CheckItem(int ID, string item)
        {
            foreach (string s in Transactions[ID].Items)
            {
                if (s == item)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Used to return items, moves items from all transactions to return transactions
        /// </summary>
        /// <param name="ID">the sales transaction ID</param>
        static public void ReturnItem(int ID)
        {
            Console.Write("Enter the return item: ");
            string item = Console.ReadLine();
            if (CheckItem(ID, item))
            {
                int index = Transactions[ID].Items.IndexOf(item);
                decimal ReturnCost = Transactions[ID].Costs[index];
                ReturnItems.Add(item);
                ReturnCosts.Add(ReturnCost);
                Transactions[ID].Items.RemoveAt(index);
                Transactions[ID].Costs.RemoveAt(index);
                decimal sum = 0;
                foreach (decimal d in Transactions[ID].Costs)
                {
                    sum += d;
                }
                Transactions[ID].Total = sum;
            }
            else
            {
                Console.WriteLine("That item does not exist");
            }
        }

        /// <summary>
        /// Used to print receipt
        /// </summary>
        /// <param name="ID">sales transaction receipt</param>
        /// <returns>receipt string</returns>
        static public string TransactionString(int ID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("\n\n");
            sb.Append(Transactions[ID].Name + "\tID: " +
                Transactions[ID].ID.ToString() + "\t" +
                Transactions[ID].Date.ToShortDateString() + "\n");

            sb.Append("\nItems Purchased: \n");
            int count = Transactions[ID].Items.Count;

            for (int i = 0; i < count; i++)
            {
                sb.Append(Transactions[ID].Items[i] + "\t" + Transactions[ID].Costs[i].ToString("C2") + "\n");
            }

            sb.Append("\nSub Total:\t" + Transactions[ID].Total.ToString("C2"));
            decimal tax = (decimal)0.10;
            sb.Append("\nTax:\t\t" + String.Format("{0:P0}", tax) + "\n");
            decimal total = (1 + tax) * Transactions[ID].Total;
            sb.Append("\nTotal:\t\t" + total.ToString("C2") + "\n");
            return sb.ToString();
        }

        /// <summary>
        /// Used to print return receipt
        /// </summary>
        /// <param name="ID">return ID</param>
        /// <returns>return receipt string</returns>
        static public string ReturnString(int ID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("\n\n");
            sb.Append(ReturnTransactions[ID].Name + "\t" +
                ReturnTransactions[ID].ID.ToString() + "\t" +
                ReturnTransactions[ID].Date.Date.ToString() + "\n");

            sb.Append("Items Returned: \n");
            int count = ReturnTransactions[ID].Items.Count;

            for (int i = 0; i < count; i++)
            {
                sb.Append(ReturnTransactions[ID].Items[i] + "\t" + ReturnTransactions[ID].Costs[i].ToString("C0") + "\n");
            }

            return sb.ToString();
        }
    }
}
