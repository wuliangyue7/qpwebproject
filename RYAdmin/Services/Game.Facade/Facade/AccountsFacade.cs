using System;
using System.Data;
using Game.Data.Factory;
using Game.Entity.Accounts;
using Game.IData;
using Game.Kernel;

namespace Game.Facade
{
    /// <summary>
    /// 帐号库外观
    /// </summary>
    public class AccountsFacade
    {
        #region Fields

        private IAccountsDataProvider aideAccountsData;

        #endregion Fields

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public AccountsFacade()
        {
            aideAccountsData = ClassFactory.IAccountsDataProvider();
        }

        #endregion 构造函数

        #region 用户基本信息管理

        /// <summary>
        /// 分页获取所有玩家列表
        /// </summary>
        /// <returns></returns>
        public PagerSet GetAccountsList( int pageIndex, int pageSize, string condition, string orderby )
        {
            return aideAccountsData.GetAccountsList( pageIndex, pageSize, condition, orderby );
        }

        /// <summary>
        /// 冻结玩家
        /// </summary>
        /// <param name="sqlQuery"></param>
        public void DongjieAccount( string sqlQuery )
        {
            aideAccountsData.DongjieAccount( sqlQuery );
        }

        /// <summary>
        /// 解冻玩家
        /// </summary>
        /// <param name="sqlQuery"></param>
        public void JieDongAccount( string sqlQuery )
        {
            aideAccountsData.JieDongAccount( sqlQuery );
        }

        /// <summary>
        /// 设为机器人
        /// </summary>
        /// <param name="sqlQuery">条件</param>
        public void SettingAndroid( string sqlQuery )
        {
            aideAccountsData.SettingAndroid( sqlQuery );
        }

        /// <summary>
        /// 取消机器人
        /// </summary>
        /// <param name="sqlQuery">条件</param>
        public void CancleAndroid( string sqlQuery )
        {
            aideAccountsData.CancleAndroid( sqlQuery );
        }

        /// <summary>
        /// 获取用户基本信息通过用户ID
        /// </summary>
        /// <param name="userID">用户UserID</param>
        /// <returns>用户实体</returns>
        public AccountsInfo GetAccountInfoByUserID( int userID )
        {
            return aideAccountsData.GetAccountInfoByUserID( userID );
        }

        /// <summary>
        /// 获取用户基本信息通过用户帐号
        /// </summary>
        /// <param name="account">用户帐号</param>
        /// <returns>用户实体</returns>
        public AccountsInfo GetAccountInfoByAccount( string account )
        {
            return aideAccountsData.GetAccountInfoByAccount( account );
        }

        /// <summary>
        /// 获取用户基本信息通过帐号
        /// </summary>
        /// <param name="account">用户帐号</param>
        /// <returns></returns>
        public AccountsInfo GetAccountInfoByNickname( string nickname )
        {
            return aideAccountsData.GetAccountInfoByNickname( nickname );
        }

        /// <summary>
        /// 获取用户基本信息通过GameID
        /// </summary>
        /// <param name="GameID">GameID</param>
        /// <returns>用户实体</returns>
        public AccountsInfo GetAccountInfoByGameID( int gameID )
        {
            return aideAccountsData.GetAccountInfoByGameID( gameID );
        }

        /// <summary>
        /// 获取帐号
        /// </summary>
        /// <param name="userID">用户UserID</param>
        /// <returns></returns>
        public string GetAccountByUserID( int userID )
        {
            return aideAccountsData.GetAccountByUserID( userID );
        }

        /// <summary>
        /// 获得用户经验值
        /// </summary>
        /// <param name="userID">用户UserID</param>
        /// <returns></returns>
        public int GetExperienceByUserID( int userID )
        {
            return aideAccountsData.GetExperienceByUserID( userID );
        }

        /// <summary>
        /// 更新用户基本信息
        /// </summary>
        /// <param name="account">AccountsInfo实体</param>
        /// <param name="account">管理员ID</param>
        /// <param name="clientIP">客户端IP</param>
        /// <returns>Message</returns>
        public Message UpdateAccount( AccountsInfo account, int masterID, string clientIP )
        {
            return aideAccountsData.UpdateAccount( account, masterID, clientIP );
        }

