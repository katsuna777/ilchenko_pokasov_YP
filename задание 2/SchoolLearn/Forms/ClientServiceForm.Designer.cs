#nullable disable

namespace SchoolLearn.Forms
{
    partial class ClientServiceForm
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
            footerPanel = new Panel();
            cancelButton = new Button();
            saveButton = new Button();
            bodyPanel = new Panel();
            serviceTitleLabel = new Label();
            durationLabel = new Label();
            clientLabel = new Label();
            clientComboBox = new ComboBox();
            clientStatusLabel = new Label();
            dateLabel = new Label();
            dateDateTimePicker = new DateTimePicker();
            timeLabel = new Label();
            timeMaskedTextBox = new MaskedTextBox();
            endCaptionLabel = new Label();
            endLabel = new Label();
            headerPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)logoPictureBox).BeginInit();
            footerPanel.SuspendLayout();
            bodyPanel.SuspendLayout();
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
            headingLabel.Text = "Запись клиента на услугу";
            headingLabel.TextAlign = ContentAlignment.MiddleLeft;
            headingLabel.Font = new Font("Comic Sans MS", 18F, FontStyle.Regular, GraphicsUnit.Point);
            headingLabel.Name = "headingLabel";
            headingLabel.TabIndex = 2;

            footerPanel.Size = new Size(700, 66);
            footerPanel.Location = new Point(0, 570);
            footerPanel.Dock = DockStyle.Bottom;
            footerPanel.Name = "footerPanel";
            footerPanel.TabIndex = 3;

            cancelButton.Size = new Size(126, 36);
            cancelButton.Location = new Point(322, 14);
            cancelButton.Text = "Назад";
            cancelButton.FlatStyle = FlatStyle.Flat;
            cancelButton.BackColor = Color.FromArgb(245, 248, 243);
            cancelButton.ForeColor = Color.FromArgb(34, 55, 66);
            cancelButton.UseVisualStyleBackColor = false;
            cancelButton.Cursor = Cursors.Hand;
            cancelButton.DialogResult = DialogResult.Cancel;
            cancelButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cancelButton.Name = "cancelButton";
            cancelButton.TabIndex = 4;

            saveButton.Size = new Size(216, 36);
            saveButton.Location = new Point(460, 14);
            saveButton.Text = "Записать клиента";
            saveButton.FlatStyle = FlatStyle.Flat;
            saveButton.BackColor = Color.FromArgb(4, 160, 255);
            saveButton.ForeColor = Color.White;
            saveButton.UseVisualStyleBackColor = false;
            saveButton.Cursor = Cursors.Hand;
            saveButton.Enabled = false;
            saveButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            saveButton.Name = "saveButton";
            saveButton.TabIndex = 5;

            bodyPanel.Size = new Size(700, 478);
            bodyPanel.Location = new Point(0, 92);
            bodyPanel.Dock = DockStyle.Fill;
            bodyPanel.AutoScroll = true;
            bodyPanel.Name = "bodyPanel";
            bodyPanel.TabIndex = 6;

            serviceTitleLabel.Size = new Size(652, 64);
            serviceTitleLabel.Location = new Point(24, 16);
            serviceTitleLabel.Text = "Наименование услуги";
            serviceTitleLabel.TextAlign = ContentAlignment.MiddleLeft;
            serviceTitleLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            serviceTitleLabel.Font = new Font("Comic Sans MS", 12F, FontStyle.Regular, GraphicsUnit.Point);
            serviceTitleLabel.Name = "serviceTitleLabel";
            serviceTitleLabel.TabIndex = 7;

            durationLabel.Size = new Size(652, 28);
            durationLabel.Location = new Point(24, 84);
            durationLabel.Text = "Длительность: 30 мин";
            durationLabel.TextAlign = ContentAlignment.MiddleLeft;
            durationLabel.ForeColor = Color.FromArgb(82, 103, 115);
            durationLabel.Name = "durationLabel";
            durationLabel.TabIndex = 8;

            clientLabel.Size = new Size(300, 24);
            clientLabel.Location = new Point(24, 128);
            clientLabel.Text = "Клиент *";
            clientLabel.TextAlign = ContentAlignment.MiddleLeft;
            clientLabel.ForeColor = Color.FromArgb(82, 103, 115);
            clientLabel.Name = "clientLabel";
            clientLabel.TabIndex = 9;

            clientComboBox.Size = new Size(652, 31);
            clientComboBox.Location = new Point(24, 160);
            clientComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            clientComboBox.DisplayMember = "FullName";
            clientComboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            clientComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            clientComboBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            clientComboBox.Name = "clientComboBox";
            clientComboBox.TabIndex = 10;

            clientStatusLabel.Size = new Size(652, 42);
            clientStatusLabel.Location = new Point(24, 200);
            clientStatusLabel.Text = "Загрузка клиентов…";
            clientStatusLabel.TextAlign = ContentAlignment.MiddleLeft;
            clientStatusLabel.ForeColor = Color.FromArgb(82, 103, 115);
            clientStatusLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            clientStatusLabel.Font = new Font("Comic Sans MS", 9F, FontStyle.Regular, GraphicsUnit.Point);
            clientStatusLabel.Name = "clientStatusLabel";
            clientStatusLabel.TabIndex = 11;

            dateLabel.Size = new Size(310, 24);
            dateLabel.Location = new Point(24, 256);
            dateLabel.Text = "Дата занятия *";
            dateLabel.TextAlign = ContentAlignment.MiddleLeft;
            dateLabel.ForeColor = Color.FromArgb(82, 103, 115);
            dateLabel.Name = "dateLabel";
            dateLabel.TabIndex = 12;

            dateDateTimePicker.Size = new Size(310, 30);
            dateDateTimePicker.Location = new Point(24, 288);
            dateDateTimePicker.Format = DateTimePickerFormat.Short;
            dateDateTimePicker.Name = "dateDateTimePicker";
            dateDateTimePicker.TabIndex = 13;

            timeLabel.Size = new Size(310, 24);
            timeLabel.Location = new Point(366, 256);
            timeLabel.Text = "Начало, ЧЧ:ММ *";
            timeLabel.TextAlign = ContentAlignment.MiddleLeft;
            timeLabel.ForeColor = Color.FromArgb(82, 103, 115);
            timeLabel.Name = "timeLabel";
            timeLabel.TabIndex = 14;

            timeMaskedTextBox.Size = new Size(310, 30);
            timeMaskedTextBox.Location = new Point(366, 288);
            timeMaskedTextBox.Mask = "00:00";
            timeMaskedTextBox.Text = "09:00";
            timeMaskedTextBox.Name = "timeMaskedTextBox";
            timeMaskedTextBox.TabIndex = 15;

            endCaptionLabel.Size = new Size(652, 28);
            endCaptionLabel.Location = new Point(24, 350);
            endCaptionLabel.Text = "Окончание занятия";
            endCaptionLabel.TextAlign = ContentAlignment.MiddleLeft;
            endCaptionLabel.ForeColor = Color.FromArgb(82, 103, 115);
            endCaptionLabel.Name = "endCaptionLabel";
            endCaptionLabel.TabIndex = 16;

            endLabel.Size = new Size(652, 36);
            endLabel.Location = new Point(24, 386);
            endLabel.Text = "Выберите дату и время";
            endLabel.TextAlign = ContentAlignment.MiddleLeft;
            endLabel.Font = new Font("Comic Sans MS", 13F, FontStyle.Regular, GraphicsUnit.Point);
            endLabel.Name = "endLabel";
            endLabel.TabIndex = 17;
            Controls.Add(bodyPanel);
            Controls.Add(footerPanel);
            Controls.Add(headerPanel);
            headerPanel.Controls.Add(logoPictureBox);
            headerPanel.Controls.Add(headingLabel);
            footerPanel.Controls.Add(cancelButton);
            footerPanel.Controls.Add(saveButton);
            bodyPanel.Controls.Add(serviceTitleLabel);
            bodyPanel.Controls.Add(durationLabel);
            bodyPanel.Controls.Add(clientLabel);
            bodyPanel.Controls.Add(clientComboBox);
            bodyPanel.Controls.Add(clientStatusLabel);
            bodyPanel.Controls.Add(dateLabel);
            bodyPanel.Controls.Add(dateDateTimePicker);
            bodyPanel.Controls.Add(timeLabel);
            bodyPanel.Controls.Add(timeMaskedTextBox);
            bodyPanel.Controls.Add(endCaptionLabel);
            bodyPanel.Controls.Add(endLabel);
            Load += ClientServiceForm_Load;
            FormClosing += ClientServiceForm_FormClosing;
            saveButton.Click += SaveButton_Click;
            dateDateTimePicker.ValueChanged += Time_Changed;
            timeMaskedTextBox.TextChanged += Time_Changed;

            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(700, 636);
            Font = new Font("Comic Sans MS", 10F, FontStyle.Regular, GraphicsUnit.Point);
            ForeColor = Color.FromArgb(34, 55, 66);
            Name = "ClientServiceForm";
            Text = "Леарн — запись клиента на услугу";
            MinimumSize = new Size(716, 650);
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            CancelButton = cancelButton;
            headerPanel.ResumeLayout(false);
            headerPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)logoPictureBox).EndInit();
            footerPanel.ResumeLayout(false);
            footerPanel.PerformLayout();
            bodyPanel.ResumeLayout(false);
            bodyPanel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }


        private Panel headerPanel;
        private PictureBox logoPictureBox;
        private Label headingLabel;
        private Panel footerPanel;
        private Button cancelButton;
        private Button saveButton;
        private Panel bodyPanel;
        private Label serviceTitleLabel;
        private Label durationLabel;
        private Label clientLabel;
        private ComboBox clientComboBox;
        private Label clientStatusLabel;
        private Label dateLabel;
        private DateTimePicker dateDateTimePicker;
        private Label timeLabel;
        private MaskedTextBox timeMaskedTextBox;
        private Label endCaptionLabel;
        private Label endLabel;
    }
}
