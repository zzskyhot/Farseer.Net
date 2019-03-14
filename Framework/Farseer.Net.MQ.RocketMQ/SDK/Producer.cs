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
using System.Reflection;
using System.Runtime.InteropServices;

namespace FS.MQ.RocketMQ.SDK
{
    public class Producer : IDisposable
    {
        public delegate void SwigDelegateProducer_0();

        public delegate void SwigDelegateProducer_1();

        public delegate IntPtr SwigDelegateProducer_2(IntPtr msg);

        public delegate IntPtr SwigDelegateProducer_3(IntPtr msg, IntPtr mq);

        public delegate void SwigDelegateProducer_4(IntPtr msg);

        private static readonly Type[] swigMethodTypes0 = { };
        private static readonly Type[] swigMethodTypes1 = { };
        private static readonly Type[] swigMethodTypes2 = {typeof(Message)};
        private static readonly Type[] swigMethodTypes3 = {typeof(Message), typeof(MessageQueueONS)};
        private static readonly Type[] swigMethodTypes4 = {typeof(Message)};
        protected bool swigCMemOwn;
        private HandleRef swigCPtr;

        private SwigDelegateProducer_0 swigDelegate0;
        private SwigDelegateProducer_1 swigDelegate1;
        private SwigDelegateProducer_2 swigDelegate2;
        private SwigDelegateProducer_3 swigDelegate3;
        private SwigDelegateProducer_4 swigDelegate4;

        internal Producer(IntPtr cPtr, bool cMemoryOwn)
        {
            swigCMemOwn = cMemoryOwn;
            swigCPtr = new HandleRef(this, cPtr);
        }

        public Producer() : this(ONSClient4CPPPINVOKE.new_Producer(), true)
        {
            SwigDirectorConnect();
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
                        ONSClient4CPPPINVOKE.delete_Producer(swigCPtr);
                    }
                    swigCPtr = new HandleRef(null, IntPtr.Zero);
                }
                GC.SuppressFinalize(this);
            }
        }

        internal static HandleRef getCPtr(Producer obj)
        {
            return obj == null ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
        }

        ~Producer()
        {
            Dispose();
        }

        public virtual void start()
        {
            ONSClient4CPPPINVOKE.Producer_start(swigCPtr);
        }

        public virtual void shutdown()
        {
            ONSClient4CPPPINVOKE.Producer_shutdown(swigCPtr);
        }

        public virtual SendResultONS send(Message msg)
        {
            var ret = new SendResultONS(ONSClient4CPPPINVOKE.Producer_send__SWIG_0(swigCPtr, Message.getCPtr(msg)),
                true);
            if (ONSClient4CPPPINVOKE.SWIGPendingException.Pending)
                throw ONSClient4CPPPINVOKE.SWIGPendingException.Retrieve();
            return ret;
        }

        public virtual SendResultONS send(Message msg, MessageQueueONS mq)
        {
            var ret = new SendResultONS(
                ONSClient4CPPPINVOKE.Producer_send__SWIG_1(swigCPtr, Message.getCPtr(msg), MessageQueueONS.getCPtr(mq)),
                true);
            if (ONSClient4CPPPINVOKE.SWIGPendingException.Pending)
                throw ONSClient4CPPPINVOKE.SWIGPendingException.Retrieve();
            return ret;
        }

        public virtual void sendOneway(Message msg)
        {
            ONSClient4CPPPINVOKE.Producer_sendOneway(swigCPtr, Message.getCPtr(msg));
            if (ONSClient4CPPPINVOKE.SWIGPendingException.Pending)
                throw ONSClient4CPPPINVOKE.SWIGPendingException.Retrieve();
        }

        private void SwigDirectorConnect()
        {
            if (SwigDerivedClassHasMethod("start", swigMethodTypes0))
                swigDelegate0 = SwigDirectorstart;
            if (SwigDerivedClassHasMethod("shutdown", swigMethodTypes1))
                swigDelegate1 = SwigDirectorshutdown;
            if (SwigDerivedClassHasMethod("send", swigMethodTypes2))
                swigDelegate2 = SwigDirectorsend__SWIG_0;
            if (SwigDerivedClassHasMethod("send", swigMethodTypes3))
                swigDelegate3 = SwigDirectorsend__SWIG_1;
            if (SwigDerivedClassHasMethod("sendOneway", swigMethodTypes4))
                swigDelegate4 = SwigDirectorsendOneway;
            ONSClient4CPPPINVOKE.Producer_director_connect(swigCPtr, swigDelegate0, swigDelegate1, swigDelegate2,
                swigDelegate3, swigDelegate4);
        }

        private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
        {
            var methodInfo = GetType().GetMethod(methodName,
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, null, methodTypes, null);
            var hasDerivedMethod = methodInfo.DeclaringType.IsSubclassOf(typeof(Producer));
            return hasDerivedMethod;
        }

        private void SwigDirectorstart()
        {
            start();
        }

        private void SwigDirectorshutdown()
        {
            shutdown();
        }

        private IntPtr SwigDirectorsend__SWIG_0(IntPtr msg)
        {
            return SendResultONS.getCPtr(send(new Message(msg, false))).Handle;
        }

        private IntPtr SwigDirectorsend__SWIG_1(IntPtr msg, IntPtr mq)
        {
            return SendResultONS.getCPtr(send(new Message(msg, false), new MessageQueueONS(mq, false))).Handle;
        }

        private void SwigDirectorsendOneway(IntPtr msg)
        {
            sendOneway(new Message(msg, false));
        }
    }
}