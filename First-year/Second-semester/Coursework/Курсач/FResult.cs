using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Курсач
{
    public partial class FResult : Form
    {
        public int[] SourceArray = new int[81];
        public bool[] PossibleArray = new bool[81];
        public int[,] DirectionArray = new int[9, 9];
        public int[] NumOfCandidates = new int[81];
        public int[,] Neighbors = new int[81, 20];
        public int[,] ResultArrays = new int[999999, 81];
        public int[,] Candidates = new int[81, 9];

        public int NumOfResults, NumOfDecisions, NumOfSteps;
        public bool Way;

        public int i, j, i0, j0, m;

        private FMain Main = new FMain();

        private void BStart_Click(object sender, EventArgs e)
        {
            if (ENumOfResults.Text != "")
                NumOfResults = Convert.ToInt32(ENumOfResults.Text);
            else
                NumOfResults = 99999;

            if (NumOfResults != 0)
                Heart();
            else
                MessageBox.Show("Не может быть 0 решений. Введите натуральное число.");

            FAuthorization Authorization = new FAuthorization(list, NumOfSteps);
            Authorization.ShowDialog();
        }

        private void BPrev_Click(object sender, EventArgs e)
        {
            if (NumOfDecisions > 1)
            {
                NumOfDecisions = NumOfDecisions - 1;
                Show(NumOfDecisions);
            }
        }

        private void BNext_Click(object sender, EventArgs e)
        {
            if (NumOfDecisions < NumOfResults)
            {
                NumOfDecisions = NumOfDecisions + 1;
                Show(NumOfDecisions);
            }
        }

        private void BSearch_Click(object sender, EventArgs e)
        {
            if (ESearch.Text != "")
                if ((Convert.ToInt32(ESearch.Text) > 0) & (Convert.ToInt32(ESearch.Text) <= NumOfResults))
                    Show(NumOfDecisions = Convert.ToInt32(ESearch.Text));
                else
                    MessageBox.Show("Решение с введенным номером не найдено");
            else
                MessageBox.Show("Введите номер решения.");
        }

        private void N12Save_Click(object sender, EventArgs e)
        {
            if (NumOfDecisions != 0)
                if (DlgSaveResult.ShowDialog() == DialogResult.OK)
                {
                    FileStream CurSudoku = new FileStream(DlgSaveResult.FileName, FileMode.Create);
                    StreamWriter writer = new StreamWriter(CurSudoku);

                    string CurStr;

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
            else
                MessageBox.Show("Нечего сохранять!");
        }

        private void BExit_Click(object sender, EventArgs e)
        {
            Hide();
            Main.Show();
        }
        List<Zveno> list = new List<Zveno>();
        public FResult(FMain Main, int[] SourceArray, int[,] Neighbors, bool Way, int[,] DirectionArray, List<Zveno> list)
        {
            InitializeComponent();

            this.Main = Main;
            this.SourceArray = SourceArray;
            this.Neighbors = Neighbors;
            this.Way = Way;
            this.DirectionArray = DirectionArray;
            this.list = list;
        }

        private void FResult_Load(object sender, EventArgs e)
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

            LNumOfDecision.Text = "";
            ENumOfResults.Text = "1";
            LNumOfSteps.Text = "";
            LNumOfDecisionShown.Text = "";

            BPrev.Enabled = false;
            BNext.Enabled = false;
            BSearch.Enabled = false;


            for (i = 0; i < 81; i++)
                if (SourceArray[i] > 0)
                    PossibleArray[i] = true;
                else
                    PossibleArray[i] = false;
        }

        private void Heart()
        {
            bool CandidateCheck(int Candidate, int NumOfEl)
            {
                int i;
                i = 0;
                while (i < 20)
                    if (Candidate == SourceArray[Neighbors[NumOfEl, i++]])
                        return false;
                return true;
            }


            void RecursiveMethod(int NumOfEl)
            {
                int Candidate;
                if (NumOfEl == 81)
                {
                    for (i = 0; i < 81; i++)
                        ResultArrays[NumOfDecisions - 1, i] = SourceArray[i];
                    NumOfDecisions++;
                }
                if ((NumOfEl < 81) & (NumOfDecisions <= NumOfResults))
                    if (PossibleArray[NumOfEl])
                        RecursiveMethod(NumOfEl + 1);
                    else
                        for (Candidate = 1; Candidate <= 9; Candidate++)
                        {
                            if (CandidateCheck(Candidate, NumOfEl))
                            {
                                SourceArray[NumOfEl] = Candidate;
                                NumOfSteps++;
                                RecursiveMethod(NumOfEl + 1);
                            }

                            SourceArray[NumOfEl] = 0;
                            NumOfSteps++;
                        }
            }

            bool ModumFieri()
            {
                bool Easy;
                int Candidate, NumOfEl;
                do
                {
                    Easy = false;

                    for (NumOfEl = 0; NumOfEl < 81; NumOfEl++)
                    {
                        NumOfCandidates[NumOfEl] = 0;

                        if (SourceArray[NumOfEl] == 0)
                        {
                            for (Candidate = 1; Candidate <= 9; Candidate++)
                                if (CandidateCheck(Candidate, NumOfEl))
                                    Candidates[NumOfEl, NumOfCandidates[NumOfEl]++] = Candidate;                                

                            if (NumOfCandidates[NumOfEl] == 0)
                                return false;

                            if (NumOfCandidates[NumOfEl] == 1)
                            {
                                SourceArray[NumOfEl] = Candidates[NumOfEl, 0];
                                Easy = true;
                            }
                        }
                    }
                }
                while (Easy);
                return true;
            }

            void RecursiveMethod_Smart(int NumOfEl)
            {
                if (NumOfEl == 81)
                {
                    for (i = 0; i < 81; i++)
                        ResultArrays[NumOfDecisions - 1, i] = SourceArray[i];
                    NumOfDecisions++;
                }
                if ((NumOfEl < 81) & (NumOfDecisions <= NumOfResults))
                {
                    if (NumOfCandidates[NumOfEl] == 0)
                        RecursiveMethod_Smart(NumOfEl + 1);
                    if (NumOfCandidates[NumOfEl] > 1)
                    {
                        for (int t = 0; t < NumOfCandidates[NumOfEl]; t++)
                        {
                            if (CandidateCheck(Candidates[NumOfEl, t], NumOfEl))
                            {
                                SourceArray[NumOfEl] = Candidates[NumOfEl, t];
                                NumOfSteps++;
                                RecursiveMethod_Smart(NumOfEl + 1);
                            }
                        }

                        SourceArray[NumOfEl] = 0;
                        NumOfSteps++;
                    }
                }
            }

            {
                NumOfDecisions = 1;
                NumOfSteps = 0;

                if (Way)
                    RecursiveMethod(0);
                else
                  if (ModumFieri())
                    RecursiveMethod_Smart(0);
                else
                    MessageBox.Show("Судоку не может быть решена. Исправьте ее и повторите попытку.");

                LNumOfDecision.Text = Convert.ToString(NumOfDecisions - 1);
                LNumOfSteps.Text = Convert.ToString(NumOfSteps);



                NumOfDecisions = 1;
                Show(NumOfDecisions);
            }
        }

        void Show(int Id)
        {
            LNumOfDecisionShown.Text = Convert.ToString(Id);
            if (Id == 1)
                BPrev.Enabled = false;
            else
                BPrev.Enabled = true;
            if (Id == NumOfResults)
                BNext.Enabled = false;
            else
                BNext.Enabled = true;

            if (NumOfResults == 1)
                BSearch.Enabled = false;
            else
                BSearch.Enabled = true;

            for (i = 0; i < 9; i++)
                for (j = 0; j < 9; j++)
                    DGV1.Rows[j].Cells[i].Value = Convert.ToString(ResultArrays[Id - 1, DirectionArray[i, j]]);
        }

    }
}