        /// <summary>
        /// 添加玩家信息
        /// </summary>
        /// <param name="account">AccountsInfo实体</param>
        /// <param name="datum">IndividualDatum实体</param>
        /// <returns></returns>
        public Message AddAccount( AccountsInfo account, IndividualDatum datum )
        {
            return aideAccountsData.AddAccount( account, datum );
        }

        /// <summary>
        /// 给用户添加奖牌
        /// </summary>
        /// <param name="userId">用户标识</param>
        /// <param name="medal">奖牌数量</param>
        /// <returns>成功返回True 失败返回False</returns>
        public bool AddUserMedal( int userId, int medal )
        {
            return aideAccountsData.AddUserMedal( userId, medal );
        }

        /// <summary>
        /// 更新用户密码
        /// </summary>
        /// <param name="accountsInfo">用户实体</param>
        /// <returns>更新成功返回True 失败返回False</returns>
        public bool UpdateUserPassword( AccountsInfo accountsInfo )
        {
            return aideAccountsData.UpdateUserPassword( accountsInfo );
        }

        #endregion 用户基本信息管理

        #region 用户详细信息管理

        /// <summary>
        /// 获取用户详细信息
        /// </summary>
        /// <param name="userID">用户UserID</param>
        /// <returns></returns>
        public IndividualDatum GetAccountDetailByUserID( int userID )
        {
            return aideAccountsData.GetAccountDetailByUserID( userID );
        }

        /// <summary>
        /// 新增详细信息
        /// </summary>
        /// <param name="accountDetail"></param>
        /// <returns></returns>
        public Message InsertAccountDetail( IndividualDatum accountDetail )
        {
            aideAccountsData.InsertAccountDetail( accountDetail );
            return new Message( true );
        }

        /// <summary>
        /// 更新用户详细信息
        /// </summary>
        /// <param name="accountDetail">IndividualDatum实体</param>
        /// <returns></returns>
        public Message UpdateAccountDetail( IndividualDatum accountDetail )
        {
            aideAccountsData.UpdateAccountDetail( accountDetail );
            return new Message( true );
        }

        #endregion 用户详细信息管理

        #region 自定义头像

        /// <summary>
        /// 获取用户头像URL地址
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string GetUserFaceUrl( int faceID, int customID )
        {
            string faceUrl = string.Empty;
            if( customID == 0 )
            {
                // 使用系统头像
                faceUrl = string.Format("/gamepic/Avatar{0}.png", faceID);
            }
            else
            {
                AccountsFace faceModel = GetAccountsFace( customID );
                if( faceModel == null )
                {
                    // 使用系统头像
                    faceUrl = string.Format("/gamepic/Avatar{0}.png", faceID);
                }
                else
                {
                    // 生成随机数
                    Random rand = new Random();
                    double randomNumber = rand.NextDouble();

                    // 自定义头像
                    faceUrl = string.Format( "/Tools/UserFace.ashx?customid={0}&x={1}", customID, randomNumber );
                }
            }
            return faceUrl;
        }

        /// <summary>
        /// 获取用户自定义头像列表
        /// </summary>
        /// <param name="userId">用户标识</param>
        /// <returns>数据集</returns>
        public DataSet GetAccountsFaceList( int userId )
        {
            return aideAccountsData.GetAccountsFaceList( userId );
        }

        /// <summary>
        /// 获取自定义头像实体
        /// </summary>
        /// <param name="customId">自定义头像</param>
        /// <returns>自定义头像实体</returns>
        public AccountsFace GetAccountsFace( int customId )
        {
            return aideAccountsData.GetAccountsFace( customId );
        }

        #endregion 自定义头像

        #region 限制字符管理

        /// <summary>
        /// 分页获取限制字符列表
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="condition">条件</param>
        /// <param name="orderby">排序</param>
        /// <returns></returns>
        public PagerSet GetConfineContentList( int pageIndex, int pageSize, string condition, string orderby )
        {
            return aideAccountsData.GetConfineContentList( pageIndex, pageSize, condition, orderby );
        }

        /// <summary>
        /// 获取限制字符
        /// </summary>
        /// <param name="pid">限制字符表主健</param>
        /// <returns></returns>
        public ConfineContent GetConfineContentByContentID( int contentID )
        {
            return aideAccountsData.GetConfineContentByContentID( contentID );
        }

        /// <summary>
        /// 新增一条限制字符信息
        /// </summary>
        /// <param name="content"></param>
        public Message InsertConfineContent( ConfineContent content )
        {
            aideAccountsData.InsertConfineContent( content );
            return new Message( true );
        }

