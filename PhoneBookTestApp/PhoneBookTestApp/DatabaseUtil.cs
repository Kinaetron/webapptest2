﻿using System;
using System.Data.SQLite;
using System.Collections.Generic;

namespace PhoneBookTestApp
{
    public class DatabaseUtil
    {
        public static void initializeDatabase()
        {
            using (var  dbConnection = GetConnection())
            {
                SQLiteCommand command =
                   new SQLiteCommand(
                       "create table PHONEBOOK (NAME varchar(255), PHONENUMBER varchar(255), ADDRESS varchar(255))",
                       dbConnection);
                command.ExecuteNonQuery();

                command =
                    new SQLiteCommand(
                        "INSERT INTO PHONEBOOK (NAME, PHONENUMBER, ADDRESS) VALUES('Chris Johnson','(321) 231-7876', '452 Freeman Drive, Algonac, MI')",
                        dbConnection);
                command.ExecuteNonQuery();

                command =
                    new SQLiteCommand(
                        "INSERT INTO PHONEBOOK (NAME, PHONENUMBER, ADDRESS) VALUES('Dave Williams','(231) 502-1236', '285 Huron St, Port Austin, MI')",
                        dbConnection);
                command.ExecuteNonQuery();


                command.ExecuteNonQuery();
            }
        }

        public static void InsertPeople(IEnumerable<Person> people)
        {
            var dbConnection = GetConnection();

            foreach (var person in people)
            {
                try
                {
                    var command =
                      new SQLiteCommand(
                          "INSERT INTO PHONEBOOK (NAME, PHONENUMBER, ADDRESS) VALUES('@name', '@phoneNumber', '@address')",
                          dbConnection);

                    command.Parameters.AddWithValue("@firstname", person.Name);
                    command.Parameters.AddWithValue("@phoneNumber", person.PhoneNumber);
                    command.Parameters.AddWithValue("@address", person.Address);

                    command.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw;
                }
            }

            dbConnection.Close();
        }

        public static SQLiteConnection GetConnection()
        {
            var dbConnection = new SQLiteConnection("Data Source= MyDatabase.sqlite;Version=3;");
            dbConnection.Open();

            return dbConnection;
        }

        public static void CleanUp()
        {
            var dbConnection = new SQLiteConnection("Data Source= MyDatabase.sqlite;Version=3;");
            dbConnection.Open();

            try
            {
                SQLiteCommand command =
                    new SQLiteCommand(
                        "drop table PHONEBOOK",
                        dbConnection);
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                dbConnection.Close();
            }
        }
    }
}