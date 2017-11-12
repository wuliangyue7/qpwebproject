using System.Data;
using Game.Data.Factory;
using Game.Entity.Record;
using Game.IData;
using Game.Kernel;
using System.Collections.Generic;

namespace Game.Facade
{
    /// <summary>
    /// 记录库外观
    /// </summary>
    public class RecordFacade
    {
        #region Fields

        private IRecordDataProvider aideRecordData;

        #endregion Fields

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public RecordFacade()
        {
            aideRecordData = ClassFactory.GetIRecordDataProvider();
        }

        #endregion 构造函数

        #region 历史修改帐号记录

        /// <summary>
        /// 分页获取历史修改帐号记录列表
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="condition">条件</param>
        /// <param name="orderby">排序</param>
        /// <returns></returns>
        public PagerSet GetRecordAccountsExpendList( int pageIndex, int pageSize, string condition, string orderby )
        {
            return aideRecordData.GetRecordAccountsExpendList( pageIndex, pageSize, condition, orderby );
        }

        /// <summary>
        /// 新增一条记录
        /// </summary>
        /// <param name="actExpend"></param>
        public void InsertRecordAccountsExpend( RecordAccountsExpend actExpend )
        {
            aideRecordData.InsertRecordAccountsExpend( actExpend );
        }

        /// <summary>
        /// 获取历史昵称或历史帐号
        /// </summary>
        /// <param name="userId">用户id</param>
        public Dictionary<int, string> GetOldNickNameOrAccountsList( int userId, int typeID )
        {
            return aideRecordData.GetOldNickNameOrAccountsList( userId, typeID );
        }

        #endregion 历史修改帐号记录

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
            return aideRecordData.GetRecordPasswdExpendList( pageIndex, pageSize, condition, orderby );
        }

        /// <summary>
        /// 新增一条记录
        /// </summary>
        /// <param name="pwExpend"></param>
        public void InsertRecordPasswdExpend( RecordPasswdExpend pwExpend )
        {
            aideRecordData.InsertRecordPasswdExpend( pwExpend );
        }

        /// <summary>
        /// 获取密码修改记录
        /// </summary>
        /// <param name="rid">记录ID</param>
        /// <returns></returns>
        public RecordPasswdExpend GetRecordPasswdExpendByRid( int rid )
        {
            return aideRecordData.GetRecordPasswdExpendByRid( rid );
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
            return aideRecordData.ConfirmPass( rid, password, type );
        }

        /// <summary>
        /// 获取历史登陆密码
        /// </summary>
        /// <param name="userId">用户id</param>
        public Dictionary<int, string> GetOldLogonPassList( int userId )
        {
            return aideRecordData.GetOldLogonPassList( userId );
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
            return aideRecordData.GetRecordGrantTreasureList( pageIndex, pageSize, condition, orderby );
        }

        /// <summary>
        /// 新增一条赠送金币记录
        /// </summary>
        /// <param name="grantTreasure"></param>
        public void InsertRecordGrantTreasure( RecordGrantTreasure grantTreasure )
        {
            aideRecordData.InsertRecordGrantTreasure( grantTreasure );
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
            return aideRecordData.GetRecordGrantMemberList( pageIndex, pageSize, condition, orderby );
        }

