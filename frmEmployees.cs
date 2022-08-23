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
    public partial class frmEmployees : Form
    {
        public frmEmployees()
        {
            InitializeComponent();
        }

        EmployeeManager employeeManager = new EmployeeManager();
        private void frmEmployees_Load(object sender, EventArgs e)
        {
            LoadEmployees();
        }

        private void LoadEmployees()
        {
            //List<Employee> employees = employeeManager.List();
            lstEmployees.DataSource = null;
            lstEmployees.DataSource = employeeManager.List();
        }

        private void lstEmployees_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstEmployees.SelectedIndex > -1)
            {
                Employee employee = lstEmployees.SelectedItem as Employee;
                txtNameSurname.Text = employee.NameSurname;
                txtEmail.Text = employee.Email;
                txtPhone.Text = employee.Phone;
            }

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadEmployees();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            Employee employee = new Employee()
            {
                NameSurname = txtNameSurname.Text,
                Email = txtEmail.Text,
                Phone = txtPhone.Text
            };
            try
            {
                if (employeeManager.Add(employee) == true)
                {
                    LoadEmployees();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (lstEmployees.SelectedIndex > -1)
            {
                Employee employee = lstEmployees.SelectedItem as Employee;
                employee.NameSurname = txtNameSurname.Text;
                employee.Email = txtEmail.Text;
                employee.Phone = txtPhone.Text;

                try
                {
                    if (employeeManager.Edit(employee))
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
            if (lstEmployees.SelectedIndex > -1)
            {
                Employee employee = lstEmployees.SelectedItem as Employee;
                DialogResult dialogResult = MessageBox.Show($"{employee.NameSurname} isimli çalışanı silmek istediğinize emin misiniz?", "Çalışan Silme", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dialogResult == DialogResult.Yes)
                {
                    if (employeeManager.Delete(employee.Id))
                        LoadEmployees();
                }

            }
        }
    }
}
