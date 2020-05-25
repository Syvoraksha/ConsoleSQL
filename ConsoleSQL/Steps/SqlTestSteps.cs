using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using TechTalk.SpecFlow;

namespace ConsoleSQL.Steps
{
    [Binding]
    public class SqlTestSteps
    {
        SqlConnection conn;
        string kiev = "";
        bool comparisonResult = false;
        string firstSecondName = "";
        Object typeId = new Object();
        Object typeFirstName = new Object();
        Object typeSecondName = new Object();
        Object typeAge = new Object();
        List<decimal> orderPrise = new List<decimal>();
        string primaryKey= "";

        [Given(@"Database connection established")]
        public void GivenDatabaseConnectionEstablished()
        {
            conn = new SqlConnection("Server=.\\SQLEXPRESS; Database=Shop; Integrated Security=true");
            conn.Open();
        }
        
        [When(@"I choose Kyiv")]
        public void WhenIChooseKyiv()
        {
            SqlCommand cmd = new SqlCommand("Select FirstName, City from Persons Where City = 'Kiev'", conn);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                kiev += reader.GetString(0) + "-" + reader.GetString(1) + "/";
            }
        }
        
        [Then(@"people from Kyiv are selected from the database")]
        public void ThenPeopleFromKyivAreSelectedFromTheDatabase()
        {
            string expRes = "Tatyana-Kiev/Sergey-Kiev/Dina-Kiev/Alexey-Kiev/Victor-Kiev/Victoria-Kiev/Vladislav-Kiev/Mikhail-Kiev/";
            Assert.AreEqual(expRes, kiev);
        }

        [When(@"I choose  FirstName, SecondName and Order")]
        public void WhenIChooseFirstNameSecondNameAndOrder()
        {
            SqlCommand cmd = new SqlCommand("Select FirstName, LastName, Orders.OrderPrice from dbo.Persons Join Orders on Persons.Id = Orders.Id_person Where OrderPrice >6000  ", conn);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                firstSecondName += reader.GetString(0) + "-" + reader.GetString(1) + "/";
                orderPrise.Add(reader.GetDecimal(2));               
            }
        }

        [When(@"I Check that the order is more than (.*)")]
        public void WhenICheckThatTheOrderIsMoreThan(int order)
        {
            int count_ = 0;
            for (int i = 0; i < orderPrise.Count; i++)
            {
                if (orderPrise[i] > order)
                {
                    count_++;
                }
            }

            if (count_ == orderPrise.Count)
            {
                comparisonResult = true;
            }
        }

        [Then(@"people whose order is more than (.*) are selected from the database")]
        public void ThenPeopleWhoseOrderIsMoreThanAreSelectedFromTheDatabase(int p0)
        {
            if (comparisonResult)
            {
                comparisonResult = false;
                string expRes = "Angelica-Chichircoza/Ilya-Novikov/Irina-Nikolaenko/Anna-Sariboga/Alexey-Melnik/Olga-Astakhova/Vladislav-Kolesnik/";
                Assert.AreEqual(expRes, firstSecondName);
            }
        }

        [When(@"I select (.*) from table Persons")]
        public void WhenISelectIdFromTablePersons(string field)
        {
            SqlCommand cmd = new SqlCommand("Select Id, FirstName, LastName, Age from Persons", conn);
            SqlDataReader reader = cmd.ExecuteReader();
            //var typeId = new Object();
            //var typeFirstName = new Object();
            //var typeSecondName = new Object();
            //var typeAge = new Object();
            while (reader.Read())
            {
                typeId = reader.GetFieldType(0);
                typeFirstName = reader.GetFieldType(1);
                typeSecondName = reader.GetFieldType(2);
                typeAge = reader.GetFieldType(3);
            }
        }

        [Then(@"I get a variable (.*) of (.*)")]
        public void ThenIGetAVariableSystem_IntOfId(string typeTable, string field)
        {
            if (field == "Age")
            {
                Assert.AreEqual(typeTable, typeAge.ToString());
            }
            if (field == "FirstName")
            {
                Assert.AreEqual(typeTable, typeFirstName.ToString());
            }
            if (field == "LastName")
            {
                Assert.AreEqual(typeTable, typeSecondName.ToString());
            }
            if (field == "id")
            {
                Assert.AreEqual(typeTable, typeId.ToString());
            }

        }

        [When(@"I make a request for a field with a primary key")]
        public void WhenIMakeARequestForAFieldWithAPrimaryKey()
        {
            SqlCommand cmd = new SqlCommand("SELECT column_name FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE WHERE OBJECTPROPERTY(OBJECT_ID(constraint_name), 'IsPrimaryKey') = 1 AND table_name = 'Persons'", conn);
            SqlDataReader reader = cmd.ExecuteReader();

             while (reader.Read())
             {
                primaryKey = reader.GetString(0);
             }
        }

        [Then(@"I get a field with a primary key")]
        public void ThenIGetAFieldWithAPrimaryKey()
        {
            Assert.AreEqual("Id", primaryKey);
        }

    }
}
