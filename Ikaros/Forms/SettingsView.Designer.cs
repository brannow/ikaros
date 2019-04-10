using System.Drawing;

namespace Ikaros.Forms
{
    partial class SettingsView
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.trackBar2 = new System.Windows.Forms.TrackBar();
            this.label6 = new System.Windows.Forms.Label();
            this.labelMapOpValue = new System.Windows.Forms.Label();
            this.labelDescOpValue = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.ZoneComboBox = new System.Windows.Forms.ComboBox();
            this.SectionComboBox = new System.Windows.Forms.ComboBox();
            this.StepComboBox = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.nextTextBox = new System.Windows.Forms.TextBox();
            this.prevTextBox = new System.Windows.Forms.TextBox();
            this.ShowTextBox = new System.Windows.Forms.TextBox();
            this.lockTextBox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(14, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(145, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "Map Opacity";
            // 
            // trackBar1
            // 
            this.trackBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBar1.Location = new System.Drawing.Point(40, 70);
            this.trackBar1.Maximum = 100;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(396, 69);
            this.trackBar1.TabIndex = 1;
            this.trackBar1.TickFrequency = 20;
            this.trackBar1.Value = 100;
            this.trackBar1.Scroll += new System.EventHandler(this.TrackBar1_Scroll);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(440, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "100%";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 20);
            this.label3.TabIndex = 3;
            this.label3.Text = "0%";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 201);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 20);
            this.label4.TabIndex = 7;
            this.label4.Text = "0%";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(440, 201);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 20);
            this.label5.TabIndex = 6;
            this.label5.Text = "100%";
            // 
            // trackBar2
            // 
            this.trackBar2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBar2.Location = new System.Drawing.Point(40, 195);
            this.trackBar2.Maximum = 100;
            this.trackBar2.Name = "trackBar2";
            this.trackBar2.Size = new System.Drawing.Size(396, 69);
            this.trackBar2.TabIndex = 5;
            this.trackBar2.TickFrequency = 20;
            this.trackBar2.Value = 100;
            this.trackBar2.Scroll += new System.EventHandler(this.TrackBar2_Scroll);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(14, 145);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(220, 26);
            this.label6.TabIndex = 4;
            this.label6.Text = "Description Opacity";
            // 
            // labelMapOpValue
            // 
            this.labelMapOpValue.AutoSize = true;
            this.labelMapOpValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMapOpValue.Location = new System.Drawing.Point(410, 20);
            this.labelMapOpValue.Name = "labelMapOpValue";
            this.labelMapOpValue.Size = new System.Drawing.Size(72, 26);
            this.labelMapOpValue.TabIndex = 8;
            this.labelMapOpValue.Text = "100%";
            // 
            // labelDescOpValue
            // 
            this.labelDescOpValue.AutoSize = true;
            this.labelDescOpValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDescOpValue.Location = new System.Drawing.Point(410, 145);
            this.labelDescOpValue.Name = "labelDescOpValue";
            this.labelDescOpValue.Size = new System.Drawing.Size(72, 26);
            this.labelDescOpValue.TabIndex = 8;
            this.labelDescOpValue.Text = "100%";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(12, 778);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(476, 41);
            this.button1.TabIndex = 9;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.OnCloseFrom);
            // 
            // ZoneComboBox
            // 
            this.ZoneComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ZoneComboBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.ZoneComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ZoneComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ZoneComboBox.ForeColor = System.Drawing.Color.White;
            this.ZoneComboBox.FormattingEnabled = true;
            this.ZoneComboBox.Location = new System.Drawing.Point(12, 301);
            this.ZoneComboBox.MaxDropDownItems = 100;
            this.ZoneComboBox.Name = "ZoneComboBox";
            this.ZoneComboBox.Size = new System.Drawing.Size(476, 28);
            this.ZoneComboBox.TabIndex = 10;
            this.ZoneComboBox.SelectedIndexChanged += new System.EventHandler(this.ZoneComboBox_SelectedIndexChanged);
            // 
            // SectionComboBox
            // 
            this.SectionComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SectionComboBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.SectionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SectionComboBox.Enabled = false;
            this.SectionComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SectionComboBox.ForeColor = System.Drawing.Color.White;
            this.SectionComboBox.FormattingEnabled = true;
            this.SectionComboBox.Location = new System.Drawing.Point(12, 384);
            this.SectionComboBox.MaxDropDownItems = 100;
            this.SectionComboBox.Name = "SectionComboBox";
            this.SectionComboBox.Size = new System.Drawing.Size(476, 28);
            this.SectionComboBox.TabIndex = 10;
            this.SectionComboBox.SelectedIndexChanged += new System.EventHandler(this.SectionComboBox_SelectedIndexChanged);
            // 
            // StepComboBox
            // 
            this.StepComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.StepComboBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.StepComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.StepComboBox.Enabled = false;
            this.StepComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.StepComboBox.ForeColor = System.Drawing.Color.White;
            this.StepComboBox.FormattingEnabled = true;
            this.StepComboBox.Location = new System.Drawing.Point(12, 470);
            this.StepComboBox.MaxDropDownItems = 100;
            this.StepComboBox.Name = "StepComboBox";
            this.StepComboBox.Size = new System.Drawing.Size(476, 28);
            this.StepComboBox.TabIndex = 10;
            this.StepComboBox.SelectedIndexChanged += new System.EventHandler(this.StepComboBox_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(14, 261);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 26);
            this.label7.TabIndex = 0;
            this.label7.Text = "Zone";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(14, 346);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(92, 26);
            this.label8.TabIndex = 0;
            this.label8.Text = "Section";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(14, 430);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(61, 26);
            this.label9.TabIndex = 0;
            this.label9.Text = "Step";
            // 
            // nextTextBox
            // 
            this.nextTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nextTextBox.BackColor = System.Drawing.Color.DimGray;
            this.nextTextBox.Location = new System.Drawing.Point(340, 566);
            this.nextTextBox.Name = "nextTextBox";
            this.nextTextBox.Size = new System.Drawing.Size(142, 26);
            this.nextTextBox.TabIndex = 11;
            this.nextTextBox.Tag = "1";
            this.nextTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nextTextBox.WordWrap = false;
            this.nextTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Capture_KeyDown);
            // 
            // prevTextBox
            // 
            this.prevTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.prevTextBox.BackColor = System.Drawing.Color.DimGray;
            this.prevTextBox.Location = new System.Drawing.Point(340, 613);
            this.prevTextBox.Name = "prevTextBox";
            this.prevTextBox.Size = new System.Drawing.Size(142, 26);
            this.prevTextBox.TabIndex = 11;
            this.prevTextBox.Tag = "2";
            this.prevTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.prevTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Capture_KeyDown);
            // 
            // ShowTextBox
            // 
            this.ShowTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ShowTextBox.BackColor = System.Drawing.Color.DimGray;
            this.ShowTextBox.Location = new System.Drawing.Point(340, 660);
            this.ShowTextBox.Name = "ShowTextBox";
            this.ShowTextBox.Size = new System.Drawing.Size(142, 26);
            this.ShowTextBox.TabIndex = 11;
            this.ShowTextBox.Tag = "3";
            this.ShowTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ShowTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Capture_KeyDown);
            // 
            // lockTextBox
            // 
            this.lockTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lockTextBox.BackColor = System.Drawing.Color.DimGray;
            this.lockTextBox.Location = new System.Drawing.Point(340, 707);
            this.lockTextBox.Name = "lockTextBox";
            this.lockTextBox.Size = new System.Drawing.Size(142, 26);
            this.lockTextBox.TabIndex = 11;
            this.lockTextBox.Tag = "4";
            this.lockTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.lockTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Capture_KeyDown);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(14, 523);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(98, 26);
            this.label10.TabIndex = 0;
            this.label10.Text = "Hotkeys";
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(221, 567);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(98, 22);
            this.label11.TabIndex = 0;
            this.label11.Text = "Next Step";
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(184, 614);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(135, 22);
            this.label12.TabIndex = 0;
            this.label12.Text = "Previous Step";
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(172, 661);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(147, 22);
            this.label13.TabIndex = 0;
            this.label13.Text = "Toggle Overlay";
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(199, 708);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(120, 22);
            this.label14.TabIndex = 0;
            this.label14.Text = "Toggle Lock";
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(321, 530);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(161, 17);
            this.label15.TabIndex = 12;
            this.label15.Text = "Reset Hotkey with ESC *";
            // 
            // SettingsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.ClientSize = new System.Drawing.Size(500, 831);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.lockTextBox);
            this.Controls.Add(this.ShowTextBox);
            this.Controls.Add(this.prevTextBox);
            this.Controls.Add(this.nextTextBox);
            this.Controls.Add(this.StepComboBox);
            this.Controls.Add(this.SectionComboBox);
            this.Controls.Add(this.ZoneComboBox);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.labelDescOpValue);
            this.Controls.Add(this.labelMapOpValue);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.trackBar2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label1);
            this.ForeColor = System.Drawing.SystemColors.Control;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsView";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SettingsView";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SettingsView_FormClosing);
            this.Load += new System.EventHandler(this.SettingsView_Load);
            this.VisibleChanged += new System.EventHandler(this.SettingsView_VisibleChanged);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TrackBar trackBar2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label labelMapOpValue;
        private System.Windows.Forms.Label labelDescOpValue;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox ZoneComboBox;
        private System.Windows.Forms.ComboBox SectionComboBox;
        private System.Windows.Forms.ComboBox StepComboBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox nextTextBox;
        private System.Windows.Forms.TextBox prevTextBox;
        private System.Windows.Forms.TextBox ShowTextBox;
        private System.Windows.Forms.TextBox lockTextBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
    }
}

