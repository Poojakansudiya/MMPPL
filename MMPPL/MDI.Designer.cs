namespace MMPPL
{
    partial class MDI
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
            this.mmppldbEntitiesView1 = new MMPPL.Views.MMPPLDBEntitiesView.MMPPLDBEntitiesView();
            this.SuspendLayout();
            // 
            // mmppldbEntitiesView1
            // 
            this.mmppldbEntitiesView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mmppldbEntitiesView1.Location = new System.Drawing.Point(0, 0);
            this.mmppldbEntitiesView1.Name = "mmppldbEntitiesView1";
            this.mmppldbEntitiesView1.Size = new System.Drawing.Size(547, 343);
            this.mmppldbEntitiesView1.TabIndex = 0;
            // 
            // MDI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(547, 343);
            this.Controls.Add(this.mmppldbEntitiesView1);
            this.Name = "MDI";
            this.Text = "MDI";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        #endregion

        private Views.MMPPLDBEntitiesView.MMPPLDBEntitiesView mmppldbEntitiesView1;
    }
}