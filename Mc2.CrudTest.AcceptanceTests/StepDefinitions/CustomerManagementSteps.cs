using FluentAssertions;
using Mc2.CrudTest.AcceptanceTests.Dto;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Mc2.CrudTest.AcceptanceTests.StepDefinitions
{
    [Binding]
    public class CustomerManagementSteps
    {

        #region Fields
        private readonly HttpClient _httpClient;
        private readonly ScenarioContext _scenarioContext;
        #endregion

        #region Ctor
        public CustomerManagementSteps(ScenarioContext scenarioContext)
        {
            _httpClient =
            new HttpClient() { BaseAddress = new Uri("https://localhost:44307") };
            _scenarioContext = scenarioContext;
        }
        #endregion

        #region Steps
        [When(@"I send following customer details")]
        public async Task WhenISendFollowingCustomerDetails(Table table)
        {
            var requestedCustomers = table.CreateSet<CustomerRequestDto>();
            var createdCustomers = new List<CustomerResponseDto>();
            foreach (var requestedCustomer in requestedCustomers)
            {
                var response = await _httpClient.PostAsJsonAsync("customers", requestedCustomer);
                var createdCustomer = await response.Content.ReadFromJsonAsync<CustomerResponseDto>();
                createdCustomers.Add(createdCustomer);
            }
            _scenarioContext.Add("CreatedCustomers", createdCustomers);
        }

        [Then(@"Customers are created successfully")]
        public async Task ThenCustomersAreCreatedSuccessfully()
        {
            var createdCustomers = _scenarioContext.Get<List<CustomerResponseDto>>("CreatedCustomers");
            foreach (var createdCustomer in createdCustomers)
            {
                var response = await _httpClient.GetFromJsonAsync<CustomerResponseDto>($"customers/{createdCustomer.Id}");
                createdCustomer.Should().BeEquivalentTo(response);
            }
        }
        #endregion
    }
}
