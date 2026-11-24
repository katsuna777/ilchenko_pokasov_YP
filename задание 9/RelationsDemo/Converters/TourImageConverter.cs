using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace RelationsDemo.Converters;

public sealed class TourImageConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is byte[] { Length: > 0 } bytes)
        {
            try
            {
                using var stream = new MemoryStream(bytes);
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = stream;
                image.EndInit();
                image.Freeze();
                return image;
            }
            catch (NotSupportedException) { }
            catch (FileFormatException) { }
        }
        var placeholder = new BitmapImage(new Uri(Path.Combine(AppContext.BaseDirectory, "Resources", "picture.png")));
        placeholder.Freeze();
        return placeholder;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
        throw new NotSupportedException();
}
