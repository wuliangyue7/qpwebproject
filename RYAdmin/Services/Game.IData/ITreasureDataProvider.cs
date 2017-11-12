using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Game.Kernel;
using Game.Entity.Treasure;
using System.Data;

namespace Game.IData
{
    /// <summary>
    /// 金币库数据层接口
    /// </summary>
    public interface ITreasureDataProvider //: IProvider
    {
        #region 充值相关
        /// <summary>
        /// 获取实卡类型记录列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="condition"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        PagerSet GetGlobalLivcardList( int pageIndex, int pageSize, string condition, string orderby );
        /// <summary>
        /// 获取实卡类型实体
        /// </summary>
        /// <param name="cardTypeID"></param>
        /// <returns></returns>
        GlobalLivcard GetGlobalLivcardInfo( int cardTypeID );
        /// <summary>
        /// 新增实卡类型
        /// </summary>
        /// <param name="globalLivcard"></param>
        void InsertGlobalLivcard( GlobalLivcard globalLivcard );
        /// <summary>
        /// 更新实卡类型
        /// </summary>
        /// <param name="globalLivcard"></param>
        void UpdateGlobalLivcard( GlobalLivcard globalLivcard );
        /// <summary>
        /// 删除实卡类型
        /// </summary>
        /// <param name="sqlQuery"></param>
        void DeleteGlobalLivcard( string sqlQuery );

        /// <summary>
        /// 获取实卡批次记录
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="condition"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        PagerSet GetLivcardBuildStreamList( int pageIndex, int pageSize, string condition, string orderby );
        /// <summary>
        /// 获取实卡批次实体
        /// </summary>
        /// <param name="buildID"></param>
        /// <returns></returns>
        LivcardBuildStream GetLivcardBuildStreamInfo( int buildID );
        /// <summary>
        /// 插入实卡批次记录
        /// </summary>
        /// <param name="livcardBuildStream"></param>
        /// <returns></returns>
        int InsertLivcardBuildStream( LivcardBuildStream livcardBuildStream );
        /// <summary>
        /// 更新实卡批次记录
        /// </summary>
        /// <param name="livcardBuildStream"></param>
        void UpdateLivcardBuildStream( LivcardBuildStream livcardBuildStream );
        /// <summary>
        /// 更新实卡批次导出次数
        /// </summary>
        /// <param name="buildID"></param>
        void UpdateLivcardBuildStream( int buildID );

        /// <summary>
        /// 获取实卡记录
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="condition"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        PagerSet GetLivcardAssociatorList( int pageIndex, int pageSize, string condition, string orderby );
        /// <summary>
        /// 获取实卡实体,根据CardID
        /// </summary>
        /// <param name="cardID"></param>
        /// <returns></returns>
        LivcardAssociator GetLivcardAssociatorInfo( int cardID );
        /// <summary>
        /// 获取实卡实体,根据SerialID
        /// </summary>
        /// <param name="serialID"></param>
        /// <returns></returns>
        LivcardAssociator GetLivcardAssociatorInfo( string serialID );
        /// <summary>
        /// 获取实卡充值记录,根据SerialID
        /// </summary>
        /// <param name="serialID"></param>
        /// <returns></returns>
        ShareDetailInfo GetShareDetailInfo( string serialID );
        /// <summary>
        /// 获取实卡的销售商名称，根据批号
        /// </summary>
        /// <param name="buildID"></param>
        /// <returns></returns>
        string GetSalesperson( int buildID );
        /// <summary>
        /// 禁用实卡
        /// </summary>
        /// <param name="sqlQuery"></param>
        void SetCardDisbale( string sqlQuery );

        /// <summary>
        /// 启用实卡
        /// </summary>
        /// <param name="sqlQuery"></param>
        void SetCardEnbale( string sqlQuery );
        /// <summary>
        /// 插入实卡记录
        /// </summary>
        /// <param name="livcardAssociator"></param>
        /// <returns></returns>
        void InsertLivcardAssociator( LivcardAssociator livcardAssociator, string[,] list );
        /// <summary>
        /// 实卡库存统计
        /// </summary>
        /// <returns></returns>
        DataSet GetLivcardStat();
        /// <summary>
        /// 实卡库存统计
        /// </summary>
        /// <returns></returns>
        DataSet GetLivcardStatByBuildID( int buildID );

        /// <summary>
        /// 获取订单记录
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="condition"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        PagerSet GetOnLineOrderList( int pageIndex, int pageSize, string condition, string orderby );

        /// <summary>
        /// 获取订单信息实体
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        OnLineOrder GetOnLineOrderInfo( string orderID );

