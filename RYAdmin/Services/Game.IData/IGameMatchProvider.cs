using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Game.Kernel;
using Game.Entity.GameMatch;
using System.Data;
using System.Data.Common;

namespace Game.IData
{
    public interface IGameMatchProvider
    {

        #region 比赛相关

        /// <summary>
        /// 获取比赛介绍实体
        /// </summary>
        /// <param name="matchID"></param>
        /// <returns></returns>
        MatchInfo GetMatchInfo(int matchID);

        /// <summary>
        /// 更新比赛介绍
        /// </summary>
        /// <param name="matchInfo"></param>
        void UpdateMatchInfo(MatchInfo matchInfo);

        /// <summary>
        /// 获取比赛配置实体
        /// </summary>
        /// <param name="matchID"></param>
        /// <returns></returns>
        MatchPublic GetMatchPublicInfo(int matchID);

        /// <summary>
        /// 获取所有比赛数据集
        /// </summary>
        /// <returns>比赛数据集</returns>
        DataSet GetAllMatch();

         /// <summary>
        /// 根据比赛类型获取比赛列表
        /// </summary>
        /// <param name="matchType"></param>
        /// <returns></returns>
        DataSet GetMatchListByMatchType(byte matchType);

        /// <summary>
        /// 根据比赛ID获取比赛列表
        /// </summary>
        /// <param name="matchId"></param>
        /// <returns></returns>
        DataSet GetMatchListByMatchID(int matchId);
        #endregion

        #region 比赛排名

        /// <summary>
        /// 获取定时赛排名记录
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="condition"></param>
        /// <param name="orderby"></param>
        PagerSet GetTimingMatchHistoryGroup(byte matchType,int pageIndex,int pageSize,string condition,string orderby); 

        #endregion

        #region 比赛奖励

        /// <summary>
        /// 获取比赛奖励配置
        /// </summary>
        /// <param name="matchId"></param>
        /// <returns></returns>
        DataSet GetMatchRewardList(int matchId);

        #endregion

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
        PagerSet GetList(string tableName,int pageIndex,int pageSize,string condition,string orderby);

        /// <summary>
        /// 执行SQL语句返回受影响的行数
        /// </summary>
        /// <param name="sql"></param>
        int ExecuteSql(string sql);

        #endregion
    }
}
