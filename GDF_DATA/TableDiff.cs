using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;
using WemadeQA.Common;

/// <summary>
/// Table Diff 뜨는 클래스.
/// </summary>
namespace GDF_DATA
{
    class TableDiff
    {
        DataBaseManager _fromDB;
        DataBaseManager _toDB;

        string _fromDBName;
        string _toDBName;
        DataSet _resultDataSet = new DataSet();
        DataSet _DiffDatSet = new DataSet();
        List<string> _keywardList = new List<string>();

        Dictionary<string, string> _dicStandardColumn = new Dictionary<string, string>();

        public DataSet ResultDataSet
        {
            get
            {
                return _resultDataSet;
            }

            set
            {
                _resultDataSet = value;
            }
        }

        public TableDiff(DataSet diffDataset, string fromDB, string toDB)
        {
            _fromDB = new DataBaseManager(ConfigData.programSettingData["MYSQLIP"], fromDB, ConfigData.programSettingData["MYSQLID"], ConfigData.programSettingData["MYSQLPW"]);
            _toDB = new DataBaseManager(ConfigData.programSettingData["MYSQLIP"], toDB, ConfigData.programSettingData["MYSQLID"], ConfigData.programSettingData["MYSQLPW"]);
            
            _DiffDatSet = diffDataset.Copy();

            _fromDBName = fromDB;
            _toDBName = toDB;

            string[] columnTexts = Properties.Resources.StandardColumn.Split('\n');

            foreach (var col in columnTexts)
            {
                string[] splitstr = col.Split(',');

                if (!_dicStandardColumn.ContainsKey(splitstr[0]))
                {
                    _dicStandardColumn.Add(splitstr[0].Replace("\r", ""), splitstr[1].Replace("\r", ""));
                }
            }
        }

        public void DoDiff()
        {
            if (_DiffDatSet == null)
                return;

            foreach (DataTable dt in _DiffDatSet.Tables)
            {
                // json 붙은 파일은 제외.
                if (dt.TableName.Contains("json"))
                    continue;
                // row가 없으면 없는거임
                if (dt.Rows.Count <= 0)
                    continue;

                ExecuteTableDiff(dt.TableName, dt.TableName);
            }

            Common.GlobalLog.Instance.Log("====================================");
            Common.GlobalLog.Instance.Log("Check Done.");
            Common.GlobalLog.Instance.Log("====================================");

        }

        private void AddRow(DataTable table, DataRow insertRow)
        {
            object[] datas = null;
            datas = insertRow.ItemArray;
            table.Rows.Add(datas);
        }

        private void CheckDataTable(DataTable fromTable, DataTable toTable,
            DataTable resultTable, List<int> keyColumnIndex)
        {
            bool isModify = false;

            foreach (DataRow row in toTable.Rows)
            {
                row.BeginEdit();
            }
            
            for (int i = 0; i < fromTable.Rows.Count; ++i)
            {
                string selectString = string.Empty;
                
                for(int j=0;j<_keywardList.Count;++j)
                {
                    selectString += $"{_keywardList[j]}=\'{fromTable.Rows[i].ItemArray[keyColumnIndex[j]]}\' AND ";
                }
                selectString = selectString.Remove(selectString.Length - 4, 4);
                DataRow[] toRow = toTable.Select(selectString);

                // 변경 확인
                if (toRow.Length > 0)
                {
                    foreach (DataColumn column in toTable.Columns)
                    {
                        if (fromTable.Columns.Contains(column.ColumnName) == false)
                            continue;

                        string fromValue = fromTable.Rows[i][column.ColumnName].ToString();

                        string temp = toRow[0][column.ColumnName].ToString();

                        if (!fromValue.Equals(toRow[0][column.ColumnName].ToString()))
                        {
                            toRow[0]["변경유형"] = "변경";
                            toRow[0][column.ColumnName] = $"{fromTable.Rows[i][column.ColumnName]} ▷ {toRow[0][column.ColumnName]}";
                            isModify = true;
                        }
                    }

                    // 변경된 테이블로 넣어준다.
                    object[] datas = null;
                    if (isModify)
                    {
                        datas = toRow[0].ItemArray;
                        resultTable.Rows.Add(datas);
                    }
                    isModify = false;
                }
                // 삭제, 추가 데이터
                // from에만 데이터가 있으면 삭제, to 에만 데이터가 있으면 추가
                else
                {
                    fromTable.Rows[i]["변경유형"] = "삭제";
                    AddRow(resultTable, fromTable.Rows[i]);
                }
            }
            for (int i = 0; i < toTable.Rows.Count; ++i)
            {
                string selectString = string.Empty;

                for (int j = 0; j < _keywardList.Count; ++j)
                {
                    selectString += $"{_keywardList[j]}=\'{toTable.Rows[i].ItemArray[keyColumnIndex[j]]}\' AND ";
                }
                selectString = selectString.Remove(selectString.Length - 4, 4);
                DataRow[] fromRow = fromTable.Select(selectString);

                if (fromRow.Length > 0)
                    continue;
                else
                {
                    toTable.Rows[i]["변경유형"] = "추가";
                    AddRow(resultTable, toTable.Rows[i]);
                }
            }

            foreach (DataRow row in toTable.Rows)
            {
                row.EndEdit();
            }

        }

        private void combineColumn(DataTable moreDT, DataTable fewDT)
        {
            foreach (DataColumn column in moreDT.Columns)
            {
                if (fewDT.Columns.Contains(column.ColumnName) == false)
                    fewDT.Columns.Add(column.ColumnName);
            }
        }

