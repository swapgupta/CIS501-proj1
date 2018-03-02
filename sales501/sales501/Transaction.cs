/*Name: Swap Gupta
 * Finished: 02/16/2018
 * File: Transaction.cs
 * Description: creates the Transaction object
 */
using System;
using System.Collections.Generic;
using System.Text;

namespace Sales501
{
    class Transaction
    {
        public string Name { get; set; }
        public List<string> Items { get; set; }
        public List<decimal> Costs { get; set; }
        public decimal Total { get; set; }
        public int ID { get; set; }
        public DateTime Date { get; set; }

        public Transaction(string name, List<string> items, List<decimal> cost, int id, DateTime date)
        {
            Name = name;
            Items = items;
            Costs = cost;
            ID = id;
            Date = date;

            decimal sum = 0;
            foreach (decimal d in cost)
            {
                sum += d;
            }
            Total = sum;
        }
    }
}
