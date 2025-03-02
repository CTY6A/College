using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Курсач
{
    public partial class FAuthorization : Form
    {
        public int i, Id;

        List<Zveno> list = new List<Zveno>();

        public FAuthorization(List<Zveno> list, int Id)
        {
            InitializeComponent();

            this.list = list;
            this.Id = Id;
        }

        private void ListToGrid()
        {
            list.Clear();
            Zveno cur;
            for (i = 0; i < dataGridView1.RowCount - 1; i++)
            {
                cur.Name = dataGridView1.Rows[i].Cells[0].Value.ToString();
                cur.LastEntrance = dataGridView1.Rows[i].Cells[1].Value.ToString();
                cur.Score = Convert.ToInt32(dataGridView1.Rows[i].Cells[2].Value.ToString());

                list.Add(cur);
            }
        }

        private void dataGridView1_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            dataGridView1.Rows[dataGridView1.RowCount - 2].Cells[1].Value = DateTime.Now.ToString();
            dataGridView1.Rows[dataGridView1.RowCount - 2].Cells[2].Value = "0";
        }

        private void FAuthorization_Load(object sender, EventArgs e)
        {
            foreach (Zveno cur in list)
                dataGridView1.Rows.Add(cur.Name, cur.LastEntrance, cur.Score.ToString());
        }

        private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView1.CurrentRow.Index < dataGridView1.RowCount - 1)
            {
                dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[1].Value = DateTime.Now.ToString();
                dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[2].Value = Convert.ToInt32(Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[2].Value) + Id);
                ListToGrid();
                Hide();
            }
        }
    }
}
