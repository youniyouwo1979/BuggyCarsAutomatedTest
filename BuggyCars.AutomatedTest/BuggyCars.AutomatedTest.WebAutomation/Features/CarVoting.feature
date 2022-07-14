Feature: Car Voting

@HighPriority
Scenario: Vote a car as loyalty member
	Given a home page
	When a new member votes the most popular car with a comment
	Then the vote is successful
	And the comment is added
	And the vote is increased 