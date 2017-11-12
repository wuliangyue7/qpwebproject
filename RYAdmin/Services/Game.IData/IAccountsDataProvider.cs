using System.Data;
using Game.Entity.Accounts;
using Game.Kernel;

namespace Game.IData
{
    /// <summary>
    /// 帐号库数据层接口
    /// </summary>
    public interface IAccountsDataProvider
    {
        #region 用户基本信息管理

        /// <summary>
        /// 分页获取玩家列表
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="condition">条件</param>
        /// <param name="orderby">排序</param>
        /// <returns></returns>
        PagerSet GetAccountsList( int pageIndex, int pageSize, string condition, string orderby );

        /// <summary>
        /// 获得用户名
        /// </summary>
        /// <param name="userID">用户UserID</param>
        /// <returns></returns>
        string GetAccountByUserID( int userID );

        /// <summary>
        /// 获得用户经验值
        /// </summary>
        /// <param name="userID">用户UserID</param>
        /// <returns></returns>
        int GetExperienceByUserID( int userID );

        /// <summary>
        /// 获取用户基本信息通过用户ID
        /// </summary>
        /// <param name="userID">用户UserID</param>
        /// <returns></returns>
        AccountsInfo GetAccountInfoByUserID( int userID );

        /// <summary>
        /// 获取用户基本信息通过帐号
        /// </summary>
        /// <param name="account">用户帐号</param>
        /// <returns></returns>
        AccountsInfo GetAccountInfoByAccount( string account );

        /// <summary>
        /// 获取用户基本信息通过帐号
        /// </summary>
        /// <param name="account">用户帐号</param>
        /// <returns></returns>
        AccountsInfo GetAccountInfoByNickname( string nickname );

        /// <summary>
        /// 获取用户基本信息通过GameID
        /// </summary>
        /// <param name="gameID"></param>
        /// <returns></returns>
        AccountsInfo GetAccountInfoByGameID( int gameID );

        /// <summary>
        /// 冻结玩家
        /// </summary>
        /// <param name="sqlQuery">条件</param>
        void DongjieAccount( string sqlQuery );

        /// <summary>
        /// 解冻玩家
        /// </summary>
        /// <param name="sqlQuery">条件</param>
        void JieDongAccount( string sqlQuery );

        /// <summary>
        /// 设为机器人
        /// </summary>
        /// <param name="sqlQuery">条件</param>
        void SettingAndroid( string sqlQuery );

        /// <summary>
        /// 取消机器人
        /// </summary>
        /// <param name="sqlQuery">条件</param>
        void CancleAndroid( string sqlQuery );

        /// <summary>
        /// 更新用户基本信息
        /// </summary>
        /// <param name="account">AccountsInfo实体</param>
        /// <param name="account">管理员ID</param>
        /// <param name="clientIP">客户端IP</param>
        /// <returns>Message</returns>
        Message UpdateAccount( AccountsInfo account, int masterID, string clientIP );

        /// <summary>
        /// 添加玩家信息
        /// </summary>
        /// <param name="account">AccountsInfo实体</param>
        /// <param name="datum">IndividualDatum实体</param>
        /// <returns></returns>
        Message AddAccount( AccountsInfo account, IndividualDatum datum );

        /// <summary>
        /// 给用户添加奖牌
        /// </summary>
        /// <param name="userId">用户标识</param>
        /// <param name="medal">奖牌数量</param>
        /// <returns>成功返回True 失败返回False</returns>
        bool AddUserMedal( int userId, int medal );

        /// <summary>
        /// 更新用户密码
        /// </summary>
        /// <param name="accountsInfo">用户实体</param>
        /// <returns>更新成功返回True 失败返回False</returns>
        bool UpdateUserPassword( AccountsInfo accountsInfo );

        #endregion 用户基本信息管理

        #region 用户详细信息管理

        /// <summary>
        /// 获取用户详细信息
        /// </summary>
        /// <param name="userID">用户UserID</param>
        /// <returns></returns>
        IndividualDatum GetAccountDetailByUserID( int userID );

