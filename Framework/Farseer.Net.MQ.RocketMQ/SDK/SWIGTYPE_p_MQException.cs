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
    public class SWIGTYPE_p_MQException
    {
        private readonly HandleRef swigCPtr;

        internal SWIGTYPE_p_MQException(IntPtr cPtr, bool futureUse)
        {
            swigCPtr = new HandleRef(this, cPtr);
        }

        protected SWIGTYPE_p_MQException()
        {
            swigCPtr = new HandleRef(null, IntPtr.Zero);
        }

        internal static HandleRef getCPtr(SWIGTYPE_p_MQException obj)
        {
            return obj == null ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
        }
    }
}