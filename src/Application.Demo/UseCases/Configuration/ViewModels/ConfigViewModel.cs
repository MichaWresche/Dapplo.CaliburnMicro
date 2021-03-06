﻿//  Dapplo - building blocks for desktop applications
//  Copyright (C) 2016-2018 Dapplo
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
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Demo.Languages;
using Application.Demo.Models;
using Application.Demo.Shared;
using Dapplo.CaliburnMicro.Configuration;
using Dapplo.CaliburnMicro.Extensions;
using Dapplo.Config.Language;

#endregion

namespace Application.Demo.UseCases.Configuration.ViewModels
{
    /// <summary>
    ///     The settings view model is, well... for the settings :)
    ///     It is a conductor where one item is active.
    /// </summary>
    public sealed class ConfigViewModel : Config<IConfigScreen>
    {
        /// <summary>
        /// Is used from View
        /// </summary>
        public IDemoConfigTranslations ConfigTranslations { get; }

        /// <summary>
        /// Is used from View
        /// </summary>
        public ICoreTranslations CoreTranslations { get; }

        public ConfigViewModel(
            LanguageContainer languageContainer,
            IEnumerable<Lazy<IConfigScreen>> configScreens,
            IDemoConfigTranslations configTranslations,
            ICoreTranslations coreTranslations,
            IDemoConfiguration demoConfiguration)
        {
            ConfigScreens = configScreens;
            ConfigTranslations = configTranslations;
            CoreTranslations = coreTranslations;

            // automatically update the DisplayName
            CoreTranslations.CreateDisplayNameBinding(this, nameof(ICoreTranslations.Settings));

            // Set the current language (this should update all registered OnPropertyChanged anyway, so it can run in the background
            var lang = demoConfiguration.Language;
            Task.Run(async () => await languageContainer.ChangeLanguageAsync(lang).ConfigureAwait(false));
        }
    }
}