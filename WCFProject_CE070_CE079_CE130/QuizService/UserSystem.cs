using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizService
{
    public class UserSystem : UserService
    {
        public SqlConnection GetConnection()
        {
            SqlConnection con = new SqlConnection("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = QuizApp; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False");
            return con;
        }

        public List<User> Login(User user)
        {
            List<User> usr = new List<User>();
            SqlConnection con = GetConnection();
            con.Open();

            SqlCommand cmd = new SqlCommand("select * from User where Email=@Email and Password=@Password", con);
            cmd.Parameters.AddWithValue("@Email", user.Email);
            cmd.Parameters.AddWithValue("@Password", user.Password);

            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read() == true)
            {
                User u = new User
                {
                    Email = dr[0].ToString(),
                    Name = dr[1].ToString(),
                    Password = dr[2].ToString(),
                    Role = dr[3].ToString()
                };

                usr.Add(u);
            }

            con.Close();
            return usr;
        }

        public bool Register(User user)
        {
            SqlConnection con = GetConnection();
            con.Open();

            SqlCommand cmd = new SqlCommand("insert into User values(@Email, @Name, @Password, @Role)", con);
            cmd.Parameters.AddWithValue("@Email", user.Email);
            cmd.Parameters.AddWithValue("@Name", user.Name);
            cmd.Parameters.AddWithValue("@Password", user.Password);
            cmd.Parameters.AddWithValue("@Role", user.Role);

            int result = cmd.ExecuteNonQuery();
            con.Close();

            if (result == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}