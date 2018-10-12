using System;
using System.Windows;
using System.Windows.Controls;

namespace SongBeamerEdit.ViewModel
{
    public class FocusControl : DependencyObject
    {
        public static readonly DependencyProperty FocusProperty = DependencyProperty.RegisterAttached("Focus",
                                                                               typeof(Boolean),
                                                                               typeof(FocusControl),
                                                                               new PropertyMetadata(OnFocusChanged));

        private static void OnFocusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d != null && d is Control)
            {
                Control sender = d as Control;
                if ((bool)e.NewValue)
                {
                    sender.GotFocus += OnLostFocus;
                    sender.Focus();
                }
                else
                {
                    sender.GotFocus -= OnLostFocus;
                }
            }
        }

        private static void OnLostFocus(object sender, RoutedEventArgs e)
        {
            if (sender != null && sender is Control)
            {
                (sender as Control).SetValue(FocusProperty, false);
            }
        }

        public static Boolean GetFocus(DependencyObject target)
        {
            return (Boolean)target.GetValue(FocusProperty);
        }

        public static void SetFocus(DependencyObject target, Boolean value)
        {
            target.SetValue(FocusProperty, value);
        }
    }
}


