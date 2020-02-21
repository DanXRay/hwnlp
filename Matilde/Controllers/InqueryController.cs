using Microsoft.AspNetCore.Mvc;
using Matilde.Models;
using Newtonsoft.Json;
using System;
using System.Web;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace Matilde.Controllers
{
    public class InqueryController : Controller
    {
        [Authorize]
        public IActionResult Index(string user = "na", string query = "")
        {

            string response = string.Empty;
            try
            {
                var n = HttpContext.User.Identity.Name;
                response = SearchRequest.SayWhat(query,n);
            }
            catch (Exception)
            {
                response = Responses.GetARandomResponse();
            }
            response = HttpUtility.JavaScriptStringEncode(response);
            string json = $@"{{""response"":""{response}""}}";
            // b = JsonConvert.DeserializeObject(json);
            //json = HttpUtility.JavaScriptStringEncode(json);
            return Content(JsonConvert.SerializeObject(json), "application/json");
        }

        public IActionResult Classify(string user = "na", string title = "", string body = "")
        {
            try
            {
                var titleResponse = SearchRequest.Classify(title);
                dynamic bloibiold = new System.Dynamic.ExpandoObject();
                bloibiold.title = titleResponse;

                //split the body into multiple requests
                //var bodArray = body.Split(". ");
                string[] bodArray = body.Split(
                new[] { "\r\n", "\r", "\n", ". " },
                StringSplitOptions.None
                );
                List<object> theBod = new List<object>();  
                foreach (string line in bodArray)
                {
                    string freshLine = line.Trim();
                    var lineResponse = SearchRequest.Classify(freshLine);
                    theBod.Add(lineResponse);
                }
                bloibiold.theBod = theBod.ToArray();

                return Content(JsonConvert.SerializeObject(bloibiold), "application/json");
            }
            catch (Exception)
            {
                string rr = Responses.GetARandomResponse();
                rr = HttpUtility.JavaScriptStringEncode(rr);
                string json = $@"{{""response"":""{rr}""}}";
                return Content(JsonConvert.SerializeObject(json), "application/json");
            }
        }

    }
}