        /// <summary>
        ///  更新限制字符信息
        /// </summary>
        /// <param name="content"></param>
        public Message UpdateConfineContent( ConfineContent content )
        {
            aideAccountsData.UpdateConfineContent( content );
            return new Message( true );
        }

        /// <summary>
        /// 删除限制字符信息
        /// </summary>
        /// <param name="pid"></param>
        public void DeleteConfineContentByContentID( int contentID )
        {
            aideAccountsData.DeleteConfineContentByContentID( contentID );
        }

        /// <summary>
        /// 删除限制字符信息
        /// </summary>
        /// <param name="sqlQuery"></param>
        public void DeleteConfineContent( string sqlQuery )
        {
            aideAccountsData.DeleteConfineContent( sqlQuery );
        }

        #endregion 限制字符管理

        #region 用户密保信息管理

        /// <summary>
        /// 获取用户密保信息
        /// </summary>
        /// <param name="pid">密保ProtectID</param>
        /// <returns></returns>
        public AccountsProtect GetAccountsProtectByPID( int pid )
        {
            return aideAccountsData.GetAccountsProtectByPID( pid );
        }

        /// <summary>
        /// 获取用户密保信息
        /// </summary>
        /// <param name="uid">用户标识</param>
        /// <returns></returns>
        public AccountsProtect GetAccountsProtectByUserID( int uid )
        {
            return aideAccountsData.GetAccountsProtectByUserID( uid );
        }

        /// <summary>
        /// 更新用户密保信息
        /// </summary>
        /// <param name="accountProtect"></param>
        public Message UpdateAccountsProtect( AccountsProtect accountProtect )
        {
            aideAccountsData.UpdateAccountsProtect( accountProtect );
            return new Message( true );
        }

        /// <summary>
        /// 删除用户密保信息
        /// </summary>
        /// <param name="pid">密保ProtectID</param>
        public void DeleteAccountsProtect( int pid )
        {
            aideAccountsData.DeleteAccountsProtect( pid );
        }

        /// <summary>
        /// 删除限制字符信息
        /// </summary>
        /// <param name="sqlQuery"></param>
        public void DeleteAccountsProtect( string sqlQuery )
        {
            aideAccountsData.DeleteAccountsProtect( sqlQuery );
        }

        #endregion 用户密保信息管理

        #region 靓号信息

        /// <summary>
        /// 随机取出10个靓号
        /// </summary>
        /// <returns></returns>
        public DataSet GetReserveIdentifierList()
        {
            return aideAccountsData.GetReserveIdentifierList();
        }

        #endregion 靓号信息

        #region 限制地址管理

        /// <summary>
        /// 分页获取限制地址列表
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="condition">条件</param>
        /// <param name="orderby">排序</param>
        /// <returns></returns>
        public PagerSet GetConfineAddressList( int pageIndex, int pageSize, string condition, string orderby )
        {
            return aideAccountsData.GetConfineAddressList( pageIndex, pageSize, condition, orderby );
        }

        /// <summary>
        /// 获取限制地址
        /// </summary>
        /// <param name="strString">限制地址表主健</param>
        /// <returns></returns>
        public ConfineAddress GetConfineAddressByAddress( string strAddress )
        {
            return aideAccountsData.GetConfineAddressByAddress( strAddress );
        }

        /// <summary>
        /// 新增一条限制地址信息
        /// </summary>
        /// <param name="address"></param>
        public Message InsertConfineAddress( ConfineAddress address )
        {
            aideAccountsData.InsertConfineAddress( address );
            return new Message( true );
        }

        /// <summary>
        ///  更新限制地址信息
        /// </summary>
        /// <param name="address"></param>
        public Message UpdateConfineAddress( ConfineAddress address )
        {
            aideAccountsData.UpdateConfineAddress( address );
            return new Message( true );
        }

        /// <summary>
        /// 删除限制地址信息
        /// </summary>
        /// <param name="strAddress"></param>
        public void DeleteConfineAddressByAddress( string strAddress )
        {
            aideAccountsData.DeleteConfineAddressByAddress( strAddress );
        }

        /// <summary>
        /// 删除限制地址信息
        /// </summary>
        /// <param name="sqlQuery"></param>
        public void DeleteConfineAddress( string sqlQuery )
        {
            aideAccountsData.DeleteConfineAddress( sqlQuery );
        }

