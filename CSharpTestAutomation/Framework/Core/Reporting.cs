using System;
using System.IO;

using CSharpAutomationFramework.Framework.Helpers;

using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace CSharpAutomationFramework.Framework.Core
{
    public class Reporting
    {
        // paths
        private String executionPath;
        private String curExecutionFolder;
        private String htmlReportsPath;
        private String snapShotsPath;
        private String harFilePath;
        private String snapshotFolderName;
        private String snapshotRelativePath;
        private String summaryReportPath;
        private String testCaseReport;
        private String scriptName;

        // Counters and Integers
        private int snapshotCount;
        private int operationCount;
        private int passCount;
        private int failCount;
        private int tcsPassed;
        private int testCaseNo;

        //Time
        DateTime summaryStartTime;
        DateTime startTime;
        DateTime endTime;

        private IWebDriver driver;
       // private FileOutputStream foutStrm = null;

        public Reporting(String env, String className, String browserName)
        {
            // Get Root Path
            String workingPath = Generic.GetBasePath();
            String rootPath = workingPath;

            // Set paths
            executionPath = rootPath + "/execution";
            curExecutionFolder = executionPath + "/" + env + "/" + className;
            htmlReportsPath = curExecutionFolder + "/" + browserName.ToLower() + "_reports";
            snapShotsPath = (htmlReportsPath + "/Snapshots");
            harFilePath = (htmlReportsPath + "/Hars");
            summaryReportPath = (htmlReportsPath + "/SummaryReport.html");
        }

        public void SetDriver(IWebDriver webDriver)
        {
            driver = webDriver;
        }

        // Function to Create Execution Folders
        public bool CreateExecutionFolders()
        {
            // Delete if folder already exists
            if (Directory.Exists(htmlReportsPath))
                Directory.Delete(htmlReportsPath, true);

            Directory.CreateDirectory(snapShotsPath);
            return true;
        }

        public void CreateSummaryReport()
        {
            CreateExecutionFolders();

            // Setting counter value
            tcsPassed = 0;
            testCaseNo = 0;
            summaryStartTime = DateTime.Now;

            try
            {
                // Open the test case report for writing                   
                StreamWriter sw = File.CreateText(summaryReportPath);
                // Close the html file

                sw.WriteLine("<HTML><BODY><TABLE BORDER=0 CELLPADDING=3 CELLSPACING=1 WIDTH=100% BGCOLOR=BLACK>");
                sw.WriteLine(@"<TR><TD WIDTH=90% ALIGN=CENTER BGCOLOR=WHITE><FONT FACE=VERDANA COLOR=ORANGE SIZE=3><B></B></FONT></TD></TR><TR><TD ALIGN=CENTER BGCOLOR=ORANGE><FONT FACE=VERDANA COLOR=WHITE SIZE=3><B>Selenium Framework Reporting</B></FONT></TD></TR></TABLE><TABLE CELLPADDING=3 WIDTH=100%><TR height=30><TD WIDTH=100% ALIGN=CENTER BGCOLOR=WHITE><FONT FACE=VERDANA COLOR=//0073C5 SIZE=2><B>  Automation Result : "
                             + DateTime.Now + " on Machine "
                             + System.Environment.MachineName + " by user "
                             + System.Environment.UserName + "</B></FONT></TD></TR><TR HEIGHT=5></TR></TABLE>");
                sw.WriteLine("<TABLE  CELLPADDING=3 CELLSPACING=1 WIDTH=100%>");
                sw.WriteLine(@"<TR COLS=6 BGCOLOR=ORANGE><TD WIDTH=10%><FONT FACE=VERDANA COLOR=BLACK SIZE=2><B>TC No.</B></FONT></TD><TD  WIDTH=70%><FONT FACE=VERDANA COLOR=BLACK SIZE=2><B>Test Name</B></FONT></TD><TD BGCOLOR=ORANGE WIDTH=30%><FONT FACE=VERDANA COLOR=BLACK SIZE=2><B>Status</B></FONT></TD></TR>");
                // Close the object
                sw.Close();
            }
            catch (IOException io)
            {
                Console.Write(io.StackTrace);
            }
        }

        public void CreateTestLevelReport(String strTestName)
        {
            operationCount = 0;
            passCount = 0;
            failCount = 0;
            snapshotCount = 0;
            scriptName = strTestName;
            testCaseReport = htmlReportsPath + "/Report_"
                        + scriptName + ".html";
            snapshotFolderName = snapShotsPath + "/" + scriptName;
            snapshotRelativePath = "Snapshots/" + scriptName;

            if (Directory.Exists(snapshotFolderName))
                Directory.Delete(snapshotFolderName);

            Directory.CreateDirectory(snapshotFolderName);

            // Open the report file to write the report
            //StreamWriter sw = File.CreateText(testCaseReport);

            ICapabilities caps = ((RemoteWebDriver)(driver)).Capabilities;

            using(StreamWriter sw = File.CreateText(testCaseReport))
            {
                try
                {
                    sw.WriteLine("<HTML><BODY><TABLE BORDER=0 CELLPADDING=3 CELLSPACING=1 WIDTH=100% BGCOLOR=ORANGE>");
                    sw.WriteLine("<TR><TD WIDTH=90% ALIGN=CENTER BGCOLOR=WHITE><FONT FACE=VERDANA COLOR=ORANGE SIZE=3><B></B></FONT></T" +
                        "D></TR><TR><TD ALIGN=CENTER BGCOLOR=ORANGE><FONT FACE=VERDANA COLOR=WHITE SIZE=3><B>Selenium Framewo" +
                        "rk Reporting</B></FONT></TD></TR></TABLE>");
                    sw.WriteLine("<TABLE CELLPADDING=3 WIDTH=100%><TR height=30><TD WIDTH=100% ALIGN=CENTER BGCOLOR=WHITE><FONT FACE=VE" +
                        "RDANA COLOR=//0073C5 SIZE=2><B>  Execution Details : "
                                  + DateTime.Now + " on Machine "
                                  + System.Environment.MachineName + " by user "
                                  + System.Environment.UserName + "</B></FONT></TD></TR><TR HEIGHT=5></TR></TABLE>");
                    sw.WriteLine("<TABLE CELLPADDING=3 WIDTH=100%><TR height=30><TD WIDTH=100% ALIGN=CENTER BGCOLOR=WHITE><FONT FACE=VE" +
                        "RDANA COLOR=//0073C5 SIZE=2><B>  OS: "
                                  + caps.Platform.ToString() + " Browser: "
                                  + caps.BrowserName + " ver: "
                                  + caps.Version + "</B></FONT></TD></TR><TR HEIGHT=5></TR></TABLE>");
                    sw.WriteLine("<TABLE BORDER=0 BORDERCOLOR=WHITE CELLPADDING=3 CELLSPACING=1 WIDTH=100%>");
                    sw.WriteLine("<TR><TD BGCOLOR=BLACK WIDTH=20%><FONT FACE=VERDANA COLOR=WHITE SIZE=2><B>TestName:</B></FONT></TD><TD" +
                        " COLSPAN=6 BGCOLOR=BLACK><FONT FACE=VERDANA COLOR=WHITE SIZE=2><B>"
                                    + scriptName + "</B></FONT></TD></TR>");
                    sw.WriteLine("</TABLE><BR/><TABLE WIDTH=100% CELLPADDING=3>");
                    sw.WriteLine(@"<TR WIDTH=100%><TH BGCOLOR=ORANGE WIDTH=5%><FONT FACE=VERDANA SIZE=2>Step No.</FONT></TH><TH BGCOLOR=ORANGE WIDTH=28%><FONT FACE=VERDANA SIZE=2>Step Description</FONT></TH><TH BGCOLOR=ORANGE WIDTH=25%><FONT FACE=VERDANA SIZE=2>Expected Value</FONT></TH><TH BGCOLOR=ORANGE WIDTH=25%><FONT FACE=VERDANA SIZE=2>Obtained Value</FONT></TH><TH BGCOLOR=ORANGE WIDTH=7%><FONT FACE=VERDANA SIZE=2>Result</FONT></TH></TR>");
                    sw.Close();
                }
                catch (IOException io)
                {
                    Console.Write(io.StackTrace);
                }
            }

           

         
            // Get the start time of the execution
            startTime = DateTime.MaxValue;
        }

        public void CloseTestLevelReport(String strTestName)
        {
            String strTestCaseResult = null;
            endTime = DateTime.Now;
            String strTimeDifference = Generic.getTimeDifference(startTime.Ticks, endTime.Ticks);

            using(StreamWriter sw = new StreamWriter(testCaseReport, true))
            {
                try
                {
                    sw.WriteLine("<TR></TR><TR><TD BGCOLOR=BLACK WIDTH=5%></TD><TD BGCOLOR=BLACK WIDTH=28%><FONT FACE=VERDANA COLOR=WHI" +
                        "TE SIZE=2><B>Time Taken : "
                                    + strTimeDifference + "</B></FONT></TD><TD BGCOLOR=BLACK WIDTH=25%><FONT FACE=VERDANA COLOR=WHITE SIZE=2><B>Pass Count : "
                                    + passCount + "</B></FONT></TD><TD BGCOLOR=BLACK WIDTH=25%><FONT FACE=VERDANA COLOR=WHITE SIZE=2><B>Fail Count : "
                                    + failCount + "</b></FONT></TD><TD BGCOLOR=Black WIDTH=7%></TD></TR>");
                    sw.WriteLine("</TABLE><TABLE WIDTH=100%><TR><TD ALIGN=RIGHT><FONT FACE=VERDANA COLOR=ORANGE SIZE=1></FONT></TD></TR" +
                        "></TABLE></BODY></HTML>");
                    sw.Close();
                }
                catch (IOException io)
                {
                    Console.Write(io.StackTrace);
                }
            }

            if (failCount != 0)
                strTestCaseResult = "Fail";
            else
                strTestCaseResult = "Pass";

            WriteToTestSummary(("Report_" + strTestName), strTestCaseResult);
        }

        public void WriteToTestSummary(String strTestCaseName, String strResult)
        {
            String sRowColor;
            String sColor;

            using(StreamWriter sw = new StreamWriter(summaryReportPath, true))
            {
                try
                {
                    // Check color result
                    if (strResult.ToUpper() == "PASSED" || strResult.ToUpper() == "PASS")
                    {
                        sColor = "GREEN";
                        tcsPassed++;
                    }
                    else if (strResult.ToUpper() == "FAILED" || strResult.ToUpper() == "FAIL")
                        sColor = "RED";
                    else
                        sColor = "ORANGE";

                    testCaseNo++;
                    if (testCaseNo % 2 == 0)
                        sRowColor = "#EEEEEE";
                    else
                        sRowColor = "#D3D3D3";

                    sw.WriteLine("<TR COLS=3 BGCOLOR="
                                    + sRowColor + "><TD  WIDTH=10%><FONT FACE=VERDANA SIZE=2>"
                                    + testCaseNo + "</FONT></TD><TD  WIDTH=70%><FONT FACE=VERDANA SIZE=2>"
                                    + strTestCaseName + "</FONT></TD><TD  WIDTH=20%><A HREF=\'"
                                    + strTestCaseName + ".html\'><FONT FACE=VERDANA SIZE=2 COLOR="
                                    + sColor + "><B>"
                                  + strResult + "</B></FONT></A></TD></TR>");
                    sw.Close();
                }
                catch (IOException io)
                {
                    Console.Write(io.StackTrace);
                }
            }
        }

        public void CloseTestSummaryReport()
        {
            DateTime summaryEndTime = DateTime.Now;
            String timeDifference = Generic.getTimeDifference(summaryStartTime.Ticks, summaryEndTime.Ticks);

            // Open the Test Summary Report File
            try
            {
                using(StreamWriter sw = new StreamWriter(summaryReportPath, true))
                {
                    sw.WriteLine("</TABLE><TABLE WIDTH=100%><TR>");
                    sw.WriteLine("<TD BGCOLOR=BLACK WIDTH=10%></TD><TD BGCOLOR=BLACK WIDTH=70%><FONT FACE=VERDANA SIZE=2 COLOR=WHITE><B>Time Taken : "
                                    + timeDifference + "</B></FONT></TD><TD BGCOLOR=BLACK WIDTH=20%><FONT FACE=WINGDINGS SIZE=4>2</FONT><FONT FACE=VERDANA SIZE=2 COLOR=WHITE><B>Total Passed: "
                                    + tcsPassed + "</B></FONT></TD>");
                    sw.WriteLine("</TR></TABLE>");
                    sw.WriteLine("<TABLE WIDTH=100%><TR><TD ALIGN=RIGHT><FONT FACE=VERDANA COLOR=ORANGE SIZE=1></FONT></TD></TR></TABLE></BODY></HTML>");
                    sw.Close();
                }
            }
            catch (IOException io)
            {
                Console.Write(io.StackTrace);
            }
        }

        public void WriteToTestLevelReport(String strDescription, String strExpectedValue, String strObtainedValue, String strResult)
        {
            String snapshotFile;
            String snapshotFilePath;
            String sRowColor;

            using(StreamWriter sw = new StreamWriter(testCaseReport, true))
            {
                // Increment the Operation Count
                operationCount++;
                // Row Color
                if (operationCount % 2 == 0)
                    sRowColor = "#EEEEEE";
                else
                    sRowColor = "#D3D3D3";

                // Check if the result is Pass or Fail
                if (strResult.ToUpper() == "PASS")
                {
                    passCount++;
                    snapshotCount++;
                    snapshotFilePath = (snapshotFolderName + ("/SS_"
                                + (snapshotCount + ".png")));
                    snapshotFile = (snapshotRelativePath + ("/SS_"
                                + (snapshotCount + ".png")));

                    ((ITakesScreenshot)(driver)).GetScreenshot().SaveAsFile(snapshotFilePath);

                    sw.WriteLine("<TR WIDTH=100%><TD BGCOLOR="
                                    + sRowColor + " WIDTH=5% ALIGN=CENTER><FONT FACE=VERDANA SIZE=2 ><B>"
                                    + operationCount + "</B></FONT></TD><TD BGCOLOR="
                                    + sRowColor + " WIDTH=28%><FONT FACE=VERDANA SIZE=2>"
                                    + strDescription + " </FONT></TD><TD BGCOLOR="
                                    + sRowColor + " WIDTH=25%><FONT FACE=VERDANA SIZE=2>"
                                    + strExpectedValue + " </FONT></TD><TD BGCOLOR="
                                    + sRowColor + " WIDTH=25%><FONT FACE=VERDANA SIZE=2>"
                                    + strObtainedValue + " </FONT></TD><TD BGCOLOR="
                                    + sRowColor + " WIDTH=7% ALIGN=CENTER><A HREF=\'"
                                    + snapshotFile + "\'><FONT FACE=VERDANA SIZE=2 COLOR=GREEN><B>"
                                    + strResult + " </B></FONT></A></TD></TR>");
                }
                else if (strResult.ToUpper() == "FAIL")
                {
                    snapshotCount++;
                    failCount++;
                    snapshotFilePath = (snapshotFolderName + ("/SS_"
                                + (snapshotCount + ".png")));
                    snapshotFile = (snapshotRelativePath + ("/SS_"
                                + (snapshotCount + ".png")));

                    ((ITakesScreenshot)(driver)).GetScreenshot().SaveAsFile(snapshotFilePath);

                    sw.WriteLine("<TR WIDTH=100%><TD BGCOLOR="
                                    + sRowColor + " WIDTH=5% ALIGN=CENTER><FONT FACE=VERDANA SIZE=2 ><B>"
                                    + operationCount + "</B></FONT></TD><TD BGCOLOR="
                                    + sRowColor + " WIDTH=28%><FONT FACE=VERDANA SIZE=2>"
                                    + strDescription + " </FONT></TD><TD BGCOLOR="
                                    + sRowColor + " WIDTH=25%><FONT FACE=VERDANA SIZE=2>"
                                    + strExpectedValue + " </FONT></TD><TD BGCOLOR="
                                    + sRowColor + " WIDTH=25%><FONT FACE=VERDANA SIZE=2>"
                                    + strObtainedValue + " </FONT></TD><TD BGCOLOR="
                                    + sRowColor + " WIDTH=7% ALIGN=CENTER><A HREF=\'"
                                    + snapshotFile + "\'><FONT FACE=VERDANA SIZE=2 COLOR=RED><B>"
                                    + strResult + " </B></FONT></A></TD></TR>");
                }
                else
                {
                    sw.WriteLine("<TR WIDTH=100%><TD BGCOLOR="
                                    + sRowColor + " WIDTH=5% ALIGN=CENTER><FONT FACE=VERDANA SIZE=2><B>"
                                    + operationCount + "</B></FONT></TD><TD BGCOLOR="
                                    + sRowColor + " WIDTH=28%><FONT FACE=VERDANA SIZE=2>"
                                    + strDescription + "</FONT></TD><TD BGCOLOR="
                                    + sRowColor + " WIDTH=25%><FONT FACE=VERDANA SIZE=2>"
                                    + strExpectedValue + "</FONT></TD><TD BGCOLOR="
                                    + sRowColor + " WIDTH=25%><FONT FACE=VERDANA SIZE=2>"
                                    + strObtainedValue + "</FONT></TD><TD BGCOLOR="
                                    + sRowColor + " WIDTH=7% ALIGN=CENTER><FONT FACE=VERDANA SIZE=2 COLOR=orange><B>"
                                    + strResult + "</B></FONT></TD></TR>");
                }

                sw.Close();
            }
        }
    }
}