using CSharpEgitimKampi601.Entities;
using CSharpEgitimKampi601.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharpEgitimKampi601
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        CustomerOperations cusomerOperations = new CustomerOperations();
        private void btnCustomerCreate_Click(object sender, EventArgs e)
        {
            var customer = new Customer()
            {
                CustomerName = txtCustomerName.Text,
                CustomerSurname = txtCustomerSurname.Text,
                CustomerBalance =decimal.Parse(txtCustomerBalance.Text),
                CustomerCity=txtCustomerCity.Text,
                CustomerShoppingCount=int.Parse(txtCustomerShoppingCount.Text),
            };

            cusomerOperations.AddCustomer(customer);
            MessageBox.Show("MÜŞTERİ EKLEME İŞLEMİ BAŞARILI","UYARI",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void btnCustomerList_Click(object sender, EventArgs e)
        {
            List<Customer> customers = cusomerOperations.GetAllCustomer();
            dataGridView1.DataSource = customers;

        }

        private void btnCustomerDelete_Click(object sender, EventArgs e)
        {
            string customerId =txtCustomerId.Text;
            cusomerOperations.DeleteCustomer(customerId);
            MessageBox.Show("MÜŞTERİ BAŞARIYLA SİLİNDİ.");
        }

        private void btnCustomerUpdate_Click(object sender, EventArgs e)
        {
            string id = txtCustomerId.Text;
            var updateCustomer = new Customer()
            {
                CustomerName = txtCustomerName.Text,
                CustomerBalance = decimal.Parse(txtCustomerBalance.Text),
                CustomerCity = txtCustomerCity.Text,
                CustomerShoppingCount = int.Parse(txtCustomerShoppingCount.Text),
                CustomerSurname = txtCustomerSurname.Text,
                CustomerId = id
            };
            cusomerOperations.UpdateCustomer(updateCustomer);
            MessageBox.Show("MÜŞTERİ BAŞARIYLA GÜNCELLENDİ.");
        }

        private void btnGetByCustomerId_Click(object sender, EventArgs e)
        {
            string id =txtCustomerId.Text;
            Customer customers = cusomerOperations.GetCustomerById(id);
            dataGridView1.DataSource = new List<Customer> { customers};
        }
    }
}
