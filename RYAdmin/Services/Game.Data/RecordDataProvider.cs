using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Game.Entity.Record;
using Game.IData;
using Game.Kernel;

namespace Game.Data
{
    /// <summary>
    /// 记录库数据层
    /// </summary>
    public class RecordDataProvider : BaseDataProvider, IRecordDataProvider
    {
        #region Fields

        private ITableProvider aideRecordAccountsExpendProvider;
        private ITableProvider aideRecordPasswdExpendProvider;
        private ITableProvider aideRecordGrantTreasureProvider;
        private ITableProvider aideRecordGrantMemberProvider;
        private ITableProvider aideRecordGrantExperienceProvider;
        private ITableProvider aideRecordGrantGameScoreProvider;
        private ITableProvider aideRecordGrantClearScoreProvider;
        private ITableProvider aideRecordGrantClearFleeProvider;
        private ITableProvider aideRecordConvertPresentProvider;
        private ITableProvider aideTaskRecordProvider;

        #endregion Fields

        #region 构造方法

        public RecordDataProvider( string connString )
            : base( connString )
        {
            aideRecordAccountsExpendProvider = GetTableProvider( RecordAccountsExpend.Tablename );
            aideRecordPasswdExpendProvider = GetTableProvider( RecordPasswdExpend.Tablename );
            aideRecordGrantTreasureProvider = GetTableProvider( RecordGrantTreasure.Tablename );
            aideRecordGrantMemberProvider = GetTableProvider( RecordGrantMember.Tablename );
            aideRecordGrantExperienceProvider = GetTableProvider( RecordGrantExperience.Tablename );
            aideRecordGrantGameScoreProvider = GetTableProvider( RecordGrantGameScore.Tablename );
            aideRecordGrantClearScoreProvider = GetTableProvider( RecordGrantClearScore.Tablename );
            aideRecordGrantClearFleeProvider = GetTableProvider( RecordGrantClearFlee.Tablename );
            aideRecordConvertPresentProvider = GetTableProvider( RecordConvertPresent.Tablename );
            aideTaskRecordProvider = GetTableProvider( RecordTask.Tablename );
        }

        #endregion 构造方法

        #region 历史修改用户名记录

        /// <summary>
        /// 分页获取历史修改用户名记录列表
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="condition">条件</param>
        /// <param name="orderby">排序</param>
        /// <returns></returns>
        public PagerSet GetRecordAccountsExpendList( int pageIndex, int pageSize, string condition, string orderby )
        {
            PagerParameters pagerPrams = new PagerParameters( RecordAccountsExpend.Tablename, orderby, condition, pageIndex, pageSize );
            return GetPagerSet2( pagerPrams );
        }

        /// <summary>
        /// 新增一条记录
        /// </summary>
        /// <param name="actExpend"></param>
        public void InsertRecordAccountsExpend( RecordAccountsExpend actExpend )
        {
            DataRow dr = aideRecordAccountsExpendProvider.NewRow();

            dr[RecordAccountsExpend._ReAccounts] = actExpend.ReAccounts;
            dr[RecordAccountsExpend._UserID] = actExpend.UserID;
            dr[RecordAccountsExpend._ClientIP] = actExpend.ClientIP;
            dr[RecordAccountsExpend._OperMasterID] = actExpend.OperMasterID;
            dr[RecordAccountsExpend._CollectDate] = DateTime.Now;
            aideRecordAccountsExpendProvider.Insert( dr );
        }

        /// <summary>
        /// 获取历史昵称或历史帐号
        /// </summary>
        /// <param name="userId">用户id</param>
        public Dictionary<int, string> GetOldNickNameOrAccountsList( int userId, int typeID )
        {
            string sql = string.Format( "SELECT ReAccounts FROM dbo.RecordAccountsExpend WHERE UserID={0} AND Type={1}", userId, typeID );
            DataSet ds = Database.ExecuteDataset( sql );
            Dictionary<int, string> dic = new Dictionary<int, string>();
            int num = 1;
            foreach( DataRow item in ds.Tables[0].Rows )
            {
                string name = item["ReAccounts"].ToString();
                dic.Add( num, name );
                num = num + 1;
            }
            return dic;
        }

