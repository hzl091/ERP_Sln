using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ERP.Common;

namespace ERP.App
{
    public partial class EditTab_Frm : Form
    {
        public EditTab_Frm()
        {
            InitializeComponent();
        }

        private void EditTab_Frm_Load(object sender, EventArgs e)
        {
            ERP.DAL.ERPRepository repository = new DAL.ERPRepository();
            var tableInfos = repository.GetTables();
            var tabNames = from p in tableInfos
                           select new {id = p.Id, name = p.Name };
            foreach (var item in tabNames)
	        {
                var node = this.treeView1.Nodes.Add(item.name);
                node.Tag = item.id;
	        }
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            this.LoadColumnInfo(e.Node.Text);
        }

        private void LoadColumnInfo(string tabName)
        {
            ERP.DAL.ERPRepository repository = new DAL.ERPRepository();
            var tableInfos = repository.GetTables(tabName);
            this.dataGridView1.DataSource = tableInfos.FirstOrDefault().ColumnInfos.ToDataTable();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string tabName = this.treeView1.SelectedNode.Text;
            ERP.DAL.ERPRepository repository = new DAL.ERPRepository();
            var colInfo = new Domain.ColumnInfo
            {
                Name = txt_name.Text,
                Type = txt_type.Text,
                IsSystem = txt_issystem.Text == "0" ? false : true,
                Sort = Convert.ToInt32(txt_sort.Text),
                Desc = txt_desc.Text,
                TableInfoId = Convert.ToInt32(treeView1.SelectedNode.Tag)
            };

            repository.AddColToTabel(tabName, colInfo);
            repository.AddColToTableInfo(tabName, colInfo);
            this.LoadColumnInfo(tabName);
        }
    }
}
