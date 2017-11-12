using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Game.Data.Factory;
using Game.IData;
using Game.Kernel;
using Game.Utils;
using Game.Entity.Accounts;
using System.Data;
using System.Data.Common;
using Game.Entity.GameMatch;

namespace Game.Facade
{
    /// <summary>
    /// 网站外观
    /// </summary>
    public class GameMatchFacade
    {

        #region Fields

        private IGameMatchProvider gameMatchData;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public GameMatchFacade()
        {
            gameMatchData = ClassFactory.GetIGameMatchProvider();
        }

        #endregion 构造函数

        #region 公共

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="condition"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public PagerSet GetList(string tableName,int pageIndex,int pageSize,string condition,string orderby)
        {
            return gameMatchData.GetList(tableName,pageIndex,pageSize,condition,orderby);
        }

        /// <summary>
        /// 执行SQL语句返回受影响的行数
        /// </summary>
        /// <param name="sql"></param>
        public int ExecuteSql(string sql)
        {
            return gameMatchData.ExecuteSql(sql);
        }

        #endregion

        #region 比赛相关

        /// <summary>
        /// 获取比赛介绍实体
        /// </summary>
        /// <param name="matchID"></param>
        /// <returns></returns>
        public MatchInfo GetMatchInfo(int matchID)
        {
            return gameMatchData.GetMatchInfo(matchID);
        }

        /// <summary>
        /// 更新比赛介绍
        /// </summary>
        /// <param name="matchInfo"></param>
        public Message UpdateMatchInfo(MatchInfo matchInfo)
        {
            try
            {
                gameMatchData.UpdateMatchInfo(matchInfo);
                return new Message(true);
            }
            catch { return new Message(false); }
        }

        /// <summary>
        /// 获取比赛配置实体
        /// </summary>
        /// <param name="matchID"></param>
        /// <returns></returns>
        public MatchPublic GetMatchPublicInfo(int matchID)
        {
            return gameMatchData.GetMatchPublicInfo(matchID);
        }

        /// <summary>
        /// 获取所有比赛数据集
        /// </summary>
        /// <returns>比赛数据集</returns>
        public DataSet GetAllMatch()
        {
            return gameMatchData.GetAllMatch();
        }

        /// <summary>
        /// 根据比赛ID获取比赛列表
        /// </summary>
        /// <param name="matchId"></param>
        /// <returns></returns>
        public DataSet GetMatchListByMatchType(byte matchType)
        {
            return gameMatchData.GetMatchListByMatchType(matchType);
        }

        /// <summary>
        /// 根据比赛ID获取比赛列表
        /// </summary>
        /// <param name="matchId"></param>
        /// <returns></returns>
        public DataSet GetMatchListByMatchID(int matchId)
        {
            return gameMatchData.GetMatchListByMatchID(matchId);
        }   

        /// <summary>
        /// 获取定时赛排名记录
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="condition"></param>
        /// <param name="orderby"></param>
        public PagerSet GetTimingMatchHistoryGroup(byte matchType,int pageIndex,int pageSize,string condition,string orderby)
        {
            return gameMatchData.GetTimingMatchHistoryGroup(matchType,pageIndex,pageSize,condition,orderby);
        }
        #endregion

        #region 比赛奖励

        /// <summary>
        /// 获取比赛奖励
        /// </summary>
        /// <param name="matchId"></param>
        /// <returns></returns>
        public DataSet GetMatchRewardList(int matchId)
        {
            return gameMatchData.GetMatchRewardList(matchId);
        }

        #endregion

    }
}
