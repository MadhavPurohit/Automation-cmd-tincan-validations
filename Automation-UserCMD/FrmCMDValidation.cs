using Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExportToExcel;


namespace Automation_UserCMD
{
    public partial class FrmCMDValidation : Form
    {
        public FrmCMDValidation()
        {
            InitializeComponent();
        }

        private void Users_Click(object sender, EventArgs e)
        {
            try
            {


                //HelperCommonMethods.charities();
                
                
                //1. Get Directory
                string rootdir = System.IO.Directory.GetCurrentDirectory();
                rootdir = Path.GetDirectoryName(rootdir);
                rootdir = Path.GetDirectoryName(rootdir);

                //2. Get corresponding file path
                string studentfilepath = rootdir + @"\TestData\Inputs\UserEnrollment\StudentIdsInput.xls";
                string userfilepath = rootdir + @"\TestData\Inputs\Users\User.xlsx";
                string OutputUser = rootdir + @"\TestData\Outputs\Users.xlsx";
                string ErrorOutputUser = rootdir + @"\TestData\Outputs\UsersError.xlsx";

                
                //3. Fill Datasets
                DataSet dsStudentid = HelperCommonMethods.ReadExcelToFillData(studentfilepath);
                DataSet dsUserEnrollment = HelperCommonMethods.ReadExcelToFillData(userfilepath,false);
                DataTable dtfinaluserenrolment = new DataTable();
                string missingdataforids = string.Empty;

                


                
                //4. Apply Business Logic - Mapping & Validations
                string idcolumn = "id";
                dtfinaluserenrolment = HelperCommonMethods.ApplyCMDBusinessLogic(dsStudentid, dsUserEnrollment, idcolumn, out missingdataforids);

                //5. Hide not required column
                HelperCommonMethods.HideColumnsfromReportUsersMapping(dtfinaluserenrolment);
                dataGridView1.DataSource = dtfinaluserenrolment;

                //6. Generate output excel
                CreateExcelFile.CreateExcelDocument(dtfinaluserenrolment, OutputUser);
                
                //7. Error Excel Genrate
                if (!string.IsNullOrEmpty(missingdataforids))
                {
                    DataTable dtfinalSkillError = new DataTable("Errors");
                    dtfinalSkillError = HelperCommonMethods.GenerateDataTableForErrors(dtfinalSkillError, missingdataforids);
                    CreateExcelFile.CreateExcelDocument(dtfinalSkillError, ErrorOutputUser);
                }
                //8. Display success failure message
                HelperCommonMethods.SuccessErrorMessage(OutputUser, missingdataforids);
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error occurred in processing" + ex.Message);
            }
        }

        private void Class_Click(object sender, EventArgs e)
        {
            LblReqdInput.Text = "Enter Organization Id";
            LblReqdInput.Visible = true;
            lblClassFilter.Visible = true;
            txtClassFilter.Visible = true;
            tbReqdInput.Visible = true;
            Submit.Visible = true;
        }


