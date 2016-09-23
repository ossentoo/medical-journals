# Medical Journals Solution
This is the Medical Journals application.

## Introduction
This is a solution that uses

* ASP.NET Core 1.0 and .NET Core class libraries
* Entity Framework 7
* SQL Local DB


## Database setup
This solution uses Entity Framework migrations.  To create the database and schema run the migrations.  The migrations can be run by:

1. Opening a Visual Studio command prompt
2. Building the solution
3. Navigating to the MedicalJournals.Entities project
4. Issuing the command 'dotnet ef --startup-project ..\..\MedicalJournals.Web\ migrations list'

This should build the project and migrate the latest schema to the local db database.

You can view the database by opening SQL Server Management Studio and connecting to: (LocalDb)\MSSQLLocalDB
You should see a database called 'MedicalJournals-4ae3798b'.