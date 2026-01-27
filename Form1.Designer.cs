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
            getmoremods.Size = new Size(576, 23);
            getmoremods.TabIndex = 11;
            getmoremods.Text = "Get More Mods";
            getmoremods.UseVisualStyleBackColor = true;
            getmoremods.Click += getmoremods_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(646, 365);
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
    }
}
