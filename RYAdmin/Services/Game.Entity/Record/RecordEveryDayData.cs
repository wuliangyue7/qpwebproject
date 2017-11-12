/*
 * 版本：4.0
 * 时间：2014-3-7
 * 作者：http://www.foxuc.com
 *
 * 描述：实体类
 *
 */

using System;
using System.Collections.Generic;

namespace Game.Entity.Record
{
	/// <summary>
	/// 实体类 RecordEveryDayData。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class RecordEveryDayData  
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "RecordEveryDayData" ;

		/// <summary>
		/// 日期标识
		/// </summary>
		public const string _DateID = "DateID" ;

		/// <summary>
		/// 玩家总数
		/// </summary>
		public const string _UserTotal = "UserTotal" ;

		/// <summary>
		/// 充值玩家
		/// </summary>
		public const string _PayUserTotal = "PayUserTotal" ;

		/// <summary>
		/// 活跃用户
		/// </summary>
		public const string _ActiveUserTotal = "ActiveUserTotal" ;

		/// <summary>
		/// 用户流失
		/// </summary>
		public const string _LossUserTotal = "LossUserTotal" ;

		/// <summary>
		/// 充值用户流失
		/// </summary>
		public const string _LossPayUserTotal = "LossPayUserTotal" ;

		/// <summary>
		/// 充值金额
		/// </summary>
		public const string _PayTotalAmount = "PayTotalAmount" ;

		/// <summary>
		/// 充值货币金额
		/// </summary>
		public const string _PayAmountForCurrency = "PayAmountForCurrency" ;

		/// <summary>
		/// 金币总数
		/// </summary>
		public const string _GoldTotal = "GoldTotal" ;

		/// <summary>
		/// 平台币总数
		/// </summary>
		public const string _CurrencyTotal = "CurrencyTotal" ;

		/// <summary>
		/// 当日游戏税收
		/// </summary>
		public const string _GameTax = "GameTax" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _GameTaxTotal = "GameTaxTotal" ;

		/// <summary>
		/// 当日银行税收
		/// </summary>
		public const string _BankTax = "BankTax" ;

		/// <summary>
		/// 当日损耗
		/// </summary>
		public const string _Waste = "Waste" ;

		/// <summary>
		/// 平均在线时长
		/// </summary>
		public const string _UserAVGOnlineTime = "UserAVGOnlineTime" ;

		/// <summary>
		/// 统计时间
		/// </summary>
		public const string _CollectDate = "CollectDate" ;
		#endregion

		#region 私有变量
		private int m_dateID;						//日期标识
		private int m_userTotal;					//玩家总数
		private int m_payUserTotal;					//充值玩家
		private int m_activeUserTotal;				//活跃用户
		private int m_lossUserTotal;				//用户流失
		private int m_lossPayUserTotal;				//充值用户流失
		private int m_payTotalAmount;				//充值金额
		private int m_payAmountForCurrency;			//充值货币金额
		private long m_goldTotal;					//金币总数
		private long m_currencyTotal;				//平台币总数
		private long m_gameTax;						//当日游戏税收
		private long m_gameTaxTotal;				//
		private long m_bankTax;						//当日银行税收
		private long m_waste;						//当日损耗
		private int m_userAVGOnlineTime;			//平均在线时长
		private DateTime m_collectDate;				//统计时间
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化RecordEveryDayData
		/// </summary>
		public RecordEveryDayData()
		{
			m_dateID=0;
			m_userTotal=0;
			m_payUserTotal=0;
			m_activeUserTotal=0;
			m_lossUserTotal=0;
			m_lossPayUserTotal=0;
			m_payTotalAmount=0;
			m_payAmountForCurrency=0;
			m_goldTotal=0;
			m_currencyTotal=0;
			m_gameTax=0;
			m_gameTaxTotal=0;
			m_bankTax=0;
			m_waste=0;
			m_userAVGOnlineTime=0;
			m_collectDate=DateTime.Now;
		}

		#endregion

		#region 公共属性

		/// <summary>
		/// 日期标识
		/// </summary>
		public int DateID
		{
			get { return m_dateID; }
			set { m_dateID = value; }
		}

		/// <summary>
		/// 玩家总数
		/// </summary>
		public int UserTotal
		{
			get { return m_userTotal; }
			set { m_userTotal = value; }
		}

		/// <summary>
		/// 充值玩家
		/// </summary>
		public int PayUserTotal
		{
			get { return m_payUserTotal; }
			set { m_payUserTotal = value; }
		}

		/// <summary>
		/// 活跃用户
		/// </summary>
		public int ActiveUserTotal
		{
			get { return m_activeUserTotal; }
			set { m_activeUserTotal = value; }
		}

		/// <summary>
		/// 用户流失
		/// </summary>
		public int LossUserTotal
		{
			get { return m_lossUserTotal; }
			set { m_lossUserTotal = value; }
		}

		/// <summary>
		/// 充值用户流失
		/// </summary>
		public int LossPayUserTotal
		{
			get { return m_lossPayUserTotal; }
			set { m_lossPayUserTotal = value; }
		}

		/// <summary>
		/// 充值金额
		/// </summary>
		public int PayTotalAmount
		{
			get { return m_payTotalAmount; }
			set { m_payTotalAmount = value; }
		}

		/// <summary>
		/// 充值货币金额
		/// </summary>
		public int PayAmountForCurrency
		{
			get { return m_payAmountForCurrency; }
			set { m_payAmountForCurrency = value; }
		}

		/// <summary>
		/// 金币总数
		/// </summary>
		public long GoldTotal
		{
			get { return m_goldTotal; }
			set { m_goldTotal = value; }
		}

		/// <summary>
		/// 平台币总数
		/// </summary>
		public long CurrencyTotal
		{
			get { return m_currencyTotal; }
			set { m_currencyTotal = value; }
		}

		/// <summary>
		/// 当日游戏税收
		/// </summary>
		public long GameTax
		{
			get { return m_gameTax; }
			set { m_gameTax = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public long GameTaxTotal
		{
			get { return m_gameTaxTotal; }
			set { m_gameTaxTotal = value; }
		}

		/// <summary>
		/// 当日银行税收
		/// </summary>
		public long BankTax
		{
			get { return m_bankTax; }
			set { m_bankTax = value; }
		}

		/// <summary>
		/// 当日损耗
		/// </summary>
		public long Waste
		{
			get { return m_waste; }
			set { m_waste = value; }
		}

		/// <summary>
		/// 平均在线时长
		/// </summary>
		public int UserAVGOnlineTime
		{
			get { return m_userAVGOnlineTime; }
			set { m_userAVGOnlineTime = value; }
		}

		/// <summary>
		/// 统计时间
		/// </summary>
		public DateTime CollectDate
		{
			get { return m_collectDate; }
			set { m_collectDate = value; }
		}
		#endregion
	}
}
