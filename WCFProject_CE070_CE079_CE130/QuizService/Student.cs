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
    public class Student : User
    {
        string teacher = string.Empty;

        [DataMember]
        public string Teacher
        {
            get { return teacher; }
            set { teacher = value; }
        }
    }

    [ServiceContract]
    public interface StudentService
    {
        [OperationContract]
        List<Student> GetAllData();

        [OperationContract]
        Student FindStudent(string email);

        [OperationContract]
        List<Student> FindStudentByTeacher(string teacher);

        [OperationContract]
        IDictionary<string, string> AddStudent(Student student);

        [OperationContract]
        bool UpdateStudent(string email, Student updatedVal);

        [OperationContract]
        bool DeleteStudent(string email);
    }
}