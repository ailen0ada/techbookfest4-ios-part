using System;
using System.Runtime.InteropServices;
using Foundation;
using ObjCRuntime;
namespace TechBookFest4
{
    public class ForwardDelegateProxy : NSObject
    {
        private NSObject _innerDelegate;

        public ForwardDelegateProxy(NSObject innerDelegate)
        {
            this._innerDelegate = innerDelegate;
        }

        public override bool RespondsToSelector(Selector sel)
        {
            return base.RespondsToSelector(sel) || this.InnerDelegateRespondsToSelector(sel);
        }

        [Export("forwardingTargetForSelector:")]
        public NSObject ForwardingTargetForSelector(Selector sel)
        {
            return this.InnerDelegateRespondsToSelector(sel) ? this._innerDelegate : null;
        }

        protected void PerformSelectorOn(string selector, NSObject argument)
        {
            var sel = new Selector(selector);
            if (this.InnerDelegateRespondsToSelector(sel))
            {
                //this._innerDelegate.PerformSelector(sel, argument, 0d);
                ObjcMsgSendWithAnArgument(this._innerDelegate.Handle, sel.Handle, argument.Handle);
            }
        }

        private bool InnerDelegateRespondsToSelector(Selector sel)
        {
            return this._innerDelegate?.RespondsToSelector(sel) ?? false;
        }

        [DllImport("/usr/lib/libobjc.dylib", EntryPoint = "objc_msgSend")]
        private static extern void ObjcMsgSendWithAnArgument(IntPtr receiver, IntPtr selector, IntPtr arg1);
    }
}
