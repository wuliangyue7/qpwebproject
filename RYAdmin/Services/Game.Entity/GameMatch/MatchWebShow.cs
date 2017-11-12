/*
 * 版本：4.0
 * 时间：2014-5-19
 * 作者：http://www.foxuc.com
 *
 * 描述：实体类
 *
 */

using System;
using System.Collections.Generic;

namespace Game.Entity.GameMatch
{
	/// <summary>
	/// 实体类 MatchWebShow。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class MatchWebShow  
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "MatchWebShow" ;

		/// <summary>
		/// 比赛标识
		/// </summary>
		public const string _MatchID = "MatchID" ;

		/// <summary>
		/// 比赛场次
		/// </summary>
		public const string _MatchNo = "MatchNo" ;

		/// <summary>
		/// 小图地址
		/// </summary>
        public const string _ImageUrl = "ImageUrl";

		/// <summary>
		/// 大图地址
		/// </summary>
		public const string _BigImageUrl = "BigImageUrl" ;

		/// <summary>
		/// 比赛摘要
		/// </summary>
		public const string _MatchSummary = "MatchSummary" ;

		/// <summary>
		/// 比赛说明
		/// </summary>
		public const string _MatchContent = "MatchContent" ;

		/// <summary>
		/// 是否推荐至首页
		/// </summary>
		public const string _IsRecommend = "IsRecommend" ;

        public const string _MatchStatus = "MatchStatus";
		#endregion

		#region 私有变量
		private int m_matchID;					//比赛标识
		private short m_matchNo;				//比赛场次
		private string m_imageUrl;			//小图地址
		private string m_bigImageUrl;			//大图地址
		private string m_matchSummary;			//比赛摘要
		private string m_matchContent;			//比赛说明
		private bool m_isRecommend;				//是否推荐至首页
        private int m_matchStatus;
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化MatchWebShow
		/// </summary>
		public MatchWebShow()
		{
			m_matchID=0;
			m_matchNo=0;
            m_imageUrl = "";
			m_bigImageUrl="";
			m_matchSummary="";
			m_matchContent="";
			m_isRecommend=false;
            m_matchStatus = 0;
		}

		#endregion

		#region 公共属性

		/// <summary>
		/// 比赛标识
		/// </summary>
		public int MatchID
		{
			get { return m_matchID; }
			set { m_matchID = value; }
		}

		/// <summary>
		/// 比赛场次
		/// </summary>
		public short MatchNo
		{
			get { return m_matchNo; }
			set { m_matchNo = value; }
		}

		/// <summary>
		/// 小图地址
		/// </summary>
		public string ImageUrl
		{
            get { return m_imageUrl; }
            set { m_imageUrl = value; }
		}

		/// <summary>
		/// 大图地址
		/// </summary>
		public string BigImageUrl
		{
			get { return m_bigImageUrl; }
			set { m_bigImageUrl = value; }
		}

		/// <summary>
		/// 比赛摘要
		/// </summary>
		public string MatchSummary
		{
			get { return m_matchSummary; }
			set { m_matchSummary = value; }
		}

		/// <summary>
		/// 比赛说明
		/// </summary>
		public string MatchContent
		{
			get { return m_matchContent; }
			set { m_matchContent = value; }
		}

		/// <summary>
		/// 是否推荐至首页
		/// </summary>
		public bool IsRecommend
		{
			get { return m_isRecommend; }
			set { m_isRecommend = value; }
		}

        public int MatchStatus
        {
            get { return m_matchStatus; }
            set { m_matchStatus = value; }
        }
		#endregion
	}
}
