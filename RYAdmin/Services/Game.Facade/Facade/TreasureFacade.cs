using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Game.IData;
using Game.Data.Factory;
using Game.Kernel;
using Game.Entity.Treasure;
using System.Data;

namespace Game.Facade
{
    /// <summary>
    /// 金币库外观
    /// </summary>
    public class TreasureFacade
    {
        #region Fields

        private ITreasureDataProvider aideTreasureData;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public TreasureFacade()
        {
            aideTreasureData = ClassFactory.GetITreasureDataProvider();
        }
        public TreasureFacade( int kindID )
        {
            aideTreasureData = ClassFactory.GetITreasureDataProvider( kindID );
        }
        #endregion

        #region 充值相关
        /// <summary>
        /// 获取实卡类型记录列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="condition"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public PagerSet GetGlobalLivcardList( int pageIndex, int pageSize, string condition, string orderby )
        {
            return aideTreasureData.GetGlobalLivcardList( pageIndex, pageSize, condition, orderby );
        }
        /// <summary>
        /// 获取实卡类型实体
        /// </summary>
        /// <param name="cardTypeID"></param>
        /// <returns></returns>
        public GlobalLivcard GetGlobalLivcardInfo( int cardTypeID )
        {
            return aideTreasureData.GetGlobalLivcardInfo( cardTypeID );
        }
        /// <summary>
        /// 新增实卡类型
        /// </summary>
        /// <param name="globalLivcard"></param>
        public Message InsertGlobalLivcard( GlobalLivcard globalLivcard )
        {
            aideTreasureData.InsertGlobalLivcard( globalLivcard );
            return new Message( true );
        }
        /// <summary>
        /// 更新实卡类型
        /// </summary>
        /// <param name="globalLivcard"></param>
        public Message UpdateGlobalLivcard( GlobalLivcard globalLivcard )
        {
            aideTreasureData.UpdateGlobalLivcard( globalLivcard );
            return new Message( true );
        }
        /// <summary>
        /// 删除实卡类型
        /// </summary>
        /// <param name="sqlQuery"></param>
        public void DeleteGlobalLivcard( string sqlQuery )
        {
            aideTreasureData.DeleteGlobalLivcard( sqlQuery );
        }

        /// <summary>
        /// 获取实卡批次记录
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="condition"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public PagerSet GetLivcardBuildStreamList( int pageIndex, int pageSize, string condition, string orderby )
        {
            return aideTreasureData.GetLivcardBuildStreamList( pageIndex, pageSize, condition, orderby );
        }
        /// <summary>
        /// 获取实卡批次实体
        /// </summary>
        /// <param name="buildID"></param>
        /// <returns></returns>
        public LivcardBuildStream GetLivcardBuildStreamInfo( int buildID )
        {
            return aideTreasureData.GetLivcardBuildStreamInfo( buildID );
        }
        /// <summary>
        /// 插入实卡批次记录
        /// </summary>
        /// <param name="livcardBuildStream"></param>
        /// <returns></returns>
        public int InsertLivcardBuildStream( LivcardBuildStream livcardBuildStream )
        {
            return aideTreasureData.InsertLivcardBuildStream( livcardBuildStream );
        }
        /// <summary>
        /// 更新实卡批次记录
        /// </summary>
        /// <param name="livcardBuildStream"></param>
        public void UpdateLivcardBuildStream( LivcardBuildStream livcardBuildStream )
        {
            aideTreasureData.UpdateLivcardBuildStream( livcardBuildStream );
        }
        /// <summary>
        /// 更新实卡批次导出次数
        /// </summary>
        /// <param name="buildID"></param>
        public void UpdateLivcardBuildStream( int buildID )
        {
            aideTreasureData.UpdateLivcardBuildStream( buildID );
        }

