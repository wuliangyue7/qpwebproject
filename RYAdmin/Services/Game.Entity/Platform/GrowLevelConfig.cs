/*
 * 版本：4.0
 * 时间：2014-1-24
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
	/// 实体类 GrowLevelConfig。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class GrowLevelConfig  
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "GrowLevelConfig" ;

		/// <summary>
		/// 等级编号
		/// </summary>
		public const string _LevelID = "LevelID" ;

		/// <summary>
		/// 所需经验值
		/// </summary>
		public const string _Experience = "Experience" ;

		/// <summary>
		/// 奖励金币
		/// </summary>
		public const string _RewardGold = "RewardGold" ;

		/// <summary>
		/// 奖励元宝
		/// </summary>
		public const string _RewardMedal = "RewardMedal" ;

		/// <summary>
		/// 等级备注
		/// </summary>
		public const string _LevelRemark = "LevelRemark" ;
		#endregion

		#region 私有变量
		private int m_levelID;				//等级编号
		private int m_experience;			//所需经验值
		private int m_rewardGold;			//奖励金币
		private int m_rewardMedal;			//奖励元宝
		private string m_levelRemark;		//等级备注
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化GrowLevelConfig
		/// </summary>
		public GrowLevelConfig()
		{
			m_levelID=0;
			m_experience=0;
			m_rewardGold=0;
			m_rewardMedal=0;
			m_levelRemark="";
		}

		#endregion

		#region 公共属性

		/// <summary>
		/// 等级编号
		/// </summary>
		public int LevelID
		{
			get { return m_levelID; }
			set { m_levelID = value; }
		}

		/// <summary>
		/// 所需经验值
		/// </summary>
		public int Experience
		{
			get { return m_experience; }
			set { m_experience = value; }
		}

		/// <summary>
		/// 奖励金币
		/// </summary>
		public int RewardGold
		{
			get { return m_rewardGold; }
			set { m_rewardGold = value; }
		}

		/// <summary>
		/// 奖励元宝
		/// </summary>
		public int RewardMedal
		{
			get { return m_rewardMedal; }
			set { m_rewardMedal = value; }
		}

		/// <summary>
		/// 等级备注
		/// </summary>
		public string LevelRemark
		{
			get { return m_levelRemark; }
			set { m_levelRemark = value; }
		}
		#endregion
	}
}
