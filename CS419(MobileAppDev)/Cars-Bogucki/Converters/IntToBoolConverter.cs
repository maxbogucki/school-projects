namespace Cars_Bogucki.Converters;
public class IntToBoolConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        if (value is int i)
        {
            var p = parameter as string;
            if (string.Equals(p, "gt0", StringComparison.OrdinalIgnoreCase))
                return i > 0;
            return i != 0;
        }
        return false;
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        => throw new NotImplementedException();
}
