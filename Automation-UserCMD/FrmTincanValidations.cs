using ExportToExcel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Automation_UserCMD
{
    public partial class FrmTincanValidations : Form
    {
        public FrmTincanValidations()
        {
            InitializeComponent();
        }

        private void btnUAChecklist_Click(object sender, EventArgs e)
        {
            //1. Get Directory
            string rootdir = System.IO.Directory.GetCurrentDirectory();
            rootdir = Path.GetDirectoryName(rootdir);
            rootdir = Path.GetDirectoryName(rootdir);

            //1. Get file Paths
            //string AssessmentIdsinputfilepath = rootdir + @"\TestData\TincanInput\tincanevents.xls";
            string TinCanfilepath = rootdir + @"\TestData\TincanInput\tincanevents.xls";
            string OutputTincan = rootdir + @"\TestData\Outputs\TincanValidaionOutput.xlsx";
            string ErrorOutputTincan = rootdir + @"\TestData\Outputs\TincanValidationOutputError.xlsx";
            string missingdataforids = string.Empty;

            //2. Fill datasets
            //DataSet dsAssessmentIds = HelperCommonMethods.ReadExcelToFillData(AssessmentIdsinputfilepath);
            DataSet dsTincan = HelperCommonMethods.ReadExcelToFillData(TinCanfilepath);
            DataTable dtfinalTincan = new DataTable();
            string idcolumn = "actorid";

            string Assessmentid = txtAssessmentId.Text;
            string TeacherId = txtTeacherId.Text;
            string StudentId = txtStudentId.Text;
            
            
            
            //3. Apply Business Logic
            dtfinalTincan = HelperCommonMethods.ApplyTincanBusinessLogic_UAChecklist(Assessmentid, TeacherId,StudentId, dsTincan, idcolumn, out missingdataforids);

            //4. Hide not reqd. columns
            //HelperCommonMethods.HideColumnsfromReportContent(dtfinalTincan);
            dtTincanOutput.DataSource = dtfinalTincan;

            //5. Generate output Excel
            //CreateExcelFile.CreateExcelDocument(dtfinalTincan, OutputTincan);

            //if (!string.IsNullOrEmpty(missingdataforids))
            //{
            //    DataTable dtfinalSkillError = new DataTable("Errors");
            //    dtfinalSkillError = HelperCommonMethods.GenerateDataTableForErrors(dtfinalSkillError, missingdataforids);
            //    CreateExcelFile.CreateExcelDocument(dtfinalSkillError, ErrorOutputTincan);
            //}
            //6. Show success failure message
            HelperCommonMethods.SuccessErrorMessage(OutputTincan, missingdataforids);
        }
    }
}