        #endregion 历史修改用户名记录

        #region 历史修改密码记录

        /// <summary>
        /// 分页获取历史修改密码记录列表
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="condition">条件</param>
        /// <param name="orderby">排序</param>
        /// <returns></returns>
        public PagerSet GetRecordPasswdExpendList( int pageIndex, int pageSize, string condition, string orderby )
        {
            PagerParameters pagerPrams = new PagerParameters( RecordPasswdExpend.Tablename, orderby, condition, pageIndex, pageSize );
            return GetPagerSet2( pagerPrams );
        }

        /// <summary>
        /// 新增一条记录
        /// </summary>
        /// <param name="pwExpend"></param>
        public void InsertRecordPasswdExpend( RecordPasswdExpend pwExpend )
        {
            DataRow dr = aideRecordPasswdExpendProvider.NewRow();

            dr[RecordPasswdExpend._ReLogonPasswd] = pwExpend.ReLogonPasswd;
            dr[RecordPasswdExpend._ReInsurePasswd] = pwExpend.ReInsurePasswd;
            dr[RecordPasswdExpend._UserID] = pwExpend.UserID;
            dr[RecordPasswdExpend._ClientIP] = pwExpend.ClientIP;
            dr[RecordPasswdExpend._OperMasterID] = pwExpend.OperMasterID;
            dr[RecordPasswdExpend._CollectDate] = DateTime.Now;
            aideRecordPasswdExpendProvider.Insert( dr );
        }

        /// <summary>
        /// 获取密码修改记录
        /// </summary>
        /// <param name="rid">记录ID</param>
        /// <returns></returns>
        public RecordPasswdExpend GetRecordPasswdExpendByRid( int rid )
        {
            string sqlQuery = string.Format( "(NOLOCK) WHERE RecordID= N'{0}'", rid );
            RecordPasswdExpend model = aideRecordPasswdExpendProvider.GetObject<RecordPasswdExpend>( sqlQuery );
            return model;
        }

        /// <summary>
        /// 确认密码
        /// </summary>
        /// <param name="rid">记录ID</param>
        /// <param name="password">需要确认的密码（密文）</param>
        /// <param name="type">密码类型，0为登录密码，1为银行密码</param>
        /// <returns></returns>
        public bool ConfirmPass( int rid, string password, int type )
        {
            string sqlWhere = string.Empty;
            if( type == 0 )
                sqlWhere = string.Format( "WHERE RecordID={0} AND ReLogonPasswd='{1}'", rid, password );
            else
                sqlWhere = string.Format( "WHERE RecordID={0} AND ReInsurePasswd='{1}'", rid, password );
            int count = aideRecordPasswdExpendProvider.GetRecordsCount( sqlWhere );
            if( count > 0 )
                return true;
            else
                return false;
        }

        /// <summary>
        /// 获取历史登陆密码
        /// </summary>
        /// <param name="userId">用户id</param>
        public Dictionary<int, string> GetOldLogonPassList( int userId )
        {
            string sql = string.Format( "SELECT ReLogonPasswd,ReInsurePasswd FROM dbo.RecordPasswdExpend WHERE UserID={0}", userId );
            DataSet ds = Database.ExecuteDataset( sql );
            Dictionary<int, string> dic = new Dictionary<int, string>();
            int num = 1;
            foreach( DataRow item in ds.Tables[0].Rows )
            {
                string pass = item["ReLogonPasswd"].ToString();
                dic.Add( num, pass );
                num = num + 1;
            }
            return dic;
        }

        #endregion 历史修改密码记录

        #region 赠送金币记录

        /// <summary>
        /// 分页获取赠送金币记录列表
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="condition">条件</param>
        /// <param name="orderby">排序</param>
        /// <returns></returns>
        public PagerSet GetRecordGrantTreasureList( int pageIndex, int pageSize, string condition, string orderby )
        {
            PagerParameters pagerPrams = new PagerParameters( RecordGrantTreasure.Tablename, orderby, condition, pageIndex, pageSize );
            return GetPagerSet2( pagerPrams );
        }

