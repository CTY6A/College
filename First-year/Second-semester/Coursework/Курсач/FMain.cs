using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;

namespace Курсач
{
    public struct Zveno
    {
        public string Name;
        public string LastEntrance;
        public int Score;
    }

    public partial class FMain : Form
    {
        public int[] SourceArray = new int[81];
        public int[,] DirectionArray = new int[9, 9];
        public int[] NumOfCandidates = new int[81];
        public int[,] Neighbors = new int[81, 20];
        public int[,] ResultArrays = new int[999999, 81];
        public bool Way;

        List<Zveno> list = new List<Zveno>();

        public string CurStr;

        public static int i, j, i0, j0, m, Id;

        public FMain()
        {
            InitializeComponent();
        }

        private void FMain_Load(object sender, EventArgs e)
        {
            //настройа таблицы//////////////////////////////////////////////////
            DGV1.AllowUserToAddRows = false;
            DGV1.AllowUserToDeleteRows = false;
            DGV1.AllowUserToResizeColumns = false;
            DGV1.AllowUserToResizeRows = false;
            DGV1.RowHeadersVisible = false;
            DGV1.ColumnHeadersVisible = false;
            DGV1.ScrollBars = ScrollBars.None;
            DGV1.GridColor = Color.Black;
            DGV1.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            DGV1.AlternatingRowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DGV1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DGV1.ColumnCount = 9;
            DGV1.Rows.Add(9);
            DGV1.MultiSelect = false;

            for (i = 0; i < 9; i++)
            {
                DGV1.Columns[i].Width = (int)(DGV1.Width / 9);
                DGV1.Rows[i].Height = (int)(DGV1.Height / 9);              
            }

            DGV1.Width = DGV1.Columns[1].Width * 9 + 3;

            for (i = 0; i < 9; i++)
                for (j = 0; j < 9; j++)
                {
                    if (((i == 3) | (i == 4) | (i == 5)) & !((j == 3) | (j == 4) | (j == 5)) |
                       !((i == 3) | (i == 4) | (i == 5)) & ((j == 3) | (j == 4) | (j == 5)))
                        DGV1.Rows[i].Cells[j].Style.BackColor = Color.Red;
                    DGV1.Rows[i].Cells[j].Value = "";
                }
            ///////////////////////////////////////////////////////////////////////////

            // инициализация //////////////////////////////////////////////////////////
            CBWay.SelectedItem = "Перебор всех цифр";

            for (i = 0; i < 9; i++)
                for (j = 0; j < 9; j++)
                    if (j % 2 == 0)
                        DirectionArray[i, j] = i + 9 * j;
                    else
                        DirectionArray[i, j] = 9 - i + 9 * j - 1;

            for (i = 0; i < 9; i++)
                for (j = 0; j < 9; j++)
                {
                    m = 0;
                    for (i0 = 0; i0 < 9; i0++)
                        for (j0 = 0; j0 < 9; j0++)
                            if ((i == i0) & (j != j0) |
                                (i != i0) & (j == j0) |
                                ((i != i0) & (j != j0) & (i / 3 == i0 / 3) & (j / 3 == j0 / 3)))

                                Neighbors[DirectionArray[i, j], m++] = DirectionArray[i0, j0];
                };
            ///////////////////////////////////////////////////////////////////////////

        }

        private void BExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void N12Save_Click(object sender, EventArgs e)
        {
            if (DlgSaveMain.ShowDialog() == DialogResult.OK)
            {
                FileStream CurSudoku = new FileStream(DlgSaveMain.FileName, FileMode.Create);
                StreamWriter writer = new StreamWriter(CurSudoku);

                for (i = 0; i < 9; i++)
                {
                    CurStr = "";
                    for (j = 0; j < 9; j++)
                        if (DGV1.Rows[i].Cells[j].Value.ToString() == "")
                            CurStr += '0';
                        else
                            CurStr += DGV1.Rows[i].Cells[j].Value.ToString();
                    writer.Write(CurStr + "\r\n");
                }

                writer.Close();
            }
        }

