Feature: Update Member Profile

@HighPriority
Scenario: Update member phone number
	Given a home page
	And a "Basic" member
	When the member phone number is updated
	And the member logouts
	And the member re-logins
	Then update phone number is successful