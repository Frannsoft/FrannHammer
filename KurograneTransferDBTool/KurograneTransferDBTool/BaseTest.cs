using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace KurograneTransferDBTool
{
    public class BaseTest
    {
        protected HttpClient client;
        protected const string BASEURL = "http://localhost:53410/api/";

        [SetUp]
        public void SetUp()
        {
            client = new HttpClient();
        }


    }
}
