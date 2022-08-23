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
    public partial class frmTasks : Form
    {
        public frmTasks()
        {
            InitializeComponent();
        }

        public int ProjectId { get; set; }
        EmployeeManager employeeManager = new EmployeeManager();
        TaskItemManager taskManager = new TaskItemManager();
        private void LoadTasks()
        {
            lstTasks.DataSource = null;
            lstTasks.DataSource = taskManager.ListByProjectId(ProjectId);
        }
        private void LoadStates()
        {
            List<KeyValuePair<int, string>> states = new List<KeyValuePair<int, string>>();
            states.Add(new KeyValuePair<int, string>((int)TaskState.None, "Belirsiz"));
            states.Add(new KeyValuePair<int, string>((int)TaskState.Todo, "Yapılacak"));
            states.Add(new KeyValuePair<int, string>((int)TaskState.InProgress, "Yapılıyor"));
            states.Add(new KeyValuePair<int, string>((int)TaskState.Done, "Tamamlandı"));

            cmbStates.DataSource = null;
            cmbStates.DataSource = states;
            cmbStates.DisplayMember = "Value";
            cmbStates.ValueMember = "Key";
        }
        private void LoadEmployees()
        {
            cmbEmployees.DataSource = null;
            cmbEmployees.DataSource = employeeManager.List();
            cmbEmployees.DisplayMember = nameof(Employee.NameSurname);
            cmbEmployees.ValueMember = nameof(Employee.Id);
        }
        private void frmTasks_Load(object sender, EventArgs e)
        {
            LoadEmployees();
            LoadStates();
            LoadTasks();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            TaskItem task = new TaskItem()
            {
                Summary = txtSummary.Text,
                Description = txtDesc.Text,
                DueDate = dtpDueDate.Checked ? dtpDueDate.Value : null,
                EmployeeId = (cmbEmployees.SelectedItem as Employee).Id,
                State = ((KeyValuePair<int,string>)cmbStates.SelectedItem).Key,
                ProjectId = ProjectId
            };
            try
            {
                if (taskManager.Add(task) == true)
                {
                    LoadTasks();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (lstTasks.SelectedIndex > -1)
            {
                TaskItem task = lstTasks.SelectedItem as TaskItem;
                task.Description = txtDesc.Text;
                task.Summary = txtSummary.Text;
                task.DueDate = dtpDueDate.Checked ? dtpDueDate.Value : null;
                task.EmployeeId = (cmbEmployees.SelectedItem as Employee).Id;
                task.State = ((KeyValuePair<int,string>)cmbStates.SelectedItem).Key;
                task.ProjectId = ProjectId;


                try
                {
                    if (taskManager.Edit(task))
                        LoadEmployees();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

            if (lstTasks.SelectedIndex > -1)
            {
                TaskItem task = lstTasks.SelectedItem as TaskItem;
                DialogResult dialogResult = MessageBox.Show($"{txtSummary.Text} isimli görevi silmek istediğinize emin misiniz?", "Görev Silme", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dialogResult == DialogResult.Yes)
                {
                    if (taskManager.Delete(task.Id))
                        LoadTasks();
                }
            }
        }

        private void lstTasks_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstTasks.SelectedIndex > -1)
            {
                TaskItem task = lstTasks.SelectedItem as TaskItem;
                txtSummary.Text = task.Summary;
                txtDesc.Text = task.Description;
                dtpDueDate.Value = task.DueDate?.Date??DateTime.Now;
                dtpDueDate.Checked = task.DueDate != null;
                cmbEmployees.SelectedValue = task.EmployeeId;
                cmbStates.SelectedValue = task.State;
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadTasks();
        }
    }
}
