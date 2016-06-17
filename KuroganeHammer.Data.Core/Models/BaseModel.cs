﻿using System.Net.Http;
using System.Threading.Tasks;

namespace KuroganeHammer.Data.Core.Models
{
    public abstract class BaseModel
    {
        public abstract Task<HttpResponseMessage> Update(HttpClient client);
        public abstract Task<HttpResponseMessage> Delete(HttpClient client);
    }
}
