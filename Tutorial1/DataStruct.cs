using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace DatabaseLibrary
{
	internal class DataStruct
	{
		public uint acctNo;
		public uint pin;
		public int balance;
		public string firstName;
		public string lastName;
		//public Bitmap avatar = new Bitmap();

		public DataStruct()
		{
			acctNo = 0;
			pin = 0;
			balance = 0;
			firstName = "";
			lastName = "";
		}

		public DataStruct(uint accountNumber,uint pinNum,int balanceNum, string fName, string lName)
		{
			acctNo = accountNumber;
			pin = pinNum;
			balance = balanceNum;
			firstName = fName;
			lastName = lName ;
		}

	}
}
