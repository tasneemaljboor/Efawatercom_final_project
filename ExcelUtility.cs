using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;

namespace AdminPagesAutomation.Utilities
{
    public class ExcelUtility
    {
        public static List<string> GetCategoryNames(string filePath, string sheetName)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"Excel file not found at: {filePath}");

            List<string> categories = new List<string>();

            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            using (var workbook = new XSSFWorkbook(stream))
            {
                var sheet = workbook.GetSheet(sheetName);
                if (sheet == null)
                    throw new ArgumentException($"Sheet '{sheetName}' not found in Excel file.");

                for (int i = 1; i <= sheet.LastRowNum; i++)
                {
                    var row = sheet.GetRow(i);
                    if (row != null)
                    {
                        var cell = row.GetCell(0);
                        if (cell != null)
                            categories.Add(cell.ToString().Trim());
                    }
                }
            }

            return categories;
        }
    }
}


