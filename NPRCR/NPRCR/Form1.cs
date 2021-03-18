using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Microsoft.Office.Interop.Excel;

namespace NPRCR
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public enum FileType { NumberOfMeals, CashReceiptApplicant };
        public struct NumberofMealsFileData
        {
            public string name;
            public string employeeNumber;
            public int card_Breakfast;
            public int card_Lunch;
            public int card_Dinner;
            public int book_Breakfast;
            public int book_Lunch;
            public int book_Dinner;
            public int lunchBox;
        }
        public struct CashReceiptApplicantDatum
        {
            public string name;
            public string employeeNumber;
            public string cashReceiptNum;
            public int totalNumberofMeals;
        }

        public Dictionary<int, NumberofMealsFileData> numberofMealsFileDatum = new Dictionary<int, NumberofMealsFileData>();
        public Dictionary<int, CashReceiptApplicantDatum> cashReceiptApplicantDatum = new Dictionary<int, CashReceiptApplicantDatum>();

        public string cashReceiptPath = string.Empty;

        //엑셀 파일의 종류에 따라 데이터를 불러온다
        private void LoadExcelFile(string filePath, FileType fileType, string SheetName)
        {
            MessageBox.Show("파일 크기에 따라 몇 분정도 소요될 수 있습니다 끄지말고 기다려주세요.");
            //백그라운드에서 엑셀 실행
            Microsoft.Office.Interop.Excel.Application application = new Microsoft.Office.Interop.Excel.Application();
            //엑셀 파일 로드
            Workbook workbook = application.Workbooks.Open(filePath);
            //엑셀 시트 로드
            Worksheet worksheet = workbook.Worksheets.get_Item(1);
            application.Visible = false;
            Range range = worksheet.UsedRange;

            try
            {
                //데이터 파싱
                switch (fileType)
                {
                    case FileType.NumberOfMeals:
                        numberofMealsFileDatum = Parsing_NumberofMealsFileData(range);
                        break;
                    case FileType.CashReceiptApplicant:
                        cashReceiptApplicantDatum = Parsing_CashReceiptApplicantData(range);
                        cashReceiptPath = filePath;
                        break;
                    default:
                        break;
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.ToString());
                ReleaseObject(worksheet);
                ReleaseObject(workbook);
                application.Quit();
                ReleaseObject(application);
            }
            finally
            {
                ReleaseObject(worksheet);
                ReleaseObject(workbook);
                application.Quit();
                ReleaseObject(application);
            }

            MessageBox.Show("완료!");

        }

        private void CalcTotalNumberofMeals()
        {
            if(cashReceiptPath == string.Empty)
            {
                MessageBox.Show("엑셀 파일을 먼저 지정하세요.");
                return;
            }
            else
            {
                MessageBox.Show("파일 크기에 따라 몇 분정도 소요될 수 있습니다 끄지말고 기다려주세요.");

            }
            //백그라운드에서 엑셀 실행
            Microsoft.Office.Interop.Excel.Application application = new Microsoft.Office.Interop.Excel.Application();
            //엑셀 파일 로드
            Workbook workbook = application.Workbooks.Open(cashReceiptPath);
            //엑셀 시트 로드
            Worksheet worksheet = workbook.Worksheets.get_Item(1);

            Dictionary<string, int> numberofMeals = CountingNumberofMeals();
            int index = 2;
            for(int i = 0; i < cashReceiptApplicantDatum.Count; i++)
            {
                if (numberofMeals.ContainsKey(cashReceiptApplicantDatum[i].employeeNumber))
                {
                    worksheet.Cells[index, 5] = numberofMeals[cashReceiptApplicantDatum[i].employeeNumber];
                }
                index++;
            }

            ReleaseObject(worksheet);
            application.Quit();
            ReleaseObject(workbook);
            ReleaseObject(application);

            MessageBox.Show("완료!");
        }

        private void SaveNewExcelFile(string savePath)
        {

            MessageBox.Show("[도 레 미 파 솔 라 시 도 시 라 솔 파 미 레 도]");

            //백그라운드에서 엑셀 실행
            Microsoft.Office.Interop.Excel.Application application = new Microsoft.Office.Interop.Excel.Application();
            //엑셀 파일 로드
            Workbook workbook = application.Workbooks.Add();
            //엑셀 시트 로드
            Worksheet worksheet = workbook.Worksheets.get_Item(1) as Worksheet;
            worksheet.Cells[1, 1] = "이름";
            worksheet.Cells[1, 2] = "사원번호";
            worksheet.Cells[1, 3] = "카드번호";
            worksheet.Cells[1, 4] = "현금영수증 발행번호";
            worksheet.Cells[1, 5] = "식사횟수";

            Dictionary<string, int> numberofMeals = CountingNumberofMeals();
            int index = 2;
            foreach (string employeeNumber in numberofMeals.Keys)
            {
                worksheet.Cells[index, 1] = GetName(employeeNumber).ToString();
                worksheet.Cells[index, 2] = employeeNumber;
                (worksheet.Cells[index, 3] as Range).NumberFormat = "@";
                //worksheet.Cells[index, 3] = GetCardNumber(employeeNumber).ToString();
                worksheet.Cells[index, 4] = GetReceipt(employeeNumber).ToString();
                worksheet.Cells[index, 5] = numberofMeals[employeeNumber].ToString();
                index++;
            }

            worksheet.Columns.AutoFit();
            workbook.SaveAs(savePath, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault);
            workbook.Close(true);

            ReleaseObject(worksheet);
            application.Quit();
            ReleaseObject(workbook);
            ReleaseObject(application);

            MessageBox.Show("완료!");
        }

        private Dictionary<string, int> CountingNumberofMeals()
        {
            Dictionary<string, int> dic = new Dictionary<string, int>();
            for(int i = 0; i < numberofMealsFileDatum.Count; i++)
            {
                if (!dic.ContainsKey(numberofMealsFileDatum[i].employeeNumber))
                {
                    dic.Add(numberofMealsFileDatum[i].employeeNumber, 0);
                }

                dic[numberofMealsFileDatum[i].employeeNumber] += numberofMealsFileDatum[i].card_Breakfast + numberofMealsFileDatum[i].card_Lunch + numberofMealsFileDatum[i].card_Dinner;
                dic[numberofMealsFileDatum[i].employeeNumber] += numberofMealsFileDatum[i].book_Breakfast + numberofMealsFileDatum[i].book_Lunch + numberofMealsFileDatum[i].book_Dinner;
                dic[numberofMealsFileDatum[i].employeeNumber] += numberofMealsFileDatum[i].lunchBox;
            }

            return dic;
        }

        //private string GetCardNumber(string employeeNumber)
        //{
        //    string answer = string.Empty;
        //    for (int i = 0; i < numberofMealsFileDatum.Count; i++)
        //    {
        //        if(numberofMealsFileDatum[i].employeeNumber == employeeNumber)
        //        {
        //            answer = numberofMealsFileDatum[i].cardNumber;
        //            break;
        //        }
        //    }
        //    return answer;
        //}

        private string GetName(string employeeNumber)
        {
            string answer = string.Empty;
            for (int i = 0; i < numberofMealsFileDatum.Count; i++)
            {
                if (numberofMealsFileDatum[i].employeeNumber == employeeNumber)
                {
                    answer = numberofMealsFileDatum[i].name;
                    break;
                }
            }
            return answer;
        }

        private string GetReceipt(string employeeNumber)
        {
            string answer = string.Empty;
            for (int i = 0; i < cashReceiptApplicantDatum.Count; i++)
            {
                if (cashReceiptApplicantDatum[i].employeeNumber == employeeNumber)
                {
                    answer = cashReceiptApplicantDatum[i].cashReceiptNum;
                    break;
                }
            }
            return answer;
        }

        static void ReleaseObject(object obj)
        {
            try
            {
                if (obj != null)
                {
                    Marshal.ReleaseComObject(obj); // 액셀 객체 해제 obj = null; } } catch(Exception ex) { obj = null; throw ex; } finally { GC.Collect(); // 가비지 수집 } }
                    obj = null;
                }
            }
            catch (Exception ex)
            {
                obj = null;
                throw ex;
            }
            finally
            {
                GC.Collect();
            }
        }

        #region Parse Method
        private Dictionary<int, NumberofMealsFileData> Parsing_NumberofMealsFileData(Range range)
        {
            Dictionary<int, NumberofMealsFileData> result = new Dictionary<int, NumberofMealsFileData>();
            for (int i = 3; i <= range.Rows.Count; i++)
            {
                int count = 1;
                NumberofMealsFileData data;
                data.name = (range.Cells[i, count++] as Range).Value2.ToString();
                //data.cardNumber = (range.Cells[i, count++] as Range).Value2.ToString();
                data.employeeNumber = (range.Cells[i, count++] as Range).Value2.ToString();
                if ((range.Cells[i, count] as Range).Value2 != null) data.card_Breakfast = int.Parse((range.Cells[i, count] as Range).Value2.ToString());
                else data.card_Breakfast = 0;
                count++;
                //data.card_Breakfast = (range.Cells[i, count++] as Range).Value2 != null ? int.Parse((range.Cells[i, count] as Range).Value2.ToString()) : 0;
                data.card_Lunch = (range.Cells[i, count] as Range).Value2 != null ? int.Parse((range.Cells[i, count] as Range).Value2.ToString()) : 0;
                count++;
                data.card_Dinner = (range.Cells[i, count] as Range).Value2 != null ? int.Parse((range.Cells[i, count] as Range).Value2.ToString()) : 0;
                count++;
                data.book_Breakfast = (range.Cells[i, count] as Range).Value2 != null ? int.Parse((range.Cells[i, count] as Range).Value2.ToString()) : 0;
                count++;
                data.book_Lunch = (range.Cells[i, count] as Range).Value2 != null ? int.Parse((range.Cells[i, count] as Range).Value2.ToString()) : 0;
                count++;
                data.book_Dinner = (range.Cells[i, count] as Range).Value2 != null ? int.Parse((range.Cells[i, count] as Range).Value2.ToString()) : 0;
                count++;
                data.lunchBox = (range.Cells[i, count] as Range).Value2 != null ? int.Parse((range.Cells[i, count] as Range).Value2.ToString()) : 0;
                count++;
                result.Add(i - 3, data);
            }

            return result;
        }

        private Dictionary<int, CashReceiptApplicantDatum> Parsing_CashReceiptApplicantData(Range range)
        {
            Dictionary<int, CashReceiptApplicantDatum> result = new Dictionary<int, CashReceiptApplicantDatum>();
            for (int i = 2; i <= range.Rows.Count; i++)
            {
                int count = 2;
                CashReceiptApplicantDatum data;
                data.name = (range.Cells[i, count++] as Range).Value2.ToString();
                data.employeeNumber = (range.Cells[i, count++] as Range).Value2.ToString();
                data.cashReceiptNum = (range.Cells[i, count++] as Range).Value2.ToString();
                data.totalNumberofMeals = 0;
                result.Add(i - 2, data);
            }

            return result;
        }
        #endregion

        //파일 선택후 경로 반환
        private string OpenFile()
        {
            string result = null;
            OpenFileDialog OFD = new OpenFileDialog();

            if (OFD.ShowDialog() == DialogResult.OK)
            {
                result = OFD.FileName;
            }
            return result;
        }

        private string SaveFile()
        {
            string result = null;
            SaveFileDialog SFD = new SaveFileDialog();

            SFD.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            SFD.FilterIndex = 2;
            SFD.RestoreDirectory = true;

            if (SFD.ShowDialog() == DialogResult.OK)
            {
                result = SFD.FileName;
            }
            return result;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string filePath = OpenFile();

            textBox1.Text = filePath;
            LoadExcelFile(filePath, FileType.NumberOfMeals, "Sheet1");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //file Open
            string filePath = OpenFile();

            textBox2.Text = filePath;
            LoadExcelFile(filePath, FileType.CashReceiptApplicant, "Sheet1");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //string filePath = SaveFile();
            //SaveNewExcelFile(filePath);

            CalcTotalNumberofMeals();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
