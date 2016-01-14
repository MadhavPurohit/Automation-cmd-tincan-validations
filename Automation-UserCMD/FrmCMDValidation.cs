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

            AssignToolTipsToButtons();
        }

        ToolTip toolTip = new ToolTip();

        private void AssignToolTipsToButtons()
        {
            toolTip.SetToolTip(Users, "Make sure UserId input file is present");
            toolTip.SetToolTip(Class, "Please enter correct organization id and course");
            toolTip.SetToolTip(UserValidationCMD, "Make sure UserId input file is present");
            toolTip.SetToolTip(ClassProdMapping, "Make sure content id input file is present");
            toolTip.SetToolTip(AssetSkillMapping, "Make sure question id input file is present");
            toolTip.SetToolTip(ContentContainer, "Make sure content container name input file is present");
            toolTip.SetToolTip(ContentContainerMapping, "Make sure content container cmd is executed");
            toolTip.SetToolTip(btnSkill, "Make sure skill name input file is present");
            toolTip.SetToolTip(btnContent, "Make sure assessment id input file is present");
            toolTip.SetToolTip(QuestionMetadata, "Make sure question id input file is present");
            toolTip.SetToolTip(btnFramework, "Please enter correct grade name filter");
            toolTip.SetToolTip(btnOrganization, "Please make sure Organization Input file is present");

            toolTip.SetToolTip(tbGradeUser, "e.g. 5");
            toolTip.SetToolTip(tbStartSecUser, "e.g. 1");
            toolTip.SetToolTip(tbEndSecUser, "e.g. 3");
            toolTip.SetToolTip(txtClassFilter, "e.g. 0505");

        }

        private void Users_Click(object sender, EventArgs e)
        {
            UserCmdInputVisibility(true);
        }

        private void UserCmdInputVisibility(bool VisibilityInput)
        {
            lbheaderUser.Visible = VisibilityInput;
            lbGradeUser.Visible = VisibilityInput;
            lbStartSecUser.Visible = VisibilityInput;
            lbEndSecUser.Visible = VisibilityInput;
            tbGradeUser.Visible = VisibilityInput;
            tbStartSecUser.Visible = VisibilityInput;
            tbEndSecUser.Visible = VisibilityInput;
            btnSubmitUsers.Visible = VisibilityInput;
        }
        private void btnSubmitUsers_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbGradeUser.Text) || string.IsNullOrEmpty(tbStartSecUser.Text) || string.IsNullOrEmpty(tbEndSecUser.Text))
            {
                MessageBox.Show("Please enter inputs filter range");
            }

            else
            {
                try
                {
                    int Gradefilter = Int32.Parse(tbGradeUser.Text);
                    int StartSecfilter = Int32.Parse(tbStartSecUser.Text);
                    int EndSecfilter = Int32.Parse(tbEndSecUser.Text);
                    btnUser_cmdValidation();
                    UserCmdInputVisibility(false);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Please enter correct inputs as numeric values");
                }

            }
        }

        private void btnUser_cmdValidation()
        {

            try
            {

                int StartSecfilter = Int32.Parse(tbStartSecUser.Text);
                int EndSecfilter = Int32.Parse(tbEndSecUser.Text);

                int Gradefilter = Int32.Parse(tbGradeUser.Text);

                //HelperCommonMethods.charities();

                //1. Get Directory
                string rootdir = System.IO.Directory.GetCurrentDirectory();
                rootdir = Path.GetDirectoryName(rootdir);
                rootdir = Path.GetDirectoryName(rootdir);

                //2. Get corresponding file path
                string studentfilepath = rootdir + @"\TestData\Inputs\IdInputs\StudentIdsInput.xlsx";
                string userfilepath = rootdir + @"\TestData\Inputs\Users\User.xlsx";



                //3. Fill Datasets
                DataSet dsStudentid = HelperCommonMethods.ReadExcelToFillData(studentfilepath);
                DataSet dsUserEnrollment = HelperCommonMethods.ReadExcelToFillData(userfilepath, false);
                DataTable dtfinaluserenrolment = new DataTable();
                string missingdataforids = string.Empty;

                string OutputFileNameAppend = dsUserEnrollment.Tables[0].Rows[1][0].ToString().Replace(" ", "_").Replace("/", "-");
                string OutputUser = rootdir + @"\TestData\Outputs\Users-" + OutputFileNameAppend + ".xlsx";
                string ErrorOutputUser = rootdir + @"\TestData\Outputs\UsersError - " + OutputFileNameAppend + ".xlsx";



                DataRow[] drinpstudents = dsStudentid.Tables[0].Select("loginName like 'dummyRow'");


                DataTable userinfo = dsStudentid.Tables[0].Copy();
                userinfo.Clear();

                for (int i = StartSecfilter; i <= EndSecfilter; i++)
                {

                    if (i < 10)
                    {
                        drinpstudents = dsStudentid.Tables[0].Select("loginName like '%sec0" + i.ToString() + "%' and loginName like '%grd0" + Gradefilter.ToString() + "%'");
                    }

                    else
                    {
                        drinpstudents = dsStudentid.Tables[0].Select("loginName like '%sec" + i.ToString() + "%' and loginName like '%grd0" + Gradefilter.ToString() + "%'");
                    }

                    //if(drinpstudents.Count() > 0)
                    //{
                    //    missingdataforids+= "input users csv of schoolnet contains multi"
                    //}

                    foreach (DataRow dr in drinpstudents)
                        userinfo.ImportRow(dr);

                }


                //4. Apply Business Logic - Mapping & Validations
                string idcolumn = "id";
                dtfinaluserenrolment = HelperCommonMethods.ApplyCMDBusinessLogic_Users(userinfo, dsUserEnrollment, idcolumn, out missingdataforids);

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
            tbReqdInput.Text = string.Empty;
            Submit.Visible = true;

            
            toolTip.SetToolTip(tbReqdInput, "e.g. 3f02bd15-8514-4d72-9d37-f60532f01e5e - (PSOCElem) Refer Organization cmd");
        }


        private void Submit_Click(object sender, EventArgs e)
        {
            try
            {

                if (LblReqdInput.Visible == true && LblReqdInput.Text == "Enter grade number filter" && txtClassFilter.Visible == false)
                {
                    if (string.IsNullOrEmpty(tbReqdInput.Text))
                    {
                        MessageBox.Show("Please enter grade number filter input");
                    }

                    else
                    {
                        try
                        {
                            int GradeNumber = Int32.Parse(tbReqdInput.Text);
                            btnFramework_cmdMapping();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Please enter numeric as input");
                        }
                    }
                }

                else
                {
                    if (string.IsNullOrEmpty(tbReqdInput.Text) || string.IsNullOrEmpty(txtClassFilter.Text))
                    {
                        MessageBox.Show("Please enter correct inputs");
                    }

                    else
                    {
                        btnClass_cmdMapping();
                    }
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


            DownloadCSV.DownloadClassCMDCSV(classinput);


            string erroroutput = string.Empty;

            //2. Read and fill datasets
            DataSet dsClassInput = HelperCommonMethods.ReadExcelToFillData(classinput, false);
            DataTable dtfinalClassOutput = new DataTable();

            string OutputFileNameAppend = dsClassInput.Tables[0].Rows[0][0].ToString().Replace(" ", "_").Replace("/", "-");
            string finalclass = rootdir + @"\TestData\Outputs\ClassOutput - " + OutputFileNameAppend +".xlsx";
            string Errorfinalclass = rootdir + @"\TestData\Outputs\ClassOutputError - " + OutputFileNameAppend + ".xlsx";

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
                string studentfilepath = rootdir + @"\TestData\Inputs\IdInputs\StudentIdsInput.xlsx";
                string userEnrollmentfilepath = rootdir + @"\TestData\Inputs\UserEnrollment\UserEnrollment.xls";
                

                //OR - 1. Get File Path from AppSettings.config
                string[] filepaths = HelperCommonMethods.GetInputOutputFilePaths_UserValidation();
                string erroroutput = string.Empty;

                //2. Read excel and fill datasets
                DataSet dsStudentid = HelperCommonMethods.ReadExcelToFillData(studentfilepath);
                DataSet dsUserEnrollment = HelperCommonMethods.ReadExcelToFillData(userEnrollmentfilepath, false);
                DataTable dtfinaluserenrolment = new DataTable();

                string OutputFileNameAppend = dsUserEnrollment.Tables[0].Rows[0][0].ToString().Replace(" ", "_").Replace("/", "-");
                string OutputUserEnrollment = rootdir + @"\TestData\Outputs\UserEnrollmentOutput - " + OutputFileNameAppend + ".xlsx";
                string ErrorOutputUserEnrollment = rootdir + @"\TestData\Outputs\UserEnrollmentOutputError - " + OutputFileNameAppend + ".xlsx";

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

                // string containerinputfilepath = rootdir + @"\TestData\Inputs\IdInputs\ContainerIdsInput.xls";

                string containerinputfilepath = rootdir + @"\TestData\Outputs\ContentContainerOutput.xlsx";

                string classProductAssfilepath = rootdir + @"\TestData\Inputs\ClassProductMapping\ClassProductAsscociation.xls";
                

                if (!File.Exists(containerinputfilepath))
                {
                    MessageBox.Show("Please execute Content Container cmd validation first to get content ids");

                }

                else
                {
                    DataSet dscontainerid = HelperCommonMethods.ReadExcelToFillData(containerinputfilepath);
                    DataSet dsClassProdAssociation = HelperCommonMethods.ReadExcelToFillData(classProductAssfilepath, false);
                    string OutputFileNameAppend = dsClassProdAssociation.Tables[0].Rows[0][0].ToString().Replace(" ", "_").Replace("/", "-");
                    string OutputClassProdMapping = rootdir + @"\TestData\Outputs\ClassProdMappingOutput - " + OutputFileNameAppend + ".xlsx";
                    string ErrorOutputClassProdMapping = rootdir + @"\TestData\Outputs\ClassProdMappingOutputError - " + OutputFileNameAppend +".xlsx";                    
                    
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
                // string assetinputfilepath = rootdir + @"\TestData\Inputs\IdInputs\QuestionIdsInput.xls";

                string assetinputfilepath = rootdir + @"\TestData\Outputs\ContentOutput.xlsx";

                string AssetSkillMappingfilepath = rootdir + @"\TestData\Inputs\AssetSkillMapping\AssetSkillsMapping.xls";
                

                if (!File.Exists(assetinputfilepath))
                {
                    MessageBox.Show("Please execute Content cmd validation first to get content ids");
                }

                else
                {
                    DataSet dsassetid = HelperCommonMethods.ReadExcelToFillData(assetinputfilepath);
                    DataSet dsAssetSkillMapping = HelperCommonMethods.ReadExcelToFillData(AssetSkillMappingfilepath, false);
                    string OutputFileNameAppend = dsAssetSkillMapping.Tables[0].Rows[0][0].ToString().Replace(" ", "_").Replace("/", "-");
                    string OutputAssetSkillMapping = rootdir + @"\TestData\Outputs\AssetSkillMappingOutput - " + OutputFileNameAppend +".xlsx";
                    string ErrorOutputAssetSkillMapping = rootdir + @"\TestData\Outputs\AssetSkillMappingOutputError - " + OutputFileNameAppend + ".xlsx";



                    DataTable dtfinalAssetSkillMapping = new DataTable();
                    dtfinalAssetSkillMapping = dsAssetSkillMapping.Tables[0].Copy();
                    dtfinalAssetSkillMapping.Clear();

                    DataSet dsassetidnw = new DataSet();
                    dsassetidnw = HelperCommonMethods.FillNewDatasetForQuestion(dsassetid);

                    string missingdataforids = string.Empty;
                    string idcolumn = "assetid";
                    dtfinalAssetSkillMapping = HelperCommonMethods.ApplyCMDBusinessLogic_AssetSkill(dsassetidnw, dsAssetSkillMapping, idcolumn, out missingdataforids);

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


                string contentnameidsinputfilepath = rootdir + @"\TestData\Inputs\IdInputs\ContentContainerNameIdsInput.xls";
                string ContentContainerfilepath = rootdir + @"\TestData\Inputs\ContentContainer\Contentcontainer.xls";
                string OutputContentContainerForOtherMapping = rootdir + @"\TestData\Outputs\ContentContainerOutput.xlsx";

                DataSet dscontentnameid = HelperCommonMethods.ReadExcelToFillData(contentnameidsinputfilepath);
                DataSet dsContentContainer = HelperCommonMethods.ReadExcelToFillData(ContentContainerfilepath, false);
                string OutputFileNameAppend = dsContentContainer.Tables[0].Rows[0][0].ToString().Replace(" ", "_").Replace("/", "-");
                string OutputContentContainer = rootdir + @"\TestData\Outputs\ContentContainerOutput - " + OutputFileNameAppend + ".xlsx";
                string ErrorOutputContentContainer = rootdir + @"\TestData\Outputs\ContentContainerOutputError - " + OutputFileNameAppend + ".xlsx";


                DataTable dtfinalContentContainer = new DataTable();
                dtfinalContentContainer = dsContentContainer.Tables[0].Copy();
                dtfinalContentContainer.Clear();

                string missingdataforids = string.Empty;
                string idcolumn = "name";
                dtfinalContentContainer = HelperCommonMethods.ApplyCMDBusinessLogicForContentContainer(dscontentnameid, dsContentContainer, idcolumn, out missingdataforids);

                HelperCommonMethods.HideColumnsfromReportContentContainer(dtfinalContentContainer);
                dataGridView1.DataSource = dtfinalContentContainer;
                CreateExcelFile.CreateExcelDocument(dtfinalContentContainer, OutputContentContainer);
                CreateExcelFile.CreateExcelDocument(dtfinalContentContainer, OutputContentContainerForOtherMapping); 
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
                

                if (!File.Exists(contentidsinputfilepath))
                {
                    MessageBox.Show("Please execute Content Container cmd validation first to get content ids");

                }

                else
                {
                    DataSet dscontentnameid = HelperCommonMethods.ReadExcelToFillData(contentidsinputfilepath);
                    DataSet dsContentContainer = HelperCommonMethods.ReadExcelToFillData(ContentContainerMappingfilepath, false);
                    string OutputFileNameAppend = dsContentContainer.Tables[0].Rows[0][0].ToString().Replace(" ", "_").Replace("/", "-");
                    string OutputContentContainerMapping = rootdir + @"\TestData\Outputs\ContentContainerMappingOutput - " + OutputFileNameAppend + ".xlsx";
                    string ErrorOutputContentContainerMapping = rootdir + @"\TestData\Outputs\ContentContainerMappingOutputError - " + OutputFileNameAppend + ".xlsx";

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
                string skillNameinputfilepath = rootdir + @"\TestData\Inputs\IdInputs\SkillNameInput.xls";
                string SkillMappingfilepath = rootdir + @"\TestData\Inputs\Skill\Skill.xls";
                
                string missingdataforids = string.Empty;

                //2. Fill datasets
                DataSet dsskillName = HelperCommonMethods.ReadExcelToFillData(skillNameinputfilepath);
                DataSet dsSkillMapping = HelperCommonMethods.ReadExcelToFillData(SkillMappingfilepath, false);
                string OutputFileNameAppend = dsSkillMapping.Tables[0].Rows[0][0].ToString().Replace(" ", "_").Replace("/", "-");
                string OutputSkill = rootdir + @"\TestData\Outputs\SkillOutput - " + OutputFileNameAppend + ".xlsx";
                string ErrorOutputSkill = rootdir + @"\TestData\Outputs\SkillOutputError - " + OutputFileNameAppend +".xlsx";

                DataTable dtfinalSkill = new DataTable();
                string idcolumn = "name";

                //3. Apply Business Logic
                dtfinalSkill = HelperCommonMethods.ApplyCMDBusinessLogic_Skill(dsskillName, dsSkillMapping, idcolumn, out missingdataforids);

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
            string AssessmentIdsinputfilepath = rootdir + @"\TestData\Inputs\IdInputs\AssessmentIdsInput.xls";
            string Contentfilepath = rootdir + @"\TestData\Inputs\Content\Content.xls";
            string OutputContentForOtherMapping = rootdir + @"\TestData\Outputs\ContentOutput.xlsx";
            
            string missingdataforids = string.Empty;

            //2. Fill datasets
            DataSet dsAssessmentIds = HelperCommonMethods.ReadExcelToFillData(AssessmentIdsinputfilepath);
            DataSet dsSContent = HelperCommonMethods.ReadExcelToFillData(Contentfilepath, false);
            string OutputFileNameAppend = dsSContent.Tables[0].Rows[0][0].ToString().Replace(" ", "_").Replace("/", "-");
            string OutputContent = rootdir + @"\TestData\Outputs\ContentOutput - " + OutputFileNameAppend + ".xlsx";
            string ErrorOutputContent = rootdir + @"\TestData\Outputs\ContentOutputError - " + OutputFileNameAppend + ".xlsx";

            DataTable dtfinalContent = new DataTable();
            string idcolumn = "id";

            //3. Apply Business Logic
            dtfinalContent = HelperCommonMethods.ApplyCMDBusinessLogic_Content(dsAssessmentIds, dsSContent, idcolumn, out missingdataforids);

            //4. Hide not reqd. columns
            HelperCommonMethods.HideColumnsfromReportContent(dtfinalContent);
            dataGridView1.DataSource = dtfinalContent;

            //5. Generate output Excel
            CreateExcelFile.CreateExcelDocument(dtfinalContent, OutputContent);
            CreateExcelFile.CreateExcelDocument(dtfinalContent, OutputContentForOtherMapping);

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
            this.Close();
        }

        private void btnFramework_Click(object sender, EventArgs e)
        {
            LblReqdInput.Text = "Enter grade number filter";
            LblReqdInput.Visible = true;
            tbReqdInput.Visible = true;
            tbReqdInput.Text = string.Empty;

            Submit.Visible = true;
            txtClassFilter.Visible = false;
            lblClassFilter.Visible = false;


            toolTip.SetToolTip(tbReqdInput, "e.g. 5");
        }

        private void btnFramework_cmdMapping()
        {
            //1. Get File paths
            string rootdir = System.IO.Directory.GetCurrentDirectory();
            rootdir = Path.GetDirectoryName(rootdir);
            rootdir = Path.GetDirectoryName(rootdir);

            string classinput = rootdir + @"\TestData\Inputs\Framework\Framework.xls";
            
            string erroroutput = string.Empty;

            //2. Read and fill datasets
            DataSet dsClassInput = HelperCommonMethods.ReadExcelToFillData(classinput, false);
            string OutputFileNameAppend = dsClassInput.Tables[0].Rows[0][0].ToString().Replace(" ", "_").Replace("/", "-");
            string finalclass = rootdir + @"\TestData\Outputs\FrameworkOutput - " + OutputFileNameAppend + ".xlsx";
            string Errorfinalclass = rootdir + @"\TestData\Outputs\FrameworkOutputError - " + OutputFileNameAppend + ".xlsx";

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
            //string QuestionIdinputfilepath = rootdir + @"\TestData\Inputs\IdInputs\QuestionIdsInput.xls";
            string QuestionIdinputfilepath = rootdir + @"\TestData\Outputs\ContentOutput.xlsx";
            string QuestionMappingfilepath = rootdir + @"\TestData\Inputs\Question Metadata\Question.xls";
            
            string missingdataforids = string.Empty;

            if (!File.Exists(QuestionIdinputfilepath))
            {
                MessageBox.Show("Please execute Content cmd validation first to get content ids");
            }

            else
            {


                //2. Fill datasets
                DataSet dsQuestionid = HelperCommonMethods.ReadExcelToFillData(QuestionIdinputfilepath);
                DataSet dsQuestionMapping = HelperCommonMethods.ReadExcelToFillData(QuestionMappingfilepath, false);
                string OutputFileNameAppend = dsQuestionMapping.Tables[0].Rows[0][0].ToString().Replace(" ", "_").Replace("/", "-");
                string OutputQuestionMetadata = rootdir + @"\TestData\Outputs\QuestionMetadataOutput" + OutputFileNameAppend + ".xlsx";
                string ErrorOutputQuestionMetadata = rootdir + @"\TestData\Outputs\QuestionMetadataOutputError - " + OutputFileNameAppend + ".xlsx";


                DataTable dtfinalQuesMetadata = new DataTable();
                string idcolumn = "questionid";


                DataSet dsQuestionidnw = new DataSet();
                dsQuestionidnw = HelperCommonMethods.FillNewDatasetForQuestion(dsQuestionid);

                //3. Apply Business Logic
                dtfinalQuesMetadata = HelperCommonMethods.ApplyCMDBusinessLogic_QuestionMetadata(dsQuestionidnw, dsQuestionMapping, idcolumn, out missingdataforids);

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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            FrmAutomationSelector frmtincan = new FrmAutomationSelector();
            frmtincan.Show();
            this.Close();
        }

        private void btnOrganization_Click(object sender, EventArgs e)
        {
            //1. Get Directory
            string rootdir = System.IO.Directory.GetCurrentDirectory();
            rootdir = Path.GetDirectoryName(rootdir);
            rootdir = Path.GetDirectoryName(rootdir);

            //1. Get file Paths
            string skillNameinputfilepath = rootdir + @"\TestData\Inputs\IdInputs\OrganizationInput.xlsx";
            string SkillMappingfilepath = rootdir + @"\TestData\Inputs\Organization\Organization.xlsx";

            string missingdataforids = string.Empty;

            //2. Fill datasets
            DataSet dsskillName = HelperCommonMethods.ReadExcelToFillData(skillNameinputfilepath);
            DataSet dsSkillMapping = HelperCommonMethods.ReadExcelToFillData(SkillMappingfilepath, false);
            string OutputFileNameAppend = dsSkillMapping.Tables[0].Rows[1][0].ToString().Replace(" ", "_").Replace("/", "-");
            string OutputSkill = rootdir + @"\TestData\Outputs\SkillOutput - " + OutputFileNameAppend + ".xlsx";
            string ErrorOutputSkill = rootdir + @"\TestData\Outputs\SkillOutputError - " + OutputFileNameAppend + ".xlsx";

            DataTable dtfinalSkill = new DataTable();
            string idcolumn = "id";

            //3. Apply Business Logic
            dtfinalSkill = HelperCommonMethods.ApplyCMDBusinessLogic_Organization(dsskillName, dsSkillMapping, idcolumn, out missingdataforids);

            //4. Hide not reqd. columns
            HelperCommonMethods.HideColumnsfromReportOrganization(dtfinalSkill);
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

      
    }
}