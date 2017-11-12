using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Game.Kernel;
using Game.Entity.NativeWeb;


namespace Game.IData
{
    /// <summary>
    /// 后台数据层接口
    /// </summary>
    public interface INativeWebDataProvider //: IProvider
    {

        #region 新闻部分

        /// <summary>
        /// 获取新闻列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="condition"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        PagerSet GetNewsList(int pageIndex, int pageSize, string condition, string orderby);

        /// <summary>
        /// 获取新闻实体
        /// </summary>
        /// <param name="newsID"></param>
        /// <returns></returns>
        News GetNewsInfo(int newsID);

        /// <summary>
        /// 新增新闻
        /// </summary>
        /// <param name="news"></param>
        void InsertNews(News news);

        /// <summary>
        /// 更新新闻
        /// </summary>
        /// <param name="news"></param>
        void UpdateNews(News news);
        
        /// <summary>
        /// 删除新闻
        /// </summary>
        /// <param name="sqlQuery"></param>
        void DeleteNews(string sqlQuery);
        #endregion

        #region 规则部分

        /// <summary>
        /// 获取规则列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="condition"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        PagerSet GetGameRulesList(int pageIndex, int pageSize, string condition, string orderby);

        /// <summary>
        /// 获取规则实体
        /// </summary>
        /// <param name="kindID"></param>
        /// <returns></returns>
        GameRulesInfo GetGameRulesInfo(int kindID);

        /// <summary>
        /// 新增规则
        /// </summary>
        /// <param name="gameRules"></param>
        void InsertGameRules(GameRulesInfo gameRules);

        /// <summary>
        /// 更新规则
        /// </summary>
        /// <param name="gameRules">规则实体</param>
        /// <param name="kindID">需要更新规则的游戏</param>
        void UpdateGameRules(GameRulesInfo gameRules,int kindID);

        /// <summary>
        /// 删除规则
        /// </summary>
        /// <param name="sqlQuery"></param>
        void DeleteGameRules(string sqlQuery);

        /// <summary>
        /// 判断规则是否存在
        /// </summary>
        /// <param name="kindID"></param>
        /// <returns></returns>
        bool JudgeRulesIsExistence( int kindID );

        #endregion

        #region 常见问题部分

        /// <summary>
        /// 获取问题列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="condition"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        PagerSet GetGameIssueList(int pageIndex, int pageSize, string condition, string orderby);

        /// <summary>
        /// 获取问题实体
        /// </summary>
        /// <param name="issueID"></param>
        /// <returns></returns>
        GameIssueInfo GetGameIssueInfo(int issueID);

        /// <summary>
        /// 新增问题
        /// </summary>
        /// <param name="gameIssue"></param>
        void InsertGameIssue(GameIssueInfo gameIssue);

        /// <summary>
        /// 更新问题
        /// </summary>
        /// <param name="gameIssue"></param>
        void UpdateGameIssue(GameIssueInfo gameIssue);

        /// <summary>
        /// 删除问题
        /// </summary>
        /// <param name="sqlQuery"></param>
        void DeleteGameIssue(string sqlQuery);
        #endregion

        #region 反馈管理

        /// <summary>
        /// 获取反馈列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="condition"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        PagerSet GetGameFeedbackList(int pageIndex, int pageSize, string condition, string orderby);

        /// <summary>
        /// 获取反馈实体
        /// </summary>
        /// <param name="feedbackID"></param>
        /// <returns></returns>
        GameFeedbackInfo GetGameFeedbackInfo(int feedbackID);

        /// <summary>
        /// 回复反馈
        /// </summary>
        /// <param name="gameFeedback"></param>
        void RevertGameFeedback(GameFeedbackInfo gameFeedback);

        /// <summary>
        /// 删除反馈
        /// </summary>
        /// <param name="sqlQuery"></param>
        void DeleteGameFeedback(string sqlQuery);

        /// <summary>
        /// 禁用反馈
        /// </summary>
        /// <param name="sqlQuery"></param>
        void SetFeedbackDisbale( string sqlQuery );
        /// <summary>
        /// 启用反馈
        /// </summary>
        /// <param name="sqlQuery"></param>
        void SetFeedbackEnbale( string sqlQuery );

        /// <summary>
        /// 获取未处理的留言总数
        /// </summary>
        /// <returns></returns>
        int GetNewMessageCounts();
        #endregion

        #region 公告部分

        /// <summary>
        /// 获取公告列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="condition"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        PagerSet GetNoticeList(int pageIndex, int pageSize, string condition, string orderby);

