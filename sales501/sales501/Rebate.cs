/*Name: Swap Gupta
 * Finished: 02/16/2018
 * File: Rebate.cs
 * Description: creates the Rebate object
 */
using System;
using System.Collections.Generic;
using System.Text;

namespace Sales501
{
    class Rebate
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public DateTime Date { get; set; }
        public decimal Discount { get; set; }

        public Rebate(string name, string address, string email, DateTime date, decimal discount)
        {
            Name = name;
            Address = address;
            Email = email;
            Date = date;
            Discount = discount;
            DateTime June = new DateTime(2018, 06, 01);
            if (DateTime.Compare(date, June) >= 0)
            {
                Console.Write("In June the standard rebate it 11%. Enter 'O' to override the June rebate.)");
                string response = Console.ReadLine().ToUpper();

                if (response == "O") ;
                else Discount = (decimal)0.11;
            }
        }
    }
}
