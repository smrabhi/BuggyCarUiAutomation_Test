Feature: BuggyCarUi
         Autmation testing for main features of the application
	

Background: Launch Application
	Given I go to application url  'https://buggy.justtestit.org'




@Smoke1
Scenario: Login and Logout 
  	When I enter username and Password
	| Username | Password |
	| smrati.kul1@gmail.com   | Admin@123    |
	And I press Login button 
	Then I can see login page
	And I can see profile page
	Then I logout from Application
	And I should not see profile details

	

@Smoke1
Scenario: Register a new user 
    Given  I click on register 
	When I can see register page
	And I fill all details 
	| Login       | FirstName | LastName    | Password  |
	| smrati.k | Smrati    | Kulshrestha | Admin@123 | 
	And I press register button 
	Then I can see success message

	
	

@SmokePage
Scenario: Popular Make Page 
	When I click on Lamborghini under popular make 
	Then I can see Lamborghini page
	



@SmokePage
	Scenario: Popular Model Page
	When I click on Popular Model 
	Then I can see Popular Model Page 
	And I can see details of the model



@SmokePage
	Scenario: Overall Rating Page
	When I click on overall rating 
	Then I can see overall rating page
	

