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

#endregion

namespace Dapplo.CaliburnMicro.Configuration
{
    /// <summary>
    ///     A node for the config screens, this has empty and sealed transactional methods
    /// </summary>
    public class ConfigNode : ConfigScreen
    {
        /// <inheritdoc />
        public sealed override void Rollback()
        {
        }

        /// <inheritdoc />
        public sealed override void Terminate()
        {
        }

        /// <inheritdoc />
        public sealed override void Commit()
        {
        }
    }
}