using System;
using System.Collections.Generic;
using System.Text;
using FS.DI;
using FS.MQ.RabbitMQ.Configuration;
using RabbitMQ.Client;

namespace FS.MQ.RabbitMQ
{
    public class RabbitProduct : IRabbitProduct
    {
        /// <summary>
        /// 配置信息
        /// </summary>
        private readonly ProductConfig _productConfig;

        /// <summary>
        ///     创建消息队列属性
        /// </summary>
        private readonly RabbitConnect _connect;

        public RabbitProduct(RabbitConnect connect, ProductConfig productConfig)
        {
            _connect       = connect;
            _productConfig = productConfig;
        }

        /// <summary>
        ///     开启生产消息
        /// </summary>
        private IModel CreateChannel()
        {
            // 如果连接断开，则要重连
            if (_connect.Connection == null || !_connect.Connection.IsOpen) _connect.Open();
            var channel = _connect.Connection.CreateModel();
            if (_productConfig.UseConfirmModel) channel.ConfirmSelect();
            return channel;
        }

        /// <summary>
        ///     关闭生产者
        /// </summary>
        public void Close(IModel channel)
        {
            channel.Close();
            channel.Dispose();
            channel = null;
        }

        /// <summary>
        ///     发送消息（Routingkey默认配置中的RoutingKey；ExchangeName默认配置中的ExchangeName）
        /// </summary>
        /// <param name="message">消息主体</param>
        /// <param name="funcBasicProperties">属性</param>
        public bool Send(string message, Action<IBasicProperties> funcBasicProperties = null)
        {
            return Send(message, _productConfig.RoutingKey, _productConfig.ExchangeName, funcBasicProperties);
        }

        /// <summary>
        ///     发送消息（Routingkey默认配置中的RoutingKey；ExchangeName默认配置中的ExchangeName）
        /// </summary>
        /// <param name="message">消息主体</param>
        /// <param name="funcBasicProperties">属性</param>
        public bool Send(IEnumerable<string> message, Action<IBasicProperties> funcBasicProperties = null)
        {
            return Send(message, _productConfig.RoutingKey, _productConfig.ExchangeName, funcBasicProperties);
        }

        /// <summary>
        ///     发送消息
        /// </summary>
        /// <param name="message">消息主体</param>
        /// <param name="routingKey">路由KEY名称</param>
        /// <param name="exchange">交换器名称</param>
        /// <param name="funcBasicProperties">属性</param>
        public bool Send(string message, string routingKey, string exchange = "", Action<IBasicProperties> funcBasicProperties = null)
        {
            IModel channel = null;
            try
            {
                channel = CreateChannel();

                var basicProperties = channel.CreateBasicProperties();
                // 默认设置为消息持久化
                if (funcBasicProperties != null) funcBasicProperties(basicProperties);
                else basicProperties.DeliveryMode = 2;

                //消息内容
                var body = Encoding.UTF8.GetBytes(message);
                //发送消息
                channel.BasicPublish(exchange: exchange, routingKey: routingKey, basicProperties: basicProperties, body: body);
                return !_productConfig.UseConfirmModel || channel.WaitForConfirms();
            }
            catch (Exception e)
            {
                IocManager.Instance.Logger.Error(e.ToString(), e);
                return false;
            }
            finally
            {
                Close(channel);
            }
        }

        /// <summary>
        ///     发送消息（批量）
        /// </summary>
        /// <param name="message">消息主体</param>
        /// <param name="routingKey">路由KEY名称</param>
        /// <param name="exchange">交换器名称</param>
        /// <param name="funcBasicProperties">属性</param>
        public bool Send(IEnumerable<string> message, string routingKey, string exchange = "", Action<IBasicProperties> funcBasicProperties = null)
        {
            IModel channel = null;
            try
            {
                channel = CreateChannel();

                var basicProperties = channel.CreateBasicProperties();
                // 默认设置为消息持久化
                if (funcBasicProperties != null) funcBasicProperties(basicProperties);
                else basicProperties.DeliveryMode = 2;

                foreach (var msg in message)
                {
                    //消息内容
                    var body = Encoding.UTF8.GetBytes(msg);
                    //发送消息
                    channel.BasicPublish(exchange: exchange, routingKey: routingKey, basicProperties: basicProperties, body: body);
                }

                return !_productConfig.UseConfirmModel || channel.WaitForConfirms();
            }
            catch (Exception e)
            {
                IocManager.Instance.Logger.Error(e.ToString(), e);
                return false;
            }
            finally
            {
                Close(channel);
            }
        }
    }
}