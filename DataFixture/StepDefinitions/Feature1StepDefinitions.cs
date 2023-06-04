using Allure.Commons;
using NUnit.Framework;
using System;
using System.Net.Http;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Infrastructure;

namespace DataFixture.StepDefinitions
{
    [Binding]
    public class Feature1StepDefinitions
    {
        HttpClient httpClient;
        HttpResponseMessage response;
        string responseBody;
        private readonly TechTalk.SpecFlow.Infrastructure.ISpecFlowOutputHelper _specFlowOutputHelper;

        public Feature1StepDefinitions(ISpecFlowOutputHelper _specFlowOutputHelper) 
        {
            httpClient = new HttpClient();
            this._specFlowOutputHelper = _specFlowOutputHelper;
        }
        [Given(@"Endpoints and resources")]
        public async Task GivenEndpointsAndResources()
        {
            Console.WriteLine("started");
            response = await httpClient.GetAsync("https://api.apilayer.com/fixer/latest?apikey=O4BAqgNL2DARrtaP3bp13PZLICsjV665&base=EUR");
            responseBody = await response.Content.ReadAsStringAsync();
            _specFlowOutputHelper.WriteLine(responseBody);
        }

        [When(@"send request")]
        public void WhenSendRequest()
        {
            Console.WriteLine("In When STatement");
            NUnit.Framework.Assert.That(response.IsSuccessStatusCode,Is.True);
        }

        [Then(@"status (.*)")]
        public void ThenStatus(int status)
        {
            if (status==200)
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
      

    }
}
