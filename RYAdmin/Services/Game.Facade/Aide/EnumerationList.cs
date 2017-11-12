using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Utils;

namespace Game.Facade
{
    public class EnumerationList
    {
        #region 商品填写信息
        /// <summary>
        /// 购买时需要填写的信息
        /// </summary>
        [Serializable]
        public enum MallNeedInfo : int
        {
            /// <summary>
            /// 真实姓名
            /// </summary>
            [EnumDescription( "真实姓名" )]
            Compellation = 1,
            /// <summary>
            /// 手机号码
            /// </summary>
            [EnumDescription( "手机号码" )]
            PhoneNumber = 2,
            /// <summary>
            /// QQ号码
            /// </summary>
            [EnumDescription( "QQ|微信" )]
            QQNumber = 4,
            /// <summary>
            /// 收货地址及邮编
            /// </summary>
            [EnumDescription( "收货地址及邮编" )]
            AdressAndCode = 8
        }
        #endregion

        #region 订单状态
        /// <summary>
        /// 订单状态
        /// </summary>
        public enum AwardOrderStatus
        {
            处理中 = 0,
            已发货 = 1,
            已收货 = 2,
            申请退货 = 3,
            同意退货等待客户发货 = 4,
            拒绝退货 = 5,
            退货成功 = 6
        }
        #endregion

        #region 任务类型

        /// <summary>
        /// 任务类型
        /// </summary>
        public enum TaskType
        {
            总赢局 = 1,
            总局数 = 2,
            首胜 = 4
        }

        #endregion

        #region 系统设置列表
        public enum SystemStatusKey
        {
            /// <summary>
            /// 默认游戏赢一局经验配置
            /// </summary>
            WinExperience
        }
        #endregion

        #region 站点配置列表

        public enum SiteConfigKey
        {
            /// <summary>
            /// 联系方式配置
            /// </summary>
            [EnumDescription( "联系方式配置" )]
            ContactConfig,
            /// <summary>
            /// 站点配置
            /// </summary>
            [EnumDescription( "站点配置" )]
            SiteConfig,
            /// <summary>
            /// 大厅整包配置
            /// </summary>
            [EnumDescription( "大厅整包配置号码" )]
            GameFullPackageConfig,
            /// <summary>
            /// 大厅简包配置
            /// </summary>
            [EnumDescription( "大厅简包配置" )]
            GameJanePackageConfig,
            /// <summary>
            /// 邮箱配置
            /// </summary>
            [EnumDescription( "邮箱配置" )]
            EmailConfig
        }

        #endregion

        #region 推广类型

        /// <summary>
        /// 推广类型
        /// </summary>
        public enum SpreadTypes
        {
            注册 = 1,
            游戏时长 = 2,
            充值 = 3,
            结算 = 4
        }

        #endregion

        #region 清理数据表类型

        /// <summary>
        /// 清理数据表类型
        /// </summary>
        public enum ClearTableTypes
        {
            玩家进出记录表 = 1,
            游戏记录总局表 = 2,
            游戏记录详情表 = 3,
            银行操作记录表 = 4
        }

        #endregion

        #region 比赛类型

        /// <summary>
        /// 比赛类型
        /// </summary>
        public enum MatchType
        {
            定时赛 = 0,
            即时赛 = 1
        }

        #endregion

        #region 扣费类型

         /// <summary>
        /// 扣费类型
        /// </summary>
        public enum MatchFeeType
        {
            金币 = 0,
            元宝 = 1
        }

        #endregion

        public enum MemberType
        {
            普通会员=0,

        } 

    }
}
