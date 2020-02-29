using System;

namespace PhoneBookTestApp
{
    class Program
    {
        private static PhoneBook phonebook = new PhoneBook();
        static void Main(string[] args)
        {
               /* TODO: create person objects and put them in the PhoneBook and database
                * John Smith, (248) 123-4567, 1234 Sand Hill Dr, Royal Oak, MI
                * Cynthia Smith, (824) 128-8758, 875 Main St, Ann Arbor, MI
                */

            phonebook.AddPerson(new Person("John Smith", "(248) 123-4567", "1234 Sand Hill Dr, Royal Oak, MI"));
            phonebook.AddPerson(new Person("Cynthia Smith", "(824) 128-8758", "875 Main St, Ann Arbor, MI"));


            // TODO: print the phone book out to System.out
            Console.WriteLine("Printing out all entries of phone book \n");
            foreach (var person in phonebook) {
                Console.WriteLine(person);
            }
            Console.WriteLine("\n");


            // TODO: find Cynthia Smith and print out just her entry
            Console.WriteLine("Printing out Cythia's entry \n");
            var cynthia = phonebook.FindPerson("Cynthia", "Smith");
            Console.WriteLine(cynthia);

            try
            {
                DatabaseUtil.initializeDatabase();

                // TODO: insert the new person objects into the database
                DatabaseUtil.InsertPeople(phonebook);

            }
            finally
            {
                DatabaseUtil.CleanUp();
            }

            Console.ReadLine();
        }
    }
}
