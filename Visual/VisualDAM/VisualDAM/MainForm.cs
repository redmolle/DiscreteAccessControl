using System;
using System.Drawing;
using System.Windows.Forms;
using Helper;

namespace VisualDAM
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void SetInput(object sender, EventArgs e, string text)
        {
            Input.Text = Helper.Help.FormatInput(text);
        }
        private void SetOutput(object sender, EventArgs e, string text)
        {
            Output.Text = text;
        }

        private void SetText(object sender, EventArgs e, Helper.InputOutput model)
        {
            if (!string.IsNullOrEmpty(model.Input))
                SetInput(sender, e, model.Input);
            if (!string.IsNullOrEmpty(model.Output))
                SetOutput(sender, e, model.Output);
            if (model.MisstakeWord != null)
                MarkMisstakes(Convert.ToInt32(model.MisstakeWord), model.MisstakeLetter, sender, e);
        }

        public void MarkMisstakes(int word, int? letter, object sender, EventArgs e)
        {
            Input.Text += " ";
            Input.SelectionStart = word;
            Input.SelectionLength = Convert.ToInt32(letter);
            Input.SelectionColor = Color.Red;
            Input.SelectionLength = 0;
            Input.SelectionStart = Input.Text.Length;
        }

        private void ShowAccessMatrix()
        {
            if (Helper.Help.ParsedSystem == null)
            {
                return;
            }
            string[,] m = Helper.Help.AccessMatrix();
            AccessMatrix.Rows.Clear();
            AccessMatrix.ColumnCount = m.GetLength(1);
            for (int i = 0; i < m.GetLength(1); i++)
            {
                string[] tmp = new string[m.GetLength(0)];
                for (int j = 0; j < m.GetLength(0); j++)
                {
                    tmp[j] = m[i, j];
                }
                AccessMatrix.Rows.Add(tmp);
            }
        }

        private void MarkUnnecessary(int l)
        {
            Input.SelectionStart = l;
            Input.SelectionLength = Input.Text.Length - l;
            Input.SelectionColor = Color.OrangeRed;
            Input.SelectionLength = 0;
            Input.SelectionStart = Input.Text.Length;
        }

        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            outputToolStripMenuItem_Click(sender, e);
            matrixToolStripMenuItem_Click(sender, e);
            InputOutput model = new InputOutput();
            try
            {
                SetInput(sender, e, Helper.Help.FormatInput(Input.Text));
                Helper.Help.SendToParse(Input.Text);
                model.Output = Helper.Help.ShowOutput(Helper.Help.ParsedSystem);
                MarkUnnecessary(Helper.Help.ParsedSystem.Length);
                ShowAccessMatrix();
            }
            catch (DAM.Model.DACException ex)
            {
                Helper.Help.ParsedSystem = null;
                model.Output = $"Встречена ошибка в тексте! {ex.message}";
                model.MisstakeWord = ex.markFrom;
                model.MisstakeLetter = ex.markTo;
            }
            catch (Exception ex)
            {
                model.Output = Helper.Help.ShowError(ex);
            }
            SetText(sender, e, model);
        }

        private void correctToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SetInput(sender, e, DAM.Dict.Example.Correct);
        }

        private void incorrectToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SetInput(sender, e, DAM.Dict.Example.InCorrect);
        }

        private void inputToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Input.Clear();
        }

        private void matrixToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AccessMatrix.Rows.Clear();
            AccessMatrix.RowCount = 0;
            AccessMatrix.ColumnCount = 0;
        }

        private void outputToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Output.Clear();
        }

        private void allToolStripMenuItem_Click(object sender, EventArgs e)
        {
            inputToolStripMenuItem_Click(sender, e);
            matrixToolStripMenuItem_Click(sender, e);
            outputToolStripMenuItem_Click(sender, e);
        }
    }
}