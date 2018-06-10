using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq;
using ERP.Domain;

namespace ERP.App
{
    public partial class Data_Frm : Form
    {
        public Data_Frm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ERP.DAL.ERPRepository repository = new DAL.ERPRepository();
            var tableInfo = repository.GetTableInfos("pre_customers");

            string s = tableInfo.GetColumnSql();
            var sortColumnInfos = tableInfo.SortColumnInfos;

            var data = repository.GetDataTable(s);
            this.dataGridView1.DataSource = data;
        }
    }
}
