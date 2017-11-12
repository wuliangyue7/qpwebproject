using System.Data;
using Game.Data.Factory;
using Game.Entity.NativeWeb;
using Game.IData;
using Game.Kernel;

namespace Game.Facade
{
    /// <summary>
    /// 后台外观
    /// </summary>
    public class NativeWebFacade //: BaseFacadeProvider
    {
        #region Fields

        private INativeWebDataProvider aideNativeWebData;

        #endregion Fields
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public NativeWebFacade()
        {
            aideNativeWebData = ClassFactory.GetINativeWebDataProvider();
        }

        #endregion 构造函数

        #region 新闻部分

        /// <summary>
        /// 获取新闻列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="condition"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public PagerSet GetNewsList( int pageIndex, int pageSize, string condition, string orderby )
        {
            return aideNativeWebData.GetNewsList( pageIndex, pageSize, condition, orderby );
        }

        /// <summary>
        /// 获取新闻实体
        /// </summary>
        /// <param name="newsID"></param>
        /// <returns></returns>
        public News GetNewsInfo( int newsID )
        {
            return aideNativeWebData.GetNewsInfo( newsID );
        }

        /// <summary>
        /// 新增新闻
        /// </summary>
        /// <param name="news"></param>
        public Message InsertNews( News news )
        {
            aideNativeWebData.InsertNews( news );
            return new Message( true );
        }

        /// <summary>
        /// 更新新闻
        /// </summary>
        /// <param name="news"></param>
        public Message UpdateNews( News news )
        {
            aideNativeWebData.UpdateNews( news );
            return new Message( true );
        }

        /// <summary>
        /// 删除新闻
        /// </summary>
        /// <param name="sqlQuery"></param>
        public void DeleteNews( string sqlQuery )
        {
            aideNativeWebData.DeleteNews( sqlQuery );
        }

        #endregion 新闻部分

        #region 规则部分

        /// <summary>
        /// 获取规则列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="condition"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public PagerSet GetGameRulesList( int pageIndex, int pageSize, string condition, string orderby )
        {
            return aideNativeWebData.GetGameRulesList( pageIndex, pageSize, condition, orderby );
        }

        /// <summary>
        /// 获取规则实体
        /// </summary>
        /// <param name="kindID"></param>
        /// <returns></returns>
        public GameRulesInfo GetGameRulesInfo( int kindID )
        {
            return aideNativeWebData.GetGameRulesInfo( kindID );
        }

        /// <summary>
        /// 新增规则
        /// </summary>
        /// <param name="gameRules"></param>
        public Message InsertGameRules( GameRulesInfo gameRules )
        {
            aideNativeWebData.InsertGameRules( gameRules );
            return new Message( true );
        }

        /// <summary>
        /// 更新规则
        /// </summary>
        /// <param name="gameRules"></param>
        public Message UpdateGameRules( GameRulesInfo gameRules, int kindID )
        {
            aideNativeWebData.UpdateGameRules( gameRules, kindID );
            return new Message( true );
        }

        /// <summary>
        /// 删除规则
        /// </summary>
        /// <param name="sqlQuery"></param>
        public void DeleteGameRules( string sqlQuery )
        {
            aideNativeWebData.DeleteGameRules( sqlQuery );
        }

        /// <summary>
        /// 判断游戏规则是否存在
        /// </summary>
        /// <param name="kindID"></param>
        /// <returns></returns>
        public bool JudgeRulesIsExistence( int kindID )
        {
            return aideNativeWebData.JudgeRulesIsExistence( kindID );
        }

        #endregion 规则部分

        #region 常见问题部分

        /// <summary>
        /// 获取问题列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="condition"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public PagerSet GetGameIssueList( int pageIndex, int pageSize, string condition, string orderby )
        {
            return aideNativeWebData.GetGameIssueList( pageIndex, pageSize, condition, orderby );
        }

        /// <summary>
        /// 获取问题实体
        /// </summary>
        /// <param name="issueID"></param>
        /// <returns></returns>
        public GameIssueInfo GetGameIssueInfo( int issueID )
        {
            return aideNativeWebData.GetGameIssueInfo( issueID );
        }

        /// <summary>
        /// 新增问题
        /// </summary>
        /// <param name="gameIssue"></param>
        public Message InsertGameIssue( GameIssueInfo gameIssue )
        {
            aideNativeWebData.InsertGameIssue( gameIssue );
            return new Message( true );
        }

