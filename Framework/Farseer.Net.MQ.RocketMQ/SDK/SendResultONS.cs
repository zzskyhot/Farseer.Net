//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 3.0.10
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------

using System;
using System.Runtime.InteropServices;

namespace FS.MQ.RocketMQ.SDK
{
    public class SendResultONS : IDisposable
    {
        protected bool swigCMemOwn;
        private HandleRef swigCPtr;

        internal SendResultONS(IntPtr cPtr, bool cMemoryOwn)
        {
            swigCMemOwn = cMemoryOwn;
            swigCPtr = new HandleRef(this, cPtr);
        }

        public SendResultONS() : this(ONSClient4CPPPINVOKE.new_SendResultONS(), true)
        {
        }

        public virtual void Dispose()
        {
            lock (this)
            {
                if (swigCPtr.Handle != IntPtr.Zero)
                {
                    if (swigCMemOwn)
                    {
                        swigCMemOwn = false;
                        ONSClient4CPPPINVOKE.delete_SendResultONS(swigCPtr);
                    }
                    swigCPtr = new HandleRef(null, IntPtr.Zero);
                }
                GC.SuppressFinalize(this);
            }
        }

        internal static HandleRef getCPtr(SendResultONS obj)
        {
            return obj == null ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
        }

        ~SendResultONS()
        {
            Dispose();
        }

        public void setMessageId(string msgId)
        {
            ONSClient4CPPPINVOKE.SendResultONS_setMessageId(swigCPtr, msgId);
            if (ONSClient4CPPPINVOKE.SWIGPendingException.Pending)
                throw ONSClient4CPPPINVOKE.SWIGPendingException.Retrieve();
        }

        public string getMessageId()
        {
            var ret = ONSClient4CPPPINVOKE.SendResultONS_getMessageId(swigCPtr);
            return ret;
        }
    }
}