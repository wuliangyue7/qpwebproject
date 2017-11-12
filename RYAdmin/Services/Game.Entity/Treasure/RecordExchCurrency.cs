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
	/// 实体类 RecordExchCurrency。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class RecordExchCurrency  
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "RecordExchCurrency" ;

		/// <summary>
		/// 日志标识
		/// </summary>
		public const string _RecordID = "RecordID" ;

		/// <summary>
		/// 商品标识
		/// </summary>
		public const string _CardID = "CardID" ;

		/// <summary>
		/// 商品名称
		/// </summary>
		public const string _CardName = "CardName" ;

		/// <summary>
		/// 兑换价格
		/// </summary>
		public const string _CardPrice = "CardPrice" ;

		/// <summary>
		/// 兑换数量
		/// </summary>
		public const string _ExchNum = "ExchNum" ;

		/// <summary>
		/// 用户标识
		/// </summary>
		public const string _UserID = "UserID" ;

		/// <summary>
		/// 花费货币
		/// </summary>
		public const string _ExchCurrency = "ExchCurrency" ;

		/// <summary>
		/// 赠送金币
		/// </summary>
		public const string _PresentScore = "PresentScore" ;

		/// <summary>
		/// 换前货币
		/// </summary>
		public const string _BeforeCurrency = "BeforeCurrency" ;

		/// <summary>
		/// 换前金币
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
		private int m_cardID;						//商品标识
		private string m_cardName;					//商品名称
		private decimal m_cardPrice;				//兑换价格
		private int m_exchNum;						//兑换数量
		private int m_userID;						//用户标识
		private decimal m_exchCurrency;				//花费货币
		private long m_presentScore;				//赠送金币
		private decimal m_beforeCurrency;			//换前货币
		private long m_beforeScore;					//换前金币
		private string m_clinetIP;					//客户端IP
		private DateTime m_inputDate;				//兑换时间
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化RecordExchCurrency
		/// </summary>
		public RecordExchCurrency()
		{
			m_recordID=0;
			m_cardID=0;
			m_cardName="";
			m_cardPrice=0;
			m_exchNum=0;
			m_userID=0;
			m_exchCurrency=0;
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
		/// 商品标识
		/// </summary>
		public int CardID
		{
			get { return m_cardID; }
			set { m_cardID = value; }
		}

		/// <summary>
		/// 商品名称
		/// </summary>
		public string CardName
		{
			get { return m_cardName; }
			set { m_cardName = value; }
		}

		/// <summary>
		/// 兑换价格
		/// </summary>
		public decimal CardPrice
		{
			get { return m_cardPrice; }
			set { m_cardPrice = value; }
		}

		/// <summary>
		/// 兑换数量
		/// </summary>
		public int ExchNum
		{
			get { return m_exchNum; }
			set { m_exchNum = value; }
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
		/// 花费货币
		/// </summary>
		public decimal ExchCurrency
		{
			get { return m_exchCurrency; }
			set { m_exchCurrency = value; }
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
		/// 换前货币
		/// </summary>
		public decimal BeforeCurrency
		{
			get { return m_beforeCurrency; }
			set { m_beforeCurrency = value; }
		}

		/// <summary>
		/// 换前金币
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
