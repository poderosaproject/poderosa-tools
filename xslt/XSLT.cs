using System;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace xslt
{
	/// <summary>
	/// Class1 の概要の説明です。
	/// </summary>
	class XSLTCommand
	{
		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			int step = 0;
			try {
				string xslt_file = null;
				string src_file = null;
				string out_file = null;
				if(args.Length==0) {
					Console.Error.WriteLine("usage [-Dname=param]... -s stylesheet -i input -o outout");
					return;
				}

				XsltArgumentList al = new XsltArgumentList();
				for(int i=0; i<args.Length; i++) {
					if(args[i]=="-s")
						xslt_file = args[++i];
					else if(args[i]=="-i")
						src_file = args[++i];
					else if(args[i]=="-o")
						out_file = args[++i];
					else if(args[i].StartsWith("-D")) {
						int e = args[i].IndexOf('=');
						string name =args[i].Substring(2, e-2);
						string value = args[i].Substring(e+1);
						al.AddParam(name, "", value);
					}
					else if(args[i].StartsWith("-"))
						throw new ApplicationException("unknown option "+args[i]);
					else
						throw new ApplicationException("source file is specified twice!");
				}


				//Create the XslTransform.
				XslTransform xslt = new XslTransform();

				step = 1;
				//Load the stylesheet.
				xslt.Load(xslt_file);

				step = 2;
				//Load the XML data file.
				XPathDocument doc = new XPathDocument(src_file);

				step = 3;
				//Transform the file.
				FileStream strm = new FileStream(out_file, FileMode.Create, FileAccess.Write);
				XmlTextWriter writer = new XmlTextWriter(strm, Encoding.UTF8);
				writer.Formatting = Formatting.Indented;
				xslt.Transform(doc, al, writer, null);
				strm.Close();
			}
			catch(Exception ex) {
				if(step==0)
					Console.Error.WriteLine("[Init]");
				else if(step==1)
					Console.Error.WriteLine("[XSLT load]");
				else if(step==2)
					Console.Error.WriteLine("[Souce document load]");
				else if(step==3)
					Console.Error.WriteLine("[Transform]");
				
				Console.Error.WriteLine(ex.Message);
				Console.Error.WriteLine(ex.StackTrace);
			}
		}
	}
}
