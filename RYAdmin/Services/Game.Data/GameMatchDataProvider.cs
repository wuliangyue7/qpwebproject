using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Game.Entity;
using Game.Kernel;

using Game.IData;
using System.Data;
using System.Data.Common;
using Game.Entity.GameMatch;
using System.Data.SqlClient;

namespace Game.Data
{
    public class GameMatchDataProvider : BaseDataProvider,IGameMatchProvider
    {
        #region Fields

        private ITableProvider aideMatchInfoProvider;
        private ITableProvider aideMatchPublicProvider;

        #endregion

        #region 构造方法

        public GameMatchDataProvider(string connString)
            : base(connString)
        {
            aideMatchInfoProvider = GetTableProvider(MatchInfo.Tablename);
            aideMatchPublicProvider = GetTableProvider(MatchPublic.Tablename);
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
            string sqlQuery = string.Format("(NOLOCK) WHERE MatchID= {0}", matchID);
            MatchInfo item = aideMatchInfoProvider.GetObject<MatchInfo>(sqlQuery);
            return item;
        }

        /// <summary>
        /// 更新比赛介绍
        /// </summary>
        /// <param name="matchInfo"></param>
        public void UpdateMatchInfo(MatchInfo matchInfo)
        {
            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.Append("UPDATE MatchInfo SET ")
                    .Append("MatchSummary=@MatchSummary, ")
                    .Append("MatchImage=@MatchImage, ")
                    .Append("MatchContent=@MatchContent, ")
                    .Append("SortID=@SortID ")
                    .Append("WHERE MatchID=@MatchID");

            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("MatchSummary", matchInfo.MatchSummary));
            prams.Add(Database.MakeInParam("MatchImage", matchInfo.MatchImage));
            prams.Add(Database.MakeInParam("MatchContent", matchInfo.MatchContent));
            prams.Add(Database.MakeInParam("SortID", matchInfo.SortID));
            prams.Add(Database.MakeInParam("MatchID", matchInfo.MatchID));

            Database.ExecuteNonQuery(CommandType.Text, sqlQuery.ToString(), prams.ToArray());
        }

        /// <summary>
        /// 获取比赛配置实体
        /// </summary>
        /// <param name="matchID"></param>
        /// <returns></returns>
        public MatchPublic GetMatchPublicInfo(int matchID)
        {
            string sqlQuery = string.Format("(NOLOCK) WHERE MatchID= {0}", matchID);
            MatchPublic item = aideMatchPublicProvider.GetObject<MatchPublic>(sqlQuery);
            return item;
        }

        /// <summary>
        /// 获取所有比赛数据集
        /// </summary>
        /// <returns>比赛数据集</returns>
        public DataSet GetAllMatch()
        {
            string sqlQuery = "SELECT * FROM MatchPublic";
            return Database.ExecuteDataset(sqlQuery);
        }

        /// <summary>
        /// 根据比赛类型获取比赛列表
        /// </summary>
        /// <param name="matchType"></param>
        /// <returns></returns>
        public DataSet GetMatchListByMatchType(byte matchType)
        {
            string sqlQuery = "SELECT * FROM MatchPublic WHERE MatchType=@MatchType";
            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("MatchType",matchType));
            return Database.ExecuteDataset(CommandType.Text,sqlQuery,prams.ToArray());
        }

        /// <summary>
        /// 根据比赛ID获取比赛列表
        /// </summary>
        /// <param name="matchId"></param>
        /// <returns></returns>
        public DataSet GetMatchListByMatchID(int matchId)
        {
            string sqlQuery = "SELECT * FROM MatchPublic WHERE MatchID=@MatchID";
            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("MatchID",matchId));
            return Database.ExecuteDataset(CommandType.Text,sqlQuery,prams.ToArray());
        }
        #endregion

        #region 比赛排名

        /// <summary>
        /// 获取定时赛排名记录
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="condition"></param>
        /// <param name="orderby"></param>
        public PagerSet GetTimingMatchHistoryGroup(byte matchType,int pageIndex,int pageSize,string condition,string orderby)
        {
            var param = new List<DbParameter>();
            param.Add(Database.MakeInParam("MatchType",matchType));
            param.Add(Database.MakeInParam("PageSize",pageSize));
            param.Add(Database.MakeInParam("PageIndex",pageIndex));
            param.Add(Database.MakeInParam("Where",condition));
            param.Add(Database.MakeInParam("OrderBy",orderby));
            param.Add(Database.MakeOutParam("PageCount",typeof(int)));
            param.Add(Database.MakeOutParam("RecordCount",typeof(int)));

            DataSet set;
            Database.RunProc("NET_PW_GetMatchHistoryGroup",param,out set);
            return new PagerSet(pageIndex,pageSize,Convert.ToInt32(param[param.Count - 3].Value),Convert.ToInt32(param[param.Count - 2].Value),set)
            {
                PageSet =
                {
                    DataSetName = "PagerSet"
                }
            };
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
            string sql = "SELECT * FROM MatchReward WHERE MatchID=@MatchID";
            List<DbParameter> param = new List<DbParameter>();
            param.Add(Database.MakeInParam("MatchID",matchId));
            return Database.ExecuteDataset(CommandType.Text,sql,param.ToArray());
        } 

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
        public PagerSet GetList(string tableName,int pageIndex,int pageSize,string condition,string orderby)
        {
            PagerParameters pagerPrams = new PagerParameters(tableName,orderby,condition,pageIndex,pageSize);
            return GetPagerSet2(pagerPrams);
        }

        /// <summary>
        /// 执行SQL语句返回受影响的行数
        /// </summary>
        /// <param name="sql"></param>
        public int ExecuteSql(string sql)
        {
            return Database.ExecuteNonQuery(sql);
        }

        #endregion
    }
}
