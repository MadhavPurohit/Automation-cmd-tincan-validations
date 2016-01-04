using Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Automation_UserCMD
{
    public class HelperCommonMethods
    {

        public static DataSet ReadExcelToFillData(string filePath, bool IsFirstRowAsColumnNames = true)
        {

            FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read);

            if (Path.GetExtension(filePath).Equals(".xls"))
            {
                //1. Reading from a binary Excel file ('97-2003 format; *.xls)
                IExcelDataReader excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
                //...
                //2. Reading from a OpenXml Excel file (2007 format; *.xlsx)
                //IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                //...
                //3. DataSet - The result of each spreadsheet will be created in the result.Tables
                excelReader.IsFirstRowAsColumnNames = IsFirstRowAsColumnNames;

                DataSet result = excelReader.AsDataSet();
                //...
                ////4. DataSet - Create column names from first row
                //excelReader.IsFirstRowAsColumnNames = true;
                //DataSet result = excelReader.AsDataSet();

                //5. Data Reader methods
                while (excelReader.Read())
                {
                    //excelReader.GetInt32(0);
                }

                //6. Free resources (IExcelDataReader is IDisposable)
                excelReader.Close();

                if (!IsFirstRowAsColumnNames)
                {

                    foreach (DataColumn column in result.Tables[0].Columns)
                    {
                        string cName = result.Tables[0].Rows[3][column.ColumnName].ToString();
                        if (!result.Tables[0].Columns.Contains(cName) && cName != "")
                        {
                            column.ColumnName = cName;
                        }
                    }

                }

                return result;

            }

            else
            {
                IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                excelReader.IsFirstRowAsColumnNames = IsFirstRowAsColumnNames;
                DataSet result = excelReader.AsDataSet();
                //...
                ////4. DataSet - Create column names from first row
                //excelReader.IsFirstRowAsColumnNames = true;
                //DataSet result = excelReader.AsDataSet();

                //5. Data Reader methods
                while (excelReader.Read())
                {
                    //excelReader.GetInt32(0);
                }

                //6. Free resources (IExcelDataReader is IDisposable)
                excelReader.Close();

                if (!IsFirstRowAsColumnNames)
                {

                    foreach (DataColumn column in result.Tables[0].Columns)
                    {
                        string cName = result.Tables[0].Rows[3][column.ColumnName].ToString();
                        if (!result.Tables[0].Columns.Contains(cName) && cName != "")
                        {
                            column.ColumnName = cName;
                        }
                    }

                }


                return result;
            }
        }




        public static DataTable ApplyCMDBusinessLogic_AssetSkill(DataSet dsInputNameId, DataSet dsMappingcsv, string idcoulmn, out string missingdataforids)
        {
            DataTable dtfinalSkill = new DataTable();

            missingdataforids = string.Empty;
            dtfinalSkill = dsMappingcsv.Tables[0].Copy();
            dtfinalSkill.Clear();
            for (int i = 0; i < dsInputNameId.Tables[0].Rows.Count; i++)
            {
                DataTable dtassetid = dsInputNameId.Tables[0];
                DataTable dtAssetSkillAssoc = dsMappingcsv.Tables[0];

                string assetid = dsInputNameId.Tables[0].Rows[i][1].ToString();
                if (!string.IsNullOrWhiteSpace(assetid))
                {
                    DataRow[] drUserEnrlDataforstudent = dtAssetSkillAssoc.Select(idcoulmn + " = " + "'" + assetid + "'");
                    int thread = 0;
                    int item = 0;

                    if (drUserEnrlDataforstudent.Count() == 0)
                    {
                        //No data available for this particular id in mapping sheet
                        missingdataforids += "\n Data is missing for id - " + assetid.ToString() + ", ";

                    }

                    else if (drUserEnrlDataforstudent.Count() == 4)
                    {
                        foreach (DataRow dr in drUserEnrlDataforstudent)
                        {

                            if (thread > 1)
                            {
                                if (item == 1)
                                    dr[3] = " ";

                                missingdataforids += HelperCommonMethods.ValidateMandatoryAndReferenceItems_AssetSkill(dr, assetid);
                                missingdataforids += HelperCommonMethods.DoValidations_OperationPerformedEvent(dr, assetid);
                                dtfinalSkill.ImportRow(dr);
                                item++;
                            }

                            thread++;
                        }
                    }
                    else
                    {
                        item = 0;
                        foreach (DataRow dr in drUserEnrlDataforstudent)
                        {
                            if (item == 1)
                                dr[3] = " ";

                            missingdataforids += HelperCommonMethods.ValidateMandatoryAndReferenceItems_AssetSkill(dr, assetid);
                            missingdataforids += HelperCommonMethods.DoValidations_OperationPerformedEvent(dr, assetid);
                            dtfinalSkill.ImportRow(dr);
                            item++;
                        }
                    }
                }

            }

            return dtfinalSkill;
        }

        public static DataTable ApplyCMDBusinessLogic_Skill(DataSet dsInputNameId, DataSet dsMappingcsv, string idcoulmn, out string missingdataforids)
        {
            DataTable dtfinalSkill = new DataTable();

            missingdataforids = string.Empty;
            dtfinalSkill = dsMappingcsv.Tables[0].Copy();
            dtfinalSkill.Clear();
            for (int i = 0; i < dsInputNameId.Tables[0].Rows.Count; i++)
            {
                DataTable dtassetid = dsInputNameId.Tables[0];
                DataTable dtAssetSkillAssoc = dsMappingcsv.Tables[0];

                string assetid = dsInputNameId.Tables[0].Rows[i][0].ToString();
                if (!string.IsNullOrWhiteSpace(assetid))
                {
                    DataRow[] drUserEnrlDataforstudent = dtAssetSkillAssoc.Select(idcoulmn + " = " + "'" + assetid + "'");
                    int thread = 0;
                    int item = 0;

                    if (drUserEnrlDataforstudent.Count() == 0)
                    {
                        //No data available for this particular id in mapping sheet
                        missingdataforids += "\n Data is missing for id - " + assetid.ToString() + ", ";

                    }

                    else if (drUserEnrlDataforstudent.Count() == 4)
                    {
                        foreach (DataRow dr in drUserEnrlDataforstudent)
                        {

                            if (thread > 1)
                            {
                                if (item == 1)
                                    dr[3] = " ";

                                missingdataforids += HelperCommonMethods.ValidateMandatoryAndReferenceItems_Skill(dr, assetid);
                                missingdataforids += HelperCommonMethods.DoValidations_OperationPerformedEvent(dr, assetid);
                                dtfinalSkill.ImportRow(dr);
                                item++;
                            }

                            thread++;
                        }
                    }
                    else
                    {
                        item = 0;
                        foreach (DataRow dr in drUserEnrlDataforstudent)
                        {
                            if (item == 1)
                                dr[3] = " ";

                            missingdataforids += HelperCommonMethods.ValidateMandatoryAndReferenceItems_Skill(dr, assetid);
                            missingdataforids += HelperCommonMethods.DoValidations_OperationPerformedEvent(dr, assetid);
                            dtfinalSkill.ImportRow(dr);
                            item++;
                        }
                    }
                }

            }

            return dtfinalSkill;
        }

        private static string ValidateMandatoryAndReferenceItems_Skill(DataRow dr, string refid)
        {
            string errors = string.Empty;

            if (string.IsNullOrEmpty(dr["id"].ToString()))
            {
                errors += "\nMandatory column id is blank for - " + refid;
            }

            if (string.IsNullOrEmpty(dr["name"].ToString()))
            {
                errors += "\nMandatory column name is blank for - " + refid;
            }

            if (string.IsNullOrEmpty(dr["code"].ToString()))
            {
                errors += "\nMandatory column code is blank for - " + refid;
            }

            if (string.IsNullOrEmpty(dr["level"].ToString()))
            {
                errors += "\nMandatory column level is blank for - " + refid;
            }
            return errors;
        }

        private static string ValidateMandatoryAndReferenceItems_AssetSkill(DataRow dr, string refid)
        {
            string errors = string.Empty;

            if (string.IsNullOrEmpty(dr["id"].ToString()))
            {
                errors += "\nMandatory column id is blank for - " + refid;
            }

            if (string.IsNullOrEmpty(dr["skillid"].ToString()))
            {
                errors += "\nMandatory column skillframeworkid is blank for - " + refid;
            }

            return errors;
        }   

        public static DataTable ApplyCMDBusinessLogic_UserEnrollment(DataSet dsInputNameId, DataSet dsMappingcsv, string idcoulmn, out string missingdataforids)
        {
            DataTable dtfinalSkill = new DataTable();
            missingdataforids = string.Empty;

            dtfinalSkill = dsMappingcsv.Tables[0].Copy();
            dtfinalSkill.Clear();
            for (int i = 0; i < dsInputNameId.Tables[0].Rows.Count; i++)
            {
                DataTable dtassetid = dsInputNameId.Tables[0];
                DataTable dtAssetSkillAssoc = dsMappingcsv.Tables[0];

                string LastUserId=string.Empty;

                string assetid = dsInputNameId.Tables[0].Rows[i][0].ToString();
                if (!string.IsNullOrWhiteSpace(assetid))
                {
                    DataRow[] drUserEnrlDataforstudent = dtAssetSkillAssoc.Select(idcoulmn + " = " + "'" + assetid + "'");
                    int thread = 0;
                    int item = 0;

                    if (drUserEnrlDataforstudent.Count() == 0)
                    {
                        //No data available for this particular id in mapping sheet
                        missingdataforids += "\n Data is mssing for following input id" + assetid.ToString() + ", ";
                    }

                    else if (drUserEnrlDataforstudent.Count() == 4)
                    {
                        foreach (DataRow dr in drUserEnrlDataforstudent)
                        {
                            if (thread > 1)
                            {
                                missingdataforids += ValidateUniqueDetailsforData_Users(LastUserId, dr, assetid);
                                if (item == 1)
                                    dr[3] = " ";


                                missingdataforids += HelperCommonMethods.ValidateMandatoryAndReferenceItems_UserEnrollment(dr, assetid);
                                missingdataforids += HelperCommonMethods.DoValidations_OperationPerformedEvent(dr, assetid);

                                dtfinalSkill.ImportRow(dr);
                                item++;
                            }

                            thread++;
                        }
                    }
                    else
                    {
                        item = 0;
                        foreach (DataRow dr in drUserEnrlDataforstudent)
                        {
                            if (item == 1)
                                dr[3] = " ";

                            missingdataforids += HelperCommonMethods.ValidateMandatoryAndReferenceItems_UserEnrollment(dr, assetid);
                            missingdataforids += HelperCommonMethods.DoValidations_OperationPerformedEvent(dr, assetid);
                            dtfinalSkill.ImportRow(dr);
                            item++;
                        }
                    }
                }

            }

            return dtfinalSkill;
        }



        public static DataTable ApplyCMDBusinessLogicForContentContainer(DataSet dsinputNameId, DataSet dsMappingcsv, string idcoulmn, out string missingdataforids)
        {
            DataTable dtfinalSkill = new DataTable();
            missingdataforids = string.Empty;
            dtfinalSkill = dsMappingcsv.Tables[0].Copy();
            dtfinalSkill.Clear();
            for (int i = 0; i < dsinputNameId.Tables[0].Rows.Count; i++)
            {
                DataTable dtassetid = dsinputNameId.Tables[0];
                DataTable dtAssetSkillAssoc = dsMappingcsv.Tables[0];
                string assetid = dsinputNameId.Tables[0].Rows[i][0].ToString();
                if (!string.IsNullOrWhiteSpace(assetid))
                {
                    DataRow[] drUserEnrlDataforstudent = dtAssetSkillAssoc.Select(idcoulmn + " = " + "'" + assetid + "'");
                    int thread = 0;
                    int item = 0;

                    if (drUserEnrlDataforstudent.Count() == 0)
                    {
                        //No data available for this particular id in mapping sheet
                        missingdataforids += "\n Data is missing for the id" + assetid.ToString() + ", ";
                    }



                    else if (drUserEnrlDataforstudent.Count() == 4)
                    {
                        foreach (DataRow dr in drUserEnrlDataforstudent)
                        {

                            if (thread > 1)
                            {
                                if (item == 1)
                                    dr[3] = " ";

                                missingdataforids += HelperCommonMethods.ValidateMandatoryAndReferenceItems_ContentContainer(dr, assetid);
                                dtfinalSkill.ImportRow(dr);
                                item++;
                            }

                            thread++;
                        }
                    }
                    else
                    {
                        item = 0;
                        foreach (DataRow dr in drUserEnrlDataforstudent)
                        {
                            if (item == 1)
                                dr[3] = " ";

                            missingdataforids += HelperCommonMethods.ValidateMandatoryAndReferenceItems_ContentContainer(dr, assetid);
                            dtfinalSkill.ImportRow(dr);
                            item++;
                        }
                    }
                }

            }

            return dtfinalSkill;
        }

        public static DataTable ApplyCMDBusinessLogicForContentContainerMapping(DataSet dsinputNameId, DataSet dsMappingcsv, string idcoulmn, out string missingdataforids)
        {
            DataTable dtfinalSkill = new DataTable();
            missingdataforids = string.Empty;
            dtfinalSkill = dsMappingcsv.Tables[0].Copy();
            dtfinalSkill.Clear();
            for (int i = 0; i < dsinputNameId.Tables[0].Rows.Count; i++)
            {
                DataTable dtassetid = dsinputNameId.Tables[0];
                DataTable dtAssetSkillAssoc = dsMappingcsv.Tables[0];
                string assetid = dsinputNameId.Tables[0].Rows[i][1].ToString();
                if (!string.IsNullOrWhiteSpace(assetid))
                {
                    DataRow[] drUserEnrlDataforstudent = dtAssetSkillAssoc.Select(idcoulmn + " = " + "'" + assetid + "'");
                    int thread = 0;
                    int item = 0;

                    if (drUserEnrlDataforstudent.Count() == 0)
                    {
                        //No data available for this particular id in mapping sheet
                        missingdataforids += "\n Data is missing for the id" + assetid.ToString() + ", ";
                    }



                    else if (drUserEnrlDataforstudent.Count() == 4)
                    {
                        foreach (DataRow dr in drUserEnrlDataforstudent)
                        {

                            if (thread > 1)
                            {
                                if (item == 1)
                                    dr[3] = " ";

                                missingdataforids += HelperCommonMethods.ValidateMandatoryAndReferenceItems_ContentContainerMapping(dr, assetid);
                                missingdataforids += HelperCommonMethods.DoValidations_OperationPerformedEvent(dr, assetid);
                                dtfinalSkill.ImportRow(dr);
                                item++;
                            }

                            thread++;
                        }
                    }
                    else
                    {
                        item = 0;
                        foreach (DataRow dr in drUserEnrlDataforstudent)
                        {
                            if (item == 1)
                                dr[3] = " ";

                            missingdataforids += HelperCommonMethods.ValidateMandatoryAndReferenceItems_ContentContainerMapping(dr, assetid);
                            missingdataforids += HelperCommonMethods.DoValidations_OperationPerformedEvent(dr, assetid);
                            dtfinalSkill.ImportRow(dr);
                            item++;
                        }
                    }
                }

            }

            return dtfinalSkill;
        }

        private static string ValidateMandatoryAndReferenceItems_ContentContainerMapping(DataRow dr, string refid)
        {
            string errors = string.Empty;

            if (string.IsNullOrEmpty(dr["contentcontainermappingid"].ToString()))
            {
                errors += "\nMandatory column contentcontainermappingid is blank for - " + refid;
            }

            //if (string.IsNullOrEmpty(dr["containerid"].ToString()))
            //{
            //    errors += "\nMandatory column name is blank for id - " + dr["id"].ToString();
            //}

            //if (string.IsNullOrEmpty(dr["containertype"].ToString()))
            //{
            //    errors += "\nMandatory column name is blank for containerid - " + dr["containerid"].ToString();
            //}

            return errors;
        }

        private static string ValidateMandatoryAndReferenceItems_ContentContainer(DataRow dr, string refid)
        {
            string errors = string.Empty;

            if (string.IsNullOrEmpty(dr["id"].ToString()))
            {
                errors += "\nMandatory column id is blank for - " + refid;
            }

            if (string.IsNullOrEmpty(dr["name"].ToString()))
            {
                errors += "\nMandatory column name is blank for - " + refid;
            }

            if (string.IsNullOrEmpty(dr["type"].ToString()))
            {
                errors += "\nMandatory column type is blank for - " + refid;
            }

            return errors;
        }



        internal static void SuccessErrorMessage(string OutputUserEnrollment, string missingdataforids)
        {
            if (!string.IsNullOrEmpty(missingdataforids))
            {
                MessageBox.Show("Error!! \n\n Could not find any data for the following ids - " + missingdataforids + "\n\n You can find output file at- \n" + OutputUserEnrollment);
            }

            else
            {
                MessageBox.Show("Success!! \n\n You can find output file at- \n" + OutputUserEnrollment);
            }
        }




        internal static string ValidateMandatoryAndReferenceItems_Class(DataRow dr, string refid)
        {
            string errors = string.Empty;

            if (string.IsNullOrEmpty(dr["id"].ToString()))
            {
                errors += "\nMandatory column id is blank for - " + refid;
            }

            if (string.IsNullOrEmpty(dr["name"].ToString()))
            {
                errors += "\nMandatory column name is blank for - " + refid;
            }

            

            return errors;
        }


        public static string ValidateMandatoryAndReferenceItems_UserEnrollment(DataRow dr, string refid)
        {
            string errors = string.Empty;

            if (string.IsNullOrEmpty(dr["id"].ToString()))
            {
                errors += "\nMandatory column id is blank for - " + refid;
            }

            if (string.IsNullOrEmpty(dr["userid"].ToString()))
            {
                errors += "\nMandatory column userid is blank for - " + refid;
            }

            if (string.IsNullOrEmpty(dr["role"].ToString()))
            {
                errors += "\nMandatory column role is blank for - " + refid;
            }

            if (string.IsNullOrEmpty(dr["roletype"].ToString()))
            {
                errors += "\nMandatory column roletype is blank for - " + refid;
            }
            return errors;
        }

        #region HideColumns
        public static void HideColumnsfromReportUsersEnrollment(DataTable dtfinaluserenrolment)
        {
            dtfinaluserenrolment.Columns.Remove("client_id");
            //dtfinaluserenrolment.Columns.Remove("enrolledtotype");
            dtfinaluserenrolment.Columns.Remove("startdatetime");
            dtfinaluserenrolment.Columns.Remove("enddatetime");
            dtfinaluserenrolment.Columns.Remove("status");
            dtfinaluserenrolment.Columns.Remove("statementid");
            dtfinaluserenrolment.Columns.Remove("accesstoken");
            //dtfinaluserenrolment.Columns.Remove("column16");
            //dtfinaluserenrolment.Columns.Remove("column17");
            dtfinaluserenrolment.Columns.Remove("rawjson");
        }

        public static void HideColumnsfromReportClass(DataTable dtfinalClassOutput)
        {
            dtfinalClassOutput.Columns.Remove("client_id");
            dtfinalClassOutput.Columns.Remove("startdatetime");
            dtfinalClassOutput.Columns.Remove("enddatetime");
            dtfinalClassOutput.Columns.Remove("status");
            dtfinalClassOutput.Columns.Remove("statementid");
            dtfinalClassOutput.Columns.Remove("accesstoken");
            dtfinalClassOutput.Columns.Remove("rawjson");
        }

        internal static void HideColumnsfromReportClassProdMapping(DataTable dtfinalClassProdMapping)
        {
            dtfinalClassProdMapping.Columns.Remove("client_id");
            dtfinalClassProdMapping.Columns.Remove("startdatetime");
            dtfinalClassProdMapping.Columns.Remove("enddatetime");
            dtfinalClassProdMapping.Columns.Remove("status");
            dtfinalClassProdMapping.Columns.Remove("statementid");
            dtfinalClassProdMapping.Columns.Remove("accesstoken");
            dtfinalClassProdMapping.Columns.Remove("rawjson");
            dtfinalClassProdMapping.Columns.Remove("operationperformedby");
            dtfinalClassProdMapping.Columns.Remove("operationperformeddatetime");

        }

        internal static void HideColumnsfromReportAssetSkillMapping(DataTable dtfinalAssetSkillMapping)
        {
            dtfinalAssetSkillMapping.Columns.Remove("skillframeworkid");
            dtfinalAssetSkillMapping.Columns.Remove("type");
            dtfinalAssetSkillMapping.Columns.Remove("client_id");
            dtfinalAssetSkillMapping.Columns.Remove("status");
            dtfinalAssetSkillMapping.Columns.Remove("statementid");
            dtfinalAssetSkillMapping.Columns.Remove("accesstoken");
            dtfinalAssetSkillMapping.Columns.Remove("rawjson");
            dtfinalAssetSkillMapping.Columns.Remove("operationperformedby");
            dtfinalAssetSkillMapping.Columns.Remove("operationperformeddatetime");
        }

        internal static void HideColumnsfromReportContentContainer(DataTable dtfinalContentContainer)
        {
            dtfinalContentContainer.Columns.Remove("client_id");
            dtfinalContentContainer.Columns.Remove("ClassID");
            dtfinalContentContainer.Columns.Remove("OrganizationID");
            dtfinalContentContainer.Columns.Remove("statementid");
            dtfinalContentContainer.Columns.Remove("accesstoken");
            dtfinalContentContainer.Columns.Remove("rawjson");
            dtfinalContentContainer.Columns.Remove("operationperformedby");
            dtfinalContentContainer.Columns.Remove("operationperformeddatetime");
        }

        internal static void HideColumnsfromReportContentContainerMapping(DataTable dtfinalContentContainer)
        {
            dtfinalContentContainer.Columns.Remove("client_id");
            dtfinalContentContainer.Columns.Remove("ext_realize_producttitle");
            dtfinalContentContainer.Columns.Remove("statementid");
            dtfinalContentContainer.Columns.Remove("accesstoken");
            dtfinalContentContainer.Columns.Remove("rawjson");
            //dtfinalContentContainer.Columns.Remove("operationperformedby");
            //dtfinalContentContainer.Columns.Remove("operationperformeddatetime");
        }

        internal static void HideColumnsfromReportUsersMapping(DataTable dtfinaluserenrolment)
        {
            dtfinaluserenrolment.Columns.Remove("client_id");
            //dtfinaluserenrolment.Columns.Remove("enrolledtotype");
            dtfinaluserenrolment.Columns.Remove("status");
            dtfinaluserenrolment.Columns.Remove("statementid");
            dtfinaluserenrolment.Columns.Remove("accesstoken");
            dtfinaluserenrolment.Columns.Remove("title");
            dtfinaluserenrolment.Columns.Remove("loginname");
            dtfinaluserenrolment.Columns.Remove("rawjson");
            dtfinaluserenrolment.Columns.Remove("email");
            dtfinaluserenrolment.Columns.Remove("gender");
            dtfinaluserenrolment.Columns.Remove("dateofbirth");
            dtfinaluserenrolment.Columns.Remove("city");
            dtfinaluserenrolment.Columns.Remove("statecode");
            dtfinaluserenrolment.Columns.Remove("country");
            dtfinaluserenrolment.Columns.Remove("timezone");
            dtfinaluserenrolment.Columns.Remove("state");
            dtfinaluserenrolment.Columns.Remove("type");
            dtfinaluserenrolment.Columns.Remove("operationperformedby");
            dtfinaluserenrolment.Columns.Remove("operationperformeddatetime");
        }

        public static void HideColumnsfromReportSkill(DataTable dtfinalSkill)
        {
            dtfinalSkill.Columns.Remove("operation");
            //dtfinaluserenrolment.Columns.Remove("enrolledtotype");
            dtfinalSkill.Columns.Remove("client_id");
            dtfinalSkill.Columns.Remove("description");
            dtfinalSkill.Columns.Remove("code");
            dtfinalSkill.Columns.Remove("frameworkname");
            dtfinalSkill.Columns.Remove("sequence");
            dtfinalSkill.Columns.Remove("status");
            dtfinalSkill.Columns.Remove("grade");
            dtfinalSkill.Columns.Remove("subject");
            dtfinalSkill.Columns.Remove("rawjson");
            dtfinalSkill.Columns.Remove("statementid");
            dtfinalSkill.Columns.Remove("accesstoken");

        }

        #endregion


        internal static DataTable ApplyCMDBusinessLogic_Class(DataTable dtclassInput, string tbReqdInput, string inputclassfilter, out string erroroutput)
        {
            DataTable dtfinalClassOutput = new DataTable();
            dtfinalClassOutput = dtclassInput.Copy();
            dtfinalClassOutput.Clear();
            erroroutput = string.Empty;

            string Queryfilter = "organizationid = " + "'" + tbReqdInput + "'" + " and name like " + "'%" + inputclassfilter + "%'";

            DataRow[] drclassInput = dtclassInput.Select(Queryfilter);
            foreach (DataRow dr in drclassInput)
            {
                erroroutput += HelperCommonMethods.ValidateMandatoryAndReferenceItems_Class(dr, tbReqdInput);
                erroroutput += HelperCommonMethods.DoOtherValidations_Class(dr, tbReqdInput);
                dtfinalClassOutput.ImportRow(dr);
            }

            return dtfinalClassOutput;
        }

        private static string DoOtherValidations_Class(DataRow dr, string refid)
        {
            string errors = string.Empty;

            if (dr["name"].ToString().Contains('@') || dr["name"].ToString().Contains('%') || dr["name"].ToString().Contains('>') || dr["name"].ToString().Contains('<'))
            {
                errors += "Weird characters in column name - " + refid;
            }

            if (string.IsNullOrEmpty(dr["operation"].ToString()))
            {
                errors += "Operation column is blank for - " + refid;
            }

            else if (dr["operation"].ToString() != "update")
            {
                errors += "Operation is not update for -" + refid;
            }

            return errors;
        }

        internal static DataTable ApplyCMDBusinessLogic_ClassProdMapping(DataSet dsInputNameId, DataSet dsMappingcsv, string idcolumn, out string missingdataforids)
        {
            DataTable dtfinalSkill = new DataTable();
            missingdataforids = string.Empty;
            dtfinalSkill = dsMappingcsv.Tables[0].Copy();
            dtfinalSkill.Clear();
            for (int i = 0; i < dsInputNameId.Tables[0].Rows.Count; i++)
            {
                DataTable dtassetid = dsInputNameId.Tables[0];
                DataTable dtAssetSkillAssoc = dsMappingcsv.Tables[0];


                string assetid = dsInputNameId.Tables[0].Rows[i][1].ToString();
                if (!string.IsNullOrWhiteSpace(assetid))
                {
                    DataRow[] drUserEnrlDataforstudent = dtAssetSkillAssoc.Select(idcolumn + " = " + "'" + assetid + "'");
                    int thread = 0;
                    int item = 0;

                    if (drUserEnrlDataforstudent.Count() == 0)
                    {
                        //No data available for this particular id in mapping sheet
                        missingdataforids += "\n Data is missing for the id" + assetid.ToString() + ", ";
                    }



                    else if (drUserEnrlDataforstudent.Count() == 4)
                    {
                        foreach (DataRow dr in drUserEnrlDataforstudent)
                        {

                            if (thread > 1)
                            {
                                if (item == 1)
                                    dr[3] = " ";

                                missingdataforids += HelperCommonMethods.ValidateMandatoryAndReferenceItems_ClassProdMapping(dr, assetid);
                                missingdataforids += HelperCommonMethods.DoValidations_OperationPerformedEvent(dr, assetid);
                                dtfinalSkill.ImportRow(dr);
                                item++;
                            }

                            thread++;
                        }
                    }
                    else
                    {
                        item = 0;
                        foreach (DataRow dr in drUserEnrlDataforstudent)
                        {
                            if (item == 1)
                                dr[3] = " ";

                            missingdataforids += HelperCommonMethods.ValidateMandatoryAndReferenceItems_ClassProdMapping(dr, assetid);
                            missingdataforids += HelperCommonMethods.DoValidations_OperationPerformedEvent(dr, assetid);

                            dtfinalSkill.ImportRow(dr);
                            item++;
                        }
                    }
                }

            }

            return dtfinalSkill;
        }

        private static string ValidateMandatoryAndReferenceItems_ClassProdMapping(DataRow dr, string refid)
        {
            string errors = string.Empty;

            if (string.IsNullOrEmpty(dr["id"].ToString()))
            {
                errors += "\nMandatory column id is blank for - " + refid;
            }

            return errors;
        }



        internal static string[] GetInputOutputFilePaths_UserValidation()
        {
            string[] FilePaths = new string[5];
            //string studentfilepath = System.Configuration.ConfigurationSettings.AppSettings["InputStudentId"].ToString();
            //string userEnrollmentfilepath = System.Configuration.ConfigurationSettings.AppSettings["InputUserEnrollment"].ToString();
            //string OutputUserEnrollment = System.Configuration.ConfigurationSettings.AppSettings["OutputUserEnrollment"].ToString();
            return FilePaths;
        }

        internal static DataTable ApplyCMDBusinessLogic_Content(DataSet dsInputNameId, DataSet dsMappingcsv, string idcolumn, out string missingdataforids)
        {
            DataTable dtfinalSkill = new DataTable();
            missingdataforids = string.Empty;
            dtfinalSkill = dsMappingcsv.Tables[0].Copy();
            dtfinalSkill.Clear();

            for (int i = 0; i < dsInputNameId.Tables[0].Rows.Count; i++)
            {
                DataTable dtassetid = dsInputNameId.Tables[0];
                DataTable dtAssetSkillAssoc = dsMappingcsv.Tables[0];

                //MP - As above datatable is treating Blob columns as Double instead of string - implemented following change
                dtAssetSkillAssoc = ConvertDatatbleColumnsToString(dtAssetSkillAssoc, 2);


                string assetid = dsInputNameId.Tables[0].Rows[i][0].ToString();
                if (!string.IsNullOrWhiteSpace(assetid))
                {
                    DataRow[] drUserEnrlDataforstudent = dtAssetSkillAssoc.Select(idcolumn + " = " + "'" + assetid.ToString() + "'");

                    if (drUserEnrlDataforstudent.Count() == 0)
                    {
                        //No data available for this particular id in mapping sheet
                        missingdataforids += "\n Data is missing for the id" + assetid.ToString() + ", ";
                    }

                    else if (drUserEnrlDataforstudent.Count() > 1)
                    {
                        missingdataforids += "\nThere are multiple rows available for single assessemnt id - " + assetid;
                    }



                //else if (drUserEnrlDataforstudent.Count() == 4)
                    //{
                    //    foreach (DataRow dr in drUserEnrlDataforstudent)
                    //    {

                //        if (thread > 1)
                    //        {
                    //            if (item == 1)
                    //                dr[3] = " ";

                //            missingdataforids += HelperCommonMethods.ValidateMandatoryAndReferenceItems_ClassProdMapping(dr);
                    //            dtfinalSkill.ImportRow(dr);
                    //            item++;
                    //        }

                //        thread++;
                    //    }
                    //}
                    else
                    {
                        //BTW - It will be only 1 row
                        foreach (DataRow dr in drUserEnrlDataforstudent)
                        {
                            //missingdataforids += HelperCommonMethods.ValidateMandatoryAndReferenceItems_Content(dr);
                            string[] quesid = new string[50];
                            //search for children
                            string Childrendata = dr["children"].ToString().Replace("\"", " ");
                            string[] childrenrow = Childrendata.Split(new string[] { "id" }, StringSplitOptions.None);
                            int quesidcount = 0;
                            for (int k = 0; k < childrenrow.Count(); k++)
                            {
                                if ((k >= 1))
                                {
                                    string[] quesdata = childrenrow[k].Split(',');
                                    quesid[quesidcount] = quesdata[0].Replace(" : ", "");
                                    quesidcount++;
                                }
                            }
                            //Suppose once we have child id in childrenrow

                            //1. Added Child
                            for (int c = 0; c < quesid.Count(); c++)
                            {
                                if (string.IsNullOrEmpty(quesid[c]))
                                    break;

                                DataRow[] drItemData = dtAssetSkillAssoc.Select(idcolumn + " = " + "'" + quesid[c] + "'");
                                dtfinalSkill.ImportRow(drItemData[0]);
                            }

                            //2. Add AssessmentRow
                            dtfinalSkill.ImportRow(dr);
                            assetid = assetid.Replace(" ", "");
                            //3. Search for Assessment Parent in children column
                                DataRow[] drAssessmentParent = dtAssetSkillAssoc.Select("children" + " like " + "'%" + assetid.Replace(" ","") + "%'");

                                //Need to confirm - 1 assessment may have multip parents - considerning 1 parent as of now
                                dtfinalSkill.ImportRow(drAssessmentParent[0]);

                                //4. Now move on to find parents of the hierarchy till Unit
                                string parent = drAssessmentParent[0]["parents"].ToString().Replace("\"", " ");
                                for (int parentcount = 0; parentcount < 5; parentcount++)
                                {
                                    //Apply Logic to get parent guid from string
                                    string[] parents = null;
                                    if (!(string.IsNullOrEmpty(parent) || string.IsNullOrWhiteSpace(parent) || parent.ToString() == "System.Data.DataRow"))
                                    {
                                        parents = parent.Split(new string[] { "id : " }, StringSplitOptions.None);
                                        parents = parents[1].Split(new string[] { " ," }, StringSplitOptions.None);
                                        parent = parents[0].ToString();
                                        DataRow[] drItemParent = dtAssetSkillAssoc.Select(idcolumn + " = " + "'" + parent.ToString() + "'");
                                        dtfinalSkill.ImportRow(drItemParent[0]);
                                        parent = drItemParent[0]["parents"].ToString().Replace("\"", " ");
                                    }
                                    if (string.IsNullOrEmpty(parent) || string.IsNullOrWhiteSpace(parent) || parent.ToString() == "System.Data.DataRow")
                                    {
                                        if (parents.Count() > 0)
                                        {
                                            if (!parents[1].Contains("Unit"))
                                            {
                                                missingdataforids += "\n Unit is not High-Level Parent for " + assetid.ToString() + ", ";
                                            }
                                        }
                                        break;
                                    }
                                }
                        }
                    }
                }

            }

            return dtfinalSkill;
        }

        private static DataTable ConvertDatatbleColumnsToString(DataTable dtAssetSkillAssoc, int ColumnNo)
        {
            DataTable dtCloned = dtAssetSkillAssoc.Clone();
            dtCloned.Columns[ColumnNo].DataType = typeof(string);
            foreach (DataRow row in dtAssetSkillAssoc.Rows)
            {
                dtCloned.ImportRow(row);
            }

            return dtCloned;
        }

        internal static void HideColumnsfromReportContent(DataTable dtfinalContent)
        {
            dtfinalContent.Columns.Remove("client_id");
            dtfinalContent.Columns.Remove("status");
            dtfinalContent.Columns.Remove("includeinmastery");
            dtfinalContent.Columns.Remove("children");
            dtfinalContent.Columns.Remove("statementid");
            dtfinalContent.Columns.Remove("rawjson");
            dtfinalContent.Columns.Remove("AccessToken");
            //dtfinalContent.Columns.Remove("operationperformeddatetime");
        }

        internal static DataTable ApplyCMDBusinessLogic_Framework(DataTable dtclassInput, string inputGradefilter, out string erroroutput)
        {
            DataTable dtfinalClassOutput = new DataTable();
            dtfinalClassOutput = dtclassInput.Copy();
            dtfinalClassOutput.Clear();
            erroroutput = string.Empty;

            string Queryfilter = "name like " + "'%" + inputGradefilter + "%'";

            DataRow[] drclassInput = dtclassInput.Select(Queryfilter);
            foreach (DataRow dr in drclassInput)
            {
                erroroutput += HelperCommonMethods.ValidateMandatoryAndReferenceItems_Framework(dr, inputGradefilter);
                dtfinalClassOutput.ImportRow(dr);
            }

            return dtfinalClassOutput;
        }

        private static string ValidateMandatoryAndReferenceItems_Framework(DataRow dr, string refid)
        {
            string errors = string.Empty;

            if (string.IsNullOrEmpty(dr["id"].ToString()))
            {
                errors += "\nMandatory column id is blank for search filter - " + refid;
            }

            if (string.IsNullOrEmpty(dr["name"].ToString()))
            {
                errors += "\nMandatory column name is blank for search filter - " + refid;
            }

            if (string.IsNullOrEmpty(dr["type"].ToString()))
            {
                errors += "\nMandatory column type is blank for search filter - " + refid;
            }

            if (string.IsNullOrEmpty(dr["grade"].ToString()))
            {
                errors += "\nMandatory column grade is blank for search filter - " + refid;
            }
            if (string.IsNullOrEmpty(dr["subject"].ToString()))
            {
                errors += "\nMandatory column subject is blank for search filter - " + refid;
            }
            

            return errors;
        }

        internal static void HideColumnsfromReportFramework(DataTable dtfinalFrameworkOutput)
        {
            dtfinalFrameworkOutput.Columns.Remove("client_id");
            dtfinalFrameworkOutput.Columns.Remove("statementid");
            dtfinalFrameworkOutput.Columns.Remove("rawjson");
            dtfinalFrameworkOutput.Columns.Remove("AccessToken");
        }

        internal static DataTable ApplyCMDBusinessLogic_QuestionMetadata(DataSet dsInputNameId, DataSet dsMappingcsv, string idcolumn, out string missingdataforids)
        {
            DataTable dtfinalSkill = new DataTable();
            missingdataforids = string.Empty;
            dtfinalSkill = dsMappingcsv.Tables[0].Copy();
            dtfinalSkill.Clear();
            for (int i = 0; i < dsInputNameId.Tables[0].Rows.Count; i++)
            {
                DataTable dtassetid = dsInputNameId.Tables[0];
                DataTable dtAssetSkillAssoc = dsMappingcsv.Tables[0];
                string assetid = dsInputNameId.Tables[0].Rows[i][1].ToString();
                if (!string.IsNullOrWhiteSpace(assetid))
                {
                    DataRow[] drUserEnrlDataforstudent = dtAssetSkillAssoc.Select(idcolumn + " = " + "'" + assetid + "'");
                    int thread = 0;
                    int item = 0;

                    if (drUserEnrlDataforstudent.Count() == 0)
                    {
                        //No data available for this particular id in mapping sheet
                        missingdataforids += "\n Data is missing for the id" + assetid.ToString() + ", ";
                    }



                    else if (drUserEnrlDataforstudent.Count() == 4)
                    {
                        foreach (DataRow dr in drUserEnrlDataforstudent)
                        {

                            if (thread > 1)
                            {
                                if (item == 1)
                                    dr[3] = " ";

                                missingdataforids += HelperCommonMethods.ValidateMandatoryAndReferenceItems_QuestionMetadata(dr, assetid);
                                missingdataforids += HelperCommonMethods.DoValidations_OperationPerformedEvent(dr, assetid);
                                dtfinalSkill.ImportRow(dr);
                                item++;
                            }

                            thread++;
                        }
                    }
                    else
                    {
                        item = 0;
                        foreach (DataRow dr in drUserEnrlDataforstudent)
                        {
                            if (item == 1)
                                dr[3] = " ";
                            missingdataforids += HelperCommonMethods.ValidateMandatoryAndReferenceItems_QuestionMetadata(dr, assetid);
                            missingdataforids += HelperCommonMethods.DoValidations_OperationPerformedEvent(dr, assetid);
                            dtfinalSkill.ImportRow(dr);
                            item++;
                        }
                    }
                }

            }

            return dtfinalSkill;
        }

        private static string ValidateMandatoryAndReferenceItems_QuestionMetadata(DataRow dr, string refid)
        {
            string errors = string.Empty;

            if (string.IsNullOrEmpty(dr["questionid"].ToString()))
            {
                errors += "\nMandatory column questionid is blank for - " + refid;
            }

            if (string.IsNullOrEmpty(dr["depthofknowledge"].ToString()))
            {
                errors += "\nMandatory column depthofknowledge is blank for - " + refid;
            }

            if (string.IsNullOrEmpty(dr["scoringmode"].ToString()))
            {
                errors += "\nMandatory column scoringmode is blank for - " + refid;
            }

            if (string.IsNullOrEmpty(dr["responsetype"].ToString()))
            {
                errors += "\nMandatory column responsetype is blank for - " + refid;
            }

            if (string.IsNullOrEmpty(dr["maxscore"].ToString()))
            {
                errors += "\nMandatory column maxscore is blank for - " + refid;
            }
            return errors;
        }

        internal static void HideColumnsfromReportQuestionMetadata(DataTable dtfinalQuesMetadata)
        {
            dtfinalQuesMetadata.Columns.Remove("client_id");
            dtfinalQuesMetadata.Columns.Remove("status");
            dtfinalQuesMetadata.Columns.Remove("comment");
            dtfinalQuesMetadata.Columns.Remove("statementid");
            dtfinalQuesMetadata.Columns.Remove("rawjson");
            dtfinalQuesMetadata.Columns.Remove("AccessToken");
        }


        internal static DataTable GenerateDataTableForErrors(DataTable dtfinalSkillError, string missingdataforids)
        {
            dtfinalSkillError.Columns.Add("Sno");
            dtfinalSkillError.Columns.Add("Error");
            int errorsno = 1;
            DataRow drerror = dtfinalSkillError.NewRow();
            drerror[0] = errorsno;
            drerror[1] = missingdataforids;
            dtfinalSkillError.Rows.Add(drerror);
            return dtfinalSkillError;
        }

        internal static DataTable ApplyTincanBusinessLogic_UAChecklist(string Assessmentid, string TeacherId, string StudentId, DataSet dsMappingcsv, string idcolumn, out string missingdataforids)
        {
            DataTable dtfinalSkill = new DataTable();
            missingdataforids = string.Empty;
            dtfinalSkill = dsMappingcsv.Tables[0].Copy();
            dtfinalSkill.Clear();

            string assetid = Assessmentid.ToString();
            string ActorId = TeacherId.ToString();
            DataTable dtAssetSkillAssoc = dsMappingcsv.Tables[0];



            if (!string.IsNullOrWhiteSpace(assetid))
            {
                DataRow[] drUserEnrlDataforstudent = dtAssetSkillAssoc.Select(idcolumn + " = " + "'" + TeacherId + "' and objectparentid = '" + assetid + "'");

                if (drUserEnrlDataforstudent.Count() == 0)
                {
                    //No data available for this particular id in mapping sheet
                    missingdataforids += "\n Data is missing for the assessment id" + assetid.ToString() + ", ";
                }

                else
                {

                    missingdataforids += ValidateGradedQuestionTincanEvent(drUserEnrlDataforstudent, assetid, dtfinalSkill);
                }

            }


            return dtfinalSkill;
        }




        //MP - Might need to remove - Sample code

        public static void charities()
        {
            WebRequest request = WebRequest.Create("https://reports.ppe.k12rs.pearsoncmg.com/ReportServer?/demo/Reports/Class&rs:Command=Render&rs:Format=CSV&StartDate=11/10/2015&EndDate=11/30/2015");

            WebResponse Response = request.GetResponse();

            StreamReader str = new StreamReader(request.GetResponse().GetResponseStream());
            StreamWriter writer = new StreamWriter("c:\\temp\\tst1.txt", true);

            while (str.Peek() >= 0)
            {
                writer.WriteLine(str.ReadLine());

                //MessageBox.Show(str.ReadLine());

            }
            writer.Close();

            //  DataTable dt = GetDataTableFromCsv("c:\\temp\\tst1.txt", true);

            // grd1.DataSource = dt;

        }

        //private DataTable GetDataTableFromCsv(string path, bool isFirstRowHeader)
        //{
        //    string header = isFirstRowHeader ? "Yes" : "No";

        //    string pathOnly = Path.GetDirectoryName(path);
        //    string fileName = Path.GetFileName(path);

        //    string sql = @"SELECT Name FROM [" + fileName + "]";

        //    using (OleDbConnection connection = new OleDbConnection(
        //              @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + pathOnly +
        //              ";Extended Properties=\"Text;HDR=" + header + "\""))
        //    using (OleDbCommand command = new OleDbCommand(sql, connection))
        //    using (OleDbDataAdapter adapter = new OleDbDataAdapter(command))
        //    {
        //        DataTable dataTable = new DataTable();
        //        dataTable.Locale = CultureInfo.CurrentCulture;
        //        adapter.Fill(dataTable);
        //        return dataTable;
        //    }
        //}



        internal static DataTable ApplyCMDBusinessLogic_Users(DataSet dsInputNameId, DataSet dsMappingcsv, string idcolumn, out string missingdataforids)
        {
            DataTable dtfinalSkill = new DataTable();

            missingdataforids = string.Empty;
            dtfinalSkill = dsMappingcsv.Tables[0].Copy();
            dtfinalSkill.Clear();
            for (int i = 0; i < dsInputNameId.Tables[0].Rows.Count; i++)
            {
                DataTable dtassetid = dsInputNameId.Tables[0];
                DataTable dtAssetSkillAssoc = dsMappingcsv.Tables[0];

                string assetid = dsInputNameId.Tables[0].Rows[i][0].ToString();
                string LastUserName = string.Empty;
                if (!string.IsNullOrWhiteSpace(assetid))
                {
                    DataRow[] drUserEnrlDataforstudent = dtAssetSkillAssoc.Select(idcolumn + " = " + "'" + assetid + "'");
                    int thread = 0;
                    int item = 0;

                    if (drUserEnrlDataforstudent.Count() == 0)
                    {
                        //No data available for this particular id in mapping sheet
                        missingdataforids += "\n Data is missing for id - " + assetid.ToString() + ", ";

                    }

                    else if (drUserEnrlDataforstudent.Count() == 4)
                    {
                        foreach (DataRow dr in drUserEnrlDataforstudent)
                        {

                            missingdataforids += ValidateUniqueDetailsforData_Users(LastUserName, dr, assetid);
                            
                           


                            if (thread > 1)
                            {
                                if (item == 1)
                                    dr[3] = " ";
                                missingdataforids += HelperCommonMethods.ValidateMandatoryAndReferenceItems_Users(dr, assetid);
                                missingdataforids += HelperCommonMethods.DoValidations_OperationPerformedEvent(dr, assetid);
                                dtfinalSkill.ImportRow(dr);
                                item++;
                            }

                            thread++;
                        }
                    }
                    else
                    {
                        item = 0;
                        foreach (DataRow dr in drUserEnrlDataforstudent)
                        {
                            missingdataforids += ValidateUniqueDetailsforData_Users(LastUserName, dr, assetid);

                            if (item == 1)
                                dr[3] = " ";
                            missingdataforids += HelperCommonMethods.ValidateMandatoryAndReferenceItems_Users(dr, assetid);
                            missingdataforids += HelperCommonMethods.DoValidations_OperationPerformedEvent(dr, assetid);
                            dtfinalSkill.ImportRow(dr);
                            item++;
                        }
                    }
                }

            }

            return dtfinalSkill;
        }

        private static string ValidateUniqueDetailsforData_Users(string LastUserName, DataRow dr, string assetid)
        {
            string error = string.Empty;
            if (!string.IsNullOrEmpty(LastUserName))
            {
                if (LastUserName != dr["lastname"].ToString())
                {
                    error += "\n Same guid for different users - " + assetid.ToString() + ", ";
                }

                LastUserName = dr["lastname"].ToString();
            }

            return error;

        }

        private static string ValidateUniqueDetailsforData_UserEnrollment(string LastUserName, DataRow dr, string assetid)
        {
            string error = string.Empty;
            if (!string.IsNullOrEmpty(LastUserName))
            {
                if (LastUserName != dr["userid"].ToString())
                {
                    error += "\n Same guid for different users - " + assetid.ToString() + ", ";
                }

                LastUserName = dr["userid"].ToString();
            }

            return error;

        }

        private static string DoValidations_OperationPerformedEvent(DataRow dr, string refid)
        {
            string errors = string.Empty;

            if (string.IsNullOrEmpty(dr["operation"].ToString()))
            {
                errors += "Operation column is blank for - " + refid;
            }

            else if (dr[0].ToString() != "update")
            {
                errors += "Operation is not update for -" + refid;
            }

            return errors;
        }



        private static string ValidateMandatoryAndReferenceItems_Users(DataRow dr, string refid)
        {
            string errors = string.Empty;

            if (string.IsNullOrEmpty(dr["id"].ToString()))
            {
                errors += "\nMandatory column id is blank for - " + refid;
            }


            return errors;
        }

        internal static DataTable ApplyTincanBusinessLogic_FixedAssessments(string Assessmentid, string TeacherId, string StudentId, DataSet dsMappingcsv, string idcolumn, out string missingdataforids)
        {
            DataTable dtfinalSkill = new DataTable();
            missingdataforids = string.Empty;
            dtfinalSkill = dsMappingcsv.Tables[0].Copy();
            dtfinalSkill.Clear();

            string assessmentid = Assessmentid.ToString();
            //string ActorId = TeacherId.ToString();
            DataTable dtAssetSkillAssoc = dsMappingcsv.Tables[0];

            if (!string.IsNullOrWhiteSpace(assessmentid))
            {
                DataRow[] drTeacherActionInfo = dtAssetSkillAssoc.Select(idcolumn + " = " + "'" + TeacherId + "' and objectparentid = '" + assessmentid + "'");
                DataRow[] drStudentActionInfo = dtAssetSkillAssoc.Select(idcolumn + " = " + "'" + StudentId + "'");// and objectparentid = '" + assessmentid + "'");


                if (drStudentActionInfo.Count() == 0)
                {
                    //No data available for this particular id in mapping sheet
                    missingdataforids += "\n Data is missing for the assessment id" + assessmentid.ToString() + " and student id " + StudentId.ToString();
                }

                else
                {
                    missingdataforids += ValidateOpenContentTincanEvent(drStudentActionInfo, assessmentid, dtfinalSkill);
                    missingdataforids += ValidateSubmitQuestionTincanEvent(drStudentActionInfo, assessmentid, dtfinalSkill);
                    missingdataforids += ValidateSubmitContentTincanEvent(drStudentActionInfo, assessmentid, dtfinalSkill);
                }

                if (drTeacherActionInfo.Count() == 0)
                {
                    //No data available for this particular id in mapping sheet
                    missingdataforids += "\n Data is missing for the assessment id" + assessmentid.ToString() + " and teacher id " + TeacherId.ToString();
                }

                else
                {
                    missingdataforids += ValidateGradedQuestionTincanEvent(drTeacherActionInfo, assessmentid, dtfinalSkill);
                    missingdataforids += ValidateGradedContentTincanEvent(drTeacherActionInfo, assessmentid, dtfinalSkill);
                }
            }

            return dtfinalSkill;
        }

        private static string ValidateSubmitContentTincanEvent(DataRow[] drStudentActionInfo, string assessmentid, DataTable dtfinalSkill)
        {
            string missingdataforids = string.Empty;
            bool FoundGradedQues = false;
            int eventcount = 0;

            if (drStudentActionInfo.Count() > 1)
            {
                //No data available for this particular id in mapping sheet
               //// missingdataforids += "\n Warning - Multiple events getting triggered for same event assessment -" + assessmentid.ToString() + ", ";
            }

            foreach (DataRow dr in drStudentActionInfo)
            {

                if (dr["eventtype"].ToString() == "submit" && dr["objecttype"].ToString() == "content")
                {
                    //Success - Got Graded Question Event
                    // missingdataforids += HelperCommonMethods.ValidateMandatoryAndReferenceItems_QuestionMetadata(dr, assetid);
                    dtfinalSkill.ImportRow(dr);
                    FoundGradedQues = true;
                    eventcount++;
                }

              
                
                //else
                //{
                //    missingdataforids += "Graded question event missing for UA Checklist Type assessment - assessment id " + assetid.ToString();
                //}

            }

            if (!FoundGradedQues)
            {
                missingdataforids += "\n Graded question event missing for assessment - assessment id " + assessmentid.ToString();
            }

            if (eventcount > 1)
            {
                missingdataforids += "\n Warning - Multiple submit content events getting triggered for same event assessment -" + assessmentid.ToString() + ", ";
            }

            return missingdataforids;
        }

        private static string ValidateSubmitQuestionTincanEvent(DataRow[] drStudentActionInfo, string assessmentid, DataTable dtfinalSkill)
        {
            string missingdataforids = string.Empty;
            bool FoundGradedQues = false;
            int eventcount = 0;

            if (drStudentActionInfo.Count() > 1)
            {
                //No data available for this particular id in mapping sheet
                ////missingdataforids += "\n Warning - Multiple events getting triggered for same event assessment -" + assessmentid.ToString() + ", ";
            }

            foreach (DataRow dr in drStudentActionInfo)
            {

                if (dr["eventtype"].ToString() == "submit" && dr["objecttype"].ToString() == "question")
                {
                    //Success - Got Graded Question Event
                    // missingdataforids += HelperCommonMethods.ValidateMandatoryAndReferenceItems_QuestionMetadata(dr, assetid);
                    dtfinalSkill.ImportRow(dr);
                    FoundGradedQues = true;
                    eventcount++;
                }

                
                
                //else
                //{
                //    missingdataforids += "Graded question event missing for UA Checklist Type assessment - assessment id " + assetid.ToString();
                //}

            }

            if (!FoundGradedQues)
            {
                missingdataforids += "\n Graded question event missing for assessment - assessment id " + assessmentid.ToString();
            }

            if (eventcount > 1)
            {
                missingdataforids += "\n Warning - Multiple submit question events getting triggered for same event assessment -" + assessmentid.ToString() + ", ";
            }
            return missingdataforids;
        }

        private static string ValidateOpenContentTincanEvent(DataRow[] drStudentActionInfo, string assessmentid, DataTable dtfinalSkill)
        {
            string missingdataforids = string.Empty;
            bool FoundGradedQues = false;
            int eventcount = 0;

            if (drStudentActionInfo.Count() > 1)
            {
                //No data available for this particular id in mapping sheet
               //// missingdataforids += "\n Warning - Multiple events getting triggered for same event assessment -" + assessmentid.ToString() + ", ";
            }

            foreach (DataRow dr in drStudentActionInfo)
            {

                if (dr["eventtype"].ToString() == "attempted" && dr["objecttype"].ToString() == "content")
                {
                    //Success - Got Graded Question Event
                    // missingdataforids += HelperCommonMethods.ValidateMandatoryAndReferenceItems_QuestionMetadata(dr, assetid);
                    dtfinalSkill.ImportRow(dr);
                    FoundGradedQues = true;
                    eventcount++;
                }
                
               
                //else
                //{
                //    missingdataforids += "Graded question event missing for UA Checklist Type assessment - assessment id " + assetid.ToString();
                //}

            }

            if (!FoundGradedQues)
            {
                missingdataforids += "\n Graded question event missing for assessment - assessment id " + assessmentid.ToString();
            }
           
            if (eventcount > 1)
            {
                missingdataforids += "\n Warning - Multiple open content events getting triggered for same event assessment -" + assessmentid.ToString() + ", ";
            }
                
            return missingdataforids;
        }

        private static string ValidateGradedQuestionTincanEvent(DataRow[] drUserEnrlDataforstudent, string assetid, DataTable dtfinalSkill)
        {
            string missingdataforids = string.Empty;
            bool FoundGradedQues = false;
            int eventcount = 0;
            
            if (drUserEnrlDataforstudent.Count() > 1)
            {
                //No data available for this particular id in mapping sheet
               //// missingdataforids += "\n Warning - Multiple events getting triggered for same event assessment -" + assetid.ToString() + ", ";
            }

            foreach (DataRow dr in drUserEnrlDataforstudent)
            {

                if (dr["eventtype"].ToString() == "graded" && dr["objecttype"].ToString() == "question")
                {
                    //Success - Got Graded Question Event
                    // missingdataforids += HelperCommonMethods.ValidateMandatoryAndReferenceItems_QuestionMetadata(dr, assetid);
                    dtfinalSkill.ImportRow(dr);
                    FoundGradedQues = true;
                    eventcount++;
                }

                //else
                //{
                //    missingdataforids += "Graded question event missing for UA Checklist Type assessment - assessment id " + assetid.ToString();
                //}

               

            }

            if(!FoundGradedQues)
            {
                missingdataforids += "\n Graded question event missing for assessment - assessment id " + assetid.ToString();
            }

            if (eventcount > 1)
            {
                missingdataforids += "\n Warning - Multiple graded question events getting triggered for same event assessment -" + assetid.ToString() + ", ";
            }

            return missingdataforids;
        }

        private static string ValidateGradedContentTincanEvent(DataRow[] drUserEnrlDataforstudent, string assetid, DataTable dtfinalSkill)
        {
            string missingdataforids = string.Empty;
            bool FoundGradedQues = false;
            int eventcount = 0;

            if (drUserEnrlDataforstudent.Count() > 1)
            {
                //No data available for this particular id in mapping sheet
               ////missingdataforids += "\n Warning - Multiple events getting triggered for same event assessment -" + assetid.ToString() + ", ";
            }

            foreach (DataRow dr in drUserEnrlDataforstudent)
            {

                if (dr["eventtype"].ToString() == "graded" && dr["objecttype"].ToString() == "content")
                {
                    //Success - Got Graded Question Event
                    // missingdataforids += HelperCommonMethods.ValidateMandatoryAndReferenceItems_QuestionMetadata(dr, assetid);
                    dtfinalSkill.ImportRow(dr);
                    FoundGradedQues = true;
                    eventcount++;
                }

                //else
                //{
                //    missingdataforids += "Graded question event missing for UA Checklist Type assessment - assessment id " + assetid.ToString();
                //}

            }

            if (!FoundGradedQues)
            {
                missingdataforids += "\nGraded question event missing for assessment - assessment id " + assetid.ToString();
            }

            if(eventcount>1)
            {
                missingdataforids += "\n Warning - Multiple Graded Content events getting triggered for same event assessment -" + assetid.ToString() + ", ";
            }

            return missingdataforids;
        }

        internal static DataSet FillNewDatasetForQuestion(DataSet dsassetid)
        {
            DataSet dsassetidnw = new DataSet();
            DataRow[] drquesinp = dsassetid.Tables[0].Select("type = 'question'");
            dsassetidnw = dsassetid.Copy();
            dsassetidnw.Clear();
            foreach (DataRow drinp in drquesinp)
            {
                dsassetidnw.Tables[0].ImportRow(drinp);
            }

            return dsassetidnw;
        }
    }
}
