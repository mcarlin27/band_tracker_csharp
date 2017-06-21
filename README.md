# Band Tracker

#### A program that allows users to track which bands have played certain venues. 6/9/17

#### By **Marilyn Carlin**

## Description

A website created with C# and HTML where a user can add or view the bands which have played user-added venues, or the venues where bands have played.

### Specs
| Behavior | Example Input | Example Output | Reasoning for Spec |
| :-------------     | :------------- | :------------- | :------------- |
| **Save one Venue** | "Madison Square Garden" | "Madison Square Garden" | Simplest possible input for Venue object. |
| **Save multiple venues** | "Madison Square Garden" "Crystal Ballroom" | "Madison Square Garden", "Crystal Ballroom" | Database should save multiple instances of an object. |
| **User can query list of venues** | "Query: All Venues" | "Madison Square Garden, Crystal Ballroom" | Uses a GetAll() method to query database. |
| **User can view a specific venue** | "Madison Square Garden" "/venues/{id}" | "Madison Square Garden" | Uses Find() method to identify individual venue by id and return full database info for venue. |
| **User can edit a specific venue** | "Edit: Madison Square Garden" "/venues/{id}/edit" | "Madison Square Garden" --> "Gadison Mare Squarden" | Uses Update() method to edit individual venue's database entry; venue identified by id. |
| **User can delete a specific venue** | "Delete: Madison Square Garden" "/venues/{id}/delete" | "Venue Deleted!" | Uses Delete() method to delete individual venue's database entry; venue identified by id. |
| **Save one band** | "Modest Mouse" | "Modest Mouse" | Simplest possible input for Band object. |
| **Save multiple bands** | "Modest Mouse" "Rilo Kiley" | "Modest Mouse", "Rilo Kiley" | Database should save multiple instances of an object. |
| **User can query list of bands** | "Query: All Bands" | "Modest Mouse, Rilo Kiley" | Uses a GetAll() method to query database. |
| **User can view a specific band** | "Modest Mouse" "/bands/{id}" | "Modest Mouse" | Uses Find() method to identify individual bands by id and return full database info for band. |
| **User can edit a specific band** | "Edit: Modest Mouse" "/bands/{id}/edit" | "Modest Mouse" --> "Mouse Modest" | Uses Update() method to edit individual band's database entry; band identified by id. |
| **User can delete a specific band** | "Delete: Modest Mouse" "/bands/{id}/delete" | "Band Deleted!" | Uses Delete() method to delete individual band's database entry; band identified by id. |
| **Links Band to specific Venue** | "Modest Mouse, Madison Square Garden" | "Madison Square Garden: Modest Mouse" | Creates many-to-many relationship between venues and bands; band_id and venue_id are inputted to join table. |
| **User can query list of bands by venue** | "Query: All Bands at Crystal Ballroom" | "Modest Mouse, Rilo Kiley" | Uses a GetBands() method to query join table by venue_id. |
| **User can query list of venues by band** | "Query: All Venues played by Modest Mouse" | "Madison Square Garden, Crystal Ballroom" | Uses a GetVenues() method to query join table by band_id. |

## Setup/Installation Requirements

1. To run this program, you must have a C# compiler. I use [Mono](http://www.mono-project.com).
2. Install the [Nancy](http://nancyfx.org/) framework to use the view engine. Follow the link for installation instructions.
3. Clone this repository.
4. Open the command line--I use PowerShell.
5. In SQLCMD: > CREATE DATABASE band_tracker; > GO > USE band_tracker; > GO > CREATE TABLE venues (id INT IDENTITY(1,1), name VARCHAR(255), city VARCHAR(255)); > CREATE TABLE bands (id INT IDENTITY(1,1), name VARCHAR(255)); > CREATE TABLE bands_venues (id INT IDENTITY(1,1), band_id INT, venue_id INT); > GO. Back up and restore this database to a test database to run tests.
6. Navigate into the repository. Use the command "dnx kestrel" to start the server.
7. On your browser, navigate to "localhost:5004" and enjoy!

## Known Bugs
* No known bugs at this time.

## Technologies Used
* C#
  * Nancy framework
  * Razor View Engine
  * ASP.NET Kestrel HTTP server
  * xUnit

* SQL

* HTML

## Support and contact details

_Contact mcarlin27 over GitHub with any questions, comments, or concerns._

### License

*{This software is licensed under the MIT license}*

Copyright (c) 2017 **_{Marilyn Carlin}_**
