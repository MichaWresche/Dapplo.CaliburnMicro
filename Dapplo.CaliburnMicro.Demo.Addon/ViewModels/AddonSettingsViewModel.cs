﻿//  Dapplo - building blocks for desktop applications
//  Copyright (C) 2016 Dapplo
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

using System.ComponentModel.Composition;
using Caliburn.Micro;
using Dapplo.CaliburnMicro.Configuration;
using Dapplo.CaliburnMicro.Demo.Addon.Languages;
using Dapplo.CaliburnMicro.Demo.Languages;
using Dapplo.CaliburnMicro.Demo.UseCases.Configuration;
using Dapplo.Utils.Extensions;

#endregion

namespace Dapplo.CaliburnMicro.Demo.Addon.ViewModels
{
	[Export(typeof(IConfigScreen))]
	public sealed class AddonSettingsViewModel : ConfigScreen, IPartImportsSatisfiedNotification
	{
		[Import]
		public IAddonTranslations AddonTranslations { get; set; }

		[Import]
		private IEventAggregator EventAggregator { get; set; }

		public void OnImportsSatisfied()
		{
			// automatically update the DisplayName
			AddonTranslations.OnPropertyChanged(e =>
			{
				DisplayName = AddonTranslations.Addon;
			}, nameof(IAddonTranslations.Addon));
		}

		public void DoSomething()
		{
			EventAggregator.PublishOnUIThread("Addon button clicked");
		}

		public override int ParentId { get; } = (int)ConfigIds.Addons;
		public override int Id { get; } = (int) (ConfigIds.Addons + 1);
	}
}