        /// <summary>
        /// 新增一条赠送金币记录
        /// </summary>
        /// <param name="grantTreasure"></param>
        public void InsertRecordGrantTreasure( RecordGrantTreasure grantTreasure )
        {
            DataRow dr = aideRecordGrantTreasureProvider.NewRow();

            dr[RecordGrantTreasure._MasterID] = grantTreasure.MasterID;
            dr[RecordGrantTreasure._CurGold] = grantTreasure.CurGold;
            dr[RecordGrantTreasure._UserID] = grantTreasure.UserID;
            dr[RecordGrantTreasure._ClientIP] = grantTreasure.ClientIP;
            dr[RecordGrantTreasure._AddGold] = grantTreasure.AddGold;
            dr[RecordGrantTreasure._Reason] = grantTreasure.Reason;
            dr[RecordGrantTreasure._CollectDate] = DateTime.Now;
            aideRecordGrantTreasureProvider.Insert( dr );
        }

        #endregion 赠送金币记录

        #region 赠送会员记录

        /// <summary>
        /// 分页获取赠送会员记录列表
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="condition">条件</param>
        /// <param name="orderby">排序</param>
        /// <returns></returns>
        public PagerSet GetRecordGrantMemberList( int pageIndex, int pageSize, string condition, string orderby )
        {
            PagerParameters pagerPrams = new PagerParameters( RecordGrantMember.Tablename, orderby, condition, pageIndex, pageSize );
            return GetPagerSet2( pagerPrams );
        }

        /// <summary>
        /// 新增一条赠送会员记录
        /// </summary>
        /// <param name="grantMember"></param>
        public void InsertRecordGrantMember( RecordGrantMember grantMember )
        {
            DataRow dr = aideRecordGrantMemberProvider.NewRow();

            dr[RecordGrantMember._MasterID] = grantMember.MasterID;
            dr[RecordGrantMember._GrantCardType] = grantMember.GrantCardType;
            dr[RecordGrantMember._UserID] = grantMember.UserID;
            dr[RecordGrantMember._ClientIP] = grantMember.ClientIP;
            dr[RecordGrantMember._MemberDays] = grantMember.MemberDays;
            dr[RecordGrantMember._Reason] = grantMember.Reason;
            dr[RecordGrantMember._CollectDate] = DateTime.Now;
            aideRecordGrantMemberProvider.Insert( dr );
        }

        /// <summary>
        /// 赠送会员
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="memberOrder"></param>
        /// <param name="days"></param>
        /// <param name="masterID"></param>
        /// <param name="strReason"></param>
        /// <param name="strIP"></param>
        /// <returns></returns>
        public Message GrantMember( int userID, int memberOrder, int days, int masterID, string strReason, string strIP )
        {
            var prams = new List<DbParameter>();
            prams.Add( Database.MakeInParam( "UserID", userID ) );
            prams.Add( Database.MakeInParam( "MemberOrder", memberOrder ) );
            prams.Add( Database.MakeInParam( "MemberDays", days ) );
            prams.Add( Database.MakeInParam( "MasterID", masterID ) );
            prams.Add( Database.MakeInParam( "Reason", strReason ) );
            prams.Add( Database.MakeInParam( "ClientIP", strIP ) );

            Message msg = MessageHelper.GetMessage( Database, "WSP_PM_GrantMember", prams );
            return msg;
        }

        #endregion 赠送会员记录

        #region 赠送经验记录

        /// <summary>
        /// 分页获取赠送经验记录列表
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="condition">条件</param>
        /// <param name="orderby">排序</param>
        /// <returns></returns>
        public PagerSet GetRecordGrantExperienceList( int pageIndex, int pageSize, string condition, string orderby )
        {
            PagerParameters pagerPrams = new PagerParameters( RecordGrantExperience.Tablename, orderby, condition, pageIndex, pageSize );
            return GetPagerSet2( pagerPrams );
        }

