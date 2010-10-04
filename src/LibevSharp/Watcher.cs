using System;
using System.Runtime.InteropServices;
namespace LibevSharp
{
	public abstract class Watcher
	{		
		private IntPtr unmanaged_callback_ptr;
		private UnmanagedWatcherCallback unmanaged_callback;

		
		internal Watcher (Loop loop)
		{
			Loop = loop;
			
			unmanaged_callback = new UnmanagedWatcherCallback (UnmanagedCallbackHandler);
			unmanaged_callback_ptr = Marshal.GetFunctionPointerForDelegate (unmanaged_callback);
		}
		
		public Loop Loop {
			get;
			private set;
		}
		
		internal IntPtr CallbackFunctionPtr {
			get { return unmanaged_callback_ptr; }
		}
		
		public abstract void Start ();

		public abstract void Stop ();
		
		protected abstract void UnmanagedCallbackHandler (IntPtr loop, IntPtr watcher, int revents);
	}
}

