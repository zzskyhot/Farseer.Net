﻿// ********************************************
// 作者：何达贤（steden） QQ：11042427
// 时间：2017-03-02 14:09
// ********************************************

using System;
using System.ComponentModel.DataAnnotations;
using System.ServiceModel;
using System.Threading.Tasks;
using FS.Core.Exception;
using FS.Core.Net;
using FS.Core.Net.Gateway;
using FS.Extends;

namespace FS.Core.MicroService
{
    /// <summary>
    /// 微服务基类（带DTO参数的检查）
    /// </summary>
    public abstract class BaseMicroService<T> : AbsBaseService
    {
        /// <summary>
        /// 微服务基类（带DTO参数的检查）
        /// </summary>
        public BaseMicroService(GatewayHeaderVO header) : base(header)
        {
        }

        /// <summary>
        /// 调用的业务逻辑
        /// </summary>
        protected abstract Task<ApiNodeResponseJson> Invoke(T dto);

        /// <summary>
        /// 带实体类检查的调用
        /// </summary>
        public async Task<ApiNodeResponseJson> InvokeCheck(string param)
        {
            try
            {
#if !DEBUG

                // 网关效验
                var gatewayResult = GatewayValidate();
                if (!gatewayResult.Status)
                {
                    return gatewayResult;
                }
#endif

                // DTO参数效验
                var dto    = ToObject<T>(param);
                var result = EntityValidator.Check(dto);
                if (!result.Status)
                {
                    return ApiNodeResponseJson.Error(result.StatusMessage, null, result.StatusCode);
                }

                return await Invoke(dto);
            }
            catch (AggregateException exp)
            {
                var err        = string.Empty;
                var statusCode = 500;
                foreach (var inner in exp.InnerExceptions)
                {
                    err += inner.Message;
                    var exception = inner as FaultException;
                    if (exception != null)
                    {
                        statusCode = exception.Code.Name.ConvertType(0);
                    }
                }

                return ApiNodeResponseJson.Error(err, null, statusCode);
            }
            catch (FaultException exp)
            {
                return ApiNodeResponseJson.Error(exp.Message, exp.Action, exp.Code.Name.ConvertType(0));
            }
            catch (System.Exception exp)
            {
                return ApiNodeResponseJson.Error(exp.GetType().FullName, null, 500);
            }
        }
    }
}