        /// <summary>
        /// 获取公告实体
        /// </summary>
        /// <param name="noticeID"></param>
        /// <returns></returns>
        Notice GetNoticeInfo(int noticeID);

        /// <summary>
        /// 新增公告
        /// </summary>
        /// <param name="notice"></param>
        void InsertNotice(Notice notice);

        /// <summary>
        /// 更新公告
        /// </summary>
        /// <param name="notice"></param>
        void UpdateNotice(Notice notice);

        /// <summary>
        /// 删除公告
        /// </summary>
        /// <param name="sqlQuery"></param>
        void DeleteNotice(string sqlQuery);
        /// <summary>
        /// 禁用公告
        /// </summary>
        /// <param name="sqlQuery"></param>
        void SetNoticeDisbale( string sqlQuery );
        /// <summary>
        /// 启用公告
        /// </summary>
        /// <param name="sqlQuery"></param>
        void SetNoticeEnbale( string sqlQuery );
        #endregion

        #region 比赛管理

        /// <summary>
        /// 获取比赛列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="condition"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        PagerSet GetGameMatchInfoList( int pageIndex, int pageSize, string condition, string orderby );

        /// <summary>
        /// 获取比赛实体
        /// </summary>
        /// <param name="matchID"></param>
        /// <returns></returns>
        GameMatchInfo GetGameMatchInfo( int matchID );

        /// <summary>
        /// 新增比赛
        /// </summary>
        /// <param name="gameMatch"></param>
        void InsertGameMatchInfo( GameMatchInfo gameMatch );

        /// <summary>
        /// 更新比赛
        /// </summary>
        /// <param name="gameMatch"></param>
        void UpdateGameMatchInfo( GameMatchInfo gameMatch );

        /// <summary>
        /// 删除比赛
        /// </summary>
        /// <param name="sqlQuery"></param>
        void DeleteGameMatchInfo( string sqlQuery );
        /// <summary>
        /// 禁用比赛
        /// </summary>
        /// <param name="sqlQuery"></param>
        void SetMatchDisbale( string sqlQuery );
        /// <summary>
        /// 启用比赛
        /// </summary>
        /// <param name="sqlQuery"></param>
        void SetMatchEnbale( string sqlQuery );

        /// <summary>
        /// 获取参赛用户列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="condition"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        PagerSet GetGameMatchUserInfoList( int pageIndex, int pageSize, string condition, string orderby );
        /// <summary>
        /// 禁用参赛用户
        /// </summary>
        /// <param name="sqlQuery"></param>
        void SetUserMatchDisbale( string sqlQuery );
        /// <summary>
        /// 启用参赛用户
        /// </summary>
        /// <param name="sqlQuery"></param>
        void SetUserMatchEnbale( string sqlQuery );
        #endregion

        #region 商品类型部分
        /// <summary>
        /// 根据父id获取类型实体
        /// </summary>
        /// <param name="parentId">父id</param>
        AwardType GetAwardTypeByPID( int typeID );

        /// <summary>
        /// 获取所有的商品类型
        /// </summary>
        DataSet GetAllAwardType();

        /// <summary>
        /// 获取所有的子类型
        /// </summary>
        DataSet GetAllChildType();

        /// <summary>
        /// 根据类型id获取类型实体
        /// </summary>
        /// <param name="typeId">类型Id</param>
        AwardType GetAwardTypeByID( int typeID );

        /// <summary>
        /// 保存商品类型信息
        /// </summary>
        /// <param name="type">类型实体</param>
        void InsertAwardTypeInfo( AwardType awardType );
       
        /// <summary>
        /// 更新商品类型信息
        /// </summary>
        /// <param name="awardType"></param>
        int UpdateAwardTypeInfo( AwardType awardType );

        /// <summary>
        /// 更改类型状态
        /// </summary>
        /// <param name="typeId">类型id</param>
        /// <param name="state">状态值</param>
        int UpdateNulity( string typeId, int state );
       
        /// <summary>
        /// 根据父类型获取对应子类型的id字符串
        /// </summary>
        /// <param name="Pid">父类型id</param>
        string GetChildAwardTypeByPID( int Pid );

        /// <summary>
        /// 删除商品类型
        /// </summary>
        /// <param name="typeId">商品类型标识</param>
        Message DeleteAwardType( int typeId );
     
        #endregion

        #region 商品信息
        /// <summary>
        /// 根据商品id获取商品实体
        /// </summary>
        /// <param name="id">商品id</param>
        AwardInfo GetAwardInfoByID( int id );

