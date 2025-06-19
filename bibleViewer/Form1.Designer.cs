namespace bibleViewer
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
            btnStart = new Button();
            itemBox = new ComboBox();
            chapterBox = new ComboBox();
            verseBox = new ComboBox();
            verseBox2 = new ComboBox();
            resultDataGridView = new DataGridView();
            btnShowInPPT = new Button();
            itemLabel = new Label();
            label2 = new Label();
            verseLabel = new Label();
            fastSearchLabel = new Label();
            fastSearchTextBox = new TextBox();
            label5 = new Label();
            ((System.ComponentModel.ISupportInitialize)resultDataGridView).BeginInit();
            SuspendLayout();
            // 
            // btnStart
            // 
            btnStart.Location = new Point(118, 270);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(94, 29);
            btnStart.TabIndex = 1;
            btnStart.Text = "찾기";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += btnStart_Click;
            // 
            // itemBox
            // 
            itemBox.DropDownHeight = 200;
            itemBox.Font = new Font("맑은 고딕", 10F, FontStyle.Regular, GraphicsUnit.Point);
            itemBox.FormattingEnabled = true;
            itemBox.IntegralHeight = false;
            itemBox.ItemHeight = 23;
            itemBox.Location = new Point(120, 119);
            itemBox.Name = "itemBox";
            itemBox.Size = new Size(200, 31);
            itemBox.TabIndex = 3;
            itemBox.Text = "창세기";
            itemBox.SelectedIndexChanged += itemBox_SelectedIndexChanged;
            // 
            // chapterBox
            // 
            chapterBox.DropDownHeight = 200;
            chapterBox.Font = new Font("맑은 고딕", 10F, FontStyle.Regular, GraphicsUnit.Point);
            chapterBox.FormattingEnabled = true;
            chapterBox.IntegralHeight = false;
            chapterBox.ItemHeight = 23;
            chapterBox.Location = new Point(120, 168);
            chapterBox.Name = "chapterBox";
            chapterBox.Size = new Size(200, 31);
            chapterBox.TabIndex = 4;
            chapterBox.Text = "1";
            chapterBox.SelectedIndexChanged += chapterBox_SelectedIndexChanged;
            // 
            // verseBox
            // 
            verseBox.DropDownHeight = 200;
            verseBox.Font = new Font("맑은 고딕", 10F, FontStyle.Regular, GraphicsUnit.Point);
            verseBox.FormattingEnabled = true;
            verseBox.IntegralHeight = false;
            verseBox.ItemHeight = 23;
            verseBox.Location = new Point(120, 217);
            verseBox.Name = "verseBox";
            verseBox.Size = new Size(63, 31);
            verseBox.TabIndex = 5;
            verseBox.Text = "1";
            // 
            // verseBox2
            // 
            verseBox2.DropDownHeight = 200;
            verseBox2.Font = new Font("맑은 고딕", 10F, FontStyle.Regular, GraphicsUnit.Point);
            verseBox2.FormattingEnabled = true;
            verseBox2.IntegralHeight = false;
            verseBox2.ItemHeight = 23;
            verseBox2.Location = new Point(257, 217);
            verseBox2.Name = "verseBox2";
            verseBox2.Size = new Size(63, 31);
            verseBox2.TabIndex = 7;
            verseBox2.Text = "1";
            // 
            // resultDataGridView
            // 
            resultDataGridView.AllowUserToAddRows = false;
            resultDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            resultDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resultDataGridView.Location = new Point(331, 73);
            resultDataGridView.Name = "resultDataGridView";
            resultDataGridView.ReadOnly = true;
            resultDataGridView.RowHeadersWidth = 51;
            resultDataGridView.RowTemplate.Height = 29;
            resultDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            resultDataGridView.Size = new Size(680, 395);
            resultDataGridView.TabIndex = 8;
            // 
            // btnShowInPPT
            // 
            btnShowInPPT.Location = new Point(226, 270);
            btnShowInPPT.Name = "btnShowInPPT";
            btnShowInPPT.Size = new Size(94, 29);
            btnShowInPPT.TabIndex = 9;
            btnShowInPPT.Text = "송출";
            btnShowInPPT.UseVisualStyleBackColor = true;
            btnShowInPPT.Click += btnShowInPPT_Click;
            // 
            // itemLabel
            // 
            itemLabel.Font = new Font("맑은 고딕", 10F, FontStyle.Regular, GraphicsUnit.Point);
            itemLabel.Location = new Point(12, 119);
            itemLabel.Name = "itemLabel";
            itemLabel.Size = new Size(81, 31);
            itemLabel.TabIndex = 10;
            itemLabel.Text = "성경 권:";
            itemLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            label2.Font = new Font("맑은 고딕", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(12, 168);
            label2.Name = "label2";
            label2.Size = new Size(81, 31);
            label2.TabIndex = 11;
            label2.Text = "장:";
            label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // verseLabel
            // 
            verseLabel.Font = new Font("맑은 고딕", 10F, FontStyle.Regular, GraphicsUnit.Point);
            verseLabel.Location = new Point(12, 217);
            verseLabel.Name = "verseLabel";
            verseLabel.Size = new Size(81, 31);
            verseLabel.TabIndex = 12;
            verseLabel.Text = "절:";
            verseLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fastSearchLabel
            // 
            fastSearchLabel.Font = new Font("맑은 고딕", 10F, FontStyle.Regular, GraphicsUnit.Point);
            fastSearchLabel.Location = new Point(12, 73);
            fastSearchLabel.Name = "fastSearchLabel";
            fastSearchLabel.Size = new Size(100, 31);
            fastSearchLabel.TabIndex = 13;
            fastSearchLabel.Text = "빠른 검색:";
            fastSearchLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fastSearchTextBox
            // 
            fastSearchTextBox.Location = new Point(120, 73);
            fastSearchTextBox.Multiline = true;
            fastSearchTextBox.Name = "fastSearchTextBox";
            fastSearchTextBox.Size = new Size(200, 31);
            fastSearchTextBox.TabIndex = 14;
            fastSearchTextBox.KeyDown += textBox1_KeyDown;
            // 
            // label5
            // 
            label5.Font = new Font("맑은 고딕", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label5.Location = new Point(206, 217);
            label5.Name = "label5";
            label5.Size = new Size(30, 31);
            label5.TabIndex = 15;
            label5.Text = "~";
            label5.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1039, 495);
            Controls.Add(label5);
            Controls.Add(fastSearchTextBox);
            Controls.Add(fastSearchLabel);
            Controls.Add(verseLabel);
            Controls.Add(label2);
            Controls.Add(itemLabel);
            Controls.Add(btnShowInPPT);
            Controls.Add(resultDataGridView);
            Controls.Add(verseBox2);
            Controls.Add(verseBox);
            Controls.Add(chapterBox);
            Controls.Add(itemBox);
            Controls.Add(btnStart);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)resultDataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btnStart;
        private ComboBox itemBox;
        private ComboBox chapterBox;
        private ComboBox verseBox;
        private ComboBox verseBox2;
        private DataGridView resultDataGridView;
        private Button btnShowInPPT;
        private Label itemLabel;
        private Label label2;
        private Label verseLabel;
        private Label fastSearchLabel;
        private TextBox fastSearchTextBox;
        private Label label5;
    }
}