namespace Proje_ve_Görev_Yönetim_Uygulaması___Dapper
{
    public partial class frmAnasayfa : Form
    {
        public frmAnasayfa()
        {
            InitializeComponent();
        }

        private void mnuEmployeesForm_Click(object sender, EventArgs e)
        {
            frmEmployees frm = new frmEmployees();
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.Show();
        }

        private void mnuProjectsForm_Click(object sender, EventArgs e)
        {
            frmProjects frm = new frmProjects();
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.Show();
        }

        private void frmAnasayfa_Load(object sender, EventArgs e)
        {

        }

        private void mnuCikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}