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
    public class Question
    {
        string que = string.Empty;
        string op1 = string.Empty;
        string op2 = string.Empty;
        string op3 = string.Empty;
        string op4 = string.Empty;
        string ans = string.Empty;

        [DataMember]
        public string QuestionVal
        {
            get { return que; }
            set { que = value; }
        }

        [DataMember]
        public string Option1
        {
            get { return op1; }
            set { op1 = value; }
        }

        [DataMember]
        public string Option2
        {
            get { return op2; }
            set { op2 = value; }
        }

        [DataMember]
        public string Option3
        {
            get { return op3; }
            set { op3 = value; }
        }

        [DataMember]
        public string Option4
        {
            get { return op4; }
            set { op4 = value; }
        }

        [DataMember]
        public string Answer
        {
            get { return ans; }
            set { ans = value; }
        }
    }

    [ServiceContract]
    public interface QuestionService
    {
        [OperationContract]
        List<Question> GetAllQuestions();

        [OperationContract]
        Question FindQuestion(string question);

        [OperationContract]
        bool AddQuestion(Question question);

        [OperationContract]
        bool UpdateQuestion(string question, Question updatedVal);

        [OperationContract]
        bool DeleteQuestion(string question);

        [OperationContract]
        bool IsCorrect(string question, string answer);
    }
}