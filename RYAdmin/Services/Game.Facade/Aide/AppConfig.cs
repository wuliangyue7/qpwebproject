using System;
using Game.Utils;

namespace Game.Facade
{
    public class AppConfig
    {
        #region 配置

        /// <summary>
        /// 管理员登陆Session/Cookies的Key值
        /// </summary>
        public static string UserCacheKey
        {
            get
            {
                string strName = Utility.GetAppSetting( "AppPrefix" );
                if( !string.IsNullOrEmpty( strName ) )
                {
                    return strName;
                }
                return "6603sAdministratorKey";
            }
        }

        /// <summary>
        /// 管理员登陆Session/Cookies的过期时间，单位分钟
        /// </summary>
        public static int UserCacheTimeOut
        {
            get
            {
                string strName = Utility.GetAppSetting( "UserCacheTimeOut" );
                if( !string.IsNullOrEmpty( strName ) )
                {
                    return Convert.ToInt32( strName );
                }
                return 30;
            }
        }

        /// <summary>
        /// 申诉加密KEY
        /// </summary>
        public static string ReportForgetPasswordKey
        {
            get
            {
                string key = ApplicationSettings.Get( "ReportForgetPasswordKey" );
                if( !string.IsNullOrEmpty( key ) )
                {
                    return key;
                }
                return "ReportForgetPasswordKeyValue";
            }
        }

        #endregion 配置

        #region 常量

        /// <summary>
        /// 验证码Session的KEY值
        /// </summary>
        public const string VerifyCodeKey = "VerifyCodeKey";

        #endregion 常量
    }
}