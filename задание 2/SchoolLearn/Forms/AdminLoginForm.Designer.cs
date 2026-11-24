#nullable disable

namespace SchoolLearn.Forms
{
    partial class AdminLoginForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }


        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            headerPanel = new Panel();
            logoPictureBox = new PictureBox();
            headingLabel = new Label();
            promptLabel = new Label();
            codeTextBox = new TextBox();
            errorLabel = new Label();
            cancelButton = new Button();
            loginButton = new Button();
            headerPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)logoPictureBox).BeginInit();
            SuspendLayout();

            headerPanel.Size = new Size(1100, 92);
            headerPanel.Location = new Point(0, 0);
            headerPanel.Dock = DockStyle.Top;
            headerPanel.BackColor = Color.FromArgb(231, 250, 191);
            headerPanel.Name = "headerPanel";
            headerPanel.TabIndex = 0;

            logoPictureBox.Size = new Size(60, 60);
            logoPictureBox.Location = new Point(24, 14);
            logoPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            logoPictureBox.TabStop = false;
            logoPictureBox.Name = "logoPictureBox";
            logoPictureBox.TabIndex = 1;

            headingLabel.Size = new Size(620, 42);
            headingLabel.Location = new Point(102, 14);
            headingLabel.Text = "Вход администратора";
            headingLabel.TextAlign = ContentAlignment.MiddleLeft;
            headingLabel.Font = new Font("Comic Sans MS", 18F, FontStyle.Regular, GraphicsUnit.Point);
            headingLabel.Name = "headingLabel";
            headingLabel.TabIndex = 2;

            promptLabel.Size = new Size(480, 36);
            promptLabel.Location = new Point(24, 112);
            promptLabel.Text = "Введите четырёхзначный код доступа.";
            promptLabel.TextAlign = ContentAlignment.MiddleLeft;
            promptLabel.Name = "promptLabel";
            promptLabel.TabIndex = 3;

            codeTextBox.Size = new Size(472, 30);
            codeTextBox.Location = new Point(24, 158);
            codeTextBox.MaxLength = 4;
            codeTextBox.UseSystemPasswordChar = true;
            codeTextBox.Name = "codeTextBox";
            codeTextBox.TabIndex = 4;

            errorLabel.Size = new Size(472, 44);
            errorLabel.Location = new Point(24, 200);
            errorLabel.Text = "";
            errorLabel.TextAlign = ContentAlignment.MiddleLeft;
            errorLabel.ForeColor = Color.Firebrick;
            errorLabel.Name = "errorLabel";
            errorLabel.TabIndex = 5;

            cancelButton.Size = new Size(116, 38);
            cancelButton.Location = new Point(242, 260);
            cancelButton.Text = "Назад";
            cancelButton.FlatStyle = FlatStyle.Flat;
            cancelButton.BackColor = Color.FromArgb(245, 248, 243);
            cancelButton.ForeColor = Color.FromArgb(34, 55, 66);
            cancelButton.UseVisualStyleBackColor = false;
            cancelButton.Cursor = Cursors.Hand;
            cancelButton.DialogResult = DialogResult.Cancel;
            cancelButton.Name = "cancelButton";
            cancelButton.TabIndex = 6;

            loginButton.Size = new Size(126, 38);
            loginButton.Location = new Point(370, 260);
            loginButton.Text = "Войти";
            loginButton.FlatStyle = FlatStyle.Flat;
            loginButton.BackColor = Color.FromArgb(4, 160, 255);
            loginButton.ForeColor = Color.White;
            loginButton.UseVisualStyleBackColor = false;
            loginButton.Cursor = Cursors.Hand;
            loginButton.Name = "loginButton";
            loginButton.TabIndex = 7;
            Controls.Add(headerPanel);
            Controls.Add(promptLabel);
            Controls.Add(codeTextBox);
            Controls.Add(errorLabel);
            Controls.Add(cancelButton);
            Controls.Add(loginButton);
            headerPanel.Controls.Add(logoPictureBox);
            headerPanel.Controls.Add(headingLabel);
            Load += AdminLoginForm_Load;
            loginButton.Click += LoginButton_Click;

            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(520, 324);
            Font = new Font("Comic Sans MS", 10F, FontStyle.Regular, GraphicsUnit.Point);
            ForeColor = Color.FromArgb(34, 55, 66);
            Name = "AdminLoginForm";
            Text = "Леарн — вход администратора";
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            AcceptButton = loginButton;
            CancelButton = cancelButton;
            headerPanel.ResumeLayout(false);
            headerPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)logoPictureBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }


        private Panel headerPanel;
        private PictureBox logoPictureBox;
        private Label headingLabel;
        private Label promptLabel;
        private TextBox codeTextBox;
        private Label errorLabel;
        private Button cancelButton;
        private Button loginButton;
    }
}
