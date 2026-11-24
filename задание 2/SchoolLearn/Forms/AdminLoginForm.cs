using System.ComponentModel;
using SchoolLearn.Services;

namespace SchoolLearn.Forms;

public partial class AdminLoginForm : Form
{
    public string Code => codeTextBox.Text;

    public AdminLoginForm()
    {
        InitializeComponent();
    }

    private void AdminLoginForm_Load(object? sender, EventArgs e)
    {
        if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
            return;
        ImageAssets.ApplyBrand(this, logoPictureBox);
        codeTextBox.Select();
    }

    private void LoginButton_Click(object? sender, EventArgs e)
    {
        if (Code != "0000")
        {
            errorLabel.Text = "Неверный код. Проверьте введённые цифры.";
            codeTextBox.SelectAll();
            codeTextBox.Focus();
            return;
        }
        DialogResult = DialogResult.OK;
    }
}
