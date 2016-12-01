#region Dapplo 2016 - GNU Lesser General Public License

// Dapplo - building blocks for .NET applications
// Copyright (C) 2016 Dapplo
// 
// For more information see: http://dapplo.net/
// Dapplo repositories are hosted on GitHub: https://github.com/dapplo
// 
// This file is part of Dapplo.CaliburnMicro
// 
// Dapplo.CaliburnMicro is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// Dapplo.CaliburnMicro is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
// 
// You should have a copy of the GNU Lesser General Public License
// along with Dapplo.CaliburnMicro. If not, see <http://www.gnu.org/licenses/lgpl.txt>.

#endregion

#region Usings

using System.Collections.Generic;
using Dapplo.CaliburnMicro.Behaviors.Security;
using Dapplo.CaliburnMicro.Menu;

#endregion

namespace Dapplo.CaliburnMicro.Security
{
	/// <summary>
	///     Extend this to make your IMenuItem authentication aware, e.g. controling the IsEnabled / Visibility
	/// </summary>
	public class AuthenticatedMenuItem<TWhen> : MenuItem, IChangeableNeedAuthentication<TWhen>
	{
		private TWhen _whenPermissionMissing;
		private TWhen _whenPermission;
		private IEnumerable<string> _permissions;
		private AuthenticationTargetProperties _authenticationTargetProperty = AuthenticationTargetProperties.None;
		private PermissionOperations _permissionOperation = PermissionOperations.Or;

		/// <inheritdoc />
		public AuthenticationTargetProperties AuthenticationTargetProperty
		{
			get { return _authenticationTargetProperty; }
			set
			{
				_authenticationTargetProperty = value;
				NotifyOfPropertyChange(nameof(AuthenticationTargetProperty));
			}
		}

		/// <inheritdoc />
		public PermissionOperations PermissionOperation
		{
			get { return _permissionOperation; }
			set
			{
				_permissionOperation = value;
				NotifyOfPropertyChange(nameof(PermissionOperation));
			}
		}

		/// <inheritdoc />
		public IEnumerable<string> Permissions
		{
			get
			{
				return _permissions;
			}
			set
			{
				_permissions = value;
				NotifyOfPropertyChange(nameof(Permissions));
			}
		}

		/// <inheritdoc />
		public TWhen WhenPermission
		{
			get
			{
				return _whenPermission;
			}
			set
			{
				_whenPermission = value;
				NotifyOfPropertyChange(nameof(WhenPermission));
			}
		}

		/// <inheritdoc />
		public TWhen WhenPermissionMissing
		{
			get
			{
				return _whenPermissionMissing;
			}
			set
			{
				_whenPermissionMissing = value;
				NotifyOfPropertyChange(nameof(WhenPermissionMissing));
			}
		}
	}
}