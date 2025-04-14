using ExcelDataReader;
using OpenQA.Selenium;
using AventStack.ExtentReports;
using System;
using System.Data;
using System.IO;
using System.Collections.Generic;

namespace Efawatercom_final_project
{
    public class ExcelHelper
    {
        public List<string[]> ReadTestDataFromExcel(string filePath)
        {
            var data = new List<string[]>();

            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet();
                    var table = result.Tables[0]; // قراءة أول ورقة من الملف

                    for (int row = 1; row < table.Rows.Count; row++)  // بدأ من 1 لتخطي الرأس
                    {
                        var rowData = new string[table.Columns.Count];

                        for (int col = 0; col < table.Columns.Count; col++)
                        {
                            rowData[col] = table.Rows[row][col].ToString();
                        }

                        data.Add(rowData);
                    }
                }
            }

            return data;
        }
    }
}
