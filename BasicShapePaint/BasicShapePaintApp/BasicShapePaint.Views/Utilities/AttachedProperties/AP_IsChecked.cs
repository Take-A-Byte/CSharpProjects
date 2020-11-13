namespace BasicShapePaint.Views.Utilities.AttachedProperties
{
    using System;
    using System.Windows;

    internal class AP_IsChecked
    {
        #region Private Fields

        private static bool setupDone = false;

        #endregion Private Fields

        #region Public Fields

        public static readonly DependencyProperty IsCheckedProperty =
            DependencyProperty.RegisterAttached("IsChecked", typeof(bool), typeof(AP_IsChecked), new PropertyMetadata(true, IsCheckedPropertyChanged));

        #endregion Public Fields

        #region Public Methods

        public static bool GetIsChecked(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsCheckedProperty);
        }

        public static void SetIsChecked(DependencyObject obj, bool value)
        {
            obj.SetValue(IsCheckedProperty, value);
        }

        #endregion Public Methods

        #region Private Methods

        private static void IsCheckedPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            if (!setupDone)
            {
                (obj as FrameworkElement).PreviewMouseLeftButtonUp += AP_IsChecked_PreviewMouseLeftButtonUp;
                setupDone = true;
            }
        }

        private static void AP_IsChecked_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            (sender as DependencyObject).SetValue(IsCheckedProperty, !GetIsChecked((sender as DependencyObject)));
        }

        #endregion Private Methods
    }
}