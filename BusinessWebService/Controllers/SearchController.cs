using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Business_GUIClasses;
using BusinessWebService.Models;

namespace BusinessWebService.Controllers
{
    public class SearchController : ApiController
    {
        //Method for requesting a search via the web service.
        public DataIntermediate Post([FromBody]SearchData value)
        {
            DataIntermediate dataObj = new DataIntermediate();
            DataModel.getInstance().SearchByLastName(value.searchString, out dataObj.acct, out dataObj.pin, out dataObj.bal, out dataObj.fname, out dataObj.lname);
            return dataObj;
        }
    }
}