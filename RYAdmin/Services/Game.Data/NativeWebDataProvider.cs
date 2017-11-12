using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Game.Entity.NativeWeb;
using Game.IData;
using Game.Kernel;

namespace Game.Data
{
    /// <summary>
    /// 网站数据层
    /// </summary>
    public class NativeWebDataProvider : BaseDataProvider, INativeWebDataProvider
    {
        #region Fields

        private ITableProvider aideNewsProvider;
        private ITableProvider aideGameRulesProvider;
        private ITableProvider aideGameIssueProvider;
        private ITableProvider aideGameFeedbackProvider;
        private ITableProvider aideNoticeProvider;
        private ITableProvider aideGameMatchInfoProvider;
        private ITableProvider aideAwardTypeProvider;
        private ITableProvider aideAwardOrderProvider;
        private ITableProvider aideAwardInfoProvider;
        private ITableProvider aideAdsProvider;
        private ITableProvider aideLossReport;

        #endregion Fields

        #region 构造方法

        /// <summary>
        /// 构造函数
        /// </summary>
        public NativeWebDataProvider(string connString)
            : base(connString)
        {
            aideNewsProvider = GetTableProvider(News.Tablename);
            aideGameRulesProvider = GetTableProvider(GameRulesInfo.Tablename);
            aideGameIssueProvider = GetTableProvider(GameIssueInfo.Tablename);
            aideGameFeedbackProvider = GetTableProvider(GameFeedbackInfo.Tablename);
            aideNoticeProvider = GetTableProvider(Notice.Tablename);
            aideGameMatchInfoProvider = GetTableProvider(GameMatchInfo.Tablename);
            aideAwardTypeProvider = GetTableProvider(AwardType.Tablename);
            aideAwardOrderProvider = GetTableProvider(AwardOrder.Tablename);
            aideAwardInfoProvider = GetTableProvider(AwardInfo.Tablename);
            aideAdsProvider = GetTableProvider(Ads.Tablename);
            aideLossReport = GetTableProvider(LossReport.Tablename);
        }

        #endregion 构造方法

        #region 新闻部分

        /// <summary>
        /// 获取新闻列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="condition"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public PagerSet GetNewsList(int pageIndex, int pageSize, string condition, string orderby)
        {
            PagerParameters pagerPrams = new PagerParameters(News.Tablename, orderby, condition, pageIndex, pageSize);

            return GetPagerSet2(pagerPrams);
        }

        /// <summary>
        /// 获取新闻实体
        /// </summary>
        /// <param name="newsID"></param>
        /// <returns></returns>
        public News GetNewsInfo(int newsID)
        {
            string sqlQuery = string.Format("(NOLOCK) WHERE NewsID= {0}", newsID);
            News news = aideNewsProvider.GetObject<News>(sqlQuery);
            return news;
        }

        /// <summary>
        /// 新增新闻
        /// </summary>
        /// <param name="news"></param>
        public void InsertNews(News news)
        {
            DataRow dr = aideNewsProvider.NewRow();

            dr[News._NewsID] = news.NewsID;
            dr[News._PopID] = news.PopID;
            dr[News._Subject] = news.Subject;
            dr[News._Subject1] = news.Subject1;
            dr[News._OnTop] = news.OnTop;
            dr[News._OnTopAll] = news.OnTopAll;
            dr[News._IsElite] = news.IsElite;
            dr[News._IsHot] = news.IsHot;
            dr[News._IsLock] = news.IsLock;
            dr[News._IsDelete] = news.IsDelete;
            dr[News._IsLinks] = news.IsLinks;
            dr[News._LinkUrl] = news.LinkUrl;
            dr[News._Body] = news.Body;
            dr[News._FormattedBody] = news.FormattedBody;
            dr[News._HighLight] = news.HighLight;
            dr[News._ClassID] = news.ClassID;
            dr[News._GameRange] = news.GameRange;
            dr[News._ImageUrl] = news.ImageUrl;
            dr[News._UserID] = news.UserID;
            dr[News._IssueIP] = news.IssueIP;
            dr[News._LastModifyIP] = news.LastModifyIP;
            dr[News._IssueDate] = news.IssueDate;
            dr[News._LastModifyDate] = news.LastModifyDate;

            aideNewsProvider.Insert(dr);
        }

        /// <summary>
        /// 更新新闻
        /// </summary>
        /// <param name="news"></param>
        public void UpdateNews(News news)
        {
            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.Append("UPDATE News SET ")
                    .Append("PopID=@PopID ,")
                    .Append("Subject=@Subject,")
                    .Append("Subject1= @Subject1 ,")

                    .Append("OnTop= @OnTop ,")
                    .Append("OnTopAll= @OnTopAll,")
                    .Append("IsElite=@IsElite ,")
                    .Append("IsHot= @IsHot,")
                    .Append("IsLock= @IsLock ,")
                    .Append("IsDelete=@IsDelete,")
                    .Append("IsLinks=@IsLinks,")
                    .Append("LinkUrl=@LinkUrl ,")

                    .Append("Body= @Body ,")
                    .Append("FormattedBody= @FormattedBody,")

                    .Append("HighLight= @HighLight,")
                    .Append("ClassID= @ClassID,")
                    .Append("GameRange= @GameRange,")
                    .Append("ImageUrl= @ImageUrl,")

                    .Append("UserID=@UserID,")
                    .Append("IssueIP=@IssueIP,")
                    .Append("LastModifyIP=@LastModifyIP ,")
                    .Append("LastModifyDate=@LastModifyDate  ")

                    .Append("WHERE NewsID= @NewsID");

            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("PopID", news.PopID));
            prams.Add(Database.MakeInParam("Subject", news.Subject));
            prams.Add(Database.MakeInParam("Subject1", news.Subject1));

            prams.Add(Database.MakeInParam("OnTop", news.OnTop));
            prams.Add(Database.MakeInParam("OnTopAll", news.OnTopAll));
            prams.Add(Database.MakeInParam("IsElite", news.IsElite));
            prams.Add(Database.MakeInParam("IsHot", news.IsHot));
            prams.Add(Database.MakeInParam("IsLock", news.IsLock));
            prams.Add(Database.MakeInParam("IsDelete", news.IsDelete));
            prams.Add(Database.MakeInParam("IsLinks", news.IsLinks));
            prams.Add(Database.MakeInParam("LinkUrl", news.LinkUrl));

            prams.Add(Database.MakeInParam("Body", news.Body));
            prams.Add(Database.MakeInParam("FormattedBody", news.FormattedBody));

            prams.Add(Database.MakeInParam("HighLight", news.HighLight));
            prams.Add(Database.MakeInParam("ClassID", news.ClassID));
            prams.Add(Database.MakeInParam("GameRange", news.GameRange));
            prams.Add(Database.MakeInParam("ImageUrl", news.ImageUrl));

