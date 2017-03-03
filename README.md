# Band Tracker Database
----
### Site to view band booking and venue

#### By Minh Phuong

## Description

This website will track bands and the venues where they've played concerts

## Setup/Installation Requirements

##### Create Database and Tables
* In a command window:
```sql
> sqlcmd -S "(localdb)\mssqllocaldb"
> CREATE DATABASE band_tracker;
> GO
> USE band_tracker;
> GO
> CREATE TABLE bands (id INT IDENTITY(1,1), name VARCHAR(255));
> GO
> CREATE TABLE venues (id INT IDENTITY(1,1), name VARCHAR(255));
> GO
> CREATE TABLE bands_venues (id INT IDENTITY(1,1), band_id INT, venue_id INT);
> GO
```
* Requires DNU, DNX, MSSQL, and Mono
* Clone to local machine
* Use command "dnu restore" in command prompt/shell
* Use command "dnx kestrel" to start server
* Navigate to http://localhost:5004 in web browser of choice

## Specifications

#### Band Class

* The DeleteAll method for the Band class will delete all rows from the bands table.
  * Example Input: none
  * Example Output: nothing

* The GetAll method for the Band class will return an empty list if there are no entries in the Band table.
  * Example Input: N/A
  * Example Output: empty list

* The Equals method for the Band class will return true if the Band in local memory matches the Band pulled from the database.
  * Example Input:  
> Local: "Green Day" , id is 1
> Database: "Green Day" , id is 1
  * Example Output: `true`

* The Save method for the Band class will save new bands to the database.
  * Example Input:  
  > New band: "Green Day"
  * Example Output: no return value

* The Save method for the Band class will assign an id to each new instance of the Band class.
  * Example Input:  
  > New band: "Green Day" , `local id: 0`  
  * Example Output:  
  > "Green Day" , `database-assigned id`  

* The GetAll method for the Band class will return all band entries in the database in the form of a list.
  * Example Input:  
  > "Green Day" , id is 10  
  > "Spice Girl", id is 11  
  * Example Output: `{Green Day object}, {Spice Girl object}`

* The Find method for the Band class will return the Band as defined in the database.
  * Example Input:
  > "Green Day"
  * Example Output:
  > "Green Day" , `database-assigned id`

* The Update method for the Band class will return the Band with the new band name and instructions.
  * Example Input:
  > "Green Day" , id is 1
  * Example Output:
  > "Yellow Day" , id is 1

The DeleteThis method for the Band class will delete the band from the list of Band.
* Example Input:
  > DeleteThis "Green Day", GetAll()
* Example Output:
  > `{No Doubt object}, {Spice Girl object}`

* The Search method for the Band class will return a list of Bands with matched name.
  * Example Input:
  > Search "Green Day"
  * Example Output:
  > "Green Day" , id is 1



#### Ingredient class


#### Band && Ingredient classes

#### User Interface



## Support and contact details

Please contact Minh Phuong mphuong@kent.edu with any questions, concerns, or suggestions.


## Technologies Used

This web application uses:
* Nancy
* Mono
* DNVM
* C#
* Razor
* MSSQL & SSMS

****

### License

*This project is licensed under the MIT license.*

Copyright (c) 2017 _**Minh Phuong**_
