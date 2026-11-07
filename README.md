Employee Working Hours – C# Assignment

Candidate Name: Vasanthakumar
Technology: C# (.NET 7.0)
Executable File: Vasanthakumar_C# Assignment.exe

Project Overview

This project is developed using C# and .NET 7.0. It connects to an employee time-tracking API and gets the working hours of each employee.

The application calculates the total hours worked by each employee and sorts the results from highest to lowest.

It also generates two output files:

EmployeeWorkingHours.html – Shows the employee names and their total working hours. Employees who worked less than 100 hours are highlighted in red.
EmployeeWorkingHoursPieChart.png – Displays a pie chart showing the working-hour contribution of each employee.
Assignment Requirements
Requirement	Status	Details
Language	Completed	Developed using C# and .NET 7.0
Executable File	Completed	Created a standalone executable file
File Naming	Completed	Follows the required naming format
Employee Data	Completed	Employee details are fetched from the live API
HTML Report	Completed	Generates an HTML table with employee working hours
Pie Chart	Completed	Generates a PNG pie chart
GitHub Upload	Completed	Source code, executable, and output files are uploaded directly without ZIP or RAR files
Output Results
1. Pie Chart

The application generates a pie chart showing how many hours each employee has worked compared to the total working hours.

File: EmployeeWorkingHoursPieChart.png

2. Employee Working Hours Report

The HTML report displays all employees along with their total working hours.

Employees who worked less than 100 hours are highlighted in red so they can be easily identified.

Employee Name	Total Hours Worked	Status
Patrick Huthinson	222.00 hrs	Normal
Abhay Singh	209.30 hrs	Normal
Stewart Malachi	206.63 hrs	Normal
John Black	204.68 hrs	Normal
Mary Poppins	182.25 hrs	Normal
Tim Perkinson	176.47 hrs	Normal
Kavvay Verma	165.72 hrs	Normal
Rita Alley	113.60 hrs	Normal
Raju Sunuwar	101.02 hrs	Normal
Tamoy Smith	90.90 hrs	Below 100 Hours
How to Run the Application
Method 1: Using the Executable File

The application is provided as a standalone executable, so there is no need to install .NET separately.

Simply double-click the following file or run it from the command prompt:

cmd
"Vasanthakumar_C# Assignment.exe"

Method 2: Using Source Code

If you have the .NET 7.0 SDK installed, you can run the project using:

bash
dotnet run


Once the application runs, it fetches the latest employee data from the API and automatically creates the HTML report and pie chart in the application directory.

Project Files

The GitHub repository contains the following files:

Vasanthakumar_C# Assignment.exe – Application executable file.
Program.cs – Contains the code to fetch employee data, calculate working hours, and generate the HTML report.
Piechart.cs – Contains the code to create the pie chart using SkiaSharp.
EmployeeWorkingHours.html – Generated employee working hours report.
EmployeeWorkingHoursPieChart.png – Generated pie chart image.
EmployeeWorkingHours.csproj – Project file containing the required settings and dependencies.
Conclusion

I developed this application to calculate employee working hours automatically using C#. It fetches data from the API, calculates and sorts the working hours, and generates an HTML report and a pie chart.

The project helped me practise API integration, data processing, sorting, and file generation using C# and .NET 7.0.
