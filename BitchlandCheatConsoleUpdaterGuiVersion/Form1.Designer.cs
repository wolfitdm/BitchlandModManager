namespace BitchlandCheatConsoleUpdaterGuiVersion
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
            button1 = new Button();
            UpdateLabel = new Label();
            downloadProgressBar = new ProgressBar();
            comboBox1 = new ComboBox();
            label1 = new Label();
            comboBox2 = new ComboBox();
            label2 = new Label();
            button2 = new Button();
            label3 = new Label();
            label4 = new Label();
            getmoremods = new Button();
            label5 = new Label();
            comboBox3 = new ComboBox();
            label6 = new Label();
            comboBox4 = new ComboBox();
            button3 = new Button();
            button4 = new Button();
            button5 = new Button();
            button6 = new Button();
            helpBepInExMods = new Button();
            helpIngameMods = new Button();
            Uninstall = new Button();
            Uninstall2 = new Button();
            deleteAllModsIncludingBepInEx = new Button();
            deleteAllIngameMods = new Button();
            downloadProgressPercent = new Label();
            removeBepInExModsBackup = new Button();
            removeIngameModBackup = new Button();
            removeAllBepInExModsBackups = new Button();
            removeAllIngameModsBackups = new Button();
            uninstallBepInExBackup = new Button();
            uninstallIngameModBackup = new Button();
            downloadSpeed = new Label();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(513, 112);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 0;
            button1.Text = "Install";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // UpdateLabel
            // 
            UpdateLabel.AutoSize = true;
            UpdateLabel.Location = new Point(12, 40);
            UpdateLabel.Name = "UpdateLabel";
            UpdateLabel.Size = new Size(0, 15);
            UpdateLabel.TabIndex = 1;
            // 
            // downloadProgressBar
            // 
            downloadProgressBar.Location = new Point(318, 40);
            downloadProgressBar.Name = "downloadProgressBar";
            downloadProgressBar.Size = new Size(270, 23);
            downloadProgressBar.TabIndex = 2;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(12, 113);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(485, 23);
            comboBox1.TabIndex = 4;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 77);
            label1.Name = "label1";
            label1.Size = new Size(79, 15);
            label1.TabIndex = 5;
            label1.Text = "BepInExMods";
            // 
            // comboBox2
            // 
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new Point(14, 205);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(483, 23);
            comboBox2.TabIndex = 6;
            comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(14, 162);
            label2.Name = "label2";
            label2.Size = new Size(77, 15);
            label2.TabIndex = 7;
            label2.Text = "IngameMods";
            // 
            // button2
            // 
            button2.Location = new Point(513, 205);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 8;
            button2.Text = "Install";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(16, 12);
            label3.Name = "label3";
            label3.Size = new Size(96, 15);
            label3.TabIndex = 9;
            label3.Text = "Download Status";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(318, 12);
            label4.Name = "label4";
            label4.Size = new Size(125, 15);
            label4.TabIndex = 10;
            label4.Text = "Download Progess Bar";
            // 
            // getmoremods
            // 
            getmoremods.Location = new Point(12, 261);
            getmoremods.Name = "getmoremods";
            getmoremods.Size = new Size(745, 23);
            getmoremods.TabIndex = 11;
            getmoremods.Text = "Get More Mods";
            getmoremods.UseVisualStyleBackColor = true;
            getmoremods.Click += getmoremods_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(14, 305);
            label5.Name = "label5";
            label5.Size = new Size(126, 15);
            label5.TabIndex = 12;
            label5.Text = "BepInExMods Backups";
            // 
            // comboBox3
            // 
            comboBox3.FormattingEnabled = true;
            comboBox3.Location = new Point(14, 338);
            comboBox3.Name = "comboBox3";
            comboBox3.Size = new Size(479, 23);
            comboBox3.TabIndex = 13;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(12, 376);
            label6.Name = "label6";
            label6.Size = new Size(124, 15);
            label6.TabIndex = 14;
            label6.Text = "IngameMods Backups";
            // 
            // comboBox4
            // 
            comboBox4.FormattingEnabled = true;
            comboBox4.Location = new Point(17, 405);
            comboBox4.Name = "comboBox4";
            comboBox4.Size = new Size(476, 23);
            comboBox4.TabIndex = 15;
            // 
            // button3
            // 
            button3.Location = new Point(513, 337);
            button3.Name = "button3";
            button3.Size = new Size(75, 23);
            button3.TabIndex = 16;
            button3.Text = "Install";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.Location = new Point(513, 404);
            button4.Name = "button4";
            button4.Size = new Size(75, 23);
            button4.TabIndex = 17;
            button4.Text = "Install";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // button5
            // 
            button5.Location = new Point(117, 73);
            button5.Name = "button5";
            button5.Size = new Size(177, 23);
            button5.TabIndex = 18;
            button5.Text = "Create Mods Backup";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // button6
            // 
            button6.Location = new Point(117, 158);
            button6.Name = "button6";
            button6.Size = new Size(174, 23);
            button6.TabIndex = 19;
            button6.Text = "Create Mods Backup";
            button6.UseVisualStyleBackColor = true;
            button6.Click += button6_Click;
            // 
            // helpBepInExMods
            // 
            helpBepInExMods.Location = new Point(682, 112);
            helpBepInExMods.Name = "helpBepInExMods";
            helpBepInExMods.Size = new Size(75, 23);
            helpBepInExMods.TabIndex = 20;
            helpBepInExMods.Text = "Help";
            helpBepInExMods.UseVisualStyleBackColor = true;
            helpBepInExMods.Click += helpBepInExMods_Click;
            // 
            // helpIngameMods
            // 
            helpIngameMods.Location = new Point(682, 205);
            helpIngameMods.Name = "helpIngameMods";
            helpIngameMods.Size = new Size(75, 23);
            helpIngameMods.TabIndex = 21;
            helpIngameMods.Text = "Help";
            helpIngameMods.UseVisualStyleBackColor = true;
            helpIngameMods.Click += helpIngameMods_Click;
            // 
            // Uninstall
            // 
            Uninstall.Location = new Point(601, 112);
            Uninstall.Name = "Uninstall";
            Uninstall.Size = new Size(75, 23);
            Uninstall.TabIndex = 22;
            Uninstall.Text = "Uninstall";
            Uninstall.UseVisualStyleBackColor = true;
            Uninstall.Click += Uninstall_Click;
            // 
            // Uninstall2
            // 
            Uninstall2.Location = new Point(601, 205);
            Uninstall2.Name = "Uninstall2";
            Uninstall2.Size = new Size(75, 23);
            Uninstall2.TabIndex = 23;
            Uninstall2.Text = "Uninstall";
            Uninstall2.UseVisualStyleBackColor = true;
            Uninstall2.Click += Uninstall2_Click;
            // 
            // deleteAllModsIncludingBepInEx
            // 
            deleteAllModsIncludingBepInEx.Location = new Point(310, 73);
            deleteAllModsIncludingBepInEx.Name = "deleteAllModsIncludingBepInEx";
            deleteAllModsIncludingBepInEx.Size = new Size(447, 23);
            deleteAllModsIncludingBepInEx.TabIndex = 24;
            deleteAllModsIncludingBepInEx.Text = "Delete all mods including bepinex";
            deleteAllModsIncludingBepInEx.UseVisualStyleBackColor = true;
            deleteAllModsIncludingBepInEx.Click += deleteAllModsIncludingBepInEx_Click;
            // 
            // deleteAllIngameMods
            // 
            deleteAllIngameMods.Location = new Point(310, 158);
            deleteAllIngameMods.Name = "deleteAllIngameMods";
            deleteAllIngameMods.Size = new Size(447, 23);
            deleteAllIngameMods.TabIndex = 25;
            deleteAllIngameMods.Text = "Delete all ingame mods";
            deleteAllIngameMods.UseVisualStyleBackColor = true;
            deleteAllIngameMods.Click += deleteAllIngameMods_Click;
            // 
            // downloadProgressPercent
            // 
            downloadProgressPercent.AutoSize = true;
            downloadProgressPercent.Location = new Point(594, 40);
            downloadProgressPercent.Name = "downloadProgressPercent";
            downloadProgressPercent.Size = new Size(26, 15);
            downloadProgressPercent.TabIndex = 26;
            downloadProgressPercent.Text = "0 %";
            // 
            // removeBepInExModsBackup
            // 
            removeBepInExModsBackup.Location = new Point(682, 337);
            removeBepInExModsBackup.Name = "removeBepInExModsBackup";
            removeBepInExModsBackup.Size = new Size(75, 23);
            removeBepInExModsBackup.TabIndex = 27;
            removeBepInExModsBackup.Text = "Delete";
            removeBepInExModsBackup.UseVisualStyleBackColor = true;
            removeBepInExModsBackup.Click += removeBepInExModsBackup_Click;
            // 
            // removeIngameModBackup
            // 
            removeIngameModBackup.Location = new Point(682, 404);
            removeIngameModBackup.Name = "removeIngameModBackup";
            removeIngameModBackup.Size = new Size(75, 23);
            removeIngameModBackup.TabIndex = 28;
            removeIngameModBackup.Text = "Delete";
            removeIngameModBackup.UseVisualStyleBackColor = true;
            removeIngameModBackup.Click += removeIngameModBackup_Click;
            // 
            // removeAllBepInExModsBackups
            // 
            removeAllBepInExModsBackups.Location = new Point(156, 301);
            removeAllBepInExModsBackups.Name = "removeAllBepInExModsBackups";
            removeAllBepInExModsBackups.Size = new Size(135, 23);
            removeAllBepInExModsBackups.TabIndex = 29;
            removeAllBepInExModsBackups.Text = "Delete all backups";
            removeAllBepInExModsBackups.UseVisualStyleBackColor = true;
            removeAllBepInExModsBackups.Click += removeAllBepInExModsBackups_Click;
            // 
            // removeAllIngameModsBackups
            // 
            removeAllIngameModsBackups.Location = new Point(159, 374);
            removeAllIngameModsBackups.Name = "removeAllIngameModsBackups";
            removeAllIngameModsBackups.Size = new Size(132, 23);
            removeAllIngameModsBackups.TabIndex = 30;
            removeAllIngameModsBackups.Text = "Delete all backups";
            removeAllIngameModsBackups.UseVisualStyleBackColor = true;
            removeAllIngameModsBackups.Click += removeAllIngameModsBackups_Click;
            // 
            // uninstallBepInExBackup
            // 
            uninstallBepInExBackup.Location = new Point(601, 337);
            uninstallBepInExBackup.Name = "uninstallBepInExBackup";
            uninstallBepInExBackup.Size = new Size(75, 23);
            uninstallBepInExBackup.TabIndex = 31;
            uninstallBepInExBackup.Text = "Uninstall";
            uninstallBepInExBackup.UseVisualStyleBackColor = true;
            uninstallBepInExBackup.Click += uninstallBepInExBackup_Click;
            // 
            // uninstallIngameModBackup
            // 
            uninstallIngameModBackup.Location = new Point(601, 405);
            uninstallIngameModBackup.Name = "uninstallIngameModBackup";
            uninstallIngameModBackup.Size = new Size(75, 23);
            uninstallIngameModBackup.TabIndex = 32;
            uninstallIngameModBackup.Text = "Uninstall";
            uninstallIngameModBackup.UseVisualStyleBackColor = true;
            uninstallIngameModBackup.Click += uninstallIngameModBackup_Click;
            // 
            // downloadSpeed
            // 
            downloadSpeed.AutoSize = true;
            downloadSpeed.Location = new Point(450, 12);
            downloadSpeed.Name = "downloadSpeed";
            downloadSpeed.Size = new Size(138, 15);
            downloadSpeed.TabIndex = 33;
            downloadSpeed.Text = "Download speed: 0 MB/s";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(772, 455);
            Controls.Add(downloadSpeed);
            Controls.Add(uninstallIngameModBackup);
            Controls.Add(uninstallBepInExBackup);
            Controls.Add(removeAllIngameModsBackups);
            Controls.Add(removeAllBepInExModsBackups);
            Controls.Add(removeIngameModBackup);
            Controls.Add(removeBepInExModsBackup);
            Controls.Add(downloadProgressPercent);
            Controls.Add(deleteAllIngameMods);
            Controls.Add(deleteAllModsIncludingBepInEx);
            Controls.Add(Uninstall2);
            Controls.Add(Uninstall);
            Controls.Add(helpIngameMods);
            Controls.Add(helpBepInExMods);
            Controls.Add(button6);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(comboBox4);
            Controls.Add(label6);
            Controls.Add(comboBox3);
            Controls.Add(label5);
            Controls.Add(getmoremods);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(button2);
            Controls.Add(label2);
            Controls.Add(comboBox2);
            Controls.Add(label1);
            Controls.Add(comboBox1);
            Controls.Add(downloadProgressBar);
            Controls.Add(UpdateLabel);
            Controls.Add(button1);
            Name = "Form1";
            Text = "BItchland Mod Manager";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private Label UpdateLabel;
        private ProgressBar downloadProgressBar;
        private ComboBox comboBox1;
        private Label label1;
        private ComboBox comboBox2;
        private Label label2;
        private Button button2;
        private Label label3;
        private Label label4;
        private Button getmoremods;
        private Label label5;
        private ComboBox comboBox3;
        private Label label6;
        private ComboBox comboBox4;
        private Button button3;
        private Button button4;
        private Button button5;
        private Button button6;
        private Button helpBepInExMods;
        private Button helpIngameMods;
        private Button Uninstall;
        private Button Uninstall2;
        private Button deleteAllModsIncludingBepInEx;
        private Button deleteAllIngameMods;
        private Label downloadProgressPercent;
        private Button removeBepInExModsBackup;
        private Button removeIngameModBackup;
        private Button removeAllBepInExModsBackups;
        private Button removeAllIngameModsBackups;
        private Button uninstallBepInExBackup;
        private Button uninstallIngameModBackup;
        private Label downloadSpeed;
    }
}
