#nullable disable

namespace SchoolLearn.Forms
{
    partial class UpcomingServicesForm
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
            subtitleLabel = new Label();
            footerPanel = new Panel();
            statusLabel = new Label();
            refreshButton = new Button();
            cancelButton = new Button();
            bodyPanel = new Panel();
            appointmentsDataGridView = new DataGridView();
            emptyLabel = new Label();
            refreshTimer = new System.Windows.Forms.Timer(components);
            serviceColumn = new DataGridViewTextBoxColumn();
            clientColumn = new DataGridViewTextBoxColumn();
            phoneColumn = new DataGridViewTextBoxColumn();
            startColumn = new DataGridViewTextBoxColumn();
            endColumn = new DataGridViewTextBoxColumn();
            remainingColumn = new DataGridViewTextBoxColumn();
            headerPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)logoPictureBox).BeginInit();
            footerPanel.SuspendLayout();
            bodyPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)appointmentsDataGridView).BeginInit();
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
            headingLabel.Text = "Ближайшие записи";
            headingLabel.TextAlign = ContentAlignment.MiddleLeft;
            headingLabel.Font = new Font("Comic Sans MS", 18F, FontStyle.Regular, GraphicsUnit.Point);
            headingLabel.Name = "headingLabel";
            headingLabel.TabIndex = 2;

            subtitleLabel.Size = new Size(650, 26);
            subtitleLabel.Location = new Point(104, 54);
            subtitleLabel.Text = "На следующие 48 часов • Обновление каждые 30 секунд";
            subtitleLabel.TextAlign = ContentAlignment.MiddleLeft;
            subtitleLabel.ForeColor = Color.FromArgb(82, 103, 115);
            subtitleLabel.Font = new Font("Comic Sans MS", 9F, FontStyle.Regular, GraphicsUnit.Point);
            subtitleLabel.Name = "subtitleLabel";
            subtitleLabel.TabIndex = 3;

            footerPanel.Size = new Size(1100, 60);
            footerPanel.Location = new Point(0, 584);
            footerPanel.Dock = DockStyle.Bottom;
            footerPanel.Name = "footerPanel";
            footerPanel.TabIndex = 4;

            statusLabel.Size = new Size(690, 36);
            statusLabel.Location = new Point(24, 12);
            statusLabel.Text = "Загрузка записей…";
            statusLabel.TextAlign = ContentAlignment.MiddleLeft;
            statusLabel.ForeColor = Color.FromArgb(82, 103, 115);
            statusLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            statusLabel.Name = "statusLabel";
            statusLabel.TabIndex = 5;

            refreshButton.Size = new Size(148, 36);
            refreshButton.Location = new Point(766, 12);
            refreshButton.Text = "Обновить";
            refreshButton.FlatStyle = FlatStyle.Flat;
            refreshButton.BackColor = Color.FromArgb(245, 248, 243);
            refreshButton.ForeColor = Color.FromArgb(34, 55, 66);
            refreshButton.UseVisualStyleBackColor = false;
            refreshButton.Cursor = Cursors.Hand;
            refreshButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            refreshButton.Name = "refreshButton";
            refreshButton.TabIndex = 6;

            cancelButton.Size = new Size(148, 36);
            cancelButton.Location = new Point(928, 12);
            cancelButton.Text = "Назад";
            cancelButton.FlatStyle = FlatStyle.Flat;
            cancelButton.BackColor = Color.FromArgb(245, 248, 243);
            cancelButton.ForeColor = Color.FromArgb(34, 55, 66);
            cancelButton.UseVisualStyleBackColor = false;
            cancelButton.Cursor = Cursors.Hand;
            cancelButton.DialogResult = DialogResult.Cancel;
            cancelButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cancelButton.Name = "cancelButton";
            cancelButton.TabIndex = 7;

            bodyPanel.Size = new Size(1100, 492);
            bodyPanel.Location = new Point(0, 92);
            bodyPanel.Dock = DockStyle.Fill;
            bodyPanel.Padding = new Padding(24, 16, 24, 0);
            bodyPanel.Name = "bodyPanel";
            bodyPanel.TabIndex = 8;

            appointmentsDataGridView.Size = new Size(1052, 476);
            appointmentsDataGridView.Location = new Point(24, 16);
            appointmentsDataGridView.Dock = DockStyle.Fill;
            appointmentsDataGridView.AutoGenerateColumns = false;
            appointmentsDataGridView.ReadOnly = true;
            appointmentsDataGridView.AllowUserToAddRows = false;
            appointmentsDataGridView.AllowUserToDeleteRows = false;
            appointmentsDataGridView.AllowUserToResizeRows = false;
            appointmentsDataGridView.RowHeadersVisible = false;
            appointmentsDataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            appointmentsDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            appointmentsDataGridView.MultiSelect = false;
            appointmentsDataGridView.BackgroundColor = Color.White;
            appointmentsDataGridView.BorderStyle = BorderStyle.None;
            appointmentsDataGridView.EnableHeadersVisualStyles = false;
            appointmentsDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            appointmentsDataGridView.Name = "appointmentsDataGridView";
            appointmentsDataGridView.TabIndex = 9;

            emptyLabel.Size = new Size(1052, 476);
            emptyLabel.Location = new Point(24, 16);
            emptyLabel.Dock = DockStyle.Fill;
            emptyLabel.Visible = false;
            emptyLabel.Text = "На ближайшие 48 часов записей нет.\r\nЗаписать клиента можно из списка услуг.";
            emptyLabel.TextAlign = ContentAlignment.MiddleCenter;
            emptyLabel.Name = "emptyLabel";
            emptyLabel.TabIndex = 10;

            refreshTimer.Interval = 30000;

            serviceColumn.DataPropertyName = "ServiceTitle";
            serviceColumn.HeaderText = "Услуга";
            serviceColumn.Width = 260;
            serviceColumn.ReadOnly = true;
            serviceColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            serviceColumn.Name = "serviceColumn";

            clientColumn.DataPropertyName = "ClientName";
            clientColumn.HeaderText = "Клиент";
            clientColumn.Width = 200;
            clientColumn.ReadOnly = true;
            clientColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            clientColumn.Name = "clientColumn";

            phoneColumn.DataPropertyName = "Phone";
            phoneColumn.HeaderText = "Телефон";
            phoneColumn.Width = 130;
            phoneColumn.ReadOnly = true;
            phoneColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            phoneColumn.Name = "phoneColumn";

            startColumn.DataPropertyName = "Start";
            startColumn.HeaderText = "Начало";
            startColumn.Width = 150;
            startColumn.ReadOnly = true;
            startColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            startColumn.Name = "startColumn";

            endColumn.DataPropertyName = "End";
            endColumn.HeaderText = "Окончание";
            endColumn.Width = 150;
            endColumn.ReadOnly = true;
            endColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            endColumn.Name = "endColumn";

            remainingColumn.DataPropertyName = "Remaining";
            remainingColumn.HeaderText = "До начала";
            remainingColumn.Width = 180;
            remainingColumn.ReadOnly = true;
            remainingColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            remainingColumn.Name = "remainingColumn";
            Controls.Add(bodyPanel);
            Controls.Add(footerPanel);
            Controls.Add(headerPanel);
            headerPanel.Controls.Add(logoPictureBox);
            headerPanel.Controls.Add(headingLabel);
            headerPanel.Controls.Add(subtitleLabel);
            bodyPanel.Controls.Add(appointmentsDataGridView);
            bodyPanel.Controls.Add(emptyLabel);
            footerPanel.Controls.Add(statusLabel);
            footerPanel.Controls.Add(refreshButton);
            footerPanel.Controls.Add(cancelButton);
            appointmentsDataGridView.Columns.AddRange(new DataGridViewColumn[] { serviceColumn, clientColumn, phoneColumn, startColumn, endColumn, remainingColumn });
            appointmentsDataGridView.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            appointmentsDataGridView.DefaultCellStyle.Padding = new Padding(5);
            appointmentsDataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(231, 250, 191);
            startColumn.DefaultCellStyle.Format = "dd.MM.yyyy HH:mm";
            endColumn.DefaultCellStyle.Format = "dd.MM.yyyy HH:mm";
            serviceColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            serviceColumn.MinimumWidth = 180;
            Load += UpcomingServicesForm_Load;
            FormClosed += UpcomingServicesForm_FormClosed;
            refreshButton.Click += RefreshButton_Click;
            refreshTimer.Tick += RefreshTimer_Tick;

            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1100, 644);
            Font = new Font("Comic Sans MS", 10F, FontStyle.Regular, GraphicsUnit.Point);
            ForeColor = Color.FromArgb(34, 55, 66);
            Name = "UpcomingServicesForm";
            Text = "Леарн — ближайшие записи";
            MinimumSize = new Size(1000, 500);
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
            ((System.ComponentModel.ISupportInitialize)appointmentsDataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }


        private Panel headerPanel;
        private PictureBox logoPictureBox;
        private Label headingLabel;
        private Label subtitleLabel;
        private Panel footerPanel;
        private Label statusLabel;
        private Button refreshButton;
        private Button cancelButton;
        private Panel bodyPanel;
        private DataGridView appointmentsDataGridView;
        private Label emptyLabel;
        private System.Windows.Forms.Timer refreshTimer;
        private DataGridViewTextBoxColumn serviceColumn;
        private DataGridViewTextBoxColumn clientColumn;
        private DataGridViewTextBoxColumn phoneColumn;
        private DataGridViewTextBoxColumn startColumn;
        private DataGridViewTextBoxColumn endColumn;
        private DataGridViewTextBoxColumn remainingColumn;
    }
}
