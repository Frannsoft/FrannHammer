﻿using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace KuroganeHammer.Data.Core.Models
{
    public class SmashAttributeType : BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime LastModified { get; set; }

        public override Task<HttpResponseMessage> Create(HttpClient client)
        {
            throw new NotImplementedException();
        }

        public override Task<HttpResponseMessage> Update(HttpClient client)
        {
            throw new NotImplementedException();
        }

        public override Task<HttpResponseMessage> Delete(HttpClient client)
        {
            throw new NotImplementedException();
        }
    }
}