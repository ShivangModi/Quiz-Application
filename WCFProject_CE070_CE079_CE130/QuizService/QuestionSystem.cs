using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizService
{
    public class QuestionSystem : QuestionService
    {
        public SqlConnection GetConnection()
        {
            SqlConnection con = new SqlConnection("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = QuizApp; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False");
            return con;
        }

        public bool AddQuestion(Question question)
        {
            SqlConnection con = GetConnection();
            con.Open();

            string query = "insert into [Question] values(@que, @op1, @op2, @op3, @op4, @ans)";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@que", question.QuestionVal);
            cmd.Parameters.AddWithValue("@op1", question.Option1);
            cmd.Parameters.AddWithValue("@op2", question.Option2);
            cmd.Parameters.AddWithValue("@op3", question.Option3);
            cmd.Parameters.AddWithValue("@op4", question.Option4);
            cmd.Parameters.AddWithValue("@ans", question.Answer);

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

        public bool DeleteQuestion(string question)
        {
            SqlConnection con = GetConnection();
            con.Open();

            string query = "delete from [Question] where Text=@q";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@q", question);

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

        public Question FindQuestion(string question)
        {
            Question q = new Question();
            SqlConnection con = GetConnection();
            con.Open();

            string query = "select * from [Question] where Text=@q";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@q", question);

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read() == true)
            {
                q.QuestionVal = reader[0].ToString();
                q.Option1 = reader[1].ToString();
                q.Option2 = reader[2].ToString();
                q.Option3 = reader[3].ToString();
                q.Option4 = reader[4].ToString();
                q.Answer = reader[5].ToString();
            }

            con.Close();
            return q;
        }

        public List<Question> GetAllQuestions()
        {
            List<Question> ques = new List<Question>();
            SqlConnection con = GetConnection();
            con.Open();

            string query = "select * from [Question]";
            SqlCommand cmd = new SqlCommand(query, con);

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Question q = new Question
                {
                    QuestionVal = reader[0].ToString(),
                    Option1 = reader[1].ToString(),
                    Option2 = reader[2].ToString(),
                    Option3 = reader[3].ToString(),
                    Option4 = reader[4].ToString(),
                    Answer = reader[5].ToString(),
                };

                ques.Add(q);
            }

            con.Close();
            return ques;
        }

        public bool IsCorrect(string question, string answer)
        {
            Question q = FindQuestion(question);
            if (answer == q.Answer)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdateQuestion(string question, Question updatedVal)
        {
            SqlConnection con = GetConnection();
            string query = "update [Question] set question=@q1, op1=@op1, op2=@op2, op3=@op3, op4=@op4, ans=@a where question=@q";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@q1", updatedVal.QuestionVal);
            cmd.Parameters.AddWithValue("@op1", updatedVal.Option1);
            cmd.Parameters.AddWithValue("@op2", updatedVal.Option2);
            cmd.Parameters.AddWithValue("@op3", updatedVal.Option3);
            cmd.Parameters.AddWithValue("@op4", updatedVal.Option4);
            cmd.Parameters.AddWithValue("@a", updatedVal.Answer);
            cmd.Parameters.AddWithValue("@q", question);
            con.Open();

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