        /// <summary>
        /// 获取实卡记录
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="condition"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public PagerSet GetLivcardAssociatorList( int pageIndex, int pageSize, string condition, string orderby )
        {
            return aideTreasureData.GetLivcardAssociatorList( pageIndex, pageSize, condition, orderby );
        }
        /// <summary>
        /// 获取实卡实体,根据CardID
        /// </summary>
        /// <param name="cardID"></param>
        /// <returns></returns>
        public LivcardAssociator GetLivcardAssociatorInfo( int cardID )
        {
            return aideTreasureData.GetLivcardAssociatorInfo( cardID );
        }
        /// <summary>
        /// 获取实卡实体,根据SerialID
        /// </summary>
        /// <param name="serialID"></param>
        /// <returns></returns>
        public LivcardAssociator GetLivcardAssociatorInfo( string serialID )
        {
            return aideTreasureData.GetLivcardAssociatorInfo( serialID );
        }
        /// <summary>
        /// 获取实卡充值记录,根据SerialID
        /// </summary>
        /// <param name="serialID"></param>
        /// <returns></returns>
        public ShareDetailInfo GetShareDetailInfo( string serialID )
        {
            return aideTreasureData.GetShareDetailInfo( serialID );
        }
        /// <summary>
        /// 获取实卡的销售商名称，根据批号
        /// </summary>
        /// <param name="buildID"></param>
        /// <returns></returns>
        public string GetSalesperson( int buildID )
        {
            return aideTreasureData.GetSalesperson( buildID );
        }
        /// <summary>
        /// 禁用实卡
        /// </summary>
        /// <param name="sqlQuery"></param>
        public void SetCardDisbale( string sqlQuery )
        {
            aideTreasureData.SetCardDisbale( sqlQuery );
        }

        /// <summary>
        /// 启用实卡
        /// </summary>
        /// <param name="sqlQuery"></param>
        public void SetCardEnbale( string sqlQuery )
        {
            aideTreasureData.SetCardEnbale( sqlQuery );
        }
        /// <summary>
        /// 插入实卡记录
        /// </summary>
        /// <param name="livcardAssociator"></param>
        /// <returns></returns>
        public void InsertLivcardAssociator( LivcardAssociator livcardAssociator, string[,] list )
        {
            aideTreasureData.InsertLivcardAssociator( livcardAssociator, list );
        }
        /// <summary>
        /// 实卡库存统计
        /// </summary>
        /// <returns></returns>
        public DataSet GetLivcardStat()
        {
            return aideTreasureData.GetLivcardStat();
        }
        /// <summary>
        /// 实卡库存统计
        /// </summary>
        /// <returns></returns>
        public DataSet GetLivcardStatByBuildID( int buildID )
        {
            return aideTreasureData.GetLivcardStatByBuildID( buildID );
        }
        /// <summary>
        /// 获取订单记录
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="condition"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public PagerSet GetOnLineOrderList( int pageIndex, int pageSize, string condition, string orderby )
        {
            return aideTreasureData.GetOnLineOrderList( pageIndex, pageSize, condition, orderby );
        }

        /// <summary>
        /// 获取订单信息实体
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public OnLineOrder GetOnLineOrderInfo( string orderID )
        {
            return aideTreasureData.GetOnLineOrderInfo( orderID );
        }

        /// <summary>
        /// 删除订单
        /// </summary>
        /// <param name="sqlQuery"></param>
        public void DeleteOnlineOrder( string sqlQuery )
        {
            aideTreasureData.DeleteOnlineOrder( sqlQuery );
        }

        /// <summary>
        /// 获取快钱返回记录
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="condition"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public PagerSet GetKQDetailList( int pageIndex, int pageSize, string condition, string orderby )
        {
            return aideTreasureData.GetKQDetailList( pageIndex, pageSize, condition, orderby );
        }

        /// <summary>
        /// 获取快钱返回记录实体
        /// </summary>
        /// <param name="detailID"></param>
        /// <returns></returns>
        public ReturnKQDetailInfo GetKQDetailInfo( int detailID )
        {
            return aideTreasureData.GetKQDetailInfo( detailID );
        }

