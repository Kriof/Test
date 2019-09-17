using Interview.Models;
using Interview.Scripts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Interview.Controllers
{
    public class ValuesController : ApiController
    {
        ApplicationHelper _applicationHelper;
        public ValuesController()
        {
            _applicationHelper = new ApplicationHelper();
        }
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public Application Get(int id)
        {
            return _applicationHelper.GetApplication(id).Result;
        }

        // POST api/values
        public void Post([FromBody]Application value)
        {
            var result = _applicationHelper.PostApplication(value).Result;
        }

        // PUT api/values/5
        public void Put([FromBody]Application value)
        {
            var result = _applicationHelper.PutApplication(value);
        }

        // DELETE api/values/5
        public void Delete(Guid id)
        {
            var result = _applicationHelper.RemoveApplication(id);
        }
    }
}
