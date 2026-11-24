using KushayVkusno.Data;

namespace KushayVkusno.Forms;

public class OrderCreateForm : Form
{
    private readonly Repository repo;
    private readonly User currentUser;
    private readonly List<Dish> selectedDishes;
    private readonly Dictionary<int, NumericUpDown> quantityInputs = new();

    private readonly NumericUpDown tableNumeric;
    private readonly TextBox detailsBox;
    private readonly Label totalCountLabel;
    private readonly Label totalPriceLabel;
    private readonly Button submitButton;
    private readonly Button cancelButton;

    public OrderCreateForm(Repository repo, User user, List<Dish> dishes)
    {
        this.repo = repo;
        this.currentUser = user;
        this.selectedDishes = dishes;

        Theme.Apply(this);
        Text = "КушайВкусно — Оформление заказа";
        ClientSize = new Size(650, 600);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        StartPosition = FormStartPosition.CenterParent;

        var header = new Label
        {
            Location = new Point(20, 15),
            Size = new Size(610, 30),
            Font = Theme.TitleFont,
            ForeColor = Theme.AccentAction,
            Text = "Оформление нового заказа"
        };

        var waiterLabel = new Label
        {
            Location = new Point(20, 55),
            Size = new Size(610, 22),
            Font = Theme.HeaderFont,
            Text = $"Официант: {currentUser.FullName}"
        };

        var dateLabel = new Label
        {
            Location = new Point(20, 80),
            Size = new Size(300, 20),
            Text = $"Дата и время: {DateTime.Now:dd.MM.yyyy HH:mm}"
        };

        var tableLbl = new Label { Location = new Point(360, 80), Size = new Size(100, 20), Text = "Номер стола:" };
        tableNumeric = new NumericUpDown
        {
            Location = new Point(470, 77),
            Size = new Size(80, 26),
            Minimum = 1,
            Maximum = 100,
            Value = 1
        };

        var dishesHeader = new Label
        {
            Location = new Point(20, 115),
            Size = new Size(610, 22),
            Font = Theme.HeaderFont,
            Text = "Выбранные блюда:"
        };

        var dishesPanel = new Panel
        {
            Location = new Point(20, 140),
            Size = new Size(610, 250),
            BorderStyle = BorderStyle.FixedSingle,
            AutoScroll = true
        };

        int y = 5;
        foreach (var dish in selectedDishes)
        {
            var pnl = new Panel { Location = new Point(5, y), Size = new Size(580, 36) };

            var nameLbl = new Label
            {
                Location = new Point(5, 6),
                Size = new Size(310, 24),
                Text = dish.Name,
                AutoEllipsis = true
            };

            var priceLbl = new Label
            {
                Location = new Point(320, 6),
                Size = new Size(110, 24),
                Text = $"{dish.Price:N2} ₽"
            };

            var qtyNum = new NumericUpDown
            {
                Location = new Point(440, 4),
                Size = new Size(65, 26),
                Minimum = 1,
                Maximum = 100,
                Value = 1
            };
            quantityInputs[dish.Id] = qtyNum;
            qtyNum.ValueChanged += (s, e) => RecalculateTotals();

            var rowTotalLbl = new Label
            {
                Location = new Point(510, 6),
                Size = new Size(70, 24),
                TextAlign = ContentAlignment.TopRight,
                Text = $"{dish.Price:N2} ₽"
            };

            qtyNum.ValueChanged += (s, e) =>
            {
                rowTotalLbl.Text = $"{(dish.Price * qtyNum.Value):N2} ₽";
            };

            pnl.Controls.Add(nameLbl);
            pnl.Controls.Add(priceLbl);
            pnl.Controls.Add(qtyNum);
            pnl.Controls.Add(rowTotalLbl);
            dishesPanel.Controls.Add(pnl);

            y += 40;
        }

        var detailsLbl = new Label { Location = new Point(20, 400), Size = new Size(300, 20), Text = "Дополнительная информация (пожелания к заказу):" };
        detailsBox = new TextBox
        {
            Location = new Point(20, 425),
            Size = new Size(610, 50),
            Multiline = true,
            ScrollBars = ScrollBars.Vertical
        };

        totalCountLabel = new Label
        {
            Location = new Point(20, 490),
            Size = new Size(300, 24),
            Font = Theme.HeaderFont,
            Text = "Количество элементов: 0"
        };

        totalPriceLabel = new Label
        {
            Location = new Point(330, 490),
            Size = new Size(300, 24),
            Font = Theme.HeaderFont,
            ForeColor = Theme.AccentAction,
            TextAlign = ContentAlignment.TopRight,
            Text = "Итоговая стоимость: 0,00 ₽"
        };

        submitButton = new Button
        {
            Location = new Point(320, 535),
            Size = new Size(180, 42),
            Text = "Оформить заказ",
            Font = Theme.HeaderFont,
            BackColor = Theme.AccentAction,
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Cursor = Cursors.Hand
        };
        submitButton.FlatAppearance.BorderSize = 0;
        submitButton.Click += async (s, e) => await SubmitOrderAsync();

        cancelButton = new Button
        {
            Location = new Point(510, 535),
            Size = new Size(120, 42),
            Text = "Отмена",
            Font = Theme.BaseFont,
            BackColor = Color.LightGray,
            FlatStyle = FlatStyle.Flat,
            Cursor = Cursors.Hand
        };
        cancelButton.FlatAppearance.BorderSize = 0;
        cancelButton.Click += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };

        Controls.Add(header);
        Controls.Add(waiterLabel);
        Controls.Add(dateLabel);
        Controls.Add(tableLbl);
        Controls.Add(tableNumeric);
        Controls.Add(dishesHeader);
        Controls.Add(dishesPanel);
        Controls.Add(detailsLbl);
        Controls.Add(detailsBox);
        Controls.Add(totalCountLabel);
        Controls.Add(totalPriceLabel);
        Controls.Add(submitButton);
        Controls.Add(cancelButton);

        RecalculateTotals();
    }

    private void RecalculateTotals()
    {
        int totalCount = 0;
        decimal totalPrice = 0;

        foreach (var dish in selectedDishes)
        {
            if (quantityInputs.TryGetValue(dish.Id, out var num))
            {
                int q = (int)num.Value;
                totalCount += q;
                totalPrice += dish.Price * q;
            }
        }

        totalCountLabel.Text = $"Количество элементов: {totalCount}";
        totalPriceLabel.Text = $"Итого: {totalPrice:N2} ₽";
    }

    private async Task SubmitOrderAsync()
    {
        submitButton.Enabled = false;
        try
        {
            var items = new List<(int DishId, int Quantity, decimal Price)>();
            foreach (var dish in selectedDishes)
            {
                int q = (int)quantityInputs[dish.Id].Value;
                if (q <= 0)
                {
                    MessageBox.Show("Количество блюд не может быть отрицательным или нулевым.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                items.Add((dish.Id, q, dish.Price));
            }

            int tableNum = (int)tableNumeric.Value;
            string details = detailsBox.Text.Trim();
            DateTime now = DateTime.Now;

            int orderId = await repo.CreateOrderAsync(currentUser.Id, now, tableNum, details, items);

            MessageBox.Show(
                $"Заказ №{orderId} успешно создан!\nСтол: {tableNum}\nЭлементов: {items.Sum(i => i.Quantity)}\nСумма: {items.Sum(i => i.Quantity * i.Price):N2} ₽",
                "Заказ оформлен", MessageBoxButtons.OK, MessageBoxIcon.Information);

            DialogResult = DialogResult.OK;
            Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка оформления заказа: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            submitButton.Enabled = true;
        }
    }
}
