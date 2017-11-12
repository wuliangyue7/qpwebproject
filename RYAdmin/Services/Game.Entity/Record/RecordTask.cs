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
	/// 实体类 RecordTask。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class RecordTask  
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "RecordTask" ;

		/// <summary>
		/// 记录标识
		/// </summary>
		public const string _RecordID = "RecordID" ;

		/// <summary>
		/// 日期标识
		/// </summary>
		public const string _DateID = "DateID" ;

		/// <summary>
		/// 用户标识
		/// </summary>
		public const string _UserID = "UserID" ;

		/// <summary>
		/// 任务标识
		/// </summary>
		public const string _TaskID = "TaskID" ;

		/// <summary>
		/// 奖励金币
		/// </summary>
		public const string _AwardGold = "AwardGold" ;

		/// <summary>
		/// 奖励元宝
		/// </summary>
		public const string _AwardMedal = "AwardMedal" ;

		/// <summary>
		/// 
		/// </summary>
		public const string _InputDate = "InputDate" ;
		#endregion

		#region 私有变量
		private int m_recordID;				//记录标识
		private int m_dateID;				//日期标识
		private int m_userID;				//用户标识
		private int m_taskID;				//任务标识
		private int m_awardGold;			//奖励金币
		private int m_awardMedal;			//奖励元宝
		private DateTime m_inputDate;		//
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化RecordTask
		/// </summary>
		public RecordTask()
		{
			m_recordID=0;
			m_dateID=0;
			m_userID=0;
			m_taskID=0;
			m_awardGold=0;
			m_awardMedal=0;
			m_inputDate=DateTime.Now;
		}

		#endregion

		#region 公共属性

		/// <summary>
		/// 记录标识
		/// </summary>
		public int RecordID
		{
			get { return m_recordID; }
			set { m_recordID = value; }
		}

		/// <summary>
		/// 日期标识
		/// </summary>
		public int DateID
		{
			get { return m_dateID; }
			set { m_dateID = value; }
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
		/// 任务标识
		/// </summary>
		public int TaskID
		{
			get { return m_taskID; }
			set { m_taskID = value; }
		}

		/// <summary>
		/// 奖励金币
		/// </summary>
		public int AwardGold
		{
			get { return m_awardGold; }
			set { m_awardGold = value; }
		}

		/// <summary>
		/// 奖励元宝
		/// </summary>
		public int AwardMedal
		{
			get { return m_awardMedal; }
			set { m_awardMedal = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public DateTime InputDate
		{
			get { return m_inputDate; }
			set { m_inputDate = value; }
		}
		#endregion
	}
}
