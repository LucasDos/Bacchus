namespace Bacchus
{
    partial class FormContextMenu
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
            this.label1 = new System.Windows.Forms.Label();
            this.add_btn = new System.Windows.Forms.Button();
            this.modif_btn = new System.Windows.Forms.Button();
            this.suppr_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(254)));
            this.label1.Location = new System.Drawing.Point(110, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(152, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Menu contextuel ";
            // 
            // add_btn
            // 
            this.add_btn.Location = new System.Drawing.Point(114, 99);
            this.add_btn.Name = "add_btn";
            this.add_btn.Size = new System.Drawing.Size(148, 30);
            this.add_btn.TabIndex = 1;
            this.add_btn.Text = "Ajouter un artcile";
            this.add_btn.UseVisualStyleBackColor = true;
            this.add_btn.Click += new System.EventHandler(this.add_btn_Click);
            // 
            // modif_btn
            // 
            this.modif_btn.Location = new System.Drawing.Point(114, 184);
            this.modif_btn.Name = "modif_btn";
            this.modif_btn.Size = new System.Drawing.Size(148, 30);
            this.modif_btn.TabIndex = 2;
            this.modif_btn.Text = "Modifier l\'article";
            this.modif_btn.UseVisualStyleBackColor = true;
            this.modif_btn.Click += new System.EventHandler(this.modif_btn_Click);
            // 
            // suppr_btn
            // 
            this.suppr_btn.Location = new System.Drawing.Point(114, 276);
            this.suppr_btn.Name = "suppr_btn";
            this.suppr_btn.Size = new System.Drawing.Size(148, 30);
            this.suppr_btn.TabIndex = 3;
            this.suppr_btn.Text = "Supprimer l\'article";
            this.suppr_btn.UseVisualStyleBackColor = true;
            this.suppr_btn.Click += new System.EventHandler(this.suppr_btn_Click);
            // 
            // FormContextMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 353);
            this.Controls.Add(this.suppr_btn);
            this.Controls.Add(this.modif_btn);
            this.Controls.Add(this.add_btn);
            this.Controls.Add(this.label1);
            this.Name = "FormContextMenu";
            this.Text = "FormContextMenu";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button add_btn;
        private System.Windows.Forms.Button modif_btn;
        private System.Windows.Forms.Button suppr_btn;
    }
}