        /// <summary>
        /// 修改商品状态
        /// </summary>
        /// <param name="idList">商品id</param>
        /// <param name="state">商品状态</param>
        int UpdateAwardInfoNulity( string idList, int state );

        /// <summary>
        /// 保存商品信息
        /// </summary>
        /// <param name="info">商品实体</param>
        void InsertAwardInfo( AwardInfo info );

        /// <summary>
        /// 更新商品信息
        /// </summary>
        /// <param name="info">商品实体</param>
        int UpdateAwardInfo( AwardInfo info );

        /// <summary>
        /// 获取最大键值
        /// </summary>
        int GetMaxAwardInfoID();

        /// <summary>
        /// 判断该类型下是否存在商品
        /// </summary>
        /// <param name="typeId">类型id</param>
        bool IsHaveGoods( int typeID );
     
        #endregion

        #region 商品订单
        /// <summary>
        /// 根据订单号获取订单
        /// </summary>
        /// <param name="orderID">订单号</param>
        AwardOrder GetAwardOrderByID( int orderID );

        /// <summary>
        /// 修改订单状态
        /// </summary>
        /// <param name="state">状态值</param>
        /// <param name="OrderID">订单号</param>
        /// <param name="note">处理备注</param>
        int UpdateOrderState( int state, int orderID, string note );

        /// <summary>
        /// 获取新的订单总数
        /// </summary>
        /// <returns></returns>
        int GetNewOrderCounts();
        #endregion

        #region 站点配置

        /// <summary>
        /// 获取站点配置
        /// </summary>
        /// <param name="configID"></param>
        /// <returns></returns>
        ConfigInfo GetConfigInfo( int configID );

        /// <summary>
        /// 获取站点配置
        /// </summary>
        /// <param name="configKey">配置Key</param>
        /// <returns></returns>
        ConfigInfo GetConfigInfo( string configKey );

        /// <summary>
        /// 更新站点配置
        /// </summary>
        /// <param name="ci"></param>
        void UpdateConfigInfo( ConfigInfo ci );

        /// <summary>
        /// 获取站点最小配置ID
        /// </summary>
        /// <returns></returns>
        int GetConfigInfoMinID();
        
        #endregion

        #region 独立页管理

        /// <summary>
        /// 获取独立页
        /// </summary>
        /// <param name="configID">页ID</param>
        /// <returns></returns>
        SinglePage GetSinglePage( int pageID );

        /// <summary>
        /// 获取独立页
        /// </summary>
        /// <param name="configKey">配置Key</param>
        /// <returns></returns>
        SinglePage GetSinglePage( string keyValue );

        /// <summary>
        /// 更新独立页
        /// </summary>
        /// <param name="singlePage">独立页实体</param>
        int UpdateSinglePage( SinglePage singlePage );

        /// <summary>
        /// 获取独立页最小ID
        /// </summary>
        /// <returns></returns>
        int GetSinglePageMinID();
       
        #endregion

        #region 广告管理

        /// <summary>
        /// 获取广告实体
        /// </summary>
        /// <param name="ID">广告ID</param>
        /// <returns>广告实体</returns>
        Ads GetAds( int ID );

        /// <summary>
        /// 删除广告
        /// </summary>
        /// <param name="sqlQuery">删除条件</param>
        void DeleteAds( string sqlQuery );

        /// <summary>
        /// 添加广告
        /// </summary>
        /// <param name="ads">广告实体</param>
        void InsertAds( Ads ads );

        /// <summary>
        /// 更新广告
        /// </summary>
        /// <param name="ads">广告实体</param>
        void UpdateAds( Ads ads );

        #endregion

        #region 申诉管理

        /// <summary>
        /// 获取申诉实体
        /// </summary>
        /// <param name="reportID">申诉ID</param>
        /// <returns></returns>
        LossReport GetLossReport( int reportID );

        /// <summary>
        /// 更新申诉实体
        /// </summary>
        /// <param name="lossReport">申诉实体</param>
        /// <returns>成功返回True，失败返回False</returns>
        bool UpdateLossReport( LossReport lossReport );

        #endregion

        #region 活动管理

        /// <summary>
        /// 删除活动
        /// </summary>
        /// <param name="idList">删除活动ID列表</param>
        void DeleteActivity(string idList);

        /// <summary>
        /// 获取活动实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Activity GetActivity(int id);

        /// <summary>
        /// 添加活动
        /// </summary>
        /// <param name="model"></param>
        void AddActivity(Activity model);

        /// <summary>
        /// 更新活动
        /// </summary>
        /// <param name="model"></param>
        void UpdateActivity(Activity model);

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