        /// <summary>
        /// 新增一条赠送经验记录
        /// </summary>
        /// <param name="grantExperience"></param>
        public void InsertRecordGrantExperience( RecordGrantExperience grantExperience )
        {
            DataRow dr = aideRecordGrantExperienceProvider.NewRow();

            dr[RecordGrantExperience._MasterID] = grantExperience.MasterID;
            dr[RecordGrantExperience._CurExperience] = grantExperience.CurExperience;
            dr[RecordGrantExperience._UserID] = grantExperience.UserID;
            dr[RecordGrantExperience._ClientIP] = grantExperience.ClientIP;
            dr[RecordGrantExperience._AddExperience] = grantExperience.AddExperience;
            dr[RecordGrantExperience._Reason] = grantExperience.Reason;
            dr[RecordGrantExperience._CollectDate] = DateTime.Now;
            aideRecordGrantExperienceProvider.Insert( dr );
        }

        #endregion 赠送经验记录

        #region 赠送积分记录

        /// <summary>
        /// 分页获取赠送积分记录列表
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="condition">条件</param>
        /// <param name="orderby">排序</param>
        /// <returns></returns>
        public PagerSet GetRecordGrantGameScoreList( int pageIndex, int pageSize, string condition, string orderby )
        {
            PagerParameters pagerPrams = new PagerParameters( RecordGrantGameScore.Tablename, orderby, condition, pageIndex, pageSize );
            return GetPagerSet2( pagerPrams );
        }

        #endregion 赠送积分记录

        #region 清零积分记录

        /// <summary>
        /// 分页获取清零积分记录列表
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="condition">条件</param>
        /// <param name="orderby">排序</param>
        /// <returns></returns>
        public PagerSet GetRecordGrantClearScoreList( int pageIndex, int pageSize, string condition, string orderby )
        {
            PagerParameters pagerPrams = new PagerParameters( RecordGrantClearScore.Tablename, orderby, condition, pageIndex, pageSize );
            return GetPagerSet2( pagerPrams );
        }

        #endregion 清零积分记录

        #region 清零逃率记录

        /// <summary>
        /// 分页获取清零逃率记录列表
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="condition">条件</param>
        /// <param name="orderby">排序</param>
        /// <returns></returns>
        public PagerSet GetRecordGrantClearFleeList( int pageIndex, int pageSize, string condition, string orderby )
        {
            PagerParameters pagerPrams = new PagerParameters( RecordGrantClearFlee.Tablename, orderby, condition, pageIndex, pageSize );
            return GetPagerSet2( pagerPrams );
        }

        #endregion 清零逃率记录

        #region 魅力兑换记录

        /// <summary>
        /// 分页获取魅力兑换记录列表
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="condition">条件</param>
        /// <param name="orderby">排序</param>
        /// <returns></returns>
        public PagerSet GetRecordConvertPresentList( int pageIndex, int pageSize, string condition, string orderby )
        {
            PagerParameters pagerPrams = new PagerParameters( RecordConvertPresent.Tablename, orderby, condition, pageIndex, pageSize );
            return GetPagerSet2( pagerPrams );
        }

        #endregion 魅力兑换记录

        #region 赠送靓号记录

        /// <summary>
        /// 分页获取赠送靓号记录列表
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="condition">条件</param>
        /// <param name="orderby">排序</param>
        /// <returns></returns>
        public PagerSet GetRecordGrantGameIDList( int pageIndex, int pageSize, string condition, string orderby )
        {
            PagerParameters pagerPrams = new PagerParameters( RecordGrantGameID.Tablename, orderby, condition, pageIndex, pageSize );
            return GetPagerSet2( pagerPrams );
        }

        /// <summary>
        /// 赠送靓号
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="gameID"></param>
        /// <param name="masterID"></param>
        /// <param name="strReason"></param>
        /// <param name="strIP"></param>
        /// <returns></returns>
        public Message GrantGameID( int userID, int gameID, int masterID, string strReason, string strIP )
        {
            var prams = new List<DbParameter>();
            prams.Add( Database.MakeInParam( "UserID", userID ) );
            prams.Add( Database.MakeInParam( "ReGameID", gameID ) );
            prams.Add( Database.MakeInParam( "MasterID", masterID ) );
            prams.Add( Database.MakeInParam( "Reason", strReason ) );
            prams.Add( Database.MakeInParam( "ClientIP", strIP ) );

            Message msg = MessageHelper.GetMessage( Database, "WSP_PM_GrantGameID", prams );
            return msg;
        }

