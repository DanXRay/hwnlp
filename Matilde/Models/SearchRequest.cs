using System;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;
using System.Text;

namespace Matilde.Models
{
    public static class SearchRequest
    {
        private static string _bearer;
        private static HttpClient httpClient;
        private const string _searchService = "https://search.test.hwapps.net";
        private const string _authService = "https://auth.test.hwapps.net";

        private static dynamic SearchOnInquery(string inquery, int retries = 0)
        {
            if (string.IsNullOrEmpty(_bearer))
            {
                GetBearer();
            }
            string json = string.Empty;
            string url = _searchService + "/topics/cb?top=1&q=" + inquery;
            var webClient = new System.Net.WebClient { Encoding = Encoding.UTF8 };
            webClient.Headers.Add(HttpRequestHeader.ContentType, "application/json");
            try
            {
                webClient.Headers["Authorization"] = _bearer;
                webClient.Headers["X-HW-Version"] = "1";
                json = webClient.DownloadString(url);
                dynamic jsonO = JsonConvert.DeserializeObject<dynamic>(json);
                return jsonO;
            }
            catch (WebException wex)
            {
                _bearer = string.Empty;//clear the bearer - in case that is the problem
                if (retries < 1)
                {
                    retries++;
                    return SearchOnInquery(inquery, retries);
                }
                else
                {
                    throw wex;
                }
            }
        }

        internal static object Classify(string inquery)
        {
            string what = string.Empty;
            dynamic bloibiold = new System.Dynamic.ExpandoObject();
            try
            {
                bloibiold.query = inquery;
                dynamic jsonO = SearchOnInquery(inquery);


                dynamic items = jsonO.items;
                if (items != null)
                {
                    var oneD = items[0].data;
                    var blarg = oneD.@abstract;
                    var blug = blarg.consumer;
                    var langO = blug["en-us"];
                    what = GetTheFirstSentence((string)langO);
                    //what = jsonO.items[0].data.abstract.consumer.en-us;
                    
                }
                //check the semantics of the inquiry
                dynamic cognition = jsonO.cognition;
                int confidence = cognition.confidence;
                bloibiold.confidence = confidence;
                if(cognition.hwcv_concepts != null){
                    bloibiold.concepts = cognition.hwcv_concepts.concepts;
                }
                //specify any replaced terms
                var replaced_terms = cognition.replaced_terms ?? null;
                if (replaced_terms != null)
                {
                    if (replaced_terms.HasValues)
                    {
                        string termy = $"[{replaced_terms[0]}?] {what}";
                        bloibiold.replaced_terms = termy;
                    }
                }
                return bloibiold;
            }
            catch (WebException wex)
            {
                what = wex.Message;
            }
            if (string.IsNullOrWhiteSpace(what))
            {
                what = Responses.GetARandomResponse();
            }
            what = what.Replace("\\r\\n", "");
            what = what.TrimStart();
            what = what.Trim();
            return what;
        }

        private static void GetBearer()
        {
            string clientId = Environment.GetEnvironmentVariable("CLIENT_ID");
            string clientSecret = Environment.GetEnvironmentVariable("CLIENT_SECRET");
            
            var authUri = new Uri(_authService + "/oauth2/token");
            using (var webClient = new WebClient { Encoding = Encoding.UTF8 })
            {
                webClient.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                webClient.Headers.Add("X-HW-Version", "1");
                webClient.Headers.Add(HttpRequestHeader.Authorization, BasicTokenGenerator(clientId, clientSecret));
                string expiresInSeconds = "80000";
                string postData = $@"{{'grant_type' : 'client_credentials','scope' : '*','expires_in' : '{expiresInSeconds}','environment' : 'test'}}";
                string reply = webClient.UploadString(authUri, postData);
                dynamic json = JsonConvert.DeserializeObject<dynamic>(reply);
                var authToken = json.access_token.ToString();
                _bearer = "Bearer " +  authToken;
            }
        }

        private static string BasicTokenGenerator(string clientId, string clientSecret)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(clientId + ":" + clientSecret);
            string token = Convert.ToBase64String(bytes);
            return $"Basic {token}";
        }

        private static void LogThis(string youser, dynamic resulto, string response)
        {
            //to the provider

        }

        public static string SayWhat(string inquery, string youser)
        {
            string what = string.Empty;
            dynamic cognition = null;
            try
            {
                dynamic jsonO = SearchOnInquery(inquery);
                dynamic items = jsonO.items;
                if (items != null)
                {
                    var oneD = items[0].data;
                    var blarg = oneD.@abstract;
                    var blug = blarg.consumer;
                    var langO = blug["en-us"];
                    what = GetTheFirstSentence((string)langO);
                    //what = jsonO.items[0].data.abstract.consumer.en-us;
                    what = EmoticonifyIt(what);
                }
                //check the semantics of the inquiry
                cognition = jsonO.cognition;
                int confidence = cognition.confidence;

                //prepend a confidence emoticon/response
                what = Emoticons.EmoticonifyTheConfidence(confidence) + " " + what;

                //specify any replaced terms
                var replaced_terms = cognition.replaced_terms ?? null;
                if (replaced_terms != null)
                {
                    if (replaced_terms.HasValues)
                    {
                        what = $"[{replaced_terms[0]}?] {what}";
                    }
                }
            }
            catch (WebException wex)
            {
                what = wex.Message;
            }
            if (string.IsNullOrWhiteSpace(what))
            {
                what = Responses.GetARandomResponse();
            }
            what = what.Replace("\\r\\n", "");
            what = what.TrimStart();
            what = what.Trim();
            //Log
            LogThis(youser, cognition, what);
            return what;
        }

        private static string EmoticonifyIt(string sentence)
        {
            //analyze it
            dynamic blargo = SearchOnInquery(sentence);
            dynamic cognition = blargo.cognition;
            if(cognition != null)
            {
                dynamic entities = cognition.hw_entities;
                if(entities != null)
                {
                    //any aspects to be matched?
                    dynamic aspects = entities.aspects;
                    if (aspects != null)
                    {
                        string recognized_aspects = aspects[0];
                        return sentence + " " + Emoticons.EmoticonFromAspect(recognized_aspects);
                    }
                }
                
            }
            return sentence;
        }



        private static string GetTheFirstSentence(string textO)
        {
            string firstSentence = string.Empty;
            int firstPeriod = textO.IndexOf('.');
            if(firstPeriod > 0)
            {
                firstSentence = textO.Substring(0,firstPeriod + 1);
            }
            else
            {
                firstSentence = textO;
            }
            
            return firstSentence;
        }

    }

}
