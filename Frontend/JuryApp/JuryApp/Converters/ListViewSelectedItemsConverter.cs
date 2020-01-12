﻿using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

namespace JuryApp.Converters
{
    public class ListViewSelectedItemsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var listView = value as ListView;
            return listView?.SelectedItems;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
