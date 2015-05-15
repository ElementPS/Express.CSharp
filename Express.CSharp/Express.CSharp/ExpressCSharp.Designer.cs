namespace Express.CSharp
{
    partial class ExpressCSharp
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
            this.txtRequest = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtResponse = new System.Windows.Forms.TextBox();
            this.btnSaleRequest = new System.Windows.Forms.Button();
            this.btnSendTransaction = new System.Windows.Forms.Button();
            this.btnClearData = new System.Windows.Forms.Button();
            this.btnHealthCheck = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtRequest
            // 
            this.txtRequest.Location = new System.Drawing.Point(15, 96);
            this.txtRequest.Multiline = true;
            this.txtRequest.Name = "txtRequest";
            this.txtRequest.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtRequest.Size = new System.Drawing.Size(383, 131);
            this.txtRequest.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Request:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 240);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Response:";
            // 
            // txtResponse
            // 
            this.txtResponse.Location = new System.Drawing.Point(12, 258);
            this.txtResponse.Multiline = true;
            this.txtResponse.Name = "txtResponse";
            this.txtResponse.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtResponse.Size = new System.Drawing.Size(383, 131);
            this.txtResponse.TabIndex = 2;
            // 
            // btnSaleRequest
            // 
            this.btnSaleRequest.Location = new System.Drawing.Point(15, 13);
            this.btnSaleRequest.Name = "btnSaleRequest";
            this.btnSaleRequest.Size = new System.Drawing.Size(137, 23);
            this.btnSaleRequest.TabIndex = 4;
            this.btnSaleRequest.Text = "Sale Request";
            this.btnSaleRequest.UseVisualStyleBackColor = true;
            this.btnSaleRequest.Click += new System.EventHandler(this.btnSaleRequest_Click);
            // 
            // btnSendTransaction
            // 
            this.btnSendTransaction.Location = new System.Drawing.Point(258, 13);
            this.btnSendTransaction.Name = "btnSendTransaction";
            this.btnSendTransaction.Size = new System.Drawing.Size(137, 23);
            this.btnSendTransaction.TabIndex = 5;
            this.btnSendTransaction.Text = "Send Transaction";
            this.btnSendTransaction.UseVisualStyleBackColor = true;
            this.btnSendTransaction.Click += new System.EventHandler(this.btnSendTransaction_Click);
            // 
            // btnClearData
            // 
            this.btnClearData.Location = new System.Drawing.Point(258, 42);
            this.btnClearData.Name = "btnClearData";
            this.btnClearData.Size = new System.Drawing.Size(137, 23);
            this.btnClearData.TabIndex = 6;
            this.btnClearData.Text = "Clear Data";
            this.btnClearData.UseVisualStyleBackColor = true;
            this.btnClearData.Click += new System.EventHandler(this.btnClearData_Click);
            // 
            // btnHealthCheck
            // 
            this.btnHealthCheck.Location = new System.Drawing.Point(15, 42);
            this.btnHealthCheck.Name = "btnHealthCheck";
            this.btnHealthCheck.Size = new System.Drawing.Size(137, 23);
            this.btnHealthCheck.TabIndex = 7;
            this.btnHealthCheck.Text = "Health Check";
            this.btnHealthCheck.UseVisualStyleBackColor = true;
            this.btnHealthCheck.Click += new System.EventHandler(this.btnHealthCheck_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(410, 401);
            this.Controls.Add(this.btnHealthCheck);
            this.Controls.Add(this.btnClearData);
            this.Controls.Add(this.btnSendTransaction);
            this.Controls.Add(this.btnSaleRequest);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtResponse);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtRequest);
            this.Name = "Form1";
            this.Text = "Express.CSharp";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtRequest;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtResponse;
        private System.Windows.Forms.Button btnSaleRequest;
        private System.Windows.Forms.Button btnSendTransaction;
        private System.Windows.Forms.Button btnClearData;
        private System.Windows.Forms.Button btnHealthCheck;
    }
}

