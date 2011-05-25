#region BSD License
/*
Copyright (c) 2004-2005 Matthew Holmes (matthew@wildfiregames.com), Dan Moorehead (dan05a@gmail.com)

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
 * $Date: 2006-01-30 14:56:58 -0500 (Mon, 30 Jan 2006) $
 * $Revision: 73 $
 */
#endregion

using System;
using System.Collections;
using System.Collections.Specialized;
using System.Xml;

using Prebuild.Core.Attributes;
using Prebuild.Core.Interfaces;

namespace Prebuild.Core.Nodes
{
	/// <summary>
	/// 
	/// </summary>
	[DataNode("Files")]
	public class FilesNode : DataNode
	{
		#region Fields

		private StringCollection m_Files;
		private Hashtable m_BuildActions;
		private Hashtable m_SubTypes;

		#endregion

		#region Constructors

		/// <summary>
		/// 
		/// </summary>
		public FilesNode()
		{
			m_Files = new StringCollection();
			m_BuildActions = new Hashtable();
			m_SubTypes = new Hashtable();
		}

		#endregion

		#region Properties

		/// <summary>
		/// 
		/// </summary>
		public int Count
		{
			get
			{
				return m_Files.Count;
			}
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// 
		/// </summary>
		/// <param name="file"></param>
		/// <returns></returns>
		public BuildAction GetBuildAction(string file)
		{
			if(!m_BuildActions.ContainsKey(file))
			{
				return BuildAction.Compile;
			}

			return (BuildAction)m_BuildActions[file];
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="file"></param>
		/// <returns></returns>
		public SubType GetSubType(string file)
		{
			if(!m_SubTypes.ContainsKey(file))
			{
				return SubType.Code;
			}

			return (SubType)m_SubTypes[file];
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="node"></param>
		public override void Parse(XmlNode node)
		{
			if( node == null )
			{
				throw new ArgumentNullException("node");
			}
			foreach(XmlNode child in node.ChildNodes)
			{
				IDataNode dataNode = Kernel.Instance.ParseNode(child, this);
				if(dataNode is FileNode)
				{
					FileNode fileNode = (FileNode)dataNode;
					if(fileNode.IsValid)
					{
						if (!m_Files.Contains(fileNode.Path))
						{
							m_Files.Add(fileNode.Path);
							m_BuildActions[fileNode.Path] = fileNode.BuildAction;
							m_SubTypes[fileNode.Path] = fileNode.SubType;
						}
					}
				}
				else if(dataNode is MatchNode)
				{
					foreach(string file in ((MatchNode)dataNode).Files)
					{
						//Console.WriteLine("fileNode: " + file);
						if (!m_Files.Contains(file))
						{
							m_Files.Add(file);
							m_BuildActions[file] = ((MatchNode)dataNode).BuildAction;
							m_SubTypes[file] = ((MatchNode)dataNode).SubType;
						}
					}
				}
			}
		}

		// TODO: Check in to why StringCollection's enumerator doesn't implement
		// IEnumerator?
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public StringEnumerator GetEnumerator()
		{
			return m_Files.GetEnumerator();
		}

		#endregion
	}
}
