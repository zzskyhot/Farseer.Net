﻿using System.Collections.Generic;

namespace FS.MQ.RabbitMQ
{
    /// <summary>
    ///     RocketMQ管理器
    /// </summary>
    public interface IRabbitManager
    {
        /// <summary>
        ///     生产普通消息
        /// </summary>
        IRabbitProduct Product { get; }

        /// <summary>
        ///     消费普通消息
        /// </summary>
        IRabbitConsumer Consumer { get; }

        /// <summary>
        /// 创建队列
        /// </summary>
        /// <param name="queueName">队列名称</param>
        /// <param name="durable">是否持久化（默认true）</param>
        /// <param name="exclusive">是否排他队列（默认false）</param>
        /// <param name="autoDelete">是否自动删除（默认false）</param>
        /// <param name="arguments">队列参数</param>
        void CreateQueue(string queueName, bool durable = true, bool exclusive = false, bool autoDelete = false, IDictionary<string, object> arguments = null);

        /// <summary>
        /// 创建队列（默认配置的队列名称）
        /// </summary>
        /// <param name="queueName">队列名称</param>
        /// <param name="durable">是否持久化（默认true）</param>
        /// <param name="exclusive">是否排他队列（默认false）</param>
        /// <param name="autoDelete">是否自动删除（默认false）</param>
        /// <param name="arguments">队列参数</param>
        void CreateQueue(bool durable = true, bool exclusive = false, bool autoDelete = false, IDictionary<string, object> arguments = null);
        
        /// <summary>
        /// 创建交换器，并绑定到队列
        /// </summary>
        /// <param name="exchangeName">交换器名称</param>
        /// <param name="exchangeType">交换器类型</param>
        /// <param name="queueName">队列名称</param>
        /// <param name="durable">是否持久化（默认true）</param>
        /// <param name="autoDelete">是否自动删除（默认false）</param>
        /// <param name="arguments">参数</param>
        /// <param name="routingKey">路由标签</param>
        void CreateExchange(string exchangeName, eumExchangeType exchangeType, string queueName, bool durable = true, bool autoDelete = false, IDictionary<string, object> arguments = null, string routingKey = "");
    }
}