using BLL.EntityLists;
using BLL.EntityManagers;

namespace Northwind_WinUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            var prd = (from i in ProductManager.selectAllProducts()
                       where i.UnitsInStock > 5
                       select i).ToList();
            this.dataGridView1.DataSource = prd;
        }
    }
}
