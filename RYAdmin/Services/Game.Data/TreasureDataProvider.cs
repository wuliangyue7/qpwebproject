using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

using Game.Kernel;
using Game.IData;
using Game.Entity.Treasure;

namespace Game.Data
{
    /// <summary>
    /// 金币库数据层
    /// </summary>
    public class TreasureDataProvider : BaseDataProvider, ITreasureDataProvider
    {
        #region Fields

        private ITableProvider aideShareDetialProvider;
        private ITableProvider aideGlobalShareProvider;
        private ITableProvider aideOnLineOrderProvider;
        private ITableProvider aideDayDetailProvider;
        private ITableProvider aideKQDetailProvider;
        private ITableProvider aideYPDetailProvider;

        private ITableProvider aideGameScoreInfoProvider;
        private ITableProvider aideRecordDrawInfoProvider;
        private ITableProvider aideRecordDrawScoreProvider;
        private ITableProvider aideGameScoreLockerProvider;

        private ITableProvider aideAndroidProvider;
        private ITableProvider aideGlobalLivcardProvider;
        private ITableProvider aideLivcardAssociatorProvider;
        private ITableProvider aideLivcardBuildStreamProvider;
        private ITableProvider aideGlobalSpreadInfoProvider;
        private ITableProvider aideGameScoreLocker;
        private ITableProvider aideVBDetailProvider;
        private ITableProvider aideAppDetailProvider;
        private ITableProvider aideGlobalAppProvider;
        private ITableProvider aideRecordExchCurrency;
        private ITableProvider aideLotteryConfigProvider;
        #endregion

        #region 构造方法

        public TreasureDataProvider(string connString)
            : base(connString)
        {
            aideShareDetialProvider = GetTableProvider(ShareDetailInfo.Tablename);
            aideGlobalShareProvider = GetTableProvider(GlobalShareInfo.Tablename);
            aideOnLineOrderProvider = GetTableProvider(OnLineOrder.Tablename);
            aideDayDetailProvider = GetTableProvider(ReturnDayDetailInfo.Tablename);
            aideKQDetailProvider = GetTableProvider(ReturnKQDetailInfo.Tablename);
            aideYPDetailProvider = GetTableProvider(ReturnYPDetailInfo.Tablename);
            aideVBDetailProvider = GetTableProvider(ReturnVBDetailInfo.Tablename);
            aideAppDetailProvider = GetTableProvider(ReturnAppDetailInfo.Tablename);
            aideGlobalAppProvider = GetTableProvider(GlobalAppInfo.Tablename);

            aideGameScoreInfoProvider = GetTableProvider(GameScoreInfo.Tablename);
            aideRecordDrawInfoProvider = GetTableProvider(RecordDrawInfo.Tablename);
            aideRecordDrawScoreProvider = GetTableProvider(RecordDrawScore.Tablename);
            aideGameScoreLockerProvider = GetTableProvider(GameScoreLocker.Tablename);

            aideAndroidProvider = GetTableProvider(AndroidManager.Tablename);
            aideGlobalLivcardProvider = GetTableProvider(GlobalLivcard.Tablename);
            aideLivcardAssociatorProvider = GetTableProvider(LivcardAssociator.Tablename);
            aideLivcardBuildStreamProvider = GetTableProvider(LivcardBuildStream.Tablename);

            aideGlobalSpreadInfoProvider = GetTableProvider(GlobalSpreadInfo.Tablename);
            aideGameScoreLocker = GetTableProvider(GameScoreLocker.Tablename);
            aideRecordExchCurrency = GetTableProvider(RecordExchCurrency.Tablename);
            aideLotteryConfigProvider = GetTableProvider(LotteryConfig.Tablename);
        }
        #endregion

        #region 充值相关

        #region 实卡类型
        /// <summary>
        /// 获取实卡类型记录列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="condition"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public PagerSet GetGlobalLivcardList(int pageIndex, int pageSize, string condition, string orderby)
        {
            PagerParameters pagerPrams = new PagerParameters(GlobalLivcard.Tablename, orderby, condition, pageIndex, pageSize);

            return GetPagerSet2(pagerPrams);
        }
        /// <summary>
        /// 获取实卡类型实体
        /// </summary>
        /// <param name="cardTypeID"></param>
        /// <returns></returns>
        public GlobalLivcard GetGlobalLivcardInfo(int cardTypeID)
        {
            string sqlQuery = string.Format("(NOLOCK) WHERE CardTypeID= '{0}'", cardTypeID);
            GlobalLivcard globalLivcard = aideGlobalLivcardProvider.GetObject<GlobalLivcard>(sqlQuery);
            return globalLivcard;
        }
        /// <summary>
        /// 新增实卡类型
        /// </summary>
        /// <param name="globalLivcard"></param>
        public void InsertGlobalLivcard(GlobalLivcard globalLivcard)
        {
            DataRow dr = aideGlobalLivcardProvider.NewRow();

            dr[GlobalLivcard._CardName] = globalLivcard.CardName;
            dr[GlobalLivcard._CardPrice] = globalLivcard.CardPrice;
            dr[GlobalLivcard._Currency] = globalLivcard.Currency;
            dr[GlobalLivcard._InputDate] = DateTime.Now;
            aideGlobalLivcardProvider.Insert(dr);
        }
        /// <summary>
        /// 更新实卡类型
        /// </summary>
        /// <param name="globalLivcard"></param>
        public void UpdateGlobalLivcard(GlobalLivcard globalLivcard)
        {
            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.Append("UPDATE GlobalLivcard SET ")
                    .Append("CardName=@CardName, ")
                    .Append("CardPrice=@CardPrice, ")
                    .Append("Currency=@Currency ")
                    .Append("WHERE CardTypeID=@CardTypeID");

            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("CardName", globalLivcard.CardName));
            prams.Add(Database.MakeInParam("CardPrice", globalLivcard.CardPrice));
            prams.Add(Database.MakeInParam("Currency", globalLivcard.Currency));
            prams.Add(Database.MakeInParam("CardTypeID", globalLivcard.CardTypeID));

            Database.ExecuteNonQuery(CommandType.Text, sqlQuery.ToString(), prams.ToArray());
        }
        /// <summary>
        /// 删除实卡类型
        /// </summary>
        /// <param name="sqlQuery"></param>
        public void DeleteGlobalLivcard(string sqlQuery)
        {
            aideGlobalLivcardProvider.Delete(sqlQuery);
        }
        #endregion

