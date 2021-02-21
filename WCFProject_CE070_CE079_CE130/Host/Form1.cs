using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ServiceModel;
using QuizService;

namespace Host
{
    public partial class Form1 : Form
    {
        private ServiceHost _ExamServiceHost;
        private ServiceHost _QuestionServiceHost;
        private ServiceHost _StudentServiceHost;
        private ServiceHost _TeacherServiceHost;
        private ServiceHost _UserServiceHost;

        public Form1()
        {
            InitializeComponent();
            startServices();
        }

        private void startServices()
        {
            btnStart.Enabled = false;
            btnStop.Enabled = true;

            _ExamServiceHost = new ServiceHost(typeof(ExamService));
            _QuestionServiceHost = new ServiceHost(typeof(QuestionService));
            _StudentServiceHost = new ServiceHost(typeof(StudentService));
            _TeacherServiceHost = new ServiceHost(typeof(TeacherService));
            _UserServiceHost = new ServiceHost(typeof(UserService));

            // now start opening connections
            lblMessage.Text = "Following Services\nrunning.";
            listBox1.Items.Clear();

            _ExamServiceHost.Open();
            listBox1.Items.Add("Exam Service running.");

            _QuestionServiceHost.Open();
            listBox1.Items.Add("Question Service running.");

            _StudentServiceHost.Open();
            listBox1.Items.Add("Student Service running.");

            _TeacherServiceHost.Open();
            listBox1.Items.Add("Teacher Service running.");

            _UserServiceHost.Open();
            listBox1.Items.Add("User Service running.");
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            startServices();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            _ExamServiceHost.Close();
            _QuestionServiceHost.Close();
            _StudentServiceHost.Close();
            _TeacherServiceHost.Close();
            _UserServiceHost.Close();

            btnStart.Enabled = true;
            btnStop.Enabled = false;
            lblMessage.Text = "All services stopped.";
            listBox1.Items.Clear();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            _ExamServiceHost.Close();
            _QuestionServiceHost.Close();
            _StudentServiceHost.Close();
            _TeacherServiceHost.Close();
            _UserServiceHost.Close();
        }
    }
}
