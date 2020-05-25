using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Collections;

namespace ConsoleSQL
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection conn = new SqlConnection("Server=.\\SQLEXPRESS; Database=Shop; Integrated Security=true");
            conn.Open();
            //SqlCommand cmd = new SqlCommand("Select FirstName from Persons Where City = 'Kiev'", conn);
            //SqlDataReader reader = cmd.ExecuteReader();
            //List<String> firstName = new List<string>();
            //string kiev="";
            //while (reader.Read())
            //{
            //    //Console.WriteLine("{0}", reader.GetString(0));
            //    firstName.Add(reader.GetString(0));
            //    kiev += reader.GetString(0) +" ";
            //}
            //reader.Close();
            //conn.Close();

            //for (int i = 0; i < firstName.Count; i++)
            //{
            //    Console.WriteLine(firstName[i]);
            //}
            //Console.WriteLine(kiev);
            //if (Debugger.IsAttached)
            //{
            //    Console.ReadLine();
            //}

            ////////////////////////
            ///
            bool comparisonResult = false;
            string firstSecondName = "";
            List<decimal> orderPrise = new List<decimal>();
            SqlCommand cmd = new SqlCommand("Select Id, FirstName, LastName, Age from Persons", conn);
            SqlDataReader reader = cmd.ExecuteReader();
            Object typeId = new Object();
            Object typeFirstName = new Object();
            Object typeSecondName = new Object();
            Object typeAge = new Object();
            while (reader.Read())
            {
                typeId = reader.GetFieldType(0);
                typeFirstName = reader.GetFieldType(1);
                typeSecondName = reader.GetFieldType(2);
                typeAge = reader.GetFieldType(3);
            }
            Console.WriteLine(typeId);
            Console.WriteLine(typeFirstName);
            Console.WriteLine(typeSecondName);
            Console.WriteLine(typeAge);



            Console.ReadLine();
        }
    }
}
