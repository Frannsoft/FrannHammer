﻿Feature: MovesApi
	As an api consumer who needs move data
	I want to be given move data when requested

@GetAll
Scenario: Request all move data
	Given The api route of api/moves
	When I request all data
	Then The result should be a list of all move data

@Get
Scenario: Request one single moves data
	Given The api route of api/moves/{id}
	When I request one specific item by id 58ff7e0306eafb0d9cf025bf
	Then The result should be just that moves data
