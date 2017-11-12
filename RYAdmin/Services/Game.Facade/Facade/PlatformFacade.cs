using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using Game.IData;
using Game.Data.Factory;
using Game.Entity.Platform;
using Game.Kernel;

namespace Game.Facade
{
    /// <summary>
    /// 平台库外观
    /// </summary>
    public class PlatformFacade
    {
        #region Fields

        private IPlatformDataProvider aidePlatformData;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public PlatformFacade()
        {
            aidePlatformData = ClassFactory.GetIPlatformDataProvider();
        }
        #endregion

        #region 机器管理

        /// <summary>
        /// 获取机器列表
        /// </summary>
        /// <returns></returns>
        public PagerSet GetDataBaseList( int pageIndex, int pageSize, string condition, string orderby )
        {
            return aidePlatformData.GetDataBaseList( pageIndex, pageSize, condition, orderby );
        }

        /// <summary>
        /// 获取机器实体
        /// </summary>
        /// <param name="dBInfoID"></param>
        /// <returns></returns>
        public DataBaseInfo GetDataBaseInfo( int dBInfoID )
        {
            return aidePlatformData.GetDataBaseInfo( dBInfoID );
        }

        /// <summary>
        /// 获取机器实体
        /// </summary>
        /// <param name="dBAddr"></param>
        /// <returns></returns>
        public DataBaseInfo GetDataBaseInfo( string dBAddr )
        {
            return aidePlatformData.GetDataBaseInfo( dBAddr );
        }

        /// <summary>
        /// 新增站点信息
        /// </summary>
        /// <param name="station"></param>
        public Message InsertDataBase( DataBaseInfo dataBase )
        {
            aidePlatformData.InsertDataBase( dataBase );
            return new Message( true );
        }

        /// <summary>
        ///  更新站点信息
        /// </summary>
        /// <param name="station"></param>
        public Message UpdateDataBase( DataBaseInfo dataBase )
        {
            aidePlatformData.UpdateDataBase( dataBase );
            return new Message( true );
        }

        /// <summary>
        /// 删除机器
        /// </summary>
        /// <param name="sqlQuery"></param>
        public void DeleteDataBase( string sqlQuery )
        {
            aidePlatformData.DeleteDataBase( sqlQuery );
        }
        /// <summary>
        /// 判断机器地址是否存在，true-存在，false-不存在
        /// </summary>
        /// <param name="dBInfoID"></param>
        /// <returns></returns>
        public bool IsExistsDBAddr( string address )
        {
            DataBaseInfo item = GetDataBaseInfo( address );
            if( item == null )
                return false;
            else
                return true;
        }
        #endregion

        #region 游戏模块管理

        /// <summary>
        /// 获取模板列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="condition"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public PagerSet GetGameGameItemList( int pageIndex, int pageSize, string condition, string orderby )
        {
            return aidePlatformData.GetGameGameItemList( pageIndex, pageSize, condition, orderby );
        }

        /// <summary>
        /// 获取模块实体
        /// </summary>
        /// <param name="gameID"></param>
        /// <returns></returns>
        public GameGameItem GetGameGameItemInfo( int gameID )
        {
            return aidePlatformData.GetGameGameItemInfo( gameID );
        }

        /// <summary>
        /// 获取游戏模块标识的最大值
        /// </summary>
        /// <returns></returns>
        public int GetMaxGameGameID()
        {
            return aidePlatformData.GetMaxGameGameID();
        }

        /// <summary>
        /// 新增模块
        /// </summary>
        /// <param name="gameGameItem"></param>
        public Message InsertGameGameItem( GameGameItem gameGameItem )
        {
            aidePlatformData.InsertGameGameItem( gameGameItem );
            return new Message( true );
        }

        /// <summary>
        /// 更新模块
        /// </summary>
        /// <param name="gameGameItem"></param>
        public Message UpdateGameGameItem( GameGameItem gameGameItem )
        {
            aidePlatformData.UpdateGameGameItem( gameGameItem );
            return new Message( true );
        }

        /// <summary>
        /// 删除模块
        /// </summary>
        /// <param name="sqlQuery"></param>
        public void DeleteGameGameItem( string sqlQuery )
        {
            aidePlatformData.DeleteGameGameItem( sqlQuery );
        }

