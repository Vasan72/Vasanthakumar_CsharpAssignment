# Employee Working Hours — C# Assignment

**Candidate Name:** Vasanthakumar  
**Technology:** C# (.NET 7.0)  
**Executable File:** `Vasanthakumar_C# Assignment.exe`  

---

## 📌 Project Overview
This application connects to the employee time-tracking API, calculates the total working hours for each employee, sorts them from highest to lowest, and automatically produces two output reports:
1. **HTML Report Table** (`EmployeeWorkingHours.html`) — displays all employees and their total hours, highlighting employees who worked less than 100 hours in red.
2. **Pie Chart Image** (`EmployeeWorkingHoursPieChart.png`) — visualizes the percentage distribution of hours worked by each employee.

---

## ✅ Assignment Requirements Checklist

| Requirement | Status | Details |
| :--- | :---: | :--- |
| **Language & Platform** | ✅ | Built using **C#** and .NET 7.0 |
| **Executable File** | ✅ | Single standalone file: `Vasanthakumar_C# Assignment.exe` |
| **Naming Convention** | ✅ | Follows `<YourName>_C# Assignment.exe` |
| **Data Integrity** | ✅ | Realistic names used from live API (*Patrick Huthinson, Abhay Singh, etc.*) |
| **Output Results** | ✅ | Both HTML table and PNG pie chart generated and included |
| **No Zip / RAR** | ✅ | All source code, executable, and outputs are uploaded directly to GitHub |

---

## 📊 Output Results

### 1. Pie Chart (`EmployeeWorkingHoursPieChart.png`)
Below is the generated pie chart visualizing each employee's contribution to total working hours:

![Employee Working Hours Pie Chart](EmployeeWorkingHoursPieChart.png)

---

### 2. Summary Table (`EmployeeWorkingHours.html`)
Employees who worked **under 100 hours** are flagged and highlighted:

| Employee Name | Total Hours Worked | Status |
| :--- | :---: | :---: |
| Patrick Huthinson | 222.00 hrs | Normal |
| Abhay Singh | 209.30 hrs | Normal |
| Stewart Malachi | 206.63 hrs | Normal |
| John Black | 204.68 hrs | Normal |
| Mary Poppins | 182.25 hrs | Normal |
| Tim Perkinson | 176.47 hrs | Normal |
| Kavvay Verma | 165.72 hrs | Normal |
| Rita Alley | 113.60 hrs | Normal |
| Raju Sunuwar | 101.02 hrs | Normal |
| **Tamoy Smith** | **90.90 hrs** | ⚠️ **< 100 hrs (Highlighted in Red)** |

---

## 🚀 How to Run

### Method 1: Run the Single Executable (Recommended)
You do not need .NET or any SDK installed. Simply double-click or run from command line:
```cmd
"Vasanthakumar_C# Assignment.exe"
```

### Method 2: Run from Source Code (.NET CLI)
```bash
dotnet run
```

When run, the application will fetch the latest entries from the API and generate both `EmployeeWorkingHours.html` and `EmployeeWorkingHoursPieChart.png` in the directory.

---

## 📁 Repository Files

* `Vasanthakumar_C# Assignment.exe` — Single, standalone executable.
* `Program.cs` — Fetches API data, calculates hours, and generates the HTML report.
* `Piechart.cs` — Creates the pie chart image using SkiaSharp.
* `EmployeeWorkingHours.html` — Generated HTML table report.
* `EmployeeWorkingHoursPieChart.png` — Generated pie chart image.
* `EmployeeWorkingHours.csproj` — C# project file with package dependencies.
