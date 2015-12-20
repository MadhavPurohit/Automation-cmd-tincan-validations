using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace ExportToExcel
{
    public partial class Form1 : Form
    {
        //  Sample code, to demonstrate how to create Excel 2007 .xlsx files from your C# applications.
        //  www.MikesKnowledgeBase.com
        //
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCreateExcel_Click(object sender, EventArgs e)
        {
#if DEBUG
            //  We'll attempt to create our example .XLSX file in our "My Documents" folder
            string MyDocumentsPath = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string TargetFilename = System.IO.Path.Combine(MyDocumentsPath, "Sample.xlsx");
#else
            // Prompt the user to enter a path/filename to save an example Excel file to
            saveFileDialog1.FileName = "Sample.xlsx";
            saveFileDialog1.Filter = "Excel 2007 files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.OverwritePrompt = false;

            //  If the user hit Cancel, then abort!
            if (saveFileDialog1.ShowDialog() != DialogResult.OK)
                return;

            string TargetFilename = saveFileDialog1.FileName;
#endif

            //  Step 1: Create a DataSet, and put some sample data in it
            DataSet ds = CreateSampleData();

            //  Step 2: Create the Excel file
            try
            {
                CreateExcelFile.CreateExcelDocument(ds, TargetFilename);
            }
            catch (Exception ex)
            { 
                MessageBox.Show("Couldn't create Excel file.\r\nException: " + ex.Message);
                return;
            }
 
            //  Step 3:  Let's open our new Excel file and shut down this application.
            Process p = new Process();
            p.StartInfo = new ProcessStartInfo(TargetFilename);
            p.Start();

            this.Close();
        }

        #region CREATE_SAMPLE_DATA
        private DataSet CreateSampleData()
        {
            //  Create a sample DataSet, containing three DataTables.
            //  (Later, this will save to Excel as three Excel worksheets.)
            //
            DataSet ds = new DataSet();

            //  Create the first table of sample data
            DataTable dt1 = new DataTable("Drivers");
            dt1.Columns.Add("UserID", Type.GetType("System.Decimal"));
            dt1.Columns.Add("Surname", Type.GetType("System.String"));
            dt1.Columns.Add("Forename", Type.GetType("System.String"));
            dt1.Columns.Add("Sex", Type.GetType("System.String"));
            dt1.Columns.Add("Date of Birth", Type.GetType("System.DateTime"));

            dt1.Rows.Add(new object[] { 1, "James", "Brown", "M", new DateTime(1962,3,19) });
            dt1.Rows.Add(new object[] { 2, "Edward", "Jones", "M", new DateTime(1939,7,12) });
            dt1.Rows.Add(new object[] { 3, "Janet", "Spender", "F", new DateTime(1996,1,7) });
            dt1.Rows.Add(new object[] { 4, "Maria", "Percy", "F", null });
            dt1.Rows.Add(new object[] { 5, "Malcolm", "Marvelous", "M", new DateTime(1973,5,7) });
            ds.Tables.Add(dt1);


            //  Create the second table of sample data
            DataTable dt2 = new DataTable("Vehicles");
            dt2.Columns.Add("Vehicle ID", Type.GetType("System.Decimal"));
            dt2.Columns.Add("Make", Type.GetType("System.String"));
            dt2.Columns.Add("Model", Type.GetType("System.String"));

            dt2.Rows.Add(new object[] { 1001, "Ford", "Banana" });
            dt2.Rows.Add(new object[] { 1002, "GM", "Thunderbird" });
            dt2.Rows.Add(new object[] { 1003, "Porsche", "Rocket" });
            dt2.Rows.Add(new object[] { 1004, "Toyota", "Gas guzzler" });
            dt2.Rows.Add(new object[] { 1005, "Fiat", "Spangly" });
            dt2.Rows.Add(new object[] { 1006, "Peugeot", "Lawnmower" });
            dt2.Rows.Add(new object[] { 1007, "Jaguar", "Freeloader" });
            dt2.Rows.Add(new object[] { 1008, "Aston Martin", "Caravanette" });
            dt2.Rows.Add(new object[] { 1009, "Mercedes-Benz", "Hitchhiker" });
            dt2.Rows.Add(new object[] { 1010, "Renault", "Sausage" });
            dt2.Rows.Add(new object[] { 1011, /*char.ConvertFromUtf32(12) + */ "Saab", "Chickennuggetmobile" });
            ds.Tables.Add(dt2);


            //  Create the third table of sample data
            DataTable dt3 = new DataTable("Vehicle owners");
            dt3.Columns.Add("User ID", Type.GetType("System.Decimal"));
            dt3.Columns.Add("Vehicle_ID", Type.GetType("System.Decimal"));

            dt3.Rows.Add(new object[] { 1, 1002 });
            dt3.Rows.Add(new object[] { 2, 1000 });
            dt3.Rows.Add(new object[] { 3, 1010 });
            dt3.Rows.Add(new object[] { 5, 1006 });
            dt3.Rows.Add(new object[] { 6, 1007 });
            ds.Tables.Add(dt3);

            return ds;
        }
        #endregion

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process p = new Process();
            p.StartInfo = new ProcessStartInfo("http://mikesknowledgebase.com/pages/CSharp/ExportToExcel.htm");
            p.Start();
        }
    }
}