        /// <summary>
        /// 新增详细信息
        /// </summary>
        /// <param name="accountDetail"></param>
        /// <returns></returns>
        void InsertAccountDetail( IndividualDatum accountDetail );

        /// <summary>
        ///  更新用户详细信息
        /// </summary>
        /// <param name="account">IndividualDatum实体</param>
        void UpdateAccountDetail( IndividualDatum accountDetail );

        #endregion 用户详细信息管理

        #region 自定义头像

        /// <summary>
        /// 获取用户自定义头像列表
        /// </summary>
        /// <param name="userId">用户标识</param>
        /// <returns>数据集</returns>
        DataSet GetAccountsFaceList( int userId );

        /// <summary>
        /// 获取自定义头像实体
        /// </summary>
        /// <param name="customId">自定义头像</param>
        /// <returns>自定义头像实体</returns>
        AccountsFace GetAccountsFace( int customId );

        #endregion 自定义头像

        #region 用户密保信息管理

        /// <summary>
        /// 获取用户密保信息
        /// </summary>
        /// <param name="pid">密保ProtectID</param>
        /// <returns></returns>
        AccountsProtect GetAccountsProtectByPID( int pid );

        /// <summary>
        /// 获取用户密保信息
        /// </summary>
        /// <param name="uid">用户标识</param>
        /// <returns></returns>
        AccountsProtect GetAccountsProtectByUserID( int uid );

        /// <summary>
        /// 更新用户密保信息
        /// </summary>
        /// <param name="accountProtect"></param>
        void UpdateAccountsProtect( AccountsProtect accountProtect );

        /// <summary>
        /// 删除用户密保信息
        /// </summary>
        /// <param name="pid">密保ProtectID</param>
        void DeleteAccountsProtect( int pid );

        /// <summary>
        /// 删除限制字符信息
        /// </summary>
        /// <param name="sqlQuery"></param>
        void DeleteAccountsProtect( string sqlQuery );

        #endregion 用户密保信息管理

        #region 靓号信息

        /// <summary>
        /// 随机取出10个靓号
        /// </summary>
        /// <returns></returns>
        DataSet GetReserveIdentifierList();

        #endregion 靓号信息

        #region 限制字符管理

        /// <summary>
        /// 分页获取限制字符列表
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="condition">条件</param>
        /// <param name="orderby">排序</param>
        /// <returns></returns>
        PagerSet GetConfineContentList( int pageIndex, int pageSize, string condition, string orderby );

        /// <summary>
        /// 获取限制字符
        /// </summary>
        /// <param name="strString">限制字符表主健</param>
        /// <returns></returns>
        ConfineContent GetConfineContentByContentID( int contentID );

        /// <summary>
        /// 新增一条限制字符信息
        /// </summary>
        /// <param name="content"></param>
        void InsertConfineContent( ConfineContent content );

        /// <summary>
        ///  更新限制字符信息
        /// </summary>
        /// <param name="content"></param>
        void UpdateConfineContent( ConfineContent content );

        /// <summary>
        /// 删除限制字符信息
        /// </summary>
        /// <param name="strString"></param>
        void DeleteConfineContentByContentID( int contentID );

        /// <summary>
        /// 删除限制字符信息
        /// </summary>
        /// <param name="sqlQuery"></param>
        void DeleteConfineContent( string sqlQuery );

        #endregion 限制字符管理

        #region 限制地址管理

        /// <summary>
        /// 分页获取限制地址列表
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="condition">条件</param>
        /// <param name="orderby">排序</param>
        /// <returns></returns>
        PagerSet GetConfineAddressList( int pageIndex, int pageSize, string condition, string orderby );

        /// <summary>
        /// 获取限制地址
        /// </summary>
        /// <param name="strAddress">限制地址表主健</param>
        /// <returns></returns>
        ConfineAddress GetConfineAddressByAddress( string strAddress );