        /// <summary>
        /// 删除订单
        /// </summary>
        /// <param name="sqlQuery"></param>
        void DeleteOnlineOrder( string sqlQuery );

        /// <summary>
        /// 获取快钱返回记录
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="condition"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        PagerSet GetKQDetailList( int pageIndex, int pageSize, string condition, string orderby );

        /// <summary>
        /// 获取快钱返回记录实体
        /// </summary>
        /// <param name="detailID"></param>
        /// <returns></returns>
        ReturnKQDetailInfo GetKQDetailInfo( int detailID );

        /// <summary>
        /// 删除快钱返回记录
        /// </summary>
        /// <param name="sqlQuery"></param>
        void DeleteKQDetail( string sqlQuery );

        /// <summary>
        /// 获取天天付返回记录
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="condition"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        PagerSet GetDayDetailList( int pageIndex, int pageSize, string condition, string orderby );

        /// <summary>
        /// 获取天天付返回记录实体
        /// </summary>
        /// <param name="detailID"></param>
        /// <returns></returns>
        ReturnDayDetailInfo GetDayDetailInfo( int detailID );

        /// <summary>
        /// 删除天天付返回记录
        /// </summary>
        /// <param name="sqlQuery"></param>
        void DeleteDayDetail( string sqlQuery );

        /// <summary>
        /// 获取易宝返回记录
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="condition"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        PagerSet GetYPDetailList( int pageIndex, int pageSize, string condition, string orderby );

        /// <summary>
        /// 获取易宝返回记录实体
        /// </summary>
        /// <param name="detailID"></param>
        /// <returns></returns>
        ReturnYPDetailInfo GetYPDetailInfo( int detailID );

        /// <summary>
        /// 删除易宝返回记录
        /// </summary>
        /// <param name="sqlQuery"></param>
        void DeleteYPDetail( string sqlQuery );

        /// <summary>
        /// 获取VB返回记录
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="condition"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        PagerSet GetVBDetailList( int pageIndex, int pageSize, string condition, string orderby );

        /// <summary>
        /// 获取VB返回记录实体
        /// </summary>
        /// <param name="detailID"></param>
        /// <returns></returns>
        ReturnVBDetailInfo GetVBDetailInfo( int detailID );

        /// <summary>
        /// 删除VB返回记录
        /// </summary>
        /// <param name="sqlQuery"></param>
        void DeleteVBDetail( string sqlQuery );

        /// <summary>
        /// 获取充值记录
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="condition"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        PagerSet GetShareDetailList( int pageIndex, int pageSize, string condition, string orderby );

        /// <summary>
        /// 获取充值服务类型
        /// </summary>
        /// <param name="shareID"></param>
        /// <returns></returns>
        GlobalShareInfo GetGlobalShareByShareID( int shareID );

        /// <summary>
        /// 获取充值服务列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="condition"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        PagerSet GetGlobalShareList( int pageIndex, int pageSize, string condition, string orderby );

        /// <summary>
        /// 获取苹果返回记录
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="condition"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        PagerSet GetAppDetailList( int pageIndex, int pageSize, string condition, string orderby );

        /// <summary>
        /// 获取苹果返回记录实体
        /// </summary>
        /// <param name="detailID"></param>
        /// <returns></returns>
        ReturnAppDetailInfo GetAppDetailInfo( int detailID );

        #region 苹果产品类型

        /// <summary>
        /// 获取产品列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="condition"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        PagerSet GetGlobalAppInfoList( int pageIndex, int pageSize, string condition, string orderby );

        /// <summary>
        /// 获取产品实体
        /// </summary>
        /// <param name="appID"></param>
        /// <returns></returns>
        GlobalAppInfo GetGlobalAppInfo( int appID );

        /// <summary>
        /// 新增产品
        /// </summary>
        /// <param name="globalLivcard"></param>
        void InsertGlobalAppInfo( GlobalAppInfo globalApp );

        /// <summary>
        /// 更新产品
        /// </summary>
        /// <param name="globalLivcard"></param>
        void UpdateGlobalAppInfo( GlobalAppInfo globalApp );

        /// <summary>
        /// 删除产品
        /// </summary>
        /// <param name="sqlQuery"></param>
        void DeleteGlobalAppInfo( string sqlQuery );

        #endregion

        #endregion

