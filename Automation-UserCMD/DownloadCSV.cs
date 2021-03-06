﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support;
using System.IO;

namespace Automation_UserCMD
{
    //[TestClass]
    public static class DownloadCSV
    {
        static IWebDriver driver;
        static string client_id = "PSOC_dev";
        static string client_secret = "psocppe@123";
        static string start_date = "12/21/2015";
        static string end_date = "12/21/2015";
        static string path = null;

        //[TestInitialize]
        public static void TestSetup(string path)
        {
            ChromeOptions chromeOptions = new ChromeOptions();
            //string outPutDirectory = System.IO.Directory.GetCurrentDirectory();
            //outPutDirectory = Path.GetDirectoryName(outPutDirectory);
            //path = outPutDirectory + @"\TestData\Inputs";
            chromeOptions.AddUserProfilePreference("download.default_directory", path);
            chromeOptions.AddUserProfilePreference("disable-popup-blocking", "true");
            driver = new ChromeDriver(chromeOptions);
        }

        //[TestCleanup]
        public static void Cleanup()
        {
            driver.Quit();
        }

        //[TestMethod]
        public static void DownloadTincanCSV(string path)
        {
            TestSetup(path);
            
            //if (System.IO.File.Exists(path + @"\tincanevents.csv"))
            //    System.IO.File.Delete(path + @"\tincanevents.csv");

            if (System.IO.File.Exists(path))
                System.IO.File.Delete(path);

            string tincanurl = "https://reports.ppe.k12rs.pearsoncmg.com/ReportServer?/QA/Reports/tincanevents&rs:Command=Render&rs:Format=CSV&StartDate=" + start_date + "&EndDate=" + end_date;
            driver.Navigate().GoToUrl(tincanurl);
            driver.FindElement(By.Id("ctl00_RSContent_txtUserName")).SendKeys(client_id);
            driver.FindElement(By.Id("ctl00_RSContent_txtPassword")).SendKeys(client_secret);
            driver.FindElement(By.Id("ctl00_RSContent_btnLogOn")).Click();
            System.Threading.Thread.Sleep(7000);

            Cleanup();
        }

        //[TestMethod]
        public static void DownloadClassCMDCSV(string path)
        {
            TestSetup(Path.GetDirectoryName(path));

            if (System.IO.File.Exists(path))
                System.IO.File.Delete(path);

            string ClassCMDurl = "https://reports.ppe.k12rs.pearsoncmg.com/ReportServer?/QA/Reports/Class&rs:Command=Render&rs:Format=CSV&StartDate=" + start_date + "&EndDate=" + end_date;
            driver.Navigate().GoToUrl(ClassCMDurl);
            driver.FindElement(By.Id("ctl00_RSContent_txtUserName")).SendKeys(client_id);
            driver.FindElement(By.Id("ctl00_RSContent_txtPassword")).SendKeys(client_secret);
            driver.FindElement(By.Id("ctl00_RSContent_btnLogOn")).Click();
            System.Threading.Thread.Sleep(7000);

            Cleanup();
        }

        //[TestMethod]
        public static void DownloadClassProductAsscociationCSV(string path)
        {
            TestSetup(Path.GetDirectoryName(path));
           
            if (System.IO.File.Exists(path))
                System.IO.File.Delete(path);
            

            string ClassCMDurl = "https://reports.ppe.k12rs.pearsoncmg.com/ReportServer?/QA/Reports/ClassProductAsscociation&rs:Command=Render&rs:Format=CSV&StartDate=" + start_date + "&EndDate=" + end_date;
            driver.Navigate().GoToUrl(ClassCMDurl);
            driver.FindElement(By.Id("ctl00_RSContent_txtUserName")).SendKeys(client_id);
            driver.FindElement(By.Id("ctl00_RSContent_txtPassword")).SendKeys(client_secret);
            driver.FindElement(By.Id("ctl00_RSContent_btnLogOn")).Click();
            System.Threading.Thread.Sleep(7000);

            Cleanup();
        }

