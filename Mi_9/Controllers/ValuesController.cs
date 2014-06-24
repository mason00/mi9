using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Mi_9.Models;
using System.Text;

namespace Mi_9.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        //public HttpResponseMessage Post([FromBody]JToken value)
        //{
        //    return new HttpResponseMessage(HttpStatusCode.OK);
        //}

        public async Task<HttpResponseMessage> Post(HttpRequestMessage request)
        {
            HttpResponseMessage m = new HttpResponseMessage();
            var jsonString = await request.Content.ReadAsStringAsync();
            var jss = new JavaScriptSerializer();
            RootObject data = null;
            try
            {
                data = jss.Deserialize<RootObject>(jsonString);
            }
            catch (Exception ex)
            {
                m.StatusCode = HttpStatusCode.BadRequest;
                m.Headers.Add("error", "{\"error\": \"Could not decode request: JSON parsing failed\"}");
                return m;
            }
            var pls = from pl in data.payload
                      where pl.drm && pl.episodeCount > 0
                      select new Response(){ image = pl.image.showImage, slug = pl.slug, title = pl.title };
            ResponseRoot rr = new ResponseRoot();
            rr.response = pls.ToList();
            string resultJson = jss.Serialize(rr);

            m.StatusCode = HttpStatusCode.Created;
            m.Content = new StringContent(resultJson, Encoding.UTF8, "application/json");
            return m;
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}