        #region 实卡批次
        /// <summary>
        /// 获取实卡批次记录
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="condition"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public PagerSet GetLivcardBuildStreamList(int pageIndex, int pageSize, string condition, string orderby)
        {
            PagerParameters pagerPrams = new PagerParameters(LivcardBuildStream.Tablename, orderby, condition, pageIndex, pageSize);

            return GetPagerSet2(pagerPrams);
        }
        /// <summary>
        /// 获取实卡批次实体
        /// </summary>
        /// <param name="buildID"></param>
        /// <returns></returns>
        public LivcardBuildStream GetLivcardBuildStreamInfo(int buildID)
        {
            string sqlQuery = string.Format("(NOLOCK) WHERE BuildID= '{0}'", buildID);
            LivcardBuildStream livcardBuildStream = aideLivcardBuildStreamProvider.GetObject<LivcardBuildStream>(sqlQuery);
            return livcardBuildStream;
        }
        /// <summary>
        /// 插入实卡批次记录
        /// </summary>
        /// <param name="livcardBuildStream"></param>
        /// <returns></returns>
        public int InsertLivcardBuildStream(LivcardBuildStream livcardBuildStream)
        {
            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("AdminName", livcardBuildStream.AdminName));
            prams.Add(Database.MakeInParam("CardTypeID", livcardBuildStream.CardTypeID));
            prams.Add(Database.MakeInParam("CardPrice", livcardBuildStream.CardPrice));
            prams.Add(Database.MakeInParam("Currency", livcardBuildStream.Currency));
            prams.Add(Database.MakeInParam("BuildCount", livcardBuildStream.BuildCount));
            prams.Add(Database.MakeInParam("BuildAddr", livcardBuildStream.BuildAddr));
            prams.Add(Database.MakeInParam("NoteInfo", livcardBuildStream.NoteInfo));
            prams.Add(Database.MakeInParam("BuildCardPacket", livcardBuildStream.BuildCardPacket));

