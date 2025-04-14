using EPPlus;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Efawater_final_project.Utilities
{
    public class ManageReportPage
    {
        public static List<TestData> ReadTestDataFromExcel(string filePath)
        {
            var testDataList = new List<TestData>();

            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var worksheet = package.Workbook.Worksheets[0]; // افترض أن البيانات في الورقة الأولى
                var rowCount = worksheet.Dimension.Rows;

                for (int row = 2; row <= rowCount; row++) // تبدأ من 2 إذا كان السطر الأول يحتوي على رؤوس الأعمدة
                {
                    var categoryName = worksheet.Cells[row, 1].Text; // العمود الأول للفئة
                    var startDate = worksheet.Cells[row, 2].Text;  // العمود الثاني لتاريخ البدء
                    var endDate = worksheet.Cells[row, 3].Text;    // العمود الثالث لتاريخ النهاية

                    testDataList.Add(new TestData
                    {
                        CategoryName = categoryName,
                        StartDate = startDate,
                        EndDate = endDate
                    });
                }
            }

            return testDataList;
        }
    }

    public class TestData
    {
        public string CategoryName { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}
