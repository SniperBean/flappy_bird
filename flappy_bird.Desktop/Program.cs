#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
#endregion

namespace flappy_bird
{
	public static class Program
	{
		static void Main (string [] args)
		{
			using (var game = new Game1 ()) {
				game.Run ();
			}
		}
	}
}