        /// <summary>
        /// 判断游戏模块标识GameID是否存在，true-存在，false-不存在
        /// </summary>
        /// <param name="gameID"></param>
        /// <returns></returns>
        public bool IsExistsGameID( int gameID )
        {
            GameGameItem item = GetGameGameItemInfo( gameID );
            if( item == null )
                return false;
            else
                return true;
        }
        #endregion

        #region 游戏类型

        /// <summary>
        /// 获取游戏类型列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="condition"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public PagerSet GetGameTypeItemList( int pageIndex, int pageSize, string condition, string orderby )
        {
            return aidePlatformData.GetGameTypeItemList( pageIndex, pageSize, condition, orderby );
        }

        /// <summary>
        /// 获取游戏类型实体
        /// </summary>
        /// <param name="gameID"></param>
        /// <returns></returns>
        public GameTypeItem GetGameTypeItemInfo( int typeID )
        {
            return aidePlatformData.GetGameTypeItemInfo( typeID );
        }
        /// <summary>
        /// 获取游戏类型ID的最大值
        /// </summary>
        /// <returns></returns>
        public int GetMaxGameTypeID()
        {
            return aidePlatformData.GetMaxGameTypeID();
        }
        /// <summary>
        /// 新增游戏类型
        /// </summary>
        /// <param name="gameGameItem"></param>
        public Message InsertGameTypeItem( GameTypeItem gameTypeItem )
        {
            aidePlatformData.InsertGameTypeItem( gameTypeItem );
            return new Message( true );
        }

        /// <summary>
        /// 更新游戏类型
        /// </summary>
        /// <param name="gameGameItem"></param>
        public Message UpdateGameTypeItem( GameTypeItem gameTypeItem )
        {
            aidePlatformData.UpdateGameTypeItem( gameTypeItem );
            return new Message( true );
        }

        /// <summary>
        /// 删除游戏类型
        /// </summary>
        /// <param name="sqlQuery"></param>
        public void DeleteGameTypeItem( string sqlQuery )
        {
            aidePlatformData.DeleteGameTypeItem( sqlQuery );
        }

        /// <summary>
        /// 判断类型ID是否存在，true-存在，false-不存在
        /// </summary>
        /// <param name="typeID"></param>
        /// <returns></returns>
        public bool IsExistsTypeID( int typeID )
        {
            GameTypeItem item = GetGameTypeItemInfo( typeID );
            if( item == null )
                return false;
            else
                return true;
        }
        #endregion

        #region 节点

        /// <summary>
        /// 获取游戏节点列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="condition"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public PagerSet GetGameNodeItemList( int pageIndex, int pageSize, string condition, string orderby )
        {
            return aidePlatformData.GetGameNodeItemList( pageIndex, pageSize, condition, orderby );
        }

        /// <summary>
        /// 获取游戏节点实体
        /// </summary>
        /// <param name="nodeID"></param>
        /// <returns></returns>
        public GameNodeItem GetGameNodeItemInfo( int nodeID )
        {
            return aidePlatformData.GetGameNodeItemInfo( nodeID );
        }

        /// <summary>
        /// 获取游戏节点ID的最大值
        /// </summary>
        /// <returns></returns>
        public int GetMaxGameNodeID()
        {
            return aidePlatformData.GetMaxGameNodeID();
        }

        /// <summary>
        /// 新增游戏节点
        /// </summary>
        /// <param name="gameNodeItem"></param>
        public Message InsertGameNodeItem( GameNodeItem gameNodeItem )
        {
            aidePlatformData.InsertGameNodeItem( gameNodeItem );
            return new Message( true );
        }

        /// <summary>
        /// 更新游戏节点
        /// </summary>
        /// <param name="gameNodeItem"></param>
        public Message UpdateGameNodeItem( GameNodeItem gameNodeItem )
        {
            aidePlatformData.UpdateGameNodeItem( gameNodeItem );
            return new Message( true );
        }