            prams.Add(Database.MakeInParam("UserID", news.UserID));
            prams.Add(Database.MakeInParam("IssueIP", news.IssueIP));
            prams.Add(Database.MakeInParam("LastModifyIP", news.LastModifyIP));
            prams.Add(Database.MakeInParam("IssueDate", news.IssueDate));
            prams.Add(Database.MakeInParam("LastModifyDate", news.LastModifyDate));

            prams.Add(Database.MakeInParam("NewsID", news.NewsID));
            Database.ExecuteNonQuery(CommandType.Text, sqlQuery.ToString(), prams.ToArray());
        }

        /// <summary>
        /// 删除新闻
        /// </summary>
        /// <param name="sqlQuery"></param>
        public void DeleteNews(string sqlQuery)
        {
            aideNewsProvider.Delete(sqlQuery);
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
        public PagerSet GetGameRulesList(int pageIndex, int pageSize, string condition, string orderby)
        {
            PagerParameters pagerPrams = new PagerParameters(GameRulesInfo.Tablename, orderby, condition, pageIndex, pageSize);

            return GetPagerSet2(pagerPrams);
        }

        /// <summary>
        /// 获取规则实体
        /// </summary>
        /// <param name="kindID"></param>
        /// <returns></returns>
        public GameRulesInfo GetGameRulesInfo(int kindID)
        {
            string sqlQuery = string.Format("(NOLOCK) WHERE KindID= {0}", kindID);
            GameRulesInfo gameRules = aideGameRulesProvider.GetObject<GameRulesInfo>(sqlQuery);
            return gameRules;
        }

        /// <summary>
        /// 新增规则
        /// </summary>
        /// <param name="gameRules"></param>
        public void InsertGameRules(GameRulesInfo gameRules)
        {
            DataRow dr = aideGameRulesProvider.NewRow();

            dr[GameRulesInfo._KindID] = gameRules.KindID;
            dr[GameRulesInfo._KindName] = gameRules.KindName;
            dr[GameRulesInfo._ImgRuleUrl] = gameRules.ImgRuleUrl;
            dr[GameRulesInfo._ThumbnailUrl] = gameRules.ThumbnailUrl;
            dr[GameRulesInfo._MobileImgUrl] = gameRules.MobileImgUrl;
            dr[GameRulesInfo._MobileSize] = gameRules.MobileSize;
            dr[GameRulesInfo._MobileDate] = gameRules.MobileDate;
            dr[GameRulesInfo._MobileVersion] = gameRules.MobileVersion;

            dr[GameRulesInfo._MobileGameType] = gameRules.MobileGameType;
            dr[GameRulesInfo._AndroidDownloadUrl] = gameRules.AndroidDownloadUrl;
            dr[GameRulesInfo._IOSDownloadUrl] = gameRules.IOSDownloadUrl;

            dr[GameRulesInfo._HelpIntro] = gameRules.HelpIntro;
            dr[GameRulesInfo._HelpRule] = gameRules.HelpRule;
            dr[GameRulesInfo._HelpGrade] = gameRules.HelpGrade;

            dr[GameRulesInfo._JoinIntro] = gameRules.JoinIntro;
            dr[GameRulesInfo._Nullity] = gameRules.Nullity;
            dr[GameRulesInfo._CollectDate] = gameRules.CollectDate;
            dr[GameRulesInfo._ModifyDate] = gameRules.ModifyDate;



            aideGameRulesProvider.Insert(dr);
        }

        /// <summary>
        /// 更新规则
        /// </summary>
        /// <param name="gameRules">规则实体</param>
        /// <param name="kindID">需要更新规则的游戏ID</param>
        public void UpdateGameRules(GameRulesInfo gameRules, int kindID)
        {
            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.Append("UPDATE GameRulesInfo SET ")
                    .Append("KindID = @KindID ,")
                    .Append("KindName = @KindName ,")
                    .Append("ImgRuleUrl = @ImgRuleUrl,")
                    .Append("ThumbnailUrl = @ThumbnailUrl,")
                    .Append("MobileImgUrl = @MobileImgUrl,")
                    .Append("MobileSize = @MobileSize,")
                    .Append("MobileDate = @MobileDate,")
                    .Append("MobileVersion = @MobileVersion,")
                    .Append("MobileGameType = @MobileGameType ,")
                    .Append("AndroidDownloadUrl = @AndroidDownloadUrl,")
                    .Append("IOSDownloadUrl = @IOSDownloadUrl,")
                    .Append("HelpIntro = @HelpIntro ,")
                    .Append("HelpRule = @HelpRule,")
                    .Append("HelpGrade = @HelpGrade,")
                    .Append("JoinIntro = @JoinIntro,")
                    .Append("Nullity = @Nullity,")
                    .Append("ModifyDate = @ModifyDate ")
                    .Append("WHERE KindID = @OldKindID");

            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("KindID", gameRules.KindID));
            prams.Add(Database.MakeInParam("KindName", gameRules.KindName));
            prams.Add(Database.MakeInParam("ImgRuleUrl", gameRules.ImgRuleUrl));
            prams.Add(Database.MakeInParam("ThumbnailUrl", gameRules.ThumbnailUrl));
            prams.Add(Database.MakeInParam("MobileImgUrl", gameRules.MobileImgUrl));
            prams.Add(Database.MakeInParam("MobileSize", gameRules.MobileSize));
            prams.Add(Database.MakeInParam("MobileDate", gameRules.MobileDate));
            prams.Add(Database.MakeInParam("MobileVersion", gameRules.MobileVersion));

            prams.Add(Database.MakeInParam("MobileGameType", gameRules.MobileGameType));
            prams.Add(Database.MakeInParam("AndroidDownloadUrl", gameRules.AndroidDownloadUrl));
            prams.Add(Database.MakeInParam("IOSDownloadUrl", gameRules.IOSDownloadUrl));

            prams.Add(Database.MakeInParam("HelpIntro", gameRules.HelpIntro));
            prams.Add(Database.MakeInParam("HelpRule", gameRules.HelpRule));
            prams.Add(Database.MakeInParam("HelpGrade", gameRules.HelpGrade));

            prams.Add(Database.MakeInParam("JoinIntro", gameRules.JoinIntro));
            prams.Add(Database.MakeInParam("Nullity", gameRules.Nullity));
            prams.Add(Database.MakeInParam("ModifyDate", gameRules.ModifyDate));
            prams.Add(Database.MakeInParam("OldKindID", kindID));
            Database.ExecuteNonQuery(CommandType.Text, sqlQuery.ToString(), prams.ToArray());
        }

