using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace Todo.Repository.Web
{
    public class BaseWebRepository
    {
        protected HttpClient client;

        public BaseWebRepository()
        {
            
        }
    }

    public class TodoWebRepository : BaseWebRepository
    {
        
    }
}