        private void Submit_Click(object sender, EventArgs e)
        {
            try
            {

                if (LblReqdInput.Visible == true && LblReqdInput.Text == "Enter grade name filter" && txtClassFilter.Visible == false)
                {
                    btnFramework_cmdMapping();

                }

                else
                {
                    btnClass_cmdMapping();
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error occurred in processing" + ex.Message);
            }
        }



        private void btnClass_cmdMapping()
        {
            //1. Get File paths
            string rootdir = System.IO.Directory.GetCurrentDirectory();
            rootdir = Path.GetDirectoryName(rootdir);
            rootdir = Path.GetDirectoryName(rootdir);

            string classinput = rootdir + @"\TestData\Inputs\Class\Class.xls";
            string finalclass = rootdir + @"\TestData\Outputs\ClassOutput.xlsx";
            string Errorfinalclass = rootdir + @"\TestData\Outputs\ClassOutputError.xlsx";
            string erroroutput = string.Empty;

            //2. Read and fill datasets
            DataSet dsClassInput = HelperCommonMethods.ReadExcelToFillData(classinput);
            DataTable dtfinalClassOutput = new DataTable();

            DataTable dtclassInput = dsClassInput.Tables[0];
            string inputclassfilter = txtClassFilter.Text.ToString();

            //3. Apply cmd business logic for mapping and Fileds validation
            dtfinalClassOutput = HelperCommonMethods.ApplyCMDBusinessLogic_Class(dtclassInput, tbReqdInput.Text.ToString(), inputclassfilter, out erroroutput);

            //4. Hide unwanted columns
            HelperCommonMethods.HideColumnsfromReportClass(dtfinalClassOutput);
            dataGridView1.DataSource = dtfinalClassOutput;

            if (!string.IsNullOrEmpty(erroroutput))
            {
                DataTable dtfinalSkillError = new DataTable("Errors");
                dtfinalSkillError = HelperCommonMethods.GenerateDataTableForErrors(dtfinalSkillError, erroroutput);
                CreateExcelFile.CreateExcelDocument(dtfinalSkillError, Errorfinalclass);
            }

            //5. Generate Output excel
            CreateExcelFile.CreateExcelDocument(dtfinalClassOutput, finalclass);

            //6. Display success failure message
            HelperCommonMethods.SuccessErrorMessage(finalclass, erroroutput);

            //7. Hide Additional input controls
            LblReqdInput.Visible = false;
            tbReqdInput.Visible = false;
            Submit.Visible = false;
            lblClassFilter.Visible = false;
            txtClassFilter.Visible = false;

        }

        void UserValidationCMD_Click(object sender, EventArgs e)
        {
            try
            {
                string rootdir = System.IO.Directory.GetCurrentDirectory();
                rootdir = Path.GetDirectoryName(rootdir);
                rootdir = Path.GetDirectoryName(rootdir);

                //1. Get file paths
                string studentfilepath = rootdir + @"\TestData\Inputs\UserEnrollment\StudentIdsInput.xls";
                string userEnrollmentfilepath = rootdir + @"\TestData\Inputs\UserEnrollment\UserEnrollment.xls";
                string OutputUserEnrollment = rootdir + @"\TestData\Outputs\UserEnrollmentOutput.xlsx";
                string ErrorOutputUserEnrollment = rootdir + @"\TestData\Outputs\UserEnrollmentOutputError.xlsx";

                //OR - 1. Get File Path from AppSettings.config
                string[] filepaths = HelperCommonMethods.GetInputOutputFilePaths_UserValidation();
                string erroroutput = string.Empty;

                //2. Read excel and fill datasets
                DataSet dsStudentid = HelperCommonMethods.ReadExcelToFillData(studentfilepath);
                DataSet dsUserEnrollment = HelperCommonMethods.ReadExcelToFillData(userEnrollmentfilepath);
                DataTable dtfinaluserenrolment = new DataTable();
                dtfinaluserenrolment = dsUserEnrollment.Tables[0].Copy();
                dtfinaluserenrolment.Clear();
                string idcolumn = "userid";

                //3. Apply business logic for mapping and Validations
                dtfinaluserenrolment = HelperCommonMethods.ApplyCMDBusinessLogic_UserEnrollment(dsStudentid, dsUserEnrollment, idcolumn, out erroroutput);

                //4. Hide unwanted columns
                HelperCommonMethods.HideColumnsfromReportUsersEnrollment(dtfinaluserenrolment);

                //5. Generate output excel
                dataGridView1.DataSource = dtfinaluserenrolment;
                CreateExcelFile.CreateExcelDocument(dtfinaluserenrolment, OutputUserEnrollment);

                if (!string.IsNullOrEmpty(erroroutput))
                {
                    DataTable dtfinalSkillError = new DataTable("Errors");
                    dtfinalSkillError = HelperCommonMethods.GenerateDataTableForErrors(dtfinalSkillError, erroroutput);
                    CreateExcelFile.CreateExcelDocument(dtfinalSkillError, ErrorOutputUserEnrollment);
                }


                //6. Show success/failure message
                HelperCommonMethods.SuccessErrorMessage(OutputUserEnrollment, erroroutput);
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error occurred in processing" + ex.Message);

            }
        }



        private void ClassProdMapping_Click(object sender, EventArgs e)
        {
            try
            {
                //1. Get Directory
                string rootdir = System.IO.Directory.GetCurrentDirectory();
                rootdir = Path.GetDirectoryName(rootdir);
                rootdir = Path.GetDirectoryName(rootdir);

                string containerinputfilepath = rootdir + @"\TestData\Inputs\ClassProductMapping\ContainerIdsInput.xls";
                string classProductAssfilepath = rootdir + @"\TestData\Inputs\ClassProductMapping\ClassProductAsscociation.xls";
                string OutputClassProdMapping = rootdir + @"\TestData\Outputs\ClassProdMappingOutput.xlsx";
                string ErrorOutputClassProdMapping = rootdir + @"\TestData\Outputs\ClassProdMappingOutputError.xlsx";

                DataSet dscontainerid = HelperCommonMethods.ReadExcelToFillData(containerinputfilepath);
                DataSet dsClassProdAssociation = HelperCommonMethods.ReadExcelToFillData(classProductAssfilepath);
                DataTable dtfinalClassProdMapping = new DataTable();
                dtfinalClassProdMapping = dsClassProdAssociation.Tables[0].Copy();
                dtfinalClassProdMapping.Clear();

                string missingdataforids = string.Empty;
                string idcolumn = "contentcontainerid";
                dtfinalClassProdMapping = HelperCommonMethods.ApplyCMDBusinessLogic_ClassProdMapping(dscontainerid, dsClassProdAssociation, idcolumn, out missingdataforids);

                HelperCommonMethods.HideColumnsfromReportClassProdMapping(dtfinalClassProdMapping);
                dataGridView1.DataSource = dtfinalClassProdMapping;
                CreateExcelFile.CreateExcelDocument(dtfinalClassProdMapping, OutputClassProdMapping);

                if (!string.IsNullOrEmpty(missingdataforids))
                {
                    DataTable dtfinalSkillError = new DataTable("Errors");
                    dtfinalSkillError = HelperCommonMethods.GenerateDataTableForErrors(dtfinalSkillError, missingdataforids);
                    CreateExcelFile.CreateExcelDocument(dtfinalSkillError, ErrorOutputClassProdMapping);
                }
                HelperCommonMethods.SuccessErrorMessage(OutputClassProdMapping, missingdataforids);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred in processing" + ex.Message);
            }
        }


        private void AssetSkillMapping_Click(object sender, EventArgs e)
        {
            try
            {
                //1. Get Directory
                string rootdir = System.IO.Directory.GetCurrentDirectory();
                rootdir = Path.GetDirectoryName(rootdir);
                rootdir = Path.GetDirectoryName(rootdir);

                string assetinputfilepath = rootdir + @"\TestData\Inputs\AssetSkillMapping\AssetIdsInput.xls";
                string AssetSkillMappingfilepath = rootdir + @"\TestData\Inputs\AssetSkillMapping\AssetSkillsMapping.xls";
                string OutputAssetSkillMapping = rootdir + @"\TestData\Outputs\AssetSkillMappingOutput.xlsx";
                string ErrorOutputAssetSkillMapping = rootdir + @"\TestData\Outputs\AssetSkillMappingOutputError.xlsx";

                DataSet dsassetid = HelperCommonMethods.ReadExcelToFillData(assetinputfilepath);
                DataSet dsAssetSkillMapping = HelperCommonMethods.ReadExcelToFillData(AssetSkillMappingfilepath);
                DataTable dtfinalAssetSkillMapping = new DataTable();
                dtfinalAssetSkillMapping = dsAssetSkillMapping.Tables[0].Copy();
                dtfinalAssetSkillMapping.Clear();

                string missingdataforids = string.Empty;
                string idcolumn = "assetid";
                dtfinalAssetSkillMapping = HelperCommonMethods.ApplyCMDBusinessLogic(dsassetid, dsAssetSkillMapping, idcolumn, out missingdataforids);

                HelperCommonMethods.HideColumnsfromReportAssetSkillMapping(dtfinalAssetSkillMapping);
                dataGridView1.DataSource = dtfinalAssetSkillMapping;
                CreateExcelFile.CreateExcelDocument(dtfinalAssetSkillMapping, OutputAssetSkillMapping);

                if (!string.IsNullOrEmpty(missingdataforids))
                {
                    DataTable dtfinalSkillError = new DataTable("Errors");
                    dtfinalSkillError = HelperCommonMethods.GenerateDataTableForErrors(dtfinalSkillError, missingdataforids);
                    CreateExcelFile.CreateExcelDocument(dtfinalSkillError, ErrorOutputAssetSkillMapping);
                }

                //MessageBox.Show("Success!! \n\n You can find output file at- \n" + @"C:\Madhav\code\Automation-UserCMD\Automation-UserCMD\TestData\Outputs");
                HelperCommonMethods.SuccessErrorMessage(OutputAssetSkillMapping, missingdataforids);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred in processing" + ex.Message);
            }
        }


        private void ContentContainer_Click(object sender, EventArgs e)
        {
            try
            {
                //1. Get Directory
                string rootdir = System.IO.Directory.GetCurrentDirectory();
                rootdir = Path.GetDirectoryName(rootdir);
                rootdir = Path.GetDirectoryName(rootdir);


                string contentnameidsinputfilepath = rootdir + @"\TestData\Inputs\ContentContainer\ContentContainerNameIdsInput.xls";
                string ContentContainerfilepath = rootdir + @"\TestData\Inputs\ContentContainer\Contentcontainer.xls";
                string OutputContentContainer = rootdir + @"\TestData\Outputs\ContentContainerOutput.xlsx";
                string ErrorOutputContentContainer = rootdir + @"\TestData\Outputs\ContentContainerOutputError.xlsx";

                DataSet dscontentnameid = HelperCommonMethods.ReadExcelToFillData(contentnameidsinputfilepath);
                DataSet dsContentContainer = HelperCommonMethods.ReadExcelToFillData(ContentContainerfilepath);
                DataTable dtfinalContentContainer = new DataTable();
                dtfinalContentContainer = dsContentContainer.Tables[0].Copy();
                dtfinalContentContainer.Clear();

                string missingdataforids = string.Empty;
                string idcolumn = "name";
                dtfinalContentContainer = HelperCommonMethods.ApplyCMDBusinessLogic(dscontentnameid, dsContentContainer, idcolumn, out missingdataforids);

                HelperCommonMethods.HideColumnsfromReportContentContainer(dtfinalContentContainer);
                dataGridView1.DataSource = dtfinalContentContainer;
                CreateExcelFile.CreateExcelDocument(dtfinalContentContainer, OutputContentContainer);
                //MessageBox.Show("Success!! \n\n You can find output file at- \n" + @"C:\Madhav\code\Automation-UserCMD\Automation-UserCMD\TestData\Outputs");

                if (!string.IsNullOrEmpty(missingdataforids))
                {
                    DataTable dtfinalSkillError = new DataTable("Errors");
                    dtfinalSkillError = HelperCommonMethods.GenerateDataTableForErrors(dtfinalSkillError, missingdataforids);
                    CreateExcelFile.CreateExcelDocument(dtfinalSkillError, ErrorOutputContentContainer);
               }

                HelperCommonMethods.SuccessErrorMessage(OutputContentContainer, missingdataforids);
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error occurred in processing" + ex.Message);
            }
        }

        private void ContentContainerMapping_Click(object sender, EventArgs e)
        {
            try
            {
                //1. Get Directory
                string rootdir = System.IO.Directory.GetCurrentDirectory();
                rootdir = Path.GetDirectoryName(rootdir);
                rootdir = Path.GetDirectoryName(rootdir);

                string contentidsinputfilepath = rootdir + @"\TestData\Outputs\ContentContainerOutput.xlsx";
                string ContentContainerMappingfilepath = rootdir + @"\TestData\Inputs\ContentContainerMapping\ContentcontainerMapping.xls";
                string OutputContentContainerMapping = rootdir + @"\TestData\Outputs\ContentContainerMappingOutput.xlsx";
                string ErrorOutputContentContainerMapping = rootdir + @"\TestData\Outputs\ContentContainerMappingOutputError.xlsx";

                if (!File.Exists(contentidsinputfilepath))
                {
                    MessageBox.Show("Please execute Content Container cmd validation first to get content ids");

                }

                else
                {
                    DataSet dscontentnameid = HelperCommonMethods.ReadExcelToFillData(contentidsinputfilepath);
                    DataSet dsContentContainer = HelperCommonMethods.ReadExcelToFillData(ContentContainerMappingfilepath);
                    DataTable dtfinalContentContainer = new DataTable();
                    dtfinalContentContainer = dsContentContainer.Tables[0].Copy();
                    dtfinalContentContainer.Clear();

                    string missingdataforids = string.Empty;
                    string idcolumn = "containerid";
                    dtfinalContentContainer = HelperCommonMethods.ApplyCMDBusinessLogicForContentContainerMapping(dscontentnameid, dsContentContainer, idcolumn, out missingdataforids);

                    HelperCommonMethods.HideColumnsfromReportContentContainerMapping(dtfinalContentContainer);
                    dataGridView1.DataSource = dtfinalContentContainer;
                    CreateExcelFile.CreateExcelDocument(dtfinalContentContainer, OutputContentContainerMapping);

                    if (!string.IsNullOrEmpty(missingdataforids))
                    {
                        DataTable dtfinalSkillError = new DataTable("Errors");
                        dtfinalSkillError = HelperCommonMethods.GenerateDataTableForErrors(dtfinalSkillError, missingdataforids);
                        CreateExcelFile.CreateExcelDocument(dtfinalSkillError, ErrorOutputContentContainerMapping);
                    }

                    HelperCommonMethods.SuccessErrorMessage(OutputContentContainerMapping, missingdataforids);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred in processing" + ex.Message);
            }
        }

        private void btnAsset_Click(object sender, EventArgs e)
        {
            try
            {
                //1. Get Directory
                string rootdir = System.IO.Directory.GetCurrentDirectory();
                rootdir = Path.GetDirectoryName(rootdir);
                rootdir = Path.GetDirectoryName(rootdir);

                //1. Get file Paths
                string skillNameinputfilepath = rootdir + @"\TestData\Inputs\Skill\SkillNameInput.xls";
                string SkillMappingfilepath = rootdir + @"\TestData\Inputs\Skill\Skill.xls";
                string OutputSkill = rootdir + @"\TestData\Outputs\SkillOutput.xlsx";
                string ErrorOutputSkill = rootdir + @"\TestData\Outputs\SkillOutputError.xlsx";
                string missingdataforids = string.Empty;

                //2. Fill datasets
                DataSet dsskillName = HelperCommonMethods.ReadExcelToFillData(skillNameinputfilepath);
                DataSet dsSkillMapping = HelperCommonMethods.ReadExcelToFillData(SkillMappingfilepath);
                DataTable dtfinalSkill = new DataTable();
                string idcolumn = "name";

                //3. Apply Business Logic
                dtfinalSkill = HelperCommonMethods.ApplyCMDBusinessLogic(dsskillName, dsSkillMapping, idcolumn, out missingdataforids);

                //4. Hide not reqd. columns
                HelperCommonMethods.HideColumnsfromReportSkill(dtfinalSkill);
                dataGridView1.DataSource = dtfinalSkill;

                //5. Generate output Excel
                CreateExcelFile.CreateExcelDocument(dtfinalSkill, OutputSkill);

                if (!string.IsNullOrEmpty(missingdataforids))
                {
                    DataTable dtfinalSkillError = new DataTable("Errors");
                    dtfinalSkillError = HelperCommonMethods.GenerateDataTableForErrors(dtfinalSkillError, missingdataforids);
                    CreateExcelFile.CreateExcelDocument(dtfinalSkillError, ErrorOutputSkill);
                }
                //6. Show success failure message
                HelperCommonMethods.SuccessErrorMessage(OutputSkill, missingdataforids);
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error occurred in processing" + ex.Message);
            }
        }

        private void btnContent_Click(object sender, EventArgs e)
        {
            //1. Get Directory
            string rootdir = System.IO.Directory.GetCurrentDirectory();
            rootdir = Path.GetDirectoryName(rootdir);
            rootdir = Path.GetDirectoryName(rootdir);

            //1. Get file Paths
            string AssessmentIdsinputfilepath = rootdir + @"\TestData\Inputs\Content\AssessmentIdsInput.xls";
            string Contentfilepath = rootdir + @"\TestData\Inputs\Content\Content.xlsx";
            string OutputContent = rootdir + @"\TestData\Outputs\ContentOutput.xlsx";
            string ErrorOutputContent = rootdir + @"\TestData\Outputs\ContentOutputError.xlsx";
            string missingdataforids = string.Empty;

            //2. Fill datasets
            DataSet dsAssessmentIds = HelperCommonMethods.ReadExcelToFillData(AssessmentIdsinputfilepath);
            DataSet dsSContent = HelperCommonMethods.ReadExcelToFillData(Contentfilepath);
            DataTable dtfinalContent = new DataTable();
            string idcolumn = "id";

            //3. Apply Business Logic
            dtfinalContent = HelperCommonMethods.ApplyCMDBusinessLogic_Content(dsAssessmentIds, dsSContent, idcolumn, out missingdataforids);

            //4. Hide not reqd. columns
            HelperCommonMethods.HideColumnsfromReportContent(dtfinalContent);
            dataGridView1.DataSource = dtfinalContent;

            //5. Generate output Excel
            CreateExcelFile.CreateExcelDocument(dtfinalContent, OutputContent);

            if (!string.IsNullOrEmpty(missingdataforids))
            {
                DataTable dtfinalSkillError = new DataTable("Errors");
                dtfinalSkillError = HelperCommonMethods.GenerateDataTableForErrors(dtfinalSkillError, missingdataforids);
                CreateExcelFile.CreateExcelDocument(dtfinalSkillError, ErrorOutputContent);
            }
            //6. Show success failure message
            HelperCommonMethods.SuccessErrorMessage(OutputContent, missingdataforids);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmTincanValidations frmtincan = new FrmTincanValidations();
            frmtincan.Show();
        }

        private void btnFramework_Click(object sender, EventArgs e)
        {
            LblReqdInput.Text = "Enter grade name filter";
            LblReqdInput.Visible = true;
            tbReqdInput.Visible = true;
            Submit.Visible = true;
        }

        private void btnFramework_cmdMapping()
        {
            //1. Get File paths
            string rootdir = System.IO.Directory.GetCurrentDirectory();
            rootdir = Path.GetDirectoryName(rootdir);
            rootdir = Path.GetDirectoryName(rootdir);

            string classinput = rootdir + @"\TestData\Inputs\Framework\Framework.xlsx";
            string finalclass = rootdir + @"\TestData\Outputs\FrameworkOutput.xlsx";
            string Errorfinalclass = rootdir + @"\TestData\Outputs\FrameworkOutputError.xlsx";
            string erroroutput = string.Empty;

            //2. Read and fill datasets
            DataSet dsClassInput = HelperCommonMethods.ReadExcelToFillData(classinput);
            DataTable dtfinalClassOutput = new DataTable();

            DataTable dtclassInput = dsClassInput.Tables[0];
            string inputGradefilter = tbReqdInput.Text.ToString();

            //3. Apply cmd business logic for mapping and Fileds validation
            dtfinalClassOutput = HelperCommonMethods.ApplyCMDBusinessLogic_Framework(dtclassInput, inputGradefilter, out erroroutput);

            //4. Hide unwanted columns
            HelperCommonMethods.HideColumnsfromReportFramework(dtfinalClassOutput);
            dataGridView1.DataSource = dtfinalClassOutput;

            //5. Generate Output excel
            CreateExcelFile.CreateExcelDocument(dtfinalClassOutput, finalclass);

            if (!string.IsNullOrEmpty(erroroutput))
            {
                DataTable dtfinalSkillError = new DataTable("Errors");
                dtfinalSkillError = HelperCommonMethods.GenerateDataTableForErrors(dtfinalSkillError, erroroutput);
                CreateExcelFile.CreateExcelDocument(dtfinalSkillError, Errorfinalclass);
            }

            //6. Display success failure message
            HelperCommonMethods.SuccessErrorMessage(finalclass, erroroutput);

            //7. Hide Additional input controls
            LblReqdInput.Visible = false;
            tbReqdInput.Visible = false;
            Submit.Visible = false;
            lblClassFilter.Visible = false;
            txtClassFilter.Visible = false;
        }

        private void QuestionMetadata_Click(object sender, EventArgs e)
        {
            //1. Get Directory
            string rootdir = System.IO.Directory.GetCurrentDirectory();
            rootdir = Path.GetDirectoryName(rootdir);
            rootdir = Path.GetDirectoryName(rootdir);

            //1. Get file Paths
            string QuestionIdinputfilepath = rootdir + @"\TestData\Inputs\Question Metadata\QuestionIdsInput.xls";
            string QuestionMappingfilepath = rootdir + @"\TestData\Inputs\Question Metadata\Question.xlsx";
            string OutputQuestionMetadata = rootdir + @"\TestData\Outputs\QuestionMetadataOutput.xlsx";
            string ErrorOutputQuestionMetadata = rootdir + @"\TestData\Outputs\QuestionMetadataOutputError.xlsx";
            string missingdataforids = string.Empty;

            //2. Fill datasets
            DataSet dsQuestionid = HelperCommonMethods.ReadExcelToFillData(QuestionIdinputfilepath);
            DataSet dsQuestionMapping = HelperCommonMethods.ReadExcelToFillData(QuestionMappingfilepath);
            DataTable dtfinalQuesMetadata = new DataTable();
            string idcolumn = "questionid";

            //3. Apply Business Logic
            dtfinalQuesMetadata = HelperCommonMethods.ApplyCMDBusinessLogic_QuestionMetadata(dsQuestionid, dsQuestionMapping, idcolumn, out missingdataforids);

            //4. Hide not reqd. columns
            HelperCommonMethods.HideColumnsfromReportQuestionMetadata(dtfinalQuesMetadata);
            dataGridView1.DataSource = dtfinalQuesMetadata;

            //5. Generate output Excel
            CreateExcelFile.CreateExcelDocument(dtfinalQuesMetadata, OutputQuestionMetadata);

            if (!string.IsNullOrEmpty(missingdataforids))
            {
                DataTable dtfinalSkillError = new DataTable("Errors");
                dtfinalSkillError = HelperCommonMethods.GenerateDataTableForErrors(dtfinalSkillError, missingdataforids);
                CreateExcelFile.CreateExcelDocument(dtfinalSkillError, ErrorOutputQuestionMetadata);
            }
            //6. Show success failure message
            HelperCommonMethods.SuccessErrorMessage(OutputQuestionMetadata, missingdataforids);
        }
    }
}