using System;
using System.Collections;
using System.Collections.Generic;

namespace PhoneBookTestApp
{
    public class PhoneBook : IPhoneBook, IEnumerable<Person>
    {
        private Dictionary<string, Person> people = new Dictionary<string, Person>();

        public void AddPerson(Person newPerson) {

            if(newPerson == null) {
                throw new ArgumentNullException(nameof(newPerson));
            }

            if (people.ContainsKey(newPerson.Name)) {
                throw new ArgumentException("A person with this name already exists in the phone book.");
            }

            people.Add(newPerson.Name, newPerson);
        }

        public Person FindPerson(string firstName, string lastName)
        {
            if(string.IsNullOrEmpty(firstName)) {
                throw new ArgumentNullException(nameof(firstName));
            }

            if(string.IsNullOrEmpty(lastName)) {
                throw new ArgumentNullException(nameof(lastName));
            }

            string name = $"{firstName} {lastName}";

            return people.ContainsKey(name) ? people[name] : null;
        }

        public IEnumerator<Person> GetEnumerator()
        {
            foreach (var person in people) {
                yield return person.Value;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}