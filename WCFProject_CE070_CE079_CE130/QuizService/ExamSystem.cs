using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizService
{
    public class ExamSystem : ExamService
    {
        public SqlConnection GetConnection()
        {
            SqlConnection con = new SqlConnection("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = QuizApp; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False; MultipleActiveResultSets=true");
            return con;
        }

        public int getLen()
        {
            SqlConnection con = GetConnection();
            con.Open();

            string query = "Select COUNT(id) from Exam";
            SqlCommand cmd = new SqlCommand(query, con);
            int cnt = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();

            return cnt;
        }

        public int getExLen()
        {
            SqlConnection con = GetConnection();
            con.Open();

            string q = "Select count(examId) from Exam";
            SqlCommand c = new SqlCommand(q, con);
            int re = Convert.ToInt32(c.ExecuteScalar());

            if (re != 0)
            {
                string query = "Select MAX(examId) from Exam";
                SqlCommand cmd = new SqlCommand(query, con);
                var res = cmd.ExecuteScalar();
                int cnt = Convert.ToInt32(res);
                con.Close();

                return cnt;
            }

            return 0;
        }

        public bool AddExam(Exam exam)
        {
            bool result = true;
            SqlConnection con = GetConnection();
            con.Open();

            int examid = getExLen() + 1;
            foreach (string q in exam.Questions)
            {
                int id = getLen() + 1;
                string query = "Insert into Exam values(@id, @question, @time, @teacher, @examid)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@question", q);
                cmd.Parameters.AddWithValue("@time", exam.DueTime);
                cmd.Parameters.AddWithValue("@teacher", exam.Teacher);
                cmd.Parameters.AddWithValue("@examid", examid);

                int res = cmd.ExecuteNonQuery();
                if (res > 0)
                {
                    result = result & true;
                }
                else
                {
                    result = false;
                }
            }

            con.Close();
            return result;
        }

        public bool DeleteExam(int id)
        {
            SqlConnection con = GetConnection();
            con.Open();

            string query = "delete from Exam where examId=@id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", id);
            int res = cmd.ExecuteNonQuery();

            query = "delete from PostExam where examId=@id";
            cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", id);
            res += cmd.ExecuteNonQuery();
            con.Close();

            if (res > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Exam GetExam(int examid)
        {
            SqlConnection con = GetConnection();
            con.Open();

            string query = "select * from Exam where examId=@e";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@e", examid);
            SqlDataReader rd = cmd.ExecuteReader();

            Exam ex = new Exam();
            List<string> ques = new List<string>();
            int id = 0, exam_id = 0;
            string question, Teacher = string.Empty;
            DateTime d = new DateTime();

            while (rd.Read())
            {
                id = rd.GetInt32(0);
                question = rd[1].ToString();
                Teacher = rd[3].ToString();
                d = rd.GetDateTime(2);
                exam_id = rd.GetInt32(4);
                ques.Add(question);
            }

            ex.Questions = ques;
            ex.Id = exam_id;
            ex.DueTime = d;
            ex.Teacher = Teacher;
            return ex;
        }

        public bool PostExam(int examid)
        {
            Exam exam = GetExam(examid);
            bool result = true;
            SqlConnection con = GetConnection();
            con.Open();
            foreach (string q in exam.Questions)
            {
                string qry = "Select COUNT(id) from PostExam";
                SqlCommand c = new SqlCommand(qry, con);
                int rs = Convert.ToInt32(c.ExecuteScalar());

                string query = "Insert into PostExam values(@id, @question, @time, @teacher, @examid)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", rs + 1);
                cmd.Parameters.AddWithValue("@question", q);
                cmd.Parameters.AddWithValue("@time", exam.DueTime);
                cmd.Parameters.AddWithValue("@teacher", exam.Teacher);
                cmd.Parameters.AddWithValue("@examid", exam.Id);

                int res = cmd.ExecuteNonQuery();
                if (res > 0)
                {
                    result = result & true;
                }
                else
                {
                    result = false;
                }
            }

            con.Close();
            return result;
        }

        public List<Exam> GetAllPostedExam(string teacher)
        {
            List<Exam> exams = new List<Exam>();
            SqlConnection con = GetConnection();
            con.Open();

            string query = "select distinct examId from PostExam where Teacher=@t";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@t", teacher);

            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                int exid = rd.GetInt32(0);
                Exam e = GetExam(exid);
                exams.Add(e);
            }

            con.Close();
            return exams;
        }

        public List<Exam> GetAllExam(string teacher)
        {
            List<Exam> exams = new List<Exam>();
            SqlConnection con = GetConnection();
            con.Open();

            string query = "select distinct examId from Exam where teacher=@t";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@t", teacher);

            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                int exid = rd.GetInt32(0);
                Exam e = GetExam(exid);
                exams.Add(e);
            }

            con.Close();
            return exams;
        }
    }
}