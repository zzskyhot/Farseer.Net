﻿// ********************************************
// 作者：何达贤（steden） QQ：11042427
// 时间：2017-03-23 22:50
// ********************************************

using System;
using System.Collections.Generic;
using FS.Configuration;
using FS.MQ.RabbitMQ.Configuration;
using RabbitMQ.Client;

namespace FS.MQ.RabbitMQ
{
    /// <summary>
    ///     Rabbit管理器
    /// </summary>
    public class RabbitManager : IRabbitManager
    {
        private static readonly object ObjLock = new object();

        /// <summary>
        ///     生产消息
        /// </summary>
        private IRabbitProduct _product;

        /// <summary>
        ///     消费消息
        /// </summary>
        private IRabbitConsumer _consumer;

        /// <summary>
        /// 配置信息
        /// </summary>
        private readonly ProductConfig _productConfig;

        /// <summary>
        /// 配置信息
        /// </summary>
        private readonly ConsumerConfig _consumerConfig;

        /// <summary>
        /// Rabbit连接
        /// </summary>
        private readonly RabbitConnect _connect;

        /// <summary> Rabbit管理器 </summary>
        public RabbitManager(RabbitConnect connect, ProductConfig productConfig)
        {
            _connect       = connect;
            _productConfig = productConfig;
        }

        /// <summary> Rabbit管理器 </summary>
        public RabbitManager(RabbitConnect connect, ConsumerConfig consumerConfig)
        {
            _connect        = connect;
            _consumerConfig = consumerConfig;
        }

        /// <summary>
        ///     生产普通消息
        /// </summary>
        public IRabbitProduct Product
        {
            get
            {
                //if (_productConfig == null) throw new Exception("当前配置的是消费端的配置，你只可以使用Consumer属性");
                if (_product != null) return _product;
                lock (ObjLock)
                {
                    return _product ?? (_product = new RabbitProduct(_connect, _productConfig));
                }
            }
        }

        /// <summary>
        ///     消费普通消息
        /// </summary>
        public IRabbitConsumer Consumer
        {
            get
            {
                //if (_consumerConfig == null) throw new Exception("当前配置的是生产端的配置，你只可以使用Product属性");
                if (_consumer != null) return _consumer;
                lock (ObjLock)
                {
                    return _consumer ?? (_consumer = new RabbitConsumer(_connect, _consumerConfig));
                }
            }
        }

        /// <summary>
        /// 创建队列
        /// </summary>
        /// <param name="queueName">队列名称</param>
        /// <param name="durable">是否持久化（默认true）</param>
        /// <param name="exclusive">是否排他队列（默认false）</param>
        /// <param name="autoDelete">是否自动删除（默认false）</param>
        /// <param name="arguments">队列参数</param>
        public void CreateQueue(string queueName, bool durable = true, bool exclusive = false, bool autoDelete = false, IDictionary<string, object> arguments = null)
        {
            if (_connect.Connection == null || !_connect.Connection.IsOpen) _connect.Open();
            using (var channel = _connect.Connection.CreateModel())
            {
                //声明一个队列
                channel.QueueDeclare(
                    queue: queueName,     //消息队列名称
                    durable: durable,     //是否缓存
                    exclusive: exclusive, // 创建后删除
                    autoDelete: autoDelete,
                    arguments: arguments
                );
            }
        }

        /// <summary>
        /// 创建交换器，并绑定到队列
        /// </summary>
        /// <param name="exchangeName">交换器名称</param>
        /// <param name="exchangeType">交换器类型</param>
        /// <param name="durable">是否持久化（默认true）</param>
        /// <param name="autoDelete">是否自动删除（默认false）</param>
        /// <param name="arguments">参数</param>
        public void CreateExchange(string exchangeName, eumExchangeType exchangeType, bool durable = true, bool autoDelete = false, IDictionary<string, object> arguments = null)
        {
            if (_connect.Connection == null || !_connect.Connection.IsOpen) _connect.Open();
            using (var channel = _connect.Connection.CreateModel())
            {
                channel.ExchangeDeclare(exchangeName, exchangeType.ToString(), durable, autoDelete, arguments); // 声明fanout交换器
            }
        }

        /// <summary>
        /// 创建队列并绑定到交换器
        /// </summary>
        /// <param name="queueName">队列名称</param>
        /// <param name="exchangeName">交换器名称</param>
        /// <param name="routingKey">路由标签</param>
        /// <param name="durable">是否持久化（默认true）</param>
        /// <param name="exclusive">是否排他队列（默认false）</param>
        /// <param name="autoDelete">是否自动删除（默认false）</param>
        /// <param name="arguments">队列参数</param>
        public void CreateQueueAndBind(string queueName, string exchangeName, string routingKey, bool durable = true, bool exclusive = false, bool autoDelete = false, IDictionary<string, object> arguments = null)
        {
            CreateQueue(queueName, durable, exclusive, autoDelete, arguments);
            BindQueueExchange(exchangeName, queueName, routingKey);
        }

        /// <summary>
        /// 绑定队列到交换器
        /// </summary>
        /// <param name="exchangeName">交换器名称</param>
        /// <param name="queueName">队列名称</param>
        /// <param name="routingKey">路由标签</param>
        public void BindQueueExchange(string exchangeName, string queueName, string routingKey)
        {
            if (_connect.Connection == null || !_connect.Connection.IsOpen) _connect.Open();
            using (var channel = _connect.Connection.CreateModel())
            {
                channel.QueueBind(queueName, exchangeName, routingKey);
            }
        }
    }
}