#region BSD License
/*
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

using System;
using System.Collections.Specialized;
using System.Reflection;

using DNPreBuild.Core;
using DNPreBuild.Core.Util;

namespace DNPreBuild 
{
	class DNPreBuild
    {
        #region Main
		
        [STAThread]
		static void Main(string[] args) 
        {
            try 
            {
                Root root = Root.Instance;
                root.Initialize(LogTarget.File | LogTarget.Console, args);
                bool exit = false;

                if(root.CommandLine.WasPassed("usage"))
                {
                    exit = true;
                    OutputUsage();
                }
                if(root.CommandLine.WasPassed("showtargets"))
                {
                    exit = true;
                    OutputTargets(root);
                }

                if(exit)
                    Environment.Exit(0);

                root.Process();
            }
            catch
            {
                Environment.Exit(1);
            }
		}

        #endregion

        #region Private Methods

        private static void OutputUsage() 
        {
            Console.WriteLine("Usage: dnpb /target <target> [options]");
            Console.WriteLine("Available command-line switches:");
            Console.WriteLine("");
            Console.WriteLine("/target          Target for .NET Pre-Build");
            Console.WriteLine("/file            XML file to process");
            Console.WriteLine("/log             Log file to write to");
            Console.WriteLine("");
            Console.WriteLine("See 'dnpb /showtargets for a list of available targets");
            Console.WriteLine("");
        }

        private static void OutputTargets(Root root)
        {
            Console.WriteLine("Targets available in .NET Pre-build:");
            Console.WriteLine("");
            foreach(string target in root.Targets.Keys)
                Console.WriteLine(target);
            Console.WriteLine("");
        }
        
        #endregion
	}
}
