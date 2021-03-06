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
    public class Consumer : IDisposable
    {
        public delegate void SwigDelegatePushConsumer_0();

        public delegate void SwigDelegatePushConsumer_1();

        public delegate void SwigDelegatePushConsumer_2(string topic, string subExpression, IntPtr listener);

        private static readonly Type[] swigMethodTypes0 = { };
        private static readonly Type[] swigMethodTypes1 = { };
        private static readonly Type[] swigMethodTypes2 = {typeof(string), typeof(string), typeof(MessageListener)};
        protected bool swigCMemOwn;
        private HandleRef swigCPtr;

        private SwigDelegatePushConsumer_0 swigDelegate0;
        private SwigDelegatePushConsumer_1 swigDelegate1;
        private SwigDelegatePushConsumer_2 swigDelegate2;

        internal Consumer(IntPtr cPtr, bool cMemoryOwn)
        {
            swigCMemOwn = cMemoryOwn;
            swigCPtr = new HandleRef(this, cPtr);
        }

        public Consumer() : this(ONSClient4CPPPINVOKE.new_PushConsumer(), true)
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
                        ONSClient4CPPPINVOKE.delete_PushConsumer(swigCPtr);
                    }
                    swigCPtr = new HandleRef(null, IntPtr.Zero);
                }
                GC.SuppressFinalize(this);
            }
        }

        internal static HandleRef getCPtr(Consumer obj)
        {
            return obj == null ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
        }

        ~Consumer()
        {
            Dispose();
        }

        public virtual void start()
        {
            ONSClient4CPPPINVOKE.PushConsumer_start(swigCPtr);
        }

        public virtual void shutdown()
        {
            ONSClient4CPPPINVOKE.PushConsumer_shutdown(swigCPtr);
        }

        public virtual void subscribe(string topic, string subExpression, MessageListener listener)
        {
            ONSClient4CPPPINVOKE.PushConsumer_subscribe(swigCPtr, topic, subExpression,
                MessageListener.getCPtr(listener));
        }

        private void SwigDirectorConnect()
        {
            if (SwigDerivedClassHasMethod("start", swigMethodTypes0))
                swigDelegate0 = SwigDirectorstart;
            if (SwigDerivedClassHasMethod("shutdown", swigMethodTypes1))
                swigDelegate1 = SwigDirectorshutdown;
            if (SwigDerivedClassHasMethod("subscribe", swigMethodTypes2))
                swigDelegate2 = SwigDirectorsubscribe;
            ONSClient4CPPPINVOKE.PushConsumer_director_connect(swigCPtr, swigDelegate0, swigDelegate1, swigDelegate2);
        }

        private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
        {
            var methodInfo = GetType().GetMethod(methodName,
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, null, methodTypes, null);
            var hasDerivedMethod = methodInfo.DeclaringType.IsSubclassOf(typeof(Consumer));
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

        private void SwigDirectorsubscribe(string topic, string subExpression, IntPtr listener)
        {
            subscribe(topic, subExpression, listener == IntPtr.Zero ? null : new MessageListener(listener, false));
        }
    }
}