        /// <summary>
        /// 获取IP注册前100名
        /// </summary>
        /// <returns></returns>
        public DataSet GetIPRegisterTop100()
        {
            return aideAccountsData.GetIPRegisterTop100();
        }

        /// <summary>
        /// 批量插入限制IP
        /// </summary>
        public void BatchInsertConfineAddress( string ipList )
        {
            aideAccountsData.BatchInsertConfineAddress( ipList );
        }

        #endregion 限制地址管理

        #region 限制机器码管理

        /// <summary>
        /// 分页获取限制机器码列表
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="condition">条件</param>
        /// <param name="orderby">排序</param>
        /// <returns></returns>
        public PagerSet GetConfineMachineList( int pageIndex, int pageSize, string condition, string orderby )
        {
            return aideAccountsData.GetConfineMachineList( pageIndex, pageSize, condition, orderby );
        }

        /// <summary>
        /// 获取限制机器码
        /// </summary>
        /// <param name="strSerial">限制机器码表主健</param>
        /// <returns></returns>
        public ConfineMachine GetConfineMachineBySerial( string strSerial )
        {
            return aideAccountsData.GetConfineMachineBySerial( strSerial );
        }

        /// <summary>
        /// 新增一条限制机器码信息
        /// </summary>
        /// <param name="machine"></param>
        public Message InsertConfineMachine( ConfineMachine machine )
        {
            aideAccountsData.InsertConfineMachine( machine );
            return new Message( true );
        }

        /// <summary>
        ///  更新限制机器码信息
        /// </summary>
        /// <param name="machine"></param>
        public Message UpdateConfineMachine( ConfineMachine machine )
        {
            aideAccountsData.UpdateConfineMachine( machine );
            return new Message( true );
        }

        /// <summary>
        /// 删除限制机器码信息
        /// </summary>
        /// <param name="strSerial"></param>
        public void DeleteConfineMachineBySerial( string strSerial )
        {
            aideAccountsData.DeleteConfineMachineBySerial( strSerial );
        }

        /// <summary>
        /// 删除限制机器码信息
        /// </summary>
        /// <param name="sqlQuery"></param>
        public void DeleteConfineMachine( string sqlQuery )
        {
            aideAccountsData.DeleteConfineMachine( sqlQuery );
        }

        /// <summary>
        /// 机器码注册前100名
        /// </summary>
        /// <returns></returns>
        public DataSet GetMachineRegisterTop100()
        {
            return aideAccountsData.GetMachineRegisterTop100();
        }

        /// <summary>
        /// 批量插入限制机器码
        /// </summary>
        public void BatchInsertConfineMachine( string machineList )
        {
            aideAccountsData.BatchInsertConfineMachine( machineList );
        }

        #endregion 限制机器码管理

        #region 系统参数配置

        /// <summary>
        /// 获取配置列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="condition"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public PagerSet GetSystemStatusInfoList( int pageIndex, int pageSize, string condition, string orderby )
        {
            return aideAccountsData.GetSystemStatusInfoList( pageIndex, pageSize, condition, orderby );
        }

        /// <summary>
        /// 获取配置信息实体
        /// </summary>
        /// <param name="statusName"></param>
        /// <returns></returns>
        public SystemStatusInfo GetSystemStatusInfo( string statusName )
        {
            return aideAccountsData.GetSystemStatusInfo( statusName );
        }

        /// <summary>
        /// 更新配置信息
        /// </summary>
        /// <param name="systemStatusInfo"></param>
        public Message UpdateSystemStatusInfo( SystemStatusInfo systemStatusInfo )
        {
            aideAccountsData.UpdateSystemStatusInfo( systemStatusInfo );
            return new Message( true );
        }

        #endregion 系统参数配置

        #region 数据分析

        /// <summary>
        /// 获取每日注册玩家数
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public DataSet GetRegUserByDays( string startDate, string endDate )
        {
            return aideAccountsData.GetRegUserByDays( startDate, endDate );
        }

        /// <summary>
        /// 获取每小时注册玩家数
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public DataSet GetRegUserByHours( string startDate, string endDate )
        {
            return aideAccountsData.GetRegUserByHours( startDate, endDate );
        }

