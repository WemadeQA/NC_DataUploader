using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using WemadeQA.Common;

namespace GDF_DATA
{
    delegate void completeHandler(string subject, string body, bool isException, List<string> dataBaseList);
    class UpLoder
    {
        public DataSet UploadDataSet = null;

        completeHandler _completeHandler;

        FileManager dataReader = null;
        DataBaseManager dbManager = null;
        private string _filePath;
        private string _dataFile;
        Common.ILog _myLog;


        string _subject = string.Empty;
        string _body = string.Empty;
        bool _isException = false;
        List<string> _dataBaseList = null;

        internal completeHandler ComplateHandler
        {
            get
            {
                return _completeHandler;
            }

            set
            {
                _completeHandler = value;
            }
        }

        public UpLoder(string FilePath, string dataFile, Common.ILog log)
        {
            _filePath = FilePath;
            _dataFile = dataFile;
            _myLog = log;
        }
        // 
        public void OnlyUpload(string dbname)
        {
            // 변환한 DataTable을 DB로 넣어줌
            dbManager = new DataBaseManager(ConfigData.programSettingData["MYSQLIP"], dbname, ConfigData.programSettingData["MYSQLID"], ConfigData.programSettingData["MYSQLPW"]);
            dbManager.UpdateTableByTXT(UploadDataSet, _filePath, dbname, dataReader.GetDataTypeDic());
            dbManager.Close();
        }

        public void Upload(List<string> dataBaseList)
        {
            _dataBaseList = dataBaseList;
            Common.GlobalLog myLog = new Common.GlobalLog(_myLog);

            dataReader = new FileManager(_filePath);
            UploadDataSet = new DataSet();

            string DBName = string.Empty;

            foreach (var name in dataBaseList)
            {
                DBName += $"테이블 : {name}\n";
            }

            //try
            //{
            // 엑셀,json파일을 C# DataTable로 변환
                //dataReader.ExcelToJson(UploadDataSet, _filePath);

                dataReader.DoExcelConvertToDataSet(UploadDataSet);

                dataReader.CreateTXTFile(UploadDataSet, _filePath);

                foreach (var name in dataBaseList)
                {
                    // 변환한 DataTable을 DB로 넣어줌
                    dbManager = new DataBaseManager(ConfigData.programSettingData["MYSQLIP"], name, ConfigData.programSettingData["MYSQLID"], ConfigData.programSettingData["MYSQLPW"]);
                    dbManager.UpdateTableByTXT(UploadDataSet, _filePath, name, dataReader.GetDataTypeDic());
                    dbManager.Close();
                }

                _subject = $"[GDF] 데이터 업데이트 {_dataFile}";
                _body = $"파일 명 : {_dataFile}\n" +
                    $"파일 갯수 : {dataReader.ReadedFileCount} / {dataReader.MaxReadFileNumber}\n" +
                    DBName;

            //}
            //catch (Exception e)
            //{
            //    _isException = true;
            //    _myLog.Log(e.Message);
            //    _subject = $"[GDF] 데이터 업데이트 에러 {_dataFile}";
            //    _body = $"============ Exception ============\n" +
            //                        $"{e.Message}\n" +
            //                        DBName +
            //                        $"{dbManager.Time.GetTimePasses()}\n" +
            //                        $"============ Exception ============\n";
            //}

            dbManager.Close();
        }

        public void Complete(List<string> diffList)
        {
            _body += $"{dbManager.Time.GetTimePasses()}\n";
            _body += "======================변경 목록====================\n";
            foreach (var diff in diffList)
            {
                _body += $"{diff}\n";
            }
            _body += "==================================================\n";
            _completeHandler(_subject, _body, _isException, _dataBaseList);
        }
    }
}
