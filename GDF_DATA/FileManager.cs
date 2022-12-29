using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ExcelDataReader;
using System.Data;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using GDF_DATA.Common;
using Newtonsoft.Json;


namespace GDF_DATA
{
    class FileManager
    {
        const int START_SHEET_INDEX = 1;
        const int START_COLUMN_INDEX = 3;
        const int DATATYPE_FILED_INDEX = 2;
        string EXCEL_FOLDER_PATH = "C:\\Users\\User\\Desktop\\0709_GameData";
        int _readedFileCount = 0;
        int _maxReadFileNumber = 0;

        Dictionary<string, List<string>> _dicDataType = new Dictionary<string, List<string>>();

        List<DataFile> _dataFileList = new List<DataFile>();

        Dictionary<string, DataTable> _dicJsonDatatable = new Dictionary<string, DataTable>();

        public int ReadedFileCount
        {
            get
            {
                return _readedFileCount;
            }

            set
            {
                _readedFileCount = value;
            }
        }

        public int MaxReadFileNumber
        {
            get
            {
                return _maxReadFileNumber;
            }

            set
            {
                _maxReadFileNumber = value;
            }
        }

        public FileManager()
        {

        }

        public FileManager(string folderPath)
        {
            EXCEL_FOLDER_PATH = folderPath;
        }

        public Dictionary<string, List<string>> GetDataTypeDic()
        {
            return _dicDataType;
        }
        public void DoExcelConvertToDataSet(DataSet data)
        {
            TimeCounter allReadTime = new TimeCounter();
            allReadTime.RecordTime();

            string[] ignoreFile = Properties.Resources.ignoreExcel.Split('\n');

            for (int i = 0; i < ignoreFile.Length; ++i)
            {
                ignoreFile[i] = ignoreFile[i].Replace("\r", "");
            }


            getAllExcelFile(ignoreFile, EXCEL_FOLDER_PATH);

            _maxReadFileNumber = _dataFileList.Count;

            foreach (var file in _dataFileList)
            {
                TimeCounter fileReadTime = new TimeCounter();
                fileReadTime.RecordTime();

                if (file.FilePath.Contains("xlsx"))
                {
                    excelToDataList(file.FilePath, file.FileName, data);
                }
                //else 
                //if (file.FilePath.Contains("json"))
                //{
                //    jsonToDataList(file);
                //}
                _readedFileCount++;

                GlobalLog.Instance.Log($"{_readedFileCount} / {_maxReadFileNumber} , {file.FileName} " + fileReadTime.GetTimePasses());
            }

            foreach (var p in _dicJsonDatatable)
            {
                data.Tables.Add(p.Value);
            }

            GlobalLog.Instance.Log(allReadTime.GetTimePasses());
        }


        public void ExcelToJson(DataSet data, string TXT_DIR)
        {
            TimeCounter allReadTime = new TimeCounter();
            allReadTime.RecordTime();

            DirectoryInfo di = new DirectoryInfo(TXT_DIR);

            if (di.Exists == false)
            {
                di.Create();
            }

            string[] ignoreFile = Properties.Resources.ignoreExcel.Split('\n');

            for (int i = 0; i < ignoreFile.Length; ++i)
            {
                ignoreFile[i] = ignoreFile[i].Replace("\r", "");
            }

            getAllExcelFile(ignoreFile, EXCEL_FOLDER_PATH);
            _maxReadFileNumber = _dataFileList.Count;

            foreach (var file in _dataFileList)
            {
                TimeCounter fileReadTime = new TimeCounter();
                fileReadTime.RecordTime();

                if (file.FilePath.Contains("xlsx"))
                {
                    ExcelToDataList(file);
                }
                _readedFileCount++;

                GlobalLog.Instance.Log($"{_readedFileCount} / {_maxReadFileNumber} , {file.FileName} " + fileReadTime.GetTimePasses());
            }


            foreach (DataTable dt in data.Tables)
            {
                string path = TXT_DIR + "\\OutPutJSON" + dt.TableName + ".json";
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(dt, Formatting.Indented);

                File.WriteAllText(path, json, Encoding.UTF8);
            }


            foreach (var p in _dicJsonDatatable)
            {
                data.Tables.Add(p.Value);
            }

            GlobalLog.Instance.Log(allReadTime.GetTimePasses());
        }


        private List<DataFile> getAllDataFile(string[] ignoreText, string path)
        {
            List<DataFile> excelDataList = new List<DataFile>();
            if (Directory.Exists(path))
            {
                var di = new DirectoryInfo(path);
                var dirs = Directory.GetDirectories(path);
                foreach (var file in di.GetFiles("*.json"))
                {
                    bool isIgnoreFile = false;
                    foreach (var ignore in ignoreText)
                    {
                        if (ignore.Equals(file.Name.Split('.')[0]))
                            isIgnoreFile = true;
                    }
                    if (isIgnoreFile) continue;
                    DataFile data = new DataFile();
                    data.FileName = file.Name.Split('.')[0];
                    data.FilePath = file.FullName;
                    data.FolderName = di.Name;
                    _dataFileList.Add(data);
                }

                if (dirs.Length > 0)
                {
                    foreach (var dir in dirs)
                    {
                        getAllDataFile(ignoreText, dir);
                    }
                }
            }

            return excelDataList;
        }