        #endregion 赠送靓号记录

        #region 任务记录

        /// <summary>
        /// 删除任务日志
        /// </summary>
        /// <param name="sqlQuery">where条件</param>
        public void DeleteTaskRecord( string sqlQuery )
        {
            aideTaskRecordProvider.Delete( sqlQuery );
        }

        #endregion 任务记录

        #region 数据分析

        /// <summary>
        /// 获取用户流失数
        /// </summary>
        /// <param name="startID"></param>
        /// <param name="endID"></param>
        /// <returns></returns>
        public DataSet GetLossUserByDay( int startID, int endID )
        {
            StringBuilder sql = new StringBuilder();
            sql.Append( "SELECT" );
            sql.Append( " DateID,LossUser,LossPayUser" );
            sql.Append( " FROM RecordEveryDayData WHERE DateID>=@StartID AND DateID<=@EndID" );

            var prams = new List<DbParameter>();
            prams.Add( Database.MakeInParam( "StartID", startID ) );
            prams.Add( Database.MakeInParam( "EndID", endID ) );

            DataSet ds = Database.ExecuteDataset( CommandType.Text, sql.ToString(), prams.ToArray() );
            return ds;
        }

        /// <summary>
        /// 获取月平均每天用户流失数
        /// </summary>
        /// <param name="startID"></param>
        /// <param name="endID"></param>
        /// <returns></returns>
        public DataSet GetLossUserByMonth()
        {
            string sql = "SELECT CONVERT(char(7), CollectDate,120 ) AS CollectDate,SUM(LossUser) AS LossUserTotal,SUM(LossPayUser) AS LossPayUserTotal FROM RecordEveryDayData";
            sql += " GROUP BY CONVERT(char(7), CollectDate, 120)";
            DataSet ds = Database.ExecuteDataset( sql );
            return ds;
        }

        /// <summary>
        /// 获取每日数据(为统计充值欲望)
        /// </summary>
        /// <param name="startID"></param>
        /// <param name="endID"></param>
        /// <returns></returns>
        public DataSet GetPayDesireByDay( int startID, int endID )
        {
            StringBuilder sql = new StringBuilder();
            sql.Append( "SELECT" );
            sql.Append( " DateID,UserTotal,PayUserTotal,ActiveUserTotal,LossUserTotal," );
            sql.Append( " LossPayUserTotal,PayTotalAmount,PayAmountForCurrency," );
            sql.Append( " GoldTotal,CurrencyTotal,GameTaxTotal,UserAVGOnlineTime" );
            sql.Append( " FROM RecordEveryDayData WHERE DateID>=@StartID AND DateID<=@EndID" );

            var prams = new List<DbParameter>();
            prams.Add( Database.MakeInParam( "StartID", startID ) );
            prams.Add( Database.MakeInParam( "EndID", endID ) );

            DataSet ds = Database.ExecuteDataset( CommandType.Text, sql.ToString(), prams.ToArray() );
            return ds;
        }

        /// <summary>
        /// 获取每日银行税收
        /// </summary>
        /// <param name="startID"></param>
        /// <param name="endID"></param>
        /// <returns></returns>
        public DataSet GetBankTaxByDay( int startID, int endID )
        {
            StringBuilder sql = new StringBuilder();
            sql.Append( "SELECT DateID,BankTax FROM RecordEveryDayData WHERE DateID>=@StartID AND DateID<=@EndID" );

            var prams = new List<DbParameter>();
            prams.Add( Database.MakeInParam( "StartID", startID ) );
            prams.Add( Database.MakeInParam( "EndID", endID ) );

            return Database.ExecuteDataset( CommandType.Text, sql.ToString(), prams.ToArray() );
        }

        /// <summary>
        /// 获取每个月银行税收
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public DataSet GetBankTaxByMonth()
        {
            string sql = "SELECT SUM(BankTax) AS BankTax,CONVERT(char(7),CollectDate,120) AS StatDate FROM RecordEveryDayData";
            sql += " GROUP BY CONVERT(char(7),CollectDate,120) ORDER BY StatDate ASC";
            return Database.ExecuteDataset( sql );
        }

