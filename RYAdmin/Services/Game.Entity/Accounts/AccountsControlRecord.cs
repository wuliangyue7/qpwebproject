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
	/// 实体类 AccountsControlRecord。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class AccountsControlRecord  
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "AccountsControlRecord" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _ID = "ID" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _UserID = "UserID" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _Accounts = "Accounts" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _ControlStatus = "ControlStatus" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _ControlType = "ControlType" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _ChangeScore = "ChangeScore" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _SustainedTimeCount = "SustainedTimeCount" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _ConsumeTimeCount = "ConsumeTimeCount" ;

		/// <summary>
		/// 结束方式 1 自动结束 2收到结束
		/// </summary>
		public const string _ConcludeType = "ConcludeType" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _StartDateTime = "StartDateTime" ;

		/// <summary>
		/// 结束时间
		/// </summary>
		public const string _ConcludeDateTime = "ConcludeDateTime" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _WinRate = "WinRate" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _WinScore = "WinScore" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _LoseScore = "LoseScore" ;
		#endregion

		#region 私有变量
		private int m_iD;							//
		private int m_userID;						//
		private string m_accounts;					//
		private short m_controlStatus;				//
		private short m_controlType;				//
		private decimal m_changeScore;				//
		private int m_sustainedTimeCount;			//
		private int m_consumeTimeCount;				//
		private short m_concludeType;				//结束方式 1 自动结束 2收到结束
		private DateTime m_startDateTime;			//
		private DateTime m_concludeDateTime;		//结束时间
		private short m_winRate;					//
		private decimal m_winScore;					//
		private decimal m_loseScore;				//
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化AccountsControlRecord
		/// </summary>
		public AccountsControlRecord()
		{
			m_iD=0;
			m_userID=0;
			m_accounts="";
			m_controlStatus=0;
			m_controlType=0;
			m_changeScore=0;
			m_sustainedTimeCount=0;
			m_consumeTimeCount=0;
			m_concludeType=0;
			m_startDateTime=DateTime.Now;
			m_concludeDateTime=DateTime.Now;
			m_winRate=0;
			m_winScore=0;
			m_loseScore=0;
		}

		#endregion

		#region 公共属性

		/// <summary>
		/// 
		/// </summary>
		public int ID
		{
			get { return m_iD; }
			set { m_iD = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public int UserID
		{
			get { return m_userID; }
			set { m_userID = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string Accounts
		{
			get { return m_accounts; }
			set { m_accounts = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public short ControlStatus
		{
			get { return m_controlStatus; }
			set { m_controlStatus = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public short ControlType
		{
			get { return m_controlType; }
			set { m_controlType = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public decimal ChangeScore
		{
			get { return m_changeScore; }
			set { m_changeScore = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public int SustainedTimeCount
		{
			get { return m_sustainedTimeCount; }
			set { m_sustainedTimeCount = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public int ConsumeTimeCount
		{
			get { return m_consumeTimeCount; }
			set { m_consumeTimeCount = value; }
		}

		/// <summary>
		/// 结束方式 1 自动结束 2收到结束
		/// </summary>
		public short ConcludeType
		{
			get { return m_concludeType; }
			set { m_concludeType = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public DateTime StartDateTime
		{
			get { return m_startDateTime; }
			set { m_startDateTime = value; }
		}

		/// <summary>
		/// 结束时间
		/// </summary>
		public DateTime ConcludeDateTime
		{
			get { return m_concludeDateTime; }
			set { m_concludeDateTime = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public short WinRate
		{
			get { return m_winRate; }
			set { m_winRate = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public decimal WinScore
		{
			get { return m_winScore; }
			set { m_winScore = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public decimal LoseScore
		{
			get { return m_loseScore; }
			set { m_loseScore = value; }
		}
		#endregion
	}
}
