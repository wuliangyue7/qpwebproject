using System.Collections.Generic;
using System.Data;
using Game.Entity.Record;
using Game.Kernel;

namespace Game.IData
{
    /// <summary>
    /// 记录库数据层接口
    /// </summary>
    public interface IRecordDataProvider //: IProvider
    {
        #region 历史修改帐号记录

        /// <summary>
        /// 分页获取历史修改帐号记录列表
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="condition">条件</param>
        /// <param name="orderby">排序</param>
        /// <returns></returns>
        PagerSet GetRecordAccountsExpendList( int pageIndex, int pageSize, string condition, string orderby );

        /// <summary>
        /// 新增一条记录
        /// </summary>
        /// <param name="actExpend"></param>
        void InsertRecordAccountsExpend( RecordAccountsExpend actExpend );

        /// <summary>
        /// 获取历史昵称或历史帐号
        /// </summary>
        /// <param name="userId">用户id</param>
        Dictionary<int, string> GetOldNickNameOrAccountsList( int userId, int typeID );

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
        PagerSet GetRecordPasswdExpendList( int pageIndex, int pageSize, string condition, string orderby );

        /// <summary>
        /// 新增一条记录
        /// </summary>
        /// <param name="pwExpend"></param>
        void InsertRecordPasswdExpend( RecordPasswdExpend pwExpend );

        /// <summary>
        /// 获取密码修改记录
        /// </summary>
        /// <param name="rid">记录ID</param>
        /// <returns></returns>
        RecordPasswdExpend GetRecordPasswdExpendByRid( int rid );

        /// <summary>
        /// 确认密码
        /// </summary>
        /// <param name="rid">记录ID</param>
        /// <param name="password">需要确认的密码（密文）</param>
        /// <param name="type">密码类型，0为登录密码，1为银行密码</param>
        /// <returns></returns>
        bool ConfirmPass( int rid, string password, int type );

        /// <summary>
        /// 获取历史登陆密码
        /// </summary>
        /// <param name="userId">用户id</param>
        Dictionary<int, string> GetOldLogonPassList( int userId );

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
        PagerSet GetRecordGrantTreasureList( int pageIndex, int pageSize, string condition, string orderby );

        /// <summary>
        /// 新增一条赠送金币记录
        /// </summary>
        /// <param name="grantTreasure"></param>
        void InsertRecordGrantTreasure( RecordGrantTreasure grantTreasure );

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
        PagerSet GetRecordGrantMemberList( int pageIndex, int pageSize, string condition, string orderby );

        /// <summary>
        /// 新增一条赠送会员记录
        /// </summary>
        /// <param name="grantMember"></param>
        void InsertRecordGrantMember( RecordGrantMember grantMember );

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
        Message GrantMember( int userID, int memberOrder, int days, int masterID, string strReason, string strIP );

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
        PagerSet GetRecordGrantExperienceList( int pageIndex, int pageSize, string condition, string orderby );

        /// <summary>
        /// 新增一条赠送经验记录
        /// </summary>
        /// <param name="grantExperience"></param>
        void InsertRecordGrantExperience( RecordGrantExperience grantExperience );

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
        PagerSet GetRecordGrantGameScoreList( int pageIndex, int pageSize, string condition, string orderby );

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
        PagerSet GetRecordGrantClearScoreList( int pageIndex, int pageSize, string condition, string orderby );

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
        PagerSet GetRecordGrantClearFleeList( int pageIndex, int pageSize, string condition, string orderby );

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
        PagerSet GetRecordConvertPresentList( int pageIndex, int pageSize, string condition, string orderby );

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
        PagerSet GetRecordGrantGameIDList( int pageIndex, int pageSize, string condition, string orderby );

        /// <summary>
        /// 赠送靓号
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="gameID"></param>
        /// <param name="masterID"></param>
        /// <param name="strReason"></param>
        /// <param name="strIP"></param>
        /// <returns></returns>
        Message GrantGameID( int userID, int gameID, int masterID, string strReason, string strIP );

        #endregion 赠送靓号记录

        #region 任务记录

        /// <summary>
        /// 删除任务日志
        /// </summary>
        /// <param name="sqlQuery">where条件</param>
        void DeleteTaskRecord( string sqlQuery );

        #endregion 任务记录

        #region 数据分析

        /// <summary>
        /// 获取用户流失数
        /// </summary>
        /// <param name="startID"></param>
        /// <param name="endID"></param>
        /// <returns></returns>
        DataSet GetLossUserByDay( int startID, int endID );

        /// <summary>
        /// 获取月平均每天用户流失数
        /// </summary>
        /// <param name="startID"></param>
        /// <param name="endID"></param>
        /// <returns></returns>
        DataSet GetLossUserByMonth();

        /// <summary>
        /// 获取每日数据(为统计充值欲望)
        /// </summary>
        /// <param name="startID"></param>
        /// <param name="endID"></param>
        /// <returns></returns>
        DataSet GetPayDesireByDay( int startID, int endID );

        /// <summary>
        /// 获取每日银行税收
        /// </summary>
        /// <param name="startID"></param>
        /// <param name="endID"></param>
        /// <returns></returns>
        DataSet GetBankTaxByDay( int startID, int endID );

        /// <summary>
        /// 获取每个月银行税收
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        DataSet GetBankTaxByMonth();

        /// <summary>
        /// 获取每日游戏税收
        /// </summary>
        /// <param name="startID"></param>
        /// <param name="endID"></param>
        /// <returns></returns>
        DataSet GetGameTaxByDay( int startID, int endID );

        /// <summary>
        /// 获取每个月游戏税收
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        DataSet GetGameTaxByMonth();

        /// <summary>
        /// 获取某日所有游戏的税收情况
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        DataSet GetGameTaxListByDateID( int dateID );

        /// <summary>
        /// 获取每个游戏税收
        /// </summary>
        /// <returns></returns>
        DataTable GetGameRevenue();

        /// <summary>
        /// 获取每个房间税收
        /// </summary>
        /// <returns></returns>
        DataTable GetRoomRevenue();

        /// <summary>
        /// 获取每个游戏损耗
        /// </summary>
        /// <returns></returns>
        DataTable GetGameWaste();

        /// <summary>
        /// 获取每个房间损耗
        /// </summary>
        /// <returns></returns>
        DataTable GetRoomWaste();

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
        PagerSet GetList( string tableName, int pageIndex, int pageSize, string condition, string orderby );

        #endregion 公共
    }
}