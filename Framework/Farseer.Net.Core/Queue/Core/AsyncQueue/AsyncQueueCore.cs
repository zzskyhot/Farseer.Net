﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FS.Core.Queue.Core.AsyncQueue
{
    /// <summary>
    /// 异步队列的核心,完成入队和出队工作
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="E"></typeparam>
    internal class AsyncQueueCore<T,E>
    {
        private readonly object _locker = new object();
        /// <summary>出队信号</summary>
        private DequeueSignal _dequeueSignal;

        private readonly ConcurrentQueue<AsyncQueueData<T,E>> _concurrentQueue = null;
        /// <summary>出队列任务</summary>
        private Task _dequeueTask=null;

        /// <summary>出队的数据</summary>
        private readonly List<AsyncQueueData<T,E>> _dequeueDataList;

        /// <summary>出队数据处理器</summary>
        private readonly Func<List<AsyncQueueData<T, E>>,int, bool> _onDataDequeueHandlers;

        /// <summary>出队时间间隔</summary>
        private readonly TimeSpan _notifyDequeueTimeSpan;

        /// <summary>队列中数据数量达到此值,则立即出队</summary>
        private readonly int _notifyDequeueSize;

        /// <summary>队列容量</summary>
        private readonly int _queueCapacity;


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="onDataDequeueHandlers">数据处理</param>
        /// <param name="queueCapacity">队列最大容量</param>
        /// <param name="notifyDequeueTimeSpan">在为满足出队的长度时,达到此时间间隔就出队一次</param>
        /// <param name="notifyDequeueSize">已入队数据达到多少,出队一次</param>
        public AsyncQueueCore(Func<List<AsyncQueueData<T, E>>,int, bool> onDataDequeueHandlers,  int queueCapacity, TimeSpan notifyDequeueTimeSpan, int notifyDequeueSize)
        {
            _concurrentQueue = new ConcurrentQueue<AsyncQueueData<T,E>>();
            _onDataDequeueHandlers = onDataDequeueHandlers;
            _dequeueDataList = new List<AsyncQueueData<T,E>>(notifyDequeueSize);
            _notifyDequeueSize = notifyDequeueSize;
            _notifyDequeueTimeSpan = notifyDequeueTimeSpan;
            _queueCapacity = queueCapacity;
        }

        public int QueueCount => _concurrentQueue.Count;
        /// <summary>
        /// 入队,超出队列容量则不入队
        /// </summary>
        /// <param name="queueData"></param>
        public bool Enqueue(AsyncQueueData<T,E> queueData)
        {
            if (QueueCount > _queueCapacity) return false;
            _concurrentQueue.Enqueue(queueData);
            if(QueueCount >= this._notifyDequeueSize )
                _dequeueSignal.SendSignal();
            return true;
        }
        /// <summary>
        /// 只能启动一次
        /// </summary>
        /// <param name="cancellationTokentoken"></param>
        public void StartDequeue(CancellationToken cancellationTokentoken)
        {
            lock (_locker)
            {
                if (_dequeueTask != null) return;

                _dequeueSignal = new DequeueSignal(this._notifyDequeueTimeSpan, cancellationTokentoken);
                _dequeueTask = new Task(() => LoopDequeue(cancellationTokentoken), TaskCreationOptions.LongRunning);
                _dequeueTask.Start();
            }
        }
        /// <summary>
        ///     循环从队列中拉出数据,回调给用户处理
        /// </summary>
        /// <param name="token">取消令牌</param>
        private void LoopDequeue(CancellationToken token)
        {
            while (true)
            {
                if(QueueCount < _notifyDequeueSize)
                    _dequeueSignal.WaitSignal();
                DeQueue(); //队列数据出队保存到回调数据列表
                HandleDequeueData(); //处理出队的数据
            }
        }

        /// <summary>
        /// 回调 数据给用户处理
        /// </summary>
        protected void HandleDequeueData()
        {
            if (_dequeueDataList.Count <= 0) return;
            this._onDataDequeueHandlers.Invoke(_dequeueDataList,QueueCount);
        }
        /// <summary>
        ///     从队列中拉出指定数量的数据,放到回调数据队列中,直到队列为空,或者回调队列满
        /// </summary>
        /// <returns></returns>
        private void DeQueue()
        {
            var maxSize = _dequeueDataList.Capacity;
            _dequeueDataList.Clear();
            for (var i = 0; i < maxSize; i++)
            {
                if (_concurrentQueue.TryDequeue(out AsyncQueueData<T,E> tempObj))
                {
                    _dequeueDataList.Add(tempObj);
                }
                else
                {
                    break;
                }
            }
        }

    }
}
