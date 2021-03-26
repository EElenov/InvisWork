using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

using InvisWork.Tools;

namespace InvisWork.Core
{
    /// <summary>
    /// Converts <see cref="Visibility"/>.Visible to <see cref="bool"/> true, the rest of the modifiers to <see cref="bool"/> false.
    /// </summary>
    public class VisibilityToBoolean : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => value.TryCast<Visibility>(out var cast) && (cast == Visibility.Visible);
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => value.TryCast<bool>(out var cast) ? (cast ? Visibility.Visible : Visibility.Collapsed) : Visibility.Collapsed;
    }
    public class VisibilityInverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => value.TryCast<Visibility>(out var cast) ? (cast == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible) : Visibility.Collapsed;
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => value.TryCast<Visibility>(out var cast) ? (cast == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible) : Visibility.Collapsed;
    }
    public class BooleanInverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => value.TryCast(out bool cast) && (cast != true);
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => value.TryCast(out bool cast) && (cast == true);
    }
    public class NullImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => value == null ? DependencyProperty.UnsetValue : value;
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => Binding.DoNothing;
    }
}