        /// <summary>
        /// 删除快钱返回记录
        /// </summary>
        /// <param name="sqlQuery"></param>
        public void DeleteKQDetail( string sqlQuery )
        {
            aideTreasureData.DeleteKQDetail( sqlQuery );
        }

        /// <summary>
        /// 获取易宝返回记录
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="condition"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public PagerSet GetYPDetailList( int pageIndex, int pageSize, string condition, string orderby )
        {
            return aideTreasureData.GetYPDetailList( pageIndex, pageSize, condition, orderby );
        }

        /// <summary>
        /// 获取易宝返回记录实体
        /// </summary>
        /// <param name="detailID"></param>
        /// <returns></returns>
        public ReturnYPDetailInfo GetYPDetailInfo( int detailID )
        {
            return aideTreasureData.GetYPDetailInfo( detailID );
        }

        /// <summary>
        /// 删除易宝返回记录
        /// </summary>
        /// <param name="sqlQuery"></param>
        public void DeleteYPDetail( string sqlQuery )
        {
            aideTreasureData.DeleteYPDetail( sqlQuery );
        }

        /// <summary>
        /// 获取VB返回记录
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="condition"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public PagerSet GetVBDetailList( int pageIndex, int pageSize, string condition, string orderby )
        {
            return aideTreasureData.GetVBDetailList( pageIndex, pageSize, condition, orderby );
        }

        /// <summary>
        /// 获取VB返回记录实体
        /// </summary>
        /// <param name="detailID"></param>
        /// <returns></returns>
        public ReturnVBDetailInfo GetVBDetailInfo( int detailID )
        {
            return aideTreasureData.GetVBDetailInfo( detailID );
        }

        /// <summary>
        /// 删除VB返回记录
        /// </summary>
        /// <param name="sqlQuery"></param>
        public void DeleteVBDetail( string sqlQuery )
        {
            aideTreasureData.DeleteVBDetail( sqlQuery );
        }


        /// <summary>
        /// 获取天天付返回记录
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="condition"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public PagerSet GetDayDetailList( int pageIndex, int pageSize, string condition, string orderby )
        {
            return aideTreasureData.GetDayDetailList( pageIndex, pageSize, condition, orderby );
        }

        /// <summary>
        /// 获取天天付返回记录实体
        /// </summary>
        /// <param name="detailID"></param>
        /// <returns></returns>
        public ReturnDayDetailInfo GetDayDetailInfo( int detailID )
        {
            return aideTreasureData.GetDayDetailInfo( detailID );
        }

        /// <summary>
        /// 删除天天付返回记录
        /// </summary>
        /// <param name="sqlQuery"></param>
        public void DeleteDayDetail( string sqlQuery )
        {
            aideTreasureData.DeleteDayDetail( sqlQuery );
        }

        /// <summary>
        /// 获取充值记录
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="condition"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public PagerSet GetShareDetailList( int pageIndex, int pageSize, string condition, string orderby )
        {
            return aideTreasureData.GetShareDetailList( pageIndex, pageSize, condition, orderby );
        }

        /// <summary>
        /// 获取充值服务类型
        /// </summary>
        /// <param name="shareID"></param>
        /// <returns></returns>
        public GlobalShareInfo GetGlobalShareByShareID( int shareID )
        {
            return aideTreasureData.GetGlobalShareByShareID( shareID );
        }

        /// <summary>
        /// 获取充值服务列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="condition"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public PagerSet GetGlobalShareList( int pageIndex, int pageSize, string condition, string orderby )
        {
            return aideTreasureData.GetGlobalShareList( pageIndex, pageSize, condition, orderby );
        }

        /// <summary>
        /// 获取苹果返回记录
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="condition"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public PagerSet GetAppDetailList( int pageIndex, int pageSize, string condition, string orderby )
        {
            return aideTreasureData.GetAppDetailList( pageIndex, pageSize, condition, orderby );
        }