        #region 用户金币信息
        /// <summary>
        /// 分页获取金币列表
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="condition">条件</param>
        /// <param name="orderby">排序</param>
        /// <returns></returns>
        PagerSet GetGameScoreInfoList( int pageIndex, int pageSize, string condition, string orderby );
        /// <summary>
        /// 获取用户金币信息
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        GameScoreInfo GetGameScoreInfoByUserID( int UserID );
        /// <summary>
        /// 获取用户的银行金币
        /// </summary>
        /// <param name="UserID">用户标识 </param>
        /// <returns></returns>
        decimal GetGameScoreByUserID( int UserID );
        /// <summary>
        /// 更新用户银行金币
        /// </summary>
        /// <param name="gameScoreInfo"></param>
        void UpdateInsureScore( GameScoreInfo gameScoreInfo );
        /// <summary>
        /// 批量赠送金币
        /// </summary>       
        /// <param name="strUserIdList">赠送对象</param>
        /// <param name="intGold">赠送金额</param>
        /// <param name="intMasterID">操作者ID</param>
        /// <param name="strReason">赠送原因</param>
        /// <param name="strIP">IP地址</param>
        /// <returns></returns>
        Message GrantTreasure( string strUserIdList, int intGold, int intMasterID, string strReason, string strIP );
        /// <summary>
        /// 赠送积分
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="kindID"></param>
        /// <param name="score"></param>
        /// <param name="masterID"></param>
        /// <param name="strReason"></param>
        /// <param name="strIP"></param>
        /// <returns></returns>
        Message GrantScore( int userID, int kindID, int score, int masterID, string strReason, string strIP );
        /// <summary>
        /// 清零积分
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="kindID"></param>      
        /// <param name="masterID"></param>
        /// <param name="strReason"></param>
        /// <param name="strIP"></param>
        /// <returns></returns>
        Message GrantClearScore( int userID, int kindID, int masterID, string strReason, string strIP );
        /// <summary>
        /// 清零逃率
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="kindID"></param>      
        /// <param name="masterID"></param>
        /// <param name="strReason"></param>
        /// <param name="strIP"></param>
        /// <returns></returns>
        Message GrantFlee( int userID, int kindID, int masterID, string strReason, string strIP );
        #endregion

        #region 用户货币信息

        /// <summary>
        /// 获取用户货币实体信息
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        UserCurrencyInfo GetUserCurrencyInfo( int userID );

        #endregion

        #region 游戏记录
        /// <summary>
        /// 分页获取游戏记录列表
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="condition">条件</param>
        /// <param name="orderby">排序</param>
        /// <returns></returns>
        PagerSet GetRecordDrawInfoList( int pageIndex, int pageSize, string condition, string orderby );

        /// <summary>
        /// 删除小于等于该日期的游戏总局记录
        /// </summary>
        /// <param name="dt">删除截止日期</param>
        /// <returns></returns>
        int DeleteRecordDrawInfoByTime( DateTime dt );

        /// <summary>
        /// 获取玩家游戏记录
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="condition">条件</param>
        /// <param name="orderby">排序</param>
        /// <returns></returns>
        PagerSet GetRecordDrawScoreList( int pageIndex, int pageSize, string condition, string orderby );

        /// <summary>
        /// 根据局ID获取游戏记录
        /// </summary>
        /// <param name="drawID">局ID</param>
        /// <returns>数据集</returns>
        DataSet GetRecordDrawScoreByDrawID( int drawID );

        /// <summary>
        /// 删除小于等于该日期的游戏详情记录
        /// </summary>
        /// <param name="dt">删除截止日期</param>
        /// <returns></returns>
        int DeleteRecordDrawScoreByTime( DateTime dt );
        #endregion

        #region 用户卡线记录
        /// <summary>
        /// 分页获取卡线记录列表
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="condition">条件</param>
        /// <param name="orderby">排序</param>
        /// <returns></returns>
        PagerSet GetGameScoreLockerList( int pageIndex, int pageSize, string condition, string orderby );
        /// <summary>
        /// 删除卡线记录
        /// </summary>
        /// <param name="userID"></param>
        void DeleteGameScoreLockerByUserID( int userID );

        /// <summary>
        /// 删除卡线记录
        /// </summary>
        /// <param name="sqlQuery"></param>
        void DeleteGameScoreLocker( string sqlQuery );

        /// <summary>
        /// 获取用户卡线信息
        /// </summary>
        /// <param name="userID">用户UserID</param>
        /// <returns></returns>
        GameScoreLocker GetGameScoreLockerByUserID( int userID );

        #endregion

        #region 机器人管理

        /// <summary>
        /// 获取机器人列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="condition"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        PagerSet GetAndroidList( int pageIndex, int pageSize, string condition, string orderby );

        /// <summary>
        /// 获取机器人实体
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        AndroidManager GetAndroidInfo( int userID );

        /// <summary>
        /// 新增机器人
        /// </summary>
        /// <param name="android"></param>
        void InsertAndroid( AndroidManager android );

