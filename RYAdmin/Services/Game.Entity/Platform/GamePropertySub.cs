/*
 * 版本：4.0
 * 时间：2016/1/13
 * 作者：http://www.foxuc.com
 *
 * 描述：实体类
 *
 */

using System;
using System.Collections.Generic;

namespace Game.Entity.Platform
{
	/// <summary>
	/// 实体类 GamePropertySub。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class GamePropertySub  
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "GamePropertySub" ;

		/// <summary>
		/// 道具ID
		/// </summary>
		public const string _ID = "ID" ;

		/// <summary>
		/// 道具归属ID
		/// </summary>
		public const string _OwnerID = "OwnerID" ;

		/// <summary>
		/// 道具数量
		/// </summary>
		public const string _Count = "Count" ;

		/// <summary>
		/// 排序ID
		/// </summary>
		public const string _SortID = "SortID" ;
		#endregion

		#region 私有变量
		private int m_iD;				//道具ID
		private int m_ownerID;			//道具归属ID
		private int m_count;			//道具数量
		private int m_sortID;			//排序ID
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化GamePropertySub
		/// </summary>
		public GamePropertySub()
		{
			m_iD=0;
			m_ownerID=0;
			m_count=0;
			m_sortID=0;
		}

		#endregion

		#region 公共属性

		/// <summary>
		/// 道具ID
		/// </summary>
		public int ID
		{
			get { return m_iD; }
			set { m_iD = value; }
		}

		/// <summary>
		/// 道具归属ID
		/// </summary>
		public int OwnerID
		{
			get { return m_ownerID; }
			set { m_ownerID = value; }
		}

		/// <summary>
		/// 道具数量
		/// </summary>
		public int Count
		{
			get { return m_count; }
			set { m_count = value; }
		}

		/// <summary>
		/// 排序ID
		/// </summary>
		public int SortID
		{
			get { return m_sortID; }
			set { m_sortID = value; }
		}
		#endregion
	}
}
