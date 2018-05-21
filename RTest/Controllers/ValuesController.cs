using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RTest.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace RTest.Controllers
{
    public class ValuesController : ApiController
    {

        public IEnumerable<Contact> Contacts { get; set; } = GetContacts();

        [ResponseCache(Duration = 60)]
        private static IEnumerable<Contact> GetContacts()
        {
            for (int i = 0; i < 100000; i++)
            {
                yield return new Contact { Age = i, Code = $"gfdjgfd{i}gfdgfd{i}", Email = $"{i}@hotmail.com", Enabled = false, Id = Guid.NewGuid(), Name = "Test" + i };
            }
        }

        [ResponseCache(Duration = 60)]
        public IEnumerable<Contact> Get()
        {
            
            return Contacts;
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([System.Web.Http.FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [System.Web.Http.FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
