using ExcelDataReader;
using System.IO;

public class ExcelHelper
{
    public void ReadExcelFile(string filePath)
    {
        using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
        {
            using (var reader = ExcelReaderFactory.CreateReader(stream))
            {
                var result = reader.AsDataSet();
                var table = result.Tables[0]; // اختيار الورقة الأولى
                var cellValue = table.Rows[0][0].ToString(); // قراءة قيمة الخلية A1
                Console.WriteLine(cellValue);
            }
        }
    }
}
