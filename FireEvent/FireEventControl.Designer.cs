namespace FireEvent
{
    partial class FireEventControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Ok_button = new System.Windows.Forms.Button();
            this.txtEditEntity = new System.Windows.Forms.TextBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.close_button = new System.Windows.Forms.Button();
            this.txtEnterdEntity = new System.Windows.Forms.TextBox();
            this.listViewEntities = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // Ok_button
            // 
            this.Ok_button.Location = new System.Drawing.Point(161, 395);
            this.Ok_button.Name = "Ok_button";
            this.Ok_button.Size = new System.Drawing.Size(75, 23);
            this.Ok_button.TabIndex = 0;
            this.Ok_button.Text = "OK";
            this.Ok_button.UseVisualStyleBackColor = true;
            this.Ok_button.Click += new System.EventHandler(this.Ok_button_Click);
            // 
            // txtEditEntity
            // 
            this.txtEditEntity.Location = new System.Drawing.Point(80, 41);
            this.txtEditEntity.Name = "txtEditEntity";
            this.txtEditEntity.Size = new System.Drawing.Size(167, 20);
            this.txtEditEntity.TabIndex = 1;
            this.txtEditEntity.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.texEditEntity_KeyPress);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblTitle.Location = new System.Drawing.Point(322, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(51, 20);
            this.lblTitle.TabIndex = 2;
            this.lblTitle.Text = "label1";
            // 
            // close_button
            // 
            this.close_button.Location = new System.Drawing.Point(404, 395);
            this.close_button.Name = "close_button";
            this.close_button.Size = new System.Drawing.Size(75, 23);
            this.close_button.TabIndex = 3;
            this.close_button.Text = "Close";
            this.close_button.UseVisualStyleBackColor = true;
            this.close_button.Click += new System.EventHandler(this.Close_button_Click);
            // 
            // txtEnterdEntity
            // 
            this.txtEnterdEntity.Enabled = false;
            this.txtEnterdEntity.Location = new System.Drawing.Point(366, 41);
            this.txtEnterdEntity.Name = "txtEnterdEntity";
            this.txtEnterdEntity.Size = new System.Drawing.Size(251, 20);
            this.txtEnterdEntity.TabIndex = 5;
            // 
            // listViewEntities
            // 
            this.listViewEntities.FullRowSelect = true;
            this.listViewEntities.Location = new System.Drawing.Point(80, 89);
            this.listViewEntities.Name = "listViewEntities";
            this.listViewEntities.Size = new System.Drawing.Size(731, 283);
            this.listViewEntities.TabIndex = 6;
            this.listViewEntities.UseCompatibleStateImageBehavior = false;
            this.listViewEntities.View = System.Windows.Forms.View.Details;
            this.listViewEntities.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listViewEntities_KeyDown);
            // 
            // FireEventControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listViewEntities);
            this.Controls.Add(this.txtEnterdEntity);
            this.Controls.Add(this.close_button);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.txtEditEntity);
            this.Controls.Add(this.Ok_button);
            this.Name = "FireEventControl";
            this.Size = new System.Drawing.Size(997, 452);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Ok_button;
        private System.Windows.Forms.TextBox txtEditEntity;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button close_button;
        private System.Windows.Forms.TextBox txtEnterdEntity;
        private System.Windows.Forms.ListView listViewEntities;
    }
}
