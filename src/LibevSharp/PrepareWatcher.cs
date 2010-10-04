using System;
using System.Runtime.InteropServices;
namespace LibevSharp
{
	public class PrepareWatcher : Watcher
	{
		private PrepareWatcherCallback callback;
		
		private UnmanagedPrepareWatcher unmanaged_watcher;
		
		public PrepareWatcher (Loop loop, PrepareWatcherCallback callback) : base (loop)
		{ 
			this.callback = callback;
			
			unmanaged_watcher = new UnmanagedPrepareWatcher ();
			unmanaged_watcher.callback = CallbackFunctionPtr;
		}
	
		public override void Start ()
		{			
			ev_prepare_start (Loop.Handle, ref unmanaged_watcher);
		}
		
		public override void Stop ()
		{			
			ev_prepare_stop (Loop.Handle, ref unmanaged_watcher);	
		}
		
		protected override void UnmanagedCallbackHandler (IntPtr _loop, IntPtr _watcher, int revents)
		{
			// Maybe I should verify the pointers?
			
			callback (Loop, this, revents);
		}
		
		[DllImport ("libev")]
		private static extern void ev_prepare_start (IntPtr loop, ref UnmanagedPrepareWatcher watcher);
		
		[DllImport ("libev")]
		private static extern void ev_prepare_stop (IntPtr loop, ref UnmanagedPrepareWatcher watcher);
	}
	
	public delegate void PrepareWatcherCallback (Loop loop, PrepareWatcher watcher, int revents);
	
	[StructLayout (LayoutKind.Sequential)]
	internal struct UnmanagedPrepareWatcher {
		
		public int active;
		public int pending;
		public int priority;
		
		public IntPtr data;
		public IntPtr callback;
		
		internal IntPtr next;
	}
}