        /// <summary>
        /// 获取苹果返回记录实体
        /// </summary>
        /// <param name="detailID"></param>
        /// <returns></returns>
        public ReturnAppDetailInfo GetAppDetailInfo( int detailID )
        {
            return aideTreasureData.GetAppDetailInfo( detailID );
        }

        #region 苹果产品类型

        /// <summary>
        /// 获取产品列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="condition"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public PagerSet GetGlobalAppInfoList( int pageIndex, int pageSize, string condition, string orderby )
        {
            return aideTreasureData.GetGlobalAppInfoList( pageIndex, pageSize, condition, orderby );
        }

        /// <summary>
        /// 获取产品实体
        /// </summary>
        /// <param name="appID"></param>
        /// <returns></returns>
        public GlobalAppInfo GetGlobalAppInfo( int appID )
        {
            return aideTreasureData.GetGlobalAppInfo( appID );
        }
        /// <summary>
        /// 新增产品
        /// </summary>
        /// <param name="globalLivcard"></param>
        public Message InsertGlobalAppInfo( GlobalAppInfo globalApp )
        {
            aideTreasureData.InsertGlobalAppInfo( globalApp );
            return new Message( true );
        }
        /// <summary>
        /// 更新产品
        /// </summary>
        /// <param name="globalLivcard"></param>
        public Message UpdateGlobalAppInfo( GlobalAppInfo globalApp )
        {
            aideTreasureData.UpdateGlobalAppInfo( globalApp );
            return new Message( true );
        }
        /// <summary>
        /// 删除产品
        /// </summary>
        /// <param name="sqlQuery"></param>
        public Message DeleteGlobalAppInfo( string sqlQuery )
        {
            try
            {
                aideTreasureData.DeleteGlobalAppInfo( sqlQuery );
                return new Message( true );
            }
            catch
            {
                return new Message( false );
            }
        }
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
        public PagerSet GetGameScoreInfoList( int pageIndex, int pageSize, string condition, string orderby )
        {
            return aideTreasureData.GetGameScoreInfoList( pageIndex, pageSize, condition, orderby );
        }
        /// <summary>
        /// 获取用户金币信息
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public GameScoreInfo GetGameScoreInfoByUserID( int UserID )
        {
            return aideTreasureData.GetGameScoreInfoByUserID( UserID );
        }
        /// <summary>
        /// 获取用户的银行金币
        /// </summary>
        /// <param name="UserID">用户标识 </param>
        /// <returns></returns>
        public decimal GetGameScoreByUserID( int UserID )
        {
            return aideTreasureData.GetGameScoreByUserID( UserID );
        }
        /// <summary>
        /// 更新用户银行金币
        /// </summary>
        /// <param name="gameScoreInfo"></param>
        public void UpdateInsureScore( GameScoreInfo gameScoreInfo )
        {
            aideTreasureData.UpdateInsureScore( gameScoreInfo );
        }
        /// <summary>
        /// 批量赠送金币
        /// </summary>      
        /// <param name="strUserIdList">赠送对象</param>
        /// <param name="intGold">赠送金额</param>
        /// <param name="intMasterID">操作者ID</param>
        /// <param name="strReason">赠送原因</param>
        /// <param name="strIP">IP地址</param>
        /// <returns></returns>
        public Message GrantTreasure( string strUserIdList, int intGold, int intMasterID, string strReason, string strIP )
        {
            return aideTreasureData.GrantTreasure( strUserIdList, intGold, intMasterID, strReason, strIP );
        }
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
        public Message GrantScore( int userID, int kindID, int score, int masterID, string strReason, string strIP )
        {
            return aideTreasureData.GrantScore( userID, kindID, score, masterID, strReason, strIP );
        }
        /// <summary>
        /// 清零积分
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="kindID"></param>      
        /// <param name="masterID"></param>
        /// <param name="strReason"></param>
        /// <param name="strIP"></param>
        /// <returns></returns>
        public Message GrantClearScore( int userID, int kindID, int masterID, string strReason, string strIP )
        {
            return aideTreasureData.GrantClearScore( userID, kindID, masterID, strReason, strIP );
        }
        /// <summary>
        /// 清零逃率
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="kindID"></param>      
        /// <param name="masterID"></param>
        /// <param name="strReason"></param>
        /// <param name="strIP"></param>
        /// <returns></returns>
        public Message GrantFlee( int userID, int kindID, int masterID, string strReason, string strIP )
        {
            return aideTreasureData.GrantFlee( userID, kindID, masterID, strReason, strIP );
        }
        #endregion

