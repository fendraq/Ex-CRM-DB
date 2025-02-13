using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server.Models;

public enum CaseStatus
{
    Unopened,
    Opened,
    Closed
}

public enum CaseCategory
{
    Shipping,
    Payment,
    Product,
    Other
}
public class Case
{
    public int id { get; set; }
    public CaseStatus status { get; set; } 
    public CaseCategory category { get; set; }
    public string? title { get; set; }
    public string? customer_first_name { get; set; }
    public string? customer_last_name { get; set; }
    public string? customer_email { get; set; }
    public DateTime? case_opened { get; set; }
    public DateTime? case_closed { get; set; }
    public int? case_handler { get; set; }

    public Case(
        int id,
        CaseStatus status,
        CaseCategory category,
        string title,
        string customerFirstName,
        string customerLastName,
        string customerEmail,
        DateTime? caseOpened,
        DateTime? caseClosed,
        int? caseHandler)
    {
        this.id = id;
        this.status = status;
        this.category = category;
        this.title = title;
        customer_first_name = customerFirstName;
        customer_last_name = customerLastName;
        customer_email = customerEmail;
        case_opened = caseOpened;
        case_closed = caseClosed;
        case_handler = caseHandler;
    }
}