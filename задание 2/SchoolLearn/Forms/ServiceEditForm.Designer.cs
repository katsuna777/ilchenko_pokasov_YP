#nullable disable

namespace SchoolLearn.Forms
{
    partial class ServiceEditForm
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
            editorPanel = new Panel();
            idPanel = new Panel();
            idLabel = new Label();
            idTextBox = new TextBox();
            detailsPanel = new Panel();
            titleLabel = new Label();
            titleTextBox = new TextBox();
            costLabel = new Label();
            costNumericUpDown = new NumericUpDown();
            durationLabel = new Label();
            durationNumericUpDown = new NumericUpDown();
            discountLabel = new Label();
            discountNumericUpDown = new NumericUpDown();
            hintLabel = new Label();
            descriptionLabel = new Label();
            descriptionTextBox = new TextBox();
            imageLabel = new Label();
            imageComboBox = new ComboBox();
            chooseImageButton = new Button();
            previewPictureBox = new PictureBox();
            headerPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)logoPictureBox).BeginInit();
            footerPanel.SuspendLayout();
            editorPanel.SuspendLayout();
            idPanel.SuspendLayout();
            detailsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)costNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)durationNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)discountNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)previewPictureBox).BeginInit();
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
            headingLabel.Text = "Добавление услуги";
            headingLabel.TextAlign = ContentAlignment.MiddleLeft;
            headingLabel.Font = new Font("Comic Sans MS", 18F, FontStyle.Regular, GraphicsUnit.Point);
            headingLabel.Name = "headingLabel";
            headingLabel.TabIndex = 2;

            footerPanel.Size = new Size(744, 60);
            footerPanel.Location = new Point(0, 700);
            footerPanel.Dock = DockStyle.Bottom;
            footerPanel.Name = "footerPanel";
            footerPanel.TabIndex = 3;

            cancelButton.Size = new Size(120, 36);
            cancelButton.Location = new Point(440, 12);
            cancelButton.Text = "Назад";
            cancelButton.FlatStyle = FlatStyle.Flat;
            cancelButton.BackColor = Color.FromArgb(245, 248, 243);
            cancelButton.ForeColor = Color.FromArgb(34, 55, 66);
            cancelButton.UseVisualStyleBackColor = false;
            cancelButton.Cursor = Cursors.Hand;
            cancelButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cancelButton.DialogResult = DialogResult.Cancel;
            cancelButton.Name = "cancelButton";
            cancelButton.TabIndex = 4;

            saveButton.Size = new Size(148, 36);
            saveButton.Location = new Point(572, 12);
            saveButton.Text = "Сохранить";
            saveButton.FlatStyle = FlatStyle.Flat;
            saveButton.BackColor = Color.FromArgb(4, 160, 255);
            saveButton.ForeColor = Color.White;
            saveButton.UseVisualStyleBackColor = false;
            saveButton.Cursor = Cursors.Hand;
            saveButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            saveButton.Name = "saveButton";
            saveButton.TabIndex = 5;

            editorPanel.Size = new Size(744, 608);
            editorPanel.Location = new Point(0, 92);
            editorPanel.Dock = DockStyle.Fill;
            editorPanel.Name = "editorPanel";
            editorPanel.TabIndex = 6;

            idPanel.Size = new Size(744, 68);
            idPanel.Location = new Point(0, 0);
            idPanel.Dock = DockStyle.Top;
            idPanel.Name = "idPanel";
            idPanel.TabIndex = 7;

            idLabel.Size = new Size(200, 24);
            idLabel.Location = new Point(24, 4);
            idLabel.Text = "Идентификатор";
            idLabel.TextAlign = ContentAlignment.MiddleLeft;
            idLabel.ForeColor = Color.FromArgb(82, 103, 115);
            idLabel.Name = "idLabel";
            idLabel.TabIndex = 8;

            idTextBox.Size = new Size(696, 27);
            idTextBox.Location = new Point(24, 32);
            idTextBox.ReadOnly = true;
            idTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            idTextBox.BackColor = Color.FromArgb(242, 245, 240);
            idTextBox.Name = "idTextBox";
            idTextBox.TabIndex = 9;

            detailsPanel.Size = new Size(744, 540);
            detailsPanel.Location = new Point(0, 68);
            detailsPanel.Dock = DockStyle.Fill;
            detailsPanel.AutoScroll = true;
            detailsPanel.Name = "detailsPanel";
            detailsPanel.TabIndex = 10;

            titleLabel.Size = new Size(400, 24);
            titleLabel.Location = new Point(24, 8);
            titleLabel.Text = "Название услуги *";
            titleLabel.TextAlign = ContentAlignment.MiddleLeft;
            titleLabel.ForeColor = Color.FromArgb(82, 103, 115);
            titleLabel.Name = "titleLabel";
            titleLabel.TabIndex = 11;

            titleTextBox.Size = new Size(696, 28);
            titleTextBox.Location = new Point(24, 36);
            titleTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            titleTextBox.MaxLength = 100;
            titleTextBox.Name = "titleTextBox";
            titleTextBox.TabIndex = 12;

            costLabel.Size = new Size(200, 24);
            costLabel.Location = new Point(24, 78);
            costLabel.Text = "Стоимость, ₽ *";
            costLabel.TextAlign = ContentAlignment.MiddleLeft;
            costLabel.ForeColor = Color.FromArgb(82, 103, 115);
            costLabel.Name = "costLabel";
            costLabel.TabIndex = 13;

            costNumericUpDown.Size = new Size(210, 28);
            costNumericUpDown.Location = new Point(24, 108);
            costNumericUpDown.Maximum = 999999999M;
            costNumericUpDown.DecimalPlaces = 2;
            costNumericUpDown.ThousandsSeparator = true;
            costNumericUpDown.Name = "costNumericUpDown";
            costNumericUpDown.TabIndex = 14;

            durationLabel.Size = new Size(210, 24);
            durationLabel.Location = new Point(254, 78);
            durationLabel.Text = "Длительность, мин *";
            durationLabel.TextAlign = ContentAlignment.MiddleLeft;
            durationLabel.ForeColor = Color.FromArgb(82, 103, 115);
            durationLabel.Name = "durationLabel";
            durationLabel.TabIndex = 15;

            durationNumericUpDown.Size = new Size(210, 28);
            durationNumericUpDown.Location = new Point(254, 108);
            durationNumericUpDown.Maximum = 240M;
            durationNumericUpDown.DecimalPlaces = 2;
            durationNumericUpDown.Value = 30M;
            durationNumericUpDown.Name = "durationNumericUpDown";
            durationNumericUpDown.TabIndex = 16;

            discountLabel.Size = new Size(210, 24);
            discountLabel.Location = new Point(484, 78);
            discountLabel.Text = "Скидка, % *";
            discountLabel.TextAlign = ContentAlignment.MiddleLeft;
            discountLabel.ForeColor = Color.FromArgb(82, 103, 115);
            discountLabel.Name = "discountLabel";
            discountLabel.TabIndex = 17;

            discountNumericUpDown.Size = new Size(210, 28);
            discountNumericUpDown.Location = new Point(484, 108);
            discountNumericUpDown.Maximum = 100M;
            discountNumericUpDown.DecimalPlaces = 2;
            discountNumericUpDown.Name = "discountNumericUpDown";
            discountNumericUpDown.TabIndex = 18;

            hintLabel.Size = new Size(696, 32);
            hintLabel.Location = new Point(24, 146);
            hintLabel.Text = "Длительность: от 0 до 240 минут. Скидка: от 0 до 100%.";
            hintLabel.TextAlign = ContentAlignment.MiddleLeft;
            hintLabel.ForeColor = Color.FromArgb(82, 103, 115);
            hintLabel.Font = new Font("Comic Sans MS", 9F, FontStyle.Regular, GraphicsUnit.Point);
            hintLabel.Name = "hintLabel";
            hintLabel.TabIndex = 19;

            descriptionLabel.Size = new Size(300, 24);
            descriptionLabel.Location = new Point(24, 184);
            descriptionLabel.Text = "Описание";
            descriptionLabel.TextAlign = ContentAlignment.MiddleLeft;
            descriptionLabel.ForeColor = Color.FromArgb(82, 103, 115);
            descriptionLabel.Name = "descriptionLabel";
            descriptionLabel.TabIndex = 20;

            descriptionTextBox.Size = new Size(696, 90);
            descriptionTextBox.Location = new Point(24, 212);
            descriptionTextBox.Multiline = true;
            descriptionTextBox.ScrollBars = ScrollBars.Vertical;
            descriptionTextBox.MaxLength = 8000;
            descriptionTextBox.AcceptsReturn = true;
            descriptionTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            descriptionTextBox.Name = "descriptionTextBox";
            descriptionTextBox.TabIndex = 21;

            imageLabel.Size = new Size(300, 24);
            imageLabel.Location = new Point(24, 314);
            imageLabel.Text = "Главное изображение";
            imageLabel.TextAlign = ContentAlignment.MiddleLeft;
            imageLabel.ForeColor = Color.FromArgb(82, 103, 115);
            imageLabel.Name = "imageLabel";
            imageLabel.TabIndex = 22;

            imageComboBox.Size = new Size(510, 30);
            imageComboBox.Location = new Point(24, 346);
            imageComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            imageComboBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            imageComboBox.Name = "imageComboBox";
            imageComboBox.TabIndex = 23;

            chooseImageButton.Size = new Size(172, 36);
            chooseImageButton.Location = new Point(548, 342);
            chooseImageButton.Text = "Выбрать файл…";
            chooseImageButton.FlatStyle = FlatStyle.Flat;
            chooseImageButton.BackColor = Color.FromArgb(245, 248, 243);
            chooseImageButton.ForeColor = Color.FromArgb(34, 55, 66);
            chooseImageButton.UseVisualStyleBackColor = false;
            chooseImageButton.Cursor = Cursors.Hand;
            chooseImageButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            chooseImageButton.Name = "chooseImageButton";
            chooseImageButton.TabIndex = 24;

            previewPictureBox.Size = new Size(180, 112);
            previewPictureBox.Location = new Point(24, 394);
            previewPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            previewPictureBox.TabStop = false;
            previewPictureBox.Name = "previewPictureBox";
            previewPictureBox.TabIndex = 25;
            Controls.Add(editorPanel);
            Controls.Add(footerPanel);
            Controls.Add(headerPanel);
            headerPanel.Controls.Add(logoPictureBox);
            headerPanel.Controls.Add(headingLabel);
            footerPanel.Controls.Add(cancelButton);
            footerPanel.Controls.Add(saveButton);
            editorPanel.Controls.Add(detailsPanel);
            editorPanel.Controls.Add(idPanel);
            idPanel.Controls.Add(idLabel);
            idPanel.Controls.Add(idTextBox);
            detailsPanel.Controls.Add(titleLabel);
            detailsPanel.Controls.Add(titleTextBox);
            detailsPanel.Controls.Add(costLabel);
            detailsPanel.Controls.Add(costNumericUpDown);
            detailsPanel.Controls.Add(durationLabel);
            detailsPanel.Controls.Add(durationNumericUpDown);
            detailsPanel.Controls.Add(discountLabel);
            detailsPanel.Controls.Add(discountNumericUpDown);
            detailsPanel.Controls.Add(hintLabel);
            detailsPanel.Controls.Add(descriptionLabel);
            detailsPanel.Controls.Add(descriptionTextBox);
            detailsPanel.Controls.Add(imageLabel);
            detailsPanel.Controls.Add(imageComboBox);
            detailsPanel.Controls.Add(chooseImageButton);
            detailsPanel.Controls.Add(previewPictureBox);
            Load += ServiceEditForm_Load;
            FormClosing += ServiceEditForm_FormClosing;
            FormClosed += ServiceEditForm_FormClosed;
            saveButton.Click += SaveButton_Click;
            chooseImageButton.Click += ChooseImageButton_Click;
            imageComboBox.SelectedIndexChanged += ImageComboBox_SelectedIndexChanged;

            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(744, 650);
            Font = new Font("Comic Sans MS", 10F, FontStyle.Regular, GraphicsUnit.Point);
            ForeColor = Color.FromArgb(34, 55, 66);
            Name = "ServiceEditForm";
            Text = "Леарн — услуга";
            MinimumSize = new Size(760, 650);
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            CancelButton = cancelButton;
            headerPanel.ResumeLayout(false);
            headerPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)logoPictureBox).EndInit();
            footerPanel.ResumeLayout(false);
            footerPanel.PerformLayout();
            editorPanel.ResumeLayout(false);
            editorPanel.PerformLayout();
            idPanel.ResumeLayout(false);
            idPanel.PerformLayout();
            detailsPanel.ResumeLayout(false);
            detailsPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)costNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)durationNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)discountNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)previewPictureBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }


        private Panel headerPanel;
        private PictureBox logoPictureBox;
        private Label headingLabel;
        private Panel footerPanel;
        private Button cancelButton;
        private Button saveButton;
        private Panel editorPanel;
        private Panel idPanel;
        private Label idLabel;
        private TextBox idTextBox;
        private Panel detailsPanel;
        private Label titleLabel;
        private TextBox titleTextBox;
        private Label costLabel;
        private NumericUpDown costNumericUpDown;
        private Label durationLabel;
        private NumericUpDown durationNumericUpDown;
        private Label discountLabel;
        private NumericUpDown discountNumericUpDown;
        private Label hintLabel;
        private Label descriptionLabel;
        private TextBox descriptionTextBox;
        private Label imageLabel;
        private ComboBox imageComboBox;
        private Button chooseImageButton;
        private PictureBox previewPictureBox;
    }
}