        /// <summary>
        /// 新增一条限制地址信息
        /// </summary>
        /// <param name="address"></param>
        void InsertConfineAddress( ConfineAddress address );

        /// <summary>
        ///  更新限制地址信息
        /// </summary>
        /// <param name="address"></param>
        void UpdateConfineAddress( ConfineAddress address );

        /// <summary>
        /// 删除限制地址信息
        /// </summary>
        /// <param name="strAddress"></param>
        void DeleteConfineAddressByAddress( string strAddress );

        /// <summary>
        /// 删除限制地址信息
        /// </summary>
        /// <param name="sqlQuery"></param>
        void DeleteConfineAddress( string sqlQuery );

        /// <summary>
        /// 获取IP注册前100名
        /// </summary>
        /// <returns></returns>
        DataSet GetIPRegisterTop100();

        /// <summary>
        /// 批量插入限制IP
        /// </summary>
        void BatchInsertConfineAddress( string ipList );

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
        PagerSet GetConfineMachineList( int pageIndex, int pageSize, string condition, string orderby );

        /// <summary>
        /// 获取限制机器码
        /// </summary>
        /// <param name="strSerial">限制机器码表主健</param>
        /// <returns></returns>
        ConfineMachine GetConfineMachineBySerial( string strSerial );

        /// <summary>
        /// 新增一条限制机器码信息
        /// </summary>
        /// <param name="machine"></param>
        void InsertConfineMachine( ConfineMachine machine );

        /// <summary>
        ///  更新限制机器码信息
        /// </summary>
        /// <param name="machine"></param>
        void UpdateConfineMachine( ConfineMachine machine );

        /// <summary>
        /// 删除限制机器码信息
        /// </summary>
        /// <param name="strSerial"></param>
        void DeleteConfineMachineBySerial( string strSerial );

        /// <summary>
        /// 删除限制机器码信息
        /// </summary>
        /// <param name="sqlQuery"></param>
        void DeleteConfineMachine( string sqlQuery );

        /// <summary>
        /// 机器码注册前100名
        /// </summary>
        /// <returns></returns>
        DataSet GetMachineRegisterTop100();

        /// <summary>
        /// 批量插入限制IP
        /// </summary>
        void BatchInsertConfineMachine( string machineList );

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
        PagerSet GetSystemStatusInfoList( int pageIndex, int pageSize, string condition, string orderby );

        /// <summary>
        /// 获取配置信息实体
        /// </summary>
        /// <param name="statusName"></param>
        /// <returns></returns>
        SystemStatusInfo GetSystemStatusInfo( string statusName );

        /// <summary>
        /// 更新配置信息
        /// </summary>
        /// <param name="systemStatusInfo"></param>
        void UpdateSystemStatusInfo( SystemStatusInfo systemStatusInfo );

        #endregion 系统参数配置

        #region 数据分析

        /// <summary>
        /// 获取每日注册玩家数
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        DataSet GetRegUserByDays( string startDate, string endDate );

        /// <summary>
        /// 获取每小时注册玩家数
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        DataSet GetRegUserByHours( string startDate, string endDate );

        /// <summary>
        /// 获取每个月注册玩家数
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        DataSet GetRegUserByMonth();

        /// <summary>
        /// 获取用户汇总统计
        /// </summary>
        /// <returns></returns>
        DataSet GetUsersStat();

        /// <summary>
        /// 获取用户数量
        /// </summary>
        /// <returns></returns>
        DataSet GetUsersNumber();

        #endregion 数据分析

        #region APP运营助手

        /// <summary>
        /// 登录统计
        /// </summary>
        /// <param name="accounts"></param>
        /// <param name="logonPass"></param>
        /// <param name="machineID"></param>
        /// <returns></returns>
        Message AppStatLogon(string accounts, string logonPass, string machineID);

        /// <summary>
        /// 用户活跃统计
        /// </summary>
        /// <param name="accounts"></param>
        /// <param name="logonPass"></param>
        /// <param name="machineID"></param>
        /// <returns></returns>
        Message AppStatUserActive(string accounts, string logonPass, string machineID);

