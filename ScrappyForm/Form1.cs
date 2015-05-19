namespace ScrappyForm
{
    using System;
    using System.Windows.Forms;

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Scrappy2.Program.SortCourse(new Uri(txtCourseUrl.Text), txtNewFolder.Text, txtCurrentFolder.Text);
        }
    }
}
