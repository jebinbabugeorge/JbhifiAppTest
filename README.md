# JbhifiAppTest
An App that allows a user to query the current weather status for a given country and city

# Problem Statement

The challenge is to build an app using C# .NET and Javascript that fronts the Current Weather Data service.

The service should have:
1. Your own back-end API that enforces rate limiting that then on calls OpenWeatherMap.com with its API keys:
	a. Rate limiting:
		i Apply your own API Key scheme.
		ii Your API Key is rate limited to 5 weather reports an hour.
		iii After that your service should respond in a way which communicates that the hourly limit has been exceeded.
		iv Create 5 API Keys. Pick a convention for handling them that you like; using simple string constants is fine.

	b. Functionality:
		i This back-end API should have a REST URL that accepts both a city name and country name.
		ii Based upon these inputs, and the API Key (your scheme), your service should decide whether or not to call the OpenWeatherMap service with its API key (the two provided above).
		iii If it does, the only weather data you need to return to the client is the description field from the weather JSON result. Whether it does or does not have this field, it should respond appropriately to the client.

2. A webpage (front-end) that:
	a. Uses the REST URL above.
	b. Allows user to enter city name and country name.
	c. Presents the result to user.
	d. Handles any error.
	
# Assumptions

As part of building this app, due to time constraints, I had to take certain assumptions and they are as follows:-

1. The API doesn't have any authentication or authorization built into it.
2. The API Key required to access the API from the react app needs to be entered via the front end by the user. In real world, this could be achieved via having the API key returned after authentication there by avoiding the user having to enter the API Key.
3. Rate Limiting is achieved using memory cache and not using distributed cache like redis. So, the rate limiting feature will not work under load balanced servers.


# Run the solution

The solution has two parts. 

a. Front End - Built using React
b. Back End - Built using ASP.NET Core Web API with C#

# Front End

Open the Solution (JbhifiAppTest/Jbhifi.UI/jbhifi-ui) using vscode or any other IDE of your choice

Run npm install

Run npm start

# Back End
Open the solution using vs 2019
Open appsettings.json and set the Open Weather Api Key 
Build the solution

Run the solution
