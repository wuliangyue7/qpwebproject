using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

using Game.Kernel;
using Game.Utils;

namespace Game.Facade.Mail
{
    /// <summary>
    /// 邮件模板
    /// </summary>
    public class MailTMLConfigInfo : IConfigInfo
    {
        private CDATA m_contentTempdate = new CDATA( "" );

        [XmlElementAttribute( "MailContent", Type = typeof( CDATA ) )]
        public CDATA MailContent
        {
            get
            {
                return m_contentTempdate;
            }
            set
            {
                m_contentTempdate = value;
            }
        }

        private string m_subjectTemplate = "";

        public string MailTitle
        {
            get
            {
                return m_subjectTemplate;
            }
            set
            {
                m_subjectTemplate = value;
            }
        }

        public MailTMLConfigInfo()
        {
        }

        public MailTMLConfigInfo( string contentTml, string titleTml )
        {
            m_contentTempdate = new CDATA( contentTml );
            m_subjectTemplate = titleTml;
        }
    }
}
