﻿using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using BoektQuiz.Context;
using BoektQuiz.Repositories;
using BoektQuiz.Services;
using BoektQuiz.ViewModels;

namespace BoektQuiz.Util
{
    public static class AppContainer
    {
        private static IContainer _container;
        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();
            //ViewModels
            builder.RegisterType<AppShellViewModel>().SingleInstance();
            builder.RegisterType<LoginViewModel>().SingleInstance();
            builder.RegisterType<QuestionViewModel>().SingleInstance();
            builder.RegisterType<RegisterViewModel>().SingleInstance();
            builder.RegisterType<RoundEndViewModel>().SingleInstance();
            builder.RegisterType<RoundStartViewModel>().SingleInstance();
            //Services
            builder.RegisterType<NavigationService>().As<INavigationService>();
            //General
            builder.RegisterInstance(BoektQuizContextFactory.Create()).As<BoektQuizContext>();
            builder.RegisterType<RoundRepository>().As<IRoundRepository>();
            _container = builder.Build();
        }
        public static object Resolve(Type typeName)
        {
            return _container.Resolve(typeName);
        }
        public static T Resolve<T>()
        {
            return _container.Resolve<T>();
        }
    }
}
