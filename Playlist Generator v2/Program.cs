using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Playlist_Generator_v2
{
	static class Program
	{
		/// <summary>
		/// Point d'entrée principal de l'application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			Application.Run(new Form1(args));
		}
	}
}
