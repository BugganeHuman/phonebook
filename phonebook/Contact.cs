namespace phonebook;

public class Contact
{
    public int ID { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public string FullName { get; set; } = "";
    public long PhoneNumber { get; set; }

    public Contact () { }

    public Contact(string fullName, long phoneNumber)
    {
        FullName = fullName;
        PhoneNumber = phoneNumber;
    }
}