        /// <summary>
        /// 新增一条赠送会员记录
        /// </summary>
        /// <param name="grantMember"></param>
        public void InsertRecordGrantMember( RecordGrantMember grantMember )
        {
            aideRecordData.InsertRecordGrantMember( grantMember );
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
        public void GrantMember( int userID, int memberOrder, int days, int masterID, string strReason, string strIP )
        {
            aideRecordData.GrantMember( userID, memberOrder, days, masterID, strReason, strIP );
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
            return aideRecordData.GetRecordGrantExperienceList( pageIndex, pageSize, condition, orderby );
        }

        /// <summary>
        /// 新增一条赠送经验记录
        /// </summary>
        /// <param name="grantExperience"></param>
        public void InsertRecordGrantExperience( RecordGrantExperience grantExperience )
        {
            aideRecordData.InsertRecordGrantExperience( grantExperience );
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
            return aideRecordData.GetRecordGrantGameScoreList( pageIndex, pageSize, condition, orderby );
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
            return aideRecordData.GetRecordGrantClearScoreList( pageIndex, pageSize, condition, orderby );
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
            return aideRecordData.GetRecordGrantClearFleeList( pageIndex, pageSize, condition, orderby );
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
            return aideRecordData.GetRecordConvertPresentList( pageIndex, pageSize, condition, orderby );
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
            return aideRecordData.GetRecordGrantGameIDList( pageIndex, pageSize, condition, orderby );
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
            return aideRecordData.GrantGameID( userID, gameID, masterID, strReason, strIP );
        }

        #endregion 赠送靓号记录

        #region 任务记录

        /// <summary>
        /// 删除任务日志
        /// </summary>
        /// <param name="sqlQuery">where条件</param>
        /// <returns></returns>
        public void DeleteTaskRecord( string sqlQuery )
        {
            aideRecordData.DeleteTaskRecord( sqlQuery );
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
            return aideRecordData.GetLossUserByDay( startID, endID );
        }

        /// <summary>
        /// 获取月平均每天用户流失数
        /// </summary>
        /// <param name="startID"></param>
        /// <param name="endID"></param>
        /// <returns></returns>
        public DataSet GetLossUserByMonth()
        {
            return aideRecordData.GetLossUserByMonth();
        }

        /// <summary>
        /// 获取每日数据(为统计充值欲望)
        /// </summary>
        /// <param name="startID"></param>
        /// <param name="endID"></param>
        /// <returns></returns>
        public DataSet GetPayDesireByDay( int startID, int endID )
        {
            return aideRecordData.GetPayDesireByDay( startID, endID );
        }

        /// <summary>
        /// 获取每日银行税收
        /// </summary>
        /// <param name="startID"></param>
        /// <param name="endID"></param>
        /// <returns></returns>
        public DataSet GetBankTaxByDay( int startID, int endID )
        {
            return aideRecordData.GetBankTaxByDay( startID, endID );
        }

        /// <summary>
        /// 获取每个月银行税收
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public DataSet GetBankTaxByMonth()
        {
            return aideRecordData.GetBankTaxByMonth();
        }

        /// <summary>
        /// 获取每日游戏税收
        /// </summary>
        /// <param name="startID"></param>
        /// <param name="endID"></param>
        /// <returns></returns>
        public DataSet GetGameTaxByDay( int startID, int endID )
        {
            return aideRecordData.GetGameTaxByDay( startID, endID );
        }

        /// <summary>
        /// 获取每个月游戏税收
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public DataSet GetGameTaxByMonth()
        {
            return aideRecordData.GetGameTaxByMonth();
        }

        /// <summary>
        /// 获取某日所有游戏的税收情况
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public DataSet GetGameTaxListByDateID( int dateID )
        {
            return aideRecordData.GetGameTaxListByDateID( dateID );
        }

        /// <summary>
        /// 获取每个游戏税收
        /// </summary>
        /// <returns></returns>
        public DataTable GetGameRevenue()
        {
            return aideRecordData.GetGameRevenue();
        }

        /// <summary>
        /// 获取每个房间税收
        /// </summary>
        /// <returns></returns>
        public DataTable GetRoomRevenue()
        {
            return aideRecordData.GetRoomRevenue();
        }

        /// <summary>
        /// 获取每个游戏损耗
        /// </summary>
        /// <returns></returns>
        public DataTable GetGameWaste()
        {
            return aideRecordData.GetGameWaste();
        }

        /// <summary>
        /// 获取每个房间损耗
        /// </summary>
        /// <returns></returns>
        public DataTable GetRoomWaste()
        {
            return aideRecordData.GetRoomWaste();
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
            return aideRecordData.GetList( tableName, pageIndex, pageSize, condition, orderby );
        }

        #endregion 公共
    }
}