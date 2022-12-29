using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;
using System.IO;

namespace GDF_DATA
{
    class ExportToExcel
    {
        public void ExportExcelSheet(System.Data.DataTable DataTable, DirectoryInfo DI)
        {
            object missingType = Type.Missing;
            Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
            Workbook excelBook = excelApp.Workbooks.Add(missingType);
            Worksheet excelWorkSheet = (Worksheet)excelBook.Worksheets.Add(missingType, missingType, missingType, missingType);
            Range range = null;
            excelApp.Visible = false;

            excelWorkSheet.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            excelWorkSheet.Name = $"{DataTable.TableName}";


            for (int i = 0; i < DataTable.Columns.Count; i++)
            {
                excelWorkSheet.Cells[1, i + 1] = DataTable.Columns[i].ColumnName;
            }

            for (int i = 0; i < DataTable.Rows.Count; i++)
            {
                for (int j = 0; j < DataTable.Columns.Count; j++)
                {
                    excelWorkSheet.UsedRange.NumberFormat = "@";
                    excelWorkSheet.Cells[i + 2, j + 1] = DataTable.Rows[i][j];
                    //if (excelWorkSheet.Cells[i + 2, j + 1].Value.ToString().Contains("▷"))
                    //{
                    //    excelWorkSheet.Cells[i + 2, j + 1].Font.Color = XlRgbColor.rgbRed;
                    //}

                    //    range = excelWorkSheet.Range[excelWorkSheet.Cells[i + 2, 1], excelWorkSheet.Cells[i + 2, j + 1]].Cells;
                    //}
                    //if (excelWorkSheet.Cells[i + 2, 1].Value.ToString().Equals("추가"))
                    //{
                    //    range.Interior.Color = XlRgbColor.rgbGreen;
                    //}
                    //else if (excelWorkSheet.Cells[i + 2, 1].Value.ToString().Equals("변경"))
                    //{
                    //    range.Interior.Color = XlRgbColor.rgbYellow;
                    //}
                    //else
                    //{
                    //    range.Interior.Color = XlRgbColor.rgbRed;
                    //}
                }
            }

            ////영역 세팅
            //range = excelWorkSheet.get_Range("A1", "I1");
            //range = excelWorkSheet.Cells[1,1];
            //range = excelWorkSheet.Range[excelWorkSheet.Cells[1, 1], excelWorkSheet.Cells[2, 2]].Cells;
            //range.Merge(true);

            excelWorkSheet.Cells.EntireColumn.AutoFit();

            excelBook.SaveAs($"{DI.ToString()}\\{DataTable.TableName}_Result", Microsoft.Office.Interop.Excel.XlFileFormat.xlOpenXMLWorkbook, missingType,
                        missingType, missingType, missingType, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
                        Microsoft.Office.Interop.Excel.XlSaveConflictResolution.xlUserResolution, true, missingType, missingType, missingType);
            excelApp.Visible = false;

            excelApp.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelWorkSheet);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelBook);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
        }
    }
}