            object obj;
            Database.RunProc("WSP_PM_BuildStreamAdd", prams, out obj);
            if(obj == null || obj.ToString().Length <= 0)
                return 0;
            return int.Parse(obj.ToString());
        }

        /// <summary>
        /// 更新实卡批次记录
        /// </summary>
        /// <param name="livcardBuildStream"></param>
        public void UpdateLivcardBuildStream(LivcardBuildStream livcardBuildStream)
        {
            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.Append("UPDATE LivcardBuildStream SET ")
                    .Append("BuildCardPacket=@BuildCardPacket ")
                    .Append("WHERE BuildID=@BuildID");

            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("BuildCardPacket", livcardBuildStream.BuildCardPacket));
            prams.Add(Database.MakeInParam("BuildID", livcardBuildStream.BuildID));

            Database.ExecuteNonQuery(CommandType.Text, sqlQuery.ToString(), prams.ToArray());
        }
        /// <summary>
        /// 更新实卡批次导出次数
        /// </summary>
        /// <param name="buildID"></param>
        public void UpdateLivcardBuildStream(int buildID)
        {
            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.Append("UPDATE LivcardBuildStream SET ")
                    .Append("DownLoadCount=DownLoadCount+1 ")
                    .Append("WHERE BuildID=@BuildID");

            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("BuildID", buildID));

            Database.ExecuteNonQuery(CommandType.Text, sqlQuery.ToString(), prams.ToArray());
        }
        #endregion

        #region 实卡信息
        /// <summary>
        /// 获取实卡记录
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="condition"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public PagerSet GetLivcardAssociatorList(int pageIndex, int pageSize, string condition, string orderby)
        {
            PagerParameters pagerPrams = new PagerParameters(LivcardAssociator.Tablename, orderby, condition, pageIndex, pageSize);

            return GetPagerSet2(pagerPrams);
        }
        /// <summary>
        /// 获取实卡实体,根据CardID
        /// </summary>
        /// <param name="cardID"></param>
        /// <returns></returns>
        public LivcardAssociator GetLivcardAssociatorInfo(int cardID)
        {
            string sqlQuery = string.Format("(NOLOCK) WHERE CardID= '{0}'", cardID);
            LivcardAssociator livcardAssociator = aideLivcardAssociatorProvider.GetObject<LivcardAssociator>(sqlQuery);
            return livcardAssociator;
        }
        /// <summary>
        /// 获取实卡实体,根据SerialID
        /// </summary>
        /// <param name="serialID"></param>
        /// <returns></returns>
        public LivcardAssociator GetLivcardAssociatorInfo(string serialID)
        {
            string sqlQuery = string.Format("(NOLOCK) WHERE SerialID= '{0}'", serialID);
            LivcardAssociator livcardAssociator = aideLivcardAssociatorProvider.GetObject<LivcardAssociator>(sqlQuery);
            return livcardAssociator;
        }
        /// <summary>
        /// 获取实卡充值记录,根据SerialID
        /// </summary>
        /// <param name="serialID"></param>
        /// <returns></returns>
        public ShareDetailInfo GetShareDetailInfo(string serialID)
        {
            string sqlQuery = string.Format("(NOLOCK) WHERE SerialID= '{0}'", serialID);
            ShareDetailInfo shareDetailInfo = aideShareDetialProvider.GetObject<ShareDetailInfo>(sqlQuery);
            return shareDetailInfo;
        }
        /// <summary>
        /// 获取实卡的销售商名称，根据批号
        /// </summary>
        /// <param name="buildID"></param>
        /// <returns></returns>
        public string GetSalesperson(int buildID)
        {
            string sqlQuery = string.Format("SELECT TOP 1 Salesperson FROM LivcardAssociator(NOLOCK) WHERE BuildID= '{0}'", buildID);
            return Database.ExecuteScalarToStr(CommandType.Text, sqlQuery);
        }
        /// <summary>
        /// 禁用实卡
        /// </summary>
        /// <param name="sqlQuery"></param>
        public void SetCardDisbale(string sqlQuery)
        {
            string sqlQueryAll = string.Format("UPDATE LivcardAssociator SET Nullity=1 {0}", sqlQuery);
            Database.ExecuteNonQuery(sqlQueryAll);
        }

        /// <summary>
        /// 启用实卡
        /// </summary>
        /// <param name="sqlQuery"></param>
        public void SetCardEnbale(string sqlQuery)
        {
            string sqlQueryAll = string.Format("UPDATE LivcardAssociator SET Nullity=0 {0}", sqlQuery);
            Database.ExecuteNonQuery(sqlQueryAll);
        }

        /// <summary>
        /// 插入实卡记录
        /// </summary>
        /// <param name="livcardAssociator"></param>
        /// <returns></returns>
        public void InsertLivcardAssociator(LivcardAssociator livcardAssociator, string[,] list)
        {
            //for( int i = 0; i < list.GetLength( 0 ); i++ )
            //{
            //    var prams = new List<DbParameter>();
            //    prams.Add( Database.MakeInParam( "SerialID", list[i, 0] ) );
            //    prams.Add( Database.MakeInParam( "Password", list[i, 1] ) );
            //    prams.Add( Database.MakeInParam( "BuildID", livcardAssociator.BuildID ) );
            //    prams.Add( Database.MakeInParam( "CardTypeID", livcardAssociator.CardTypeID ) );
            //    prams.Add( Database.MakeInParam( "CardPrice", livcardAssociator.CardPrice ) );
            //    prams.Add( Database.MakeInParam( "Currency", livcardAssociator.Currency ) );

            //    prams.Add( Database.MakeInParam( "UseRange", livcardAssociator.UseRange ) );
            //    prams.Add( Database.MakeInParam( "SalesPerson", livcardAssociator.SalesPerson ) );
            //    prams.Add( Database.MakeInParam( "ValidDate", livcardAssociator.ValidDate ) );

            //    Database.RunProc( "WSP_PM_LivcardAdd", prams );
            //}

            string serialIdList = string.Empty;
            string passwordList = string.Empty;

            int count = list.GetLength(0);
            for(int i = 0; i < count; i = i + 100)
            {
                var prams = new List<DbParameter>();
                serialIdList = string.Empty;
                passwordList = string.Empty;
                for(int j = i; j < i + 100; j++)
                {
                    if(j >= count)
                    {
                        break;
                    }
                    serialIdList += string.Format("{0},", list[j, 0]);
                    passwordList += string.Format("{0},", list[j, 1]);
                }
                if(!string.IsNullOrEmpty(serialIdList) && !string.IsNullOrEmpty(passwordList))
                {
                    serialIdList = serialIdList.TrimEnd(new char[] { ',' });
                    passwordList = passwordList.TrimEnd(new char[] { ',' });

                    prams.Add(Database.MakeInParam("SerialID", serialIdList));
                    prams.Add(Database.MakeInParam("Password", passwordList));
                    prams.Add(Database.MakeInParam("BuildID", livcardAssociator.BuildID));
                    prams.Add(Database.MakeInParam("CardTypeID", livcardAssociator.CardTypeID));
                    prams.Add(Database.MakeInParam("CardPrice", livcardAssociator.CardPrice));
                    prams.Add(Database.MakeInParam("Currency", livcardAssociator.Currency));

                    prams.Add(Database.MakeInParam("UseRange", livcardAssociator.UseRange));
                    prams.Add(Database.MakeInParam("SalesPerson", livcardAssociator.SalesPerson));
                    prams.Add(Database.MakeInParam("ValidDate", livcardAssociator.ValidDate));

                    Database.RunProc("WSP_PM_LivcardAdd", prams);
                }
            }
        }

        /// <summary>
        /// 实卡库存统计
        /// </summary>
        /// <returns></returns>
        public DataSet GetLivcardStat()
        {
            DataSet ds;
            Database.RunProc("WSP_PM_LivcardStat", out ds);
            return ds;
        }

        /// <summary>
        /// 实卡库存统计
        /// </summary>
        /// <returns></returns>
        public DataSet GetLivcardStatByBuildID(int buildID)
        {
            DataSet ds;
            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("BuildID", buildID));
            Database.RunProc("NET_PM_LivcardStatByBuildID", prams, out ds);
            return ds;
        }
        #endregion

        /// <summary>
        /// 获取订单记录
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="condition"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public PagerSet GetOnLineOrderList(int pageIndex, int pageSize, string condition, string orderby)
        {
            PagerParameters pagerPrams = new PagerParameters(OnLineOrder.Tablename, orderby, condition, pageIndex, pageSize);

            return GetPagerSet2(pagerPrams);
        }

        /// <summary>
        /// 获取订单信息实体
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public OnLineOrder GetOnLineOrderInfo(string orderID)
        {
            string sqlQuery = string.Format("(NOLOCK) WHERE OrderID= '{0}'", orderID);
            OnLineOrder onLineOrder = aideOnLineOrderProvider.GetObject<OnLineOrder>(sqlQuery);
            return onLineOrder;
        }

        /// <summary>
        /// 删除订单
        /// </summary>
        /// <param name="sqlQuery"></param>
        public void DeleteOnlineOrder(string sqlQuery)
        {
            aideOnLineOrderProvider.Delete(sqlQuery);
        }

        /// <summary>
        /// 获取快钱返回记录
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="condition"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public PagerSet GetKQDetailList(int pageIndex, int pageSize, string condition, string orderby)
        {
            PagerParameters pagerPrams = new PagerParameters(ReturnKQDetailInfo.Tablename, orderby, condition, pageIndex, pageSize);

            return GetPagerSet2(pagerPrams);
        }

        /// <summary>
        /// 获取快钱返回记录实体
        /// </summary>
        /// <param name="detailID"></param>
        /// <returns></returns>
        public ReturnKQDetailInfo GetKQDetailInfo(int detailID)
        {
            string sqlQuery = string.Format("(NOLOCK) WHERE DetailID= {0}", detailID);
            ReturnKQDetailInfo detailInfo = aideKQDetailProvider.GetObject<ReturnKQDetailInfo>(sqlQuery);
            return detailInfo;
        }

        /// <summary>
        /// 删除快钱返回记录
        /// </summary>
        /// <param name="sqlQuery"></param>
        public void DeleteKQDetail(string sqlQuery)
        {
            aideKQDetailProvider.Delete(sqlQuery);
        }
        /// <summary>
        /// 获取易宝返回记录
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="condition"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public PagerSet GetYPDetailList(int pageIndex, int pageSize, string condition, string orderby)
        {
            PagerParameters pagerPrams = new PagerParameters(ReturnYPDetailInfo.Tablename, orderby, condition, pageIndex, pageSize);

            return GetPagerSet2(pagerPrams);
        }

        /// <summary>
        /// 获取易宝返回记录实体
        /// </summary>
        /// <param name="detailID"></param>
        /// <returns></returns>
        public ReturnYPDetailInfo GetYPDetailInfo(int detailID)
        {
            string sqlQuery = string.Format("(NOLOCK) WHERE DetailID= {0}", detailID);
            ReturnYPDetailInfo detailInfo = aideYPDetailProvider.GetObject<ReturnYPDetailInfo>(sqlQuery);
            return detailInfo;
        }

        /// <summary>
        /// 删除易宝返回记录
        /// </summary>
        /// <param name="sqlQuery"></param>
        public void DeleteYPDetail(string sqlQuery)
        {
            aideYPDetailProvider.Delete(sqlQuery);
        }

        /// <summary>
        /// 获取VB返回记录
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="condition"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public PagerSet GetVBDetailList(int pageIndex, int pageSize, string condition, string orderby)
        {
            PagerParameters pagerPrams = new PagerParameters(ReturnVBDetailInfo.Tablename, orderby, condition, pageIndex, pageSize);
            return GetPagerSet2(pagerPrams);
        }

        /// <summary>
        /// 获取VB返回记录实体
        /// </summary>
        /// <param name="detailID"></param>
        /// <returns></returns>
        public ReturnVBDetailInfo GetVBDetailInfo(int detailID)
        {
            string sqlQuery = string.Format("(NOLOCK) WHERE DetailID= {0}", detailID);
            ReturnVBDetailInfo detailInfo = aideVBDetailProvider.GetObject<ReturnVBDetailInfo>(sqlQuery);
            return detailInfo;
        }

        /// <summary>
        /// 删除VB返回记录
        /// </summary>
        /// <param name="sqlQuery"></param>
        public void DeleteVBDetail(string sqlQuery)
        {
            aideVBDetailProvider.Delete(sqlQuery);
        }

        /// <summary>
        /// 获取天天付返回记录
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="condition"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public PagerSet GetDayDetailList(int pageIndex, int pageSize, string condition, string orderby)
        {
            PagerParameters pagerPrams = new PagerParameters(ReturnDayDetailInfo.Tablename, orderby, condition, pageIndex, pageSize);
            return GetPagerSet2(pagerPrams);
        }

        /// <summary>
        /// 获取天天付返回记录实体
        /// </summary>
        /// <param name="detailID"></param>
        /// <returns></returns>
        public ReturnDayDetailInfo GetDayDetailInfo(int detailID)
        {
            ReturnDayDetailInfo info = new ReturnDayDetailInfo();
            return info;
        }

        /// <summary>
        /// 删除天天付返回记录
        /// </summary>
        /// <param name="sqlQuery"></param>
        public void DeleteDayDetail(string sqlQuery)
        {
            aideDayDetailProvider.Delete(sqlQuery);
        }

        /// <summary>
        /// 获取充值记录
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="condition"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public PagerSet GetShareDetailList(int pageIndex, int pageSize, string condition, string orderby)
        {
            PagerParameters pagerPrams = new PagerParameters(ShareDetailInfo.Tablename, orderby, condition, pageIndex, pageSize);

            return GetPagerSet2(pagerPrams);
        }

        /// <summary>
        /// 获取充值服务类型
        /// </summary>
        /// <param name="shareID"></param>
        /// <returns></returns>
        public GlobalShareInfo GetGlobalShareByShareID(int shareID)
        {
            string sqlQuery = string.Format("(NOLOCK) WHERE ShareID= N'{0}'", shareID);
            GlobalShareInfo globalShare = aideGlobalShareProvider.GetObject<GlobalShareInfo>(sqlQuery);
            return globalShare;
        }

        /// <summary>
        /// 获取充值服务列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="condition"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public PagerSet GetGlobalShareList(int pageIndex, int pageSize, string condition, string orderby)
        {
            PagerParameters pagerPrams = new PagerParameters(GlobalShareInfo.Tablename, orderby, condition, pageIndex, pageSize);

            return GetPagerSet2(pagerPrams);
        }

        /// <summary>
        /// 获取苹果返回记录
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="condition"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public PagerSet GetAppDetailList(int pageIndex, int pageSize, string condition, string orderby)
        {
            PagerParameters pagerPrams = new PagerParameters(ReturnAppDetailInfo.Tablename, orderby, condition, pageIndex, pageSize);
            return GetPagerSet2(pagerPrams);
        }

        /// <summary>
        /// 获取苹果返回记录实体
        /// </summary>
        /// <param name="detailID"></param>
        /// <returns></returns>
        public ReturnAppDetailInfo GetAppDetailInfo(int detailID)
        {
            string sqlQuery = string.Format("(NOLOCK) WHERE DetailID= {0}", detailID);
            ReturnAppDetailInfo detailInfo = aideAppDetailProvider.GetObject<ReturnAppDetailInfo>(sqlQuery);
            return detailInfo;
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
        public PagerSet GetGlobalAppInfoList(int pageIndex, int pageSize, string condition, string orderby)
        {
            PagerParameters pagerPrams = new PagerParameters(GlobalAppInfo.Tablename, orderby, condition, pageIndex, pageSize);

            return GetPagerSet2(pagerPrams);
        }
        /// <summary>
        /// 获取产品实体
        /// </summary>
        /// <param name="appID"></param>
        /// <returns></returns>
        public GlobalAppInfo GetGlobalAppInfo(int appID)
        {
            string sqlQuery = string.Format("(NOLOCK) WHERE AppID= '{0}'", appID);
            GlobalAppInfo globalApp = aideGlobalAppProvider.GetObject<GlobalAppInfo>(sqlQuery);
            return globalApp;
        }
        /// <summary>
        /// 新增产品
        /// </summary>
        /// <param name="globalLivcard"></param>
        public void InsertGlobalAppInfo(GlobalAppInfo globalApp)
        {
            DataRow dr = aideGlobalAppProvider.NewRow();

            dr[GlobalAppInfo._ProductID] = globalApp.ProductID;
            dr[GlobalAppInfo._ProductName] = globalApp.ProductName;
            dr[GlobalAppInfo._Description] = globalApp.Description;
            dr[GlobalAppInfo._Price] = globalApp.Price;
            dr[GlobalAppInfo._AttachCurrency] = globalApp.AttachCurrency;
            dr[GlobalAppInfo._TagID] = globalApp.TagID;
            dr[GlobalAppInfo._CollectDate] = globalApp.CollectDate;

            aideGlobalAppProvider.Insert(dr);
        }
        /// <summary>
        /// 更新产品
        /// </summary>
        /// <param name="globalLivcard"></param>
        public void UpdateGlobalAppInfo(GlobalAppInfo globalApp)
        {
            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.Append("UPDATE GlobalAppInfo SET ")
                    .Append("ProductID=@ProductID, ")
                    .Append("ProductName=@ProductName, ")
                    .Append("Description=@Description, ")
                    .Append("Price=@Price, ")
                    .Append("AttachCurrency=@AttachCurrency, ")
                    .Append("TagID=@TagID ")
                    .Append("WHERE AppID=@AppID");

            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("ProductID", globalApp.ProductID));
            prams.Add(Database.MakeInParam("ProductName", globalApp.ProductName));
            prams.Add(Database.MakeInParam("Description", globalApp.Description));
            prams.Add(Database.MakeInParam("Price", globalApp.Price));
            prams.Add(Database.MakeInParam("AttachCurrency", globalApp.AttachCurrency));
            prams.Add(Database.MakeInParam("TagID", globalApp.TagID));
            prams.Add(Database.MakeInParam("AppID", globalApp.AppID));

            Database.ExecuteNonQuery(CommandType.Text, sqlQuery.ToString(), prams.ToArray());
        }
        /// <summary>
        /// 删除产品
        /// </summary>
        /// <param name="sqlQuery"></param>
        public void DeleteGlobalAppInfo(string sqlQuery)
        {
            aideGlobalAppProvider.Delete(sqlQuery);
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
        public PagerSet GetGameScoreInfoList(int pageIndex, int pageSize, string condition, string orderby)
        {
            PagerParameters pagerPrams = new PagerParameters(GameScoreInfo.Tablename, orderby, condition, pageIndex, pageSize);
            return GetPagerSet2(pagerPrams);
        }
        /// <summary>
        /// 获取用户金币信息
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public GameScoreInfo GetGameScoreInfoByUserID(int UserID)
        {
            string sqlQuery = string.Format("(NOLOCK) WHERE UserID= N'{0}'", UserID);
            GameScoreInfo gameScoreInfo = aideGameScoreInfoProvider.GetObject<GameScoreInfo>(sqlQuery);
            return gameScoreInfo;
        }
        /// <summary>
        /// 获取用户的银行金币
        /// </summary>
        /// <param name="UserID">用户标识 </param>
        /// <returns></returns>
        public decimal GetGameScoreByUserID(int UserID)
        {
            GameScoreInfo gameScoreInfo = GetGameScoreInfoByUserID(UserID);
            if(gameScoreInfo == null)
                return 0;
            else
                return gameScoreInfo.InsureScore;
        }
        /// <summary>
        /// 更新用户银行金币
        /// </summary>
        /// <param name="gameScoreInfo"></param>
        public void UpdateInsureScore(GameScoreInfo gameScoreInfo)
        {
            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.Append("UPDATE GameScoreInfo SET ")
                    .Append("InsureScore=@InsureScore ")
                    .Append("WHERE UserID=@UserID");

            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("InsureScore", gameScoreInfo.InsureScore));
            prams.Add(Database.MakeInParam("UserID", gameScoreInfo.UserID));

            Database.ExecuteNonQuery(CommandType.Text, sqlQuery.ToString(), prams.ToArray());
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
        public Message GrantTreasure(string strUserIdList, int intGold, int intMasterID, string strReason, string strIP)
        {
            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("strUserIDList", strUserIdList));
            prams.Add(Database.MakeInParam("dwAddGold", intGold));
            prams.Add(Database.MakeInParam("dwMasterID", intMasterID));
            prams.Add(Database.MakeInParam("strReason", strReason));
            prams.Add(Database.MakeInParam("strClientIP", strIP));

            Message msg = MessageHelper.GetMessage(Database, "NET_PM_GrantTreasure", prams);
            return msg;
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
        public Message GrantScore(int userID, int kindID, int score, int masterID, string strReason, string strIP)
        {
            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("UserID", userID));
            prams.Add(Database.MakeInParam("KindID", kindID));
            prams.Add(Database.MakeInParam("AddScore", score));
            prams.Add(Database.MakeInParam("MasterID", masterID));
            prams.Add(Database.MakeInParam("Reason", strReason));
            prams.Add(Database.MakeInParam("ClientIP", strIP));

            Message msg = MessageHelper.GetMessage(Database, "WSP_PM_GrantGameScore", prams);
            return msg;
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
        public Message GrantClearScore(int userID, int kindID, int masterID, string strReason, string strIP)
        {
            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("UserID", userID));
            prams.Add(Database.MakeInParam("KindID", kindID));
            prams.Add(Database.MakeInParam("MasterID", masterID));
            prams.Add(Database.MakeInParam("Reason", strReason));
            prams.Add(Database.MakeInParam("ClientIP", strIP));

            Message msg = MessageHelper.GetMessage(Database, "WSP_PM_GrantClearScore", prams);
            return msg;
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
        public Message GrantFlee(int userID, int kindID, int masterID, string strReason, string strIP)
        {
            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("UserID", userID));
            prams.Add(Database.MakeInParam("KindID", kindID));
            prams.Add(Database.MakeInParam("MasterID", masterID));
            prams.Add(Database.MakeInParam("Reason", strReason));
            prams.Add(Database.MakeInParam("ClientIP", strIP));

            Message msg = MessageHelper.GetMessage(Database, "WSP_PM_GrantClearFlee", prams);
            return msg;
        }
        #endregion

        #region 用户货币信息

        /// <summary>
        /// 获取用户货币实体信息
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public UserCurrencyInfo GetUserCurrencyInfo(int userID)
        {
            string sql = "SELECT * FROM UserCurrencyInfo WHERE UserID=@UserID";
            List<DbParameter> parms = new List<DbParameter>();
            parms.Add(Database.MakeInParam("UserID", userID));
            return Database.ExecuteObject<UserCurrencyInfo>(sql, parms);
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
        public PagerSet GetRecordDrawInfoList(int pageIndex, int pageSize, string condition, string orderby)
        {
            PagerParameters pagerPrams = new PagerParameters(RecordDrawInfo.Tablename, orderby, condition, pageIndex, pageSize);
            return GetPagerSet2(pagerPrams);
        }

        /// <summary>
        /// 删除小于等于该日期的游戏总局记录
        /// </summary>
        /// <param name="dt">删除截止日期</param>
        /// <returns></returns>
        public int DeleteRecordDrawInfoByTime(DateTime dt)
        {
            string sqlQuery = "DELETE RecordDrawInfo WHERE InsertTime<@Date";

            var param = new List<DbParameter>();
            param.Add(Database.MakeInParam("Date", dt));

            return Database.ExecuteNonQuery(CommandType.Text, sqlQuery, param.ToArray());
        }

        /// <summary>
        /// 获取玩家游戏记录
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="condition">条件</param>
        /// <param name="orderby">排序</param>
        /// <returns></returns>
        public PagerSet GetRecordDrawScoreList(int pageIndex, int pageSize, string condition, string orderby)
        {
            PagerParameters pagerPrams = new PagerParameters(RecordDrawScore.Tablename, orderby, condition, pageIndex, pageSize);
            return GetPagerSet2(pagerPrams);
        }

        /// <summary>
        /// 根据局ID获取游戏记录
        /// </summary>
        /// <param name="drawID">局ID</param>
        /// <returns>数据集</returns>
        public DataSet GetRecordDrawScoreByDrawID(int drawID)
        {
            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("dwDrawID", drawID));

            return Database.ExecuteDataset(CommandType.StoredProcedure, "WSP_PM_GetRecordDrawScoreByDrawID", prams.ToArray());
        }

        /// <summary>
        /// 删除小于等于该日期的游戏详情记录
        /// </summary>
        /// <param name="dt">删除截止日期</param>
        /// <returns></returns>
        public int DeleteRecordDrawScoreByTime(DateTime dt)
        {
            string sqlQuery = "DELETE RecordDrawScore WHERE InsertTime<@Date";

            var param = new List<DbParameter>();
            param.Add(Database.MakeInParam("Date", dt));

            return Database.ExecuteNonQuery(CommandType.Text, sqlQuery, param.ToArray());
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
        public PagerSet GetGameScoreLockerList(int pageIndex, int pageSize, string condition, string orderby)
        {
            PagerParameters pagerPrams = new PagerParameters(GameScoreLocker.Tablename, orderby, condition, pageIndex, pageSize);
            return GetPagerSet2(pagerPrams);
        }
        /// <summary>
        /// 删除卡线记录
        /// </summary>
        /// <param name="userID"></param>
        public void DeleteGameScoreLockerByUserID(int userID)
        {
            string sqlQuery = string.Format("WHERE UserID='{0}'", userID);
            aideGameScoreLockerProvider.Delete(sqlQuery);
        }

        /// <summary>
        /// 删除卡线记录
        /// </summary>
        /// <param name="sqlQuery"></param>
        public void DeleteGameScoreLocker(string sqlQuery)
        {
            aideGameScoreLockerProvider.Delete(sqlQuery);
        }

        /// <summary>
        /// 获取用户卡线信息
        /// </summary>
        /// <param name="userID">用户UserID</param>
        /// <returns></returns>
        public GameScoreLocker GetGameScoreLockerByUserID(int userID)
        {
            string sqlQuery = string.Format("(NOLOCK) WHERE UserID= N'{0}'", userID);
            GameScoreLocker model = aideGameScoreLocker.GetObject<GameScoreLocker>(sqlQuery);
            return model;
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
        public PagerSet GetAndroidList(int pageIndex, int pageSize, string condition, string orderby)
        {
            PagerParameters pagerPrams = new PagerParameters(AndroidManager.Tablename, orderby, condition, pageIndex, pageSize);

            return GetPagerSet2(pagerPrams);
        }

        /// <summary>
        /// 获取机器人实体
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public AndroidManager GetAndroidInfo(int userID)
        {
            string sqlQuery = string.Format("(NOLOCK) WHERE UserID= {0}", userID);
            AndroidManager android = aideAndroidProvider.GetObject<AndroidManager>(sqlQuery);
            return android;
        }

        /// <summary>
        /// 新增机器人
        /// </summary>
        /// <param name="android"></param>
        public void InsertAndroid(AndroidManager android)
        {
            DataRow dr = aideAndroidProvider.NewRow();

            dr[AndroidManager._UserID] = android.UserID;
            dr[AndroidManager._ServerID] = android.ServerID;
            dr[AndroidManager._MinPlayDraw] = android.MinPlayDraw;
            dr[AndroidManager._MaxPlayDraw] = android.MaxPlayDraw;
            dr[AndroidManager._MinTakeScore] = android.MinTakeScore;
            dr[AndroidManager._MaxTakeScore] = android.MaxTakeScore;
            dr[AndroidManager._MinReposeTime] = android.MinReposeTime;
            dr[AndroidManager._MaxReposeTime] = android.MaxReposeTime;
            dr[AndroidManager._ServiceGender] = android.ServiceGender;
            dr[AndroidManager._ServiceTime] = android.ServiceTime;
            dr[AndroidManager._AndroidNote] = android.AndroidNote;
            dr[AndroidManager._Nullity] = android.Nullity;
            dr[AndroidManager._CreateDate] = android.CreateDate;

            aideAndroidProvider.Insert(dr);
        }

        /// <summary>
        /// 更新机器人
        /// </summary>
        /// <param name="awardType"></param>
        public void UpdateAndroid(AndroidManager android)
        {
            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.Append("UPDATE AndroidManager SET ")
                    .Append("ServerID=@ServerID ,")
                    .Append("MinPlayDraw=@MinPlayDraw,")
                    .Append("MaxPlayDraw=@MaxPlayDraw,")
                    .Append("MinTakeScore=@MinTakeScore,")
                    .Append("MaxTakeScore=@MaxTakeScore,")
                    .Append("MinReposeTime=@MinReposeTime,")
                    .Append("MaxReposeTime=@MaxReposeTime,")
                    .Append("ServiceGender=@ServiceGender,")
                    .Append("ServiceTime=@ServiceTime,")
                    .Append("AndroidNote=@AndroidNote,")
                    .Append("Nullity=@Nullity ")
                    .Append("WHERE UserID= @UserID");
            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("ServerID", android.ServerID));
            prams.Add(Database.MakeInParam("MinPlayDraw", android.MinPlayDraw));
            prams.Add(Database.MakeInParam("MaxPlayDraw", android.MaxPlayDraw));
            prams.Add(Database.MakeInParam("MinTakeScore", android.MinTakeScore));
            prams.Add(Database.MakeInParam("MaxTakeScore", android.MaxTakeScore));
            prams.Add(Database.MakeInParam("MinReposeTime", android.MinReposeTime));
            prams.Add(Database.MakeInParam("MaxReposeTime", android.MaxReposeTime));
            prams.Add(Database.MakeInParam("ServiceGender", android.ServiceGender));
            prams.Add(Database.MakeInParam("ServiceTime", android.ServiceTime));
            prams.Add(Database.MakeInParam("AndroidNote", android.AndroidNote));
            prams.Add(Database.MakeInParam("Nullity", android.Nullity));

            prams.Add(Database.MakeInParam("UserID", android.UserID));
            Database.ExecuteNonQuery(CommandType.Text, sqlQuery.ToString(), prams.ToArray());
        }

        /// <summary>
        /// 删除机器人
        /// </summary>
        /// <param name="sqlQuery"></param>
        public void DeleteAndroid(string sqlQuery)
        {
            aideAndroidProvider.Delete(sqlQuery);
        }

        /// <summary>
        /// 冻结或解冻机器人
        /// </summary>
        /// <param name="nullity"></param>
        /// <param name="sqlQuery"></param>
        public void NullityAndroid(byte nullity, string sqlQuery)
        {
            string sqlQueryAll = string.Format("UPDATE AndroidManager SET Nullity={0} {1}", nullity, sqlQuery);
            Database.ExecuteNonQuery(sqlQueryAll);
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
        public PagerSet GetRecordUserInoutList(int pageIndex, int pageSize, string condition, string orderby)
        {
            PagerParameters pagerPrams = new PagerParameters(RecordUserInout.Tablename, orderby, condition, pageIndex, pageSize);

            return GetPagerSet2(pagerPrams);
        }

        /// <summary>
        /// 删除小于等于该日期的进出记录
        /// </summary>
        /// <param name="dt">删除截止日期</param>
        /// <returns></returns>
        public int DeleteRecordUserInoutByTime(DateTime dt)
        {
            string sqlQuery = "DELETE RecordUserInout WHERE LeaveTime<@Date";

            var param = new List<DbParameter>();
            param.Add(Database.MakeInParam("Date", dt));

            return Database.ExecuteNonQuery(CommandType.Text, sqlQuery, param.ToArray());
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
        public PagerSet GetRecordInsureList(int pageIndex, int pageSize, string condition, string orderby)
        {
            PagerParameters pagerPrams = new PagerParameters(RecordInsure.Tablename, orderby, condition, pageIndex, pageSize);

            return GetPagerSet2(pagerPrams);
        }

        /// <summary>
        /// 获取转账税收前100名
        /// </summary>
        /// <returns></returns>
        public DataSet GetUserTransferTop100()
        {
            return Database.ExecuteDataset(CommandType.StoredProcedure, "WSP_PM_GetUserTransferTop100");
        }

        /// <summary>
        /// 删除小于等于该日期的银行记录
        /// </summary>
        /// <param name="dt">删除截止日期</param>
        /// <returns></returns>
        public int DeleteRecordInsureByTime(DateTime dt)
        {
            string sqlQuery = "DELETE RecordInsure WHERE CollectDate<@Date";

            var param = new List<DbParameter>();
            param.Add(Database.MakeInParam("Date", dt));

            return Database.ExecuteNonQuery(CommandType.Text, sqlQuery, param.ToArray());
        }
        #endregion

        #region 推广管理
        /// <summary>
        /// 获取推广配置信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public GlobalSpreadInfo GetGlobalSpreadInfo(int id)
        {
            string sqlQuery = string.Format("(NOLOCK) WHERE ID= {0}", id);
            GlobalSpreadInfo spread = aideGlobalSpreadInfoProvider.GetObject<GlobalSpreadInfo>(sqlQuery);
            return spread;
        }
        /// <summary>
        /// 更新推广配置信息
        /// </summary>
        /// <param name="spreadinfo"></param>
        public void UpdateGlobalSpreadInfo(GlobalSpreadInfo spreadinfo)
        {
            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.Append("UPDATE GlobalSpreadInfo SET ")
                    .Append("RegisterGrantScore=@RegisterGrantScore ,")
                    .Append("PlayTimeCount=@PlayTimeCount,")
                    .Append("PlayTimeGrantScore=@PlayTimeGrantScore,")
                    .Append("FillGrantRate=@FillGrantRate,")
                    .Append("BalanceRate=@BalanceRate,")
                    .Append("MinBalanceScore=@MinBalanceScore ")
                    .Append("WHERE ID= @ID");
            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("ID", spreadinfo.ID));
            prams.Add(Database.MakeInParam("RegisterGrantScore", spreadinfo.RegisterGrantScore));
            prams.Add(Database.MakeInParam("PlayTimeCount", spreadinfo.PlayTimeCount));
            prams.Add(Database.MakeInParam("PlayTimeGrantScore", spreadinfo.PlayTimeGrantScore));
            prams.Add(Database.MakeInParam("FillGrantRate", spreadinfo.FillGrantRate));
            prams.Add(Database.MakeInParam("BalanceRate", spreadinfo.BalanceRate));
            prams.Add(Database.MakeInParam("MinBalanceScore", spreadinfo.MinBalanceScore));

            Database.ExecuteNonQuery(CommandType.Text, sqlQuery.ToString(), prams.ToArray());
        }

        /// <summary>
        /// 获取推广财务列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="condition"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public PagerSet GetRecordSpreadInfoList(int pageIndex, int pageSize, string condition, string orderby)
        {
            PagerParameters pagerPrams = new PagerParameters(RecordSpreadInfo.Tablename, orderby, condition, pageIndex, pageSize);

            return GetPagerSet2(pagerPrams);
        }
        /// <summary>
        /// 获取推广员的推广总收入金币
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public int GetSpreadScore(int userID)
        {
            string sqlQuery = string.Format("SELECT ISNULL(SUM(Score),0) FROM RecordSpreadInfo WHERE Score>0 AND UserID= {0}", userID);
            object obj = Database.ExecuteScalar(CommandType.Text, sqlQuery);
            if(obj == null || obj.ToString().Length <= 0)
                return 0;
            return int.Parse(obj.ToString());
        }
        #endregion

        #region 数据分析

        /// <summary>
        /// 获取金币分布数据
        /// </summary>
        /// <returns></returns>
        public DataSet GetGoldDistribution()
        {
            DataSet ds = Database.ExecuteDataset(CommandType.StoredProcedure, "NET_PM_AnalGoldDistribution");
            return ds;
        }

        /// <summary>
        /// 充值统计
        /// </summary>
        /// <param name="siteID"></param>
        /// <returns></returns>
        public DataSet GetPayStat()
        {
            DataSet ds = Database.ExecuteDataset("NET_PM_AnalPayStat");
            return ds;
        }

        /// <summary>
        /// 根据天充值统计
        /// </summary>
        /// <param name="startID"></param>
        /// <param name="endID"></param>
        /// <returns></returns>
        public DataSet GetPayStatByDay(string startDate, string endDate)
        {
            string sql = "SELECT CONVERT(VARCHAR(10),ApplyDate,120) AS ApplyDate,SUM(PayAmount) AS PayAmount FROM ShareDetailInfo WHERE ApplyDate>=@StartDate AND ApplyDate<=@EndDate";
            sql += " GROUP BY CONVERT(VARCHAR(10),ApplyDate,120) ORDER BY ApplyDate DESC";

            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("StartDate", startDate));
            prams.Add(Database.MakeInParam("EndDate", endDate));

            DataSet ds = Database.ExecuteDataset(CommandType.Text, sql, prams.ToArray());
            return ds;
        }

        /// <summary>
        /// 获取每天活跃玩家数
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public DataSet GetActiveUserByDay(int startDateID, int endDateID)
        {
            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("StartDateID", startDateID));
            prams.Add(Database.MakeInParam("EndDateID", endDateID));
            DataSet ds = Database.ExecuteDataset(CommandType.StoredProcedure, "WSP_PM_StatActiveUserTotalByDay", prams.ToArray());
            return ds;
        }

        /// <summary>
        /// 获取每月活跃玩家数
        /// </summary>
        /// <returns></returns>
        public DataSet GetActivieUserByMonth()
        {
            DataSet ds = Database.ExecuteDataset(CommandType.StoredProcedure, "WSP_PM_StatActiveUserTotalByMonth", null);
            return ds;
        }

        /// <summary>
        /// 统计需要清理的数据表记录总数、记录最大日期、记录最小日期等
        /// </summary>
        /// <returns></returns>
        public DataSet StatRecordTable()
        {
            return Database.ExecuteDataset(CommandType.StoredProcedure, "WSP_PM_StatRecordTable", null);
        }

        /// <summary>
        /// 获取统计信息
        /// </summary>
        /// <returns></returns>
        public DataSet GetStatInfo()
        {
            DataSet ds;
            Database.RunProc("NET_PM_StatInfo", out ds);
            return ds;
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
            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("strAccounts", accounts));
            prams.Add(Database.MakeInParam("strPassword", logonPass));
            prams.Add(Database.MakeInParam("strMachineSerial", machineID));
            prams.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            Message msg = MessageHelper.GetMessageForDataSet(Database, "APP_PM_StatFilled", prams);
            return msg;
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
            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("strAccounts", accounts));
            prams.Add(Database.MakeInParam("strPassword", logonPass));
            prams.Add(Database.MakeInParam("strMachineSerial", machineID));
            prams.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            Message msg = MessageHelper.GetMessageForDataSet(Database, "APP_PM_StatFilledCash", prams);
            return msg;
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
            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("strAccounts", accounts));
            prams.Add(Database.MakeInParam("strPassword", logonPass));
            prams.Add(Database.MakeInParam("strMachineSerial", machineID));
            prams.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            Message msg = MessageHelper.GetMessageForDataSet(Database, "APP_PM_StatFilledScore", prams);
            return msg;
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
            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("strAccounts", accounts));
            prams.Add(Database.MakeInParam("strPassword", logonPass));
            prams.Add(Database.MakeInParam("strMachineSerial", machineID));
            prams.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            Message msg = MessageHelper.GetMessageForDataSet(Database, "APP_PM_StatScorePresent", prams);
            return msg;
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
            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("strAccounts", accounts));
            prams.Add(Database.MakeInParam("strPassword", logonPass));
            prams.Add(Database.MakeInParam("strMachineSerial", machineID));
            prams.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            Message msg = MessageHelper.GetMessageForDataSet(Database, "APP_PM_StatScoreRevenue", prams);
            return msg;
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
            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("strAccounts", accounts));
            prams.Add(Database.MakeInParam("strPassword", logonPass));
            prams.Add(Database.MakeInParam("strMachineSerial", machineID));
            prams.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            Message msg = MessageHelper.GetMessageForDataSet(Database, "APP_PM_StatScoreWaste", prams);
            return msg;
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
            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("strAccounts", accounts));
            prams.Add(Database.MakeInParam("strPassword", logonPass));
            prams.Add(Database.MakeInParam("strMachineSerial", machineID));
            prams.Add(Database.MakeInParam("dwTypeID", typeID));
            prams.Add(Database.MakeInParam("dwDateType", dateType));
            prams.Add(Database.MakeInParam("strStartDate", startDate));
            prams.Add(Database.MakeInParam("strEndDate", endDate));
            prams.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            Message msg = MessageHelper.GetMessageForDataSet(Database, "APP_PM_GetChargeData", prams);
            return msg;
        }

        /// <summary>
        /// 充值详情（金币）
        /// </summary>
        /// <param name="accounts"></param>
        /// <param name="logonPass"></param>
        /// <param name="machineID"></param>
        /// <param name="dateType"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public Message AppGetPayScoreData(string accounts, string logonPass, string machineID, int dateType, string startDate, string endDate)
        {
            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("strAccounts", accounts));
            prams.Add(Database.MakeInParam("strPassword", logonPass));
            prams.Add(Database.MakeInParam("strMachineSerial", machineID));
            prams.Add(Database.MakeInParam("dwDateType", dateType));
            prams.Add(Database.MakeInParam("strStartDate", startDate));
            prams.Add(Database.MakeInParam("strEndDate", endDate));
            prams.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            Message msg = MessageHelper.GetMessageForDataSet(Database, "APP_PM_GetPayScoreData", prams);
            return msg;
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
            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("strAccounts", accounts));
            prams.Add(Database.MakeInParam("strPassword", logonPass));
            prams.Add(Database.MakeInParam("strMachineSerial", machineID));
            prams.Add(Database.MakeInParam("dwTypeID", typeID));
            prams.Add(Database.MakeInParam("dwDateType", dateType));
            prams.Add(Database.MakeInParam("strStartDate", startDate));
            prams.Add(Database.MakeInParam("strEndDate", endDate));
            prams.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            Message msg = MessageHelper.GetMessageForDataSet(Database, "APP_PM_GetRevenueData", prams);
            return msg;
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
            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("strAccounts", accounts));
            prams.Add(Database.MakeInParam("strPassword", logonPass));
            prams.Add(Database.MakeInParam("strMachineSerial", machineID));
            prams.Add(Database.MakeInParam("dwTypeID", typeID));
            prams.Add(Database.MakeInParam("dwDateType", dateType));
            prams.Add(Database.MakeInParam("strStartDate", startDate));
            prams.Add(Database.MakeInParam("strEndDate", endDate));
            prams.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            Message msg = MessageHelper.GetMessageForDataSet(Database, "APP_PM_GetPresentData", prams);
            return msg;
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
            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("strAccounts", accounts));
            prams.Add(Database.MakeInParam("strPassword", logonPass));
            prams.Add(Database.MakeInParam("strMachineSerial", machineID));
            prams.Add(Database.MakeInParam("dwDateType", dateType));
            prams.Add(Database.MakeInParam("strStartDate", startDate));
            prams.Add(Database.MakeInParam("strEndDate", endDate));
            prams.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            Message msg = MessageHelper.GetMessageForDataSet(Database, "APP_PM_GetWasteData", prams);
            return msg;
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
            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("strAccounts", accounts));
            prams.Add(Database.MakeInParam("strPassword", logonPass));
            prams.Add(Database.MakeInParam("strMachineSerial", machineID));
            prams.Add(Database.MakeInParam("dwDateType", dateType));
            prams.Add(Database.MakeInParam("strStartDate", startDate));
            prams.Add(Database.MakeInParam("strEndDate", endDate));
            prams.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            Message msg = MessageHelper.GetMessageForDataSet(Database, "APP_PM_GetPlatScoreData", prams);
            return msg;
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
            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("strAccounts", accounts));
            prams.Add(Database.MakeInParam("strPassword", logonPass));
            prams.Add(Database.MakeInParam("strMachineSerial", machineID));
            prams.Add(Database.MakeInParam("dwTypeID", typeID));
            prams.Add(Database.MakeInParam("dwDateType", dateType));
            prams.Add(Database.MakeInParam("strStartDate", startDate));
            prams.Add(Database.MakeInParam("strEndDate", endDate));
            prams.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            Message msg = MessageHelper.GetMessageForDataSet(Database, "APP_PM_GetMemberData", prams);
            return msg;
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
            string sqlQuery = string.Format("SELECT ISNULL(SUM(AgentRevenue),0) FROM RecordUserRevenue WHERE UserID= {0}", userID);
            object obj = Database.ExecuteScalar(CommandType.Text, sqlQuery);
            if (obj == null || obj.ToString().Length <= 0)
                return 0;
            return Convert.ToInt64(obj);
        }

        /// <summary>
        /// 获取代理商下级玩家贡献的充值分成金币
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public Int64 GetChildPayProvide(int userID)
        {
            string sqlQuery = string.Format("SELECT ISNULL(SUM(Score),0) FROM RecordAgentInfo WHERE ChildrenID= {0} AND TypeID=1", userID);
            object obj = Database.ExecuteScalar(CommandType.Text, sqlQuery);
            if (obj == null || obj.ToString().Length <= 0)
                return 0;
            return Convert.ToInt64(obj);
        }

        /// <summary>
        /// 获取代理分成详情
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public DataSet GetAgentFinance(int userID)
        {
            var prams = new List<DbParameter>();

            prams.Add(Database.MakeInParam("dwUserID", userID));

            return Database.ExecuteDataset(CommandType.StoredProcedure, "WSP_PM_GetAgentFinance", prams.ToArray());
        }

        /// <summary>
        /// 手工统计税收
        /// </summary>
        public void StatRevenueHand()
        {
            Database.ExecuteScalar(CommandType.StoredProcedure, "WSP_PM_StatAccountRevenueHand");
        }
        /// <summary>
        /// 手工统计返现
        /// </summary>
        public void StatAgentPayHand()
        {
            Database.ExecuteScalar(CommandType.StoredProcedure, "WSP_PM_StatAgentPayHand");
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
            string sqlQuery = string.Format("(NOLOCK) WHERE ID= {0}", id);
            LotteryConfig model = aideLotteryConfigProvider.GetObject<LotteryConfig>(sqlQuery);
            return model;
        }

        /// <summary>
        /// 更新转盘配置
        /// </summary>
        /// <param name="spreadinfo"></param>
        public void UpdateLotteryConfig(LotteryConfig model)
        {
            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.Append("UPDATE LotteryConfig SET ")
                    .Append("FreeCount=@FreeCount ,")
                    .Append("ChargeFee=@ChargeFee,")
                    .Append("IsCharge=@IsCharge ")                    
                    .Append("WHERE ID= @ID");
            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("ID", model.ID));
            prams.Add(Database.MakeInParam("FreeCount", model.FreeCount));
            prams.Add(Database.MakeInParam("ChargeFee", model.ChargeFee));
            prams.Add(Database.MakeInParam("IsCharge", model.IsCharge));            

            Database.ExecuteNonQuery(CommandType.Text, sqlQuery.ToString(), prams.ToArray());
        }

        /// <summary>
        /// 更新转盘奖品配置
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateLotteryItem(LotteryItem model)
        {
            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.Append("UPDATE LotteryItem SET ")
                    .Append("ItemType=@ItemType,")
                    .Append("ItemQuota=@ItemQuota,")
                    .Append("ItemRate=@ItemRate ")
                    .Append("WHERE ItemIndex=@ItemIndex");

            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("ItemType", model.ItemType));
            prams.Add(Database.MakeInParam("ItemQuota", model.ItemQuota));
            prams.Add(Database.MakeInParam("ItemRate", model.ItemRate));
            prams.Add(Database.MakeInParam("ItemIndex", model.ItemIndex));

            return Database.ExecuteNonQuery(CommandType.Text, sqlQuery.ToString(), prams.ToArray());
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

        #endregion

    }
}
