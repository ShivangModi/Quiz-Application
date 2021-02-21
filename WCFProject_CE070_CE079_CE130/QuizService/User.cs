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
    public class User
    {
        string username = string.Empty;
        string email = string.Empty;
        string password = string.Empty;
        string role;

        [DataMember]
        public string Name
        {
            get { return username; }
            set { username = value; }
        }

        [DataMember]
        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        [DataMember]
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        [DataMember]
        public string Role
        {
            get { return role; }
            set { role = value; }
        }
    }

    [ServiceContract]
    public interface UserService
    {
        [OperationContract]
        List<User> Login(User user);

        [OperationContract]
        bool Register(User user);
    }
}