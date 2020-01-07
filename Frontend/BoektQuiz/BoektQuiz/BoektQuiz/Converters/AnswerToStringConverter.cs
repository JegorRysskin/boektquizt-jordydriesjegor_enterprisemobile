using BoektQuiz.Models;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace BoektQuiz.Converters
{
    public class AnswerToStringConverter : IValueConverter
    {
        private Answer answer;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Answer answer)
            {
                this.answer = answer;
                return answer.AnswerString;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (answer != null)
            {
                if (value is string answerString)
                {
                    answer.AnswerString = answerString;
                    return answer;
                }
            }

            return null;
        }
    }
}
