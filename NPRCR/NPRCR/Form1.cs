using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;

namespace NPRCR
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void LoadExcelFile()
        {
            Microsoft.Office.Interop.Excel.Application application = new Microsoft.Office.Interop.Excel.Application();
            Workbook workbook = application.Workbooks.Open(textBox1.Text);
            Worksheet worksheet = workbook.Worksheets.get_Item("Sheet1");
            application.Visible = false;
            Range range = worksheet.UsedRange;
            textBox1.Text = range.Rows.Count.ToString() + "::" + range.Columns.Count.ToString();
        }

        private string OpenFile()
        {
            string result = null;
            OpenFileDialog OFD = new OpenFileDialog();
            if(OFD.ShowDialog() == DialogResult.OK)
            {
                result = OFD.FileName;
            }
            return result;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Hellow World!");
            textBox1.Text = OpenFile();
            LoadExcelFile();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
