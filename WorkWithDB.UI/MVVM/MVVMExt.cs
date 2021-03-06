﻿using System;
using System.Windows;
using System.Windows.Controls;

namespace WorkWithDB.UI.MVVM
{
    public static class MVVMExt
    {
        /// <summary>
        /// Gets "MVVMExt.Password" attached property. Please be carefull to use in production, it's insecure.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static String GetPassword(PasswordBox obj)
        {
            return (String)obj.GetValue(PasswordProperty);
        }
        /// <summary>
        /// Sets "MVVMExt.Password" attached property. Please be carefull to use in production, it's insecure.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetPassword(PasswordBox obj, String value)
        {
            obj.SetValue(PasswordProperty, value);
        }

        // Using a DependencyProperty as the backing store for EncryptedPassword.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.RegisterAttached(
                "Password", 
                typeof(String), 
                typeof(MVVMExt), 
                new FrameworkPropertyMetadata("FakeValueOfPassword_efg34t34tervfgt5gb3dfgbe54stb5", CallBack)
                {
                    BindsTwoWayByDefault = true
                }
            );

        private static void CallBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var passwordBox = (PasswordBox) d;

            //We want event to be fired only once
            passwordBox.PasswordChanged -= PasswordBoxOnPasswordChanged;
            passwordBox.PasswordChanged += PasswordBoxOnPasswordChanged;

            if (passwordBox.Password != (string)e.NewValue)
                passwordBox.Password = (string)e.NewValue;
        }

        private static void PasswordBoxOnPasswordChanged(object sender, RoutedEventArgs routedEventArgs)
        {
            var passwordBox = (PasswordBox)sender;
            passwordBox.SetValue(PasswordProperty, passwordBox.Password);
        }
    }
}