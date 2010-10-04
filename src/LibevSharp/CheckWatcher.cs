using System;
using System.Runtime.InteropServices;
namespace LibevSharp
{
	public class CheckWatcher : Watcher
	{
		private CheckWatcherCallback callback;
		
		private UnmanagedCheckWatcher unmanaged_watcher;
		
		public CheckWatcher (Loop loop, CheckWatcherCallback callback) : base (loop)
		{ 
			this.callback = callback;
			
			unmanaged_watcher = new UnmanagedCheckWatcher ();
			unmanaged_watcher.callback = CallbackFunctionPtr;
		}
	
		public override void Start ()
		{			
			ev_check_start (Loop.Handle, ref unmanaged_watcher);
		}
		
		public override void Stop ()
		{			
			ev_check_stop (Loop.Handle, ref unmanaged_watcher);	
		}
		
		protected override void UnmanagedCallbackHandler (IntPtr _loop, IntPtr _watcher, int revents)
		{
			// Maybe I should verify the pointers?
			
			callback (Loop, this, revents);
		}
		
		[DllImport ("libev")]
		private static extern void ev_check_start (IntPtr loop, ref UnmanagedCheckWatcher watcher);
		
		[DllImport ("libev")]
		private static extern void ev_check_stop (IntPtr loop, ref UnmanagedCheckWatcher watcher);
	}
	
	public delegate void CheckWatcherCallback (Loop loop, CheckWatcher watcher, int revents);
	
	[StructLayout (LayoutKind.Sequential)]
	internal struct UnmanagedCheckWatcher {
		
		public int active;
		public int pending;
		public int priority;
		
		public IntPtr data;
		public IntPtr callback;
		
		internal IntPtr next;
	}
}