        /// <summary>
        /// 删除游戏节点
        /// </summary>
        /// <param name="sqlQuery"></param>
        public void DeleteGameNodeItem( string sqlQuery )
        {
            aidePlatformData.DeleteGameNodeItem( sqlQuery );
        }
        /// <summary>
        /// 判断节点是否存在，true-存在，false-不存在
        /// </summary>
        /// <param name="nodeID"></param>
        /// <returns></returns>
        public bool IsExistsNodeID( int nodeID )
        {
            GameNodeItem item = GetGameNodeItemInfo( nodeID );
            if( item == null )
                return false;
            else
                return true;
        }
        #endregion

        #region 自定义页

        /// <summary>
        /// 获取自定义页列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="condition"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public PagerSet GetGamePageItemList( int pageIndex, int pageSize, string condition, string orderby )
        {
            return aidePlatformData.GetGamePageItemList( pageIndex, pageSize, condition, orderby );
        }

        /// <summary>
        /// 获取自定义页实体
        /// </summary>
        /// <param name="pageID"></param>
        /// <returns></returns>
        public GamePageItem GetGamePageItemInfo( int pageID )
        {
            return aidePlatformData.GetGamePageItemInfo( pageID );
        }
        /// <summary>
        /// 获取页面ID的最大值
        /// </summary>
        /// <returns></returns>
        public int GetMaxGamePageID()
        {
            return aidePlatformData.GetMaxGamePageID();
        }
        /// <summary>
        /// 新增自定义页
        /// </summary>
        /// <param name="gamePageItem"></param>
        public Message InsertGamePageItem( GamePageItem gamePageItem )
        {
            aidePlatformData.InsertGamePageItem( gamePageItem );
            return new Message( true );
        }

        /// <summary>
        /// 更新自定义页
        /// </summary>
        /// <param name="gamePageItem"></param>
        public Message UpdateGamePageItem( GamePageItem gamePageItem )
        {
            aidePlatformData.UpdateGamePageItem( gamePageItem );
            return new Message( true );
        }

        /// <summary>
        /// 删除自定义页
        /// </summary>
        /// <param name="sqlQuery"></param>
        public void DeleteGamePageItem( string sqlQuery )
        {
            aidePlatformData.DeleteGamePageItem( sqlQuery );
        }
        /// <summary>
        /// 判断页面是否存在，true-存在，false-不存在
        /// </summary>
        /// <param name="pageID"></param>
        /// <returns></returns>
        public bool IsExistsPageID( int pageID )
        {
            GamePageItem item = GetGamePageItemInfo( pageID );
            if( item == null )
                return false;
            else
                return true;
        }
        #endregion

        #region 游戏
        /// <summary>
        /// 获取积分游戏列表
        /// </summary>
        /// <returns></returns>
        public DataSet GetGameList()
        {
            return aidePlatformData.GetGameList();
        }
        /// <summary>
        /// 获取游戏列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="condition"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public PagerSet GetGameKindItemList( int pageIndex, int pageSize, string condition, string orderby )
        {
            return aidePlatformData.GetGameKindItemList( pageIndex, pageSize, condition, orderby );
        }

        /// <summary>
        /// 获取游戏实体
        /// </summary>
        /// <param name="gameID"></param>
        /// <returns></returns>
        public GameKindItem GetGameKindItemInfo( int kindID )
        {
            return aidePlatformData.GetGameKindItemInfo( kindID );
        }

        /// <summary>
        /// 获取游戏标识KindID的最大值
        /// </summary>
        /// <returns></returns>
        public int GetMaxGameKindID()
        {
            return aidePlatformData.GetMaxGameKindID();
        }

        /// <summary>
        /// 新增游戏
        /// </summary>
        /// <param name="gameGameItem"></param>
        public Message InsertGameKindItem( GameKindItem gameKindItem )
        {
            aidePlatformData.InsertGameKindItem( gameKindItem );
            return new Message( true );
        }

        /// <summary>
        /// 更新游戏
        /// </summary>
        /// <param name="gameGameItem"></param>
        public Message UpdateGameKindItem( GameKindItem gameKindItem )
        {
            aidePlatformData.UpdateGameKindItem( gameKindItem );
            return new Message( true );
        }

