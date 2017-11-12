/*
 * 版本：4.0
 * 时间：2016/10/20
 * 作者：http://www.foxuc.com
 *
 * 描述：实体类
 *
 */

using System;
using System.Collections.Generic;

namespace Game.Entity.Accounts
{
	/// <summary>
	/// 实体类 AccountsAgentGame。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class AccountsAgentGame  
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "AccountsAgentGame" ;

        /// <summary>
        /// ID
        /// </summary>
        public const string _ID = "ID";

		/// <summary>
		/// 代理标识
		/// </summary>
		public const string _AgentID = "AgentID" ;

		/// <summary>
		/// 游戏标识
		/// </summary>
		public const string _KindID = "KindID" ;

		/// <summary>
		/// 设备标识(1:大厅,2:手机)
		/// </summary>
		public const string _DeviceID = "DeviceID" ;

		/// <summary>
		/// 排序
		/// </summary>
		public const string _SortID = "SortID" ;

		/// <summary>
		/// 创建日期
		/// </summary>
		public const string _CollectDate = "CollectDate" ;
		#endregion

		#region 私有变量
        private int m_id;                       //ID
		private int m_agentID;					//代理标识
		private int m_kindID;					//游戏标识
		private int m_deviceID;					//设备标识(1:大厅,2:手机)
		private int m_sortID;					//排序
		private DateTime m_collectDate;			//创建日期
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化AccountsAgentGame
		/// </summary>
		public AccountsAgentGame()
		{
            m_id = 0;
			m_agentID=0;
			m_kindID=0;
			m_deviceID=0;
			m_sortID=0;
			m_collectDate=DateTime.Now;
		}

		#endregion

		#region 公共属性

        /// <summary>
        /// ID
        /// </summary>
        public int ID
        {
            get { return m_id; }
            set { m_id = value; }
        }

		/// <summary>
		/// 代理标识
		/// </summary>
		public int AgentID
		{
			get { return m_agentID; }
			set { m_agentID = value; }
		}

		/// <summary>
		/// 游戏标识
		/// </summary>
		public int KindID
		{
			get { return m_kindID; }
			set { m_kindID = value; }
		}

		/// <summary>
		/// 设备标识(1:大厅,2:手机)
		/// </summary>
		public int DeviceID
		{
			get { return m_deviceID; }
			set { m_deviceID = value; }
		}

		/// <summary>
		/// 排序
		/// </summary>
		public int SortID
		{
			get { return m_sortID; }
			set { m_sortID = value; }
		}

		/// <summary>
		/// 创建日期
		/// </summary>
		public DateTime CollectDate
		{
			get { return m_collectDate; }
			set { m_collectDate = value; }
		}
		#endregion
	}
}