        /// <summary>
        /// 更新机器人
        /// </summary>
        /// <param name="awardType"></param>
        void UpdateAndroid( AndroidManager android );

        /// <summary>
        /// 删除机器人
        /// </summary>
        /// <param name="sqlQuery"></param>
        void DeleteAndroid( string sqlQuery );

        /// <summary>
        /// 冻结或解冻机器人
        /// </summary>
        /// <param name="nullity"></param>
        /// <param name="sqlQuery"></param>
        void NullityAndroid( byte nullity, string sqlQuery );
        #endregion

        #region 用户进出记录

        /// <summary>
        /// 获取进出记录列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="condition"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        PagerSet GetRecordUserInoutList( int pageIndex, int pageSize, string condition, string orderby );

        /// <summary>
        /// 删除小于等于该日期的进出记录
        /// </summary>
        /// <param name="dt">删除截止日期</param>
        /// <returns></returns>
        int DeleteRecordUserInoutByTime( DateTime dt );
        #endregion

        #region 用户银行记录

        /// <summary>
        /// 获取用户银行记录
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="condition"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        PagerSet GetRecordInsureList( int pageIndex, int pageSize, string condition, string orderby );

        /// <summary>
        /// 获取转账税收前100名
        /// </summary>
        /// <returns></returns>
        DataSet GetUserTransferTop100();

        /// <summary>
        /// 删除小于等于该日期的银行记录
        /// </summary>
        /// <param name="dt">删除截止日期</param>
        /// <returns></returns>
        int DeleteRecordInsureByTime( DateTime dt );
        #endregion

        #region 推广管理
        /// <summary>
        /// 获取推广配置信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        GlobalSpreadInfo GetGlobalSpreadInfo( int id );
        /// <summary>
        /// 更新推广配置信息
        /// </summary>
        /// <param name="spreadinfo"></param>
        void UpdateGlobalSpreadInfo( GlobalSpreadInfo spreadinfo );

        /// <summary>
        /// 获取推广财务列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="condition"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        PagerSet GetRecordSpreadInfoList( int pageIndex, int pageSize, string condition, string orderby );
        /// <summary>
        /// 获取推广员的推广总收入金币
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        int GetSpreadScore( int userID );
        #endregion

        #region 数据分析

        /// <summary>
        /// 获取金币分布数据
        /// </summary>
        /// <returns></returns>
        DataSet GetGoldDistribution();

        /// <summary>
        /// 充值统计
        /// </summary>
        /// <param name="siteID"></param>
        /// <returns></returns>
        DataSet GetPayStat();

        /// <summary>
        /// 根据天充值统计
        /// </summary>
        /// <param name="startID"></param>
        /// <param name="endID"></param>
        /// <returns></returns>
        DataSet GetPayStatByDay( string startDate, string endDate );

        /// <summary>
        /// 获取每天活跃玩家数
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        DataSet GetActiveUserByDay( int startDateID, int endDateID );

        /// <summary>
        /// 获取每月活跃玩家数
        /// </summary>
        /// <returns></returns>
        DataSet GetActivieUserByMonth();

        /// <summary>
        /// 统计需要清理的数据表记录总数、记录最大日期、记录最小日期等
        /// </summary>
        /// <returns></returns>
        DataSet StatRecordTable();

        /// <summary>
        /// 获取统计信息
        /// </summary>
        /// <returns></returns>
        DataSet GetStatInfo();
        #endregion

        #region APP运营助手

        /// <summary>
        /// 充值统计
        /// </summary>
        /// <param name="accounts"></param>
        /// <param name="logonPass"></param>
        /// <param name="machineID"></param>
        /// <returns></returns>
        Message AppStatFilled(string accounts, string logonPass, string machineID);

        /// <summary>
        /// 充值统计（现金）
        /// </summary>
        /// <param name="accounts"></param>
        /// <param name="logonPass"></param>
        /// <param name="machineID"></param>
        /// <returns></returns>
        Message AppStatFilledCash(string accounts, string logonPass, string machineID);

        /// <summary>
        /// 充值统计（金币）
        /// </summary>
        /// <param name="accounts"></param>
        /// <param name="logonPass"></param>
        /// <param name="machineID"></param>
        /// <returns></returns>
        Message AppStatFilledScore(string accounts, string logonPass, string machineID);

        /// <summary>
        /// 金币统计（赠送）
        /// </summary>
        /// <param name="accounts"></param>
        /// <param name="logonPass"></param>
        /// <param name="machineID"></param>
        /// <returns></returns>
        Message AppStatScorePresent(string accounts, string logonPass, string machineID);

