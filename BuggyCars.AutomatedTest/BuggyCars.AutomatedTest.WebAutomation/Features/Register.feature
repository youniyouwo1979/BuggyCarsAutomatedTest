Feature: Member Register

Background: Pre-Condition
	Given a home page

@HighPriority
Scenario: Sign up as a new member
	When a new member registers
	Then register is successful

@MediumPriority @NegativeTest
Scenario: Attempt register using registered loyalty member
	When an attempt is made to register with an existing loyalty member
	Then register fails with the 'already registered loyalty member' error message