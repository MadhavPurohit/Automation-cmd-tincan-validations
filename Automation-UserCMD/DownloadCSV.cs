using System;
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
    [TestClass]
    public class DownloadCSV
    {
        IWebDriver driver;
        string client_id = "PSOC_dev";
        string client_secret = "psocppe@123";
        string start_date = "12/21/2015";
        string end_date = "12/21/2015";
        string path = null;

        [TestInitialize]
        public void TestSetup()
        {
            ChromeOptions chromeOptions = new ChromeOptions();
            string outPutDirectory = System.IO.Directory.GetCurrentDirectory();
            outPutDirectory = Path.GetDirectoryName(outPutDirectory);
            path = outPutDirectory + @"\TestData\Inputs";
            chromeOptions.AddUserProfilePreference("download.default_directory", path);
            chromeOptions.AddUserProfilePreference("disable-popup-blocking", "true");
            driver = new ChromeDriver(chromeOptions);
        }

        [TestCleanup]
        public void Cleanup()
        {
            driver.Quit();
        }

        [TestMethod]
        public void DownloadTincanCSV()
        {
            if (System.IO.File.Exists(path + @"\tincanevents.csv"))
                System.IO.File.Delete(path + @"\tincanevents.csv");

            string tincanurl = "https://reports.ppe.k12rs.pearsoncmg.com/ReportServer?/QA/Reports/tincanevents&rs:Command=Render&rs:Format=CSV&StartDate=" + start_date + "&EndDate=" + end_date;
            driver.Navigate().GoToUrl(tincanurl);
            driver.FindElement(By.Id("ctl00_RSContent_txtUserName")).SendKeys(client_id);
            driver.FindElement(By.Id("ctl00_RSContent_txtPassword")).SendKeys(client_secret);
            driver.FindElement(By.Id("ctl00_RSContent_btnLogOn")).Click();
            System.Threading.Thread.Sleep(7000);
        }

        [TestMethod]
        public void DownloadClassCMDCSV()
        {
            if (System.IO.File.Exists(path + @"\Class.csv"))
                System.IO.File.Delete(path + @"\Class.csv");

            string ClassCMDurl = "https://reports.ppe.k12rs.pearsoncmg.com/ReportServer?/QA/Reports/Class&rs:Command=Render&rs:Format=CSV&StartDate=" + start_date + "&EndDate=" + end_date;
            driver.Navigate().GoToUrl(ClassCMDurl);
            driver.FindElement(By.Id("ctl00_RSContent_txtUserName")).SendKeys(client_id);
            driver.FindElement(By.Id("ctl00_RSContent_txtPassword")).SendKeys(client_secret);
            driver.FindElement(By.Id("ctl00_RSContent_btnLogOn")).Click();
            System.Threading.Thread.Sleep(7000);
        }

        [TestMethod]
        public void DownloadClassProductAsscociationCSV()
        {
            if (System.IO.File.Exists(path + @"\ClassProductAsscociation.csv"))
                System.IO.File.Delete(path + @"\ClassProductAsscociation.csv");

            string ClassCMDurl = "https://reports.ppe.k12rs.pearsoncmg.com/ReportServer?/QA/Reports/ClassProductAsscociation&rs:Command=Render&rs:Format=CSV&StartDate=" + start_date + "&EndDate=" + end_date;
            driver.Navigate().GoToUrl(ClassCMDurl);
            driver.FindElement(By.Id("ctl00_RSContent_txtUserName")).SendKeys(client_id);
            driver.FindElement(By.Id("ctl00_RSContent_txtPassword")).SendKeys(client_secret);
            driver.FindElement(By.Id("ctl00_RSContent_btnLogOn")).Click();
            System.Threading.Thread.Sleep(7000);
        }

        [TestMethod]
        public void DownloadContentCSV()
        {
            if (System.IO.File.Exists(path + @"\Content.csv"))
                System.IO.File.Delete(path + @"\Content.csv");

            string ClassCMDurl = "https://reports.ppe.k12rs.pearsoncmg.com/ReportServer?/QA/Reports/Content&rs:Command=Render&rs:Format=CSV&StartDate=" + start_date + "&EndDate=" + end_date;
            driver.Navigate().GoToUrl(ClassCMDurl);
            driver.FindElement(By.Id("ctl00_RSContent_txtUserName")).SendKeys(client_id);
            driver.FindElement(By.Id("ctl00_RSContent_txtPassword")).SendKeys(client_secret);
            driver.FindElement(By.Id("ctl00_RSContent_btnLogOn")).Click();
            System.Threading.Thread.Sleep(7000);
        }

        [TestMethod]
        public void DownloadContentassignmentCSV()
        {
            if (System.IO.File.Exists(path + @"\Contentassignment.csv"))
                System.IO.File.Delete(path + @"\Contentassignment.csv");

            string ClassCMDurl = "https://reports.ppe.k12rs.pearsoncmg.com/ReportServer?/QA/Reports/Contentassignment&rs:Command=Render&rs:Format=CSV&StartDate=" + start_date + "&EndDate=" + end_date;
            driver.Navigate().GoToUrl(ClassCMDurl);
            driver.FindElement(By.Id("ctl00_RSContent_txtUserName")).SendKeys(client_id);
            driver.FindElement(By.Id("ctl00_RSContent_txtPassword")).SendKeys(client_secret);
            driver.FindElement(By.Id("ctl00_RSContent_btnLogOn")).Click();
            System.Threading.Thread.Sleep(7000);
        }
    }
}
