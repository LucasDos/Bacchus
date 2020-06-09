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
            this.sfAjout_btn = new System.Windows.Forms.Button();
            this.marqueAjout_btn = new System.Windows.Forms.Button();
            this.famAjout_btn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.suppr_btn = new System.Windows.Forms.Button();
            this.modif_btn = new System.Windows.Forms.Button();
            this.add_btn = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
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
            // sfAjout_btn
            // 
            this.sfAjout_btn.Location = new System.Drawing.Point(83, 90);
            this.sfAjout_btn.Name = "sfAjout_btn";
            this.sfAjout_btn.Size = new System.Drawing.Size(190, 30);
            this.sfAjout_btn.TabIndex = 4;
            this.sfAjout_btn.Text = "Ajouter une Sous Famille";
            this.sfAjout_btn.UseVisualStyleBackColor = true;
            this.sfAjout_btn.Click += new System.EventHandler(this.sfAjout_btn_Click);
            // 
            // marqueAjout_btn
            // 
            this.marqueAjout_btn.Location = new System.Drawing.Point(83, 144);
            this.marqueAjout_btn.Name = "marqueAjout_btn";
            this.marqueAjout_btn.Size = new System.Drawing.Size(190, 30);
            this.marqueAjout_btn.TabIndex = 5;
            this.marqueAjout_btn.Text = "Ajouter une Marque";
            this.marqueAjout_btn.UseVisualStyleBackColor = true;
            this.marqueAjout_btn.Click += new System.EventHandler(this.marqueAjout_btn_Click);
            // 
            // famAjout_btn
            // 
            this.famAjout_btn.Location = new System.Drawing.Point(83, 39);
            this.famAjout_btn.Name = "famAjout_btn";
            this.famAjout_btn.Size = new System.Drawing.Size(190, 30);
            this.famAjout_btn.TabIndex = 6;
            this.famAjout_btn.Text = "Ajouter une Famille";
            this.famAjout_btn.UseVisualStyleBackColor = true;
            this.famAjout_btn.Click += new System.EventHandler(this.famAjout_btn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.suppr_btn);
            this.groupBox1.Controls.Add(this.modif_btn);
            this.groupBox1.Controls.Add(this.add_btn);
            this.groupBox1.Location = new System.Drawing.Point(12, 42);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(358, 88);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Articles";
            // 
            // suppr_btn
            // 
            this.suppr_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(254)));
            this.suppr_btn.Location = new System.Drawing.Point(252, 30);
            this.suppr_btn.Name = "suppr_btn";
            this.suppr_btn.Size = new System.Drawing.Size(100, 30);
            this.suppr_btn.TabIndex = 2;
            this.suppr_btn.Text = "Supprimer";
            this.suppr_btn.UseVisualStyleBackColor = true;
            this.suppr_btn.Click += new System.EventHandler(this.suppr_btn_Click_1);
            // 
            // modif_btn
            // 
            this.modif_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(254)));
            this.modif_btn.Location = new System.Drawing.Point(131, 30);
            this.modif_btn.Name = "modif_btn";
            this.modif_btn.Size = new System.Drawing.Size(100, 30);
            this.modif_btn.TabIndex = 1;
            this.modif_btn.Text = "Modifier";
            this.modif_btn.UseVisualStyleBackColor = true;
            this.modif_btn.Click += new System.EventHandler(this.modif_btn_Click_1);
            // 
            // add_btn
            // 
            this.add_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(254)));
            this.add_btn.Location = new System.Drawing.Point(6, 30);
            this.add_btn.Name = "add_btn";
            this.add_btn.Size = new System.Drawing.Size(100, 30);
            this.add_btn.TabIndex = 0;
            this.add_btn.Text = "Ajouter";
            this.add_btn.UseVisualStyleBackColor = true;
            this.add_btn.Click += new System.EventHandler(this.add_btn_Click_1);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.famAjout_btn);
            this.groupBox2.Controls.Add(this.sfAjout_btn);
            this.groupBox2.Controls.Add(this.marqueAjout_btn);
            this.groupBox2.Location = new System.Drawing.Point(12, 147);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(358, 194);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Autres";
            // 
            // FormContextMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 353);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Name = "FormContextMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormContextMenu";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button sfAjout_btn;
        private System.Windows.Forms.Button marqueAjout_btn;
        private System.Windows.Forms.Button famAjout_btn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button add_btn;
        private System.Windows.Forms.Button suppr_btn;
        private System.Windows.Forms.Button modif_btn;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}