        /// <summary>
        /// 删除规则
        /// </summary>
        /// <param name="sqlQuery"></param>
        public void DeleteGameRules(string sqlQuery)
        {
            aideGameRulesProvider.Delete(sqlQuery);
        }

        ///<summary>
        ///判断游戏规则是否存在
        ///</summary>
        ///<paraparam name="kindID">游戏ID</paraparam>
        ///<returns>存在返回true,不存在返回false</returns>
        public bool JudgeRulesIsExistence(int kindID)
        {
            string sqlQuery = string.Format("(NOLOCK) WHERE KindID= {0}", kindID);
            GameRulesInfo gameRules = aideGameRulesProvider.GetObject<GameRulesInfo>(sqlQuery);
            if(gameRules == null)
            {
                return false;
            }
            else
            {
                return true;
            }
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
        public PagerSet GetGameIssueList(int pageIndex, int pageSize, string condition, string orderby)
        {
            PagerParameters pagerPrams = new PagerParameters(GameIssueInfo.Tablename, orderby, condition, pageIndex, pageSize);

            return GetPagerSet2(pagerPrams);
        }

        /// <summary>
        /// 获取问题实体
        /// </summary>
        /// <param name="issueID"></param>
        /// <returns></returns>
        public GameIssueInfo GetGameIssueInfo(int issueID)
        {
            string sqlQuery = string.Format("(NOLOCK) WHERE IssueID= {0}", issueID);
            GameIssueInfo gameIssue = aideGameIssueProvider.GetObject<GameIssueInfo>(sqlQuery);
            return gameIssue;
        }

        /// <summary>
        /// 新增问题
        /// </summary>
        /// <param name="gameIssue"></param>
        public void InsertGameIssue(GameIssueInfo gameIssue)
        {
            DataRow dr = aideGameIssueProvider.NewRow();

            dr[GameIssueInfo._IssueID] = gameIssue.IssueID;
            dr[GameIssueInfo._IssueTitle] = gameIssue.IssueTitle;
            dr[GameIssueInfo._IssueContent] = gameIssue.IssueContent;
            dr[GameIssueInfo._TypeID] = gameIssue.TypeID;
            dr[GameIssueInfo._Nullity] = gameIssue.Nullity;
            dr[GameIssueInfo._CollectDate] = gameIssue.CollectDate;
            dr[GameIssueInfo._ModifyDate] = gameIssue.ModifyDate;

            aideGameIssueProvider.Insert(dr);
        }

        /// <summary>
        /// 更新问题
        /// </summary>
        /// <param name="gameIssue"></param>
        public void UpdateGameIssue(GameIssueInfo gameIssue)
        {
            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.Append("UPDATE GameIssueInfo SET ")
                    .Append("IssueTitle=@IssueTitle ,")
                    .Append("IssueContent=@IssueContent,")
                    .Append("TypeID=@TypeID,")
                    .Append("Nullity= @Nullity ,")
                    .Append("ModifyDate= @ModifyDate ")

                    .Append("WHERE IssueID= @IssueID");

            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("IssueTitle", gameIssue.IssueTitle));
            prams.Add(Database.MakeInParam("IssueContent", gameIssue.IssueContent));
            prams.Add(Database.MakeInParam("TypeID", gameIssue.TypeID));
            prams.Add(Database.MakeInParam("Nullity", gameIssue.Nullity));
            prams.Add(Database.MakeInParam("ModifyDate", gameIssue.ModifyDate));

            prams.Add(Database.MakeInParam("IssueID", gameIssue.IssueID));
            Database.ExecuteNonQuery(CommandType.Text, sqlQuery.ToString(), prams.ToArray());
        }

        /// <summary>
        /// 删除问题
        /// </summary>
        /// <param name="sqlQuery"></param>
        public void DeleteGameIssue(string sqlQuery)
        {
            aideGameIssueProvider.Delete(sqlQuery);
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
        public PagerSet GetGameFeedbackList(int pageIndex, int pageSize, string condition, string orderby)
        {
            PagerParameters pagerPrams = new PagerParameters(GameFeedbackInfo.Tablename, orderby, condition, pageIndex, pageSize);

            return GetPagerSet2(pagerPrams);
        }

        /// <summary>
        /// 获取反馈实体
        /// </summary>
        /// <param name="feedbackID"></param>
        /// <returns></returns>
        public GameFeedbackInfo GetGameFeedbackInfo(int feedbackID)
        {
            string sqlQuery = string.Format("(NOLOCK) WHERE FeedbackID= {0}", feedbackID);
            GameFeedbackInfo gameFeedback = aideGameFeedbackProvider.GetObject<GameFeedbackInfo>(sqlQuery);
            return gameFeedback;
        }

        /// <summary>
        /// 回复反馈
        /// </summary>
        /// <param name="gameFeedback"></param>
        public void RevertGameFeedback(GameFeedbackInfo gameFeedback)
        {
            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.Append("UPDATE GameFeedbackInfo SET ")
                    .Append("RevertUserID=@RevertUserID ,")
                    .Append("RevertContent=@RevertContent,")
                    .Append("RevertDate= @RevertDate, ")
                    .Append("Nullity= @Nullity, ")
                    .Append("IsProcessed= @IsProcessed ")
                    .Append("WHERE FeedbackID= @FeedbackID");

            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("RevertUserID", gameFeedback.RevertUserID));
            prams.Add(Database.MakeInParam("RevertContent", gameFeedback.RevertContent));
            prams.Add(Database.MakeInParam("RevertDate", gameFeedback.RevertDate));
            prams.Add(Database.MakeInParam("Nullity", gameFeedback.Nullity));
            prams.Add(Database.MakeInParam("IsProcessed", gameFeedback.IsProcessed));

            prams.Add(Database.MakeInParam("FeedbackID", gameFeedback.FeedbackID));

            Database.ExecuteNonQuery(CommandType.Text, sqlQuery.ToString(), prams.ToArray());
        }

        /// <summary>
        /// 删除反馈
        /// </summary>
        /// <param name="sqlQuery"></param>
        public void DeleteGameFeedback(string sqlQuery)
        {
            aideGameFeedbackProvider.Delete(sqlQuery);
        }

        /// <summary>
        /// 禁用反馈
        /// </summary>
        /// <param name="sqlQuery"></param>
        public void SetFeedbackDisbale(string sqlQuery)
        {
            string sqlQueryAll = string.Format("UPDATE GameFeedbackInfo SET Nullity=1 {0}", sqlQuery);
            Database.ExecuteNonQuery(sqlQueryAll);
        }

        /// <summary>
        /// 启用反馈
        /// </summary>
        /// <param name="sqlQuery"></param>
        public void SetFeedbackEnbale(string sqlQuery)
        {
            string sqlQueryAll = string.Format("UPDATE GameFeedbackInfo SET Nullity=0 {0}", sqlQuery);
            Database.ExecuteNonQuery(sqlQueryAll);
        }

