Feature: Logout

@HighPriority
Scenario: Loyalty member logout successful
	Given a home page
	And a "Basic" member
	When the member logouts
	Then logout is successful