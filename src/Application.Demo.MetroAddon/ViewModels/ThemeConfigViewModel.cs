﻿//  Dapplo - building blocks for desktop applications
//  Copyright (C) 2016-2017 Dapplo
// 
//  For more information see: http://dapplo.net/
//  Dapplo repositories are hosted on GitHub: https://github.com/dapplo
// 
//  This file is part of Dapplo.CaliburnMicro
// 
//  Dapplo.CaliburnMicro is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
// 
//  Dapplo.CaliburnMicro is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
// 
//  You should have a copy of the GNU Lesser General Public License
//  along with Dapplo.CaliburnMicro. If not, see <http://www.gnu.org/licenses/lgpl.txt>.

#region using

using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reactive.Disposables;
using System.Threading.Tasks;
using Application.Demo.MetroAddon.Configurations;
using Caliburn.Micro;
using Dapplo.CaliburnMicro.Configuration;
using Dapplo.CaliburnMicro.Extensions;
using Dapplo.CaliburnMicro.Metro;
using Dapplo.Log;
using Dapplo.Utils.Extensions;
using MahApps.Metro.Controls.Dialogs;

#endregion

namespace Application.Demo.MetroAddon.ViewModels
{
    [Export(typeof(IConfigScreen))]
    [SuppressMessage("Sonar Code Smell", "S110:Inheritance tree of classes should not be too deep", Justification = "MVVM Framework brings huge interitance tree.")]
    public sealed class ThemeConfigViewModel : ConfigScreen, IDisposable
    {
        private static readonly LogSource Log = new LogSource();

        /// <summary>
        ///     Here all disposables are registered, so we can clean the up
        /// </summary>
        private CompositeDisposable _disposables;

        /// <summary>
        ///     The avaible theme accents
        /// </summary>
        public ObservableCollection<Tuple<ThemeAccents, string>> AvailableThemeAccents { get; set; } = new ObservableCollection<Tuple<ThemeAccents, string>>();

        /// <summary>
        ///     The avaible themes
        /// </summary>
        public ObservableCollection<Tuple<Themes, string>> AvailableThemes { get; set; } = new ObservableCollection<Tuple<Themes, string>>();

        [Import]
        private CredentialsViewModel CredentialsVm { get; set; }

        /// <summary>
        ///     Used to make it possible to show a MahApps dialog
        /// </summary>
        [Import]
        private IDialogCoordinator Dialogcoordinator { get; set; }

        [Import(typeof(IWindowManager))]
        private MetroWindowManager MetroWindowManager { get; set; }

        [Import]
        public IUiConfiguration UiConfiguration { get; set; }

        [Import]
        public IUiTranslations UiTranslations { get; set; }

        /// <summary>
        ///     Used to show a "normal" dialog
        /// </summary>
        [Import]
        private IWindowManager WindowsManager { get; set; }

        public override void Commit()
        {
            // Manually commit
            UiConfiguration.CommitTransaction();
            MetroWindowManager.ChangeTheme(UiConfiguration.Theme);
            MetroWindowManager.ChangeThemeAccent(UiConfiguration.ThemeAccent);
            base.Commit();
        }


        /// <summary>
        ///     Show a MahApps dialog from the MVVM
        /// </summary>
        /// <returns></returns>
        public async Task Dialog()
        {
            await Dialogcoordinator.ShowMessageAsync(this, "Message from VM", "MVVM based dialogs!");
        }

        public override void Initialize(IConfig config)
        {
            // Prepare disposables
            _disposables?.Dispose();
            _disposables = new CompositeDisposable();

            AvailableThemeAccents.Clear();
            foreach (var themeAccent in Enum.GetValues(typeof(ThemeAccents)).Cast<ThemeAccents>())
            {
                var translation = themeAccent.EnumValueOf();
                AvailableThemeAccents.Add(new Tuple<ThemeAccents, string>(themeAccent, translation));
            }

            AvailableThemes.Clear();
            foreach (var theme in Enum.GetValues(typeof(Themes)).Cast<Themes>())
            {
                var translation = theme.EnumValueOf();
                AvailableThemes.Add(new Tuple<Themes, string>(theme, translation));
            }

            // Place this under the Ui parent
            ParentId = "Ui";

            // Make sure Commit/Rollback is called on the UiConfiguration
            config.Register(UiConfiguration);

            // automatically update the DisplayName
            _disposables.Add(UiTranslations.CreateDisplayNameBinding(this, nameof(IUiTranslations.Theme)));

            base.Initialize(config);
        }

        /// <summary>
        ///     Show the credentials for the login
        /// </summary>
        public void Login()
        {
            var result = WindowsManager.ShowDialog(CredentialsVm);
            Log.Info().WriteLine($"Girl you know it's {result}");
        }

        protected override void OnDeactivate(bool close)
        {
            _disposables.Dispose();
            base.OnDeactivate(close);
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}