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
    public class PullConsumer : IDisposable
    {
        public delegate void SwigDelegatePullConsumer_0();

        public delegate void SwigDelegatePullConsumer_1();

        public delegate void SwigDelegatePullConsumer_10(IntPtr mq);

        public delegate void SwigDelegatePullConsumer_2(string topic, IntPtr mqs);

        public delegate IntPtr SwigDelegatePullConsumer_3(IntPtr mq, string subExpression, long offset, int maxNums);

        public delegate long SwigDelegatePullConsumer_4(IntPtr mq, long timestamp);

        public delegate long SwigDelegatePullConsumer_5(IntPtr mq);

        public delegate long SwigDelegatePullConsumer_6(IntPtr mq);

        public delegate void SwigDelegatePullConsumer_7(IntPtr mq, long offset);

        public delegate void SwigDelegatePullConsumer_8(IntPtr mq);

        public delegate long SwigDelegatePullConsumer_9(IntPtr mq, bool fromStore);

        private static readonly Type[] swigMethodTypes0 = { };
        private static readonly Type[] swigMethodTypes1 = { };

        private static readonly Type[] swigMethodTypes2 =
            {typeof(string), typeof(SWIGTYPE_p_std__vectorT_ons__MessageQueueONS_t)};

        private static readonly Type[] swigMethodTypes3 =
            {typeof(MessageQueueONS), typeof(string), typeof(long), typeof(int)};

        private static readonly Type[] swigMethodTypes4 = {typeof(MessageQueueONS), typeof(long)};
        private static readonly Type[] swigMethodTypes5 = {typeof(MessageQueueONS)};
        private static readonly Type[] swigMethodTypes6 = {typeof(MessageQueueONS)};
        private static readonly Type[] swigMethodTypes7 = {typeof(MessageQueueONS), typeof(long)};
        private static readonly Type[] swigMethodTypes8 = {typeof(MessageQueueONS)};
        private static readonly Type[] swigMethodTypes9 = {typeof(MessageQueueONS), typeof(bool)};
        private static readonly Type[] swigMethodTypes10 = {typeof(MessageQueueONS)};
        protected bool swigCMemOwn;
        private HandleRef swigCPtr;

        private SwigDelegatePullConsumer_0 swigDelegate0;
        private SwigDelegatePullConsumer_1 swigDelegate1;
        private SwigDelegatePullConsumer_10 swigDelegate10;
        private SwigDelegatePullConsumer_2 swigDelegate2;
        private SwigDelegatePullConsumer_3 swigDelegate3;
        private SwigDelegatePullConsumer_4 swigDelegate4;
        private SwigDelegatePullConsumer_5 swigDelegate5;
        private SwigDelegatePullConsumer_6 swigDelegate6;
        private SwigDelegatePullConsumer_7 swigDelegate7;
        private SwigDelegatePullConsumer_8 swigDelegate8;
        private SwigDelegatePullConsumer_9 swigDelegate9;

        internal PullConsumer(IntPtr cPtr, bool cMemoryOwn)
        {
            swigCMemOwn = cMemoryOwn;
            swigCPtr = new HandleRef(this, cPtr);
        }

        public PullConsumer() : this(ONSClient4CPPPINVOKE.new_PullConsumer(), true)
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
                        ONSClient4CPPPINVOKE.delete_PullConsumer(swigCPtr);
                    }
                    swigCPtr = new HandleRef(null, IntPtr.Zero);
                }
                GC.SuppressFinalize(this);
            }
        }

        internal static HandleRef getCPtr(PullConsumer obj)
        {
            return obj == null ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
        }

        ~PullConsumer()
        {
            Dispose();
        }

        public virtual void start()
        {
            ONSClient4CPPPINVOKE.PullConsumer_start(swigCPtr);
        }

        public virtual void shutdown()
        {
            ONSClient4CPPPINVOKE.PullConsumer_shutdown(swigCPtr);
        }

        public virtual void fetchSubscribeMessageQueues(string topic,
            SWIGTYPE_p_std__vectorT_ons__MessageQueueONS_t mqs)
        {
            ONSClient4CPPPINVOKE.PullConsumer_fetchSubscribeMessageQueues(swigCPtr, topic,
                SWIGTYPE_p_std__vectorT_ons__MessageQueueONS_t.getCPtr(mqs));
            if (ONSClient4CPPPINVOKE.SWIGPendingException.Pending)
                throw ONSClient4CPPPINVOKE.SWIGPendingException.Retrieve();
        }

        public virtual PullResultONS pull(MessageQueueONS mq, string subExpression, long offset, int maxNums)
        {
            var ret = new PullResultONS(
                ONSClient4CPPPINVOKE.PullConsumer_pull(swigCPtr, MessageQueueONS.getCPtr(mq), subExpression, offset,
                    maxNums), true);
            if (ONSClient4CPPPINVOKE.SWIGPendingException.Pending)
                throw ONSClient4CPPPINVOKE.SWIGPendingException.Retrieve();
            return ret;
        }

        public virtual long searchOffset(MessageQueueONS mq, long timestamp)
        {
            var ret = ONSClient4CPPPINVOKE.PullConsumer_searchOffset(swigCPtr, MessageQueueONS.getCPtr(mq), timestamp);
            if (ONSClient4CPPPINVOKE.SWIGPendingException.Pending)
                throw ONSClient4CPPPINVOKE.SWIGPendingException.Retrieve();
            return ret;
        }

        public virtual long maxOffset(MessageQueueONS mq)
        {
            var ret = ONSClient4CPPPINVOKE.PullConsumer_maxOffset(swigCPtr, MessageQueueONS.getCPtr(mq));
            if (ONSClient4CPPPINVOKE.SWIGPendingException.Pending)
                throw ONSClient4CPPPINVOKE.SWIGPendingException.Retrieve();
            return ret;
        }

        public virtual long minOffset(MessageQueueONS mq)
        {
            var ret = ONSClient4CPPPINVOKE.PullConsumer_minOffset(swigCPtr, MessageQueueONS.getCPtr(mq));
            if (ONSClient4CPPPINVOKE.SWIGPendingException.Pending)
                throw ONSClient4CPPPINVOKE.SWIGPendingException.Retrieve();
            return ret;
        }

        public virtual void updateConsumeOffset(MessageQueueONS mq, long offset)
        {
            ONSClient4CPPPINVOKE.PullConsumer_updateConsumeOffset(swigCPtr, MessageQueueONS.getCPtr(mq), offset);
            if (ONSClient4CPPPINVOKE.SWIGPendingException.Pending)
                throw ONSClient4CPPPINVOKE.SWIGPendingException.Retrieve();
        }

        public virtual void removeConsumeOffset(MessageQueueONS mq)
        {
            ONSClient4CPPPINVOKE.PullConsumer_removeConsumeOffset(swigCPtr, MessageQueueONS.getCPtr(mq));
            if (ONSClient4CPPPINVOKE.SWIGPendingException.Pending)
                throw ONSClient4CPPPINVOKE.SWIGPendingException.Retrieve();
        }

        public virtual long fetchConsumeOffset(MessageQueueONS mq, bool fromStore)
        {
            var ret = ONSClient4CPPPINVOKE.PullConsumer_fetchConsumeOffset(swigCPtr, MessageQueueONS.getCPtr(mq),
                fromStore);
            if (ONSClient4CPPPINVOKE.SWIGPendingException.Pending)
                throw ONSClient4CPPPINVOKE.SWIGPendingException.Retrieve();
            return ret;
        }

        public virtual void persistConsumerOffset4PullConsumer(MessageQueueONS mq)
        {
            ONSClient4CPPPINVOKE.PullConsumer_persistConsumerOffset4PullConsumer(swigCPtr, MessageQueueONS.getCPtr(mq));
            if (ONSClient4CPPPINVOKE.SWIGPendingException.Pending)
                throw ONSClient4CPPPINVOKE.SWIGPendingException.Retrieve();
        }

        private void SwigDirectorConnect()
        {
            if (SwigDerivedClassHasMethod("start", swigMethodTypes0))
                swigDelegate0 = SwigDirectorstart;
            if (SwigDerivedClassHasMethod("shutdown", swigMethodTypes1))
                swigDelegate1 = SwigDirectorshutdown;
            if (SwigDerivedClassHasMethod("fetchSubscribeMessageQueues", swigMethodTypes2))
                swigDelegate2 = SwigDirectorfetchSubscribeMessageQueues;
            if (SwigDerivedClassHasMethod("pull", swigMethodTypes3))
                swigDelegate3 = SwigDirectorpull;
            if (SwigDerivedClassHasMethod("searchOffset", swigMethodTypes4))
                swigDelegate4 = SwigDirectorsearchOffset;
            if (SwigDerivedClassHasMethod("maxOffset", swigMethodTypes5))
                swigDelegate5 = SwigDirectormaxOffset;
            if (SwigDerivedClassHasMethod("minOffset", swigMethodTypes6))
                swigDelegate6 = SwigDirectorminOffset;
            if (SwigDerivedClassHasMethod("updateConsumeOffset", swigMethodTypes7))
                swigDelegate7 = SwigDirectorupdateConsumeOffset;
            if (SwigDerivedClassHasMethod("removeConsumeOffset", swigMethodTypes8))
                swigDelegate8 = SwigDirectorremoveConsumeOffset;
            if (SwigDerivedClassHasMethod("fetchConsumeOffset", swigMethodTypes9))
                swigDelegate9 = SwigDirectorfetchConsumeOffset;
            if (SwigDerivedClassHasMethod("persistConsumerOffset4PullConsumer", swigMethodTypes10))
                swigDelegate10 = SwigDirectorpersistConsumerOffset4PullConsumer;
            ONSClient4CPPPINVOKE.PullConsumer_director_connect(swigCPtr, swigDelegate0, swigDelegate1, swigDelegate2,
                swigDelegate3, swigDelegate4, swigDelegate5, swigDelegate6, swigDelegate7, swigDelegate8, swigDelegate9,
                swigDelegate10);
        }

        private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
        {
            var methodInfo = GetType().GetMethod(methodName,
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, null, methodTypes, null);
            var hasDerivedMethod = methodInfo.DeclaringType.IsSubclassOf(typeof(PullConsumer));
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

        private void SwigDirectorfetchSubscribeMessageQueues(string topic, IntPtr mqs)
        {
            fetchSubscribeMessageQueues(topic, new SWIGTYPE_p_std__vectorT_ons__MessageQueueONS_t(mqs, false));
        }

        private IntPtr SwigDirectorpull(IntPtr mq, string subExpression, long offset, int maxNums)
        {
            return PullResultONS.getCPtr(pull(new MessageQueueONS(mq, false), subExpression, offset, maxNums)).Handle;
        }

        private long SwigDirectorsearchOffset(IntPtr mq, long timestamp)
        {
            return searchOffset(new MessageQueueONS(mq, false), timestamp);
        }

        private long SwigDirectormaxOffset(IntPtr mq)
        {
            return maxOffset(new MessageQueueONS(mq, false));
        }

        private long SwigDirectorminOffset(IntPtr mq)
        {
            return minOffset(new MessageQueueONS(mq, false));
        }

        private void SwigDirectorupdateConsumeOffset(IntPtr mq, long offset)
        {
            updateConsumeOffset(new MessageQueueONS(mq, false), offset);
        }

        private void SwigDirectorremoveConsumeOffset(IntPtr mq)
        {
            removeConsumeOffset(new MessageQueueONS(mq, false));
        }

        private long SwigDirectorfetchConsumeOffset(IntPtr mq, bool fromStore)
        {
            return fetchConsumeOffset(new MessageQueueONS(mq, false), fromStore);
        }

        private void SwigDirectorpersistConsumerOffset4PullConsumer(IntPtr mq)
        {
            persistConsumerOffset4PullConsumer(new MessageQueueONS(mq, false));
        }
    }
}