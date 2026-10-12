namespace phonebook;

using System.Linq;

public static class ContactRepository
{
    public static void CreateContact(string fullName, long phoneNumber)
    {
        using (var db = new AppDbContext())
        {
            var newContact = new Contact(fullName, phoneNumber);
            db.Contacts.Add(newContact);
            db.SaveChanges();
        }
    }

    public static List<Contact> ReadAllContacts()
    {
        using var db = new AppDbContext();
        var contacts = db.Contacts.ToList();
        return contacts;
    }

    public static void UpdateContact(int id, string? fullName, long? phoneNumber)
    {
        using var db = new AppDbContext();
        var contact = db.Contacts.FirstOrDefault(c =>  c.ID == id);
        if (contact is not null)
        {
            if (fullName is not null)
            { 
              contact.FullName = fullName;
              db.SaveChanges();
            }

            if (phoneNumber is not null)
            {
                contact.PhoneNumber = phoneNumber.Value;
                db.SaveChanges();
            }
            
        }
    }

    public static void DeleteContact(int id)
    {
        using var db = new AppDbContext();
        var contact = db.Contacts.FirstOrDefault(c => c.ID == id);
        if (contact is not null)
        {
            db.Contacts.Remove(contact);
            db.SaveChanges();
        }
    }

}