        //[TestMethod]
        public static void DownloadContentCSV(string path)
        {
            TestSetup(Path.GetDirectoryName(path));

            if (System.IO.File.Exists(path))
                System.IO.File.Delete(path);

            string ClassCMDurl = "https://reports.ppe.k12rs.pearsoncmg.com/ReportServer?/QA/Reports/Content&rs:Command=Render&rs:Format=CSV&StartDate=" + start_date + "&EndDate=" + end_date;
            driver.Navigate().GoToUrl(ClassCMDurl);
            driver.FindElement(By.Id("ctl00_RSContent_txtUserName")).SendKeys(client_id);
            driver.FindElement(By.Id("ctl00_RSContent_txtPassword")).SendKeys(client_secret);
            driver.FindElement(By.Id("ctl00_RSContent_btnLogOn")).Click();
            System.Threading.Thread.Sleep(7000);

            Cleanup();
        }

        //[TestMethod]
        public static void DownloadContentassignmentCSV(string path)
        {
            TestSetup(Path.GetDirectoryName(path));

            if (System.IO.File.Exists(path))
                System.IO.File.Delete(path);

            string ClassCMDurl = "https://reports.ppe.k12rs.pearsoncmg.com/ReportServer?/QA/Reports/Contentassignment&rs:Command=Render&rs:Format=CSV&StartDate=" + start_date + "&EndDate=" + end_date;
            driver.Navigate().GoToUrl(ClassCMDurl);
            driver.FindElement(By.Id("ctl00_RSContent_txtUserName")).SendKeys(client_id);
            driver.FindElement(By.Id("ctl00_RSContent_txtPassword")).SendKeys(client_secret);
            driver.FindElement(By.Id("ctl00_RSContent_btnLogOn")).Click();
            System.Threading.Thread.Sleep(7000);

            Cleanup();
        }

        public static void DownloadContentcontainerCSV(string path)
        {
            TestSetup(Path.GetDirectoryName(path));

            if (System.IO.File.Exists(path))
                System.IO.File.Delete(path);

            string ClassCMDurl = "https://reports.ppe.k12rs.pearsoncmg.com/ReportServer?/QA/Reports/Contentcontainer&rs:Command=Render&rs:Format=CSV&StartDate="+ start_date + "&EndDate=" + end_date;
            driver.Navigate().GoToUrl(ClassCMDurl);
            driver.FindElement(By.Id("ctl00_RSContent_txtUserName")).SendKeys(client_id);
            driver.FindElement(By.Id("ctl00_RSContent_txtPassword")).SendKeys(client_secret);
            driver.FindElement(By.Id("ctl00_RSContent_btnLogOn")).Click();
            System.Threading.Thread.Sleep(7000);

            Cleanup();
        }

        public static void DownloadContentContainerMappingCSV(string path)
        {
            TestSetup(Path.GetDirectoryName(path));

            if (System.IO.File.Exists(path))
                System.IO.File.Delete(path);

            string ClassCMDurl = "https://reports.ppe.k12rs.pearsoncmg.com/ReportServer?/QA/Reports/ContentContainerMapping&rs:Command=Render&rs:Format=CSV&StartDate="+ start_date + "&EndDate=" + end_date;
            driver.Navigate().GoToUrl(ClassCMDurl);
            driver.FindElement(By.Id("ctl00_RSContent_txtUserName")).SendKeys(client_id);
            driver.FindElement(By.Id("ctl00_RSContent_txtPassword")).SendKeys(client_secret);
            driver.FindElement(By.Id("ctl00_RSContent_btnLogOn")).Click();
            System.Threading.Thread.Sleep(7000);

            Cleanup();
        }

