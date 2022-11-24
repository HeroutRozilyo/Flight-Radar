# PFlight
A project in Windows systems. Written in C#
We used air traffic data to visually monitor incoming and outgoing flights from Tel Aviv, and also enabled a search in the history of the flights in which the user was interested.
The display layer is based on WPF, the 3-layer template, the MVVM template for each user process, including the Command classes, as well as the Micro Frontends and UserControl templates.
Saving and retrieving data is done using Entity Framework.
Using the Rest API we received data on the flight data (FlightRadar24), the weather data (OpenWeather) and Hagai Yishal data (HebCal)

Open Window:

<img width="702" alt="image" src="https://user-images.githubusercontent.com/73179974/203745249-3c758f2c-8fa9-4cde-a1eb-f3e782e22564.png">

The display of flights at a given time is updated automatically

<img width="804" alt="image" src="https://user-images.githubusercontent.com/73179974/203745392-fadcbef7-1c6a-40ba-a43f-a4d84bdfd774.png">

When you click on one of the flights (in the table or on the map), information about the aforementioned flight will be displayed, as well as the updated flight route on the map

<img width="807" alt="image" src="https://user-images.githubusercontent.com/73179974/203745585-58e7c97a-47e8-4294-9c8f-40f41061f0c5.png">
<img width="806" alt="image" src="https://user-images.githubusercontent.com/73179974/203745791-50e312cf-489d-4cb7-b7db-01a0a456efb5.png">

 Search the user's flight history
 
<img width="251" alt="image" src="https://user-images.githubusercontent.com/73179974/203745976-0feaf7c8-bca6-4eca-9808-063c6f2e510d.png">

filtering using a filter

<img width="674" alt="image" src="https://user-images.githubusercontent.com/73179974/203746251-03675db0-9ebd-4782-bad9-eccc440723f3.png">