        /// <summary>
        /// 删除游戏
        /// </summary>
        /// <param name="sqlQuery"></param>
        public void DeleteGameKindItem( string sqlQuery )
        {
            aidePlatformData.DeleteGameKindItem( sqlQuery );
        }

        /// <summary>
        /// 判断游戏标识KindID是否存在，true-存在，false-不存在
        /// </summary>
        /// <param name="kindID"></param>
        /// <returns></returns>
        public bool IsExistsKindID( int kindID )
        {
            GameKindItem item = GetGameKindItemInfo( kindID );
            if( item == null )
                return false;
            else
                return true;
        }

        /// <summary>
        /// 更新游戏配置
        /// </summary>
        /// <param name="gameConfig">游戏配置实体</param>
        public void UpdateGameConfig( GameConfig gameConfig )
        {
            aidePlatformData.UpdateGameConfig( gameConfig );
        }

        /// <summary>
        /// 获取游戏配置
        /// </summary>
        /// <param name="kindID">游戏标识</param>
        /// <returns></returns>
        public GameConfig GetGameConfig( int kindID )
        {
            return aidePlatformData.GetGameConfig( kindID );
        }
        #endregion

        #region 手游

        /// <summary>
        /// 获取游戏列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="condition"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public PagerSet GetMobileKindItemList(int pageIndex, int pageSize, string condition, string orderby)
        {
            return aidePlatformData.GetMobileKindItemList(pageIndex, pageSize, condition, orderby);
        }

        /// <summary>
        /// 获取游戏实体
        /// </summary>
        /// <param name="gameID"></param>
        /// <returns></returns>
        public MobileKindItem GetMobileKindItemInfo(int kindID)
        {
            return aidePlatformData.GetMobileKindItemInfo(kindID);
        }

        /// <summary>
        /// 获取游戏标识KindID的最大值
        /// </summary>
        /// <returns></returns>
        public int GetMaxMobileKindID()
        {
            return aidePlatformData.GetMaxMobileKindID();
        }

        /// <summary>
        /// 新增游戏
        /// </summary>
        /// <param name="gameGameItem"></param>
        public Message InsertMobileKindItem(MobileKindItem model)
        {
            try
            {
                aidePlatformData.InsertMobileKindItem(model);
                return new Message(true);
            }
            catch
            {
                return new Message(false);
            }
        }

        /// <summary>
        /// 更新游戏
        /// </summary>
        /// <param name="gameGameItem"></param>
        public Message UpdateMobileKindItem(MobileKindItem model)
        {
            try
            {
                aidePlatformData.UpdateMobileKindItem(model);
                return new Message(true);
            }
            catch
            {
                return new Message(false);
            }
        }

        /// <summary>
        /// 删除游戏
        /// </summary>
        /// <param name="sqlQuery"></param>
        public Message DeleteMobileKindItem(string sqlQuery)
        {
            try
            {
                aidePlatformData.DeleteMobileKindItem(sqlQuery);
                return new Message(true);
            }
            catch
            {
                return new Message(false);
            }            
        }
        #endregion

        #region 游戏房间

        /// <summary>
        /// 获取游戏房间列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="condition"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public PagerSet GetGameRoomInfoList( int pageIndex, int pageSize, string condition, string orderby )
        {
            return aidePlatformData.GetGameRoomInfoList( pageIndex, pageSize, condition, orderby );
        }

        /// <summary>
        /// 获取游戏房间实体
        /// </summary>
        /// <param name="gameID"></param>
        /// <returns></returns>
        public GameRoomInfo GetGameRoomInfoInfo( int serverID )
        {
            return aidePlatformData.GetGameRoomInfoInfo( serverID );
        }

        /// <summary>
        /// 新增游戏房间
        /// </summary>
        /// <param name="gameGameItem"></param>
        public Message InsertGameRoomInfo( GameRoomInfo gameRoomInfo )
        {
            aidePlatformData.InsertGameRoomInfo( gameRoomInfo );
            return new Message( true );
        }

        /// <summary>
        /// 更新游戏房间
        /// </summary>
        /// <param name="gameGameItem"></param>
        public Message UpdateGameRoomInfo( GameRoomInfo gameRoomInfo )
        {
            aidePlatformData.UpdateGameRoomInfo( gameRoomInfo );
            return new Message( true );
        }

