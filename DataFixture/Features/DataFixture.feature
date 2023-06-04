Feature: Feature2

A short summary of the feature

@tag1
Scenario: Verify Status of Get Request
	Given USer sends Endpoints and AccessKey and "EUR" 
	When User sends request
	Then Verify status should be 200

#Scenario: Positive Scenario- Verify value of Success
#	Given USer sends Endpoints and AccessKey and "NOK" 
#	When User sends request
#	Then Verify success value should be True

Scenario: Positive Scenario- Verify value of provided Currency in Base Key
	Given USer sends Endpoints and AccessKey and "EUR" 
	When User sends request
	Then verify "cur" in base key

Scenario: Positive Scenario- Verify rates
	Given USer sends Endpoints and AccessKey and "EUR" 
	When User sends request
	Then verify rates for given "AED"

Scenario: Negative Scenario - Verify rates
	Given USer sends Endpoints and AccessKey and "EUR" 
	When User sends request
	Then verify currency exist in rates for given "AAD"