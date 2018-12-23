using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Net.Http;
using System.Net.Http.Headers;
using Facade.Core;
using Newtonsoft.Json;


namespace Facade.Core
{
    public class ServiceBClient
    {
        public Book SearchBook(string isbn)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:50454");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("/api/ServiceB/" + isbn).Result;
            string jsonData = response.Content.ReadAsStringAsync().Result;
            Book book = JsonConvert.DeserializeObject<Book>(jsonData);
            return book;
        }
    }
}