        #region 用户货币信息

        /// <summary>
        /// 获取用户货币实体信息
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public UserCurrencyInfo GetUserCurrencyInfo( int userID )
        {
            return aideTreasureData.GetUserCurrencyInfo( userID );
        }

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
        public PagerSet GetRecordDrawInfoList( int pageIndex, int pageSize, string condition, string orderby )
        {
            return aideTreasureData.GetRecordDrawInfoList( pageIndex, pageSize, condition, orderby );
        }

        /// <summary>
        /// 删除小于等于该日期的游戏总局记录
        /// </summary>
        /// <param name="dt">删除截止日期</param>
        /// <returns></returns>
        public int DeleteRecordDrawInfoByTime( DateTime dt )
        {
            return aideTreasureData.DeleteRecordDrawInfoByTime( dt );
        }

        /// <summary>
        /// 获取玩家游戏记录
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="condition">条件</param>
        /// <param name="orderby">排序</param>
        /// <returns></returns>
        public PagerSet GetRecordDrawScoreList( int pageIndex, int pageSize, string condition, string orderby )
        {
            return aideTreasureData.GetRecordDrawScoreList( pageIndex, pageSize, condition, orderby );
        }

        /// <summary>
        /// 根据局ID获取游戏记录
        /// </summary>
        /// <param name="drawID">局ID</param>
        /// <returns>数据集</returns>
        public DataSet GetRecordDrawScoreByDrawID( int drawID )
        {
            return aideTreasureData.GetRecordDrawScoreByDrawID( drawID );
        }

        /// <summary>
        /// 删除小于等于该日期的游戏详情记录
        /// </summary>
        /// <param name="dt">删除截止日期</param>
        /// <returns></returns>
        public int DeleteRecordDrawScoreByTime( DateTime dt )
        {
            return aideTreasureData.DeleteRecordDrawScoreByTime( dt );
        }
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
        public PagerSet GetGameScoreLockerList( int pageIndex, int pageSize, string condition, string orderby )
        {
            return aideTreasureData.GetGameScoreLockerList( pageIndex, pageSize, condition, orderby );
        }
        /// <summary>
        /// 删除卡线记录
        /// </summary>
        /// <param name="userID"></param>
        public void DeleteGameScoreLockerByUserID( int userID )
        {
            aideTreasureData.DeleteGameScoreLockerByUserID( userID );
        }

        /// <summary>
        /// 删除卡线记录
        /// </summary>
        /// <param name="sqlQuery"></param>
        public void DeleteGameScoreLocker( string sqlQuery )
        {
            aideTreasureData.DeleteGameScoreLocker( sqlQuery );
        }

        /// <summary>
        /// 获取用户卡线信息
        /// </summary>
        /// <param name="userID">用户UserID</param>
        /// <returns></returns>
        public GameScoreLocker GetGameScoreLockerByUserID( int userID )
        {
            return aideTreasureData.GetGameScoreLockerByUserID( userID );
        }
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
        public PagerSet GetAndroidList( int pageIndex, int pageSize, string condition, string orderby )
        {
            return aideTreasureData.GetAndroidList( pageIndex, pageSize, condition, orderby );
        }

        /// <summary>
        /// 获取机器人实体
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public AndroidManager GetAndroidInfo( int userID )
        {
            return aideTreasureData.GetAndroidInfo( userID );
        }

        /// <summary>
        /// 新增机器人
        /// </summary>
        /// <param name="android"></param>
        public Message InsertAndroid( AndroidManager android )
        {
            aideTreasureData.InsertAndroid( android );
            return new Message( true );
        }

