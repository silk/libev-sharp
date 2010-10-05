using System;

using LibevSharp;
using System.Net;
using System.Net.Sockets;

namespace LibevSharpDemo
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			IPAddress ip_address = IPAddress.Parse ("0.0.0.0");
			IPEndPoint endpoint = new IPEndPoint (ip_address, 8080);
			
			Socket s = new Socket (AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			s.SetSocketOption (SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
			s.Blocking = false;
			s.Bind (endpoint);
			s.Listen (128);
			
			Loop loop = Loop.CreateDefaultLoop ();
			IOWatcher io = new IOWatcher (s.Handle, EventTypes.Read, loop, HandleIO);
			TimerWatcher timer = new TimerWatcher (TimeSpan.FromSeconds (5.5), TimeSpan.FromSeconds (5.5), loop, HandleTimer);
			PrepareWatcher prepare = new PrepareWatcher (loop, HandlePrepare);
			CheckWatcher check = new CheckWatcher (loop, HandleCheck);
			
			io.Start ();
			timer.Start ();
			prepare.Start ();
			check.Start ();
			
			loop.RunBlocking ();
		}
		
		private static void HandleIO (Loop loop, IOWatcher watcher, int revents)
		{
			Console.WriteLine ("GOT SOME IO!");
			watcher.Stop ();
			
			loop.Unloop (UnloopType.All);
		}
		
		private static void HandleTimer (Loop loop, TimerWatcher watcher, int revents)
		{
			Console.WriteLine ("handling the timer!");	
		}
			
		private static void HandlePrepare (Loop loop, PrepareWatcher watcher, int revents)
		{
			Console.WriteLine ("HanDLING PREPARE");	
		}
		
		private static void HandleCheck (Loop loop, CheckWatcher watcher, int revents)
		{
			Console.WriteLine ("HanDLING CHECK");	
		}
	}
}

