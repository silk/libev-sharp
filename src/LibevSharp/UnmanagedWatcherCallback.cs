using System;


namespace LibevSharp
{
	internal delegate void UnmanagedWatcherCallback (IntPtr loop, IntPtr watcher, int revents);
}

