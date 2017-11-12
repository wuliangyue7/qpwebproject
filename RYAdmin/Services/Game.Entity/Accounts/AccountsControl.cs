/*
 * 版本：4.0
 * 时间：2014/8/18
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
	/// 实体类 AccountsControl。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class AccountsControl  
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "AccountsControl" ;

		/// <summary>
		/// 用户标识
		/// </summary>
		public const string _UserID = "UserID" ;

		/// <summary>
		/// 用户账号
		/// </summary>
		public const string _Accounts = "Accounts" ;

		/// <summary>
		/// 激活时间
		/// </summary>
		public const string _ActiveDateTime = "ActiveDateTime" ;

		/// <summary>
		/// 控制状态(1 黑名单 2 白名单)
		/// </summary>
		public const string _ControlStatus = "ControlStatus" ;

		/// <summary>
		/// 控制类型(2 时间控制 1 金币变更控制)
		/// </summary>
		public const string _ControlType = "ControlType" ;

		/// <summary>
		/// 变更金币(即 赢多少 或 输多少)
		/// </summary>
		public const string _ChangeScore = "ChangeScore" ;

		/// <summary>
		/// 持续时间
		/// </summary>
		public const string _SustainedTimeCount = "SustainedTimeCount" ;

		/// <summary>
		/// 消耗时间
		/// </summary>
		public const string _ConsumeTimeCount = "ConsumeTimeCount" ;

		/// <summary>
		/// 胜率
		/// </summary>
		public const string _WinRate = "WinRate" ;

		/// <summary>
		/// 累计赢的金币
		/// </summary>
		public const string _WinScore = "WinScore" ;

		/// <summary>
		/// 累计输的金币
		/// </summary>
		public const string _LoseScore = "LoseScore" ;
		#endregion

		#region 私有变量
		private int m_userID;						//用户标识
		private string m_accounts;					//用户账号
		private DateTime m_activeDateTime;			//激活时间
		private short m_controlStatus;				//控制状态(1 黑名单 2 白名单)
		private short m_controlType;				//控制类型(2 时间控制 1 金币变更控制)
		private long m_changeScore;					//变更金币(即 赢多少 或 输多少)
		private int m_sustainedTimeCount;			//持续时间
		private int m_consumeTimeCount;				//消耗时间
		private short m_winRate;					//胜率
		private long m_winScore;					//累计赢的金币
		private long m_loseScore;					//累计输的金币
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化AccountsControl
		/// </summary>
		public AccountsControl()
		{
			m_userID=0;
			m_accounts="";
			m_activeDateTime=DateTime.Now;
			m_controlStatus=0;
			m_controlType=0;
			m_changeScore=0;
			m_sustainedTimeCount=0;
			m_consumeTimeCount=0;
			m_winRate=0;
			m_winScore=0;
			m_loseScore=0;
		}

		#endregion

		#region 公共属性

		/// <summary>
		/// 用户标识
		/// </summary>
		public int UserID
		{
			get { return m_userID; }
			set { m_userID = value; }
		}

		/// <summary>
		/// 用户账号
		/// </summary>
		public string Accounts
		{
			get { return m_accounts; }
			set { m_accounts = value; }
		}

		/// <summary>
		/// 激活时间
		/// </summary>
		public DateTime ActiveDateTime
		{
			get { return m_activeDateTime; }
			set { m_activeDateTime = value; }
		}

		/// <summary>
		/// 控制状态(1 黑名单 2 白名单)
		/// </summary>
		public short ControlStatus
		{
			get { return m_controlStatus; }
			set { m_controlStatus = value; }
		}

		/// <summary>
		/// 控制类型(2 时间控制 1 金币变更控制)
		/// </summary>
		public short ControlType
		{
			get { return m_controlType; }
			set { m_controlType = value; }
		}

		/// <summary>
		/// 变更金币(即 赢多少 或 输多少)
		/// </summary>
		public long ChangeScore
		{
			get { return m_changeScore; }
			set { m_changeScore = value; }
		}

		/// <summary>
		/// 持续时间
		/// </summary>
		public int SustainedTimeCount
		{
			get { return m_sustainedTimeCount; }
			set { m_sustainedTimeCount = value; }
		}

		/// <summary>
		/// 消耗时间
		/// </summary>
		public int ConsumeTimeCount
		{
			get { return m_consumeTimeCount; }
			set { m_consumeTimeCount = value; }
		}

		/// <summary>
		/// 胜率
		/// </summary>
		public short WinRate
		{
			get { return m_winRate; }
			set { m_winRate = value; }
		}

		/// <summary>
		/// 累计赢的金币
		/// </summary>
		public long WinScore
		{
			get { return m_winScore; }
			set { m_winScore = value; }
		}

		/// <summary>
		/// 累计输的金币
		/// </summary>
		public long LoseScore
		{
			get { return m_loseScore; }
			set { m_loseScore = value; }
		}
		#endregion
	}
}
