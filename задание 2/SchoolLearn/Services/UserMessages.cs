using MySqlConnector;

namespace SchoolLearn.Services;

internal static class UserMessages
{
    public static void Error(IWin32Window? owner, Exception exception)
    {
        var message = exception is MySqlException databaseError
            ? "Не удалось выполнить запрос к базе данных. Проверьте сеть и доступность " +
                "сервера 85.236.189.123:13306, затем повторите действие.\nКод ошибки: " + databaseError.Number
            : exception.Message;
        MessageBox.Show(owner, message, "Леарн — ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}