        /// <summary>
        /// 删除游戏房间
        /// </summary>
        /// <param name="sqlQuery"></param>
        public void DeleteGameRoomInfo( string sqlQuery )
        {
            aidePlatformData.DeleteGameRoomInfo( sqlQuery );
        }
        #endregion

        #region 在线统计

        /// <summary>
        /// 根据查询条件获得在线人数统计信息
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        public DataSet GetOnLineStreamInfoList( string year, string month, string day )
        {
            string sqlQuery = "";
            if( month == "-1" )//年统计
            {
                sqlQuery = "select CONVERT(varchar(7),InsertDateTime, 120) as InsertDateTime,max(OnLineCountSum) as MaxCount,min(OnLineCountSum) as MinCount,";
                sqlQuery += "convert(int,round(avg(OnLineCountSum*1.0),0)) as AvgCount from OnLineStreamInfo ";
                sqlQuery += "where year(InsertDateTime)='" + year + "' group by CONVERT(varchar(7),InsertDateTime, 120) order by InsertDateTime asc";
            }
            else if( day == "-1" )//月统计
            {
                DateTime startTime = Convert.ToDateTime( year + "-" + month + "-" + "01" );
                DateTime endTime = startTime.AddMonths( 1 );
                sqlQuery = "select CONVERT(varchar(10),InsertDateTime, 120) as InsertDateTime,max(OnLineCountSum) as MaxCount,min(OnLineCountSum) as MinCount,";
                sqlQuery += "convert(int,round(avg(OnLineCountSum*1.0),0)) as AvgCount from OnLineStreamInfo ";
                sqlQuery += "where InsertDateTime>'" + startTime.ToString() + "' and InsertDateTime<'" + endTime.ToString() + "' group by CONVERT(varchar(10),InsertDateTime, 120) order by InsertDateTime asc";
            }
            else//日统计
            {
                string date = year + "-" + month + "-" + day;
                sqlQuery = "select datepart(hh,InsertDateTime) as InsertDateTime,max(OnLineCountSum) as MaxCount,min(OnLineCountSum) as MinCount,";
                sqlQuery += "convert(int,round(avg(OnLineCountSum*1.0),0)) as AvgCount from OnLineStreamInfo ";
                sqlQuery += "where convert(varchar(10),InsertDateTime,120)='" + date + "' group by datepart(hh,InsertDateTime) order by InsertDateTime asc";
            }
            return aidePlatformData.GetOnLineStreamInfoList( sqlQuery );
        }

        /// <summary>
        /// 根据查询条件获得在线游戏人数信息(年/月/日/时)
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <param name="hour"></param>
        /// <returns></returns>
        public DataSet GetOnlineStreamGameInfoList( string year, string month, string day, string hour )
        {
            string sqlQuery = "";
            if( day == "-1" )
            {
                DateTime startTime = Convert.ToDateTime( year + "-" + month + "-" + "01" );
                DateTime endTime = startTime.AddMonths( 1 );
                sqlQuery = "select * from OnLineStreamInfo where InsertDateTime>'" + startTime.ToString() + "' and InsertDateTime<'" + endTime.ToString() + "' order by InsertDateTime asc";
            }
            else if( hour == "-1" )
            {
                string date = year + "-" + month + "-" + day;
                sqlQuery = "select * from OnLineStreamInfo where convert(varchar(10),InsertDateTime,120)='" + date + "' order by InsertDateTime asc";
            }
            else
            {
                string date = year + "-" + month + "-" + day;
                sqlQuery = "select * from OnLineStreamInfo where convert(varchar(10),InsertDateTime,120)='" + date + "' and datepart(hh,InsertDateTime)='" + hour + "' order by InsertDateTime asc";
            }
            return aidePlatformData.GetOnLineStreamInfoList( sqlQuery );
        }

        /// <summary>
        /// 获取在线人数列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="condition"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public PagerSet GetOnLineStreamInfoList( int pageIndex, int pageSize, string condition, string orderby )
        {
            return aidePlatformData.GetOnLineStreamInfoList( pageIndex, pageSize, condition, orderby );
        }
        #endregion

