using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using System.Web;
using System.IO;

using Game.Kernel;

namespace Game.Facade.Mail
{
    /// <summary>
    /// Mail 配置
    /// </summary>
    public class MailConfigInfo
    {
        #region Fields

        private string m_accounts;
        private string m_password;
        private string m_smtpServer;
        private int m_port;
        private string m_smtpSenderEmail;
        private string m_lossreportUrl;

        #endregion

        #region 构造方法

        /// <summary>
        /// 初始化实例对象
        /// </summary>
        /// <param name="accounts"></param>
        /// <param name="password"></param>
        /// <param name="smtpServer"></param>
        /// <param name="port"></param>
        /// <param name="smtpSenderEmail"></param>
        public MailConfigInfo()
        {
        }

        #endregion

        #region 公开属性

        /// <summary>
        /// 邮箱用户名
        /// </summary>
        public string Accounts
        {
            get
            {
                return m_accounts;
            }
            set
            {
                m_accounts = value;
            }
        }

        /// <summary>
        /// 邮箱密码
        /// </summary>
        public string Password
        {
            get
            {
                return m_password;
            }
            set
            {
                m_password = value;
            }
        }

        /// <summary>
        /// 邮件服务器,如：smtp.163.com
        /// </summary>
        public string SmtpServer
        {
            get
            {
                return m_smtpServer;
            }
            set
            {
                m_smtpServer = value;
            }
        }

        /// <summary>
        /// 服务器端口,25,587(gmail)
        /// </summary>
        public int Port
        {
            get
            {
                return m_port;
            }
            set
            {
                m_port = value;
            }
        }

        /// <summary>
        /// 邮箱地址(guoshulang@163.com)
        /// </summary>
        public string SmtpSenderEmail
        {
            get
            {
                return m_smtpSenderEmail;
            }
            set
            {
                m_smtpSenderEmail = value;
            }
        }

        /// <summary>
        /// 激活连接
        /// </summary>
        public string LossreportUrl
        {
            get
            {
                return m_lossreportUrl;
            }
            set
            {
                m_lossreportUrl = value;
            }
        }

        #endregion

        #region 获取服务器路径

        /// <summary>
        /// 获取服务器上的真正路径。
        /// </summary>
        /// <param name="strPath"></param>
        /// <returns></returns>
        public string GetMapPath( string strPath )
        {
            if( String.IsNullOrEmpty( strPath ) )
                throw new Exception( "strPath 不能为空！" );

            string filename = string.Empty;
            HttpContext context = null;
            try
            {
                context = HttpContext.Current;
            }
            catch
            {
                context = null;
            }
            if( context != null )
            {
                filename = context.Server.MapPath( strPath );
            }
            else
            {
                string tmp = Path.Combine( strPath, "" );
                tmp = tmp.StartsWith( @"\\" ) ? tmp.Remove( 0, 2 ) : tmp;
                filename = String.Concat( AppDomain.CurrentDomain.BaseDirectory, Path.Combine( strPath, "" ) );
            }
            return filename;
        }
        #endregion
    }
}
