using Allure.Commons;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using SpecFlow.Internal.Json;
using Stripe;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Web.Helpers;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Infrastructure;
using static System.Net.WebRequestMethods;

namespace DataFixture.StepDefinitions
{
    [Binding]
    public class DataFixtureStepDefinitions
    {
        HttpClient httpClient;
        HttpResponseMessage response;
        HttpResponseMessage Customer;
        string responseBody;
        private readonly TechTalk.SpecFlow.Infrastructure.ISpecFlowOutputHelper _specFlowOutputHelper;
        public DataFixtureStepDefinitions(ISpecFlowOutputHelper _specFlowOutputHelper)
        {
            httpClient = new HttpClient();
            this._specFlowOutputHelper = _specFlowOutputHelper;
        }

        [Given(@"USer sends Endpoints and AccessKey and ""([^""]*)""")]
        public async Task GivenUSerSendsEndpointsAndAccessKeyAnd(string cur)
        {
            Console.WriteLine("started");
            string baseUri= "https://api.apilayer.com/fixer/latest?apikey=O4BAqgNL2DARrtaP3bp13PZLICsjV665&base=" + cur;
            response = await httpClient.GetAsync(baseUri);
            string responseBody = await response.Content.ReadAsStringAsync();


            using (StringReader reader = new StringReader(responseBody))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    // Process each line of the response
                    Console.WriteLine(line);
                }
                string responseContent = await response.Content.ReadAsStringAsync();
                string[] responseLines = responseContent.Split('\n');
                foreach (string lines in responseLines)
                {
                    // Process each line of the response
                    Console.WriteLine(lines);
                }
                string responseContents = await response.Content.ReadAsStringAsync();
                var jsonResponse = JsonConvert.DeserializeObject<JObject>(responseContents);
                Console.WriteLine(jsonResponse["success"]);

                string val_s = (string)jsonResponse["success"];
                if (val_s == "False")
                {
                    Console.WriteLine("Working asExpected");

                    Assert.IsTrue(Regex.IsMatch("True", val_s), "Response does not match");

                }
                else
                {
                    Assert.IsTrue(Regex.IsMatch("True", val_s), "Response does match");
                }
                /* foreach (var property in jsonResponse.Properties())
                 {
                     string propertyName = property.Name;
                     JToken propertyValue = property.Value;

                     // Process each property of the response
                     Console.WriteLine($"Property Name: {propertyName}, Value: {propertyValue}");

                 }*/
            }