        /// <summary>
        /// 更新机器人
        /// </summary>
        /// <param name="awardType"></param>
        public Message UpdateAndroid( AndroidManager android )
        {
            aideTreasureData.UpdateAndroid( android );
            return new Message( true );
        }

        /// <summary>
        /// 删除机器人
        /// </summary>
        /// <param name="sqlQuery"></param>
        public void DeleteAndroid( string sqlQuery )
        {
            aideTreasureData.DeleteAndroid( sqlQuery );
        }

        /// <summary>
        /// 冻结或解冻机器人
        /// </summary>
        /// <param name="nullity"></param>
        /// <param name="sqlQuery"></param>
        public void NullityAndroid( byte nullity, string sqlQuery )
        {
            aideTreasureData.NullityAndroid( nullity, sqlQuery );
        }
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
        public PagerSet GetRecordUserInoutList( int pageIndex, int pageSize, string condition, string orderby )
        {
            return aideTreasureData.GetRecordUserInoutList( pageIndex, pageSize, condition, orderby );
        }

        /// <summary>
        /// 删除小于等于该日期的进出记录
        /// </summary>
        /// <param name="dt">删除截止日期</param>
        /// <returns></returns>
        public int DeleteRecordUserInoutByTime( DateTime dt )
        {
            return aideTreasureData.DeleteRecordUserInoutByTime( dt );
        }
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
        public PagerSet GetRecordInsureList( int pageIndex, int pageSize, string condition, string orderby )
        {
            return aideTreasureData.GetRecordInsureList( pageIndex, pageSize, condition, orderby );
        }

        /// <summary>
        /// 获取转账税收前100名
        /// </summary>
        /// <returns></returns>
        public DataSet GetUserTransferTop100()
        {
            return aideTreasureData.GetUserTransferTop100();
        }

        /// <summary>
        /// 删除小于等于该日期的银行记录
        /// </summary>
        /// <param name="dt">删除截止日期</param>
        /// <returns></returns>
        public int DeleteRecordInsureByTime( DateTime dt )
        {
            return aideTreasureData.DeleteRecordInsureByTime( dt );
        }
        #endregion

        #region 推广管理
        /// <summary>
        /// 获取推广配置信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public GlobalSpreadInfo GetGlobalSpreadInfo( int id )
        {
            return aideTreasureData.GetGlobalSpreadInfo( id );
        }
        /// <summary>
        /// 更新推广配置信息
        /// </summary>
        /// <param name="spreadinfo"></param>
        public void UpdateGlobalSpreadInfo( GlobalSpreadInfo spreadinfo )
        {
            aideTreasureData.UpdateGlobalSpreadInfo( spreadinfo );
        }

        /// <summary>
        /// 获取推广财务列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="condition"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public PagerSet GetRecordSpreadInfoList( int pageIndex, int pageSize, string condition, string orderby )
        {
            return aideTreasureData.GetRecordSpreadInfoList( pageIndex, pageSize, condition, orderby );
        }
        /// <summary>
        /// 获取推广员的推广总收入金币
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public int GetSpreadScore( int userID )
        {
            return aideTreasureData.GetSpreadScore( userID );
        }
        #endregion

        #region 数据分析

        /// <summary>
        /// 获取金币分布数据
        /// </summary>
        /// <returns></returns>
        public DataSet GetGoldDistribution()
        {
            DataSet ds = aideTreasureData.GetGoldDistribution();
            return ds;
        }

        /// <summary>
        /// 充值统计
        /// </summary>
        /// <param name="siteID"></param>
        /// <returns></returns>
        public DataSet GetPayStat()
        {
            return aideTreasureData.GetPayStat();
        }

        /// <summary>
        /// 根据天充值统计
        /// </summary>
        /// <param name="startID"></param>
        /// <param name="endID"></param>
        /// <returns></returns>
        public DataSet GetPayStatByDay( string startID, string endID )
        {
            return aideTreasureData.GetPayStatByDay( startID, endID );
        }

