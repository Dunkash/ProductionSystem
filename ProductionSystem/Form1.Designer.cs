﻿
namespace ProductionSystem
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.facts_box = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.text_box = new System.Windows.Forms.RichTextBox();
            this.forward_btn = new System.Windows.Forms.Button();
            this.backword_btn = new System.Windows.Forms.Button();
            this.clear_btn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // facts_box
            // 
            this.facts_box.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.facts_box.FormattingEnabled = true;
            this.facts_box.Location = new System.Drawing.Point(18, 41);
            this.facts_box.Name = "facts_box";
            this.facts_box.Size = new System.Drawing.Size(362, 429);
            this.facts_box.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Факты";
            // 
            // text_box
            // 
            this.text_box.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.text_box.Location = new System.Drawing.Point(493, 41);
            this.text_box.Name = "text_box";
            this.text_box.Size = new System.Drawing.Size(851, 455);
            this.text_box.TabIndex = 2;
            this.text_box.Text = "";
            // 
            // forward_btn
            // 
            this.forward_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.forward_btn.Location = new System.Drawing.Point(18, 495);
            this.forward_btn.Name = "forward_btn";
            this.forward_btn.Size = new System.Drawing.Size(168, 83);
            this.forward_btn.TabIndex = 3;
            this.forward_btn.Text = "Прямой вывод";
            this.forward_btn.UseVisualStyleBackColor = true;
            this.forward_btn.Click += new System.EventHandler(this.forward_btn_Click);
            // 
            // backword_btn
            // 
            this.backword_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.backword_btn.Location = new System.Drawing.Point(212, 495);
            this.backword_btn.Name = "backword_btn";
            this.backword_btn.Size = new System.Drawing.Size(168, 83);
            this.backword_btn.TabIndex = 4;
            this.backword_btn.Text = "Обратный вывод";
            this.backword_btn.UseVisualStyleBackColor = true;
            this.backword_btn.Click += new System.EventHandler(this.backword_btn_Click);
            // 
            // clear_btn
            // 
            this.clear_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.clear_btn.Location = new System.Drawing.Point(493, 495);
            this.clear_btn.Name = "clear_btn";
            this.clear_btn.Size = new System.Drawing.Size(168, 83);
            this.clear_btn.TabIndex = 5;
            this.clear_btn.Text = "Очистить";
            this.clear_btn.UseVisualStyleBackColor = true;
            this.clear_btn.Click += new System.EventHandler(this.clear_btn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(488, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(131, 25);
            this.label2.TabIndex = 6;
            this.label2.Text = "Логи работы";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1363, 647);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.clear_btn);
            this.Controls.Add(this.backword_btn);
            this.Controls.Add(this.forward_btn);
            this.Controls.Add(this.text_box);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.facts_box);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox facts_box;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox text_box;
        private System.Windows.Forms.Button forward_btn;
        private System.Windows.Forms.Button backword_btn;
        private System.Windows.Forms.Button clear_btn;
        private System.Windows.Forms.Label label2;
    }
}