        /// <summary>
        /// 用户会员统计
        /// </summary>
        /// <param name="accounts"></param>
        /// <param name="logonPass"></param>
        /// <param name="machineID"></param>
        /// <returns></returns>
        Message AppStatUserMember(string accounts, string logonPass, string machineID);

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
        Message AppGetLogonData(string accounts, string logonPass, int typeID, string machineID, int dateType, string startDate, string endDate);

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
        Message AppGetRegData(string accounts, string logonPass, int typeID, string machineID, int dateType, string startDate, string endDate);
        #endregion

        #region 输赢控制

        /// <summary>
        /// 读取输赢控制基础配置
        /// </summary>
        /// <returns></returns>
        ControlConfig GetControlConfig();

        /// <summary>
        /// 更新输赢控制基础配置
        /// </summary>
        void UpdateControlConfig(ControlConfig model);

        /// <summary>
        /// 删除黑白名单
        /// </summary>
        /// <param name="where"></param>
        void DeleteAccountsControl(string where);

        /// <summary>
        /// 添加黑白名单
        /// </summary>
        /// <param name="model"></param>
        void AddAccountsControl(AccountsControl model);

        /// <summary>
        /// 更新黑白名单
        /// </summary>
        /// <param name="model"></param>
        void UpdateAccountsControl(AccountsControl model);

         /// <summary>
        /// 查询黑白名单实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        AccountsControl GetAccountsControl(int id);
        
        #endregion

        #region 代理商管理

        /// <summary>
        /// 冻结玩家
        /// </summary>
        /// <param name="sqlQuery">条件</param>
        void DongjieAgent(string sqlQuery);

        /// <summary>
        /// 解冻玩家
        /// </summary>
        /// <param name="sqlQuery">条件</param>
        void JieDongAgent(string sqlQuery);

        /// <summary>
        /// 获取用户基本信息通过用户ID
        /// </summary>
        /// <param name="userID">用户UserID</param>
        /// <returns></returns>
        AccountsAgent GetAccountAgentByUserID(int userID);

        /// <summary>
        /// 获取代理信息通过域名
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        AccountsAgent GetAccountAgentByDomain(string domain);

        /// <summary>
        /// 添加代理信息
        /// </summary>
        /// <param name="agent"></param>
        /// <returns></returns>
        Message AddAgent(AccountsAgent agent);

        /// <summary>
        /// 更新代理基本信息
        /// </summary>
        /// <param name="agent"></param>
        /// <returns></returns>
        bool UpdateAgent(AccountsAgent agent);

        /// <summary>
        /// 获取代理下级玩家数量
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        int GetAgentChildCount(int userID);

        /// <summary>
        /// 获取代理游戏列表实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        AccountsAgentGame GetAccountsAgentGameInfo(int id);

        /// <summary>
        /// 新增代理游戏列表
        /// </summary>
        /// <param name="gameGameItem"></param>
        void InsertAccountsAgentGame(AccountsAgentGame model);

        /// <summary>
        /// 更新代理游戏列表
        /// </summary>
        /// <param name="gameGameItem"></param>
        void UpdateAccountsAgentGame(AccountsAgentGame model);

        /// <summary>
        /// 删除代理游戏列表
        /// </summary>
        /// <param name="sqlQuery"></param>
        void DeleteAccountsAgentGame(string sqlQuery);

        #endregion

        #region 会员属性

        /// <summary>
        /// 获取会员类型数据集
        /// </summary>
        /// <returns></returns>
        DataSet GetMemberPropertyList();

        /// <summary>
        /// 获取会员类型实体
        /// </summary>
        /// <param name="cardID"></param>
        /// <returns></returns>
        MemberProperty GetMemberProperty(int memberOrder);

        /// <summary>
        /// 更新会员类型
        /// </summary>
        /// <param name="awardType"></param>
        void UpdateMemberType(MemberProperty memberType);
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

        #endregion 公共
    }
}