        /// <summary>
        /// 获取每天活跃玩家数
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public DataSet GetActiveUserByDay( int startDateID, int endDateID )
        {
            return aideTreasureData.GetActiveUserByDay( startDateID, endDateID );
        }

        /// <summary>
        /// 获取每月活跃玩家数
        /// </summary>
        /// <returns></returns>
        public DataSet GetActivieUserByMonth()
        {
            return aideTreasureData.GetActivieUserByMonth();
        }

        /// <summary>
        /// 统计需要清理的数据表记录总数、记录最大日期、记录最小日期等
        /// </summary>
        /// <returns></returns>
        public DataSet StatRecordTable()
        {
            return aideTreasureData.StatRecordTable();
        }

        /// <summary>
        /// 获取统计信息
        /// </summary>
        /// <returns></returns>
        public DataSet GetStatInfo()
        {
            return aideTreasureData.GetStatInfo();
        }
        #endregion

        #region APP运营助手

        /// <summary>
        /// 充值统计
        /// </summary>
        /// <param name="accounts"></param>
        /// <param name="logonPass"></param>
        /// <param name="machineID"></param>
        /// <returns></returns>
        public Message AppStatFilled(string accounts, string logonPass, string machineID)
        {
            return aideTreasureData.AppStatFilled(accounts, logonPass, machineID);
        }

        /// <summary>
        /// 充值统计（现金）
        /// </summary>
        /// <param name="accounts"></param>
        /// <param name="logonPass"></param>
        /// <param name="machineID"></param>
        /// <returns></returns>
        public Message AppStatFilledCash(string accounts, string logonPass, string machineID)
        {
            return aideTreasureData.AppStatFilledCash(accounts, logonPass, machineID);
        }

        /// <summary>
        /// 充值统计（金币）
        /// </summary>
        /// <param name="accounts"></param>
        /// <param name="logonPass"></param>
        /// <param name="machineID"></param>
        /// <returns></returns>
        public Message AppStatFilledScore(string accounts, string logonPass, string machineID)
        {
            return aideTreasureData.AppStatFilledScore(accounts, logonPass, machineID);
        }

        /// <summary>
        /// 金币统计（赠送）
        /// </summary>
        /// <param name="accounts"></param>
        /// <param name="logonPass"></param>
        /// <param name="machineID"></param>
        /// <returns></returns>
        public Message AppStatScorePresent(string accounts, string logonPass, string machineID)
        {
            return aideTreasureData.AppStatScorePresent(accounts, logonPass, machineID);
        }

        /// <summary>
        /// 金币统计（税收）
        /// </summary>
        /// <param name="accounts"></param>
        /// <param name="logonPass"></param>
        /// <param name="machineID"></param>
        /// <returns></returns>
        public Message AppStatScoreRevenue(string accounts, string logonPass, string machineID)
        {
            return aideTreasureData.AppStatScoreRevenue(accounts, logonPass, machineID);
        }

        /// <summary>
        /// 金币统计（损耗）
        /// </summary>
        /// <param name="accounts"></param>
        /// <param name="logonPass"></param>
        /// <param name="machineID"></param>
        /// <returns></returns>
        public Message AppStatScoreWaste(string accounts, string logonPass, string machineID)
        {
            return aideTreasureData.AppStatScoreWaste(accounts, logonPass, machineID);
        }

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
        public Message AppGetChargeData(string accounts, string logonPass, int typeID, string machineID, int dateType, string startDate, string endDate)
        {
            return aideTreasureData.AppGetChargeData(accounts, logonPass, typeID, machineID, dateType, startDate, endDate);
        }

