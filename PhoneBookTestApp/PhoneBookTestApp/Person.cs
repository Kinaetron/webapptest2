using System;

namespace PhoneBookTestApp
{
    public class Person
    {
        public string Name { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Address { get; private set; }

        public Person(string name, string phoneNumber, string address)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            PhoneNumber = phoneNumber ?? throw new ArgumentNullException(nameof(phoneNumber));
            Address = address ?? throw new ArgumentNullException(nameof(address));
        }

        public override string ToString()
        {
            return $"{Name}, {PhoneNumber}, {Address}";
        }
    }
}