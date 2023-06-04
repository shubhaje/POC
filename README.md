SpecFlow_C# BDD Project
Open the solution file POC.sln in Visual Studio or your preferred IDE. Dependencies This example uses the following dependencies:

SpecFlow: A BDD (Behavior Driven Development) framework for .NET. HttpClient: A class for sending HTTP requests and receiving HTTP responses from a resource identified by a URI. These dependencies will be automatically restored when you build the solution. SpecFlow NUnit SpecFlow MSTest SpecFlow Allure and so on

Usage Open the DataFixture project in the solution.

Open the Feature1.feature and DataFixture.feature file located in the Features folder. This file contains a SpecFlow scenario Get API.

Implement the step definitions for the scenario in the DataFixtureStepDefinitions.cs and Feature1StepDefinition.cs file located in the StepDefinitions folder. This is where you can write the C# code that interacts with HttpClient and performs the necessary HTTP requests.

Build the solution to ensure all the dependencies are resolved.

Run the SpecFlow tests using the test runner in your IDE or via the command line. This will execute the scenario defined in the GoogleSearch.feature file and execute the corresponding step definitions.

Also you can view overall allure report using "allure serve allure-results"
