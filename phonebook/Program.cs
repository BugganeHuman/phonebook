using phonebook;
using Spectre.Console;

var db = new AppDbContext();
db.Database.EnsureCreated();
db.Dispose();

Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.InputEncoding = System.Text.Encoding.UTF8;

Console.WriteLine("\nWelcome to Phonebook\n");

while (true)
{
    string choice = AnsiConsole.Prompt(
        new SelectionPrompt<string>()
        .HighlightStyle(new Style(Color.GreenYellow))
        .AddChoices("\n  Read All", "Add new", "Update", "Delete", "Escape")
        );
    if (choice == "Escape") { break; }

    else if (choice == "\n  Read All")
    {
        List<Contact> allContacts = ContactRepository.ReadAllContacts();
        var table = new Table();
        table.ShowRowSeparators();
        table.AddColumns("ID", "Created At", "Full Name", "Phone number");
        foreach (Contact contact in allContacts)
        {
            table.AddRow(contact.ID.ToString(), contact.CreatedAt.ToString("dd.MM.yyyy"),
                         contact.FullName, contact.PhoneNumber.ToString());
        }
        AnsiConsole.Write(table);
    }

    else if (choice == "Add new")
    {
        string fullName = AnsiConsole.Ask<string>("Fullname: ");
        long phoneNumber = AnsiConsole.Ask<long>("Phone Number: ");
        ContactRepository.CreateContact(fullName, phoneNumber);
        AnsiConsole.MarkupLine("\n[green]DONE[/]\n");
    }

    else if (choice == "Update")
    {
        int id = AnsiConsole.Ask<int>("ID of contact, or Enter to skip: "); 
        string? fullName = AnsiConsole.Prompt(
                    new TextPrompt<string?>("Full Name to update, or Enter to skip: ")
                    .DefaultValue(null)
                    .HideDefaultValue());
        long? phoneNumber = AnsiConsole.Prompt(
            new TextPrompt<long?>("Phone number, or Enter to skip: ")
                     .DefaultValue(null)
                    .HideDefaultValue());
        ContactRepository.UpdateContact(id, fullName, phoneNumber);
        AnsiConsole.MarkupLine("\n[green]DONE[/]\n");
    }

    else if (choice == "Delete")
    {
        int id = AnsiConsole.Ask<int>("ID of contact, or Enter to skip: ");
        bool isSure = AnsiConsole.Confirm("Are you sure?", false);
        if (isSure)
        {
            ContactRepository.DeleteContact(id);
            AnsiConsole.MarkupLine("\n[green]DONE[/]\n");
        }
        else
        {
            AnsiConsole.MarkupLine("\n[red]REJECTED[/]\n");
        }
    }

}