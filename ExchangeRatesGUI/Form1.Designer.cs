namespace ExchangeRatesGUI
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            AppName = new Label();
            treeView1 = new TreeView();
            Dates = new ListBox();
            textBoxCurrency = new TextBox();
            textBoxExchangeRate = new TextBox();
            labelChooseDate = new Label();
            labelCurrency = new Label();
            labelExchangeRate = new Label();
            buttonAddExchangeRate = new Button();
            buttonShowDate = new Button();
            buttonDownloadApi = new Button();
            buttonDeleteCurrency = new Button();
            labelAddCurrency = new Label();
            SuspendLayout();
            // 
            // AppName
            // 
            AppName.AutoSize = true;
            AppName.BackColor = SystemColors.ActiveCaption;
            AppName.Font = new Font("Snap ITC", 18F, FontStyle.Italic, GraphicsUnit.Point, 0);
            AppName.ForeColor = SystemColors.ActiveCaptionText;
            AppName.Location = new Point(239, 9);
            AppName.Name = "AppName";
            AppName.Size = new Size(348, 31);
            AppName.TabIndex = 0;
            AppName.Text = "Exchange Rates Api App";
            // 
            // treeView1
            // 
            treeView1.Location = new Point(12, 43);
            treeView1.Name = "treeView1";
            treeView1.Size = new Size(399, 395);
            treeView1.TabIndex = 1;
            treeView1.AfterSelect += treeView1_AfterSelect;
            // 
            // Dates
            // 
            Dates.FormattingEnabled = true;
            Dates.ItemHeight = 15;
            Dates.Location = new Point(417, 79);
            Dates.Name = "Dates";
            Dates.ScrollAlwaysVisible = true;
            Dates.Size = new Size(136, 34);
            Dates.TabIndex = 2;
            Dates.SelectedIndexChanged += Dates_SelectedIndexChanged;
            // 
            // textBoxCurrency
            // 
            textBoxCurrency.Location = new Point(417, 184);
            textBoxCurrency.Name = "textBoxCurrency";
            textBoxCurrency.Size = new Size(110, 23);
            textBoxCurrency.TabIndex = 3;
            textBoxCurrency.Text = "Only 3 first";
            // 
            // textBoxExchangeRate
            // 
            textBoxExchangeRate.Location = new Point(417, 233);
            textBoxExchangeRate.Name = "textBoxExchangeRate";
            textBoxExchangeRate.Size = new Size(113, 23);
            textBoxExchangeRate.TabIndex = 4;
            textBoxExchangeRate.Text = "ex.: 3.123";
            // 
            // labelChooseDate
            // 
            labelChooseDate.AutoSize = true;
            labelChooseDate.BackColor = SystemColors.Info;
            labelChooseDate.Font = new Font("Algerian", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelChooseDate.Location = new Point(417, 54);
            labelChooseDate.Name = "labelChooseDate";
            labelChooseDate.Size = new Size(145, 24);
            labelChooseDate.TabIndex = 5;
            labelChooseDate.Text = "Choose date";
            // 
            // labelCurrency
            // 
            labelCurrency.AutoSize = true;
            labelCurrency.BackColor = SystemColors.Info;
            labelCurrency.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 238);
            labelCurrency.Location = new Point(417, 161);
            labelCurrency.Name = "labelCurrency";
            labelCurrency.Size = new Size(66, 20);
            labelCurrency.TabIndex = 6;
            labelCurrency.Text = "Currency";
            // 
            // labelExchangeRate
            // 
            labelExchangeRate.AutoSize = true;
            labelExchangeRate.BackColor = SystemColors.Info;
            labelExchangeRate.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 238);
            labelExchangeRate.Location = new Point(417, 210);
            labelExchangeRate.Name = "labelExchangeRate";
            labelExchangeRate.Size = new Size(102, 20);
            labelExchangeRate.TabIndex = 7;
            labelExchangeRate.Text = "Exchange rate";
            // 
            // buttonAddExchangeRate
            // 
            buttonAddExchangeRate.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 238);
            buttonAddExchangeRate.Location = new Point(572, 184);
            buttonAddExchangeRate.Name = "buttonAddExchangeRate";
            buttonAddExchangeRate.Size = new Size(97, 53);
            buttonAddExchangeRate.TabIndex = 8;
            buttonAddExchangeRate.Text = "Add new currency";
            buttonAddExchangeRate.UseVisualStyleBackColor = true;
            buttonAddExchangeRate.Click += buttonAddExchangeRate_Click;
            // 
            // buttonShowDate
            // 
            buttonShowDate.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 238);
            buttonShowDate.Location = new Point(425, 363);
            buttonShowDate.Name = "buttonShowDate";
            buttonShowDate.Size = new Size(119, 52);
            buttonShowDate.TabIndex = 9;
            buttonShowDate.Text = "Show date";
            buttonShowDate.UseVisualStyleBackColor = true;
            buttonShowDate.Click += buttonShowDate_Click;
            // 
            // buttonDownloadApi
            // 
            buttonDownloadApi.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 238);
            buttonDownloadApi.Location = new Point(550, 363);
            buttonDownloadApi.Name = "buttonDownloadApi";
            buttonDownloadApi.Size = new Size(119, 52);
            buttonDownloadApi.TabIndex = 10;
            buttonDownloadApi.Text = "Download today's rates";
            buttonDownloadApi.UseVisualStyleBackColor = true;
            buttonDownloadApi.Click += buttonDownloadApi_Click;
            // 
            // buttonDeleteCurrency
            // 
            buttonDeleteCurrency.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 238);
            buttonDeleteCurrency.Location = new Point(675, 363);
            buttonDeleteCurrency.Name = "buttonDeleteCurrency";
            buttonDeleteCurrency.Size = new Size(119, 52);
            buttonDeleteCurrency.TabIndex = 11;
            buttonDeleteCurrency.Text = "Delete selected currency";
            buttonDeleteCurrency.UseVisualStyleBackColor = true;
            buttonDeleteCurrency.Click += buttonDeleteCurrency_Click;
            // 
            // labelAddCurrency
            // 
            labelAddCurrency.AutoSize = true;
            labelAddCurrency.BackColor = SystemColors.Info;
            labelAddCurrency.Font = new Font("Algerian", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelAddCurrency.Location = new Point(417, 128);
            labelAddCurrency.Name = "labelAddCurrency";
            labelAddCurrency.Size = new Size(240, 24);
            labelAddCurrency.TabIndex = 12;
            labelAddCurrency.Text = "Adding new currency";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(labelAddCurrency);
            Controls.Add(buttonDeleteCurrency);
            Controls.Add(buttonDownloadApi);
            Controls.Add(buttonShowDate);
            Controls.Add(buttonAddExchangeRate);
            Controls.Add(labelExchangeRate);
            Controls.Add(labelCurrency);
            Controls.Add(labelChooseDate);
            Controls.Add(textBoxExchangeRate);
            Controls.Add(textBoxCurrency);
            Controls.Add(Dates);
            Controls.Add(treeView1);
            Controls.Add(AppName);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label AppName;
        private TreeView treeView1;
        private ListBox Dates;
        private TextBox textBoxCurrency;
        private TextBox textBoxExchangeRate;
        private Label labelChooseDate;
        private Label labelCurrency;
        private Label labelExchangeRate;
        private Button buttonAddExchangeRate;
        private Button buttonShowDate;
        private Button buttonDownloadApi;
        private Button buttonDeleteCurrency;
        private Label labelAddCurrency;
    }
}