        /// <summary>
        /// 金币统计（税收）
        /// </summary>
        /// <param name="accounts"></param>
        /// <param name="logonPass"></param>
        /// <param name="machineID"></param>
        /// <returns></returns>
        Message AppStatScoreRevenue(string accounts, string logonPass, string machineID);

        /// <summary>
        /// 金币统计（损耗）
        /// </summary>
        /// <param name="accounts"></param>
        /// <param name="logonPass"></param>
        /// <param name="machineID"></param>
        /// <returns></returns>
        Message AppStatScoreWaste(string accounts, string logonPass, string machineID);

        /// <summary>
        /// 充值详情（现金）
        /// </summary>
        /// <param name="accounts"></param>
        /// <param name="logonPass"></param>
        /// <param name="typeID"></param>
        /// <param name="machineID"></param>
        /// <param name="dateType"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        Message AppGetChargeData(string accounts, string logonPass, int typeID, string machineID, int dateType, string startDate, string endDate);

        /// <summary>
        /// 充值详情（金币）
        /// </summary>
        /// <param name="accounts"></param>
        /// <param name="logonPass"></param>
        /// <param name="machineID"></param>
        /// <returns></returns>
        Message AppGetPayScoreData(string accounts, string logonPass, string machineID, int dateType, string startDate, string endDate);

        /// <summary>
        /// 税收详情
        /// </summary>
        /// <param name="accounts"></param>
        /// <param name="logonPass"></param>
        /// <param name="typeID"></param>
        /// <param name="machineID"></param>
        /// <param name="dateType"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        Message AppGetRevenueData(string accounts, string logonPass, int typeID, string machineID, int dateType, string startDate, string endDate);

        /// <summary>
        /// 赠送详情
        /// </summary>
        /// <param name="accounts"></param>
        /// <param name="logonPass"></param>
        /// <param name="typeID"></param>
        /// <param name="machineID"></param>
        /// <param name="dateType"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        Message AppGetPresentData(string accounts, string logonPass, int typeID, string machineID, int dateType, string startDate, string endDate);

        /// <summary>
        /// 损耗详情
        /// </summary>
        /// <param name="accounts"></param>
        /// <param name="logonPass"></param>
        /// <param name="machineID"></param>
        /// <param name="dateType"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        Message AppGetWasteData(string accounts, string logonPass, string machineID, int dateType, string startDate, string endDate);

        /// <summary>
        /// 平台金币详情
        /// </summary>
        /// <param name="accounts"></param>
        /// <param name="logonPass"></param>
        /// <param name="machineID"></param>
        /// <param name="dateType"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        Message AppGetPlatScoreData(string accounts, string logonPass, string machineID, int dateType, string startDate, string endDate);

        /// <summary>
        /// 充值会员详情
        /// </summary>
        /// <param name="accounts"></param>
        /// <param name="logonPass"></param>
        /// <param name="typeID"></param>
        /// <param name="machineID"></param>
        /// <param name="dateType"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        Message AppGetMemberData(string accounts, string logonPass, int typeID, string machineID, int dateType, string startDate, string endDate);
        #endregion

        #region 代理商管理

        /// <summary>
        /// 获取代理商下级玩家贡献税收分成金币
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        Int64 GetChildRevenueProvide(int userID);

        /// <summary>
        /// 获取代理商下级玩家贡献的充值分成金币
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        Int64 GetChildPayProvide(int userID);

        /// <summary>
        /// 获取代理分成详情
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        DataSet GetAgentFinance(int userID);

        /// <summary>
        /// 手工统计税收
        /// </summary>
        void StatRevenueHand();

        /// <summary>
        /// 手工统计返现
        /// </summary>
        void StatAgentPayHand();
        #endregion

        #region 转盘管理

        /// <summary>
        /// 获取转盘配置
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        LotteryConfig GetLotteryConfig(int id);

        /// <summary>
        /// 更新转盘配置
        /// </summary>
        /// <param name="spreadinfo"></param>
        void UpdateLotteryConfig(LotteryConfig model);

        /// <summary>
        /// 更新转盘奖品配置
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int UpdateLotteryItem(LotteryItem model);
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
        PagerSet GetList( string tableName, int pageIndex, int pageSize, string condition, string orderby );

        /// <summary>
        /// 执行SQL语句返回受影响的行数
        /// </summary>
        /// <param name="sql"></param>
        int ExecuteSql( string sql );

        /// <summary>
        ///  执行sql返回DataSet
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        DataSet GetDataSetBySql( string sql );

        /// <summary>
        /// 执行SQL语句返回一个值
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        string GetScalarBySql( string sql );

        #endregion        
    }
}
