@SignIn
Feature: Member Signs In

Background: Pre-Condition
	Given a home page

@C60070 @HighPriority
Scenario: Loyalty member sign in successful
	When a "Basic" member signs in
	Then sign in is successful

@C60071 @MediumPriority @ErrorMessage
Scenario: Attempt sign in with an unregistered member identifier
	When an attempt is made to sign in with a valid password but with an unregistered member identifier
	Then sign in fails with the 'invalid credentials' error message

@C60072 @MediumPriority @ErrorMessage
Scenario: Attempt sign in as a loyalty member with incorrect password
	When an attempt is made to sign in with a valid member identifier but with an incorrect password
	Then sign in fails with the 'invalid credentials' error message

@60073 @HighPriority
Scenario: Loyalty member sign out successful
	Given a "Basic" member
	When the member logouts
	Then logout is successful
