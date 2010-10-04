using System;
using System.Runtime.InteropServices;
namespace LibevSharp
{
	public class TimerWatcher : Watcher
	{
		private TimerWatcherCallback callback;
		
		private UnmanagedTimerWatcher unmanaged_watcher;
		
		public TimerWatcher (TimeSpan repeat, Loop loop, TimerWatcherCallback callback) : this (TimeSpan.Zero, repeat, loop, callback)
		{
		}
		
		public TimerWatcher (TimeSpan after, TimeSpan repeat, Loop loop, TimerWatcherCallback callback) : base (loop)
		{	
			this.callback = callback;
			
			unmanaged_watcher = new UnmanagedTimerWatcher ();
			
			unmanaged_watcher.callback = CallbackFunctionPtr;
			unmanaged_watcher.after = after.TotalSeconds;
			unmanaged_watcher.repeat = after.TotalSeconds;
			
			Console.WriteLine ("SIZE OF UNMANAGED TIMER:  '{0}'", Marshal.SizeOf (typeof (UnmanagedTimerWatcher)));
		}
		
		public TimeSpan Repeat {
			get {
				return TimeSpan.FromSeconds (unmanaged_watcher.repeat);	
			}
			set {
				unmanaged_watcher.repeat = value.TotalSeconds;	
			}
		}
		
		public override void Start ()
		{
			ev_timer_start (Loop.Handle, ref unmanaged_watcher);
		}
		
		public override void Stop ()
		{
			ev_timer_stop (Loop.Handle, ref unmanaged_watcher);
		}
		
		protected override void UnmanagedCallbackHandler (IntPtr loop, IntPtr watcher, int revents)
		{
			callback (Loop, this, revents);
		}
		
		[DllImport ("libev")]
		private static extern void ev_timer_start (IntPtr loop, ref UnmanagedTimerWatcher watcher);
		
		[DllImport ("libev")]
		private static extern void ev_timer_stop (IntPtr loop, ref UnmanagedTimerWatcher watcher);
	}
	
	public delegate void TimerWatcherCallback (Loop loop, TimerWatcher watcher, int revents);
	
	[StructLayout (LayoutKind.Sequential)]
	internal struct UnmanagedTimerWatcher {
		
		public int active;
		public int pending;
		public int priority;
		
		public IntPtr data;
		public IntPtr callback;
				
		public double after;
		public double repeat;
	}
	
	
}