        /// <summary>
        /// 充值详情（金币）
        /// </summary>
        /// <param name="accounts"></param>
        /// <param name="logonPass"></param>
        /// <param name="machineID"></param>
        /// <returns></returns>
        public Message AppGetPayScoreData(string accounts, string logonPass, string machineID, int dateType, string startDate, string endDate)
        {
            return aideTreasureData.AppGetPayScoreData(accounts, logonPass, machineID, dateType, startDate, endDate);
        }

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
        public Message AppGetRevenueData(string accounts, string logonPass, int typeID, string machineID, int dateType, string startDate, string endDate)
        {
            return aideTreasureData.AppGetRevenueData(accounts, logonPass, typeID, machineID, dateType, startDate, endDate);
        }

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
        public Message AppGetPresentData(string accounts, string logonPass, int typeID, string machineID, int dateType, string startDate, string endDate)
        {
            return aideTreasureData.AppGetPresentData(accounts, logonPass, typeID, machineID, dateType, startDate, endDate);
        }

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
        public Message AppGetWasteData(string accounts, string logonPass, string machineID, int dateType, string startDate, string endDate)
        {
            return aideTreasureData.AppGetWasteData(accounts, logonPass, machineID, dateType, startDate, endDate);
        }

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
        public Message AppGetPlatScoreData(string accounts, string logonPass, string machineID, int dateType, string startDate, string endDate)
        {
            return aideTreasureData.AppGetPlatScoreData(accounts, logonPass, machineID, dateType, startDate, endDate);
        }

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
        public Message AppGetMemberData(string accounts, string logonPass, int typeID, string machineID, int dateType, string startDate, string endDate)
        {
            return aideTreasureData.AppGetMemberData(accounts, logonPass, typeID, machineID, dateType, startDate, endDate);
        }
        #endregion

        #region 代理商管理

        /// <summary>
        /// 获取代理商下级玩家贡献税收分成金币
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public Int64 GetChildRevenueProvide(int userID)
        {
            return aideTreasureData.GetChildRevenueProvide(userID);
        }

        /// <summary>
        /// 获取代理商下级玩家贡献的充值分成金币
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public Int64 GetChildPayProvide(int userID)
        {
            return aideTreasureData.GetChildPayProvide(userID);
        }

        /// <summary>
        /// 获取代理分成详情
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public DataSet GetAgentFinance(int userID)
        {
            return aideTreasureData.GetAgentFinance(userID);
        }

        /// <summary>
        /// 手工统计税收
        /// </summary>
        public void StatRevenueHand()
        {
            aideTreasureData.StatRevenueHand();
        }
        /// <summary>
        /// 手工统计返现
        /// </summary>
        public void StatAgentPayHand()
        {
            aideTreasureData.StatAgentPayHand();
        }
        #endregion

        #region 转盘管理

        /// <summary>
        /// 获取转盘配置
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public LotteryConfig GetLotteryConfig(int id)
        {
            return aideTreasureData.GetLotteryConfig(id);
        }

        /// <summary>
        /// 更新转盘配置
        /// </summary>
        /// <param name="spreadinfo"></param>
        public void UpdateLotteryConfig(LotteryConfig model)
        {
            aideTreasureData.UpdateLotteryConfig(model);
        }

        /// <summary>
        /// 更新转盘奖品配置
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateLotteryItem(LotteryItem model)
        {
            return aideTreasureData.UpdateLotteryItem(model);
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
        public PagerSet GetList( string tableName, int pageIndex, int pageSize, string condition, string orderby )
        {
            return aideTreasureData.GetList( tableName, pageIndex, pageSize, condition, orderby );
        }

        /// <summary>
        /// 执行SQL语句返回受影响的行数
        /// </summary>
        /// <param name="sql"></param>
        public int ExecuteSql( string sql )
        {
            return aideTreasureData.ExecuteSql( sql );
        }

        /// <summary>
        ///  执行sql返回DataSet
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public DataSet GetDataSetBySql( string sql )
        {
            return aideTreasureData.GetDataSetBySql( sql );
        }

        /// <summary>
        /// 执行SQL语句返回一个值
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public string GetScalarBySql( string sql )
        {
            return aideTreasureData.GetScalarBySql( sql );
        }

        #endregion             
    }
}