        /// <summary>
        /// 更新问题
        /// </summary>
        /// <param name="gameIssue"></param>
        public Message UpdateGameIssue( GameIssueInfo gameIssue )
        {
            aideNativeWebData.UpdateGameIssue( gameIssue );
            return new Message( true );
        }

        /// <summary>
        /// 删除问题
        /// </summary>
        /// <param name="sqlQuery"></param>
        public void DeleteGameIssue( string sqlQuery )
        {
            aideNativeWebData.DeleteGameIssue( sqlQuery );
        }

        #endregion 常见问题部分

        #region 反馈管理

        /// <summary>
        /// 获取反馈列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="condition"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public PagerSet GetGameFeedbackList( int pageIndex, int pageSize, string condition, string orderby )
        {
            return aideNativeWebData.GetGameFeedbackList( pageIndex, pageSize, condition, orderby );
        }

        /// <summary>
        /// 获取反馈实体
        /// </summary>
        /// <param name="feedbackID"></param>
        /// <returns></returns>
        public GameFeedbackInfo GetGameFeedbackInfo( int feedbackID )
        {
            return aideNativeWebData.GetGameFeedbackInfo( feedbackID );
        }

        /// <summary>
        /// 回复反馈
        /// </summary>
        /// <param name="gameFeedback"></param>
        public Message RevertGameFeedback( GameFeedbackInfo gameFeedback )
        {
            aideNativeWebData.RevertGameFeedback( gameFeedback );
            return new Message( true );
        }

        /// <summary>
        /// 删除反馈
        /// </summary>
        /// <param name="sqlQuery"></param>
        public void DeleteGameFeedback( string sqlQuery )
        {
            aideNativeWebData.DeleteGameFeedback( sqlQuery );
        }

        /// <summary>
        /// 禁用反馈
        /// </summary>
        /// <param name="sqlQuery"></param>
        public void SetFeedbackDisbale( string sqlQuery )
        {
            aideNativeWebData.SetFeedbackDisbale( sqlQuery );
        }

        /// <summary>
        /// 启用反馈
        /// </summary>
        /// <param name="sqlQuery"></param>
        public void SetFeedbackEnbale( string sqlQuery )
        {
            aideNativeWebData.SetFeedbackEnbale( sqlQuery );
        }

        /// <summary>
        /// 获取未处理的留言总数
        /// </summary>
        /// <returns></returns>
        public int GetNewMessageCounts()
        {
            return aideNativeWebData.GetNewMessageCounts();
        }

        #endregion 反馈管理

        #region 公告部分

        /// <summary>
        /// 获取公告列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="condition"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public PagerSet GetNoticeList( int pageIndex, int pageSize, string condition, string orderby )
        {
            return aideNativeWebData.GetNoticeList( pageIndex, pageSize, condition, orderby );
        }

        /// <summary>
        /// 获取公告实体
        /// </summary>
        /// <param name="noticeID"></param>
        /// <returns></returns>
        public Notice GetNoticeInfo( int noticeID )
        {
            return aideNativeWebData.GetNoticeInfo( noticeID );
        }

        /// <summary>
        /// 新增公告
        /// </summary>
        /// <param name="notice"></param>
        public Message InsertNotice( Notice notice )
        {
            aideNativeWebData.InsertNotice( notice );
            return new Message( true );
        }

        /// <summary>
        /// 更新公告
        /// </summary>
        /// <param name="notice"></param>
        public Message UpdateNotice( Notice notice )
        {
            aideNativeWebData.UpdateNotice( notice );
            return new Message( true );
        }

        /// <summary>
        /// 删除公告
        /// </summary>
        /// <param name="sqlQuery"></param>
        public void DeleteNotice( string sqlQuery )
        {
            aideNativeWebData.DeleteNotice( sqlQuery );
        }

        /// <summary>
        /// 禁用公告
        /// </summary>
        /// <param name="sqlQuery"></param>
        public void SetNoticeDisbale( string sqlQuery )
        {
            aideNativeWebData.SetNoticeDisbale( sqlQuery );
        }

        /// <summary>
        /// 启用公告
        /// </summary>
        /// <param name="sqlQuery"></param>
        public void SetNoticeEnbale( string sqlQuery )
        {
            aideNativeWebData.SetNoticeEnbale( sqlQuery );
        }

