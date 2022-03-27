# WeatherWebApplication
Code challenge from IDB to Show the current wather based on the geolocation and the next 5 days and as an extra the application also allows user to enter the city name to show the weather. 

**Coded and designed by Rodolfo Ruiz Morales - March, 27 2022 for the InterAmerican Development Bank**

**Technology stack:**
Visual Studio 2022
.NET Core 6
UI Blazor server -HTML, CSS-
Languaje C#

**The solution have the following:**
UI
Data Service
Share Classes and AppState helper to local storage user preferences such as city name and units (F/C)
Unit Test

**NuGets:**
Darnton.Blazor.DeviceInterop - for geolocation, under MIT licence https://www.nuget.org/packages/Darnton.Blazor.DeviceInterop
Microsoft.Fast.Components.FluentUI - basic input controls, dark and light mode
Page.Blazor - for easy navigation
System.Configuration.ConfigurationManager - to read the app.settings file where are hosted several url and api key for forecasting the weather

**API:**
To get the current weather by using longitude, latitude and city name: https://api.openweathermap.org/data/2.5/weather
To get the forecast for 5 days by hour based on longitude, latitude and city name: https://api.openweathermap.org/data/2.5/forecast

URL to get dinamycally get the weather icon: http://openweathermap.org/img/w/

**Several screenshots:**

**ALLOW ACCESS FOR GEOLOCATION**

![image](https://user-images.githubusercontent.com/34136023/160300129-05305428-d964-42a4-9475-e49b41da3474.png)


**UNIT TESTS**

![image](https://user-images.githubusercontent.com/34136023/160299418-ca2135bc-7108-4702-97bb-8ca39e18e484.png)



**ERROR HANDLING:**

![image](https://user-images.githubusercontent.com/34136023/160299430-42f0991a-fda6-4038-9289-84af99ed183c.png)



**CLICK SEARCH WITH EMPTY CITY NAME **

![image](https://user-images.githubusercontent.com/34136023/160299438-e6b9318c-04ad-4efc-990e-6e3cef0f9909.png)



**WITH CITY NAME:**

![image](https://user-images.githubusercontent.com/34136023/160299446-fa7ccec8-6f0a-4656-9f43-5b68821c45de.png)



**FORECAST 5 DAYS WITH CITY NAME CELCIUS:**

![image](https://user-images.githubusercontent.com/34136023/160299477-2b797249-9372-4713-9fd4-833faecfa737.png)


**GEOLOCATED :**

![image](https://user-images.githubusercontent.com/34136023/160299484-e96b4aa6-c526-4d12-a4fc-d15bcef64015.png)


**SMALL SCREENS - RESPONSIVE**

![image](https://user-images.githubusercontent.com/34136023/160300583-e77a4b71-1a91-4538-9bf1-5a2131e5607a.png)

![image](https://user-images.githubusercontent.com/34136023/160300592-3abbca38-4f99-464e-ac9d-0db7f32cde26.png)

![image](https://user-images.githubusercontent.com/34136023/160300615-cc8bc67a-9052-4ecb-9f8d-2ac09291eee6.png)

