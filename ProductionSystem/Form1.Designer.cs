
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
            this.label3 = new System.Windows.Forms.Label();
            this.d_facts_box = new System.Windows.Forms.CheckedListBox();
            this.SuspendLayout();
            // 
            // facts_box
            // 
            this.facts_box.CheckOnClick = true;
            this.facts_box.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.facts_box.FormattingEnabled = true;
            this.facts_box.Location = new System.Drawing.Point(18, 41);
            this.facts_box.Name = "facts_box";
            this.facts_box.Size = new System.Drawing.Size(362, 429);
            this.facts_box.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Open Sans", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(13, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "Факты";
            // 
            // text_box
            // 
            this.text_box.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.text_box.Location = new System.Drawing.Point(764, 41);
            this.text_box.Name = "text_box";
            this.text_box.Size = new System.Drawing.Size(755, 455);
            this.text_box.TabIndex = 2;
            this.text_box.Text = "";
            // 
            // forward_btn
            // 
            this.forward_btn.BackColor = System.Drawing.SystemColors.Control;
            this.forward_btn.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.forward_btn.Location = new System.Drawing.Point(18, 495);
            this.forward_btn.Name = "forward_btn";
            this.forward_btn.Size = new System.Drawing.Size(168, 83);
            this.forward_btn.TabIndex = 3;
            this.forward_btn.Text = "Прямой вывод";
            this.forward_btn.UseVisualStyleBackColor = false;
            this.forward_btn.Click += new System.EventHandler(this.forward_btn_Click);
            // 
            // backword_btn
            // 
            this.backword_btn.BackColor = System.Drawing.SystemColors.Control;
            this.backword_btn.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.backword_btn.Location = new System.Drawing.Point(386, 495);
            this.backword_btn.Name = "backword_btn";
            this.backword_btn.Size = new System.Drawing.Size(168, 83);
            this.backword_btn.TabIndex = 4;
            this.backword_btn.Text = "Обратный вывод";
            this.backword_btn.UseVisualStyleBackColor = false;
            this.backword_btn.Visible = false;
            this.backword_btn.Click += new System.EventHandler(this.backword_btn_Click);
            // 
            // clear_btn
            // 
            this.clear_btn.BackColor = System.Drawing.SystemColors.Control;
            this.clear_btn.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.clear_btn.Location = new System.Drawing.Point(764, 495);
            this.clear_btn.Name = "clear_btn";
            this.clear_btn.Size = new System.Drawing.Size(168, 83);
            this.clear_btn.TabIndex = 5;
            this.clear_btn.Text = "Очистить";
            this.clear_btn.UseVisualStyleBackColor = false;
            this.clear_btn.Click += new System.EventHandler(this.clear_btn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Open Sans", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(759, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 23);
            this.label2.TabIndex = 6;
            this.label2.Text = "Логи работы";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Open Sans", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(381, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(185, 23);
            this.label3.TabIndex = 8;
            this.label3.Text = "Выведенные факты";
            this.label3.Visible = false;
            // 
            // d_facts_box
            // 
            this.d_facts_box.CheckOnClick = true;
            this.d_facts_box.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.d_facts_box.FormattingEnabled = true;
            this.d_facts_box.Location = new System.Drawing.Point(386, 41);
            this.d_facts_box.Name = "d_facts_box";
            this.d_facts_box.Size = new System.Drawing.Size(362, 429);
            this.d_facts_box.TabIndex = 7;
            this.d_facts_box.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1642, 680);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.d_facts_box);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.clear_btn);
            this.Controls.Add(this.backword_btn);
            this.Controls.Add(this.forward_btn);
            this.Controls.Add(this.text_box);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.facts_box);
            this.Name = "Form1";
            this.Text = "Minecraft production system";
            this.Load += new System.EventHandler(this.Form1_Load);
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
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckedListBox d_facts_box;
    }
}