        #region 系统消息

        /// <summary>
        /// 获取系统消息列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="condition"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public PagerSet GetSystemMessageList( int pageIndex, int pageSize, string condition, string orderby )
        {
            return aidePlatformData.GetSystemMessageList( pageIndex, pageSize, condition, orderby );
        }

        /// <summary>
        /// 获取消息实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SystemMessage GetSystemMessageInfo( int id )
        {
            return aidePlatformData.GetSystemMessageInfo( id );
        }

        /// <summary>
        /// 新增游戏
        /// </summary>
        /// <param name="gameGameItem"></param>
        public Message InsertSystemMessage( SystemMessage systemMessage )
        {
            aidePlatformData.InsertSystemMessage( systemMessage );
            return new Message( true );
        }

        /// <summary>
        /// 更新游戏
        /// </summary>
        /// <param name="gameGameItem"></param>
        public Message UpdateSystemMessage( SystemMessage systemMessage )
        {
            aidePlatformData.UpdateSystemMessage( systemMessage );
            return new Message( true );
        }

        /// <summary>
        /// 删除系统消息
        /// </summary>
        /// <param name="sqlQuery"></param>
        public void DeleteSystemMessage( string sqlQuery )
        {
            aidePlatformData.DeleteSystemMessage( sqlQuery );
        }

        #endregion

        #region 泡点管理

        /// <summary>
        /// 获取泡点规则列表
        /// </summary>
        /// <returns></returns>
        public PagerSet GetGlobalPlayPresentList( int pageIndex, int pageSize, string condition, string orderby )
        {
            return aidePlatformData.GetGlobalPlayPresentList( pageIndex, pageSize, condition, orderby );
        }

        /// <summary>
        /// 获取泡点规则实体
        /// </summary>
        /// <param name="serverID"></param>
        /// <returns></returns>
        public GlobalPlayPresent GetGlobalPlayPresentInfo( int serverID )
        {
            return aidePlatformData.GetGlobalPlayPresentInfo( serverID );
        }

        /// <summary>
        /// 新增泡点规则信息
        /// </summary>
        /// <param name="globalPlayPresent"></param>
        public Message InsertGlobalPlayPresent( GlobalPlayPresent globalPlayPresent )
        {
            aidePlatformData.InsertGlobalPlayPresent( globalPlayPresent );
            return new Message( true );
        }

        /// <summary>
        ///  更新泡点规则信息
        /// </summary>
        /// <param name="globalPlayPresent"></param>
        public Message UpdateGlobalPlayPresent( GlobalPlayPresent globalPlayPresent )
        {
            aidePlatformData.UpdateGlobalPlayPresent( globalPlayPresent );
            return new Message( true );
        }

        /// <summary>
        /// 删除泡点规则
        /// </summary>
        /// <param name="sqlQuery"></param>
        public void DeleteGlobalPlayPresent( string sqlQuery )
        {
            aidePlatformData.DeleteGlobalPlayPresent( sqlQuery );
        }
        #endregion

        #region 任务系统

        /// <summary>
        /// 根据任务实体
        /// </summary>
        /// <param name="id">任务id</param>
        public TaskInfo GetTaskInfoByID( int id )
        {
            return aidePlatformData.GetTaskInfoByID( id );
        }

        /// <summary>
        /// 保存任务信息
        /// </summary>
        /// <param name="info">任务实体</param>
        public bool InsertTaskInfo( TaskInfo info )
        {
            aidePlatformData.InsertTaskInfo( info );
            return true;
        }

        /// <summary>
        /// 更新任务信息
        /// </summary>
        /// <param name="info">任务实体</param>
        public bool UpdateTaskInfo( TaskInfo info )
        {
            int result = aidePlatformData.UpdateTaskInfo( info );
            if( result > 0 )
                return true;
            return false;
        }

        /// <summary>
        /// 删除任务
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <returns></returns>
        public void DeleteTaskInfo( string sqlQuery )
        {
            aidePlatformData.DeleteTaskInfo( sqlQuery );
        }

        #endregion

