
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class Employee
{
    public string Name { get; set; } = "";
    public double TotalHours { get; set; }
}

public class Program
{
    private static readonly string ApiUrl = "https://rc-vault-fap-live-1.azurewebsites.net/api/gettimeentries?code=vO17RnE8vuzXzPJo5eaLLjXjmRW07law99QTD90zat9FfOQJKKUcgQ==";

    public static async Task Main()
    {
        try
        {
            Console.WriteLine("Fetching employee time entries...");

            List<Employee> employees = await GetEmployeeWorkingHours();

            if (employees.Count == 0)
            {
                Console.WriteLine("No employee records found.");
                return;
            }

            employees = employees
                .OrderByDescending(e => e.TotalHours)
                .ToList();

            GenerateHtml(employees);

            await PieChart.GenerateAsync(employees);

            Console.WriteLine("\nReports generated successfully!");
            Console.WriteLine("HTML: EmployeeWorkingHours.html");
            Console.WriteLine("PNG: EmployeeWorkingHoursPieChart.png");
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine("API request failed: " + ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }

    private static async Task<List<Employee>> GetEmployeeWorkingHours()
    {
        using HttpClient client = new HttpClient();

        using HttpResponseMessage response = await client.GetAsync(ApiUrl);

        response.EnsureSuccessStatusCode();

        string json = await response.Content.ReadAsStringAsync();

        using JsonDocument document = JsonDocument.Parse(json);

        JsonElement entries = document.RootElement;

        // Support APIs that return an array directly or wrap it in an object.
        if (entries.ValueKind == JsonValueKind.Object)
        {
            string[] possibleKeys =
            {
                "data",
                "entries",
                "timeEntries",
                "value"
            };

            bool found = false;

            foreach (string key in possibleKeys)
            {
                if (entries.TryGetProperty(key, out JsonElement property)
                    && property.ValueKind == JsonValueKind.Array)
                {
                    entries = property;
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                throw new Exception("Could not find an employee entries array in the API response.");
            }
        }

        if (entries.ValueKind != JsonValueKind.Array)
        {
            throw new Exception("The API response is not a valid JSON array.");
        }

        Dictionary<string, double> employeeHours =
            new Dictionary<string, double>(StringComparer.OrdinalIgnoreCase);

        foreach (JsonElement entry in entries.EnumerateArray())
        {
            string employeeName =
                GetString(entry, "EmployeeName");

            string startTime =
                GetString(entry, "StarTimeUtc");

            if (string.IsNullOrWhiteSpace(startTime))
            {
                startTime = GetString(entry, "StartTimeUtc");
            }

            string endTime =
                GetString(entry, "EndTimeUtc");

            if (string.IsNullOrWhiteSpace(employeeName)
                || string.IsNullOrWhiteSpace(startTime)
                || string.IsNullOrWhiteSpace(endTime))
            {
                continue;
            }

            if (!DateTimeOffset.TryParse(
                startTime,
                CultureInfo.InvariantCulture,
                DateTimeStyles.AssumeUniversal,
                out DateTimeOffset start))
            {
                continue;
            }

            if (!DateTimeOffset.TryParse(
                endTime,
                CultureInfo.InvariantCulture,
                DateTimeStyles.AssumeUniversal,
                out DateTimeOffset end))
            {
                continue;
            }

            double hours = (end - start).TotalHours;

            if (hours <= 0)
            {
                continue;
            }

            if (!employeeHours.ContainsKey(employeeName))
            {
                employeeHours[employeeName] = 0;
            }

            employeeHours[employeeName] += hours;
        }

        return employeeHours
            .Select(item => new Employee
            {
                Name = item.Key,
                TotalHours = item.Value
            })
            .ToList();
    }

    private static string GetString(JsonElement element, string propertyName)
    {
        if (element.TryGetProperty(propertyName, out JsonElement value))
        {
            return value.ValueKind == JsonValueKind.String
                ? value.GetString() ?? ""
                : value.ToString();
        }

        return "";
    }

    private static void GenerateHtml(List<Employee> employees)
    {
        StringBuilder html = new StringBuilder();

        html.AppendLine("<!DOCTYPE html>");
        html.AppendLine("<html>");
        html.AppendLine("<head>");
        html.AppendLine("<meta charset='UTF-8'>");
        html.AppendLine("<title>Employee Working Hours</title>");

        html.AppendLine("<style>");
        html.AppendLine("body { font-family: Arial; margin: 40px; background: #f5f7fa; }");
        html.AppendLine("h1 { color: #003B8E; }");
        html.AppendLine("table { width: 700px; border-collapse: collapse; background: white; }");
        html.AppendLine("th, td { padding: 14px; border: 1px solid #ddd; text-align: left; }");
        html.AppendLine("th { background: #005CC8; color: white; }");
        html.AppendLine(".low-hours { background: #ffe0e0; color: #b00020; }");
        html.AppendLine("tr:nth-child(even):not(.low-hours) { background: #f2f6fc; }");
        html.AppendLine("</style>");

        html.AppendLine("</head>");
        html.AppendLine("<body>");

        html.AppendLine("<h1>Employee Working Hours Report</h1>");
        html.AppendLine("<table>");

        html.AppendLine("<tr>");
        html.AppendLine("<th>Employee Name</th>");
        html.AppendLine("<th>Total Hours Worked</th>");
        html.AppendLine("</tr>");

        foreach (Employee employee in employees)
        {
            string rowClass = employee.TotalHours < 100
                ? "class='low-hours'"
                : "";

            html.AppendLine($"<tr {rowClass}>");
            html.AppendLine($"<td>{System.Net.WebUtility.HtmlEncode(employee.Name)}</td>");
            html.AppendLine($"<td>{employee.TotalHours:F2}</td>");
            html.AppendLine("</tr>");
        }

        html.AppendLine("</table>");
        html.AppendLine("</body>");
        html.AppendLine("</html>");

        System.IO.File.WriteAllText(
            "EmployeeWorkingHours.html",
            html.ToString()
        );
    }
}