        /// <summary>
        /// 获取每日游戏税收
        /// </summary>
        /// <param name="startID"></param>
        /// <param name="endID"></param>
        /// <returns></returns>
        public DataSet GetGameTaxByDay( int startID, int endID )
        {
            StringBuilder sql = new StringBuilder();
            sql.Append( "SELECT DateID,GameTax FROM RecordEveryDayData WHERE DateID>=@StartID AND DateID<=@EndID" );

            var prams = new List<DbParameter>();
            prams.Add( Database.MakeInParam( "StartID", startID ) );
            prams.Add( Database.MakeInParam( "EndID", endID ) );

            return Database.ExecuteDataset( CommandType.Text, sql.ToString(), prams.ToArray() );
        }

        /// <summary>
        /// 获取每个月游戏税收
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public DataSet GetGameTaxByMonth()
        {
            string sql = "SELECT SUM(GameTax) AS GameTax,CONVERT(char(7),CollectDate,120) AS StatDate FROM RecordEveryDayData";
            sql += " GROUP BY CONVERT(char(7),CollectDate,120) ORDER BY StatDate ASC";
            return Database.ExecuteDataset( sql );
        }

        /// <summary>
        /// 获取某日所有游戏的税收情况
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public DataSet GetGameTaxListByDateID( int dateID )
        {
            StringBuilder sql = new StringBuilder();
            sql.Append( "SELECT KindID,SUM(Revenue) AS Revenue FROM RecordEveryDayRoomData WHERE DateID=@DateID GROUP BY KindID ORDER BY KindID ASC" );

            var prams = new List<DbParameter>();
            prams.Add( Database.MakeInParam( "DateID", dateID ) );

            return Database.ExecuteDataset( CommandType.Text, sql.ToString(), prams.ToArray() );
        }

        /// <summary>
        /// 获取每个游戏税收
        /// </summary>
        /// <returns></returns>
        public DataTable GetGameRevenue()
        {
            string sqlQuery = @"SELECT KindID,SUM(Revenue) AS Revenue FROM RecordEveryDayRoomData GROUP BY KindID";
            DataSet ds;
            ds = Database.ExecuteDataset( CommandType.Text, sqlQuery );
            return ds.Tables[0];
        }

        /// <summary>
        /// 获取每个房间税收
        /// </summary>
        /// <returns></returns>
        public DataTable GetRoomRevenue()
        {
            string sqlQuery = @"SELECT ServerID,SUM(Revenue) AS Revenue FROM RecordEveryDayRoomData GROUP BY ServerID";
            DataSet ds;
            ds = Database.ExecuteDataset( CommandType.Text, sqlQuery );
            return ds.Tables[0];
        }

        /// <summary>
        /// 获取每个游戏损耗
        /// </summary>
        /// <returns></returns>
        public DataTable GetGameWaste()
        {
            string sqlQuery = @"SELECT KindID,SUM(Waste) AS Waste FROM RecordEveryDayRoomData GROUP BY KindID";
            DataSet ds;
            ds = Database.ExecuteDataset( CommandType.Text, sqlQuery );
            return ds.Tables[0];
        }

        /// <summary>
        /// 获取每个房间损耗
        /// </summary>
        /// <returns></returns>
        public DataTable GetRoomWaste()
        {
            string sqlQuery = @"SELECT ServerID,SUM(Waste) AS Waste FROM RecordEveryDayRoomData GROUP BY ServerID";
            DataSet ds;
            ds = Database.ExecuteDataset( CommandType.Text, sqlQuery );
            return ds.Tables[0];
        }

        #endregion 数据分析

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
        public PagerSet GetList( string tableName, int pageIndex, int pageSize, string condition, string orderby )
        {
            PagerParameters pagerPrams = new PagerParameters( tableName, orderby, condition, pageIndex, pageSize );
            return GetPagerSet2( pagerPrams );
        }

        #endregion 公共
    }
}