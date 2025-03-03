using System;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

namespace Курсач
{
    public partial class FAuthorization : Form
    {
        public string PlayerNickname { get; set; }
        public Color PlayerColor { get; set; }
        public FAuthorization()
        {
            InitializeComponent();
            
            CBColor.Items.Add(Color.Green);
            CBColor.Items.Add(Color.Brown);
            CBColor.Items.Add(Color.Yellow);
            CBColor.Items.Add(Color.SpringGreen);
            CBColor.Items.Add(Color.Violet);
            CBColor.Items.Add(Color.SkyBlue);
            CBColor.Items.Add(Color.SeaGreen);
            CBColor.Items.Add(Color.RoyalBlue);
            CBColor.Items.Add(Color.Purple);
        }     
        private void BCancel_Click(object sender, EventArgs e)
        {
            Hide();
        }
        private void CBColor_DrawItem(object sender, DrawItemEventArgs e)
        {
            int MarginWidth = 4;
            int MarginHeight = 4;
            // Draw a ComboBox item that is displaying a color sample
            if (e.Index < 0) return;
            // Clear the background appropriately.
            e.DrawBackground();
            // Draw the color sample.
            int hgt = e.Bounds.Height - 2 * MarginHeight;
            Rectangle rect = new Rectangle(
                e.Bounds.X + MarginWidth,
                e.Bounds.Y + MarginHeight,
                hgt, hgt);
            ComboBox cbo = sender as ComboBox;
            Color color = (Color)cbo.Items[e.Index];
            using (SolidBrush brush = new SolidBrush(color))
            {
                e.Graphics.FillRectangle(brush, rect);
            }
            // Outline the sample in black.
            e.Graphics.DrawRectangle(Pens.Black, rect);
            // Draw the color's name to the right.
            using (Font font = new Font(cbo.Font.FontFamily,
                cbo.Font.Size * 0.75f, FontStyle.Bold))
            {
                using (StringFormat sf = new StringFormat())
                {
                    sf.Alignment = StringAlignment.Near;
                    sf.LineAlignment = StringAlignment.Center;
                    int x = hgt + 2 * MarginWidth;
                    int y = e.Bounds.Y + e.Bounds.Height / 2;
                    e.Graphics.TextRenderingHint =
                        TextRenderingHint.AntiAliasGridFit;
                    e.Graphics.DrawString(color.Name, font,
                        Brushes.Black, x, y, sf);
                }
            }
            // Draw the focus rectangle if appropriate.
            e.DrawFocusRectangle();
        }

        private void FAuthorization_Load(object sender, EventArgs e)
        {
            TBNickname.Text = PlayerNickname;
            CBColor.SelectedItem = PlayerColor;
        }

        private void BChoose_Click(object sender, EventArgs e)
        {
            PlayerNickname = TBNickname.Text;
            PlayerColor = (Color)CBColor.SelectedItem;
            Hide();
        }
    }
}
