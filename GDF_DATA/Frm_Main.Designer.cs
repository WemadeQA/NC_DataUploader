namespace GDF_DATA
{
    partial class MIR4_GDF_UPLOADER
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MIR4_GDF_UPLOADER));
            this.label59 = new System.Windows.Forms.Label();
            this.rxtx_FolderPath = new System.Windows.Forms.TextBox();
            this.rtxt_Log = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_FolderPath = new System.Windows.Forms.Button();
            this.btn_Upload = new System.Windows.Forms.Button();
            this.check_SendMail = new System.Windows.Forms.CheckBox();
            this.cb_Master = new System.Windows.Forms.CheckBox();
            this.cb_Beta = new System.Windows.Forms.CheckBox();
            this.cb_Stage = new System.Windows.Forms.CheckBox();
            this.cb_live = new System.Windows.Forms.CheckBox();
            this.cb_CheckPreTable = new System.Windows.Forms.CheckBox();
            this.cb_CheckLiveTable = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cb_alpha = new System.Windows.Forms.CheckBox();
            this.cb_GlobalAlpha = new System.Windows.Forms.CheckBox();
            this.cb_GlobalLive = new System.Windows.Forms.CheckBox();
            this.cb_GlobalStage = new System.Windows.Forms.CheckBox();
            this.cb_GlobalBeta = new System.Windows.Forms.CheckBox();
            this.cb_GlobalMaster = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label59
            // 
            this.label59.AutoSize = true;
            this.label59.BackColor = System.Drawing.Color.White;
            this.label59.Font = new System.Drawing.Font("Infinity Sans Regular", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label59.Location = new System.Drawing.Point(12, 9);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(65, 15);
            this.label59.TabIndex = 107;
            this.label59.Text = "데이터 폴더";
            // 
            // rxtx_FolderPath
            // 
            this.rxtx_FolderPath.AllowDrop = true;
            this.rxtx_FolderPath.Font = new System.Drawing.Font("Infinity Sans Regular", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.rxtx_FolderPath.Location = new System.Drawing.Point(94, 6);
            this.rxtx_FolderPath.Name = "rxtx_FolderPath";
            this.rxtx_FolderPath.ReadOnly = true;
            this.rxtx_FolderPath.Size = new System.Drawing.Size(203, 23);
            this.rxtx_FolderPath.TabIndex = 106;
            this.rxtx_FolderPath.DragDrop += new System.Windows.Forms.DragEventHandler(this.rxtx_FolderPath_DragDrop);
            this.rxtx_FolderPath.DragEnter += new System.Windows.Forms.DragEventHandler(this.rxtx_FolderPath_DragEnter);
            // 
            // rtxt_Log
            // 
            this.rtxt_Log.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtxt_Log.BackColor = System.Drawing.Color.Black;
            this.rtxt_Log.Font = new System.Drawing.Font("Infinity Sans Regular", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.rtxt_Log.ForeColor = System.Drawing.Color.White;
            this.rtxt_Log.Location = new System.Drawing.Point(15, 239);
            this.rtxt_Log.Name = "rtxt_Log";
            this.rtxt_Log.ReadOnly = true;
            this.rtxt_Log.Size = new System.Drawing.Size(660, 328);
            this.rtxt_Log.TabIndex = 111;
            this.rtxt_Log.Text = "";
            this.rtxt_Log.TextChanged += new System.EventHandler(this.rtxt_Log_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Font = new System.Drawing.Font("Infinity Sans Regular", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.Location = new System.Drawing.Point(12, 221);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 15);
            this.label2.TabIndex = 112;
            this.label2.Text = "진행 내역";
            // 
            // btn_FolderPath
            // 
            this.btn_FolderPath.BackColor = System.Drawing.Color.White;
            this.btn_FolderPath.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_FolderPath.Font = new System.Drawing.Font("Infinity Sans Regular", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_FolderPath.Location = new System.Drawing.Point(303, 5);
            this.btn_FolderPath.Name = "btn_FolderPath";
            this.btn_FolderPath.Size = new System.Drawing.Size(75, 23);
            this.btn_FolderPath.TabIndex = 113;
            this.btn_FolderPath.Text = "검색";
            this.btn_FolderPath.UseVisualStyleBackColor = false;
            this.btn_FolderPath.Click += new System.EventHandler(this.btn_FolderPath_Click);
            // 
            // btn_Upload
            // 
            this.btn_Upload.BackColor = System.Drawing.Color.White;
            this.btn_Upload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Upload.Font = new System.Drawing.Font("Infinity Sans Regular", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_Upload.Location = new System.Drawing.Point(12, 168);
            this.btn_Upload.Name = "btn_Upload";
            this.btn_Upload.Size = new System.Drawing.Size(660, 39);
            this.btn_Upload.TabIndex = 114;
            this.btn_Upload.Text = "업로드";
            this.btn_Upload.UseVisualStyleBackColor = false;
            this.btn_Upload.Click += new System.EventHandler(this.btn_Upload_Click);
            // 
            // check_SendMail
            // 
            this.check_SendMail.AutoSize = true;
            this.check_SendMail.Font = new System.Drawing.Font("Infinity Sans Regular", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.check_SendMail.Location = new System.Drawing.Point(30, 118);
            this.check_SendMail.Name = "check_SendMail";
            this.check_SendMail.Size = new System.Drawing.Size(109, 19);
            this.check_SendMail.TabIndex = 116;
            this.check_SendMail.Text = "결과 메일 보내기";
            this.check_SendMail.UseVisualStyleBackColor = true;
            // 
            // cb_Master
            // 
            this.cb_Master.AutoSize = true;
            this.cb_Master.Font = new System.Drawing.Font("Infinity Sans Regular", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cb_Master.Location = new System.Drawing.Point(6, 48);
            this.cb_Master.Name = "cb_Master";
            this.cb_Master.Size = new System.Drawing.Size(92, 19);
            this.cb_Master.TabIndex = 117;
            this.cb_Master.Text = "NC_MASTER";
            this.cb_Master.UseVisualStyleBackColor = true;
            // 
            // cb_Beta
            // 
            this.cb_Beta.AutoSize = true;
            this.cb_Beta.Font = new System.Drawing.Font("Infinity Sans Regular", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cb_Beta.Location = new System.Drawing.Point(119, 48);
            this.cb_Beta.Name = "cb_Beta";
            this.cb_Beta.Size = new System.Drawing.Size(74, 19);
            this.cb_Beta.TabIndex = 118;
            this.cb_Beta.Text = "NC_BETA";
            this.cb_Beta.UseVisualStyleBackColor = true;
            // 
            // cb_Stage
            // 
            this.cb_Stage.AutoSize = true;
            this.cb_Stage.Font = new System.Drawing.Font("Infinity Sans Regular", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cb_Stage.Location = new System.Drawing.Point(215, 48);
            this.cb_Stage.Name = "cb_Stage";
            this.cb_Stage.Size = new System.Drawing.Size(78, 19);
            this.cb_Stage.TabIndex = 119;
            this.cb_Stage.Text = "NC_Stage";
            this.cb_Stage.UseVisualStyleBackColor = true;
            // 
            // cb_live
            // 
            this.cb_live.AutoSize = true;
            this.cb_live.Font = new System.Drawing.Font("Infinity Sans Regular", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cb_live.Location = new System.Drawing.Point(305, 48);
            this.cb_live.Name = "cb_live";
            this.cb_live.Size = new System.Drawing.Size(70, 19);
            this.cb_live.TabIndex = 120;
            this.cb_live.Text = "NC_LIVE";
            this.cb_live.UseVisualStyleBackColor = true;
            // 
            // cb_CheckPreTable
            // 
            this.cb_CheckPreTable.AutoSize = true;
            this.cb_CheckPreTable.Font = new System.Drawing.Font("Infinity Sans Regular", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cb_CheckPreTable.Location = new System.Drawing.Point(30, 143);
            this.cb_CheckPreTable.Name = "cb_CheckPreTable";
            this.cb_CheckPreTable.Size = new System.Drawing.Size(109, 19);
            this.cb_CheckPreTable.TabIndex = 121;
            this.cb_CheckPreTable.Text = "직전 버전과 비교";
            this.cb_CheckPreTable.UseVisualStyleBackColor = true;
            this.cb_CheckPreTable.CheckedChanged += new System.EventHandler(this.cb_CheckPreTable_CheckedChanged);
            // 
            // cb_CheckLiveTable
            // 
            this.cb_CheckLiveTable.AutoSize = true;
            this.cb_CheckLiveTable.Font = new System.Drawing.Font("Infinity Sans Regular", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cb_CheckLiveTable.Location = new System.Drawing.Point(143, 143);
            this.cb_CheckLiveTable.Name = "cb_CheckLiveTable";
            this.cb_CheckLiveTable.Size = new System.Drawing.Size(95, 19);
            this.cb_CheckLiveTable.TabIndex = 122;
            this.cb_CheckLiveTable.Text = "라이브와 비교";
            this.cb_CheckLiveTable.UseVisualStyleBackColor = true;
            this.cb_CheckLiveTable.CheckedChanged += new System.EventHandler(this.cb_CheckLiveTable_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("Infinity Sans Regular", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(10, 120);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(15, 15);
            this.label1.TabIndex = 123;
            this.label1.Text = "1.";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.White;
            this.label4.Font = new System.Drawing.Font("Infinity Sans Regular", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.Location = new System.Drawing.Point(9, 144);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 15);
            this.label4.TabIndex = 124;
            this.label4.Text = "2.";
            // 
            // cb_alpha
            // 
            this.cb_alpha.AutoSize = true;
            this.cb_alpha.Font = new System.Drawing.Font("Infinity Sans Regular", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cb_alpha.Location = new System.Drawing.Point(394, 48);
            this.cb_alpha.Name = "cb_alpha";
            this.cb_alpha.Size = new System.Drawing.Size(76, 19);
            this.cb_alpha.TabIndex = 125;
            this.cb_alpha.Text = "NC_alpha";
            this.cb_alpha.UseVisualStyleBackColor = true;
            // 
            // cb_GlobalAlpha
            // 
            this.cb_GlobalAlpha.AutoSize = true;
            this.cb_GlobalAlpha.Font = new System.Drawing.Font("Infinity Sans Regular", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cb_GlobalAlpha.Location = new System.Drawing.Point(394, 73);
            this.cb_GlobalAlpha.Name = "cb_GlobalAlpha";
            this.cb_GlobalAlpha.Size = new System.Drawing.Size(95, 19);
            this.cb_GlobalAlpha.TabIndex = 130;
            this.cb_GlobalAlpha.Text = "Global_alpha";
            this.cb_GlobalAlpha.UseVisualStyleBackColor = true;
            // 
            // cb_GlobalLive
            // 
            this.cb_GlobalLive.AutoSize = true;
            this.cb_GlobalLive.Font = new System.Drawing.Font("Infinity Sans Regular", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cb_GlobalLive.Location = new System.Drawing.Point(305, 73);
            this.cb_GlobalLive.Name = "cb_GlobalLive";
            this.cb_GlobalLive.Size = new System.Drawing.Size(89, 19);
            this.cb_GlobalLive.TabIndex = 129;
            this.cb_GlobalLive.Text = "Global_LIVE";
            this.cb_GlobalLive.UseVisualStyleBackColor = true;
            // 
            // cb_GlobalStage
            // 
            this.cb_GlobalStage.AutoSize = true;
            this.cb_GlobalStage.Font = new System.Drawing.Font("Infinity Sans Regular", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cb_GlobalStage.Location = new System.Drawing.Point(215, 73);
            this.cb_GlobalStage.Name = "cb_GlobalStage";
            this.cb_GlobalStage.Size = new System.Drawing.Size(97, 19);
            this.cb_GlobalStage.TabIndex = 128;
            this.cb_GlobalStage.Text = "Global_Stage";
            this.cb_GlobalStage.UseVisualStyleBackColor = true;
            // 
            // cb_GlobalBeta
            // 
            this.cb_GlobalBeta.AutoSize = true;
            this.cb_GlobalBeta.Font = new System.Drawing.Font("Infinity Sans Regular", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cb_GlobalBeta.Location = new System.Drawing.Point(119, 73);
            this.cb_GlobalBeta.Name = "cb_GlobalBeta";
            this.cb_GlobalBeta.Size = new System.Drawing.Size(93, 19);
            this.cb_GlobalBeta.TabIndex = 127;
            this.cb_GlobalBeta.Text = "Global_BETA";
            this.cb_GlobalBeta.UseVisualStyleBackColor = true;
            // 
            // cb_GlobalMaster
            // 
            this.cb_GlobalMaster.AutoSize = true;
            this.cb_GlobalMaster.Font = new System.Drawing.Font("Infinity Sans Regular", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cb_GlobalMaster.Location = new System.Drawing.Point(6, 73);
            this.cb_GlobalMaster.Name = "cb_GlobalMaster";
            this.cb_GlobalMaster.Size = new System.Drawing.Size(111, 19);
            this.cb_GlobalMaster.TabIndex = 126;
            this.cb_GlobalMaster.Text = "Global_MASTER";
            this.cb_GlobalMaster.UseVisualStyleBackColor = true;
            // 
            // MIR4_GDF_UPLOADER
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(684, 578);
            this.Controls.Add(this.cb_GlobalAlpha);
            this.Controls.Add(this.cb_GlobalLive);
            this.Controls.Add(this.cb_GlobalStage);
            this.Controls.Add(this.cb_GlobalBeta);
            this.Controls.Add(this.cb_GlobalMaster);
            this.Controls.Add(this.cb_alpha);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cb_CheckLiveTable);
            this.Controls.Add(this.cb_CheckPreTable);
            this.Controls.Add(this.cb_live);
            this.Controls.Add(this.cb_Stage);
            this.Controls.Add(this.cb_Beta);
            this.Controls.Add(this.cb_Master);
            this.Controls.Add(this.check_SendMail);
            this.Controls.Add(this.btn_Upload);
            this.Controls.Add(this.btn_FolderPath);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.rtxt_Log);
            this.Controls.Add(this.label59);
            this.Controls.Add(this.rxtx_FolderPath);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MIR4_GDF_UPLOADER";
            this.Text = "NC_GDF_UPLOADER";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MIR4_GDF_UPLOADER_FormClosing);
            this.Load += new System.EventHandler(this.MIR4_GDF_UPLOADER_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.TextBox rxtx_FolderPath;
        private System.Windows.Forms.RichTextBox rtxt_Log;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_FolderPath;
        private System.Windows.Forms.Button btn_Upload;
        private System.Windows.Forms.CheckBox check_SendMail;
        private System.Windows.Forms.CheckBox cb_Master;
        private System.Windows.Forms.CheckBox cb_Beta;
        private System.Windows.Forms.CheckBox cb_Stage;
        private System.Windows.Forms.CheckBox cb_live;
        private System.Windows.Forms.CheckBox cb_CheckPreTable;
        private System.Windows.Forms.CheckBox cb_CheckLiveTable;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox cb_alpha;
        private System.Windows.Forms.CheckBox cb_GlobalAlpha;
        private System.Windows.Forms.CheckBox cb_GlobalLive;
        private System.Windows.Forms.CheckBox cb_GlobalStage;
        private System.Windows.Forms.CheckBox cb_GlobalBeta;
        private System.Windows.Forms.CheckBox cb_GlobalMaster;
    }
}

