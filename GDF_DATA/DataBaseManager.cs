using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.IO;
using GDF_DATA.Common;

namespace GDF_DATA
{
    class DataBaseManager
    {
        MySqlConnection _connect;

        const int MAX_ROWDATA_SIZE = 350;
        public TimeCounter Time = new TimeCounter();

        public MySqlConnection Connect
        {
            get
            {
                return _connect;
            }

            set
            {
                _connect = value;
            }
        }

        public DataBaseManager(string ip, string database, string uid, string pwd)
        {
            string connect = string.Format($"Server={ip};Database={database};Uid={uid};Pwd={pwd};Allow User Variables=True;AllowLoadLocalInfile = true");

            Connect = new MySqlConnection(connect);

            Connect.Open();

        }

        public void Close()
        {
            Connect.Close();
        }

        // data table이 있으면 삭제.
        private void deleteDataTable(DataSet dataSet)
        {
            var cmd = new MySqlCommand();
            cmd.Connection = Connect;
            foreach (DataTable dt in dataSet.Tables)
            {
                cmd.CommandText = $"DROP TABLE IF EXISTS {dt.TableName}";
                cmd.ExecuteNonQuery();
            }
        }

        private string getDataType(Dictionary<string, List<string>> dicDataType, string colname, string tableName, int index)
        {
            string type = "VARCHAR(70)";

            if (tableName != "SYSTEM_DATA")
            {
                if (dicDataType[tableName].Count <= index)
                {
                    type = "VARCHAR(350)";
                    return type;
                }

                if (colname.Contains("[]"))
                    type = "VARCHAR(350)";
                else if (dicDataType[tableName][index].Equals("int[]"))
                    type = "VARCHAR(350)";
                else if (dicDataType[tableName][index].Equals("View"))
                    type = "VARCHAR(500)";
                else if (dicDataType[tableName][index].Equals("string"))
                    type = "TEXT(1200)";
                else if (dicDataType[tableName][index].Equals("배열"))
                    type = "VARCHAR(350)";
            }
            else
            {
                type = "VARCHAR(50)";
            }

            return type;
        }

        public void UpdateTableByTXT(DataSet dataset, string TXT_DIR, string DBName, Dictionary<string, List<string>> dicDataType)
        {
            Time.RecordTime();
            var cmd = new MySqlCommand();
            cmd.Connection = Connect;

            deleteDataTable(dataset);
            TXT_DIR = TXT_DIR + "\\OutPutText";

            int tableIndex = 0;
            foreach (DataTable dt in dataset.Tables)
            {
                string engineText = "engine=innodb";
                if (dt.Columns.Count <= 0)
                    continue;
                if(dt.TableName.Contains("SYSTEM_DATA"))
                {
                    engineText = "engine=MyiSAM";
                }
                int columnIndex = 0;
                GlobalLog.Instance.Log($"{tableIndex} : tablename : {dt.TableName}");
                List<string> columnTypeList = new List<string>();
                // 테이블 만들기
                cmd.CommandText = $"CREATE TABLE IF NOT EXISTS {dt.TableName} (";
                foreach (DataColumn dc in dt.Columns)
                {
                    string columnName = dc.ColumnName;

                    //ServerVisit_ 문자열 자르기
                    if(columnName.Contains("ServerVisit_"))
                    {
                        columnName = columnName.Replace("ServerVisit_", "");
                    }

                    cmd.CommandText += $"`{columnName.Replace(" ", "")}` {getDataType(dicDataType, columnName, dt.TableName, columnIndex)}";
                    columnTypeList.Add(getDataType(dicDataType, columnName, dt.TableName, columnIndex));
                    if (!dt.Columns[dt.Columns.Count - 1].Equals(dc))
                        cmd.CommandText += ",";
                    columnIndex++;
                }
                cmd.CommandText += ")" + $" {engineText} character set utf8 collate utf8_general_ci";

                cmd.ExecuteNonQuery();
                string path = TXT_DIR.Replace("\\", "\\\\") + "\\" + dt.TableName + ".txt";

                var loader = new MySqlBulkLoader(Connect)
                {
                    CharacterSet = "utf8",
                    FileName = path,
                    TableName = dt.TableName,
                    FieldTerminator = "\t",      // 열 구분자
                    LineTerminator = "\n",      // 줄 구분자
                    NumberOfLinesToSkip = 0,
                };
                loader.Local = true;
                loader.Load();

                tableIndex++;
            }
        }
        public void CreateDataTablebyDataSet(DataSet dataSet, Dictionary<string, List<string>> dicDataType)
        {
            Time.RecordTime();
            var cmd = new MySqlCommand();
            cmd.Connection = Connect;

            deleteDataTable(dataSet);

            foreach (DataTable dt in dataSet.Tables)
            {
                List<string> columnTypeList = new List<string>();
                int columnIndex = 0;
                // 테이블 만들기
                cmd.CommandText = $"CREATE TABLE IF NOT EXISTS {dt.TableName} (";
                foreach (DataColumn dc in dt.Columns)
                {
                    GlobalLog.Instance.Log($"{columnIndex} : {dc.ColumnName} , tablename : {dt.TableName}");
                    cmd.CommandText += $"`{dc.ColumnName}` {getDataType(dicDataType, dc.ColumnName, dt.TableName, columnIndex)}";
                    columnTypeList.Add(getDataType(dicDataType, dc.ColumnName, dt.TableName, columnIndex));
                    if (!dt.Columns[dt.Columns.Count - 1].Equals(dc))
                        cmd.CommandText += ",";
                    columnIndex++;
                }
                cmd.CommandText += ")" + " default character set utf8 collate utf8_general_ci";

                cmd.ExecuteNonQuery();

                // data 넣기
                foreach (DataRow dr in dt.Rows)
                {
                    int index = 0;
                    cmd.CommandText = $"INSERT into {dt.TableName} ";
                    cmd.CommandText += "VALUES(";
                    foreach (var item in dr.ItemArray)
                    {
                        string rowValue = item.ToString().Replace("\"", "").Replace("–", "-").Replace("\xC2\xA0", "");
                        rowValue = rowValue.Trim();

                        if (rowValue.Length >= MAX_ROWDATA_SIZE)
                            rowValue = "0";

                        GlobalLog.Instance.Log($"{dt.TableName} , {index} : {item.ToString()}");
                        cmd.CommandText += $"\"{rowValue}\"";
                        if (dr.ItemArray.Length - 1 > index)
                        {
                            cmd.CommandText += ",";
                        }
                        index++;
                    }
                    cmd.CommandText += ")";
                    cmd.ExecuteNonQuery();
                }
            }
            GlobalLog.Instance.Log(Time.GetTimePasses());
        }

        public void SetDataUpdate(string dataBase)
        {
            string today = DateTime.Now.ToString("yyyy.MM.dd");
            string serverName = string.Empty;
            var cmd = new MySqlCommand();
            cmd.Connection = Connect;

            if (dataBase.Equals("nc_datatable_master"))
                serverName = "master";
            else if (dataBase.Equals("nc_datatable_beta"))
                serverName = "beta";
            else if (dataBase.Equals("nc_datatable_stage"))
                serverName = "stage";
            else if (dataBase.Equals("nc_datatable_live"))
                serverName = "live";


            cmd.CommandText = $"UPDATE `data_update` SET `update_date` = '{today}' WHERE `server` = '{serverName}'";
            cmd.ExecuteNonQuery();
        }
    }
}