        #endregion 公告部分

        #region 比赛管理

        /// <summary>
        /// 获取比赛列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="condition"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public PagerSet GetGameMatchInfoList( int pageIndex, int pageSize, string condition, string orderby )
        {
            return aideNativeWebData.GetGameMatchInfoList( pageIndex, pageSize, condition, orderby );
        }

        /// <summary>
        /// 获取比赛实体
        /// </summary>
        /// <param name="matchID"></param>
        /// <returns></returns>
        public GameMatchInfo GetGameMatchInfo( int matchID )
        {
            return aideNativeWebData.GetGameMatchInfo( matchID );
        }

        /// <summary>
        /// 新增比赛
        /// </summary>
        /// <param name="gameMatch"></param>
        public void InsertGameMatchInfo( GameMatchInfo gameMatch )
        {
            aideNativeWebData.InsertGameMatchInfo( gameMatch );
        }

        /// <summary>
        /// 更新比赛
        /// </summary>
        /// <param name="gameMatch"></param>
        public void UpdateGameMatchInfo( GameMatchInfo gameMatch )
        {
            aideNativeWebData.UpdateGameMatchInfo( gameMatch );
        }

        /// <summary>
        /// 删除比赛
        /// </summary>
        /// <param name="sqlQuery"></param>
        public void DeleteGameMatchInfo( string sqlQuery )
        {
            aideNativeWebData.DeleteGameMatchInfo( sqlQuery );
        }

        /// <summary>
        /// 禁用比赛
        /// </summary>
        /// <param name="sqlQuery"></param>
        public void SetMatchDisbale( string sqlQuery )
        {
            aideNativeWebData.SetMatchDisbale( sqlQuery );
        }

        /// <summary>
        /// 启用比赛
        /// </summary>
        /// <param name="sqlQuery"></param>
        public void SetMatchEnbale( string sqlQuery )
        {
            aideNativeWebData.SetMatchEnbale( sqlQuery );
        }

        /// <summary>
        /// 获取参赛用户列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="condition"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public PagerSet GetGameMatchUserInfoList( int pageIndex, int pageSize, string condition, string orderby )
        {
            return aideNativeWebData.GetGameMatchUserInfoList( pageIndex, pageSize, condition, orderby );
        }

        /// <summary>
        /// 禁用参赛用户
        /// </summary>
        /// <param name="sqlQuery"></param>
        public void SetUserMatchDisbale( string sqlQuery )
        {
            aideNativeWebData.SetUserMatchDisbale( sqlQuery );
        }

        /// <summary>
        /// 启用参赛用户
        /// </summary>
        /// <param name="sqlQuery"></param>
        public void SetUserMatchEnbale( string sqlQuery )
        {
            aideNativeWebData.SetUserMatchEnbale( sqlQuery );
        }

        #endregion 比赛管理

        #region 商品类型部分

        /// <summary>
        /// 根据父id获取类型实体
        /// </summary>
        /// <param name="parentId">父id</param>
        public AwardType GetAwardTypeByPID( int typeID )
        {
            return aideNativeWebData.GetAwardTypeByPID( typeID );
        }

        /// <summary>
        /// 获取所有的商品类型
        /// </summary>
        public DataSet GetAllAwardType()
        {
            return aideNativeWebData.GetAllAwardType();
        }

        /// <summary>
        /// 获取所有的子类型
        /// </summary>
        public DataSet GetAllChildType()
        {
            return aideNativeWebData.GetAllChildType();
        }

        /// <summary>
        /// 根据类型id获取类型实体
        /// </summary>
        /// <param name="typeId">类型Id</param>
        public AwardType GetAwardTypeByID( int typeID )
        {
            return aideNativeWebData.GetAwardTypeByID( typeID );
        }

        /// <summary>
        /// 保存商品类型信息
        /// </summary>
        /// <param name="type">类型实体</param>
        public bool InsertAwardTypeInfo( AwardType awardType )
        {
            aideNativeWebData.InsertAwardTypeInfo( awardType );
            return true;
        }

        /// <summary>
        /// 更新商品类型信息
        /// </summary>
        /// <param name="awardType"></param>
        public bool UpdateAwardTypeInfo( AwardType awardType )
        {
            int result = aideNativeWebData.UpdateAwardTypeInfo( awardType );
            return result > 0 ? true : false;
        }

