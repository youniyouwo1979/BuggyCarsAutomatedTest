# Web Automation Documentation

## Introduction

This file contains the prerequisites setup section and provide the steps to excute the automation tests

### Prerequisites

✓ Install Visual Studio 2022
✓ Clone the automation test repository: https://github.com/youniyouwo1979/BuggyCarsAutomatedTest.git

### Setup tips

Before running automation tests, follow the below setup steps:

1. Launch the automation code in Visual Studio:

-> Launch the visual studio, select 'Open a project or solution'
-> Locate the local folder where the automation repository is cloned, and select solution file 'BuggyCars.AutomatedTest.sln'

Note: The target framework of the project is .NET 5.0. When the solution is opened, it could shows some error, and it would prompt to install .NET 5.0

2. Install Specflow extension:

The automation tests are built using Specflow framework. The Specflow extension is required to be installed before running tests.

To install it:
-> Select menu 'Extensions'
-> Select 'Manange Extensions'
-> Search 'Specflow' in search text box. The Extension 'Specflow for Visual Studio 2022' is available to install
-> Install the Extension 'Specflow for Visual Studio 2022'

3. Configure nuget packages:

The nuget packages used by the project need to be installed properly. Otherwise, the project build will fail. 

To validate if nuget packages are installed properlty:
-> Select menu 'Build'
-> Select 'Rebuild Solution'

If there are build failure, need to restore the nuget packages:
-> Option 1: Right click on the Solution, and select 'Restore NuGet Packages'
-> Option 2: Select menu 'Tools', 'NuGet Package Manager', 'Package Manager Console'
	- In the console, execute the command: dotnet restore
	- Then rebuild solution

### Steps to execute automation tests

The tests can be executed within Visual Studio. The Test Explorer is used to execute the tests. 

1. Open Test Explorer tab:

Select menu 'View' -> 'Test Explorer'

2. Select the test scenario to be exectued from the tree within the Test Explorer tab:

Right click on the test scenario -> 'Run'