        /// <summary>
        /// 获取每个月注册玩家数
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public DataSet GetRegUserByMonth()
        {
            return aideAccountsData.GetRegUserByMonth();
        }

        /// <summary>
        /// 获取用户汇总统计
        /// </summary>
        /// <returns></returns>
        public DataSet GetUsersStat()
        {
            return aideAccountsData.GetUsersStat();
        }

        /// <summary>
        /// 获取用户数量
        /// </summary>
        /// <returns></returns>
        public DataSet GetUsersNumber()
        {
            return aideAccountsData.GetUsersNumber();
        }

        #endregion 数据分析

        #region APP运营助手

        /// <summary>
        /// 登录统计
        /// </summary>
        /// <param name="accounts"></param>
        /// <param name="logonPass"></param>
        /// <param name="machineID"></param>
        /// <returns></returns>
        public Message AppStatLogon(string accounts, string logonPass, string machineID)
        {
            return aideAccountsData.AppStatLogon(accounts, logonPass, machineID);
        }

        /// <summary>
        /// 用户活跃统计
        /// </summary>
        /// <param name="accounts"></param>
        /// <param name="logonPass"></param>
        /// <param name="machineID"></param>
        /// <returns></returns>
        public Message AppStatUserActive(string accounts, string logonPass, string machineID)
        {
            return aideAccountsData.AppStatUserActive(accounts, logonPass, machineID);
        }

        /// <summary>
        /// 用户会员统计
        /// </summary>
        /// <param name="accounts"></param>
        /// <param name="logonPass"></param>
        /// <param name="machineID"></param>
        /// <returns></returns>
        public Message AppStatUserMember(string accounts, string logonPass, string machineID)
        {
            return aideAccountsData.AppStatUserMember(accounts, logonPass, machineID);
        }

        /// <summary>
        /// 登录详情
        /// </summary>
        /// <param name="accounts"></param>
        /// <param name="logonPass"></param>
        /// <param name="typeID"></param>
        /// <param name="machineID"></param>
        /// <param name="dateType"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public Message AppGetLogonData(string accounts, string logonPass, int typeID, string machineID, int dateType, string startDate, string endDate)
        {
            return aideAccountsData.AppGetLogonData(accounts, logonPass, typeID, machineID, dateType, startDate, endDate);
        }

        /// <summary>
        /// 注册详情
        /// </summary>
        /// <param name="user"></param>
        /// <param name="typeID"></param>
        /// <param name="dateType"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="errorDes"></param>
        /// <param name="ds"></param>
        /// <returns></returns>
        public Message AppGetRegData(string accounts, string logonPass, int typeID, string machineID, int dateType, string startDate, string endDate)
        {
            return aideAccountsData.AppGetRegData(accounts, logonPass, typeID, machineID, dateType, startDate, endDate);
        }
        #endregion

        #region 输赢控制

        /// <summary>
        /// 查询输赢控制基础配置
        /// </summary>
        /// <returns></returns>
        public ControlConfig GetControlConfig()
        {
            return aideAccountsData.GetControlConfig();
        }

        /// <summary>
        /// 
        /// </summary>
        public void UpdateControlConfig(ControlConfig model)
        {
            aideAccountsData.UpdateControlConfig(model);
        }

        /// <summary>
        /// 删除黑白名单
        /// </summary>
        /// <param name="where"></param>
        public void DeleteAccountsControl(string where)
        {
            aideAccountsData.DeleteAccountsControl(where);
        }

        /// <summary>
        /// 添加黑白名单
        /// </summary>
        /// <param name="model"></param>
        public void AddAccountsControl(AccountsControl model)
        {
            aideAccountsData.AddAccountsControl(model);
        }

        /// <summary>
        /// 更新黑白名单
        /// </summary>
        /// <param name="model"></param>
        public void UpdateAccountsControl(AccountsControl model)
        {
            aideAccountsData.UpdateAccountsControl(model);
        }

         /// <summary>
        /// 查询黑白名单实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AccountsControl GetAccountsControl(int id)
        {
            return aideAccountsData.GetAccountsControl(id);
        }
        #endregion

        #region 代理商管理

        /// <summary>
        /// 冻结玩家
        /// </summary>
        /// <param name="sqlQuery">条件</param>
        public void DongjieAgent(string sqlQuery)
        {
            aideAccountsData.DongjieAgent(sqlQuery);
        }