        /// <summary>
        /// 获取未处理的留言总数
        /// </summary>
        /// <returns></returns>
        public int GetNewMessageCounts()
        {
            string sqlQuery = "SELECT COUNT(FeedbackID) FROM GameFeedbackInfo WHERE IsProcessed=0";
            object obj = Database.ExecuteScalar(CommandType.Text, sqlQuery);
            if(obj != null)
            {
                return Convert.ToInt32(obj);
            }
            return 0;
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
        public PagerSet GetNoticeList(int pageIndex, int pageSize, string condition, string orderby)
        {
            PagerParameters pagerPrams = new PagerParameters(Notice.Tablename, orderby, condition, pageIndex, pageSize);

            return GetPagerSet2(pagerPrams);
        }

        /// <summary>
        /// 获取公告实体
        /// </summary>
        /// <param name="noticeID"></param>
        /// <returns></returns>
        public Notice GetNoticeInfo(int noticeID)
        {
            string sqlQuery = string.Format("(NOLOCK) WHERE NoticeID= {0}", noticeID);
            Notice notice = aideNoticeProvider.GetObject<Notice>(sqlQuery);
            return notice;
        }

        /// <summary>
        /// 新增公告
        /// </summary>
        /// <param name="notice"></param>
        public void InsertNotice(Notice notice)
        {
            DataRow dr = aideNoticeProvider.NewRow();

            dr[Notice._Subject] = notice.Subject;
            dr[Notice._IsLink] = notice.IsLink;
            dr[Notice._LinkUrl] = notice.LinkUrl;
            dr[Notice._Body] = notice.Body;
            dr[Notice._Location] = notice.Location;
            dr[Notice._Width] = notice.Width;
            dr[Notice._Height] = notice.Height;
            dr[Notice._StartDate] = notice.StartDate;
            dr[Notice._OverDate] = notice.OverDate;
            dr[Notice._Nullity] = notice.Nullity;
            aideNoticeProvider.Insert(dr);
        }

        /// <summary>
        /// 更新公告
        /// </summary>
        /// <param name="notice"></param>
        public void UpdateNotice(Notice notice)
        {
            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.Append("UPDATE Notice SET ")
                    .Append("Subject=@Subject ,")
                    .Append("IsLink=@IsLink,")
                    .Append("LinkUrl= @LinkUrl ,")
                    .Append("Body= @Body,")
                    .Append("Location= @Location,")
                    .Append("Width=@Width,")
                    .Append("Height= @Height,")
                    .Append("StartDate= @StartDate,")
                    .Append("OverDate=@OverDate,")
                    .Append("Nullity=@Nullity ")
                    .Append("WHERE NoticeID= @NoticeID");

            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("Subject", notice.Subject));
            prams.Add(Database.MakeInParam("IsLink", notice.IsLink));
            prams.Add(Database.MakeInParam("LinkUrl", notice.LinkUrl));
            prams.Add(Database.MakeInParam("Body", notice.Body));
            prams.Add(Database.MakeInParam("Location", notice.Location));
            prams.Add(Database.MakeInParam("Width", notice.Width));
            prams.Add(Database.MakeInParam("Height", notice.Height));
            prams.Add(Database.MakeInParam("StartDate", notice.StartDate));
            prams.Add(Database.MakeInParam("OverDate", notice.OverDate));
            prams.Add(Database.MakeInParam("Nullity", notice.Nullity));

            prams.Add(Database.MakeInParam("NoticeID", notice.NoticeID));
            Database.ExecuteNonQuery(CommandType.Text, sqlQuery.ToString(), prams.ToArray());
        }

        /// <summary>
        /// 删除公告
        /// </summary>
        /// <param name="sqlQuery"></param>
        public void DeleteNotice(string sqlQuery)
        {
            aideNoticeProvider.Delete(sqlQuery);
        }

        /// <summary>
        /// 禁用公告
        /// </summary>
        /// <param name="sqlQuery"></param>
        public void SetNoticeDisbale(string sqlQuery)
        {
            string sqlQueryAll = string.Format("UPDATE Notice SET Nullity=1 {0}", sqlQuery);
            Database.ExecuteNonQuery(sqlQueryAll);
        }

        /// <summary>
        /// 启用公告
        /// </summary>
        /// <param name="sqlQuery"></param>
        public void SetNoticeEnbale(string sqlQuery)
        {
            string sqlQueryAll = string.Format("UPDATE Notice SET Nullity=0 {0}", sqlQuery);
            Database.ExecuteNonQuery(sqlQueryAll);
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
        public PagerSet GetGameMatchInfoList(int pageIndex, int pageSize, string condition, string orderby)
        {
            PagerParameters pagerPrams = new PagerParameters(GameMatchInfo.Tablename, orderby, condition, pageIndex, pageSize);

            return GetPagerSet2(pagerPrams);
        }

        /// <summary>
        /// 获取比赛实体
        /// </summary>
        /// <param name="matchID"></param>
        /// <returns></returns>
        public GameMatchInfo GetGameMatchInfo(int matchID)
        {
            string sqlQuery = string.Format("(NOLOCK) WHERE MatchID= {0}", matchID);
            GameMatchInfo match = aideGameMatchInfoProvider.GetObject<GameMatchInfo>(sqlQuery);
            return match;
        }

        /// <summary>
        /// 新增比赛
        /// </summary>
        /// <param name="gameMatch"></param>
        public void InsertGameMatchInfo(GameMatchInfo gameMatch)
        {
            DataRow dr = aideGameMatchInfoProvider.NewRow();

            dr[GameMatchInfo._MatchID] = gameMatch.MatchID;
            dr[GameMatchInfo._MatchTitle] = gameMatch.MatchTitle;
            dr[GameMatchInfo._MatchSummary] = gameMatch.MatchSummary;
            dr[GameMatchInfo._MatchContent] = gameMatch.MatchContent;
            dr[GameMatchInfo._ApplyBeginDate] = gameMatch.ApplyBeginDate;
            dr[GameMatchInfo._ApplyEndDate] = gameMatch.ApplyEndDate;
            dr[GameMatchInfo._MatchStatus] = gameMatch.MatchStatus;
            dr[GameMatchInfo._Nullity] = gameMatch.Nullity;
            dr[GameMatchInfo._CollectDate] = gameMatch.CollectDate;
            dr[GameMatchInfo._ModifyDate] = gameMatch.ModifyDate;

            aideGameMatchInfoProvider.Insert(dr);
        }

        /// <summary>
        /// 更新比赛
        /// </summary>
        /// <param name="gameMatch"></param>
        public void UpdateGameMatchInfo(GameMatchInfo gameMatch)
        {
            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.Append("UPDATE GameMatchInfo SET ")
                    .Append("MatchTitle=@MatchTitle ,")
                    .Append("MatchSummary=@MatchSummary,")
                    .Append("MatchContent= @MatchContent ,")
                    .Append("ApplyBeginDate= @ApplyBeginDate,")
                    .Append("ApplyEndDate= @ApplyEndDate,")
                    .Append("MatchStatus=@MatchStatus,")
                    .Append("Nullity= @Nullity,")
                    .Append("ModifyDate=@ModifyDate ")
                    .Append("WHERE MatchID= @MatchID");

            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("MatchTitle", gameMatch.MatchTitle));
            prams.Add(Database.MakeInParam("MatchSummary", gameMatch.MatchSummary));
            prams.Add(Database.MakeInParam("MatchContent", gameMatch.MatchContent));
            prams.Add(Database.MakeInParam("ApplyBeginDate", gameMatch.ApplyBeginDate));
            prams.Add(Database.MakeInParam("ApplyEndDate", gameMatch.ApplyEndDate));
            prams.Add(Database.MakeInParam("MatchStatus", gameMatch.MatchStatus));
            prams.Add(Database.MakeInParam("Nullity", gameMatch.Nullity));
            prams.Add(Database.MakeInParam("ModifyDate", gameMatch.ModifyDate));
            prams.Add(Database.MakeInParam("MatchID", gameMatch.MatchID));

            Database.ExecuteNonQuery(CommandType.Text, sqlQuery.ToString(), prams.ToArray());
        }