        //비교를 위해 컬럼 밸런스를 맞추는 작업.
        private void SetColumnBalance(DataTable fromTable, DataTable toTable)
        {
            // from이 더많은 경우는 컬럼 삭제..
            //if (fromTable.Columns.Count > toTable.Columns.Count)
            //{
            combineColumn(fromTable, toTable);
            //}
            // 컬럼이 추가된 경우
            //if (fromTable.Columns.Count < toTable.Columns.Count)
            //{
            combineColumn(toTable, fromTable);
            //}

        }

        private bool CheckTableExits(string TableName, string DBName)
        {
            var cmd = new MySqlCommand($"SELECT EXISTS (SELECT 1 FROM Information_schema.tables WHERE table_schema = '{DBName}' AND TABLE_NAME = '{TableName}') AS flag", _fromDB.Connect);
            var reader = cmd.ExecuteReader();
            string check = "0";
            while (reader.Read())
            {
                check = reader["flag"].ToString();
            }
            reader.Close();

            return check == "1" ? true : false;
        }

        private void AddNewTable(ref DataTable resultTable, DataTable toTable)
        {
            resultTable = toTable.Copy();
            resultTable.Columns.Add("변경유형");
            resultTable.Columns["변경유형"].SetOrdinal(0);

            foreach (DataRow row in resultTable.Rows)
            {
                row.BeginEdit();
                row["변경유형"] = "추가";
                row.EndEdit();
            }
            resultTable.TableName = toTable.TableName;
        }

        private DataTable getDataTable(string toTableName, DataBaseManager DB)
        {
            DataSet ds = new DataSet();
            var cmd_to = new MySqlCommand($"SELECT * FROM {toTableName}", DB.Connect);
            var adpt_to = new MySqlDataAdapter(cmd_to);
            adpt_to.Fill(ds);

            return ds.Tables[0]; ;
        }

        private List<int> getStandardColumnKey(DataTable fromTable)
        {
            _keywardList.Clear();
            List<int> key = new List<int>();
            foreach (var table in _dicStandardColumn)
            {
                if (table.Key.Contains(fromTable.TableName))
                {
                    string[] values = table.Value.Split(':');
                    foreach(var value in values)
                    {
                        key.Add(fromTable.Columns.IndexOf(value));
                        _keywardList.Add(value);
                    }
                }
            }

            if (key.Count == 0)
            {
                _keywardList.Add($"{fromTable.Columns[1]}");
                key.Add(1);
            }

            return key;
        }

        private void ExecuteTableDiff(string fromTableName, string toTableName)
        {
            DataTable fromTable = null;
            DataTable toTable = null;
            DataTable resultTable = null;

            // to 테이블 가져오기
            toTable = getDataTable(toTableName, _toDB);

            // from 테이블 가져오기
            if (CheckTableExits(fromTableName, _fromDBName) == false)
            {
                // 없으면 그냥 리턴
                AddNewTable(ref resultTable, toTable);
                return;
            }


            fromTable = getDataTable(fromTableName, _fromDB);
            fromTable.TableName = fromTableName;

            // 테이블 컬럼 갯수가 다를경우 맞춰주기 위해서 하는 작업.
            SetColumnBalance(fromTable, toTable);
            resultTable = toTable.Clone();
            resultTable.TableName = toTableName;

            resultTable = toTable.Clone();
            resultTable.TableName = toTableName;


            // 테이블에 변경유형 컬럼 추가
            fromTable.Columns.Add("변경유형");
            fromTable.Columns["변경유형"].SetOrdinal(0);

            toTable.Columns.Add("변경유형");
            toTable.Columns["변경유형"].SetOrdinal(0);

            resultTable.Columns.Add("변경유형");
            resultTable.Columns["변경유형"].SetOrdinal(0);

            // 키값 넣기. 기준값을 따로 설정한애가 아니면 Name을 쓴다.
            List<int> keyColumnIndex = getStandardColumnKey(fromTable);

            // 일단 기준컬럼(NAME)이 없는 테이블은 비교 하지 않음.
            if (keyColumnIndex.Count == 0 || keyColumnIndex[0] == -1)
                return;

            // 테이블 비교
            CheckDataTable(fromTable, toTable, resultTable, keyColumnIndex);

            if (resultTable.Rows.Count > 0)
            {
                List<string> convertedColumnsName = new List<string>();
                convertedColumnsName = ExcuteConvertColumnsName(resultTable);

                // 컬럼 번역명 행 추가 선언
                DataRow columnsNameRow = resultTable.NewRow();

                for (int i = 0; i<resultTable.Columns.Count; i++)
                {
                    columnsNameRow[i] = convertedColumnsName[i];
                }
                resultTable.Rows.InsertAt(columnsNameRow, 0);
                
                ResultDataSet.Tables.Add(resultTable);
            }
          
            Common.GlobalLog.Instance.Log(resultTable.TableName + ", check done");
        }

        // 결과 테이블 컬럼 변역 진행
        private List<string> ExcuteConvertColumnsName(DataTable resultTable)
        {
            // 컬럼명 영문, 번역문 딕셔너리
            Dictionary<string, string> ColumnNameList = ColumnConverter.ReadColumnText();
            // 테이블 컬럼명 배열에 가져오는 작업
            string[] tableColumns = resultTable.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();
            List<string> convertColumnsName = new List<string>();

            for (int i = 0; i < resultTable.Columns.Count; i++)
            {
                // 컬럼명이 번역문 딕셔너리에 존재할경우 리스트에 번역명 추가
                if (ColumnNameList.ContainsKey(tableColumns[i]))
                    convertColumnsName.Add(ColumnNameList[tableColumns[i]]);
                // 미존재시 공백 추가
                else
                    convertColumnsName.Add(" ");
            }
            // 변역명 리스트 반환
            return convertColumnsName;
        }
    }
}