        /// <summary>
        /// 解冻玩家
        /// </summary>
        /// <param name="sqlQuery">条件</param>
        public void JieDongAgent(string sqlQuery)
        {
            aideAccountsData.JieDongAgent(sqlQuery);
        }

        /// <summary>
        /// 获取用户基本信息通过用户ID
        /// </summary>
        /// <param name="userID">用户UserID</param>
        /// <returns></returns>
        public AccountsAgent GetAccountAgentByUserID(int userID)
        {
            return aideAccountsData.GetAccountAgentByUserID(userID);
        }

        /// <summary>
        /// 获取代理信息通过域名
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public AccountsAgent GetAccountAgentByDomain(string domain)
        {
            return aideAccountsData.GetAccountAgentByDomain(domain);
        }

        /// <summary>
        /// 添加代理信息
        /// </summary>
        /// <param name="agent"></param>
        /// <returns></returns>
        public Message AddAgent(AccountsAgent agent)
        {
            return aideAccountsData.AddAgent(agent);
        }

        /// <summary>
        /// 更新代理信息
        /// </summary>
        /// <param name="agent"></param>
        /// <returns></returns>
        public bool UpdateAgent(AccountsAgent agent)
        {
            return aideAccountsData.UpdateAgent(agent);
        }

        /// <summary>
        /// 获取代理下级玩家数量
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public int GetAgentChildCount(int userID)
        {
            return aideAccountsData.GetAgentChildCount(userID);
        }

        /// <summary>
        /// 获取代理游戏列表实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AccountsAgentGame GetAccountsAgentGameInfo(int id)
        {
            return aideAccountsData.GetAccountsAgentGameInfo(id);
        }

        /// <summary>
        /// 新增代理游戏列表
        /// </summary>
        /// <param name="gameGameItem"></param>
        public Message InsertAccountsAgentGame(AccountsAgentGame model)
        {
            try
            {
                aideAccountsData.InsertAccountsAgentGame(model);
                return new Message(true);
            }
            catch(Exception ex)
            {
                return new Message(false, ex.Message);
            }
        }

        /// <summary>
        /// 更新代理游戏列表
        /// </summary>
        /// <param name="gameGameItem"></param>
        public Message UpdateAccountsAgentGame(AccountsAgentGame model)
        {
            try
            {
                aideAccountsData.UpdateAccountsAgentGame(model);
                return new Message(true);
            }
            catch (Exception ex)
            {
                return new Message(false, ex.Message);
            }
        }

        /// <summary>
        /// 删除代理游戏列表
        /// </summary>
        /// <param name="sqlQuery"></param>
        public Message DeleteAccountsAgentGame(string sqlQuery)
        {
            try
            {
                aideAccountsData.DeleteAccountsAgentGame(sqlQuery);
                return new Message(true);
            }
            catch (Exception ex)
            {
                return new Message(false, ex.Message);
            }
        }   
        #endregion

        #region 会员属性

        /// <summary>
        /// 获取会员类型数据集
        /// </summary>
        /// <returns></returns>
        public DataSet GetMemberPropertyList()
        {
            return aideAccountsData.GetMemberPropertyList();
        }

        /// <summary>
        /// 获取会员类型实体
        /// </summary>
        /// <param name="cardID"></param>
        /// <returns></returns>
        public MemberProperty GetMemberProperty(int memberOrder)
        {
            return aideAccountsData.GetMemberProperty(memberOrder);
        }

        /// <summary>
        /// 更新会员类型
        /// </summary>
        /// <param name="awardType"></param>
        public void UpdateMemberType(MemberProperty memberType)
        {
            aideAccountsData.UpdateMemberType(memberType);
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
            return aideAccountsData.GetList( tableName, pageIndex, pageSize, condition, orderby );
        }

        /// <summary>
        /// 执行SQL语句返回受影响的行数
        /// </summary>
        /// <param name="sql"></param>
        public int ExecuteSql( string sql )
        {
            return aideAccountsData.ExecuteSql( sql );
        }

        /// <summary>
        ///  执行sql返回DataSet
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public DataSet GetDataSetBySql( string sql )
        {
            return aideAccountsData.GetDataSetBySql( sql );
        }

        /// <summary>
        /// 执行SQL语句返回一个值
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public string GetScalarBySql( string sql )
        {
            return aideAccountsData.GetScalarBySql( sql );
        }

        #endregion 公共
    }
}