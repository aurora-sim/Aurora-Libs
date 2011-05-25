#region BSD License
/*
Copyright (c) 2004 Matthew Holmes (matthew@wildfiregames.com)

Redistribution and use in source and binary forms, with or without modification, are permitted
provided that the following conditions are met:

* Redistributions of source code must retain the above copyright notice, this list of conditions 
  and the following disclaimer. 
* Redistributions in binary form must reproduce the above copyright notice, this list of conditions 
  and the following disclaimer in the documentation and/or other materials provided with the 
  distribution. 
* The name of the author may not be used to endorse or promote products derived from this software 
  without specific prior written permission. 

THIS SOFTWARE IS PROVIDED BY THE AUTHOR ``AS IS'' AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, 
BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE 
ARE DISCLAIMED. IN NO EVENT SHALL THE AUTHOR BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL,
EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS
OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY
OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING
IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/
#endregion

#region CVS Information
/*
 * $Source$
 * $Author: jendave $
 * $Date: 2006-06-27 11:31:54 -0400 (Tue, 27 Jun 2006) $
 * $Revision: 113 $
 */
#endregion

using System;
using System.Collections;
using System.Collections.Specialized;
using System.IO;

using Prebuild.Core.Attributes;
using Prebuild.Core.Interfaces;
using Prebuild.Core.Nodes;
using Prebuild.Core.Utilities;

namespace Prebuild.Core.Targets
{
	/// <summary>
	/// 
	/// </summary>
	public struct ToolInfo
	{
		string name;
		string guid;
		string fileExtension;
		string xmlTag;

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
		public string Name
		{
			get
			{
				return name;
			}
			set
			{
				name = value;
			}
		}

		/// <summary>
		/// Gets or sets the GUID.
		/// </summary>
		/// <value>The GUID.</value>
		public string Guid
		{
			get
			{
				return guid;
			}
			set
			{
				guid = value;
			}
		}

		/// <summary>
		/// Gets or sets the file extension.
		/// </summary>
		/// <value>The file extension.</value>
		public string FileExtension
		{
			get
			{
				return fileExtension;
			}
			set
			{
				fileExtension = value;
			}
		}
		/// <summary>
		/// Gets or sets the XML tag.
		/// </summary>
		/// <value>The XML tag.</value>
		public string XmlTag
		{
			get
			{
				return xmlTag;
			}
			set
			{
				xmlTag = value;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ToolInfo"/> class.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="guid">The GUID.</param>
		/// <param name="fileExtension">The file extension.</param>
		/// <param name="xml">The XML.</param>
		public ToolInfo(string name, string guid, string fileExtension, string xml)
		{
			this.name = name;
			this.guid = guid;
			this.fileExtension = fileExtension;
			this.xmlTag = xml;
		}

		/// <summary>
		/// Equals operator
		/// </summary>
		/// <param name="obj">ToolInfo to compare</param>
		/// <returns>true if toolInfos are equal</returns>
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			if (obj.GetType() != typeof(ToolInfo))
				return false;
                
			ToolInfo c = (ToolInfo)obj;   
			return ((this.name == c.name) && (this.guid == c.guid) && (this.fileExtension == c.fileExtension) && (this.xmlTag == c.xmlTag));
		}

		/// <summary>
		/// Equals operator
		/// </summary>
		/// <param name="c1">ToolInfo to compare</param>
		/// <param name="c2">ToolInfo to compare</param>
		/// <returns>True if toolInfos are equal</returns>
		public static bool operator== (ToolInfo c1, ToolInfo c2)
		{
			return ((c1.name == c2.name) && (c1.guid == c2.guid) && (c1.fileExtension == c2.fileExtension) && (c1.xmlTag == c2.xmlTag));
		}

		/// <summary>
		/// Not equals operator
		/// </summary>
		/// <param name="c1">ToolInfo to compare</param>
		/// <param name="c2">ToolInfo to compare</param>
		/// <returns>True if toolInfos are not equal</returns>
		public static bool operator!= (ToolInfo c1, ToolInfo c2)
		{
			return !(c1 == c2);
		}

		/// <summary>
		/// Hash Code
		/// </summary>
		/// <returns>Hash code</returns>
		public override int GetHashCode()
		{
			return name.GetHashCode() ^ guid.GetHashCode() ^ this.fileExtension.GetHashCode() ^ this.xmlTag.GetHashCode();

		}
	}

