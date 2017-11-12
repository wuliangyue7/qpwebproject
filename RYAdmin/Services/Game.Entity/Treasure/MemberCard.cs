/*
 * 版本：4.0
 * 时间：2014-3-14
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
	/// 实体类 MemberCard。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class MemberCard  
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "MemberCard" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _CardID = "CardID" ;

		/// <summary>
		/// 会员卡名称
		/// </summary>
		public const string _CardName = "CardName" ;

		/// <summary>
		/// 会员卡价格
		/// </summary>
		public const string _CardPrice = "CardPrice" ;

		/// <summary>
		/// 赠送金币数
		/// </summary>
		public const string _PresentScore = "PresentScore" ;

		/// <summary>
		/// 会员等级
		/// </summary>
		public const string _MemberOrder = "MemberOrder" ;

		/// <summary>
		/// 用户权限
		/// </summary>
		public const string _UserRight = "UserRight" ;

		/// <summary>
		/// 会员卡描述
		/// </summary>
		public const string _Describe = "Describe" ;
		#endregion

		#region 私有变量
		private int m_cardID;				//
		private string m_cardName;			//会员卡名称
		private int m_cardPrice;			//会员卡价格
		private int m_presentScore;			//赠送金币数
		private byte m_memberOrder;			//会员等级
		private int m_userRight;			//用户权限
		private string m_describe;			//会员卡描述
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化MemberCard
		/// </summary>
		public MemberCard()
		{
			m_cardID=0;
			m_cardName="";
			m_cardPrice=0;
			m_presentScore=0;
			m_memberOrder=0;
			m_userRight=0;
			m_describe="";
		}

		#endregion

		#region 公共属性

		/// <summary>
		/// 
		/// </summary>
		public int CardID
		{
			get { return m_cardID; }
			set { m_cardID = value; }
		}

		/// <summary>
		/// 会员卡名称
		/// </summary>
		public string CardName
		{
			get { return m_cardName; }
			set { m_cardName = value; }
		}

		/// <summary>
		/// 会员卡价格
		/// </summary>
		public int CardPrice
		{
			get { return m_cardPrice; }
			set { m_cardPrice = value; }
		}

		/// <summary>
		/// 赠送金币数
		/// </summary>
		public int PresentScore
		{
			get { return m_presentScore; }
			set { m_presentScore = value; }
		}

		/// <summary>
		/// 会员等级
		/// </summary>
		public byte MemberOrder
		{
			get { return m_memberOrder; }
			set { m_memberOrder = value; }
		}

		/// <summary>
		/// 用户权限
		/// </summary>
		public int UserRight
		{
			get { return m_userRight; }
			set { m_userRight = value; }
		}

		/// <summary>
		/// 会员卡描述
		/// </summary>
		public string Describe
		{
			get { return m_describe; }
			set { m_describe = value; }
		}
		#endregion
	}
}
