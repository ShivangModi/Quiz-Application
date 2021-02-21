using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace QuizService
{
    [DataContract]
    public class Exam
    {
        int examid;
        List<string> questions;
        DateTime dueTime;
        string teacher;

        [DataMember]
        public int Id
        {
            get { return examid; }
            set { examid = value; }
        }

        [DataMember]
        public List<string> Questions
        {
            get { return questions; }
            set { questions = value; }
        }

        [DataMember]
        public DateTime DueTime
        {
            get { return dueTime; }
            set { dueTime = value; }
        }

        [DataMember]
        public string Teacher
        {
            get { return teacher; }
            set { teacher = value; }
        }
    }

    [ServiceContract]
    public interface ExamService
    {
        [OperationContract]
        bool AddExam(Exam exam);

        [OperationContract]
        bool DeleteExam(int id);

        [OperationContract]
        Exam GetExam(int examid);

        [OperationContract]
        bool PostExam(int examid);

        [OperationContract]
        List<Exam> GetAllPostedExam(string teacher);

        [OperationContract]
        List<Exam> GetAllExam(string teacher);
    }
}