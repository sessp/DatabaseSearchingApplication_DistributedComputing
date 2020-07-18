using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary
{
    internal class DBGenerator
    {
        private Random r = new Random();
        string[] n = { "firstname1", "firstname2", "firstname3","firstname4", "firstname5", "firstname6", "firstname7", "firstname8" };
        string[] n1 = { "lastname1", "lastname2", "lastname3", "lastname4", "lastname5", "lastname6", "lastname7", "lastname8" };
        int[] intArray = { 1, 2, 3, 4 };
        uint[] uintArray = { 1, 2, 3, 4 };

        private string GetFirstName()
        {
            return n[r.Next(0,3)];
        }

        private string GetLastName()
        {
            return n1[r.Next(0, 3)];
        }

        private uint GetPIN()
        {
            return uintArray[r.Next(0, 3)];
        }

        private uint GetAcctNo()
        {
            return uintArray[r.Next(0, 3)];
        }

        private int GetBalance()
        {
            return intArray[r.Next(0, 3)];
        }

        public void GetNextAccount(out uint pin, out uint acctNo, out string firstName, out string lastName, out int balance)
        {
            pin = GetPIN();
            acctNo = GetAcctNo();
            firstName = GetFirstName();
            lastName = GetLastName();
            balance = GetBalance();
        }
    }
}
