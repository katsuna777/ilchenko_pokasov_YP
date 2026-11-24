using KushayVkusno.Data;

namespace KushayVkusno.Forms;

public class DishEditForm : Form
{
    private readonly Repository repo;
    private readonly Dish? originalDish;
    private readonly List<Category> categories;

    private readonly TextBox nameBox;
    private readonly NumericUpDown priceNumeric;
    private readonly ComboBox categoryBox;
    private readonly TextBox descriptionBox;
    private readonly PictureBox pictureBox;
    private readonly Button chooseImageButton;
    private readonly Button saveButton;
    private readonly Button cancelButton;
    private string? selectedImagePath;

    public DishEditForm(Repository repo, Dish? dish, List<Category> categories)
    {
        this.repo = repo;
        this.originalDish = dish;
        this.categories = categories;

        Theme.Apply(this);
        Text = dish == null ? "КушайВкусно — Добавление блюда" : "КушайВкусно — Редактирование блюда";
        ClientSize = new Size(580, 520);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        StartPosition = FormStartPosition.CenterParent;

        var heading = new Label
        {
            Location = new Point(20, 15),
            Size = new Size(540, 30),
            Font = Theme.TitleFont,
            ForeColor = Theme.AccentAction,
            Text = dish == null ? "Новое блюдо" : $"Редактирование блюда (ID: {dish.Id})"
        };

        var nameLbl = new Label { Location = new Point(20, 60), Size = new Size(200, 20), Text = "Наименование блюда:" };
        nameBox = new TextBox { Location = new Point(20, 82), Size = new Size(340, 28), Text = dish?.Name ?? "" };

        var catLbl = new Label { Location = new Point(20, 120), Size = new Size(200, 20), Text = "Категория:" };
        categoryBox = new ComboBox { Location = new Point(20, 142), Size = new Size(340, 28), DropDownStyle = ComboBoxStyle.DropDownList };
        foreach (var c in categories) categoryBox.Items.Add(c.Name);
        if (dish != null)
        {
            categoryBox.SelectedItem = dish.CategoryName;
        }
        else if (categoryBox.Items.Count > 0)
        {
            categoryBox.SelectedIndex = 0;
        }

        var priceLbl = new Label { Location = new Point(20, 180), Size = new Size(200, 20), Text = "Стоимость (руб):" };
        priceNumeric = new NumericUpDown
        {
            Location = new Point(20, 202),
            Size = new Size(180, 28),
            DecimalPlaces = 2,
            Maximum = 1000000,
            Minimum = 0,
            Value = dish?.Price ?? 100.00m
        };

        var descLbl = new Label { Location = new Point(20, 240), Size = new Size(200, 20), Text = "Описание / Состав:" };
        descriptionBox = new TextBox
        {
            Location = new Point(20, 262),
            Size = new Size(340, 140),
            Multiline = true,
            ScrollBars = ScrollBars.Vertical,
            Text = dish?.Description ?? ""
        };

        var imgLbl = new Label { Location = new Point(380, 60), Size = new Size(180, 20), Text = "Изображение:" };
        pictureBox = new PictureBox
        {
            Location = new Point(380, 82),
            Size = new Size(180, 180),
            BorderStyle = BorderStyle.FixedSingle,
            SizeMode = PictureBoxSizeMode.Zoom,
            Image = Theme.LoadImage(dish?.ImagePath)
        };
        selectedImagePath = dish?.ImagePath;

        chooseImageButton = new Button
        {
            Location = new Point(380, 272),
            Size = new Size(180, 36),
            Text = "Выбрать фото...",
            Font = Theme.BaseFont,
            BackColor = Theme.SecondaryBackground,
            FlatStyle = FlatStyle.Flat,
            Cursor = Cursors.Hand
        };
        chooseImageButton.FlatAppearance.BorderSize = 0;
        chooseImageButton.Click += ChooseImage_Click;

        saveButton = new Button
        {
            Location = new Point(240, 445),
            Size = new Size(180, 42),
            Text = "Сохранить",
            Font = Theme.HeaderFont,
            BackColor = Theme.AccentAction,
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Cursor = Cursors.Hand
        };
        saveButton.FlatAppearance.BorderSize = 0;
        saveButton.Click += async (s, e) => await SaveAsync();

        cancelButton = new Button
        {
            Location = new Point(430, 445),
            Size = new Size(130, 42),
            Text = "Отмена",
            Font = Theme.BaseFont,
            BackColor = Color.LightGray,
            FlatStyle = FlatStyle.Flat,
            Cursor = Cursors.Hand
        };
        cancelButton.FlatAppearance.BorderSize = 0;
        cancelButton.Click += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };

        Controls.Add(heading);
        Controls.Add(nameLbl);
        Controls.Add(nameBox);
        Controls.Add(catLbl);
        Controls.Add(categoryBox);
        Controls.Add(priceLbl);
        Controls.Add(priceNumeric);
        Controls.Add(descLbl);
        Controls.Add(descriptionBox);
        Controls.Add(imgLbl);
        Controls.Add(pictureBox);
        Controls.Add(chooseImageButton);
        Controls.Add(saveButton);
        Controls.Add(cancelButton);
    }

    private void ChooseImage_Click(object? sender, EventArgs e)
    {
        using var ofd = new OpenFileDialog
        {
            Filter = "Изображения|*.jpg;*.jpeg;*.png",
            Title = "Выберите изображение блюда"
        };
        if (ofd.ShowDialog(this) == DialogResult.OK)
        {
            try
            {
                var destDir = Path.Combine(AppContext.BaseDirectory, "Images");
                Directory.CreateDirectory(destDir);
                var fileName = Path.GetFileName(ofd.FileName);
                var destPath = Path.Combine(destDir, fileName);
                File.Copy(ofd.FileName, destPath, overwrite: true);

                selectedImagePath = fileName;
                pictureBox.Image?.Dispose();
                pictureBox.Image = Theme.LoadImage(fileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки файла: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    private async Task SaveAsync()
    {
        string name = nameBox.Text.Trim();
        string desc = descriptionBox.Text.Trim();
        if (string.IsNullOrWhiteSpace(name))
        {
            MessageBox.Show("Введите наименование блюда.", "Валидация", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            nameBox.Focus();
            return;
        }

        if (categoryBox.SelectedIndex < 0)
        {
            MessageBox.Show("Выберите категорию блюда.", "Валидация", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var cat = categories[categoryBox.SelectedIndex];

        saveButton.Enabled = false;
        try
        {
            var dish = new Dish(
                originalDish?.Id ?? 0,
                name,
                priceNumeric.Value,
                cat.Id,
                cat.Name,
                desc,
                selectedImagePath
            );

            await repo.SaveDishAsync(dish);
            DialogResult = DialogResult.OK;
            Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка при сохранении: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            saveButton.Enabled = true;
        }
    }
}