            _specFlowOutputHelper.WriteLine(responseBody);
            Console.WriteLine("request done");
            Console.WriteLine("{abc}");

        }

        [When(@"User sends request")]
        public void WhenUserSendsRequest()
        {
            Console.WriteLine("In When STatement");
            Console.WriteLine("{responseBody}");
            NUnit.Framework.Assert.That(response.IsSuccessStatusCode, Is.True);
        }

        [Then(@"Verify status should be (.*)")]
        public void ThenVerifyStatusShouldBe(int status)
        {
            if (status == 200)
            {
                Console.WriteLine("Request was successful");
                NUnit.Framework.Assert.That(response.IsSuccessStatusCode, Is.True);
                Console.WriteLine(status.ToString());
            }
            else
            {
                Console.WriteLine("Unexpected Error code {response.StatusCode}: { response.ErrorMessage}");
            }

        }

        [Given(@"User sets Endpoing, Accesskey and currency as ""([^""]*)""")]
        public async Task GivenUserSetsEndpoingAccesskeyAndCurrencyAs(string cur)
        {
            Console.WriteLine("started");
            response = await httpClient.GetAsync("https://api.apilayer.com/fixer/latest?apikey=O4BAqgNL2DARrtaP3bp13PZLICsjV665&base=EUR");

        }

        [When(@"User sends request with input")]
        public async Task WhenUserSendsRequestWithInput()
        {
            string responseBody = await response.Content.ReadAsStringAsync();
            _specFlowOutputHelper.WriteLine(responseBody);
            Console.WriteLine("request done");
            Console.WriteLine("{abc}");
        }

        [Then(@"Verify status code should be (.*)")]
        public async Task ThenVerifyStatusCodeShouldBe(int status)
        {
            NUnit.Framework.Assert.That(response.IsSuccessStatusCode, Is.True);
            HttpStatusCode statusCode = response.StatusCode;
            if (statusCode == HttpStatusCode.OK)
            {
                Console.WriteLine("Request was successful");
                NUnit.Framework.Assert.That(response.IsSuccessStatusCode, Is.True);
                Console.WriteLine(status.ToString());
            }
            else
            {
                NUnit.Framework.Assert.That(response.IsSuccessStatusCode, Is.False);
                Console.WriteLine(status.ToString());
                Console.WriteLine("Unexpected Error code {response.StatusCode}: { response.ErrorMessage}");
            }

            

        }
        [Then(@"Verify success value should be True")]
        public async Task ThenVerifySuccessValueShouldBeTrue()
        {
            using (StringReader reader = new StringReader(responseBody))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    // Process each line of the response
                    Console.WriteLine(line);
                }
                string responseContent = await response.Content.ReadAsStringAsync();
                string[] responseLines = responseContent.Split('\n');
                foreach (string lines in responseLines)
                {
                    // Process each line of the response
                    Console.WriteLine(lines);
                }
                string responseContents = await response.Content.ReadAsStringAsync();
                var jsonResponse = JsonConvert.DeserializeObject<JObject>(responseContents);
                Console.WriteLine(jsonResponse["success"]);

                string val_s = (string)jsonResponse["success"];
                if (val_s == "True")
                {
                    Console.WriteLine("Working asExpected");

                    

                }
                else
                {
                    Assert.IsFalse(Regex.IsMatch("True", val_s), "Response does match");
                }
            }
        }
        [Then(@"verify ""([^""]*)"" in base key")]
        public async Task ThenVerifyInBaseKey(string cur)
        {
            string responseContents = await response.Content.ReadAsStringAsync();
            var jsonResponse = JsonConvert.DeserializeObject<JObject>(responseContents);
            Console.WriteLine(jsonResponse["base"]);

            string val_s = (string)jsonResponse["success"];
            if (val_s == cur)
            {
                Console.WriteLine("Working asExpected");
                Assert.IsTrue(Regex.IsMatch(cur, val_s), "Response does match");


            }
            else
            {
                Assert.IsFalse(Regex.IsMatch(cur, val_s), "Response does match");
            }
        }
        
        [Then(@"verify rates for given ""([^""]*)""")]
        public async Task ThenVerifyRatesForGiven(string currency)
        {
            string responseContents = await response.Content.ReadAsStringAsync();
            var jsonResponse = JsonConvert.DeserializeObject<JObject>(responseContents);
            /*  Console.WriteLine(jsonResponse["rates"][currency]);*/
            /* string val_s = (string)jsonResponse["rates"][currency];*/
            if ((Boolean)jsonResponse["rates"][currency] == true)
            {
                try
                {
                    Console.WriteLine("Working asExpected");
                }
                catch(Exception ex)
                {
                    throw new TargetInvocationException(ex);
                }
            

            }
           
        }
        [Then(@"verify currency exist in rates for given ""([^""]*)""")]
        public async Task ThenVerifyCurrencyExistInRatesForGiven(string curr)
        {
            string responseContents = await response.Content.ReadAsStringAsync();
            var jsonResponse = JsonConvert.DeserializeObject<JObject>(responseContents);
            /*  Console.WriteLine(jsonResponse["rates"][currency]);*/
             string val_s = (string)jsonResponse["rates"][curr];
            Console.Write(val_s);
            if (val_s == null)
            {
                Assert.False(null);
            }
           
        }



    }
}
