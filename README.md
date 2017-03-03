# Band Tracker Database
### Site to view band booking and venue

#### By _**Minh Phuong**_

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



#### Ingredient class


#### Recipe && Ingredient classes

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

===

### License

*This project is licensed under the MIT license.*

Copyright (c) 2017 _**Minh Phuong**_
