Feature: SqlTest
	In order to avoid silly mistakes
	As a QA Engineer
	I want to сheck that database is working properly

Background:
	Given Database connection established

Scenario: Choose people from Kiev
	When I choose Kyiv
	Then people from Kyiv are selected from the database

Scenario: Choose a FirstName, SecondName whose order is more than 6000
	When I choose  FirstName, SecondName and Order
	When I Check that the order is more than 6000
	Then people whose order is more than 6000 are selected from the database

Scenario Outline: Сheck data types of variables
	When I select <fieldTable> from table Persons
	Then I get a variable <typeTable> of <fieldTable>

	Examples:
		| fieldTable | typeTable     |
		| id         | System.Int32  |
		| FirstName  | System.String |
		| LastName   | System.String |
		| Age        | System.Byte   |

Scenario: Find the field that is marked with the primary key in the table
	When I make a request for a field with a primary key
	Then I get a field with a primary key