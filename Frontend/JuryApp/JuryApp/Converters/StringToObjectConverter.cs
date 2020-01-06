using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using GalaSoft.MvvmLight;
using JuryApp.Core.Models;
using JuryApp.Core.Models.Collections;

namespace JuryApp.Converters
{
    public class StringToObjectConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
             // return ((ObservableCollection<string>)value).Select(s => new CorrectAnswer(s)).ToList();
             return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
             // return ((CorrectAnswers)value).Select(s => s.CorrectAnswerText).ToList();
             return null;
        }
    }
}
