#nullable disable

namespace SchoolLearn.Controls
{
    partial class ServiceCardControl
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
            servicePictureBox = new PictureBox();
            titleLabel = new Label();
            oldPriceLabel = new Label();
            priceLabel = new Label();
            discountLabel = new Label();
            descriptionLabel = new Label();
            editButton = new Button();
            deleteButton = new Button();
            bookButton = new Button();
            ((System.ComponentModel.ISupportInitialize)servicePictureBox).BeginInit();
            SuspendLayout();

            servicePictureBox.Size = new Size(140, 118);
            servicePictureBox.Location = new Point(14, 14);
            servicePictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            servicePictureBox.TabStop = false;
            servicePictureBox.Name = "servicePictureBox";
            servicePictureBox.TabIndex = 0;

            titleLabel.Size = new Size(780, 50);
            titleLabel.Location = new Point(174, 12);
            titleLabel.Text = "Название услуги";
            titleLabel.TextAlign = ContentAlignment.MiddleLeft;
            titleLabel.Font = new Font("Comic Sans MS", 12F, FontStyle.Regular, GraphicsUnit.Point);
            titleLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            titleLabel.Name = "titleLabel";
            titleLabel.TabIndex = 1;

            oldPriceLabel.Size = new Size(110, 28);
            oldPriceLabel.Location = new Point(174, 64);
            oldPriceLabel.Text = "1 000,00 ₽";
            oldPriceLabel.TextAlign = ContentAlignment.MiddleLeft;
            oldPriceLabel.Font = new Font("Comic Sans MS", 10F, FontStyle.Strikeout, GraphicsUnit.Point);
            oldPriceLabel.ForeColor = Color.FromArgb(82, 103, 115);
            oldPriceLabel.Name = "oldPriceLabel";
            oldPriceLabel.TabIndex = 2;

            priceLabel.Size = new Size(420, 28);
            priceLabel.Location = new Point(286, 64);
            priceLabel.Text = "900,00 ₽ за 30 мин";
            priceLabel.TextAlign = ContentAlignment.MiddleLeft;
            priceLabel.Font = new Font("Comic Sans MS", 11F, FontStyle.Regular, GraphicsUnit.Point);
            priceLabel.Name = "priceLabel";
            priceLabel.TabIndex = 3;

            discountLabel.Size = new Size(170, 28);
            discountLabel.Location = new Point(780, 64);
            discountLabel.Text = "Скидка 10%";
            discountLabel.TextAlign = ContentAlignment.MiddleLeft;
            discountLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            discountLabel.ForeColor = Color.FromArgb(82, 103, 115);
            discountLabel.Name = "discountLabel";
            discountLabel.TabIndex = 4;

            descriptionLabel.Size = new Size(780, 32);
            descriptionLabel.Location = new Point(174, 99);
            descriptionLabel.Text = "Описание услуги";
            descriptionLabel.TextAlign = ContentAlignment.MiddleLeft;
            descriptionLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            descriptionLabel.ForeColor = Color.FromArgb(82, 103, 115);
            descriptionLabel.AutoEllipsis = true;
            descriptionLabel.Name = "descriptionLabel";
            descriptionLabel.TabIndex = 5;

            editButton.Size = new Size(160, 34);
            editButton.Location = new Point(174, 140);
            editButton.Text = "Редактировать";
            editButton.FlatStyle = FlatStyle.Flat;
            editButton.BackColor = Color.FromArgb(245, 248, 243);
            editButton.ForeColor = Color.FromArgb(34, 55, 66);
            editButton.UseVisualStyleBackColor = false;
            editButton.Cursor = Cursors.Hand;
            editButton.Name = "editButton";
            editButton.TabIndex = 6;

            deleteButton.Size = new Size(110, 34);
            deleteButton.Location = new Point(346, 140);
            deleteButton.Text = "Удалить";
            deleteButton.FlatStyle = FlatStyle.Flat;
            deleteButton.BackColor = Color.FromArgb(245, 248, 243);
            deleteButton.ForeColor = Color.FromArgb(34, 55, 66);
            deleteButton.UseVisualStyleBackColor = false;
            deleteButton.Cursor = Cursors.Hand;
            deleteButton.Name = "deleteButton";
            deleteButton.TabIndex = 7;

            bookButton.Size = new Size(214, 34);
            bookButton.Location = new Point(468, 140);
            bookButton.Text = "Записать клиента";
            bookButton.FlatStyle = FlatStyle.Flat;
            bookButton.BackColor = Color.FromArgb(4, 160, 255);
            bookButton.ForeColor = Color.White;
            bookButton.UseVisualStyleBackColor = false;
            bookButton.Cursor = Cursors.Hand;
            bookButton.Name = "bookButton";
            bookButton.TabIndex = 8;
            Controls.Add(servicePictureBox);
            Controls.Add(titleLabel);
            Controls.Add(oldPriceLabel);
            Controls.Add(priceLabel);
            Controls.Add(discountLabel);
            Controls.Add(descriptionLabel);
            Controls.Add(editButton);
            Controls.Add(deleteButton);
            Controls.Add(bookButton);
            editButton.Click += EditButton_Click;
            deleteButton.Click += DeleteButton_Click;
            bookButton.Click += BookButton_Click;

            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Size = new Size(980, 188);
            Font = new Font("Comic Sans MS", 10F, FontStyle.Regular, GraphicsUnit.Point);
            ForeColor = Color.FromArgb(34, 55, 66);
            Name = "ServiceCardControl";
            BorderStyle = BorderStyle.FixedSingle;
            Margin = new Padding(0, 0, 0, 12);
            ((System.ComponentModel.ISupportInitialize)servicePictureBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }


        private PictureBox servicePictureBox;
        private Label titleLabel;
        private Label oldPriceLabel;
        private Label priceLabel;
        private Label discountLabel;
        private Label descriptionLabel;
        private Button editButton;
        private Button deleteButton;
        private Button bookButton;
    }
}
