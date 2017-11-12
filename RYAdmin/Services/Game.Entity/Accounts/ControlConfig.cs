/*
 * 版本：4.0
 * 时间：2014/8/18
 * 作者：http://www.foxuc.com
 *
 * 描述：实体类
 *
 */

using System;
using System.Collections.Generic;

namespace Game.Entity.Accounts
{
	/// <summary>
	/// 实体类 ControlConfig。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class ControlConfig  
	{
		#region 常量

		/// <summary>
		/// 表名
		/// </summary>
		public const string Tablename = "ControlConfig" ;

		/// <summary>
		/// 是否允许自动控制
		/// </summary>
		public const string _AutoControlEnable = "AutoControlEnable" ;

		/// <summary>
		/// 黑名单持续时间
		/// </summary>
		public const string _BSustainedTimeCount = "BSustainedTimeCount" ;

		/// <summary>
		/// 白名单持续时间
		/// </summary>
		public const string _WSustainedTimeCount = "WSustainedTimeCount" ;

		/// <summary>
		/// 加入黑名单累计赢分
		/// </summary>
		public const string _JoinBlackWinScore = "JoinBlackWinScore" ;

		/// <summary>
		/// 加入白名单累计输分
		/// </summary>
		public const string _JoinWhiteLoseScore = "JoinWhiteLoseScore" ;

		/// <summary>
		/// 黑名单用户结束方式
		/// </summary>
		public const string _BlackControlType = "BlackControlType" ;

		/// <summary>
		/// 白名单用户结束方式
		/// </summary>
		public const string _WhiteControlType = "WhiteControlType" ;

		/// <summary>
		/// 退出黑名单累计输分
		/// </summary>
		public const string _QuitBlackLoseScore = "QuitBlackLoseScore" ;

		/// <summary>
		/// 退出白名单累计赢分
		/// </summary>
		public const string _QuitWhiteWinScore = "QuitWhiteWinScore" ;

		/// <summary>
		/// 黑名单胜率
		/// </summary>
		public const string _BlackWinRate = "BlackWinRate" ;

		/// <summary>
		/// 白名单胜率
		/// </summary>
		public const string _WhiteWinRate = "WhiteWinRate" ;
		#endregion

		#region 私有变量
		private byte m_autoControlEnable;			//是否允许自动控制
		private int m_bSustainedTimeCount;			//黑名单持续时间
		private int m_wSustainedTimeCount;			//白名单持续时间
		private long m_joinBlackWinScore;			//加入黑名单累计赢分
		private long m_joinWhiteLoseScore;			//加入白名单累计输分
		private short m_blackControlType;			//黑名单用户结束方式
		private short m_whiteControlType;			//白名单用户结束方式
		private long m_quitBlackLoseScore;			//退出黑名单累计输分
		private long m_quitWhiteWinScore;			//退出白名单累计赢分
		private short m_blackWinRate;				//黑名单胜率
		private short m_whiteWinRate;				//白名单胜率
		#endregion

		#region 构造方法

		/// <summary>
		/// 初始化ControlConfig
		/// </summary>
		public ControlConfig()
		{
			m_autoControlEnable=0;
			m_bSustainedTimeCount=0;
			m_wSustainedTimeCount=0;
			m_joinBlackWinScore=0;
			m_joinWhiteLoseScore=0;
			m_blackControlType=0;
			m_whiteControlType=0;
			m_quitBlackLoseScore=0;
			m_quitWhiteWinScore=0;
			m_blackWinRate=0;
			m_whiteWinRate=0;
		}

		#endregion

		#region 公共属性

		/// <summary>
		/// 是否允许自动控制
		/// </summary>
		public byte AutoControlEnable
		{
			get { return m_autoControlEnable; }
			set { m_autoControlEnable = value; }
		}

		/// <summary>
		/// 黑名单持续时间
		/// </summary>
		public int BSustainedTimeCount
		{
			get { return m_bSustainedTimeCount; }
			set { m_bSustainedTimeCount = value; }
		}

		/// <summary>
		/// 白名单持续时间
		/// </summary>
		public int WSustainedTimeCount
		{
			get { return m_wSustainedTimeCount; }
			set { m_wSustainedTimeCount = value; }
		}

		/// <summary>
		/// 加入黑名单累计赢分
		/// </summary>
		public long JoinBlackWinScore
		{
			get { return m_joinBlackWinScore; }
			set { m_joinBlackWinScore = value; }
		}

		/// <summary>
		/// 加入白名单累计输分
		/// </summary>
		public long JoinWhiteLoseScore
		{
			get { return m_joinWhiteLoseScore; }
			set { m_joinWhiteLoseScore = value; }
		}

		/// <summary>
		/// 黑名单用户结束方式
		/// </summary>
		public short BlackControlType
		{
			get { return m_blackControlType; }
			set { m_blackControlType = value; }
		}

		/// <summary>
		/// 白名单用户结束方式
		/// </summary>
		public short WhiteControlType
		{
			get { return m_whiteControlType; }
			set { m_whiteControlType = value; }
		}

		/// <summary>
		/// 退出黑名单累计输分
		/// </summary>
		public long QuitBlackLoseScore
		{
			get { return m_quitBlackLoseScore; }
			set { m_quitBlackLoseScore = value; }
		}

		/// <summary>
		/// 退出白名单累计赢分
		/// </summary>
		public long QuitWhiteWinScore
		{
			get { return m_quitWhiteWinScore; }
			set { m_quitWhiteWinScore = value; }
		}

		/// <summary>
		/// 黑名单胜率
		/// </summary>
		public short BlackWinRate
		{
			get { return m_blackWinRate; }
			set { m_blackWinRate = value; }
		}

		/// <summary>
		/// 白名单胜率
		/// </summary>
		public short WhiteWinRate
		{
			get { return m_whiteWinRate; }
			set { m_whiteWinRate = value; }
		}
		#endregion
	}
}
