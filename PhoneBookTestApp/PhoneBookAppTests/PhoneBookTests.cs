using FluentAssertions;
using PhoneBookTestApp;
using System;
using System.Linq;
using Xunit;

namespace PhoneBookAppTests
{

    public class PhoneBookTests
    {
        private class Arrangement
        {
            public PhoneBook SUT { get; }

            public Person Person { get; }

            public Arrangement(Person person, PhoneBook phoneBook)
            {
                Person = person;
                SUT = phoneBook;
            }
        }

        private class ArrangementBuilder
        {
            private Person person;
            private PhoneBook phoneBook = new PhoneBook();

            public ArrangementBuilder WithPerson()
            {
                this.person = new Person("first last", "phoneNumber", "address");
                return this;
            }

            public ArrangementBuilder WithPersonAdded()
            {
                this.person = new Person("first last", "phoneNumber", "address");
                phoneBook.AddPerson(person);
                return this;
            }

            public Arrangement Build()
            {
                return new Arrangement(person, phoneBook);
            }
        }

        [Fact]
        public void PhoneBook_Add_WithNullPerson_ShouldThrowOutArguementNullException()
        {
            // Arrange
            var arrangement = new ArrangementBuilder()
                .Build();

            // Act 
            var error = Record.Exception(() => arrangement.SUT.AddPerson(null));

            // Assert
            error.Should().BeOfType<ArgumentNullException>();
            error.Message.Should().Be("Value cannot be null.\r\nParameter name: newPerson");
        }

        [Fact]
        public void PhoneBook_Add_WithSamePersonAlreadyAdded_ShouldThrowOutArguementException()
        {
            // Arrange
            var arrangement = new ArrangementBuilder()
                .WithPersonAdded()
                .Build();

            // Act 
            var error = Record.Exception(() => arrangement.SUT.AddPerson(arrangement.Person));

            // Assert
            error.Should().BeOfType<ArgumentException>();
            error.Message.Should().Be("A person with this name already exists in the phone book.");
        }

        [Fact]
        public void PhoneBook_Add_WithValidPerson_ShouldAdd()
        {
            // Arrange
            var arrangement = new ArrangementBuilder()
                .WithPerson()
                .Build();



            // Act 
            arrangement.SUT.AddPerson(arrangement.Person);

            // Assert
            arrangement.SUT.Count().Should().Be(1);
        }

        [Fact]
        public void PhoneBook_FindPerson_WithNullFirstName_ShouldThrowArgumentNullException()
        {
            var arrangement = new ArrangementBuilder()
               .Build();

            // Act 
            var error = Record.Exception(() => arrangement.SUT.FindPerson(null, "last"));

            // Assert
            error.Should().BeOfType<ArgumentNullException>();
            error.Message.Should().Be("Value cannot be null.\r\nParameter name: firstName");
        }

        [Fact]
        public void PhoneBook_FindPerson_WithNullLastName_ShouldThrowArgumentNullException()
        {
            // Arrange
            var arrangement = new ArrangementBuilder()
              .Build();

            // Act 
            var error = Record.Exception(() => arrangement.SUT.FindPerson("first", null));

            // Assert
            error.Should().BeOfType<ArgumentNullException>();
            error.Message.Should().Be("Value cannot be null.\r\nParameter name: lastName");
        }

        [Fact]
        public void PhoneBook_FindPerson_WithValidDetails_ShoudReturnCorrectPerson()
        {
            // Arrange
            var arrangement = new ArrangementBuilder()
                .WithPersonAdded()
               .Build();

            // Act
            var results = arrangement.SUT.FindPerson("first", "last");

            results.Name.Should().Be(arrangement.Person.Name);
            results.PhoneNumber.Should().Be(arrangement.Person.PhoneNumber);
            results.Address.Should().Be(arrangement.Person.Address);
        }
    }
}
