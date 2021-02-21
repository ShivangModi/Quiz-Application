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
    public class Teacher
    {
        string name = string.Empty;
        string email = string.Empty;

        [DataMember]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        [DataMember]
        public string Email
        {
            get { return email; }
            set { email = value; }
        }
    }

    [ServiceContract]
    public interface TeacherService
    {
        [OperationContract]
        List<Teacher> GetAllData();

        [OperationContract]
        Teacher FindTeacher(string email);

        [OperationContract]
        bool AddTeacher(Teacher teacher);

        [OperationContract]
        bool UpdateTeacher(string email, Teacher updatedVal);

        [OperationContract]
        bool deleteTeacher(string email);
    }
}