        private List<DataFile> getAllExcelFile(string[] ignoreText, string path)
        {
            List<DataFile> excelDataList = new List<DataFile>();
            if (Directory.Exists(path))
            {
                var di = new DirectoryInfo(path);
                var dirs = Directory.GetDirectories(path);
                foreach (var file in di.GetFiles("*.xlsx"))
                {
                    bool isIgnoreFile = false;
                    foreach (var ignore in ignoreText)
                    {
                        if (ignore.Equals(file.Name.Split('.')[0]))
                            isIgnoreFile = true;
                    }
                    if (isIgnoreFile) continue;
                    DataFile data = new DataFile();
                    data.FileName = file.Name.Split('.')[0];
                    data.FilePath = file.FullName;
                    data.FolderName = di.Name;
                    _dataFileList.Add(data);
                }

                if (dirs.Length > 0)
                {
                    foreach (var dir in dirs)
                    {
                        getAllDataFile(ignoreText, dir);
                    }
                }
            }

            return excelDataList;
        }


        void jsonToDataList(DataFile fileData)
        {
            DataTable dt = ConvertDataTable(fileData.FilePath);
            List<string> typelist = new List<string>();
            for (int i = 0; i < dt.Columns.Count; ++i)
            {
                if (dt.Columns[i].ColumnName.Contains("TextKr") ||
                    dt.Columns[i].ColumnName.Contains("CHT") ||
                    dt.Columns[i].ColumnName.Contains("CHS") ||
                    dt.Columns[i].ColumnName.Contains("JPN") ||
                    dt.Columns[i].ColumnName.Contains("ENG") ||
                    dt.Columns[i].ColumnName.Contains("THA") ||
                    dt.Columns[i].ColumnName.Contains("IND") ||
                    dt.Columns[i].ColumnName.Contains("VIE") ||
                    dt.Columns[i].ColumnName.Contains("GER") ||
                    dt.Columns[i].ColumnName.Contains("SPA") ||
                    dt.Columns[i].ColumnName.Contains("POR") ||
                    dt.Columns[i].ColumnName.Contains("RUS") ||
                    dt.Columns[i].ColumnName.Contains("Raid_Script") ||
                    dt.Columns[i].ColumnName.Contains("Progress_Enter_Condition"))
                    typelist.Add("string");                
                else if (dt.Columns[i].ColumnName.Contains("ClassItem"))
                    typelist.Add("View");
                else
                    typelist.Add("int");
            }
 
            if (fileData.FolderName.Contains("json_") == false)
            {
                dt.TableName = fileData.FileName;
                _dicJsonDatatable.Add(fileData.FileName, dt);
                _dicDataType.Add(fileData.FileName, typelist);
            }
            else
            {
                dt.Columns.Add("FILENAME");
                for (int i = 0; i < dt.Rows.Count; ++i)
                {
                    dt.Rows[i]["FILENAME"] = fileData.FileName;
                }
                // 데이터 테이블을 하나로 만들기 위해 이어줌
                if (!_dicJsonDatatable.ContainsKey(fileData.FolderName))
                {
                    dt.TableName = fileData.FolderName;
                    // 폴더 JSON에는 파일 이름 추가.. GENDATA에 NPC를 위해서

                    _dicJsonDatatable.Add(fileData.FolderName, dt);
                    _dicDataType.Add(fileData.FolderName, typelist);
                }
                else
                {

                    _dicJsonDatatable[fileData.FolderName].Merge(dt);
                }
            }
        }


        void ExcelToDataList(DataFile fileData)
        {
            DataTable dt = new DataTable();

            List<string> typelist = new List<string>();

            {
                dt.Columns.Add("FILENAME");
                for (int i = 0; i < dt.Rows.Count; ++i)
                {
                    dt.Rows[i]["FILENAME"] = fileData.FileName;
                }
                // 데이터 테이블을 하나로 만들기 위해 이어줌
                if (!_dicJsonDatatable.ContainsKey(fileData.FolderName))
                {
                    dt.TableName = fileData.FolderName;
                    // 폴더 JSON에는 파일 이름 추가.. GENDATA에 NPC를 위해서

                    _dicJsonDatatable.Add(fileData.FolderName, dt);
                    _dicDataType.Add(fileData.FolderName, typelist);
                }
                else
                {
                    _dicJsonDatatable[fileData.FolderName].Merge(dt);
                }
            }
        }

