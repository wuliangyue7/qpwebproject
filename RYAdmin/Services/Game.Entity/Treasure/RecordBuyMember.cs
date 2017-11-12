/*
 * 版本：4.0
 * 时间：2014-3-21
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
	/// 实体类 RecordBuyMember。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class RecordBuyMember  
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "RecordBuyMember" ;

		/// <summary>
		/// 日志标识
		/// </summary>
		public const string _RecordID = "RecordID" ;

		/// <summary>
		/// 用户标识
		/// </summary>
		public const string _UserID = "UserID" ;

		/// <summary>
		/// 购买会员类型
		/// </summary>
		public const string _MemberOrder = "MemberOrder" ;

		/// <summary>
		/// 购买月数
		/// </summary>
		public const string _MemberMonths = "MemberMonths" ;

		/// <summary>
		/// 会员每月价格（单位：货币）
		/// </summary>
		public const string _MemberPrice = "MemberPrice" ;

		/// <summary>
		/// 总花费货币
		/// </summary>
		public const string _Currency = "Currency" ;

		/// <summary>
		/// 赠送金币
		/// </summary>
		public const string _PresentScore = "PresentScore" ;

		/// <summary>
		/// 购买前货币
		/// </summary>
		public const string _BeforeCurrency = "BeforeCurrency" ;

		/// <summary>
		/// 购买前金币
		/// </summary>
		public const string _BeforeScore = "BeforeScore" ;

		/// <summary>
		/// 客户端IP
		/// </summary>
		public const string _ClinetIP = "ClinetIP" ;

		/// <summary>
		/// 兑换时间
		/// </summary>
		public const string _InputDate = "InputDate" ;
		#endregion

		#region 私有变量
		private int m_recordID;						//日志标识
		private int m_userID;						//用户标识
		private int m_memberOrder;					//购买会员类型
		private int m_memberMonths;					//购买月数
		private decimal m_memberPrice;				//会员每月价格（单位：货币）
		private decimal m_currency;					//总花费货币
		private long m_presentScore;				//赠送金币
		private decimal m_beforeCurrency;			//购买前货币
		private long m_beforeScore;					//购买前金币
		private string m_clinetIP;					//客户端IP
		private DateTime m_inputDate;				//兑换时间
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化RecordBuyMember
		/// </summary>
		public RecordBuyMember()
		{
			m_recordID=0;
			m_userID=0;
			m_memberOrder=0;
			m_memberMonths=0;
			m_memberPrice=0;
			m_currency=0;
			m_presentScore=0;
			m_beforeCurrency=0;
			m_beforeScore=0;
			m_clinetIP="";
			m_inputDate=DateTime.Now;
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
		/// 购买会员类型
		/// </summary>
		public int MemberOrder
		{
			get { return m_memberOrder; }
			set { m_memberOrder = value; }
		}

		/// <summary>
		/// 购买月数
		/// </summary>
		public int MemberMonths
		{
			get { return m_memberMonths; }
			set { m_memberMonths = value; }
		}

		/// <summary>
		/// 会员每月价格（单位：货币）
		/// </summary>
		public decimal MemberPrice
		{
			get { return m_memberPrice; }
			set { m_memberPrice = value; }
		}

		/// <summary>
		/// 总花费货币
		/// </summary>
		public decimal Currency
		{
			get { return m_currency; }
			set { m_currency = value; }
		}

		/// <summary>
		/// 赠送金币
		/// </summary>
		public long PresentScore
		{
			get { return m_presentScore; }
			set { m_presentScore = value; }
		}

		/// <summary>
		/// 购买前货币
		/// </summary>
		public decimal BeforeCurrency
		{
			get { return m_beforeCurrency; }
			set { m_beforeCurrency = value; }
		}

		/// <summary>
		/// 购买前金币
		/// </summary>
		public long BeforeScore
		{
			get { return m_beforeScore; }
			set { m_beforeScore = value; }
		}

		/// <summary>
		/// 客户端IP
		/// </summary>
		public string ClinetIP
		{
			get { return m_clinetIP; }
			set { m_clinetIP = value; }
		}

		/// <summary>
		/// 兑换时间
		/// </summary>
		public DateTime InputDate
		{
			get { return m_inputDate; }
			set { m_inputDate = value; }
		}
		#endregion
	}
}
