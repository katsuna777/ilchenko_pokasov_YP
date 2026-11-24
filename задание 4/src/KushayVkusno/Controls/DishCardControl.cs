using KushayVkusno.Data;

namespace KushayVkusno.Controls;

public class DishCardControl : UserControl
{
    private readonly PictureBox pictureBox;
    private readonly Label nameLabel;
    private readonly Label descriptionLabel;
    private readonly Label categoryLabel;
    private readonly Label priceLabel;
    private readonly CheckBox selectCheckBox;

    public Dish Dish { get; }
    public bool IsSelected => selectCheckBox.Checked;

    public event EventHandler? SelectionChanged;

    public DishCardControl(Dish dish, bool showSelection = true)
    {
        Dish = dish;
        DoubleBuffered = true;
        Size = new Size(800, 130);
        Margin = new Padding(4);
        BackColor = Theme.MainBackground;
        BorderStyle = BorderStyle.FixedSingle;

        pictureBox = new PictureBox
        {
            Location = new Point(10, 10),
            Size = new Size(110, 108),
            SizeMode = PictureBoxSizeMode.Zoom,
            Image = Theme.LoadImage(dish.ImagePath)
        };

        selectCheckBox = new CheckBox
        {
            Location = new Point(130, 12),
            Size = new Size(20, 20),
            Visible = showSelection,
            Cursor = Cursors.Hand
        };
        selectCheckBox.CheckedChanged += (s, e) =>
        {
            BackColor = selectCheckBox.Checked ? Theme.SecondaryBackground : Theme.MainBackground;
            SelectionChanged?.Invoke(this, EventArgs.Empty);
        };

        nameLabel = new Label
        {
            Location = new Point(showSelection ? 160 : 130, 10),
            Size = new Size(620, 26),
            Font = Theme.HeaderFont,
            ForeColor = Theme.TextDark,
            Text = dish.Name,
            AutoEllipsis = true
        };

        descriptionLabel = new Label
        {
            Location = new Point(130, 38),
            Size = new Size(650, 40),
            Font = Theme.BaseFont,
            ForeColor = Color.DimGray,
            Text = dish.Description,
            AutoEllipsis = true
        };

        categoryLabel = new Label
        {
            Location = new Point(130, 84),
            Size = new Size(350, 24),
            Font = Theme.BaseFont,
            ForeColor = Theme.TextDark,
            Text = $"Категория: {dish.CategoryName}"
        };

        priceLabel = new Label
        {
            Location = new Point(500, 84),
            Size = new Size(280, 24),
            Font = Theme.HeaderFont,
            ForeColor = Theme.AccentAction,
            TextAlign = ContentAlignment.TopRight,
            Text = $"Цена: {dish.Price:N2} ₽"
        };

        Controls.Add(pictureBox);
        Controls.Add(selectCheckBox);
        Controls.Add(nameLabel);
        Controls.Add(descriptionLabel);
        Controls.Add(categoryLabel);
        Controls.Add(priceLabel);

        Click += (s, e) => { if (showSelection) selectCheckBox.Checked = !selectCheckBox.Checked; };
        nameLabel.Click += (s, e) => { if (showSelection) selectCheckBox.Checked = !selectCheckBox.Checked; };
        descriptionLabel.Click += (s, e) => { if (showSelection) selectCheckBox.Checked = !selectCheckBox.Checked; };
    }

    public void SetChecked(bool isChecked)
    {
        selectCheckBox.Checked = isChecked;
    }
}
