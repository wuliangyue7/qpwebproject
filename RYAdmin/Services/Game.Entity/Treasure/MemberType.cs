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
	/// 实体类 MemberType。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class MemberType  
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "MemberType" ;

		/// <summary>
		/// 会员等级
		/// </summary>
		public const string _MemberOrder = "MemberOrder" ;

		/// <summary>
		/// 会员名称
		/// </summary>
		public const string _MemberName = "MemberName" ;

		/// <summary>
		/// 会员每月价格（单位：货币）
		/// </summary>
		public const string _MemberPrice = "MemberPrice" ;

		/// <summary>
		/// 购买单个赠送游戏币数
		/// </summary>
		public const string _PresentScore = "PresentScore" ;

		/// <summary>
		/// 用户权限
		/// </summary>
		public const string _UserRight = "UserRight" ;
		#endregion

		#region 私有变量
		private byte m_memberOrder;			//会员等级
		private string m_memberName;		//会员名称
		private decimal m_memberPrice;		//会员每月价格（单位：货币）
		private int m_presentScore;			//购买单个赠送游戏币数
		private int m_userRight;			//用户权限
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化MemberType
		/// </summary>
		public MemberType()
		{
			m_memberOrder=0;
			m_memberName="";
			m_memberPrice=0;
			m_presentScore=0;
			m_userRight=0;
		}

		#endregion

		#region 公共属性

		/// <summary>
		/// 会员等级
		/// </summary>
		public byte MemberOrder
		{
			get { return m_memberOrder; }
			set { m_memberOrder = value; }
		}

		/// <summary>
		/// 会员名称
		/// </summary>
		public string MemberName
		{
			get { return m_memberName; }
			set { m_memberName = value; }
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
		/// 购买单个赠送游戏币数
		/// </summary>
		public int PresentScore
		{
			get { return m_presentScore; }
			set { m_presentScore = value; }
		}

		/// <summary>
		/// 用户权限
		/// </summary>
		public int UserRight
		{
			get { return m_userRight; }
			set { m_userRight = value; }
		}
		#endregion
	}
}
