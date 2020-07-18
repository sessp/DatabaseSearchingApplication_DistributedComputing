using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Business_GUIClasses;
using BusinessWebService.Models;

namespace BusinessWebService.Controllers
{
    public class GetValuesController : ApiController
    {

        //Method for getting an entry, via an index, through the webservice.
        public DataIntermediate Get(int id)
        {
            DataIntermediate dataObj = new DataIntermediate();
            int bal;
            uint pin, acct;
            string lname, fname;
            DataModel.getInstance().GetValuesForEntry(id, out dataObj.acct, out dataObj.pin, out dataObj.bal, out dataObj.fname, out dataObj.lname);
            Debug.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!\n" + "!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!\n");
            return dataObj;
        }
    }
}