        #region 签到
        /// <summary>
        /// 查询每天签到奖励金币
        /// </summary>
        /// <returns></returns>
        public DataSet GetSigninConfig()
        {
            return aidePlatformData.GetSigninConfig();
        }

        /// <summary>
        /// 更新每天签到奖励金币
        /// </summary>
        /// <param name="ds"></param>
        public void UpdateSigninConfig( DataSet ds )
        {
            aidePlatformData.UpdateSigninConfig( ds );
        }
        #endregion

        #region 等级配置

        /// <summary>
        /// 更新等级配置
        /// </summary>
        /// <param name="glf"></param>
        /// <returns></returns>
        public int UpdateGrowLevelConfig( GrowLevelConfig glc )
        {
            return aidePlatformData.UpdateGrowLevelConfig( glc );
        }

        #endregion

        #region 道具管理

        #region 道具类型

        /// <summary>
        /// 获取道具类型列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="condition"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public PagerSet GetGamePropertyTypeList(int pageIndex, int pageSize, string condition, string orderby)
        {
            return aidePlatformData.GetGamePropertyTypeList(pageIndex, pageSize, condition, orderby);
        }

        /// <summary>
        /// 获取道具类型实体
        /// </summary>
        /// <param name="typeID"></param>
        /// <returns></returns>
        public GamePropertyType GetGamePropertyTypeInfo(int typeID)
        {
            return aidePlatformData.GetGamePropertyTypeInfo(typeID);
        }

        /// <summary>
        /// 获取道具类型ID的最大值
        /// </summary>
        /// <returns></returns>
        public int GetMaxPropertyTypeID()
        {
            return aidePlatformData.GetMaxPropertyTypeID();
        }
        /// <summary>
        /// 新增道具类型
        /// </summary>
        /// <param name="gamePropertyType"></param>
        public Message InsertGamePropertyType(GamePropertyType gamePropertyType)
        {
            aidePlatformData.InsertGamePropertyType(gamePropertyType);
            return new Message(true);
        }

        /// <summary>
        /// 更新道具类型
        /// </summary>
        /// <param name="gamePropertyType"></param>
        public Message UpdateGamePropertyType(GamePropertyType gamePropertyType)
        {
            aidePlatformData.UpdateGamePropertyType(gamePropertyType);
            return new Message(true);
        }

        /// <summary>
        /// 删除道具类型
        /// </summary>
        /// <param name="sqlQuery"></param>
        public void DeleteGamePropertyType(string sqlQuery)
        {
            aidePlatformData.DeleteGamePropertyType(sqlQuery);
        }
        #endregion

        #region 道具

        /// <summary>
        /// 获取道具列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="condition"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public PagerSet GetGamePropertyList(int pageIndex, int pageSize, string condition, string orderby)
        {
            return aidePlatformData.GetGamePropertyList(pageIndex, pageSize, condition, orderby);
        }

        /// <summary>
        /// 获取礼包列表
        /// </summary>
        /// <param name="kind"></param>
        /// <returns></returns>
        public IList<GameProperty> GetGamePropertyGift(int kind)
        {
            return aidePlatformData.GetGamePropertyGift(kind);
        }

        /// <summary>
        /// 获取道具实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public GameProperty GetGamePropertyInfo(int id)
        {
            return aidePlatformData.GetGamePropertyInfo(id);
        }

        /// <summary>
        /// 获取最大的道具ID
        /// </summary>
        /// <returns></returns>
        public int GetMaxPropertyID()
        {
            return aidePlatformData.GetMaxPropertyID();
        }

        /// <summary>
        /// 新增道具
        /// </summary>
        /// <param name="station"></param>
        public void InsertGameProperty(GameProperty property)
        {
            aidePlatformData.InsertGameProperty(property);
        }

        /// <summary>
        /// 更新道具
        /// </summary>
        /// <param name="property"></param>
        public void UpdateGameProperty(GameProperty property)
        {
            aidePlatformData.UpdateGameProperty(property);
        }