        public static void DownloadUserEnrollmentCSV(string path)
        {
            TestSetup(Path.GetDirectoryName(path));

            if (System.IO.File.Exists(path))
                System.IO.File.Delete(path);

            string ClassCMDurl = "https://reports.ppe.k12rs.pearsoncmg.com/ReportServer?/QA/Reports/UserEnrollment&rs:Command=Render&rs:Format=CSV&StartDate=" + start_date + "&EndDate=" + end_date;
            driver.Navigate().GoToUrl(ClassCMDurl);
            driver.FindElement(By.Id("ctl00_RSContent_txtUserName")).SendKeys(client_id);
            driver.FindElement(By.Id("ctl00_RSContent_txtPassword")).SendKeys(client_secret);
            driver.FindElement(By.Id("ctl00_RSContent_btnLogOn")).Click();
            System.Threading.Thread.Sleep(7000);

            Cleanup();
        }

        public static void DownloadUserCSV(string path)
        {
            TestSetup(Path.GetDirectoryName(path));

            if (System.IO.File.Exists(path))
                System.IO.File.Delete(path);

            string ClassCMDurl = "https://reports.ppe.k12rs.pearsoncmg.com/ReportServer?/QA/Reports/User&rs:Command=Render&rs:Format=CSV&StartDate=" + start_date + "&EndDate=" + end_date;
            driver.Navigate().GoToUrl(ClassCMDurl);
            driver.FindElement(By.Id("ctl00_RSContent_txtUserName")).SendKeys(client_id);
            driver.FindElement(By.Id("ctl00_RSContent_txtPassword")).SendKeys(client_secret);
            driver.FindElement(By.Id("ctl00_RSContent_btnLogOn")).Click();
            System.Threading.Thread.Sleep(7000);

            Cleanup();
        }

        public static void DownloadOrganizationCSV(string path)
        {
            TestSetup(Path.GetDirectoryName(path));

            if (System.IO.File.Exists(path))
                System.IO.File.Delete(path);

            string ClassCMDurl = "https://reports.ppe.k12rs.pearsoncmg.com/ReportServer?/QA/Reports/Organization&rs:Command=Render&rs:Format=CSV&StartDate=03/20/2015&EndDate=" + start_date + "&EndDate=" + end_date;
            driver.Navigate().GoToUrl(ClassCMDurl);
            driver.FindElement(By.Id("ctl00_RSContent_txtUserName")).SendKeys(client_id);
            driver.FindElement(By.Id("ctl00_RSContent_txtPassword")).SendKeys(client_secret);
            driver.FindElement(By.Id("ctl00_RSContent_btnLogOn")).Click();
            System.Threading.Thread.Sleep(7000);

            Cleanup();
        }

        public static void DownloadSkillCSV(string path)
        {
            TestSetup(Path.GetDirectoryName(path));

            if (System.IO.File.Exists(path))
                System.IO.File.Delete(path);

            string ClassCMDurl = "https://reports.ppe.k12rs.pearsoncmg.com/ReportServer?/QA/Reports/Skill&rs:Command=Render&rs:Format=CSV&StartDate=" + start_date + "&EndDate=" + end_date;
            driver.Navigate().GoToUrl(ClassCMDurl);
            driver.FindElement(By.Id("ctl00_RSContent_txtUserName")).SendKeys(client_id);
            driver.FindElement(By.Id("ctl00_RSContent_txtPassword")).SendKeys(client_secret);
            driver.FindElement(By.Id("ctl00_RSContent_btnLogOn")).Click();
            System.Threading.Thread.Sleep(7000);

            Cleanup();
        }

        public static void DownloadStandardCSV(string path)
        {
            TestSetup(Path.GetDirectoryName(path));

            if (System.IO.File.Exists(path))
                System.IO.File.Delete(path);

            string ClassCMDurl = "https://reports.ppe.k12rs.pearsoncmg.com/ReportServer?/QA/Reports/Standard&rs:Command=Render&rs:Format=CSV&StartDate=" + start_date + "&EndDate=" + end_date;
            driver.Navigate().GoToUrl(ClassCMDurl);
            driver.FindElement(By.Id("ctl00_RSContent_txtUserName")).SendKeys(client_id);
            driver.FindElement(By.Id("ctl00_RSContent_txtPassword")).SendKeys(client_secret);
            driver.FindElement(By.Id("ctl00_RSContent_btnLogOn")).Click();
            System.Threading.Thread.Sleep(7000);

            Cleanup();
        }

