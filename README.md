# Eric Croom

**Problem Statement:**

Create an application that uses the "I can haz dad joke" API ([https://icanhazdadjoke.com/api](https://icanhazdadjoke.com/api)) to display jokes.

The front-end code should be as simple as possible. Use any front-end mechanism you wish but keep it simple. We're interested in how you would design and implement the back-end service.

As such, all the communication with the icanhazdadjoke API and preparing the data for display should be implemented in a back-end service using C# and .NET.

There should be two options the user can choose from:

1. Fetch a random joke.

2. Accept a search term and display the first 30 jokes containing that term. The matching term should be emphasized in some simple way (upper, quotes, angle brackets, etc.). The matching jokes should be grouped by length: Short (<10 words), Medium (<20 words), Long (>= 20 words)


## Project 
There are two folders, APITest and FrontEndTest.  Each contains a .NET Core project.  APITest contains the API and unit tests where FrontEndTest contains an simple MVC project for interacting with the API.

### Instructions

(You may need to restore and rebuild before launching)
Launch the API project.  This should open with port 5003.  
Launch the front end project.  If the backend port is not 5003 update line:  23 of the JokesClient.cs file of the front end with the appropriate port.

Press the get "Get a Random Joke" button or enter a term and search.  One suggestion for good search results is the term "dad".

Another option in lieu of the front end would be to use swagger.  I added swagger for API definition and interaction:  https://localhost:5003/swagger/index.html

### Future Considerations
Should this project grow I would do the following:
1. Determine a way to add dependency injection into the test project.  There is a nuget package for xunit that can be applied for this purpose.
2. Refactor the Joke entities into a base class and then inherit based on usage.  Though the current entities are small should they grow or need expanded functionality they could benefit from having a base.
3. Clean up the front end project.  Per specifications little time was spent on perfecting the front end.