	/// <summary>
	/// 
	/// </summary>
	[Target( "vs2005" )]
	public class VS2005Target : ITarget
	{
		#region Inner Classes

		#endregion

		#region Fields

		string solutionVersion = "9.00";
		string productVersion = "8.0.50727";
		string schemaVersion = "2.0";
		string versionName = "Visual C# 2005";
		VSVersion version = VSVersion.VS80;

		Hashtable tools;
		Kernel kernel;

		/// <summary>
		/// Gets or sets the solution version.
		/// </summary>
		/// <value>The solution version.</value>
		protected string SolutionVersion
		{
			get
			{
				return this.solutionVersion;
			}
			set
			{
				this.solutionVersion = value;
			}
		}
		/// <summary>
		/// Gets or sets the product version.
		/// </summary>
		/// <value>The product version.</value>
		protected string ProductVersion
		{
			get
			{
				return this.productVersion;
			}
			set
			{
				this.productVersion = value;
			}
		}
		/// <summary>
		/// Gets or sets the schema version.
		/// </summary>
		/// <value>The schema version.</value>
		protected string SchemaVersion
		{
			get
			{
				return this.schemaVersion;
			}
			set
			{
				this.schemaVersion = value;
			}
		}
		/// <summary>
		/// Gets or sets the name of the version.
		/// </summary>
		/// <value>The name of the version.</value>
		protected string VersionName
		{
			get
			{
				return this.versionName;
			}
			set
			{
				this.versionName = value;
			}
		}
		/// <summary>
		/// Gets or sets the version.
		/// </summary>
		/// <value>The version.</value>
		protected VSVersion Version
		{
			get
			{
				return this.version;
			}
			set
			{
				this.version = value;
			}
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="VS2005Target"/> class.
		/// </summary>
		public VS2005Target()
		{
			this.tools = new Hashtable();

			this.tools[ "C#" ] = new ToolInfo( "C#", "{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}", "csproj", "CSHARP" );
		}

		#endregion

		#region Private Methods

		private string MakeRefPath( ProjectNode project )
		{
			string ret = "";
			foreach ( ReferencePathNode node in project.ReferencePaths )
			{
				try
				{
					string fullPath = Helper.ResolvePath( node.Path );
					if ( ret.Length < 1 )
					{
						ret = fullPath;
					}
					else
					{
						ret += ";" + fullPath;
					}
				}
				catch ( ArgumentException )
				{
					this.kernel.Log.Write( LogType.Warning, "Could not resolve reference path: {0}", node.Path );
				}
			}

			return ret;
		}

		private void WriteProject( SolutionNode solution, ProjectNode project )
		{
			if ( !tools.ContainsKey( project.Language ) )
			{
				throw new UnknownLanguageException( "Unknown .NET language: " + project.Language );
			}

			ToolInfo toolInfo = (ToolInfo)tools[ project.Language ];
			string projectFile = Helper.MakeFilePath( project.FullPath, project.Name, toolInfo.FileExtension );
			StreamWriter ps = new StreamWriter( projectFile );

			kernel.CurrentWorkingDirectory.Push();
			Helper.SetCurrentDir( Path.GetDirectoryName( projectFile ) );

			#region Project File
			using ( ps )
			{
				ps.WriteLine( "<Project DefaultTargets=\"Build\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" );
				//ps.WriteLine("\t<{0}", toolInfo.XMLTag);
				ps.WriteLine( "\t<PropertyGroup>" );
				ps.WriteLine( "\t\t<ProjectType>Local</ProjectType>" );
				ps.WriteLine( "\t\t<ProductVersion>{0}</ProductVersion>", this.ProductVersion );
				ps.WriteLine( "\t\t<SchemaVersion>{0}</SchemaVersion>", this.SchemaVersion );
				ps.WriteLine( "\t\t<ProjectGuid>{{{0}}}</ProjectGuid>", project.Guid.ToString().ToUpper() );

				ps.WriteLine( "\t\t<Configuration Condition = \" '$(Configuration)' == '' \">Debug</Configuration>" );
				ps.WriteLine( "\t\t<Platform Condition = \" '$(Platform)' == '' \">AnyCPU</Platform>" );
				//ps.WriteLine("\t\t<Build>");

				//ps.WriteLine("\t\t\t<Settings");
				ps.WriteLine( "\t\t<ApplicationIcon>{0}</ApplicationIcon>", project.AppIcon );
				ps.WriteLine( "\t\t<AssemblyKeyContainerName></AssemblyKeyContainerName>" );
				ps.WriteLine( "\t\t<AssemblyName>{0}</AssemblyName>", project.AssemblyName );
				ps.WriteLine( "\t\t<AssemblyOriginatorKeyFile></AssemblyOriginatorKeyFile>" );
				ps.WriteLine( "\t\t<DefaultClientScript>JScript</DefaultClientScript>" );
				ps.WriteLine( "\t\t<DefaultHTMLPageLayout>Grid</DefaultHTMLPageLayout>" );
				ps.WriteLine( "\t\t<DefaultTargetSchema>IE50</DefaultTargetSchema>" );
				ps.WriteLine( "\t\t<DelaySign>false</DelaySign>" );

				//if(m_Version == VSVersion.VS70)
				//    ps.WriteLine("\t\t\t\tNoStandardLibraries = \"false\"");

				ps.WriteLine( "\t\t<OutputType>{0}</OutputType>", project.Type.ToString() );
				ps.WriteLine( "\t\t<RootNamespace>{0}</RootNamespace>", project.RootNamespace );
				ps.WriteLine( "\t\t<StartupObject>{0}</StartupObject>", project.StartupObject );
				//ps.WriteLine("\t\t\t>");
				ps.WriteLine( "\t\t<FileUpgradeFlags></FileUpgradeFlags>" );

				ps.WriteLine( "\t</PropertyGroup>" );

				foreach ( ConfigurationNode conf in project.Configurations )
				{
					ps.Write( "\t<PropertyGroup " );
					ps.WriteLine( "Condition=\" '$(Configuration)|$(Platform)' == '{0}|AnyCPU' \">", conf.Name );
					ps.WriteLine( "\t\t<AllowUnsafeBlocks>{0}</AllowUnsafeBlocks>", conf.Options[ "AllowUnsafe" ] );
					ps.WriteLine( "\t\t<BaseAddress>{0}</BaseAddress>", conf.Options[ "BaseAddress" ] );
					ps.WriteLine( "\t\t<CheckForOverflowUnderflow>{0}</CheckForOverflowUnderflow>", conf.Options[ "CheckUnderflowOverflow" ] );
					ps.WriteLine( "\t\t<ConfigurationOverrideFile></ConfigurationOverrideFile>" );
					ps.WriteLine( "\t\t<DefineConstants>{0}</DefineConstants>", conf.Options[ "CompilerDefines" ] );
					ps.WriteLine( "\t\t<DocumentationFile>{0}</DocumentationFile>", conf.Options[ "XmlDocFile" ] );
					ps.WriteLine( "\t\t<DebugSymbols>{0}</DebugSymbols>", conf.Options[ "DebugInformation" ] );
					ps.WriteLine( "\t\t<FileAlignment>{0}</FileAlignment>", conf.Options[ "FileAlignment" ] );
					//                    ps.WriteLine("\t\t<IncrementalBuild = \"{0}\"", conf.Options["IncrementalBuild"]);

					//                    if(m_Version == VSVersion.VS71)
					//                    {
					//                        ps.WriteLine("\t\t\t\t\tNoStdLib = \"{0}\"", conf.Options["NoStdLib"]);
					//                        ps.WriteLine("\t\t\t\t\tNoWarn = \"{0}\"", conf.Options["SuppressWarnings"]);
					//                    }

					ps.WriteLine( "\t\t<Optimize>{0}</Optimize>", conf.Options[ "OptimizeCode" ] );
					ps.WriteLine( "\t\t<OutputPath>{0}</OutputPath>",
						Helper.EndPath( Helper.NormalizePath( conf.Options[ "OutputPath" ].ToString() ) ) );
					ps.WriteLine( "\t\t<RegisterForComInterop>{0}</RegisterForComInterop>", conf.Options[ "RegisterComInterop" ] );
					ps.WriteLine( "\t\t<RemoveIntegerChecks>{0}</RemoveIntegerChecks>", conf.Options[ "RemoveIntegerChecks" ] );
					ps.WriteLine( "\t\t<TreatWarningsAsErrors>{0}</TreatWarningsAsErrors>", conf.Options[ "WarningsAsErrors" ] );
					ps.WriteLine( "\t\t<WarningLevel>{0}</WarningLevel>", conf.Options[ "WarningLevel" ] );
					ps.WriteLine( "\t\t<NoWarn>{0}</NoWarn>", conf.Options[ "SuppressWarnings" ] );
					ps.WriteLine( "\t</PropertyGroup>" );
				}

				//ps.WriteLine("\t\t\t</Settings>");

				// Assembly References
				ps.WriteLine( "\t<ItemGroup>" );
				foreach ( ReferenceNode refr in project.References )
				{
					if ( !solution.ProjectsTable.ContainsKey( refr.Name ) )
					{
						ps.Write( "\t\t<Reference" );
						ps.WriteLine( " Include = \"{0}\">", refr.Name );
						ps.WriteLine( "\t\t\t<Name>{0}</Name>", refr.Name );
                        // TODO: Allow reference to *.exe files
                        ps.WriteLine( "\t\t\t<HintPath>{0}</HintPath>", Helper.MakePathRelativeTo( project.FullPath, refr.Path + "\\" + refr.Name + ".dll" ) );
						ps.WriteLine( "\t\t</Reference>" );
					}
				}
				ps.WriteLine( "\t</ItemGroup>" );

				//Project References
				ps.WriteLine( "\t<ItemGroup>" );
				foreach ( ReferenceNode refr in project.References )
				{
					if ( solution.ProjectsTable.ContainsKey( refr.Name ) )
					{
						ProjectNode refProject = (ProjectNode)solution.ProjectsTable[ refr.Name ];
                        // TODO: Allow reference to visual basic projects
						ps.WriteLine( "\t\t<ProjectReference Include=\"{0}\">", Helper.MakePathRelativeTo( project.FullPath, Helper.MakeFilePath( refProject.FullPath, refProject.Name, "csproj" ) ) );
						//<ProjectReference Include="..\..\RealmForge\Utility\RealmForge.Utility.csproj">
						ps.WriteLine( "\t\t\t<Name>{0}</Name>", refProject.Name );
						//  <Name>RealmForge.Utility</Name>
						ps.WriteLine( "\t\t\t<Project>{{{0}}}</Project>", refProject.Guid.ToString().ToUpper() );
						//  <Project>{6880D1D3-69EE-461B-B841-5319845B20D3}</Project>
						ps.WriteLine( "\t\t\t<Package>{0}</Package>", toolInfo.Guid.ToString().ToUpper() );
						//  <Package>{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}</Package>
						ps.WriteLine( "\t\t</ProjectReference>" );
						//</ProjectReference>
					}
					else
					{
					}
				}
				ps.WriteLine( "\t</ItemGroup>" );

				//                ps.WriteLine("\t\t</Build>");
				ps.WriteLine( "\t<ItemGroup>" );

				//                ps.WriteLine("\t\t\t<Include>");
				foreach ( string file in project.Files )
				{
					ps.Write( "\t\t<{0} ", project.Files.GetBuildAction( file ) );
					ps.WriteLine( " Include =\"{0}\">", file.Replace( ".\\", "" ) );
					ps.WriteLine( "\t\t\t<SubType>Code</SubType>" );
					ps.WriteLine( "\t\t</{0}>", project.Files.GetBuildAction( file ) );

					//                    ps.WriteLine("\t\t\t\t<File");
					//                    ps.WriteLine("\t\t\t\t\tRelPath = \"{0}\"", file.Replace(".\\", ""));
					//                    ps.WriteLine("\t\t\t\t\tSubType = \"Code\"");
					//                    ps.WriteLine("\t\t\t\t\tBuildAction = \"{0}\"", project.Files.GetBuildAction(file));
					//                    ps.WriteLine("\t\t\t\t/>");
				}
				//                ps.WriteLine("\t\t\t</Include>");

				ps.WriteLine( "\t</ItemGroup>" );
				ps.WriteLine( "\t<Import Project=\"$(MSBuildBinPath)\\Microsoft.CSHARP.Targets\" />" );
				ps.WriteLine( "\t<PropertyGroup>" );
				ps.WriteLine( "\t\t<PreBuildEvent>" );
				ps.WriteLine( "\t\t</PreBuildEvent>" );
				ps.WriteLine( "\t\t<PostBuildEvent>" );
				ps.WriteLine( "\t\t</PostBuildEvent>" );
				ps.WriteLine( "\t</PropertyGroup>" );
				//                ps.WriteLine("\t</{0}>", toolInfo.XMLTag);
				ps.WriteLine( "</Project>" );
			}
			#endregion

			#region User File

			ps = new StreamWriter( projectFile + ".user" );
			using ( ps )
			{
				ps.WriteLine( "<Project xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">" );
				//ps.WriteLine( "<VisualStudioProject>" );
				//ps.WriteLine("\t<{0}>", toolInfo.XMLTag);
				//ps.WriteLine("\t\t<Build>");
				ps.WriteLine( "\t<PropertyGroup>" );
				//ps.WriteLine("\t\t\t<Settings ReferencePath=\"{0}\">", MakeRefPath(project));
				ps.WriteLine( "\t\t<Configuration Condition=\" '$(Configuration)' == '' \">Debug</Configuration>" );
				ps.WriteLine( "\t\t<Platform Condition=\" '$(Platform)' == '' \">AnyCPU</Platform>" );
				ps.WriteLine( "\t\t<ReferencePath>{0}</ReferencePath>", MakeRefPath( project ) );
				ps.WriteLine( "\t\t<LastOpenVersion>{0}</LastOpenVersion>", this.ProductVersion );
				ps.WriteLine( "\t\t<ProjectView>ProjectFiles</ProjectView>" );
				ps.WriteLine( "\t\t<ProjectTrust>0</ProjectTrust>" );
				ps.WriteLine( "\t</PropertyGroup>" );
				foreach ( ConfigurationNode conf in project.Configurations )
				{
					ps.Write( "\t<PropertyGroup" );
					ps.Write( " Condition = \" '$(Configuration)|$(Platform)' == '{0}|AnyCPU' \"", conf.Name );
					ps.WriteLine( " />" );
				}
				//ps.WriteLine("\t\t\t</Settings>");

				//ps.WriteLine("\t\t</Build>");
				//ps.WriteLine("\t</{0}>", toolInfo.XMLTag);
				//ps.WriteLine("</VisualStudioProject>");
				ps.WriteLine( "</Project>" );
			}
			#endregion

			kernel.CurrentWorkingDirectory.Pop();
		}

		private void WriteSolution( SolutionNode solution )
		{
			kernel.Log.Write( "Creating {0} solution and project files", this.VersionName );

			foreach ( ProjectNode project in solution.Projects )
			{
				kernel.Log.Write( "...Creating project: {0}", project.Name );
				WriteProject( solution, project );
			}

			kernel.Log.Write( "" );
			string solutionFile = Helper.MakeFilePath( solution.FullPath, solution.Name, "sln" );
			StreamWriter ss = new StreamWriter( solutionFile );

			kernel.CurrentWorkingDirectory.Push();
			Helper.SetCurrentDir( Path.GetDirectoryName( solutionFile ) );

			using ( ss )
			{
				ss.WriteLine( "Microsoft Visual Studio Solution File, Format Version {0}", this.SolutionVersion );
				ss.WriteLine( "# Visual Studio 2005" );
				foreach ( ProjectNode project in solution.Projects )
				{
					if ( !tools.ContainsKey( project.Language ) )
					{
						throw new UnknownLanguageException( "Unknown .NET language: " + project.Language );
					}

					ToolInfo toolInfo = (ToolInfo)tools[ project.Language ];

					string path = Helper.MakePathRelativeTo( solution.FullPath, project.FullPath );
					ss.WriteLine( "Project(\"{0}\") = \"{1}\", \"{2}\", \"{{{3}}}\"",
						toolInfo.Guid, project.Name, Helper.MakeFilePath( path, project.Name,
						toolInfo.FileExtension ), project.Guid.ToString().ToUpper() );

					//ss.WriteLine("\tProjectSection(ProjectDependencies) = postProject");
					//ss.WriteLine("\tEndProjectSection");

					ss.WriteLine( "EndProject" );
				}

                if ( solution.Files != null )
                {
                    ss.WriteLine( "Project(\"{0}\") = \"Solution Items\", \"Solution Items\", \"{1}\"", "{2150E333-8FDC-42A3-9474-1A3956D46DE8}", "{468F1D07-AD17-4CC3-ABD0-2CA268E4E1A6}" );
                    ss.WriteLine( "\tProjectSection(SolutionItems) = preProject" );
                    foreach ( string file in solution.Files )
                        ss.WriteLine( "\t\t{0} = {0}", file );
                    ss.WriteLine( "\tEndProjectSection" );
                    ss.WriteLine( "EndProject" );
                }

				ss.WriteLine( "Global" );

				ss.WriteLine( "\tGlobalSection(SolutionConfigurationPlatforms) = preSolution" );
				foreach ( ConfigurationNode conf in solution.Configurations )
				{
					ss.WriteLine( "\t\t{0}|Any CPU = {0}|Any CPU", conf.Name );
				}
				ss.WriteLine( "\tEndGlobalSection" );

				if ( solution.Projects.Count > 1 )
				{
					ss.WriteLine( "\tGlobalSection(ProjectDependencies) = postSolution" );
				}
				foreach ( ProjectNode project in solution.Projects )
				{
					for ( int i = 0; i < project.References.Count; i++ )
					{
						ReferenceNode refr = (ReferenceNode)project.References[ i ];
						if ( solution.ProjectsTable.ContainsKey( refr.Name ) )
						{
							ProjectNode refProject = (ProjectNode)solution.ProjectsTable[ refr.Name ];
							ss.WriteLine( "\t\t({{{0}}}).{1} = ({{{2}}})",
								project.Guid.ToString().ToUpper()
								, i,
								refProject.Guid.ToString().ToUpper()
								);
						}
					}
				}
				if ( solution.Projects.Count > 1 )
				{
					ss.WriteLine( "\tEndGlobalSection" );
				}
				ss.WriteLine( "\tGlobalSection(ProjectConfigurationPlatforms) = postSolution" );
				foreach ( ProjectNode project in solution.Projects )
				{
					foreach ( ConfigurationNode conf in solution.Configurations )
					{
						ss.WriteLine( "\t\t{{{0}}}.{1}|Any CPU.ActiveCfg = {1}|Any CPU",
							project.Guid.ToString().ToUpper(),
							conf.Name );

						ss.WriteLine( "\t\t{{{0}}}.{1}|Any CPU.Build.0 = {1}|Any CPU",
							project.Guid.ToString().ToUpper(),
							conf.Name );
					}
				}
				ss.WriteLine( "\tEndGlobalSection" );
				ss.WriteLine( "\tGlobalSection(SolutionProperties) = preSolution" );
				ss.WriteLine( "\t\tHideSolutionNode = FALSE" );
				ss.WriteLine( "\tEndGlobalSection" );

				ss.WriteLine( "EndGlobal" );
			}

			kernel.CurrentWorkingDirectory.Pop();
		}

		private void CleanProject( ProjectNode project )
		{
			kernel.Log.Write( "...Cleaning project: {0}", project.Name );

			ToolInfo toolInfo = (ToolInfo)tools[ project.Language ];
			string projectFile = Helper.MakeFilePath( project.FullPath, project.Name, toolInfo.FileExtension );
			string userFile = projectFile + ".user";

			Helper.DeleteIfExists( projectFile );
			Helper.DeleteIfExists( userFile );
		}

		private void CleanSolution( SolutionNode solution )
		{
			kernel.Log.Write( "Cleaning {0} solution and project files", this.VersionName, solution.Name );

			string slnFile = Helper.MakeFilePath( solution.FullPath, solution.Name, "sln" );
			string suoFile = Helper.MakeFilePath( solution.FullPath, solution.Name, "suo" );

			Helper.DeleteIfExists( slnFile );
			Helper.DeleteIfExists( suoFile );

			foreach ( ProjectNode project in solution.Projects )
			{
				CleanProject( project );
			}

			kernel.Log.Write( "" );
		}

		#endregion

		#region ITarget Members

		/// <summary>
		/// Writes the specified kern.
		/// </summary>
		/// <param name="kern">The kern.</param>
		public virtual void Write( Kernel kern )
		{
			if( kern == null )
			{
				throw new ArgumentNullException("kern");
			}
			kernel = kern;
			foreach ( SolutionNode sol in kernel.Solutions )
			{
				WriteSolution( sol );
			}
			kernel = null;
		}

		/// <summary>
		/// Cleans the specified kern.
		/// </summary>
		/// <param name="kern">The kern.</param>
		public virtual void Clean( Kernel kern )
		{
			if( kern == null )
			{
				throw new ArgumentNullException("kern");
			}
			kernel = kern;
			foreach ( SolutionNode sol in kernel.Solutions )
			{
				CleanSolution( sol );
			}
			kernel = null;
		}

		/// <summary>
		/// Gets the name.
		/// </summary>
		/// <value>The name.</value>
		public virtual string Name
		{
			get
			{
				return "vs2005";
			}
		}

		#endregion
	}
}