        public static void DownloadSkillStandardMappingCSV(string path)
        {
            TestSetup(Path.GetDirectoryName(path));

            if (System.IO.File.Exists(path))
                System.IO.File.Delete(path);

            string ClassCMDurl = "https://reports.ppe.k12rs.pearsoncmg.com/ReportServer?/QA/Reports/SkillStandardMapping&rs:Command=Render&rs:Format=CSV&StartDate="+ start_date + "&EndDate=" + end_date;
            driver.Navigate().GoToUrl(ClassCMDurl);
            driver.FindElement(By.Id("ctl00_RSContent_txtUserName")).SendKeys(client_id);
            driver.FindElement(By.Id("ctl00_RSContent_txtPassword")).SendKeys(client_secret);
            driver.FindElement(By.Id("ctl00_RSContent_btnLogOn")).Click();
            System.Threading.Thread.Sleep(7000);

            Cleanup();
        }

        public static void DownloadFrameworkCSV(string path)
        {
            TestSetup(Path.GetDirectoryName(path));

            if (System.IO.File.Exists(path))
                System.IO.File.Delete(path);

            string ClassCMDurl = "https://reports.ppe.k12rs.pearsoncmg.com/ReportServer?/QA/Reports/Framework&rs:Command=Render&rs:Format=CSV&StartDate=" + start_date + "&EndDate=" + end_date;
            driver.Navigate().GoToUrl(ClassCMDurl);
            driver.FindElement(By.Id("ctl00_RSContent_txtUserName")).SendKeys(client_id);
            driver.FindElement(By.Id("ctl00_RSContent_txtPassword")).SendKeys(client_secret);
            driver.FindElement(By.Id("ctl00_RSContent_btnLogOn")).Click();
            System.Threading.Thread.Sleep(7000);

            Cleanup();
        }

        public static void DownloadAssetSkillMappingCSV(string path)
        {
            TestSetup(Path.GetDirectoryName(path));

            if (System.IO.File.Exists(path))
                System.IO.File.Delete(path);

            string ClassCMDurl = "https://reports.ppe.k12rs.pearsoncmg.com/ReportServer?/QA/Reports/AssetSkillsMapping&rs:Command=Render&rs:Format=CSV&StartDate="+ start_date + "&EndDate=" + end_date;
            driver.Navigate().GoToUrl(ClassCMDurl);
            driver.FindElement(By.Id("ctl00_RSContent_txtUserName")).SendKeys(client_id);
            driver.FindElement(By.Id("ctl00_RSContent_txtPassword")).SendKeys(client_secret);
            driver.FindElement(By.Id("ctl00_RSContent_btnLogOn")).Click();
            System.Threading.Thread.Sleep(7000);

            Cleanup();
        }

        public static void DownloadStandardFrameworkMappingCSV(string path)
        {
            TestSetup(Path.GetDirectoryName(path));

            if (System.IO.File.Exists(path))
                System.IO.File.Delete(path);

            string ClassCMDurl = "https://reports.ppe.k12rs.pearsoncmg.com/ReportServer?/QA/Reports/StandardFrameworkMapping&rs:Command=Render&rs:Format=CSV&StartDate=" + start_date + "&EndDate=" + end_date;
            driver.Navigate().GoToUrl(ClassCMDurl);
            driver.FindElement(By.Id("ctl00_RSContent_txtUserName")).SendKeys(client_id);
            driver.FindElement(By.Id("ctl00_RSContent_txtPassword")).SendKeys(client_secret);
            driver.FindElement(By.Id("ctl00_RSContent_btnLogOn")).Click();
            System.Threading.Thread.Sleep(7000);

            Cleanup();
        }
    }
}
