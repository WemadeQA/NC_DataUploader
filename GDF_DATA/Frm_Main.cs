using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using WemadeQA.Common;

namespace GDF_DATA
{
    public partial class MIR4_GDF_UPLOADER : Form
    {
        private bool isUploading = false;
        private Dictionary<string, CheckBox> _dicDataBase = new Dictionary<string, CheckBox>();
        public MIR4_GDF_UPLOADER()
        {
            InitializeComponent();
            cb_Master.Checked = true;
            check_SendMail.Checked = true;
            cb_CheckPreTable.Checked = true;

            _dicDataBase.Add("nc_datatable_master", cb_Master);
            _dicDataBase.Add("nc_datatable_beta", cb_Beta);
            _dicDataBase.Add("nc_datatable_stage", cb_Stage);
            _dicDataBase.Add("nc_datatable_live", cb_live);

            WemadeQA.Common.ConfigData.SetConfigData();
        }

        public void UploadComplete(string CompleteSubject, string Completebody, bool isException, List<string> dataBaseList)
        {
            isUploading = false;
            btn_Upload.Text = "업로드";

            if (check_SendMail.Checked || isException)
            {
                MailManager mail = new MailManager(ConfigData.programSettingData["MailAddress"],
                    ConfigData.programSettingData["MaillPW"], "eutteumiyo@wemade.com");
                mail.SendEmail(CompleteSubject, Completebody);

                //mail.SetMailInfomation(ConfigData.programSettingData["MailAddress"],
                //    ConfigData.programSettingData["MaillPW"], "goodnight1996@wemade.com");
                //mail.SendEmail(CompleteSubject, Completebody);

                //mail.SetMailInfomation(ConfigData.programSettingData["MailAddress"],
                //    ConfigData.programSettingData["MaillPW"], "eutteumiyo@wemade.com");
                //mail.SendEmail(CompleteSubject, Completebody);

                //mail.SetMailInfomation(ConfigData.programSettingData["MailAddress"],
                //    ConfigData.programSettingData["MaillPW"], "frameset82@wemade.com");
                //mail.SendEmail(CompleteSubject, Completebody);

                //mail.SetMailInfomation(ConfigData.programSettingData["MailAddress"],
                //    ConfigData.programSettingData["MaillPW"], "yejin@wemade.com");
                //mail.SendEmail(CompleteSubject, Completebody);
            }
            // 오류난게 아니면 날짜 바꾸기
            if (isException == false)
            {
                //DataBaseManager dbManager = new DataBaseManager(ConfigData.programSettingData["MYSQLIP"], 
                //    "mir4_gdf_server", ConfigData.programSettingData["MYSQLID"], ConfigData.programSettingData["MYSQLPW"]);
                //foreach (var name in dataBaseList)
                //    dbManager.SetDataUpdate(name);
            }

            Common.GlobalLog.Instance.Log("====================================");
            Common.GlobalLog.Instance.Log("Upload Done.");
            Common.GlobalLog.Instance.Log("====================================");
        }

        private void btn_FolderPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.ShowDialog();
            rxtx_FolderPath.Text = dialog.SelectedPath;
        }

        private void btn_Upload_Click(object sender, EventArgs e)
        {
            if (isUploading)
            {
                isUploading = false;
                btn_Upload.Text = "업로드";
            }
            else
            {
                btn_Upload.Text = "업로드중";
                isUploading = true;

                string[] fileString = rxtx_FolderPath.Text.Split('\\');
                string fileName = fileString[fileString.Length - 1];
                List<string> dbList = new List<string>();
                List<string> diffList = new List<string>();

                foreach (var check in _dicDataBase)
                {
                    if (check.Value.Checked)
                    {
                        dbList.Add(check.Key);
                    }
                }

                // 사용할 json만 뽑기
                JsonConvert jsonConvert = new JsonConvert();
                jsonConvert.SetJsonFoler(rxtx_FolderPath.Text);

                UpLoder uploader = new UpLoder(rxtx_FolderPath.Text, fileName, new Common.LogRichTextBox(rtxt_Log));

                uploader.ComplateHandler = UploadComplete;
                uploader.Upload(dbList);

                TableDiff diff = null;
                DirectoryInfo DI = null;

                

                if (cb_CheckLiveTable.Checked)
                {
                    if (!cb_GlobalStage.Checked)
                    {
                        diff = new TableDiff(uploader.UploadDataSet, "nc_datatable_live", "nc_datatable_stage");
                        //diff = new TableDiff(uploader.UploadDataSet, "mir4_live", "mir4_stage");
                        DI = new DirectoryInfo(rxtx_FolderPath.Text + "\\Diff_Result_All");
                    }
                    else
                    {
                        //diff = new TableDiff(uploader.UploadDataSet, "mir4_live_global", "nc_datatable_stage_global");
                        ////diff = new TableDiff(uploader.UploadDataSet, "mir4_live", "mir4_stage");
                        //DI = new DirectoryInfo(rxtx_FolderPath.Text + "\\Diff_Result_All");
                    }         

                }
                else if (cb_CheckPreTable.Checked)
                {
                    if(!cb_GlobalBeta.Checked)
                    {
                        diff = new TableDiff(uploader.UploadDataSet, "nc_datatable_alpha", "nc_datatable_beta");
                        DI = new DirectoryInfo(rxtx_FolderPath.Text + "\\Diff_Result");
                    }
                    else
                    {
                        //diff = new TableDiff(uploader.UploadDataSet, "mir4_alpha_Global", "mir4_Beta_Global");
                        //DI = new DirectoryInfo(rxtx_FolderPath.Text + "\\Diff_Result");
                    }

                }

                if (DI != null)
                {
                    if (!DI.Exists)
                        DI.Create();
                }

                if (cb_CheckLiveTable.Checked || cb_CheckPreTable.Checked)
                {
                    diff.DoDiff();
                    //파일로 뽑아내기

                    ClosedXML CXML = new ClosedXML();
                    foreach (DataTable DT in diff.ResultDataSet.Tables)
                    {
                        CXML.ExportExcelSheet(DT, DI);
                        diffList.Add(DT.TableName);
                    }

                    Common.GlobalLog.Instance.Log("====================================");
                    Common.GlobalLog.Instance.Log("Diff Done.");
                    Common.GlobalLog.Instance.Log("====================================");

                    // 비교끝난후 alpha를 최신으로 업데이트
                    if (cb_CheckPreTable.Checked && cb_Beta.Checked)
                        uploader.OnlyUpload("nc_datatable_alpha");
                    //if (cb_CheckPreTable.Checked && cb_GlobalBeta.Checked)
                    //    uploader.OnlyUpload("mir4_alpha_global");
                }
                jsonConvert.UndoFolder();

                uploader.Complete(diffList);
            }
        }

        private void MIR4_GDF_UPLOADER_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void cb_CheckPreTable_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_CheckPreTable.Checked)
                cb_CheckLiveTable.Checked = false;
        }

        private void cb_CheckLiveTable_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_CheckLiveTable.Checked)
                cb_CheckPreTable.Checked = false;
        }

        private void rxtx_FolderPath_DragDrop(object sender, DragEventArgs e)
        {
            string Datapath = string.Empty;
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (string str in files)
                {
                    Datapath += str;
                }
            }
            rxtx_FolderPath.Text = Datapath;
        }

        private void rxtx_FolderPath_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy | DragDropEffects.Scroll;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void rtxt_Log_TextChanged(object sender, EventArgs e)
        {

        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void MIR4_GDF_UPLOADER_Load(object sender, EventArgs e)
        {

        }
    }
}