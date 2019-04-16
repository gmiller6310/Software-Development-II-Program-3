namespace UPVApp
{
    partial class ChooseAddressForm
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
            this.components = new System.ComponentModel.Container();
            this.addressListComboBox = new System.Windows.Forms.ComboBox();
            this.chooseAddressLabel = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // addressListComboBox
            // 
            this.addressListComboBox.FormattingEnabled = true;
            this.addressListComboBox.Location = new System.Drawing.Point(79, 34);
            this.addressListComboBox.Name = "addressListComboBox";
            this.addressListComboBox.Size = new System.Drawing.Size(121, 21);
            this.addressListComboBox.TabIndex = 0;
            this.addressListComboBox.Validating += new System.ComponentModel.CancelEventHandler(this.addressCbo_Validating);
            this.addressListComboBox.Validated += new System.EventHandler(this.AllFields_Validated);
            // 
            // chooseAddressLabel
            // 
            this.chooseAddressLabel.AutoSize = true;
            this.chooseAddressLabel.Location = new System.Drawing.Point(80, 9);
            this.chooseAddressLabel.Name = "chooseAddressLabel";
            this.chooseAddressLabel.Size = new System.Drawing.Size(120, 13);
            this.chooseAddressLabel.TabIndex = 1;
            this.chooseAddressLabel.Text = "Choose Address to Edit:";
            // 
            // okButton
            // 
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(53, 72);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 2;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Validating += new System.ComponentModel.CancelEventHandler(this.addressCbo_Validating);
            this.okButton.Validated += new System.EventHandler(this.AllFields_Validated);
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(154, 72);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 3;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.cancelButton_MouseDown);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // ChooseAddressForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(284, 107);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.chooseAddressLabel);
            this.Controls.Add(this.addressListComboBox);
            this.Name = "ChooseAddressForm";
            this.Text = "ChooseAddress";
            this.Load += new System.EventHandler(this.ChooseAddressForm_Load);
            this.Validating += new System.ComponentModel.CancelEventHandler(this.addressCbo_Validating);
            this.Validated += new System.EventHandler(this.AllFields_Validated);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox addressListComboBox;
        private System.Windows.Forms.Label chooseAddressLabel;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}