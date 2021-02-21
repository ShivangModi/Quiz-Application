using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizService
{
    public class TeacherSystem : TeacherService
    {
        public SqlConnection GetConnection()
        {
            SqlConnection con = new SqlConnection("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = QuizApp; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False");
            return con;
        }

        public bool AddTeacher(Teacher teacher)
        {
            SqlConnection con = GetConnection();
            con.Open();

            string query = "insert into Teacher values(@Name, @Email)";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Name", teacher.Name);
            cmd.Parameters.AddWithValue("@Email", teacher.Email);

            int result = cmd.ExecuteNonQuery();
            con.Close();

            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool deleteTeacher(string email)
        {
            SqlConnection con = GetConnection();
            con.Open();

            string query = "delete from Teacher where Email=@Email";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Email", email);

            int result = cmd.ExecuteNonQuery();
            con.Close();

            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Teacher FindTeacher(string email)
        {
            Teacher tch = new Teacher();
            SqlConnection con = GetConnection();
            con.Open();

            string query = "select * from Teacher where Email=@Email";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Email", email);

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read() == true)
            {
                tch.Email = reader[0].ToString();
                tch.Name = reader[1].ToString();
            }

            con.Close();
            return tch;
        }

        public List<Teacher> GetAllData()
        {
            List<Teacher> tch = new List<Teacher>();
            SqlConnection con = GetConnection();
            con.Open();

            string query = "select * from Teacher";
            SqlCommand cmd = new SqlCommand(query, con);

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Teacher t = new Teacher
                {
                    Name = reader[0].ToString(),
                    Email = reader[1].ToString()
                };

                tch.Add(t);
            }

            con.Close();
            return tch;
        }

        public bool UpdateTeacher(string email, Teacher updatedVal)
        {
            SqlConnection con = GetConnection();
            con.Open();

            string query = "update Teacher set Name=@Name,Email=@Email where Email=@E";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Name", updatedVal.Name);
            cmd.Parameters.AddWithValue("@Email", updatedVal.Email);
            cmd.Parameters.AddWithValue("@E", email);

            int result = cmd.ExecuteNonQuery();
            con.Close();

            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}