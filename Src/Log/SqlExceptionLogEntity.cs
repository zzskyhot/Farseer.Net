﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using FS.Component;
using FS.Configs;
using FS.Utils.Common;

namespace FS.Log
{
    /// <summary> SQL异常记录 </summary>
    [Serializable]
    public class SqlExceptionLogEntity : AbsLogEntity
    {
        /// <summary> SQL异常记录管理器 </summary>
        private static readonly CommonLogManger<SqlExceptionLogEntity> LogManger = new CommonLogManger<SqlExceptionLogEntity>(SysMapPath.AppData + "log/SqlExceptionLog/", $"{DateTime.Now.ToString("yy-MM-dd")}.xml", 1);

        public SqlExceptionLogEntity()
        {
        }

        /// <summary> SQL异常记录写入~/App_Data/SqlExceptionLog.xml </summary>
        /// <param name="name">表名称</param>
        /// <param name="cmdType">执行方式</param>
        /// <param name="sql">T-SQL</param>
        /// <param name="param">SQL参数</param>
        /// <param name="ex">异常信息</param>
        public static void Write(Exception ex, string name, CommandType cmdType, string sql, List<DbParameter> param)
        {
            var entity = new SqlExceptionLogEntity {ex = ex, Message = ex.Message.Replace("\r\n", ""), Name = name, CmdType = cmdType, SqlParamList = new List<SqlParam>(), Sql = sql,};
            if (param != null && param.Count > 0) { param.ForEach(o => entity.SqlParamList.Add(new SqlParam {Name = o.ParameterName, Value = (o.Value ?? "null").ToString()})); }
            entity.Write();
        }

        /// <summary> 执行对象 </summary>
        public CommandType CmdType { get; set; }

        /// <summary> 执行表名称 </summary>
        public string Name { get; set; }

        /// <summary> 执行SQL </summary>
        public string Sql { get; set; }

        /// <summary> 执行参数 </summary>
        public List<SqlParam> SqlParamList { get; set; }

        /// <summary> 异常消息 </summary>
        public string Message { get; set; }

        /// <summary> 写入~/App_Data/SqlExceptionLog.xml </summary>
        private void Write()
        {
            RecordExecuteMethod();

            LogManger.Write(this);

            // 发送邮件
            if (SystemConfigs.ConfigEntity.IsSendExceptionEMail) { SendEmail(); }
        }

        /// <summary> 发送邮件 </summary>
        private void SendEmail()
        {
            var mail = ExceptionEmailConfigs.ConfigEntity;
            var smtp = new SmtpMail(mail.LoginName, mail.LoginPwd, mail.SendMail, "Farseer.Net SQL异常记录", mail.SmtpServer, 0, mail.SmtpPort);
            var body = new StringBuilder();
            body.AppendFormat("<b>发现时间：</b> {0}<br />", CreateAt.ToString("yyyy年MM月dd日 HH:mm:ss"));
            body.AppendFormat("<b>程序文件：</b> <u>{0}</u> <b>第{1}行</b> <font color=red>{2}()</font><br />", FileName, LineNo, MethodName);

            switch (CmdType)
            {
                case CommandType.StoredProcedure:
                    body.AppendFormat("<b>存储过程：</b> {0}<br />", Name);
                    break;
                case CommandType.Text:
                    body.AppendFormat("<b>表视图名：</b> {0}<br />", Name);
                    body.AppendFormat("<b>Sql语句：</b> {0}<br />", Sql);
                    break;
            }

            body.AppendFormat("<b>Sql参数：</b><br />");
            SqlParamList.ForEach(o => body.AppendFormat("{0} = {1}<br />", o.Name, o.Value));
            body.AppendFormat("<b>错误消息：</b><font color=red>{0}</font><br />", Message);
            smtp.Send(mail.EmailAddress, $"{DateTime.Now.ToString("yyyy年MM月dd日 HH:mm:ss")}：警告！数据库异常：{Message}", body.ToString());
        }
    }

    public class SqlParam
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}