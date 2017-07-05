Feature: UniqueApi
	As an api consumer who needs character unique data
	I want to be given character unique data properties when requested

Scenario: Request all unique data
	Given The api route of api/uniqueproperties
	When I request all data
	Then the result should be a list of all unique data