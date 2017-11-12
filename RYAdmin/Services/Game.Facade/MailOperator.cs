using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Net;

namespace Game.Facade
{
    public class MailOperator
    {
        /// <summary>
        ///     发送邮件(批量发送，少于20个接受人)
        /// </summary>
        /// <param name="smtpserver">SMTP 主机服务器</param>
        /// <param name="port"> SMTP 主机上的端口号。默认值为 25</param>
        /// <param name="account">发件人邮箱</param>
        /// <param name="pass">发件人邮箱密码</param>
        /// <param name="from_alias"></param>
        /// <param name="title">邮件标题</param>
        /// <param name="content">邮件内容</param>
        /// <param name="ishtml"></param>
        /// <param name="encoding"></param>
        /// <param name="priority"></param>
        /// <param name="to_list">邮件接收人地址，不能为空</param>
        public void SendMail(string smtpserver, int? port, string account, string pass, string from_alias, string title, string content, bool ishtml, Encoding encoding, MailPriority priority, Dictionary<string, string> to_list)
        {
            if (to_list.Count <= 0)
            {
                throw new Exception("邮件接收人地址不能为空");
            }
            if (to_list.Count >= 20)
            {
                throw new Exception("每次只能发送给少于20个接收人！");
            }
            SmtpClient client = new SmtpClient();
            client.Host = smtpserver;
            if (port.HasValue)
            {
                client.Port = port.Value;
            }
            client.EnableSsl = true;//开启安全连接。
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(account, pass);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            MailAddress address = new MailAddress(account);
            if (!string.IsNullOrEmpty(from_alias))
            {
                address = new MailAddress(account, from_alias);
            }
            MailMessage message = new MailMessage();
            message.From = address;
            foreach (KeyValuePair<string, string> pair in to_list)
            {
                if (string.IsNullOrEmpty(pair.Value))
                {
                    message.To.Add(new MailAddress(pair.Key));
                }
                else
                {
                    message.To.Add(new MailAddress(pair.Key, pair.Value));
                }
            }
            message.Subject = title;
            message.Body = content;
            message.BodyEncoding = encoding;
            message.IsBodyHtml = ishtml;
            message.Priority = priority;
            try
            {
                client.Send(message);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        /// <summary>
        /// 发送邮件(单个发送)
        /// </summary>
        /// <param name="smtpserver">SMTP 主机服务器</param>
        /// <param name="port">SMTP 主机上的端口号。默认值为 25</param>
        /// <param name="userName">发件人邮箱登陆帐号</param>
        /// <param name="password">发件人邮箱登录密码</param>
        /// <param name="from_mail">发件人邮箱地址</param>
        /// <param name="from_alias">发件人别名</param>
        /// <param name="to_mail">接收人邮箱地址</param>
        /// <param name="to_alias">接收人别名</param>
        /// <param name="title">邮件标题</param>
        /// <param name="content">邮件内容</param>
        /// <param name="ishtml">获取或设置指示邮件正文是否为 Html 格式的值。默认为false</param>
        /// <param name="enableSsl">指定 SmtpClient 是否使用安全套接字层 (SSL) 加密连接。默认为false</param>
        /// <param name="encoding">获取或设置用于邮件正文的编码。</param>
        /// <param name="priority">获取或设置此电子邮件的优先级。</param>
        public void SendMail(string smtpserver, int? port, string userName, string password, string from_mail, string from_alias, string to_mail, string to_alias, string title, string content, bool ishtml, bool enableSsl, Encoding encoding, MailPriority priority)
        {
            SmtpClient client = new SmtpClient();
            client.Host = smtpserver;
            if (port.HasValue)
            {
                client.Port = port.Value;
            }

            client.EnableSsl = enableSsl;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(userName, password);
            //通过网络发送到Smtp服务器
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            MailAddress from = new MailAddress(from_mail);
            if (!string.IsNullOrEmpty(from_alias))
            {
                from = new MailAddress(from_mail, from_alias);
            }
            MailAddress to = new MailAddress(to_mail);
            if (!string.IsNullOrEmpty(to_alias))
            {
                to = new MailAddress(to_mail, to_alias);
            }
            MailMessage message = new MailMessage(from, to);
            message.Subject = title;
            message.Body = content;
            message.BodyEncoding = encoding;
            message.IsBodyHtml = ishtml;
            message.Priority = priority;
            message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;
            //StringBuilder sb = new StringBuilder();
            //sb.Append(client.EnableSsl);
            //sb.Append(client.Port);
            //sb.Append(from);
            //sb.Append(to);
            //Fetch.logstr("Email", sb.ToString());
            try
            {
                client.Send(message);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}
