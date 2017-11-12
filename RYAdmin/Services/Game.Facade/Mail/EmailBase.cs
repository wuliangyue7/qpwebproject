using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.Threading;

using Game.Utils;

namespace Game.Facade.Mail
{
    /// <summary>
    /// 邮件发送基类
    /// </summary>
    public class EmailBase
    {
        #region Fields

        private string m_accounts;
        private string m_password;
        private string m_content;
        private string m_smtpServer;
        private int m_port;
        private string m_smtpSenderEmail;
        private MessageRender _messageRender;
        private string contentTempdate;
        private string subjectTemplate;

        #endregion

        #region 构造方法

        /// <summary>
        /// 初始化实例对象
        /// </summary>
        public EmailBase()
        {
            this._messageRender = new MessageRender();
        }

        /// <summary>
        /// 初始化实例对象
        /// </summary>
        public EmailBase( MailConfigInfo mailConfigInfo )
        {
            this._messageRender = new MessageRender();
            this.m_smtpServer = mailConfigInfo.SmtpServer;
            this.m_smtpSenderEmail = mailConfigInfo.SmtpSenderEmail;
            this.m_accounts = mailConfigInfo.Accounts;
            this.m_password = mailConfigInfo.Password;
            this.m_port = mailConfigInfo.Port;
        }

        /// <summary>
        /// 初始化实例对象
        /// </summary>
        /// <param name="subjectTemplate"></param>
        /// <param name="contentTemplate"></param>
        public EmailBase( string subjectTemplate, string contentTemplate, MailConfigInfo mailConfigInfo )
            : this( mailConfigInfo )
        {
            this._messageRender = new MessageRender();
            this.subjectTemplate = subjectTemplate;
            this.contentTempdate = contentTemplate;
        }

        /// <summary>
        /// 初始化实例对象
        /// </summary>
        /// <param name="subjectTemplate"></param>
        /// <param name="contentTemplate"></param>
        public EmailBase( string subjectTemplate, string contentTemplate )
            : this()
        {
            this._messageRender = new MessageRender();
            this.subjectTemplate = subjectTemplate;
            this.contentTempdate = contentTemplate;
        }

        /// <summary>
        /// 初始化实例对象
        /// </summary>
        /// <param name="mailTml"></param>
        public EmailBase( MailTMLConfigInfo mailTml, MailConfigInfo mailConfigInfo )
            : this( mailConfigInfo )
        {
            this._messageRender = new MessageRender();
            this.subjectTemplate = mailTml.MailTitle;
            this.contentTempdate = mailTml.MailContent.Text;
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
        ///  邮件服务器,如：smtp.163.com
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
        /// 邮件主题
        /// </summary>
        public virtual string Subject
        {
            get
            {
                return this._messageRender.Render( this.subjectTemplate );
            }
        }

        /// <summary>
        /// 邮件内容
        /// </summary>
        public string Content
        {
            get
            {
                if( string.IsNullOrEmpty( this.m_content ) )
                {
                    this.m_content = this._messageRender.Render( this.contentTempdate );
                }
                return this.m_content;
            }

            set
            {
                this.m_content = value;
            }
        }

        /// <summary>
        /// 内容呈现
        /// </summary>
        public MessageRender Render
        {
            get
            {
                return this._messageRender;
            }
        }

        #endregion

        #region 公开方法

        /// <summary>
        /// 发送测试邮件
        /// </summary>
        /// <param name="address"></param>
        public void TestEmail( string address )
        {
            SmtpClient client = new SmtpClient( this.SmtpServer )
            {
                UseDefaultCredentials = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential( this.Accounts, this.Password ),
                Port = this.Port
            };
            MailMessage message = new MailMessage( this.SmtpSenderEmail, address, this.Subject, this.Content )
            {
                IsBodyHtml = true,
                BodyEncoding = Encoding.GetEncoding( "gb2312" )
            };

            //当不是25端口(gmail:587)
            if( this.Port != 25 )
            {
                client.EnableSsl = true;
            }

            client.Send( message );
            message.Dispose();
        }


        /// <summary>
        /// 发送
        /// </summary>
        /// <param name="mailAddress"></param>
        public void Send( string[] mailAddress )
        {
            if( mailAddress != null )
            {
                WaitCallback callBack = new WaitCallback( this.Send );
                foreach( string str in mailAddress )
                {
                    if( !string.IsNullOrEmpty( str ) )
                    {
                        ThreadPool.QueueUserWorkItem( callBack, str.Trim() );
                    }
                }
            }
        }

        /// <summary>
        /// 发送
        /// </summary>
        /// <param name="email"></param>
        private void Send( object email )
        {
            try
            {
                SmtpClient client = new SmtpClient( this.SmtpServer )
                {
                    UseDefaultCredentials = false,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new NetworkCredential( this.Accounts, this.Password )
                };
                MailMessage message = new MailMessage( this.SmtpSenderEmail, email.ToString(), this.Subject, this.Content )
                {
                    IsBodyHtml = true,
                    BodyEncoding = Encoding.GetEncoding( "gb2312" )
                };

                //当不是25端口(gmail:587)
                if( this.Port != 25 )
                {
                    client.EnableSsl = true;
                }

                client.Send( message );
                message.Dispose();
            }
            catch( Exception ex )
            {
                Game.Utils.TextLogger.Write( ex.ToString() );
            }
        }

        /// <summary>
        /// 发送
        /// </summary>
        /// <param name="address"></param>
        public void Send( string address )
        {
            ThreadPool.QueueUserWorkItem( new WaitCallback( this.Send ), address );
        }

        /// <summary>
        /// 邮件回调
        /// </summary>
        /// <param name="mailAddress"></param>
        /// <param name="errorMessage"></param>
        public delegate void MassMailingCallback( string mailAddress, string errorMessage );

        #endregion
    }
}
