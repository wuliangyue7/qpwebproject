using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Game.Kernel;
using Game.Utils;

namespace Game.Facade.Mail
{
    /// <summary>
    /// 忘记密码模版配置
    /// </summary>
    public class TMLForgetConfigManager : DefaultConfigFileManager
    {
        #region Fields

        private static MailTMLConfigInfo m_configinfo = null;

        /// <summary>
        /// 文件修改时间
        /// </summary>
        private static DateTime m_fileoldchange;

        #endregion

        #region 构造方法

        /// <summary>
        /// 初始化文件修改时间和对象实例
        /// </summary>
        static TMLForgetConfigManager()
        {
            m_fileoldchange = System.IO.File.GetLastWriteTime(ConfigFilePath);
            m_configinfo = (MailTMLConfigInfo)DefaultConfigFileManager.DeserializeInfo(ConfigFilePath, typeof(MailTMLConfigInfo));
        }

        #endregion

        #region 公开属性

        /// <summary>
        /// 当前的配置类实例
        /// </summary>
        public static new IConfigInfo ConfigInfo
        {
            get
            {
                return m_configinfo;
            }
            set
            {
                m_configinfo = (MailTMLConfigInfo)value;
            }
        }

        /// <summary>
        /// 配置文件所在路径
        /// </summary>
        public static string filename = null;


        /// <summary>
        /// 获取配置文件所在路径
        /// </summary>
        //public new static string ConfigFilePath
        //{
        //    get
        //    {
        //        return filename;
        //    }
        //}

        /// <summary>
        /// 返回配置类实例
        /// </summary>
        /// <returns></returns>
        public static MailTMLConfigInfo LoadConfig()
        {
            ConfigInfo = DefaultConfigFileManager.LoadConfig(ref m_fileoldchange, ConfigFilePath, ConfigInfo, false);
            return ConfigInfo as MailTMLConfigInfo;
        }

        /// <summary>
        /// 保存配置类实例
        /// </summary>
        /// <returns></returns>
        public override bool SaveConfig()
        {
            return base.SaveConfig( ConfigFilePath, ConfigInfo );
        }

        #endregion
    }
}
