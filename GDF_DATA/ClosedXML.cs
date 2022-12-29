using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDF_DATA
{
    class ClosedXML
    {
        public void ExportExcelSheet(System.Data.DataTable DataTable, DirectoryInfo DI)
        {
            var WorkBook = new XLWorkbook();
            if (DataTable.TableName.Equals("DROP_REWARD_BOX_POSITION"))
            {
                return;
            }
            var WorkSheet = WorkBook.Worksheets.Add(DataTable, "결과");

            WorkSheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
            WorkSheet.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            WorkSheet.Row(1).Style.Fill.BackgroundColor = XLColor.White;
            WorkSheet.Row(1).Style.Font.FontColor = XLColor.CoolBlack;

            for (int i = 0; i < DataTable.Rows.Count; i++)
            {
                if (WorkSheet.Cell(i + 2, 1).Value.ToString().Equals("추가"))
                {
                    WorkSheet.Row(i + 2).Style.Fill.BackgroundColor = XLColor.Green;
                    WorkSheet.Row(i + 2).Style.Font.FontColor = XLColor.White;
                }
                else if (WorkSheet.Cell(i + 2, 1).Value.ToString().Equals("변경"))
                {
                    WorkSheet.Row(i + 2).Style.Fill.BackgroundColor = XLColor.Yellow;
                    for (int j = 0; j < DataTable.Columns.Count; j++)
                    {
                        if (WorkSheet.Cell(i + 2, j + 1).Value.ToString().Contains("▷"))
                            WorkSheet.Cell(i + 2, j + 1).Style.Font.FontColor = XLColor.Red;
                    }
                }
                else
                {
                    WorkSheet.Row(i + 3).Style.Fill.BackgroundColor = XLColor.Red;
                }
            }
            WorkSheet.Tables.FirstOrDefault().ShowAutoFilter = false;
            WorkSheet.RangeUsed().Style.Border.InsideBorder = XLBorderStyleValues.Hair;
            WorkSheet.RangeUsed().Style.Border.InsideBorderColor = XLColor.Black;
            WorkSheet.RangeUsed().Style.Border.OutsideBorder = XLBorderStyleValues.Hair;
            WorkSheet.RangeUsed().Style.Border.OutsideBorderColor = XLColor.Black;
            WorkSheet.Columns().AdjustToContents();
            WorkSheet.Rows().AdjustToContents();
            

            WorkBook.SaveAs($"{DI.ToString()}\\{DataTable.TableName}_Diff.xlsx");
            Common.GlobalLog.Instance.Log(DataTable.TableName + ", Export done");
        }
    }
}
