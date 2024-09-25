namespace FileWatchmanSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            panel1 = new Panel();
            btnInstallUninstallService = new Button();
            btnStopService = new Button();
            btnStartService = new Button();
            btnSaveSettings = new Button();
            panel2 = new Panel();
            panel3 = new Panel();
            panel4 = new Panel();
            panel5 = new Panel();
            lblEmailValidation = new Label();
            btnBrowse = new Button();
            comboBoxTimerInterval = new ComboBox();
            txtToEmailName = new TextBox();
            txtToFilter = new TextBox();
            txtToEmailAddress = new TextBox();
            txtPathToWatch = new TextBox();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            folderBrowserDialog1 = new FolderBrowserDialog();
            panel1.SuspendLayout();
            panel5.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(btnInstallUninstallService);
            panel1.Controls.Add(btnStopService);
            panel1.Controls.Add(btnStartService);
            panel1.Controls.Add(btnSaveSettings);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 398);
            panel1.Name = "panel1";
            panel1.Size = new Size(800, 52);
            panel1.TabIndex = 0;
            // 
            // btnInstallUninstallService
            // 
            btnInstallUninstallService.Location = new Point(543, 11);
            btnInstallUninstallService.Name = "btnInstallUninstallService";
            btnInstallUninstallService.Size = new Size(145, 29);
            btnInstallUninstallService.TabIndex = 3;
            btnInstallUninstallService.Text = "Install or Uninstall";
            btnInstallUninstallService.UseVisualStyleBackColor = true;
            btnInstallUninstallService.Click += btnInstallUninstallService_Click;
            // 
            // btnStopService
            // 
            btnStopService.Location = new Point(392, 11);
            btnStopService.Name = "btnStopService";
            btnStopService.Size = new Size(145, 29);
            btnStopService.TabIndex = 2;
            btnStopService.Text = "Stop Service";
            btnStopService.UseVisualStyleBackColor = true;
            btnStopService.Click += btnStopService_Click;
            // 
            // btnStartService
            // 
            btnStartService.Location = new Point(241, 11);
            btnStartService.Name = "btnStartService";
            btnStartService.Size = new Size(145, 29);
            btnStartService.TabIndex = 1;
            btnStartService.Text = "Start Service";
            btnStartService.UseVisualStyleBackColor = true;
            btnStartService.Click += btnStartService_Click;
            // 
            // btnSaveSettings
            // 
            btnSaveSettings.Location = new Point(90, 11);
            btnSaveSettings.Name = "btnSaveSettings";
            btnSaveSettings.Size = new Size(145, 29);
            btnSaveSettings.TabIndex = 0;
            btnSaveSettings.Text = "Save Settings";
            btnSaveSettings.UseVisualStyleBackColor = true;
            btnSaveSettings.Click += btnSaveSettings_Click;
            // 
            // panel2
            // 
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(800, 42);
            panel2.TabIndex = 1;
            // 
            // panel3
            // 
            panel3.Dock = DockStyle.Right;
            panel3.Location = new Point(753, 42);
            panel3.Name = "panel3";
            panel3.Size = new Size(47, 356);
            panel3.TabIndex = 2;
            // 
            // panel4
            // 
            panel4.Dock = DockStyle.Left;
            panel4.Location = new Point(0, 42);
            panel4.Name = "panel4";
            panel4.Size = new Size(42, 356);
            panel4.TabIndex = 1;
            // 
            // panel5
            // 
            panel5.Controls.Add(lblEmailValidation);
            panel5.Controls.Add(btnBrowse);
            panel5.Controls.Add(comboBoxTimerInterval);
            panel5.Controls.Add(txtToEmailName);
            panel5.Controls.Add(txtToFilter);
            panel5.Controls.Add(txtToEmailAddress);
            panel5.Controls.Add(txtPathToWatch);
            panel5.Controls.Add(label5);
            panel5.Controls.Add(label4);
            panel5.Controls.Add(label3);
            panel5.Controls.Add(label2);
            panel5.Controls.Add(label1);
            panel5.Dock = DockStyle.Fill;
            panel5.Location = new Point(42, 42);
            panel5.Name = "panel5";
            panel5.Size = new Size(711, 356);
            panel5.TabIndex = 1;
            // 
            // lblEmailValidation
            // 
            lblEmailValidation.AutoSize = true;
            lblEmailValidation.Location = new Point(540, 108);
            lblEmailValidation.Name = "lblEmailValidation";
            lblEmailValidation.Size = new Size(0, 20);
            lblEmailValidation.TabIndex = 12;
            // 
            // btnBrowse
            // 
            btnBrowse.ForeColor = SystemColors.ActiveCaptionText;
            btnBrowse.Image = (Image)resources.GetObject("btnBrowse.Image");
            btnBrowse.Location = new Point(303, 65);
            btnBrowse.Name = "btnBrowse";
            btnBrowse.Size = new Size(41, 40);
            btnBrowse.TabIndex = 11;
            btnBrowse.UseVisualStyleBackColor = true;
            btnBrowse.Click += btnBrowse_Click;
            // 
            // comboBoxTimerInterval
            // 
            comboBoxTimerInterval.FormattingEnabled = true;
            comboBoxTimerInterval.Location = new Point(305, 204);
            comboBoxTimerInterval.Name = "comboBoxTimerInterval";
            comboBoxTimerInterval.Size = new Size(151, 28);
            comboBoxTimerInterval.TabIndex = 10;
            // 
            // txtToEmailName
            // 
            txtToEmailName.Location = new Point(305, 138);
            txtToEmailName.Name = "txtToEmailName";
            txtToEmailName.Size = new Size(229, 27);
            txtToEmailName.TabIndex = 9;
            // 
            // txtToFilter
            // 
            txtToFilter.Location = new Point(305, 171);
            txtToFilter.Name = "txtToFilter";
            txtToFilter.Size = new Size(229, 27);
            txtToFilter.TabIndex = 7;
            // 
            // txtToEmailAddress
            // 
            txtToEmailAddress.Location = new Point(305, 105);
            txtToEmailAddress.Name = "txtToEmailAddress";
            txtToEmailAddress.Size = new Size(229, 27);
            txtToEmailAddress.TabIndex = 6;
            txtToEmailAddress.TextChanged += txtToEmailAddress_TextChanged;
            // 
            // txtPathToWatch
            // 
            txtPathToWatch.Location = new Point(350, 72);
            txtPathToWatch.Name = "txtPathToWatch";
            txtPathToWatch.Size = new Size(229, 27);
            txtPathToWatch.TabIndex = 5;
            txtPathToWatch.TextChanged += txtPathToWatch_TextChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(159, 207);
            label5.Name = "label5";
            label5.Size = new Size(140, 20);
            label5.TabIndex = 4;
            label5.Text = "Timer Interval (ms) :";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(230, 174);
            label4.Name = "label4";
            label4.Size = new Size(69, 20);
            label4.TabIndex = 3;
            label4.Text = "To Filter :";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(182, 141);
            label3.Name = "label3";
            label3.Size = new Size(117, 20);
            label3.TabIndex = 2;
            label3.Text = "To Email Name :";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(171, 108);
            label2.Name = "label2";
            label2.Size = new Size(130, 20);
            label2.TabIndex = 1;
            label2.Text = "To Email Address :";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(192, 75);
            label1.Name = "label1";
            label1.Size = new Size(109, 20);
            label1.TabIndex = 0;
            label1.Text = "Path To Watch :";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(panel5);
            Controls.Add(panel4);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "File Watchman";
            panel1.ResumeLayout(false);
            panel5.ResumeLayout(false);
            panel5.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Button btnInstallUninstallService;
        private Button btnStopService;
        private Button btnStartService;
        private Button btnSaveSettings;
        private Panel panel2;
        private Panel panel3;
        private Panel panel4;
        private Panel panel5;
        private TextBox txtToEmailName;
        private TextBox txtToFilter;
        private TextBox txtToEmailAddress;
        private TextBox txtPathToWatch;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private ComboBox comboBoxTimerInterval;
        private Button btnBrowse;
        private FolderBrowserDialog folderBrowserDialog1;
        private Label lblEmailValidation;
    }
}
