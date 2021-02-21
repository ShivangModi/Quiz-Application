using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizService
{
    public class StudentSystem : StudentService
    {
        public SqlConnection GetConnection()
        {
            SqlConnection con = new SqlConnection("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = QuizApp; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False");
            return con;
        }

        public IDictionary<string, string> AddStudent(Student student)
        {
            SqlConnection con = GetConnection();
            con.Open();

            string query = "insert into Student values(@Name, @Email, @Password, @Role, @Teacher)";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Name", student.Name);
            cmd.Parameters.AddWithValue("@Email", student.Email);
            cmd.Parameters.AddWithValue("@Password", student.Password);
            cmd.Parameters.AddWithValue("@Role", student.Role);
            cmd.Parameters.AddWithValue("@Teacher", student.Teacher);

            int result = cmd.ExecuteNonQuery();
            con.Close();

            if (result <= 0)
            {
                return null;
            }
            else
            {
                User u = new User
                {
                    Email = student.Email,
                    Name = student.Name,
                    Password = student.Password,
                    Role = "Student"
                };

                UserSystem us = new UserSystem();
                us.Register(u);

                IDictionary<string, string> d = new Dictionary<string, string>();
                d.Add("Email", student.Email);
                d.Add("Password", student.Password);

                return d;
            }
        }

        public bool DeleteStudent(string email)
        {
            SqlConnection con = GetConnection();
            con.Open();

            string query = "delete from Student where Email=@Email";
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

        public Student FindStudent(string email)
        {
            Student s = new Student();
            SqlConnection con = GetConnection();
            con.Open();

            string query = "select * from Student where Email=@Email";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Email", email);

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read() == true)
            {
                s.Name = reader[0].ToString();
                s.Email = reader[1].ToString();
                s.Password = reader[2].ToString();
                s.Role = reader[3].ToString();
                s.Teacher = reader[4].ToString();
            }

            con.Close();
            return s;
        }

        public List<Student> FindStudentByTeacher(string teacher)
        {
            List<Student> students = new List<Student>();
            SqlConnection con = GetConnection();
            con.Open();

            string query = "select * from Student where Teacher=@t";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@t", teacher);

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Student s = new Student
                {
                    Name = reader[0].ToString(),
                    Email = reader[1].ToString(),
                    Password = reader[2].ToString(),
                    Role = reader[3].ToString(),
                    Teacher = reader[4].ToString()
                };

                students.Add(s);
            }

            con.Close();
            return students;
        }

        public List<Student> GetAllData()
        {
            List<Student> students = new List<Student>();
            SqlConnection con = GetConnection();
            con.Open();

            string query = "select * from Student";
            SqlCommand cmd = new SqlCommand(query, con);

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Student s = new Student
                {
                    Name = reader[0].ToString(),
                    Email = reader[1].ToString(),
                    Password = reader[2].ToString(),
                    Role = reader[3].ToString(),
                    Teacher = reader[4].ToString()
                };

                students.Add(s);
            }

            con.Close();
            return students;
        }

        public bool UpdateStudent(string email, Student updatedVal)
        {
            SqlConnection con = GetConnection();
            con.Open();

            string query = "update Student set Name=@Name, Email=@Email, Password=@p, Role=@r, Teacher=@t where Email=@E";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Name", updatedVal.Name);
            cmd.Parameters.AddWithValue("@Email", updatedVal.Email);
            cmd.Parameters.AddWithValue("@p", updatedVal.Password);
            cmd.Parameters.AddWithValue("@r", updatedVal.Role);
            cmd.Parameters.AddWithValue("@t", updatedVal.Teacher);
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