        /// <summary>
        /// 删除比赛
        /// </summary>
        /// <param name="sqlQuery"></param>
        public void DeleteGameMatchInfo(string sqlQuery)
        {
            aideGameMatchInfoProvider.Delete(sqlQuery);
        }

        /// <summary>
        /// 禁用比赛
        /// </summary>
        /// <param name="sqlQuery"></param>
        public void SetMatchDisbale(string sqlQuery)
        {
            string sqlQueryAll = string.Format("UPDATE GameMatchInfo SET Nullity=1 {0}", sqlQuery);
            Database.ExecuteNonQuery(sqlQueryAll);
        }

        /// <summary>
        /// 启用比赛
        /// </summary>
        /// <param name="sqlQuery"></param>
        public void SetMatchEnbale(string sqlQuery)
        {
            string sqlQueryAll = string.Format("UPDATE GameMatchInfo SET Nullity=0 {0}", sqlQuery);
            Database.ExecuteNonQuery(sqlQueryAll);
        }

        /// <summary>
        /// 获取参赛用户列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="condition"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public PagerSet GetGameMatchUserInfoList(int pageIndex, int pageSize, string condition, string orderby)
        {
            PagerParameters pagerPrams = new PagerParameters(GameMatchUserInfo.Tablename, orderby, condition, pageIndex, pageSize);

            return GetPagerSet2(pagerPrams);
        }

        /// <summary>
        /// 禁用参赛用户
        /// </summary>
        /// <param name="sqlQuery"></param>
        public void SetUserMatchDisbale(string sqlQuery)
        {
            string sqlQueryAll = string.Format("UPDATE GameMatchUserInfo SET Nullity=1 {0}", sqlQuery);
            Database.ExecuteNonQuery(sqlQueryAll);
        }

        /// <summary>
        /// 启用参赛用户
        /// </summary>
        /// <param name="sqlQuery"></param>
        public void SetUserMatchEnbale(string sqlQuery)
        {
            string sqlQueryAll = string.Format("UPDATE GameMatchUserInfo SET Nullity=0 {0}", sqlQuery);
            Database.ExecuteNonQuery(sqlQueryAll);
        }

        #endregion 比赛管理

        #region 商品类型部分

        /// <summary>
        /// 根据父id获取类型实体
        /// </summary>
        /// <param name="parentId">父id</param>
        public AwardType GetAwardTypeByPID(int typeID)
        {
            string sqlQuery = string.Format(" WHERE TypeID={0}", typeID);
            AwardType awardType = aideAwardTypeProvider.GetObject<AwardType>(sqlQuery);
            return awardType;
        }

        /// <summary>
        /// 获取所有的商品类型
        /// </summary>
        public DataSet GetAllAwardType()
        {
            string sqlQuery = "SELECT * FROM dbo.AwardType WHERE Nullity=0";
            return Database.ExecuteDataset(sqlQuery);
        }

        /// <summary>
        /// 获取所有的子类型
        /// </summary>
        public DataSet GetAllChildType()
        {
            string sql = "SELECT * FROM dbo.AwardType WHERE ParentID!=0 AND Nullity=0";
            return Database.ExecuteDataset(sql);
        }

        /// <summary>
        /// 根据类型id获取类型实体
        /// </summary>
        /// <param name="typeId">类型Id</param>
        public AwardType GetAwardTypeByID(int typeID)
        {
            string sqlQuery = string.Format(" WHERE TypeID={0}", typeID);
            AwardType awardType = aideAwardTypeProvider.GetObject<AwardType>(sqlQuery);
            return awardType;
        }

        /// <summary>
        /// 保存商品类型信息
        /// </summary>
        /// <param name="type">类型实体</param>
        public void InsertAwardTypeInfo(AwardType awardType)
        {
            DataRow dr = aideAwardTypeProvider.NewRow();
            dr[AwardType._TypeName] = awardType.TypeName;
            dr[AwardType._ParentID] = awardType.ParentID;
            dr[AwardType._SortID] = awardType.SortID;
            dr[AwardType._Nullity] = awardType.Nullity;
            dr[AwardType._CollectDate] = awardType.CollectDate;
            aideAwardTypeProvider.Insert(dr);
        }

        /// <summary>
        /// 更新商品类型信息
        /// </summary>
        /// <param name="awardType"></param>
        public int UpdateAwardTypeInfo(AwardType awardType)
        {
            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.Append("UPDATE AwardType SET ")
                    .Append("TypeName=@TypeName,")
                    .Append("ParentID=@ParentID,")
                    .Append("SortID=@SortID,")
                    .Append("Nullity=@Nullity,")
                    .Append("CollectDate=@CollectDate")
                    .Append(" WHERE TypeID= @TypeID");

            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("TypeName", awardType.TypeName));
            prams.Add(Database.MakeInParam("ParentID", awardType.ParentID));
            prams.Add(Database.MakeInParam("SortID", awardType.SortID));
            prams.Add(Database.MakeInParam("Nullity", awardType.Nullity));
            prams.Add(Database.MakeInParam("CollectDate", awardType.CollectDate));
            prams.Add(Database.MakeInParam("TypeID", awardType.TypeID));