        /// <summary>
        /// 删除道具
        /// </summary>
        /// <param name="sqlQuery"></param>
        public void DeleteGameProperty(string sqlQuery)
        {
            aidePlatformData.DeleteGameProperty(sqlQuery);
        }
        /// <summary>
        /// 禁用道具
        /// </summary>
        /// <param name="sqlQuery"></param>
        public void SetPropertyDisbale(string sqlQuery)
        {
            aidePlatformData.SetPropertyDisbale(sqlQuery);
        }

        /// <summary>
        /// 启用道具
        /// </summary>
        /// <param name="sqlQuery"></param>
        public void SetPropertyEnbale(string sqlQuery)
        {
            aidePlatformData.SetPropertyEnbale(sqlQuery);
        }

        /// <summary>
        /// 设置道具推荐属性
        /// </summary>
        /// <param name="recommend"></param>
        /// <param name="sqlQuery"></param>
        public void SetPropertyRecommend(int recommend, string sqlQuery)
        {
            aidePlatformData.SetPropertyRecommend(recommend, sqlQuery);
        }
        #endregion

        #region 礼包

        /// <summary>
        /// 获取礼包明细实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public GamePropertySub GetGamePropertySubInfo(int id, int ownerID)
        {
            return aidePlatformData.GetGamePropertySubInfo(id, ownerID);
        }

        /// <summary>
        /// 新增礼包明细
        /// </summary>
        /// <param name="station"></param>
        public void InsertGamePropertySub(GamePropertySub property)
        {
            aidePlatformData.InsertGamePropertySub(property);
        }

        /// <summary>
        /// 更新礼包明细
        /// </summary>
        /// <param name="property"></param>
        public void UpdateGamePropertySub(GamePropertySub property)
        {
            aidePlatformData.UpdateGamePropertySub(property);
        }

        /// <summary>
        /// 删除礼包明细
        /// </summary>
        /// <param name="sqlQuery"></param>
        public void DeleteGamePropertySub(string sqlQuery)
        {
            aidePlatformData.DeleteGamePropertySub(sqlQuery);
        }
        #endregion
        
        #endregion

        #region 获取积分库连接字符串

        /// <summary>
        /// 获取积分库连接字符串
        /// </summary>
        /// <param name="kindId"></param>
        public string GetConn( int kindID )
        {
            return aidePlatformData.GetConn( kindID );
        }

        #endregion

        #region APP运营助手

        /// <summary>
        /// 在线统计
        /// </summary>
        /// <param name="accounts"></param>
        /// <param name="logonPass"></param>
        /// <param name="machineID"></param>
        /// <returns></returns>
        public Message AppStatOnline(string accounts, string logonPass, string machineID)
        {
            return aidePlatformData.AppStatOnline(accounts, logonPass, machineID);
        }

        /// <summary>
        /// 平台总览
        /// </summary>
        /// <param name="accounts"></param>
        /// <param name="logonPass"></param>
        /// <param name="machineID"></param>
        /// <returns></returns>
        public Message AppPlatformGeneral(string accounts, string logonPass, string machineID)
        {
            return aidePlatformData.AppPlatformGeneral(accounts, logonPass, machineID);
        }

        /// <summary>
        /// 用户在线统计
        /// </summary>
        /// <param name="accounts"></param>
        /// <param name="logonPass"></param>
        /// <param name="machineID"></param>
        /// <returns></returns>
        public Message AppStatUserOnline(string accounts, string logonPass, string machineID)
        {
            return aidePlatformData.AppStatUserOnline(accounts, logonPass, machineID);
        }

        /// <summary>
        /// 在线详情
        /// </summary>
        /// <param name="accounts"></param>
        /// <param name="logonPass"></param>
        /// <param name="machineID"></param>
        /// <param name="dateType"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public Message AppGetOnlineData(string accounts, string logonPass, string machineID, int dateType, string startDate, string endDate)
        {
            return aidePlatformData.AppGetOnlineData(accounts, logonPass, machineID, dateType, startDate, endDate);
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
            return aidePlatformData.GetList( tableName, pageIndex, pageSize, condition, orderby );
        }


        /// <summary>
        /// 执行SQL语句返回受影响的行数
        /// </summary>
        /// <param name="sql"></param>
        public int ExecuteSql( string sql )
        {
            return aidePlatformData.ExecuteSql( sql );
        }

        #endregion
    }
}
