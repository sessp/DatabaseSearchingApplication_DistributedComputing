using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace BusinessTier
{
	[ServiceContract]
	public interface BusinessServerInterface
	{
		[OperationContract]
		int GetNumEntries();
		[OperationContract]
		void GetValuesForEntry(int index, out uint aN, out uint pin, out int bal, out string fName, out string lName);

		[OperationContract]
		void SearchByLastName(string lastNameSearchString, out uint aN, out uint pin, out int bal, out string fName, out string lName);
		/* function that will take in a string and return the contents of the
		first record that has a last name matching the string*/
	}
}
