//-------------------------------------------------------------------------------------------------
// <copyright file="Nit.cs" company="Microsoft">
//    Copyright (c) Microsoft Corporation.  All rights reserved.
//    
//    The use and distribution terms for this software are covered by the
//    Common Public License 1.0 (http://opensource.org/licenses/cpl1.0.php)
//    which can be found in the file CPL.TXT at the root of this distribution.
//    By using this software in any fashion, you are agreeing to be bound by
//    the terms of this license.
//    
//    You must not remove this notice, or any other, from this software.
// </copyright>
// 
// <summary>
// Build task to execute the Lux test runner.
// </summary>
//-------------------------------------------------------------------------------------------------

namespace Microsoft.Tools.WindowsInstallerXml.Lux
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.IO;
    using System.Text;

    using Microsoft.Build.Framework;
    using Microsoft.Build.Utilities;
    using Microsoft.Tools.WindowsInstallerXml.Build.Tasks;

    /// <summary>
    /// An MSBuild task to run the Nit test runner.
    /// </summary>
    public sealed class Nit : WixToolTask
    {
        private const string NitToolName = "Nit.exe";

        /// <summary>
        /// Initializes a new instance of the Nit class.
        /// </summary>
        public Nit()
        {
        }

        /// <summary>
        /// Gets or sets the list of test packages to run.
        /// </summary>
        public ITaskItem[] TestPackages
        {
            get;
            set;
        }

        /// <summary>
        /// Get the name of the executable.
        /// </summary>
        /// <remarks>The ToolName is used with the ToolPath to get the location of nit.exe.</remarks>
        /// <value>The name of the executable.</value>
        protected override string ToolName
        {
            get { return NitToolName; }
        }

        /// <summary>
        /// Get the path to the executable. 
        /// </summary>
        /// <remarks>GetFullPathToTool is only called when the ToolPath property is not set (see the ToolName remarks above).</remarks>
        /// <returns>The full path to the executable or simply nit.exe if it's expected to be in the system path.</returns>
        protected override string GenerateFullPathToTool()
        {
            // If there's not a ToolPath specified, it has to be in the system path.
            if (String.IsNullOrEmpty(this.ToolPath))
            {
                return NitToolName;
            }

            return Path.Combine(Path.GetFullPath(this.ToolPath), NitToolName);
        }

        /// <summary>
        /// Builds a command line from options in this task.
        /// </summary>
        /// <param name="commandLineBuilder">The command-line builder provided by the base class.</param>
        protected override void BuildCommandLine(WixCommandLineBuilder commandLineBuilder)
        {
            base.BuildCommandLine(commandLineBuilder);

            commandLineBuilder.AppendFileNamesIfNotNull(this.TestPackages, " ");
        }
    }
}