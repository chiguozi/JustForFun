using System;
using System.Drawing;
using System.Windows.Forms;

namespace ExcelTools
{
    partial class BuildExcelTools
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BuildExcelTools));
            this.excelPathTitle = new System.Windows.Forms.Label();
            this.excelPath = new System.Windows.Forms.TextBox();
            this.excelBrowser_btn = new System.Windows.Forms.Button();
            this.searchTitle = new System.Windows.Forms.Label();
            this.build_create_btn = new System.Windows.Forms.Button();
            this.selectListBox = new System.Windows.Forms.ListBox();
            this.previewListBox = new System.Windows.Forms.ListBox();
            this.allSet_btn = new System.Windows.Forms.Button();
            this.clearAllSet_btn = new System.Windows.Forms.Button();
            this.addSet_btn = new System.Windows.Forms.Button();
            this.removeSet_btn = new System.Windows.Forms.Button();
            this.searchSetTxt = new System.Windows.Forms.TextBox();
            this.searchPreTxt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buildStateBox = new System.Windows.Forms.ListBox();
            this.createCs_btn = new System.Windows.Forms.Button();
            this.buildExcel_btn = new System.Windows.Forms.Button();
            this.clearContext_btn = new System.Windows.Forms.Button();
            this.csFile_btn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.csFileLabel = new System.Windows.Forms.TextBox();
            this.dataFile_btn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.dataFileLabel = new System.Windows.Forms.TextBox();
            this.reset_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // excelPathTitle
            // 
            resources.ApplyResources(this.excelPathTitle, "excelPathTitle");
            this.excelPathTitle.Name = "excelPathTitle";
            // 
            // excelPath
            // 
            resources.ApplyResources(this.excelPath, "excelPath");
            this.excelPath.Name = "excelPath";
            this.excelPath.ReadOnly = true;
            // 
            // excelBrowser_btn
            // 
            resources.ApplyResources(this.excelBrowser_btn, "excelBrowser_btn");
            this.excelBrowser_btn.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.excelBrowser_btn.Name = "excelBrowser_btn";
            this.excelBrowser_btn.UseVisualStyleBackColor = true;
            this.excelBrowser_btn.Click += new System.EventHandler(this.BrowserFolderPanel);
            // 
            // searchTitle
            // 
            resources.ApplyResources(this.searchTitle, "searchTitle");
            this.searchTitle.ForeColor = System.Drawing.Color.Red;
            this.searchTitle.Name = "searchTitle";
            // 
            // build_create_btn
            // 
            resources.ApplyResources(this.build_create_btn, "build_create_btn");
            this.build_create_btn.BackColor = System.Drawing.Color.White;
            this.build_create_btn.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.build_create_btn.Name = "build_create_btn";
            this.build_create_btn.UseVisualStyleBackColor = false;
            // 
            // selectListBox
            // 
            resources.ApplyResources(this.selectListBox, "selectListBox");
            this.selectListBox.FormattingEnabled = true;
            this.selectListBox.Name = "selectListBox";
            this.selectListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            // 
            // previewListBox
            // 
            resources.ApplyResources(this.previewListBox, "previewListBox");
            this.previewListBox.FormattingEnabled = true;
            this.previewListBox.Name = "previewListBox";
            this.previewListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            // 
            // allSet_btn
            // 
            resources.ApplyResources(this.allSet_btn, "allSet_btn");
            this.allSet_btn.Name = "allSet_btn";
            this.allSet_btn.UseVisualStyleBackColor = true;
            // 
            // clearAllSet_btn
            // 
            resources.ApplyResources(this.clearAllSet_btn, "clearAllSet_btn");
            this.clearAllSet_btn.Name = "clearAllSet_btn";
            this.clearAllSet_btn.UseVisualStyleBackColor = true;
            // 
            // addSet_btn
            // 
            resources.ApplyResources(this.addSet_btn, "addSet_btn");
            this.addSet_btn.Name = "addSet_btn";
            this.addSet_btn.UseVisualStyleBackColor = true;
            // 
            // removeSet_btn
            // 
            resources.ApplyResources(this.removeSet_btn, "removeSet_btn");
            this.removeSet_btn.Name = "removeSet_btn";
            this.removeSet_btn.UseVisualStyleBackColor = true;
            // 
            // searchSetTxt
            // 
            resources.ApplyResources(this.searchSetTxt, "searchSetTxt");
            this.searchSetTxt.Name = "searchSetTxt";
            // 
            // searchPreTxt
            // 
            resources.ApplyResources(this.searchPreTxt, "searchPreTxt");
            this.searchPreTxt.Name = "searchPreTxt";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Name = "label1";
            // 
            // buildStateBox
            // 
            resources.ApplyResources(this.buildStateBox, "buildStateBox");
            this.buildStateBox.FormattingEnabled = true;
            this.buildStateBox.Name = "buildStateBox";
            // 
            // createCs_btn
            // 
            resources.ApplyResources(this.createCs_btn, "createCs_btn");
            this.createCs_btn.BackColor = System.Drawing.Color.Green;
            this.createCs_btn.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.createCs_btn.Name = "createCs_btn";
            this.createCs_btn.UseVisualStyleBackColor = false;
            // 
            // buildExcel_btn
            // 
            resources.ApplyResources(this.buildExcel_btn, "buildExcel_btn");
            this.buildExcel_btn.BackColor = System.Drawing.Color.Red;
            this.buildExcel_btn.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.buildExcel_btn.Name = "buildExcel_btn";
            this.buildExcel_btn.UseVisualStyleBackColor = false;
            // 
            // clearContext_btn
            // 
            resources.ApplyResources(this.clearContext_btn, "clearContext_btn");
            this.clearContext_btn.Name = "clearContext_btn";
            this.clearContext_btn.UseVisualStyleBackColor = true;
            // 
            // csFile_btn
            // 
            resources.ApplyResources(this.csFile_btn, "csFile_btn");
            this.csFile_btn.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.csFile_btn.Name = "csFile_btn";
            this.csFile_btn.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // csFileLabel
            // 
            resources.ApplyResources(this.csFileLabel, "csFileLabel");
            this.csFileLabel.Name = "csFileLabel";
            this.csFileLabel.ReadOnly = true;
            // 
            // dataFile_btn
            // 
            resources.ApplyResources(this.dataFile_btn, "dataFile_btn");
            this.dataFile_btn.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.dataFile_btn.Name = "dataFile_btn";
            this.dataFile_btn.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // dataFileLabel
            // 
            resources.ApplyResources(this.dataFileLabel, "dataFileLabel");
            this.dataFileLabel.Name = "dataFileLabel";
            this.dataFileLabel.ReadOnly = true;
            // 
            // reset_btn
            // 
            resources.ApplyResources(this.reset_btn, "reset_btn");
            this.reset_btn.BackColor = System.Drawing.Color.Yellow;
            this.reset_btn.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.reset_btn.Name = "reset_btn";
            this.reset_btn.UseVisualStyleBackColor = false;
            // 
            // BuildExcelTools
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.reset_btn);
            this.Controls.Add(this.dataFile_btn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dataFileLabel);
            this.Controls.Add(this.csFile_btn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.csFileLabel);
            this.Controls.Add(this.clearContext_btn);
            this.Controls.Add(this.buildExcel_btn);
            this.Controls.Add(this.createCs_btn);
            this.Controls.Add(this.buildStateBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.searchPreTxt);
            this.Controls.Add(this.removeSet_btn);
            this.Controls.Add(this.addSet_btn);
            this.Controls.Add(this.clearAllSet_btn);
            this.Controls.Add(this.allSet_btn);
            this.Controls.Add(this.previewListBox);
            this.Controls.Add(this.selectListBox);
            this.Controls.Add(this.build_create_btn);
            this.Controls.Add(this.searchTitle);
            this.Controls.Add(this.searchSetTxt);
            this.Controls.Add(this.excelBrowser_btn);
            this.Controls.Add(this.excelPathTitle);
            this.Controls.Add(this.excelPath);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BuildExcelTools";
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion

        private TextBox excelPath;
        private Label excelPathTitle;
        private Button excelBrowser_btn;
        private Label searchTitle;
        private Button build_create_btn;
        private ListBox selectListBox;
        private ListBox previewListBox;
        private Button allSet_btn;
        private Button clearAllSet_btn;
        private Button addSet_btn;
        private Button removeSet_btn;
        private TextBox searchSetTxt;
        private TextBox searchPreTxt;
        private Label label1;
        private ListBox buildStateBox;
        private Button createCs_btn;
        private Button buildExcel_btn;
        private Button clearContext_btn;
        private Button csFile_btn;
        private Label label2;
        private TextBox csFileLabel;
        private Button dataFile_btn;
        private Label label3;
        private TextBox dataFileLabel;
        private Button reset_btn;

    }
}