        public void insertTableData(DataTable resultDT, JObject item)
        {
            IList<string> keys = item.Properties().Select(p => p.Name).ToList();
            if (resultDT.Columns.Count != keys.Count)
            {
                foreach (var key in keys)
                {
                    if (resultDT.Columns.Contains(key) == false)
                        resultDT.Columns.Add(key);
                }
                int columnIndex = 0;
                // 키 순서대로 위치 변경
                foreach (var key in keys)
                {
                    resultDT.Columns[key].SetOrdinal(columnIndex);
                    columnIndex++;
                }
            }

            List<object> values = new List<object>();

            foreach (var token in item)
            {
                string value = token.Value.ToString();
                if (value.ToString().Contains("[") && token.Value.ToString().Contains("]"))
                {
                    value = value.Replace("[", "").Replace("]", "").Replace("\r\n", "").Replace(" ", "");
                }
                values.Add(value);
            }

            resultDT.Rows.Add(values.ToArray());
        }

        private DataTable ConvertDataTable(string path)
        {
            DataTable resultDT = new DataTable();
            string jsonText = System.IO.File.ReadAllText(path);

            if (jsonText.Contains("[") == false)
            {
                JObject jsonData = JObject.Parse(jsonText);
                insertTableData(resultDT, jsonData);
            }
            else
            {
                JArray jsonData = JArray.Parse(jsonText);
                foreach (JObject item in jsonData)
                {
                    //Regex.Replace(item., @"\s", "")
                    insertTableData(resultDT, item);
                }
            }
            return resultDT;
        }

        public void CreateTXTFile(DataSet dataset, string TXT_DIR)
        {
            DirectoryInfo di = new DirectoryInfo(TXT_DIR + "\\OutPutText");

            if (di.Exists == false)
            {
                di.Create();
            }

            TXT_DIR = TXT_DIR + "\\OutPutText";

            foreach (DataTable dt in dataset.Tables)
            {
                StringBuilder sb = new StringBuilder();

                foreach (DataRow row in dt.Rows)
                {
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        string rowValue = row[i].ToString().Replace("\t", "").Replace("\n", "");

                        sb.Append(rowValue);
                        sb.Append(i == dt.Columns.Count - 1 ? "\n" : "\t");
                    }
                }

                string path = TXT_DIR + "\\" + dt.TableName + ".txt";
                StreamWriter sw = new StreamWriter(path, false);
                sw.Write(sb.ToString());
                sw.Close();
            }
        }

        void excelToDataList(string path, string tableName, DataSet data)
        {
            var stream = File.Open(path, FileMode.Open, FileAccess.Read);
            var reader = ExcelReaderFactory.CreateReader(stream);

            var result = reader.AsDataSet();
            DataTable firstTable = result.Tables["data"];

            firstTable.TableName = tableName;

            List<string> dataTypeList = new List<string>();

            try
            {
                // !(기획참고용) 없애기
                //for (int i = firstTable.Columns.Count - 1; i >= 0; --i)
                //{
                //    if (firstTable.Rows[START_COLUMN_INDEX][i].ToString().Contains("!"))
                //    {
                //        Regex cntStr = new Regex("!");
                //        var returnStr = int.Parse(cntStr.Matches(firstTable.Rows[START_COLUMN_INDEX][i].ToString()).Count.ToString());
                //        if (returnStr == 1)
                //        {
                //            firstTable.Columns.RemoveAt(i);
                //        }
                //        continue;
                //    }
                //}
                // datatype 가져오기
                for (int i = 1; i < firstTable.Columns.Count; ++i)
                {
                    //// []=> 배열은 string으로 저장.
                    //if (firstTable.Rows[DATATYPE_FILED_INDEX + 1][i].ToString().Contains("[]"))
                    //    dataTypeList.Add("string");

                    //// 다 스트링으로
                    //else
                        //dataTypeList.Add(firstTable.Rows[DATATYPE_FILED_INDEX][i].ToString());
                        dataTypeList.Add("VARCHAR(1500)");
                }

                _dicDataType.Add(firstTable.TableName, dataTypeList);

                // 공백Row 삭제
                for (int i = 0; i < firstTable.Rows.Count; ++i)
                {
                    if (firstTable.Rows[0][0].ToString() == "")
                    {
                        if (firstTable.TableName == "PublicMission" && firstTable.Rows[i][0].ToString() == "")
                        {
                            firstTable.Columns.RemoveAt(0);
                        }
                        else 
                            firstTable.Rows.RemoveAt(0);
                    }
                    else
                        break;
                }

                // coulums값을 기본에서 우리에 맞게 row에 있는애로 가져온다.
                for (int i = firstTable.Columns.Count - 1; i >= 0; --i)
                {
                    if (!firstTable.Rows[0][i].ToString().Equals(string.Empty))
                    {
                        firstTable.Columns[i].ColumnName = firstTable.Rows[0][i].ToString().Replace(" ", "");
                    }
                    if (firstTable.Columns[i].ColumnName.Contains("Column"))
                    {
                        firstTable.Columns.RemoveAt(i);
                        continue;
                    }
                }

                firstTable.Rows.RemoveAt(0);

                //dataGridView1.DataSource = firstTable;
                data.Tables.Add(firstTable.Copy());
            }
            catch (System.Exception e)
            {
                GlobalLog.Instance.Log(e.ToString());
            }
        }
    }
}
