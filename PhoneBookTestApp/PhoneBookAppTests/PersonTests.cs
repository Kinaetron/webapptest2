using FluentAssertions;
using PhoneBookTestApp;
using System;
using Xunit;

namespace PhoneBookAppTests
{
    public class PersonTests
    {

        [Fact]
        public void Person_WithNullName_ShouldThrowOutArguementNullException()
        {
            // Act 
            var error = Record.Exception(() => new Person(null, "phoneNumber", "address"));

            // Assert
            error.Should().BeOfType<ArgumentNullException>();
            error.Message.Should().Be("Value cannot be null.\r\nParameter name: name");
        }

        [Fact]
        public void Person_WithPhoneNumber_ShouldThrowOutArguementNullException()
        {
            // Act 
            var error = Record.Exception(() => new Person("first last", null, "address"));

            // Assert
            error.Should().BeOfType<ArgumentNullException>();
            error.Message.Should().Be("Value cannot be null.\r\nParameter name: phoneNumber");
        }

        [Fact]
        public void Person_WithNullAddress_ShouldThrowOutArguementNullException()
        {
            // Act 
            var error = Record.Exception(() => new Person("first last", "phoneNumber", null));

            // Assert
            error.Should().BeOfType<ArgumentNullException>();
            error.Message.Should().Be("Value cannot be null.\r\nParameter name: address");
        }

        [Fact]
        public void Person_WithValidDetails_ShouldCreatePerson()
        {
            // Act 
            var person = new Person("first last", "phoneNumber", "address");

            // Assert
            person.Name.Should().Be("first last");
            person.PhoneNumber.Should().Be("phoneNumber");
            person.Address.Should().Be("address");
        }
    }
}
