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
	/// 实体类 RecordEveryDayRoomData。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class RecordEveryDayRoomData  
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "RecordEveryDayRoomData" ;

		/// <summary>
		/// 日期标识
		/// </summary>
		public const string _DateID = "DateID" ;

		/// <summary>
		/// 游戏标识
		/// </summary>
		public const string _KindID = "KindID" ;

		/// <summary>
		/// 房间标识
		/// </summary>
		public const string _ServerID = "ServerID" ;

		/// <summary>
		/// 损耗
		/// </summary>
		public const string _Waste = "Waste" ;

		/// <summary>
		/// 税收
		/// </summary>
		public const string _Revenue = "Revenue" ;

		/// <summary>
		/// 奖牌
		/// </summary>
		public const string _Medal = "Medal" ;

		/// <summary>
		/// 录入日期
		/// </summary>
		public const string _CollectDate = "CollectDate" ;
		#endregion

		#region 私有变量
		private int m_dateID;					//日期标识
		private int m_kindID;					//游戏标识
		private int m_serverID;					//房间标识
		private long m_waste;					//损耗
		private long m_revenue;					//税收
		private int m_medal;					//奖牌
		private DateTime m_collectDate;			//录入日期
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化RecordEveryDayRoomData
		/// </summary>
		public RecordEveryDayRoomData()
		{
			m_dateID=0;
			m_kindID=0;
			m_serverID=0;
			m_waste=0;
			m_revenue=0;
			m_medal=0;
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
		/// 游戏标识
		/// </summary>
		public int KindID
		{
			get { return m_kindID; }
			set { m_kindID = value; }
		}

		/// <summary>
		/// 房间标识
		/// </summary>
		public int ServerID
		{
			get { return m_serverID; }
			set { m_serverID = value; }
		}

		/// <summary>
		/// 损耗
		/// </summary>
		public long Waste
		{
			get { return m_waste; }
			set { m_waste = value; }
		}

		/// <summary>
		/// 税收
		/// </summary>
		public long Revenue
		{
			get { return m_revenue; }
			set { m_revenue = value; }
		}

		/// <summary>
		/// 奖牌
		/// </summary>
		public int Medal
		{
			get { return m_medal; }
			set { m_medal = value; }
		}

		/// <summary>
		/// 录入日期
		/// </summary>
		public DateTime CollectDate
		{
			get { return m_collectDate; }
			set { m_collectDate = value; }
		}
		#endregion
	}
}