        /// <summary>
        /// 更改类型状态
        /// </summary>
        /// <param name="typeId">类型id</param>
        /// <param name="state">状态值</param>
        public int UpdateNulity( string typeId, int state )
        {
            return aideNativeWebData.UpdateNulity( typeId, state );
        }

        /// <summary>
        /// 根据父类型获取对应子类型的id字符串
        /// </summary>
        /// <param name="Pid">父类型id</param>
        public string GetChildAwardTypeByPID( int Pid )
        {
            return aideNativeWebData.GetChildAwardTypeByPID( Pid );
        }

        /// <summary>
        /// 删除商品类型
        /// </summary>
        /// <param name="typeId">商品类型标识</param>
        public Message DeleteAwardType( int typeId )
        {
            return aideNativeWebData.DeleteAwardType( typeId );
        }

        #endregion 商品类型部分

        #region 商品信息

        /// <summary>
        /// 根据商品id获取商品实体
        /// </summary>
        /// <param name="id">商品id</param>
        public AwardInfo GetAwardInfoByID( int id )
        {
            return aideNativeWebData.GetAwardInfoByID( id );
        }

        /// <summary>
        /// 修改商品状态
        /// </summary>
        /// <param name="idList">商品id</param>
        /// <param name="state">商品状态</param>
        public int UpdateAwardInfoNulity( string idList, int state )
        {
            return aideNativeWebData.UpdateAwardInfoNulity( idList, state );
        }

        /// <summary>
        /// 保存商品信息
        /// </summary>
        /// <param name="info">商品实体</param>
        public bool InsertAwardInfo( AwardInfo info )
        {
            aideNativeWebData.InsertAwardInfo( info );
            return true;
        }

        /// <summary>
        /// 更新商品信息
        /// </summary>
        /// <param name="info">商品实体</param>
        public bool UpdateAwardInfo( AwardInfo info )
        {
            int result = aideNativeWebData.UpdateAwardInfo( info );
            return result > 0 ? true : false;
        }

        /// <summary>
        /// 获取最大键值
        /// </summary>
        public int GetMaxAwardInfoID()
        {
            return aideNativeWebData.GetMaxAwardInfoID();
        }

        /// <summary>
        /// 判断该类型下是否存在商品
        /// </summary>
        /// <param name="typeId">类型id</param>
        public bool IsHaveGoods( int typeID )
        {
            return aideNativeWebData.IsHaveGoods( typeID );
        }

        #endregion 商品信息

        #region 商品订单

        /// <summary>
        /// 根据订单号获取订单
        /// </summary>
        /// <param name="orderID">订单号</param>
        public AwardOrder GetAwardOrderByID( int orderID )
        {
            return aideNativeWebData.GetAwardOrderByID( orderID );
        }

        /// <summary>
        /// 修改订单状态
        /// </summary>
        /// <param name="state">状态值</param>
        /// <param name="OrderID">订单号</param>
        /// <param name="note">处理备注</param>
        public int UpdateOrderState( int state, int orderID, string note )
        {
            return aideNativeWebData.UpdateOrderState( state, orderID, note );
        }

        /// <summary>
        /// 获取新的订单总数
        /// </summary>
        /// <returns></returns>
        public int GetNewOrderCounts()
        {
            return aideNativeWebData.GetNewOrderCounts();
        }

        #endregion 商品订单

        #region 站点配置

        /// <summary>
        /// 获取站点配置
        /// </summary>
        /// <param name="configID"></param>
        /// <returns></returns>
        public ConfigInfo GetConfigInfo( int configID )
        {
            return aideNativeWebData.GetConfigInfo( configID );
        }

        /// <summary>
        /// 获取站点配置
        /// </summary>
        /// <param name="configKey">配置Key</param>
        /// <returns></returns>
        public ConfigInfo GetConfigInfo( string configKey )
        {
            return aideNativeWebData.GetConfigInfo( configKey );
        }

        /// <summary>
        /// 更新站点配置
        /// </summary>
        /// <param name="ci"></param>
        public void UpdateConfigInfo( ConfigInfo ci )
        {
            aideNativeWebData.UpdateConfigInfo( ci );
        }

        /// <summary>
        /// 获取站点最小配置ID
        /// </summary>
        /// <returns></returns>
        public int GetConfigInfoMinID()
        {
            return aideNativeWebData.GetConfigInfoMinID();
        }

