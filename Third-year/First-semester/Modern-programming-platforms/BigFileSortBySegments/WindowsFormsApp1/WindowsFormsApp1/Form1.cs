using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();            
        }

        private void ButtonGenerateFile_Click(object sender, EventArgs e)
        {                                  
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                Stopwatch CurStopwatch = new Stopwatch();

                ConsoleApp1.RandomFile CurRandomFile = new ConsoleApp1.RandomFile(saveFileDialog.FileName);

                CurStopwatch.Start();
                CurRandomFile.GenerateFile();
                CurStopwatch.Stop();
                MessageBox.Show("Генерация выполнена (" + CurStopwatch.ElapsedMilliseconds + ")");
            }
        }

        private void ButtonSortFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Stopwatch CurStopwatch = new Stopwatch();

                ConsoleApp1.SortFile CurSortFile = new ConsoleApp1.SortFile(openFileDialog.FileName);

                CurStopwatch.Start();
                CurSortFile.Sort();
                CurStopwatch.Stop();
                MessageBox.Show("Сортировка выполнена (" + CurStopwatch.ElapsedMilliseconds + ")");
            }
        }
    }
}
