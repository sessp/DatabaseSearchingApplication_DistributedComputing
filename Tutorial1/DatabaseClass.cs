using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary
{
	public class DatabaseClass
	{
		//Class is designed as instructed in tutorials. Doesn't really deviate from their design specification.

		List<DataStruct> dS;

		public DatabaseClass()
		{
			dS = new List<DatabaseLibrary.DataStruct>();

			//Generate the "Database"
			uint pin, acctNo;
			int bal;
			string fName, lName;
			DatabaseLibrary.DBGenerator dbGenerator = new DBGenerator();
			int i;
			for (i = 0; i < 100000; i++)
			{
				dbGenerator.GetNextAccount(out pin,out acctNo,out fName,out lName,out bal);
				
				DataStruct dataStructure = new DataStruct(acctNo, pin, bal, fName, lName);
				dS.Add(dataStructure);
			}
		}

		//Methods to get each of the fields of the entry, via the entries index.
		public uint GetAcctNoByIndex(int index)
		{
			return dS[index].acctNo;
		}

		public uint GetPINByIndex(int index)
		{
			return dS[index].pin;
		}

		public string GetFirstNameByIndex(int index)
		{
			return dS[index].firstName;
		}

		public string GetLastNameByIndex(int index)
		{
			return dS[index].lastName;
		}

		public int GetBalanceByIndex(int index)
		{
			return dS[index].balance;
		}

		public int GetNumRecords()
		{
			return dS.Count;
		}

	}
}