        private void N11Open_Click(object sender, EventArgs e)
        {
            if (DlgOpenMain.ShowDialog() == DialogResult.OK)
            {
                FileStream CurSudoku = new FileStream(DlgOpenMain.FileName, FileMode.Open);
                StreamReader reader = new StreamReader(CurSudoku);

                for (i = 0; i < 9; i++)
                {

                    CurStr = reader.ReadLine();
                    for (j = 0; j < 9; j++)
                        DGV1.Rows[i].Cells[j].Value = CurStr[j];
                }
                reader.Close();
            }
        }

        private void FMain_Shown(object sender, EventArgs e)
        {
            FileStream CurList = new FileStream("List.lst", FileMode.Open);
            StreamReader reader = new StreamReader(CurList);
            Zveno cur;
            while ((cur.Name = reader.ReadLine()) != null)
            {
                cur.LastEntrance = reader.ReadLine();
                cur.Score = Convert.ToInt32(reader.ReadLine());

                list.Add(cur);
            }
                reader.Close();
        }

        private void FMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            FileStream CurList = new FileStream("List.lst", FileMode.Create);
            StreamWriter writer = new StreamWriter(CurList);

            foreach (Zveno cur in list)
            {
                writer.WriteLine(cur.Name);
                writer.WriteLine(cur.LastEntrance);
                writer.WriteLine(cur.Score.ToString());
            }

            writer.Close();
        }

        private void N21Change_Click(object sender, EventArgs e)
        {
            FAuthorization Authorization = new FAuthorization(list, 0);
            Authorization.ShowDialog();
        }
        
        private void BInput_Click(object sender, EventArgs e)
        {            

            for (i = 0; i < 9; i++)
                for (j = 0; j < 9; j++)
                    if (DGV1.Rows[j].Cells[i].Value.ToString() == "")
                        SourceArray[DirectionArray[i, j]] = 0;
                    else
                        if ((DGV1.Rows[j].Cells[i].Value.ToString() == "0") |
                            (DGV1.Rows[j].Cells[i].Value.ToString() == "1") |
                            (DGV1.Rows[j].Cells[i].Value.ToString() == "2") |
                            (DGV1.Rows[j].Cells[i].Value.ToString() == "3") |
                            (DGV1.Rows[j].Cells[i].Value.ToString() == "4") |
                            (DGV1.Rows[j].Cells[i].Value.ToString() == "5") |
                            (DGV1.Rows[j].Cells[i].Value.ToString() == "6") |
                            (DGV1.Rows[j].Cells[i].Value.ToString() == "7") |
                            (DGV1.Rows[j].Cells[i].Value.ToString() == "8"))
                        SourceArray[DirectionArray[i, j]] = Convert.ToInt32(DGV1.Rows[j].Cells[i].Value.ToString());
                    else
                        SourceArray[DirectionArray[i, j]] = 0;
            if (CBWay.SelectedItem.ToString() == "Перебор всех цифр")
                Way = true;
            else
                Way = false;

            FResult Result = new FResult(this, SourceArray, Neighbors, Way, DirectionArray, list);
       
            if (Control(SourceArray))
            {
                Hide();
                Result.Show();
            }
            else
                MessageBox.Show("Судоку не может быть решена. Исправьте ее и повторите попытку.");
        }

        private void CBWay_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CBWay.SelectedItem.ToString() == "Перебор всех цифр")
            {
                LSingles.Visible = false;
                LRecursively.Visible = true;
            }
            else if (CBWay.SelectedItem.ToString() == "С поиском одиночек")
            {
                LSingles.Visible = true;
                LRecursively.Visible = false;
            }
        }

        private void BReset_Click(object sender, EventArgs e)
        {
            for (i = 0; i < 9; i++)
                for (j = 0; j < 9; j++)
                    DGV1.Rows[j].Cells[i].Value = "";
        }

        private bool Control(int[] Sudoku)
        {
            for (i = 0; i < 9; i++)
                for (j = 0; j < 9; j++)
                    if ((Sudoku[i] == Sudoku[Neighbors[i, j]]) & (Sudoku[i] > 0))
                        return false; 
            return true;
        }
    }
}
