@SignIn
Feature: Member Login

Background: Pre-Condition
	Given a home page

@HighPriority
Scenario: Loyalty member login successful
	When a "Basic" member logins
	Then login is successful

@MediumPriority @NegativeTest
Scenario: Attempt login with an unregistered member identifier
	When an attempt is made to login with an unregistered member
	Then login fails with the 'invalid credentials' error message