            return Database.ExecuteNonQuery(CommandType.Text, sqlQuery.ToString(), prams.ToArray());
        }

        /// <summary>
        /// 更改类型状态
        /// </summary>
        /// <param name="typeId">类型id</param>
        /// <param name="state">状态值</param>
        public int UpdateNulity(string typeId, int state)
        {
            string sql = string.Format("UPDATE AwardType SET Nullity={0} WHERE TypeID IN({1})", state, typeId);
            return Database.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 根据父类型获取对应子类型的id字符串
        /// </summary>
        /// <param name="Pid">父类型id</param>
        public string GetChildAwardTypeByPID(int Pid)
        {
            string sql = string.Format("SELECT TypeID FROM AwardType WHERE ParentID={0}", Pid);
            DataSet ds = Database.ExecuteDataset(sql);
            string strId = "";
            foreach(DataRow item in ds.Tables[0].Rows)
            {
                strId = strId + item["TypeID"].ToString() + ",";
            }
            if(strId != "")
            {
                return strId.Substring(0, strId.Length - 1);
            }
            return "";
        }

        /// <summary>
        /// 删除商品类型
        /// </summary>
        /// <param name="typeId">商品类型标识</param>
        public Message DeleteAwardType(int typeId)
        {
            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("TypeID", typeId));
            prams.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));
            return MessageHelper.GetMessage(Database, "WSP_PM_DeleteAwardType", prams);
        }

        #endregion 商品类型部分

        #region 商品信息

        /// <summary>
        /// 根据商品id获取商品实体
        /// </summary>
        /// <param name="id">商品id</param>
        public AwardInfo GetAwardInfoByID(int id)
        {
            string sqlQuery = string.Format(" WHERE AwardID={0}", id);
            AwardInfo awardInfo = aideAwardInfoProvider.GetObject<AwardInfo>(sqlQuery);
            return awardInfo;
        }

        /// <summary>
        /// 修改商品状态
        /// </summary>
        /// <param name="idList">商品id</param>
        /// <param name="state">商品状态</param>
        public int UpdateAwardInfoNulity(string idList, int state)
        {
            string sql = string.Format("UPDATE dbo.AwardInfo SET Nullity={0} WHERE AwardID IN ({1})", state, idList);
            return Database.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 保存商品信息
        /// </summary>
        /// <param name="info">商品实体</param>
        public void InsertAwardInfo(AwardInfo info)
        {
            DataRow dr = aideAwardInfoProvider.NewRow();
            dr[AwardInfo._AwardName] = info.AwardName;
            dr[AwardInfo._BigImage] = info.BigImage;
            dr[AwardInfo._BuyCount] = info.BuyCount;
            dr[AwardInfo._Description] = info.Description;
            dr[AwardInfo._Inventory] = info.Inventory;
            dr[AwardInfo._CollectDate] = info.CollectDate;
            dr[AwardInfo._IsMember] = info.IsMember;
            dr[AwardInfo._IsReturn] = info.IsReturn;
            dr[AwardInfo._NeedInfo] = info.NeedInfo;
            dr[AwardInfo._Nullity] = info.Nullity;
            dr[AwardInfo._Price] = info.Price;
            dr[AwardInfo._SmallImage] = info.SmallImage;
            dr[AwardInfo._SortID] = info.SortID;
            dr[AwardInfo._TypeID] = info.TypeID;
            aideAwardInfoProvider.Insert(dr);
        }

        /// <summary>
        /// 更新商品信息
        /// </summary>
        /// <param name="info">商品实体</param>
        public int UpdateAwardInfo(AwardInfo info)
        {
            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.Append("UPDATE AwardInfo SET ")
                    .Append("AwardName=@AwardName,")
                    .Append("BigImage=@BigImage,")
                    .Append("BuyCount=@BuyCount,")
                    .Append("Description=@Description,")
                    .Append("Inventory=@Inventory,")
                    .Append("IsMember=@IsMember,")
                    .Append("IsReturn=@IsReturn,")
                    .Append("NeedInfo=@NeedInfo,")
                    .Append("Nullity=@Nullity,")
                    .Append("Price=@Price,")
                    .Append("SmallImage=@SmallImage,")
                    .Append("SortID=@SortID,")
                    .Append("TypeID=@TypeID ")
                    .Append(" WHERE AwardID= @AwardID");

            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("AwardName", info.AwardName));
            prams.Add(Database.MakeInParam("BigImage", info.BigImage));
            prams.Add(Database.MakeInParam("BuyCount", info.BuyCount));
            prams.Add(Database.MakeInParam("Description", info.Description));
            prams.Add(Database.MakeInParam("Inventory", info.Inventory));
            prams.Add(Database.MakeInParam("CollectDate", info.CollectDate));
            prams.Add(Database.MakeInParam("IsMember", info.IsMember));
            prams.Add(Database.MakeInParam("IsReturn", info.IsReturn));
            prams.Add(Database.MakeInParam("NeedInfo", info.NeedInfo));
            prams.Add(Database.MakeInParam("Nullity", info.Nullity));
            prams.Add(Database.MakeInParam("Price", info.Price));
            prams.Add(Database.MakeInParam("SmallImage", info.SmallImage));
            prams.Add(Database.MakeInParam("SortID", info.SortID));
            prams.Add(Database.MakeInParam("TypeID", info.TypeID));
            prams.Add(Database.MakeInParam("AwardID", info.AwardID));

            return Database.ExecuteNonQuery(CommandType.Text, sqlQuery.ToString(), prams.ToArray());
        }

        /// <summary>
        /// 获取最大键值
        /// </summary>
        public int GetMaxAwardInfoID()
        {
            string sql = string.Format("SELECT MAX(AwardID) FROM dbo.AwardInfo");
            return Convert.ToInt32(Database.ExecuteScalar(CommandType.Text, sql));
        }

        /// <summary>
        /// 判断该类型下是否存在商品
        /// </summary>
        /// <param name="typeId">类型id</param>
        public bool IsHaveGoods(int typeID)
        {
            string sqlQuery = "SELECT TOP 1 * FROM AwardInfo WHERE TypeID=" + typeID;
            int result = Database.ExecuteNonQuery(CommandType.Text, sqlQuery);
            return result > 0 ? true : false;
        }

        #endregion 商品信息

        #region 商品订单

        /// <summary>
        /// 根据订单号获取订单
        /// </summary>
        /// <param name="orderID">订单号</param>
        public AwardOrder GetAwardOrderByID(int orderID)
        {
            string sqlQuery = "SELECT * FROM AwardOrder WHERE OrderID=" + orderID;
            return Database.ExecuteObject<AwardOrder>(sqlQuery);
        }

        /// <summary>
        /// 修改订单状态
        /// </summary>
        /// <param name="state">状态值</param>
        /// <param name="OrderID">订单号</param>
        /// <param name="note">处理备注</param>
        public int UpdateOrderState(int state, int orderID, string note)
        {
            string sql = string.Format("UPDATE dbo.AwardOrder SET OrderStatus={0},SolveDate='{1}',SolveNote='{2}' WHERE OrderID={3}",
                state, DateTime.Now, note, orderID);
            return Database.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 获取新的订单总数
        /// </summary>
        /// <returns></returns>
        public int GetNewOrderCounts()
        {
            string sqlQuery = "SELECT COUNT(OrderID) FROM AwardOrder WHERE OrderStatus=0";
            object obj = Database.ExecuteScalar(CommandType.Text, sqlQuery);
            if(obj != null)
            {
                return Convert.ToInt32(obj);
            }
            return 0;
        }

        #endregion 商品订单

        #region 站点配置

        /// <summary>
        /// 获取站点配置
        /// </summary>
        /// <param name="configID">配置ID</param>
        /// <returns></returns>
        public ConfigInfo GetConfigInfo(int configID)
        {
            string sql = "SELECT * FROM ConfigInfo WHERE ConfigID=@ConfigID";
            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("ConfigID", configID));
            return Database.ExecuteObject<ConfigInfo>(sql, prams);
        }

        /// <summary>
        /// 获取站点配置
        /// </summary>
        /// <param name="configKey">配置Key</param>
        /// <returns></returns>
        public ConfigInfo GetConfigInfo(string configKey)
        {
            string sql = "SELECT * FROM ConfigInfo WHERE ConfigKey=@ConfigKey";
            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("ConfigKey", configKey));
            return Database.ExecuteObject<ConfigInfo>(sql, prams);
        }

        /// <summary>
        /// 更新站点配置
        /// </summary>
        /// <param name="ci"></param>
        public void UpdateConfigInfo(ConfigInfo ci)
        {
            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.Append("UPDATE ConfigInfo SET ")
                    .Append("Field1=@Field1 ,")
                    .Append("Field2=@Field2,")
                    .Append("Field3=@Field3 ,")
                    .Append("Field4=@Field4,")
                    .Append("Field5=@Field5,")
                    .Append("Field6=@Field6,")
                    .Append("Field7=@Field7,")
                    .Append("Field8=@Field8, ")
                    .Append("ConfigString=@ConfigString")
                    .Append(" WHERE ConfigID=@ConfigID");

            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("Field1", ci.Field1));
            prams.Add(Database.MakeInParam("Field2", ci.Field2));
            prams.Add(Database.MakeInParam("Field3", ci.Field3));
            prams.Add(Database.MakeInParam("Field4", ci.Field4));
            prams.Add(Database.MakeInParam("Field5", ci.Field5));
            prams.Add(Database.MakeInParam("Field6", ci.Field6));
            prams.Add(Database.MakeInParam("Field7", ci.Field7));
            prams.Add(Database.MakeInParam("Field8", ci.Field8));
            prams.Add(Database.MakeInParam("ConfigString", ci.ConfigString));
            prams.Add(Database.MakeInParam("ConfigID", ci.ConfigID));

            Database.ExecuteNonQuery(CommandType.Text, sqlQuery.ToString(), prams.ToArray());
        }

        /// <summary>
        /// 获取站点最小配置ID
        /// </summary>
        /// <returns></returns>
        public int GetConfigInfoMinID()
        {
            string sql = "SELECT ISNULL(MIN(ConfigID),0) FROM ConfigInfo";
            object o = Database.ExecuteScalar(CommandType.Text, sql);
            if(o != null)
            {
                return Convert.ToInt32(o);
            }
            return 0;
        }

        #endregion 站点配置

        #region 独立页管理

        /// <summary>
        /// 获取独立页
        /// </summary>
        /// <param name="configID">页ID</param>
        /// <returns></returns>
        public SinglePage GetSinglePage(int pageID)
        {
            string sql = "SELECT * FROM SinglePage WHERE PageID=@pageID";
            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("PageID", pageID));
            return Database.ExecuteObject<SinglePage>(sql, prams);
        }

        /// <summary>
        /// 获取独立页
        /// </summary>
        /// <param name="configKey">配置Key</param>
        /// <returns></returns>
        public SinglePage GetSinglePage(string keyValue)
        {
            string sql = "SELECT * FROM SinglePage WHERE KeyValue=@KeyValue";
            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("KeyValue", keyValue));
            return Database.ExecuteObject<SinglePage>(sql, prams);
        }

        /// <summary>
        /// 更新独立页
        /// </summary>
        /// <param name="singlePage">独立页实体</param>
        public int UpdateSinglePage(SinglePage singlePage)
        {
            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.Append("UPDATE SinglePage SET ")
                    .Append("PageName=@PageName,")
                    .Append("KeyWords=@KeyWords,")
                    .Append("Description=@Description,")
                    .Append("Contents=@Contents")
                    .Append(" WHERE PageID=@PageID");

            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("PageName", singlePage.PageName));
            prams.Add(Database.MakeInParam("KeyWords", singlePage.KeyWords));
            prams.Add(Database.MakeInParam("Description", singlePage.Description));
            prams.Add(Database.MakeInParam("Contents", singlePage.Contents));
            prams.Add(Database.MakeInParam("PageID", singlePage.PageID));

            return Database.ExecuteNonQuery(CommandType.Text, sqlQuery.ToString(), prams.ToArray());
        }

        /// <summary>
        /// 获取独立页最小ID
        /// </summary>
        /// <returns></returns>
        public int GetSinglePageMinID()
        {
            string sql = "SELECT ISNULL(MIN(PageID),0) FROM SinglePage";
            object o = Database.ExecuteScalar(CommandType.Text, sql);
            if(o != null)
            {
                return Convert.ToInt32(o);
            }
            return 0;
        }

        #endregion 独立页管理

        #region 广告管理

        /// <summary>
        /// 获取广告实体
        /// </summary>
        /// <param name="ID">广告ID</param>
        /// <returns>广告实体</returns>
        public Ads GetAds(int ID)
        {
            string sqlQuery = string.Format(" WHERE ID={0}", ID);
            Ads ads = aideAdsProvider.GetObject<Ads>(sqlQuery);
            return ads;
        }

        /// <summary>
        /// 删除广告
        /// </summary>
        /// <param name="sqlQuery">删除条件</param>
        public void DeleteAds(string sqlQuery)
        {
            aideAdsProvider.Delete(sqlQuery);
        }

        /// <summary>
        /// 添加广告
        /// </summary>
        /// <param name="ads">广告实体</param>
        public void InsertAds(Ads ads)
        {
            //设置广告ID
            StringBuilder sqlQuery = new StringBuilder("SELECT MAX(ID) FROM Ads");
            object obj = Database.ExecuteScalar(CommandType.Text, sqlQuery.ToString());
            int maxID = 0;
            if(obj != null)
            {
                maxID = Convert.ToInt32(obj);
            }
            maxID += 1;

            //添加广告
            sqlQuery = new StringBuilder();
            sqlQuery.Append("INSERT Ads(ID,Title,ResourceURL,LinkURL,Type,SortID,Remark) ")
                    .Append("VALUES(@ID,@Title,@ResourceURL,@LinkURL,@Type,@SortID,@Remark)");

            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("ID", maxID));
            prams.Add(Database.MakeInParam("Title", ads.Title));
            prams.Add(Database.MakeInParam("ResourceURL", ads.ResourceURL));
            prams.Add(Database.MakeInParam("LinkURL", ads.LinkURL));
            prams.Add(Database.MakeInParam("Type", ads.Type));
            prams.Add(Database.MakeInParam("SortID", ads.SortID));
            prams.Add(Database.MakeInParam("Remark", ads.Remark));

            Database.ExecuteNonQuery(CommandType.Text, sqlQuery.ToString(), prams.ToArray());
        }

        /// <summary>
        /// 更新广告
        /// </summary>
        /// <param name="ads">广告实体</param>
        public void UpdateAds(Ads ads)
        {
            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.Append("UPDATE Ads SET ")
                    .Append("Title=@Title ,")
                    .Append("ResourceURL=@ResourceURL,")
                    .Append("LinkUrl= @LinkUrl ,")
                    .Append("Type=@Type,")
                    .Append("SortID=@SortID,")
                    .Append("Remark=@Remark")
                    .Append(" WHERE ID= @ID");

            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("ID", ads.ID));
            prams.Add(Database.MakeInParam("Title", ads.Title));
            prams.Add(Database.MakeInParam("ResourceURL", ads.ResourceURL));
            prams.Add(Database.MakeInParam("LinkURL", ads.LinkURL));
            prams.Add(Database.MakeInParam("Type", ads.Type));
            prams.Add(Database.MakeInParam("SortID", ads.SortID));
            prams.Add(Database.MakeInParam("Remark", ads.Remark));

            Database.ExecuteNonQuery(CommandType.Text, sqlQuery.ToString(), prams.ToArray());
        }

        #endregion 广告管理

        #region 申诉管理

        /// <summary>
        /// 获取申诉实体
        /// </summary>
        /// <param name="reportID">申诉ID</param>
        /// <returns></returns>
        public LossReport GetLossReport(int reportID)
        {
            string sqlQuery = string.Format(" WHERE ReportID={0}", reportID);
            LossReport lossReport = aideLossReport.GetObject<LossReport>(sqlQuery);
            return lossReport;
        }

        /// <summary>
        /// 更新申诉实体
        /// </summary>
        /// <param name="lossReport">申诉实体</param>
        /// <returns>成功返回True，失败返回False</returns>
        public bool UpdateLossReport(LossReport lossReport)
        {
            string sqlQuery = string.Format("UPDATE LossReport SET ProcessStatus={0},SendCount=SendCount+1,SolveDate='{1}',OverDate='{2}' WHERE ReportID={3}"
              , lossReport.ProcessStatus, DateTime.Now, DateTime.Now.AddHours(24), lossReport.ReportID);
            if(Database.ExecuteNonQuery(sqlQuery) > 0)
                return true;
            return false;
        }

        #endregion 申诉管理

        #region 活动管理

        /// <summary>
        /// 删除活动
        /// </summary>
        /// <param name="idList">删除活动ID列表</param>
        public void DeleteActivity(string idList)
        {
            string sqlQuery = string.Format("DELETE Activity WHERE ActivityID IN({0})", idList);
            Database.ExecuteNonQuery(sqlQuery);
        }

        /// <summary>
        /// 获取活动实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Activity GetActivity(int id)
        {
            string sqlQuery = "SELECT * FROM Activity WHERE ActivityID=@ActivityID";
            List<DbParameter> param = new List<DbParameter>();
            param.Add(Database.MakeInParam("ActivityID", id));
            return Database.ExecuteObject<Activity>(sqlQuery, param);
        }

        /// <summary>
        /// 添加活动
        /// </summary>
        /// <param name="model"></param>
        public void AddActivity(Activity model)
        {
            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.Append("INSERT INTO Activity(Title,ImageUrl,Describe,SortID,IsRecommend) ");
            sqlQuery.Append("VALUES(@Title,@ImageUrl,@Describe,@SortID,@IsRecommend)");
            List<DbParameter> param = new List<DbParameter>();
            param.Add(Database.MakeInParam("Title", model.Title));
            param.Add(Database.MakeInParam("ImageUrl", model.ImageUrl));
            param.Add(Database.MakeInParam("Describe", model.Describe));
            param.Add(Database.MakeInParam("SortID", model.SortID));
            param.Add(Database.MakeInParam("IsRecommend", model.IsRecommend));
            Database.ExecuteNonQuery(CommandType.Text, sqlQuery.ToString(), param.ToArray());
        }

        /// <summary>
        /// 更新活动
        /// </summary>
        /// <param name="model"></param>
        public void UpdateActivity(Activity model)
        {
            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.Append("UPDATE Activity SET Title=@Title,ImageUrl=@ImageUrl,Describe=@Describe,SortID=@SortID,IsRecommend=@IsRecommend ");
            sqlQuery.Append("WHERE ActivityID=@ActivityID");
            List<DbParameter> param = new List<DbParameter>();
            param.Add(Database.MakeInParam("Title", model.Title));
            param.Add(Database.MakeInParam("ImageUrl", model.ImageUrl));
            param.Add(Database.MakeInParam("Describe", model.Describe));
            param.Add(Database.MakeInParam("SortID", model.SortID));
            param.Add(Database.MakeInParam("ActivityID", model.ActivityID));
            param.Add(Database.MakeInParam("IsRecommend", model.IsRecommend));
            Database.ExecuteNonQuery(CommandType.Text, sqlQuery.ToString(), param.ToArray());
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
        public PagerSet GetList(string tableName, int pageIndex, int pageSize, string condition, string orderby)
        {
            PagerParameters pagerPrams = new PagerParameters(tableName, orderby, condition, pageIndex, pageSize);
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

        /// <summary>
        ///  执行SQL语句返回DataSet
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public DataSet GetDataSetBySql(string sql)
        {
            DataSet ds = Database.ExecuteDataset(sql);
            return ds;
        }

        /// <summary>
        /// 执行SQL语句返回一个值
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public string GetScalarBySql(string sql)
        {
            return Database.ExecuteScalarToStr(CommandType.Text, sql);
        }

        #endregion 公共
    }
}