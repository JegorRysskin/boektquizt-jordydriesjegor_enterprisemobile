﻿using System;

using GalaSoft.MvvmLight.Ioc;

using JuryApp.Services;
using JuryApp.Views;

namespace JuryApp.ViewModels
{
    [Windows.UI.Xaml.Data.Bindable]
    public class ViewModelLocator
    {
        private static ViewModelLocator _current;

        public static ViewModelLocator Current => _current ?? (_current = new ViewModelLocator());

        private ViewModelLocator()
        {
            SimpleIoc.Default.Register(() => new NavigationServiceEx());
            SimpleIoc.Default.Register<ShellViewModel>();
            Register<MainViewModel, MainPage>();
            Register<QuizzenViewModel, QuizzenPage>();
            Register<TeamsViewModel, TeamsPage>();
        }

        public TeamsViewModel TeamsViewModel => SimpleIoc.Default.GetInstance<TeamsViewModel>();

        public QuizzenViewModel QuizzenViewModel => SimpleIoc.Default.GetInstance<QuizzenViewModel>();

        public MainViewModel MainViewModel => SimpleIoc.Default.GetInstance<MainViewModel>();

        public ShellViewModel ShellViewModel => SimpleIoc.Default.GetInstance<ShellViewModel>();

        public NavigationServiceEx NavigationService => SimpleIoc.Default.GetInstance<NavigationServiceEx>();

        public void Register<VM, V>()
            where VM : class
        {
            SimpleIoc.Default.Register<VM>();

            NavigationService.Configure(typeof(VM).FullName, typeof(V));
        }
    }
}