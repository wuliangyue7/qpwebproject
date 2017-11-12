/*
 * 版本：4.0
 * 时间：2013-12-23
 * 作者：http://www.foxuc.com
 *
 * 描述：实体类
 *
 */

using System;
using System.Collections.Generic;

namespace Game.Entity.Treasure
{
	/// <summary>
	/// 实体类 RecordCurrencyChange。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class RecordCurrencyChange  
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "RecordCurrencyChange" ;

		/// <summary>
		/// 日志标识
		/// </summary>
		public const string _RecordID = "RecordID" ;

		/// <summary>
		/// 用户标识
		/// </summary>
		public const string _UserID = "UserID" ;

		/// <summary>
		/// 货币变更数
		/// </summary>
		public const string _ChangeCurrency = "ChangeCurrency" ;

		/// <summary>
		/// 变更类型
		/// </summary>
		public const string _ChangeType = "ChangeType" ;

		/// <summary>
		/// 变更前货币
		/// </summary>
		public const string _BeforeCurrency = "BeforeCurrency" ;

		/// <summary>
		/// 变更后货币数
		/// </summary>
		public const string _AfterCurrency = "AfterCurrency" ;

		/// <summary>
		/// 变更IP
		/// </summary>
		public const string _ClinetIP = "ClinetIP" ;

		/// <summary>
		/// 变更时间
		/// </summary>
		public const string _InputDate = "InputDate" ;

		/// <summary>
		/// 备注
		/// </summary>
		public const string _Remark = "Remark" ;
		#endregion

		#region 私有变量
		private int m_recordID;						//日志标识
		private int m_userID;						//用户标识
		private decimal m_changeCurrency;			//货币变更数
		private byte m_changeType;					//变更类型
		private decimal m_beforeCurrency;			//变更前货币
		private decimal m_afterCurrency;			//变更后货币数
		private string m_clinetIP;					//变更IP
		private DateTime m_inputDate;				//变更时间
		private string m_remark;					//备注
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化RecordCurrencyChange
		/// </summary>
		public RecordCurrencyChange()
		{
			m_recordID=0;
			m_userID=0;
			m_changeCurrency=0;
			m_changeType=0;
			m_beforeCurrency=0;
			m_afterCurrency=0;
			m_clinetIP="";
			m_inputDate=DateTime.Now;
			m_remark="";
		}

		#endregion

		#region 公共属性

		/// <summary>
		/// 日志标识
		/// </summary>
		public int RecordID
		{
			get { return m_recordID; }
			set { m_recordID = value; }
		}

		/// <summary>
		/// 用户标识
		/// </summary>
		public int UserID
		{
			get { return m_userID; }
			set { m_userID = value; }
		}

		/// <summary>
		/// 货币变更数
		/// </summary>
		public decimal ChangeCurrency
		{
			get { return m_changeCurrency; }
			set { m_changeCurrency = value; }
		}

		/// <summary>
		/// 变更类型
		/// </summary>
		public byte ChangeType
		{
			get { return m_changeType; }
			set { m_changeType = value; }
		}

		/// <summary>
		/// 变更前货币
		/// </summary>
		public decimal BeforeCurrency
		{
			get { return m_beforeCurrency; }
			set { m_beforeCurrency = value; }
		}

		/// <summary>
		/// 变更后货币数
		/// </summary>
		public decimal AfterCurrency
		{
			get { return m_afterCurrency; }
			set { m_afterCurrency = value; }
		}

		/// <summary>
		/// 变更IP
		/// </summary>
		public string ClinetIP
		{
			get { return m_clinetIP; }
			set { m_clinetIP = value; }
		}

		/// <summary>
		/// 变更时间
		/// </summary>
		public DateTime InputDate
		{
			get { return m_inputDate; }
			set { m_inputDate = value; }
		}

		/// <summary>
		/// 备注
		/// </summary>
		public string Remark
		{
			get { return m_remark; }
			set { m_remark = value; }
		}
		#endregion
	}
}
