using Proje_ve_Görev_Yönetim_Uygulaması___Dapper.Managers;
using Proje_ve_Görev_Yönetim_Uygulaması___Dapper.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proje_ve_Görev_Yönetim_Uygulaması___Dapper
{
    public partial class frmProjects : Form
    {
        public frmProjects()
        {
            InitializeComponent();
        }

        ProjectManager projectmanager = new ProjectManager();
        private void LoadProjects()
        {
            lstProjects.DataSource = null;
            lstProjects.DataSource = projectmanager.List();
        }
        private void frmProjects_Load(object sender, EventArgs e)
        {
            LoadProjects();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadProjects();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            Project project = new Project()
            {
                Name = txtName.Text,
                Description = txtDesc.Text,
                StartDate = dtpStart.Checked ? dtpStart.Value : null,
                FinishDate = dtpFinish.Checked ? dtpFinish.Value : null,
            };
            try
            {
                if (projectmanager.Add(project) == true)
                {
                    LoadProjects();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (lstProjects.SelectedIndex > -1)
            {
                Project project = lstProjects.SelectedItem as Project;
                project.Name = txtName.Text;
                project.Description = txtDesc.Text;
                project.StartDate = dtpStart.Checked ? dtpStart.Value : null;
                project.FinishDate = dtpFinish.Checked ? dtpFinish.Value : null;

                try
                {
                    if (projectmanager.Edit(project))
                        LoadProjects();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void lstProjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstProjects.SelectedIndex > -1)
            {
                Project project = lstProjects.SelectedItem as Project;
                txtName.Text = project.Name;
                txtDesc.Text = project.Description;
                dtpStart.Value = project.StartDate?.Date ?? DateTime.Now;
                dtpStart.Checked = project.StartDate != null;
                dtpFinish.Value = project.FinishDate?.Date ?? DateTime.Now;
                dtpFinish.Checked = project.FinishDate != null;
            }
        }

        private void lstProjects_DoubleClick(object sender, EventArgs e)
        {
            if (lstProjects.SelectedIndex > -1)
            {
                Project project = lstProjects.SelectedItem as Project;
                frmTasks frm = new frmTasks();
                frm.Text = $"{project.Name} Görevleri";
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ProjectId = project.Id;
                frm.Show();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lstProjects.SelectedIndex > -1)
            {
                Project project = lstProjects.SelectedItem as Project;
                DialogResult dialogResult = MessageBox.Show($"{project.Name} isimli çalışanı silmek istediğinize emin misiniz?", "Çalışan Silme", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dialogResult == DialogResult.Yes)
                {
                    if (projectmanager.Delete(project.Id))
                        LoadProjects();
                }

            }
        }
    }
}
