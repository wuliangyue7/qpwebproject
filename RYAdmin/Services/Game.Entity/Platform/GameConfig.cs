/*
 * 版本：4.0
 * 时间：2014-3-28
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
	/// 实体类 GameConfig。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class GameConfig  
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "GameConfig" ;

		/// <summary>
		/// 类型标识
		/// </summary>
		public const string _KindID = "KindID" ;

		/// <summary>
		/// 显示输赢公告的最低输赢金币数
		/// </summary>
		public const string _NoticeChangeGolds = "NoticeChangeGolds" ;

		/// <summary>
		/// 赢一局经验
		/// </summary>
		public const string _WinExperience = "WinExperience" ;
		#endregion

		#region 私有变量
		private int m_kindID;						//类型标识
		private long m_noticeChangeGolds;			//显示输赢公告的最低输赢金币数
		private int m_winExperience;				//赢一局经验
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化GameConfig
		/// </summary>
		public GameConfig()
		{
			m_kindID=0;
			m_noticeChangeGolds=0;
			m_winExperience=0;
		}

		#endregion

		#region 公共属性

		/// <summary>
		/// 类型标识
		/// </summary>
		public int KindID
		{
			get { return m_kindID; }
			set { m_kindID = value; }
		}

		/// <summary>
		/// 显示输赢公告的最低输赢金币数
		/// </summary>
		public long NoticeChangeGolds
		{
			get { return m_noticeChangeGolds; }
			set { m_noticeChangeGolds = value; }
		}

		/// <summary>
		/// 赢一局经验
		/// </summary>
		public int WinExperience
		{
			get { return m_winExperience; }
			set { m_winExperience = value; }
		}
		#endregion
	}
}
