#nullable disable

namespace SchoolLearn
{
    partial class Form1
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
            adminButton = new Button();
            filterPanel = new Panel();
            searchLabel = new Label();
            searchTextBox = new TextBox();
            discountFilterLabel = new Label();
            discountComboBox = new ComboBox();
            sortLabel = new Label();
            sortComboBox = new ComboBox();
            resetButton = new Button();
            adminPanel = new Panel();
            addButton = new Button();
            upcomingButton = new Button();
            adminModeLabel = new Label();
            footerPanel = new Panel();
            countLabel = new Label();
            refreshButton = new Button();
            listPanel = new Panel();
            servicesFlowLayoutPanel = new FlowLayoutPanel();
            serviceCardPreview = new SchoolLearn.Controls.ServiceCardControl();
            emptyLabel = new Label();
            headerPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)logoPictureBox).BeginInit();
            filterPanel.SuspendLayout();
            adminPanel.SuspendLayout();
            footerPanel.SuspendLayout();
            listPanel.SuspendLayout();
            servicesFlowLayoutPanel.SuspendLayout();
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
            headingLabel.Text = "Леарн";
            headingLabel.TextAlign = ContentAlignment.MiddleLeft;
            headingLabel.Font = new Font("Comic Sans MS", 18F, FontStyle.Regular, GraphicsUnit.Point);
            headingLabel.Name = "headingLabel";
            headingLabel.TabIndex = 2;

            subtitleLabel.Size = new Size(650, 26);
            subtitleLabel.Location = new Point(104, 54);
            subtitleLabel.Text = "Школа иностранных языков • Каталог услуг";
            subtitleLabel.TextAlign = ContentAlignment.MiddleLeft;
            subtitleLabel.ForeColor = Color.FromArgb(82, 103, 115);
            subtitleLabel.Font = new Font("Comic Sans MS", 9F, FontStyle.Regular, GraphicsUnit.Point);
            subtitleLabel.Name = "subtitleLabel";
            subtitleLabel.TabIndex = 3;

            adminButton.Size = new Size(300, 40);
            adminButton.Location = new Point(772, 26);
            adminButton.Text = "Вход администратора";
            adminButton.FlatStyle = FlatStyle.Flat;
            adminButton.BackColor = Color.FromArgb(245, 248, 243);
            adminButton.ForeColor = Color.FromArgb(34, 55, 66);
            adminButton.UseVisualStyleBackColor = false;
            adminButton.Cursor = Cursors.Hand;
            adminButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            adminButton.Name = "adminButton";
            adminButton.TabIndex = 4;

            filterPanel.Size = new Size(1100, 98);
            filterPanel.Location = new Point(0, 92);
            filterPanel.Dock = DockStyle.Top;
            filterPanel.Name = "filterPanel";
            filterPanel.TabIndex = 5;

            searchLabel.Size = new Size(360, 24);
            searchLabel.Location = new Point(24, 10);
            searchLabel.Text = "Поиск по названию и описанию";
            searchLabel.TextAlign = ContentAlignment.MiddleLeft;
            searchLabel.ForeColor = Color.FromArgb(82, 103, 115);
            searchLabel.Name = "searchLabel";
            searchLabel.TabIndex = 6;

            searchTextBox.Size = new Size(390, 31);
            searchTextBox.Location = new Point(24, 42);
            searchTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            searchTextBox.PlaceholderText = "Название или описание услуги";
            searchTextBox.Name = "searchTextBox";
            searchTextBox.TabIndex = 7;

            discountFilterLabel.Size = new Size(170, 24);
            discountFilterLabel.Location = new Point(430, 10);
            discountFilterLabel.Text = "Скидка";
            discountFilterLabel.TextAlign = ContentAlignment.MiddleLeft;
            discountFilterLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            discountFilterLabel.ForeColor = Color.FromArgb(82, 103, 115);
            discountFilterLabel.Name = "discountFilterLabel";
            discountFilterLabel.TabIndex = 8;

            discountComboBox.Size = new Size(176, 31);
            discountComboBox.Location = new Point(430, 42);
            discountComboBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            discountComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            discountComboBox.Name = "discountComboBox";
            discountComboBox.TabIndex = 9;

            sortLabel.Size = new Size(220, 24);
            sortLabel.Location = new Point(622, 10);
            sortLabel.Text = "Стоимость со скидкой";
            sortLabel.TextAlign = ContentAlignment.MiddleLeft;
            sortLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            sortLabel.ForeColor = Color.FromArgb(82, 103, 115);
            sortLabel.Name = "sortLabel";
            sortLabel.TabIndex = 10;

            sortComboBox.Size = new Size(220, 31);
            sortComboBox.Location = new Point(622, 42);
            sortComboBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            sortComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            sortComboBox.Name = "sortComboBox";
            sortComboBox.TabIndex = 11;

            resetButton.Size = new Size(214, 38);
            resetButton.Location = new Point(858, 38);
            resetButton.Text = "Сбросить";
            resetButton.FlatStyle = FlatStyle.Flat;
            resetButton.BackColor = Color.FromArgb(245, 248, 243);
            resetButton.ForeColor = Color.FromArgb(34, 55, 66);
            resetButton.UseVisualStyleBackColor = false;
            resetButton.Cursor = Cursors.Hand;
            resetButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            resetButton.Name = "resetButton";
            resetButton.TabIndex = 12;

            adminPanel.Size = new Size(1100, 58);
            adminPanel.Location = new Point(0, 190);
            adminPanel.Dock = DockStyle.Top;
            adminPanel.Visible = false;
            adminPanel.Name = "adminPanel";
            adminPanel.TabIndex = 13;

            addButton.Size = new Size(192, 38);
            addButton.Location = new Point(24, 4);
            addButton.Text = "Добавить услугу";
            addButton.FlatStyle = FlatStyle.Flat;
            addButton.BackColor = Color.FromArgb(4, 160, 255);
            addButton.ForeColor = Color.White;
            addButton.UseVisualStyleBackColor = false;
            addButton.Cursor = Cursors.Hand;
            addButton.Name = "addButton";
            addButton.TabIndex = 14;

            upcomingButton.Size = new Size(218, 38);
            upcomingButton.Location = new Point(228, 4);
            upcomingButton.Text = "Ближайшие записи";
            upcomingButton.FlatStyle = FlatStyle.Flat;
            upcomingButton.BackColor = Color.FromArgb(245, 248, 243);
            upcomingButton.ForeColor = Color.FromArgb(34, 55, 66);
            upcomingButton.UseVisualStyleBackColor = false;
            upcomingButton.Cursor = Cursors.Hand;
            upcomingButton.Name = "upcomingButton";
            upcomingButton.TabIndex = 15;

            adminModeLabel.Size = new Size(300, 36);
            adminModeLabel.Location = new Point(466, 5);
            adminModeLabel.Text = "Режим администратора";
            adminModeLabel.TextAlign = ContentAlignment.MiddleLeft;
            adminModeLabel.ForeColor = Color.FromArgb(82, 103, 115);
            adminModeLabel.Name = "adminModeLabel";
            adminModeLabel.TabIndex = 16;

            footerPanel.Size = new Size(1100, 54);
            footerPanel.Location = new Point(0, 706);
            footerPanel.Dock = DockStyle.Bottom;
            footerPanel.Name = "footerPanel";
            footerPanel.TabIndex = 17;

            countLabel.Size = new Size(810, 34);
            countLabel.Location = new Point(24, 10);
            countLabel.Text = "Подключение к базе данных…";
            countLabel.TextAlign = ContentAlignment.MiddleLeft;
            countLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            countLabel.ForeColor = Color.FromArgb(82, 103, 115);
            countLabel.Name = "countLabel";
            countLabel.TabIndex = 18;

            refreshButton.Size = new Size(182, 38);
            refreshButton.Location = new Point(890, 8);
            refreshButton.Text = "Обновить";
            refreshButton.FlatStyle = FlatStyle.Flat;
            refreshButton.BackColor = Color.FromArgb(245, 248, 243);
            refreshButton.ForeColor = Color.FromArgb(34, 55, 66);
            refreshButton.UseVisualStyleBackColor = false;
            refreshButton.Cursor = Cursors.Hand;
            refreshButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            refreshButton.Name = "refreshButton";
            refreshButton.TabIndex = 19;

            listPanel.Size = new Size(1100, 458);
            listPanel.Location = new Point(0, 248);
            listPanel.Dock = DockStyle.Fill;
            listPanel.Name = "listPanel";
            listPanel.TabIndex = 20;

            servicesFlowLayoutPanel.Size = new Size(1100, 458);
            servicesFlowLayoutPanel.Location = new Point(0, 0);
            servicesFlowLayoutPanel.Dock = DockStyle.Fill;
            servicesFlowLayoutPanel.AutoScroll = true;
            servicesFlowLayoutPanel.WrapContents = false;
            servicesFlowLayoutPanel.FlowDirection = FlowDirection.TopDown;
            servicesFlowLayoutPanel.Padding = new Padding(24, 6, 12, 0);
            servicesFlowLayoutPanel.Name = "servicesFlowLayoutPanel";
            servicesFlowLayoutPanel.TabIndex = 21;

            serviceCardPreview.Size = new Size(1030, 188);
            serviceCardPreview.Location = new Point(24, 6);
            serviceCardPreview.Margin = new Padding(0, 0, 0, 12);
            serviceCardPreview.Name = "serviceCardPreview";
            serviceCardPreview.TabIndex = 22;

            emptyLabel.Size = new Size(1100, 458);
            emptyLabel.Location = new Point(0, 0);
            emptyLabel.Dock = DockStyle.Fill;
            emptyLabel.Visible = false;
            emptyLabel.Text = "Ничего не найдено. Измените запрос или выберите «Все скидки».";
            emptyLabel.TextAlign = ContentAlignment.MiddleCenter;
            emptyLabel.Name = "emptyLabel";
            emptyLabel.TabIndex = 23;
            Controls.Add(listPanel);
            Controls.Add(footerPanel);
            Controls.Add(adminPanel);
            Controls.Add(filterPanel);
            Controls.Add(headerPanel);
            headerPanel.Controls.Add(logoPictureBox);
            headerPanel.Controls.Add(headingLabel);
            headerPanel.Controls.Add(subtitleLabel);
            headerPanel.Controls.Add(adminButton);
            filterPanel.Controls.Add(searchLabel);
            filterPanel.Controls.Add(searchTextBox);
            filterPanel.Controls.Add(discountFilterLabel);
            filterPanel.Controls.Add(discountComboBox);
            filterPanel.Controls.Add(sortLabel);
            filterPanel.Controls.Add(sortComboBox);
            filterPanel.Controls.Add(resetButton);
            adminPanel.Controls.Add(addButton);
            adminPanel.Controls.Add(upcomingButton);
            adminPanel.Controls.Add(adminModeLabel);
            footerPanel.Controls.Add(countLabel);
            footerPanel.Controls.Add(refreshButton);
            listPanel.Controls.Add(servicesFlowLayoutPanel);
            listPanel.Controls.Add(emptyLabel);
            servicesFlowLayoutPanel.Controls.Add(serviceCardPreview);
            discountComboBox.Items.AddRange(new object[] { "Все скидки", "От 0 до 5%", "От 5 до 15%", "От 15 до 30%", "От 30 до 70%", "От 70 до 100%" });
            sortComboBox.Items.AddRange(new object[] { "Без сортировки", "Сначала дешевле", "Сначала дороже" });
            Load += Form1_Load;
            searchTextBox.TextChanged += Filters_Changed;
            discountComboBox.SelectedIndexChanged += Filters_Changed;
            sortComboBox.SelectedIndexChanged += Filters_Changed;
            servicesFlowLayoutPanel.SizeChanged += ServicesFlowLayoutPanel_SizeChanged;
            resetButton.Click += ResetButton_Click;
            adminButton.Click += AdminButton_Click;
            addButton.Click += AddButton_Click;
            upcomingButton.Click += UpcomingButton_Click;
            refreshButton.Click += RefreshButton_Click;

            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1100, 650);
            Font = new Font("Comic Sans MS", 10F, FontStyle.Regular, GraphicsUnit.Point);
            ForeColor = Color.FromArgb(34, 55, 66);
            Name = "Form1";
            Text = "Леарн — услуги школы иностранных языков";
            MinimumSize = new Size(1060, 600);
            StartPosition = FormStartPosition.CenterScreen;
            headerPanel.ResumeLayout(false);
            headerPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)logoPictureBox).EndInit();
            filterPanel.ResumeLayout(false);
            filterPanel.PerformLayout();
            adminPanel.ResumeLayout(false);
            adminPanel.PerformLayout();
            footerPanel.ResumeLayout(false);
            footerPanel.PerformLayout();
            listPanel.ResumeLayout(false);
            listPanel.PerformLayout();
            servicesFlowLayoutPanel.ResumeLayout(false);
            servicesFlowLayoutPanel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }


        private Panel headerPanel;
        private PictureBox logoPictureBox;
        private Label headingLabel;
        private Label subtitleLabel;
        private Button adminButton;
        private Panel filterPanel;
        private Label searchLabel;
        private TextBox searchTextBox;
        private Label discountFilterLabel;
        private ComboBox discountComboBox;
        private Label sortLabel;
        private ComboBox sortComboBox;
        private Button resetButton;
        private Panel adminPanel;
        private Button addButton;
        private Button upcomingButton;
        private Label adminModeLabel;
        private Panel footerPanel;
        private Label countLabel;
        private Button refreshButton;
        private Panel listPanel;
        private FlowLayoutPanel servicesFlowLayoutPanel;
        private SchoolLearn.Controls.ServiceCardControl serviceCardPreview;
        private Label emptyLabel;
    }
}