        #endregion 站点配置

        #region 独立页管理

        /// <summary>
        /// 获取独立页
        /// </summary>
        /// <param name="configID">页ID</param>
        /// <returns></returns>
        public SinglePage GetSinglePage( int pageID )
        {
            return aideNativeWebData.GetSinglePage( pageID );
        }

        /// <summary>
        /// 获取独立页
        /// </summary>
        /// <param name="configKey">配置Key</param>
        /// <returns></returns>
        public SinglePage GetSinglePage( string keyValue )
        {
            return aideNativeWebData.GetSinglePage( keyValue );
        }

        /// <summary>
        /// 更新独立页
        /// </summary>
        /// <param name="singlePage">独立页实体</param>
        public int UpdateSinglePage( SinglePage singlePage )
        {
            return aideNativeWebData.UpdateSinglePage( singlePage );
        }

        /// <summary>
        /// 获取独立页最小ID
        /// </summary>
        /// <returns></returns>
        public int GetSinglePageMinID()
        {
            return aideNativeWebData.GetSinglePageMinID();
        }

        #endregion 独立页管理

        #region 广告管理

        /// <summary>
        /// 获取广告实体
        /// </summary>
        /// <param name="ID">广告ID</param>
        /// <returns>广告实体</returns>
        public Ads GetAds( int ID )
        {
            return aideNativeWebData.GetAds( ID );
        }

        /// <summary>
        /// 删除广告
        /// </summary>
        /// <param name="sqlQuery">删除条件</param>
        public void DeleteAds( string sqlQuery )
        {
            aideNativeWebData.DeleteAds( sqlQuery );
        }

        /// <summary>
        /// 添加广告
        /// </summary>
        /// <param name="ads">广告实体</param>
        public void InsertAds( Ads ads )
        {
            aideNativeWebData.InsertAds( ads );
        }

        /// <summary>
        /// 更新广告
        /// </summary>
        /// <param name="ads">广告实体</param>
        public void UpdateAds( Ads ads )
        {
            aideNativeWebData.UpdateAds( ads );
        }

        #endregion 广告管理

        #region 申诉管理

        /// <summary>
        /// 获取申诉实体
        /// </summary>
        /// <param name="reportID">申诉ID</param>
        /// <returns></returns>
        public LossReport GetLossReport( int reportID )
        {
            return aideNativeWebData.GetLossReport( reportID );
        }

        /// <summary>
        /// 更新申诉实体
        /// </summary>
        /// <param name="lossReport">申诉实体</param>
        /// <returns>成功返回True，失败返回False</returns>
        public bool UpdateLossReport( LossReport lossReport )
        {
            return aideNativeWebData.UpdateLossReport( lossReport );
        }

        #endregion 申诉管理

        #region 活动管理

        /// <summary>
        /// 删除活动
        /// </summary>
        /// <param name="idList">删除活动ID列表</param>
        public void DeleteActivity(string idList)
        {
            aideNativeWebData.DeleteActivity(idList);
        }

        /// <summary>
        /// 获取活动实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Activity GetActivity(int id)
        {
            return aideNativeWebData.GetActivity(id);
        }

        /// <summary>
        /// 添加活动
        /// </summary>
        /// <param name="model"></param>
        public void AddActivity(Activity model)
        {
            aideNativeWebData.AddActivity(model);
        }

        /// <summary>
        /// 更新活动
        /// </summary>
        /// <param name="model"></param>
        public void UpdateActivity(Activity model)
        {
            aideNativeWebData.UpdateActivity(model);
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
            return aideNativeWebData.GetList( tableName, pageIndex, pageSize, condition, orderby );
        }

        /// <summary>
        /// 执行SQL语句返回受影响的行数
        /// </summary>
        /// <param name="sql"></param>
        public int ExecuteSql( string sql )
        {
            return aideNativeWebData.ExecuteSql( sql );
        }

        /// <summary>
        ///  执行sql返回DataSet
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public DataSet GetDataSetBySql( string sql )
        {
            return aideNativeWebData.GetDataSetBySql( sql );
        }

        /// <summary>
        /// 执行SQL语句返回一个值
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public string GetScalarBySql( string sql )
        {
            return aideNativeWebData.GetScalarBySql( sql );
        }

        #endregion 公共
    }
}