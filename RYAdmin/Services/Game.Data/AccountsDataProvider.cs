using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Game.Entity.Accounts;
using Game.IData;
using Game.Kernel;

namespace Game.Data
{
    /// <summary>
    /// 帐号库数据层
    /// </summary>
    public class AccountsDataProvider : BaseDataProvider, IAccountsDataProvider
    {
        #region Fields

        private ITableProvider aideAccountsProvider;
        private ITableProvider aideIndividualDatumProvider;
        private ITableProvider aideAccountsProtectProvider;
        private ITableProvider aideConfineContentProvider;
        private ITableProvider aideConfineAddressProvider;
        private ITableProvider aideConfineMachineProvider;
        private ITableProvider aideSystemStatusInfoProvider;
        private ITableProvider aideAccountsControlProvider;
        private ITableProvider aideAccountsAgent;
        private ITableProvider aideMemberProperty;
        private ITableProvider aideAccountsAgentGame;

        #endregion Fields

        #region 构造方法

        /// <summary>
        /// 构造函数
        /// </summary>
        public AccountsDataProvider(string connString)
            : base(connString)
        {
            aideAccountsProvider = GetTableProvider(AccountsInfo.Tablename);
            aideIndividualDatumProvider = GetTableProvider(IndividualDatum.Tablename);
            aideAccountsProtectProvider = GetTableProvider(AccountsProtect.Tablename);
            aideConfineContentProvider = GetTableProvider(ConfineContent.Tablename);
            aideConfineAddressProvider = GetTableProvider(ConfineAddress.Tablename);
            aideConfineMachineProvider = GetTableProvider(ConfineMachine.Tablename);
            aideSystemStatusInfoProvider = GetTableProvider(SystemStatusInfo.Tablename);
            aideAccountsControlProvider = GetTableProvider(AccountsControl.Tablename);
            aideAccountsAgent = GetTableProvider(AccountsAgent.Tablename);
            aideMemberProperty = GetTableProvider(MemberProperty.Tablename);
            aideAccountsAgentGame = GetTableProvider(AccountsAgentGame.Tablename);
        }

        #endregion 构造方法

        #region 用户基本信息管理

        /// <summary>
        /// 分页获取玩家列表
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="condition">条件</param>
        /// <param name="orderby">排序</param>
        /// <returns></returns>
        public PagerSet GetAccountsList(int pageIndex, int pageSize, string condition, string orderby)
        {
            PagerParameters pagerPrams = new PagerParameters(AccountsInfo.Tablename, orderby, condition, pageIndex, pageSize);
            return GetPagerSet2(pagerPrams);
        }

        /// <summary>
        /// 获得用户名
        /// </summary>
        /// <param name="userID">用户UserID</param>
        /// <returns></returns>
        public string GetAccountByUserID(int userID)
        {
            AccountsInfo model = GetAccountInfoByUserID(userID);
            if(model != null)
                return model.Accounts;
            else
                return "";
        }

        /// <summary>
        /// 获得用户经验值
        /// </summary>
        /// <param name="userID">用户UserID</param>
        /// <returns></returns>
        public int GetExperienceByUserID(int userID)
        {
            AccountsInfo model = GetAccountInfoByUserID(userID);
            if(model != null)
                return model.Experience;
            else
                return 0;
        }

        /// <summary>
        /// 获取用户基本信息通过用户ID
        /// </summary>
        /// <param name="userID">用户UserID</param>
        /// <returns></returns>
        public AccountsInfo GetAccountInfoByUserID(int userID)
        {
            string sqlQuery = string.Format("(NOLOCK) WHERE UserID= N'{0}'", userID);
            AccountsInfo model = aideAccountsProvider.GetObject<AccountsInfo>(sqlQuery);
            if(model != null)
                return model;
            else
                return new AccountsInfo();
        }

        /// <summary>
        /// 获取用户基本信息通过帐号
        /// </summary>
        /// <param name="account">用户帐号</param>
        /// <returns></returns>
        public AccountsInfo GetAccountInfoByAccount(string account)
        {
            string sqlQuery = string.Format("SELECT * FROM AccountsInfo(NOLOCK) WHERE Accounts=@Accounts");

            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("Accounts", account));

            AccountsInfo model = Database.ExecuteObject<AccountsInfo>(sqlQuery, prams);
            if(model != null)
                return model;
            else
                return new AccountsInfo();
        }

        /// <summary>
        /// 获取用户基本信息通过帐号
        /// </summary>
        /// <param name="account">用户帐号</param>
        /// <returns></returns>
        public AccountsInfo GetAccountInfoByNickname(string nickname)
        {
            string sqlQuery = string.Format("SELECT * FROM AccountsInfo(NOLOCK) WHERE NickName=@NickName");

            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("NickName", nickname));

            AccountsInfo model = Database.ExecuteObject<AccountsInfo>(sqlQuery, prams);
            if(model != null)
                return model;
            else
                return new AccountsInfo();
        }

        /// <summary>
        /// 获取用户基本信息通过GameID
        /// </summary>
        /// <param name="account">用户帐号</param>
        /// <returns></returns>
        public AccountsInfo GetAccountInfoByGameID(int gameID)
        {
            string sqlQuery = string.Format("SELECT * FROM AccountsInfo(NOLOCK) WHERE GameID=@GameID");

            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("GameID", gameID));

            AccountsInfo model = Database.ExecuteObject<AccountsInfo>(sqlQuery, prams);
            if(model != null)
                return model;
            else
                return new AccountsInfo();
        }

        /// <summary>
        /// 冻结玩家
        /// </summary>
        /// <param name="sqlQuery">条件</param>
        public void DongjieAccount(string sqlQuery)
        {
            string sqlQueryAll = string.Format("UPDATE AccountsInfo SET Nullity=1 {0}", sqlQuery);
            Database.ExecuteNonQuery(sqlQueryAll);
        }

        /// <summary>
        /// 解冻玩家
        /// </summary>
        /// <param name="sqlQuery">条件</param>
        public void JieDongAccount(string sqlQuery)
        {
            string sqlQueryAll = string.Format("UPDATE AccountsInfo SET Nullity=0 {0}", sqlQuery);
            Database.ExecuteNonQuery(sqlQueryAll);
        }

        /// <summary>
        /// 设为机器人
        /// </summary>
        /// <param name="sqlQuery">条件</param>
        public void SettingAndroid(string sqlQuery)
        {
            string sqlQueryAll = string.Format("UPDATE AccountsInfo SET IsAndroid=1 {0};", sqlQuery);
            sqlQueryAll += string.Format("INSERT INTO AndroidLockInfo(UserID) SELECT UserID FROM AccountsInfo {0} AND UserID NOT IN(SELECT UserID FROM AndroidLockInfo {0})", sqlQuery);
            Database.ExecuteNonQuery(sqlQueryAll);
        }

        /// <summary>
        /// 取消机器人
        /// </summary>
        /// <param name="sqlQuery">条件</param>
        public void CancleAndroid(string sqlQuery)
        {
            string sqlQueryAll = string.Format("UPDATE AccountsInfo SET IsAndroid=0 {0};", sqlQuery);
            sqlQueryAll += string.Format("DELETE AndroidLockInfo {0}", sqlQuery);
            Database.ExecuteNonQuery(sqlQueryAll);
        }

        /// <summary>
        /// 添加玩家信息
        /// </summary>
        /// <param name="account">AccountsInfo实体</param>
        /// <param name="datum">IndividualDatum实体</param>
        /// <returns></returns>
        public Message AddAccount(AccountsInfo account, IndividualDatum datum)
        {
            var prams = new List<DbParameter>();

            prams.Add(Database.MakeInParam("strAccounts", account.Accounts));
            prams.Add(Database.MakeInParam("strNickName", account.NickName));
            prams.Add(Database.MakeInParam("strLogonPass", account.LogonPass));
            prams.Add(Database.MakeInParam("strInsurePass", account.InsurePass));
            prams.Add(Database.MakeInParam("strDynamicPass", account.DynamicPass));
            prams.Add(Database.MakeInParam("dwFaceID", account.FaceID));
            prams.Add(Database.MakeInParam("strUnderWrite", account.UnderWrite));
            prams.Add(Database.MakeInParam("strPassPortID", account.PassPortID));
            prams.Add(Database.MakeInParam("strCompellation", account.Compellation));

            prams.Add(Database.MakeInParam("dwExperience", account.Experience));
            prams.Add(Database.MakeInParam("dwPresent", account.Present));
            prams.Add(Database.MakeInParam("dwLoveLiness", account.LoveLiness));
            prams.Add(Database.MakeInParam("dwUserRight", account.UserRight));
            prams.Add(Database.MakeInParam("dwMasterRight", account.MasterRight));
            prams.Add(Database.MakeInParam("dwServiceRight", account.ServiceRight));
            prams.Add(Database.MakeInParam("dwMasterOrder", account.MasterOrder));

            prams.Add(Database.MakeInParam("dwMemberOrder", account.MemberOrder));
            prams.Add(Database.MakeInParam("dtMemberOverDate", account.MemberOverDate));
            prams.Add(Database.MakeInParam("dtMemberSwitchDate", account.MemberSwitchDate));
            prams.Add(Database.MakeInParam("dwGender", account.Gender));
            prams.Add(Database.MakeInParam("dwNullity", account.Nullity));

            prams.Add(Database.MakeInParam("dwStunDown", account.StunDown));
            prams.Add(Database.MakeInParam("dwMoorMachine", account.MoorMachine));
            prams.Add(Database.MakeInParam("strRegisterIP", account.RegisterIP));
            prams.Add(Database.MakeInParam("strRegisterMachine", account.RegisterMachine));
            prams.Add(Database.MakeInParam("IsAndroid", account.IsAndroid));

            prams.Add(Database.MakeInParam("strQQ", datum.QQ));
            prams.Add(Database.MakeInParam("strEMail", datum.EMail));
            prams.Add(Database.MakeInParam("strSeatPhone", datum.SeatPhone));
            prams.Add(Database.MakeInParam("strMobilePhone", datum.MobilePhone));

            prams.Add(Database.MakeInParam("strDwellingPlace", datum.DwellingPlace));
            prams.Add(Database.MakeInParam("strPostalCode", datum.PostalCode));
            prams.Add(Database.MakeInParam("strUserNote", datum.UserNote));
            prams.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            Message msg = MessageHelper.GetMessage(Database, "NET_PM_AddAccount", prams);
            return msg;
        }

        /// <summary>
        ///  更新用户基本信息
        /// </summary>
        /// <param name="account">AccountsInfo实体</param>
        /// <param name="account">管理员ID</param>
        /// <param name="clientIP">客户端IP</param>
        /// <returns>Message</returns>
        public Message UpdateAccount(AccountsInfo account, int masterID, string clientIP)
        {
            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("dwUserID", account.UserID));
            prams.Add(Database.MakeInParam("strAccounts", account.Accounts));
            prams.Add(Database.MakeInParam("strNickName", account.NickName));
            prams.Add(Database.MakeInParam("strLogonPass", account.LogonPass));
            prams.Add(Database.MakeInParam("strInsurePass", account.InsurePass));

            prams.Add(Database.MakeInParam("strUnderWrite", account.UnderWrite));
            prams.Add(Database.MakeInParam("dwExperience", account.Experience));
            prams.Add(Database.MakeInParam("dwPresent", account.Present));
            prams.Add(Database.MakeInParam("dwLoveLiness", account.LoveLiness));

            prams.Add(Database.MakeInParam("dwGender", account.Gender));
            prams.Add(Database.MakeInParam("dwFaceID", account.FaceID));
            prams.Add(Database.MakeInParam("dwCustomID", account.CustomID));
            prams.Add(Database.MakeInParam("dwStunDown", account.StunDown));
            prams.Add(Database.MakeInParam("dwNullity", account.Nullity));
            prams.Add(Database.MakeInParam("dwMoorMachine", account.MoorMachine));
            prams.Add(Database.MakeInParam("dwIsAndroid", account.IsAndroid));

            prams.Add(Database.MakeInParam("dwUserRight", account.UserRight));
            prams.Add(Database.MakeInParam("dwMasterOrder", account.MasterOrder));
            prams.Add(Database.MakeInParam("dwMasterRight", account.MasterRight));

            prams.Add(Database.MakeInParam("dwMasterID", masterID));
            prams.Add(Database.MakeInParam("strClientIP", clientIP));

            prams.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            return MessageHelper.GetMessage(Database, "NET_PM_UpdateAccountInfo", prams);
        }

        /// <summary>
        /// 给用户添加奖牌
        /// </summary>
        /// <param name="userId">用户标识</param>
        /// <param name="medal">奖牌数量</param>
        /// <returns>更新成功返回True 失败返回False</returns>
        public bool AddUserMedal(int userId, int medal)
        {
            string sqlQuery = "UPDATE AccountsInfo SET UserMedal=UserMedal+@UserMedal WHERE UserID=@UserID";
            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("UserMedal", medal));
            prams.Add(Database.MakeInParam("UserID", userId));
            if(Database.ExecuteNonQuery(CommandType.Text, sqlQuery, prams.ToArray()) > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 更新用户密码
        /// </summary>
        /// <param name="accountsInfo">用户实体</param>
        /// <returns>更新成功返回True 失败返回False</returns>
        public bool UpdateUserPassword(AccountsInfo accountsInfo)
        {
            StringBuilder sqlQuery = new StringBuilder("UPDATE AccountsInfo SET ");
            var prams = new List<DbParameter>();
            if(!string.IsNullOrEmpty(accountsInfo.LogonPass))
            {
                sqlQuery.Append(" LogonPass=@LogonPass");
                prams.Add(Database.MakeInParam("LogonPass", accountsInfo.LogonPass));
            }
            if(!string.IsNullOrEmpty(accountsInfo.InsurePass))
            {
                sqlQuery.Append(",InsurePass=@InsurePass ");
                prams.Add(Database.MakeInParam("InsurePass", accountsInfo.InsurePass));
            }
            sqlQuery.Append(" WHERE UserID=@UserID");
            prams.Add(Database.MakeInParam("UserID", accountsInfo.UserID));
            if(Database.ExecuteNonQuery(CommandType.Text, sqlQuery.ToString(), prams.ToArray()) > 0)
            {
                return true;
            }
            return false;
        }

        #endregion 用户基本信息管理

        #region 用户详细信息管理

        /// <summary>
        /// 获取用户详细信息
        /// </summary>
        /// <param name="userID">用户UserID</param>
        /// <returns></returns>
        public IndividualDatum GetAccountDetailByUserID(int userID)
        {
            string sqlQuery = string.Format("(NOLOCK) WHERE UserID= N'{0}'", userID);
            IndividualDatum model = aideIndividualDatumProvider.GetObject<IndividualDatum>(sqlQuery);
            return model;
        }

        /// <summary>
        /// 新增详细信息
        /// </summary>
        /// <param name="accountDetail"></param>
        /// <returns></returns>
        public void InsertAccountDetail(IndividualDatum accountDetail)
        {
            DataRow dr = aideIndividualDatumProvider.NewRow();

            dr[IndividualDatum._UserID] = accountDetail.UserID;
            dr[IndividualDatum._QQ] = accountDetail.QQ;
            dr[IndividualDatum._EMail] = accountDetail.EMail;
            dr[IndividualDatum._SeatPhone] = accountDetail.SeatPhone;
            dr[IndividualDatum._MobilePhone] = accountDetail.MobilePhone;
            dr[IndividualDatum._DwellingPlace] = accountDetail.DwellingPlace;
            dr[IndividualDatum._PostalCode] = accountDetail.PostalCode;
            dr[IndividualDatum._CollectDate] = accountDetail.CollectDate;
            dr[IndividualDatum._UserNote] = accountDetail.UserNote;

            aideIndividualDatumProvider.Insert(dr);
        }

        /// <summary>
        ///  更新用户详细信息
        /// </summary>
        /// <param name="account">IndividualDatum实体</param>
        public void UpdateAccountDetail(IndividualDatum accountDetail)
        {
            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.Append("UPDATE IndividualDatum SET ")
                    .Append("QQ=@QQ, ")
                    .Append("EMail=@EMail, ")
                    .Append("MobilePhone=@MobilePhone, ")
                    .Append("PostalCode=@PostalCode, ")
                    .Append("DwellingPlace=@DwellingPlace, ")
                    .Append("UserNote=@UserNote ")
                    .Append("WHERE UserID=@UserID");

            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("QQ", accountDetail.QQ));
            prams.Add(Database.MakeInParam("EMail", accountDetail.EMail));
            prams.Add(Database.MakeInParam("SeatPhone", accountDetail.SeatPhone));
            prams.Add(Database.MakeInParam("MobilePhone", accountDetail.MobilePhone));
            prams.Add(Database.MakeInParam("PostalCode", accountDetail.PostalCode));
            prams.Add(Database.MakeInParam("DwellingPlace", accountDetail.DwellingPlace));
            prams.Add(Database.MakeInParam("UserNote", accountDetail.UserNote));
            prams.Add(Database.MakeInParam("UserID", accountDetail.UserID));

            Database.ExecuteNonQuery(CommandType.Text, sqlQuery.ToString(), prams.ToArray());
        }

        #endregion 用户详细信息管理

        #region 用户密保信息管理

        /// <summary>
        /// 获取用户密保信息
        /// </summary>
        /// <param name="pid">密保ProtectID</param>
        /// <returns></returns>
        public AccountsProtect GetAccountsProtectByPID(int pid)
        {
            string sqlQuery = string.Format("(NOLOCK) WHERE ProtectID= N'{0}'", pid);
            AccountsProtect model = aideAccountsProtectProvider.GetObject<AccountsProtect>(sqlQuery);
            return model;
        }

        /// <summary>
        /// 获取用户密保信息
        /// </summary>
        /// <param name="uid">用户标识</param>
        /// <returns></returns>
        public AccountsProtect GetAccountsProtectByUserID(int uid)
        {
            string sqlQuery = string.Format("(NOLOCK) WHERE UserID= N'{0}'", uid);
            AccountsProtect model = aideAccountsProtectProvider.GetObject<AccountsProtect>(sqlQuery);
            if(model != null)
                return model;
            else
                return new AccountsProtect();
        }

        /// <summary>
        /// 更新用户密保信息
        /// </summary>
        /// <param name="accountProtect"></param>
        public void UpdateAccountsProtect(AccountsProtect accountProtect)
        {
            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.Append("UPDATE AccountsProtect SET ")
                    .Append("Question1=@Question1, ")
                    .Append("Response1=@Response1, ")
                    .Append("Question2=@Question2, ")
                    .Append("Response2=@Response2, ")
                    .Append("Question3=@Question3, ")
                    .Append("Response3=@Response3, ")
                    .Append("SafeEmail=@SafeEmail, ")
                    .Append("ModifyDate=getdate()  ")
                    .Append("WHERE ProtectID=@ProtectID");

            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("Question1", accountProtect.Question1));
            prams.Add(Database.MakeInParam("Response1", accountProtect.Response1));
            prams.Add(Database.MakeInParam("Question2", accountProtect.Question2));
            prams.Add(Database.MakeInParam("Response2", accountProtect.Response2));
            prams.Add(Database.MakeInParam("Question3", accountProtect.Question3));
            prams.Add(Database.MakeInParam("Response3", accountProtect.Response3));
            prams.Add(Database.MakeInParam("SafeEmail", accountProtect.SafeEmail));
            prams.Add(Database.MakeInParam("ProtectID", accountProtect.ProtectID));

            Database.ExecuteNonQuery(CommandType.Text, sqlQuery.ToString(), prams.ToArray());
        }

        /// <summary>
        /// 删除用户密保信息
        /// </summary>
        /// <param name="pid">密保ProtectID</param>
        public void DeleteAccountsProtect(int pid)
        {
            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.AppendFormat("DELETE AccountsProtect WHERE ProtectID={0}; ", pid);
            sqlQuery.AppendFormat("UPDATE AccountsInfo SET ProtectID=0 WHERE ProtectID={0}", pid);
            Database.ExecuteNonQuery(CommandType.Text, sqlQuery.ToString());
        }

        /// <summary>
        /// 删除限制字符信息
        /// </summary>
        /// <param name="sqlQuery"></param>
        public void DeleteAccountsProtect(string sqlQuery)
        {
            aideAccountsProtectProvider.Delete(sqlQuery);
        }

        #endregion 用户密保信息管理

        #region 自定义头像

        /// <summary>
        /// 获取用户自定义头像列表
        /// </summary>
        /// <param name="userId">用户标识</param>
        /// <returns>数据集</returns>
        public DataSet GetAccountsFaceList(int userId)
        {
            string sqlQuery = "SELECT * FROM AccountsFace WHERE UserID=@UserID";
            List<DbParameter> prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("UserID", userId));
            return Database.ExecuteDataset(CommandType.Text, sqlQuery, prams.ToArray());
        }

        /// <summary>
        /// 获取自定义头像实体
        /// </summary>
        /// <param name="customId">自定义头像</param>
        /// <returns>自定义头像实体</returns>
        public AccountsFace GetAccountsFace(int customId)
        {
            string sqlQuery = "SELECT * FROM AccountsFace WHERE ID=@ID";
            List<DbParameter> prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("ID", customId));
            AccountsFace model = Database.ExecuteObject<AccountsFace>(sqlQuery, prams);
            return model;
        }

        #endregion 自定义头像

        #region 靓号信息

        /// <summary>
        /// 随机取出10个靓号
        /// </summary>
        /// <returns></returns>
        public DataSet GetReserveIdentifierList()
        {
            string sqlCommandText = @"SELECT TOP 10 GameID FROM ReserveIdentifier
                                        WHERE Distribute=0 ORDER BY NEWID()";
            DataSet ds = Database.ExecuteDataset(CommandType.Text, sqlCommandText);
            return ds;
        }

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
        public PagerSet GetConfineContentList(int pageIndex, int pageSize, string condition, string orderby)
        {
            PagerParameters pagerPrams = new PagerParameters(ConfineContent.Tablename, orderby, condition, pageIndex, pageSize);
            return GetPagerSet2(pagerPrams);
        }

        /// <summary>
        /// 获取限制字符
        /// </summary>
        /// <param name="strString">限制字符表主健</param>
        /// <returns></returns>
        public ConfineContent GetConfineContentByContentID(int contentID)
        {
            string sqlQuery = string.Format("(NOLOCK) WHERE ContentID= N'{0}'", contentID);
            ConfineContent model = aideConfineContentProvider.GetObject<ConfineContent>(sqlQuery);
            if(model != null)
                return model;
            else
                return null;
        }

        /// <summary>
        /// 新增一条限制字符信息
        /// </summary>
        /// <param name="content"></param>
        public void InsertConfineContent(ConfineContent content)
        {
            DataRow dr = aideConfineContentProvider.NewRow();

            dr[ConfineContent._String] = content.String;
            if(!content.EnjoinOverDate.Equals(Convert.ToDateTime("1900-01-01")))
                dr[ConfineContent._EnjoinOverDate] = content.EnjoinOverDate;
            dr[ConfineContent._CollectDate] = DateTime.Now;
            aideConfineContentProvider.Insert(dr);
        }

        /// <summary>
        ///  更新限制字符信息
        /// </summary>
        /// <param name="content"></param>
        public void UpdateConfineContent(ConfineContent content)
        {
            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.Append("UPDATE ConfineContent SET ")
                    .Append("EnjoinOverDate=@EnjoinOverDate ")
                    .Append("WHERE String=@String");

            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("EnjoinOverDate", content.EnjoinOverDate.Equals(Convert.ToDateTime("1900-01-01")) ? null : content.EnjoinOverDate.ToString()));
            prams.Add(Database.MakeInParam("String", content.String));

            Database.ExecuteNonQuery(CommandType.Text, sqlQuery.ToString(), prams.ToArray());
        }

        /// <summary>
        /// 删除限制字符信息
        /// </summary>
        /// <param name="strString"></param>
        public void DeleteConfineContentByContentID(int contentID)
        {
            string sqlQuery = string.Format("WHERE ContentID={0}", contentID);
            aideConfineContentProvider.Delete(sqlQuery);
        }

        /// <summary>
        /// 删除限制字符信息
        /// </summary>
        /// <param name="sqlQuery"></param>
        public void DeleteConfineContent(string sqlQuery)
        {
            aideConfineContentProvider.Delete(sqlQuery);
        }

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
        public PagerSet GetConfineAddressList(int pageIndex, int pageSize, string condition, string orderby)
        {
            PagerParameters pagerPrams = new PagerParameters(ConfineAddress.Tablename, orderby, condition, pageIndex, pageSize);
            return GetPagerSet2(pagerPrams);
        }

        /// <summary>
        /// 获取限制地址
        /// </summary>
        /// <param name="strAddress">限制地址表主健</param>
        /// <returns></returns>
        public ConfineAddress GetConfineAddressByAddress(string strAddress)
        {
            string sqlQuery = string.Format("SELECT * FROM ConfineAddress(NOLOCK) WHERE AddrString=@AddrString");

            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("AddrString", strAddress));

            ConfineAddress model = Database.ExecuteObject<ConfineAddress>(sqlQuery, prams);
            return model;
        }

        /// <summary>
        /// 新增一条限制地址信息
        /// </summary>
        /// <param name="address"></param>
        public void InsertConfineAddress(ConfineAddress address)
        {
            DataRow dr = aideConfineAddressProvider.NewRow();

            dr[ConfineAddress._AddrString] = address.AddrString;
            dr[ConfineAddress._EnjoinLogon] = address.EnjoinLogon;
            dr[ConfineAddress._EnjoinRegister] = address.EnjoinRegister;
            if(!address.EnjoinOverDate.Equals(Convert.ToDateTime("1900-01-01")))
                dr[ConfineContent._EnjoinOverDate] = address.EnjoinOverDate;
            dr[ConfineAddress._CollectNote] = address.CollectNote;
            dr[ConfineAddress._CollectDate] = DateTime.Now;
            aideConfineAddressProvider.Insert(dr);
        }

        /// <summary>
        ///  更新限制地址信息
        /// </summary>
        /// <param name="address"></param>
        public void UpdateConfineAddress(ConfineAddress address)
        {
            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.Append("UPDATE ConfineAddress SET ")
                .Append("EnjoinLogon=@EnjoinLogon, ")
                .Append("EnjoinRegister=@EnjoinRegister, ")
                .Append("EnjoinOverDate=@EnjoinOverDate, ")
                .Append("CollectNote=@CollectNote ")
                .Append("WHERE AddrString=@AddrString");

            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("EnjoinLogon", address.EnjoinLogon));
            prams.Add(Database.MakeInParam("EnjoinRegister", address.EnjoinRegister));
            prams.Add(Database.MakeInParam("EnjoinOverDate", address.EnjoinOverDate.Equals(Convert.ToDateTime("1900-01-01")) ? null : address.EnjoinOverDate.ToString()));
            prams.Add(Database.MakeInParam("CollectNote", address.CollectNote));
            prams.Add(Database.MakeInParam("AddrString", address.AddrString));

            Database.ExecuteNonQuery(CommandType.Text, sqlQuery.ToString(), prams.ToArray());
        }

        /// <summary>
        /// 删除限制地址信息
        /// </summary>
        /// <param name="strAddress"></param>
        public void DeleteConfineAddressByAddress(string strAddress)
        {
            string sqlQuery = string.Format("WHERE AddrString='{0}'", strAddress);
            aideConfineAddressProvider.Delete(sqlQuery);
        }

        /// <summary>
        /// 删除限制地址信息
        /// </summary>
        /// <param name="sqlQuery"></param>
        public void DeleteConfineAddress(string sqlQuery)
        {
            aideConfineAddressProvider.Delete(sqlQuery);
        }

        /// <summary>
        /// 获取IP注册前100名
        /// </summary>
        /// <returns></returns>
        public DataSet GetIPRegisterTop100()
        {
            return Database.ExecuteDataset(CommandType.StoredProcedure, "WSP_PM_GetIPRegisterTop100");
        }

        /// <summary>
        /// 批量插入限制IP
        /// </summary>
        public void BatchInsertConfineAddress(string ipList)
        {
            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("strIpList", ipList));
            Database.ExecuteNonQuery(CommandType.StoredProcedure, "NET_PM_BatchInsertConfineAddress", prams.ToArray());
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
        public PagerSet GetConfineMachineList(int pageIndex, int pageSize, string condition, string orderby)
        {
            PagerParameters pagerPrams = new PagerParameters(ConfineMachine.Tablename, orderby, condition, pageIndex, pageSize);
            return GetPagerSet2(pagerPrams);
        }

        /// <summary>
        /// 获取限制机器码
        /// </summary>
        /// <param name="strSerial">限制机器码表主健</param>
        /// <returns></returns>
        public ConfineMachine GetConfineMachineBySerial(string strSerial)
        {
            string sqlQuery = string.Format("SELECT * FROM ConfineMachine(NOLOCK) WHERE MachineSerial=@MachineSerial");
            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("MachineSerial", strSerial));
            ConfineMachine model = Database.ExecuteObject<ConfineMachine>(sqlQuery, prams);
            return model;
        }

        /// <summary>
        /// 新增一条限制机器码信息
        /// </summary>
        /// <param name="machine"></param>
        public void InsertConfineMachine(ConfineMachine machine)
        {
            DataRow dr = aideConfineMachineProvider.NewRow();

            dr[ConfineMachine._MachineSerial] = machine.MachineSerial;
            dr[ConfineMachine._EnjoinLogon] = machine.EnjoinLogon;
            dr[ConfineMachine._EnjoinRegister] = machine.EnjoinRegister;
            if(!machine.EnjoinOverDate.Equals(Convert.ToDateTime("1900-01-01")))
                dr[ConfineContent._EnjoinOverDate] = machine.EnjoinOverDate;
            dr[ConfineMachine._CollectNote] = machine.CollectNote;
            dr[ConfineMachine._CollectDate] = DateTime.Now;
            aideConfineMachineProvider.Insert(dr);
        }

        /// <summary>
        ///  更新限制机器码信息
        /// </summary>
        /// <param name="machine"></param>
        public void UpdateConfineMachine(ConfineMachine machine)
        {
            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.Append("UPDATE ConfineMachine SET ")
                .Append("EnjoinLogon=@EnjoinLogon, ")
                .Append("EnjoinRegister=@EnjoinRegister, ")
                .Append("EnjoinOverDate=@EnjoinOverDate, ")
                .Append("CollectNote=@CollectNote ")
                .Append("WHERE MachineSerial=@MachineSerial");

            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("EnjoinLogon", machine.EnjoinLogon));
            prams.Add(Database.MakeInParam("EnjoinRegister", machine.EnjoinRegister));
            prams.Add(Database.MakeInParam("EnjoinOverDate", machine.EnjoinOverDate.Equals(Convert.ToDateTime("1900-01-01")) ? null : machine.EnjoinOverDate.ToString()));
            prams.Add(Database.MakeInParam("CollectNote", machine.CollectNote));
            prams.Add(Database.MakeInParam("MachineSerial", machine.MachineSerial));

            Database.ExecuteNonQuery(CommandType.Text, sqlQuery.ToString(), prams.ToArray());
        }

        /// <summary>
        /// 删除限制机器码信息
        /// </summary>
        /// <param name="strSerial"></param>
        public void DeleteConfineMachineBySerial(string strSerial)
        {
            string sqlQuery = string.Format("WHERE MachineSerial='{0}'", strSerial);
            aideConfineMachineProvider.Delete(sqlQuery);
        }

        /// <summary>
        /// 删除限制机器码信息
        /// </summary>
        /// <param name="sqlQuery"></param>
        public void DeleteConfineMachine(string sqlQuery)
        {
            aideConfineMachineProvider.Delete(sqlQuery);
        }

        /// <summary>
        /// 机器码注册前100名
        /// </summary>
        /// <returns></returns>
        public DataSet GetMachineRegisterTop100()
        {
            return Database.ExecuteDataset(CommandType.StoredProcedure, "WSP_PM_GetMachineRegisterTop100");
        }

        /// <summary>
        /// 批量插入限制机器码
        /// </summary>
        public void BatchInsertConfineMachine(string machineList)
        {
            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("strMachineList", machineList));
            Database.ExecuteNonQuery(CommandType.StoredProcedure, "NET_PM_BatchInsertConfineMachine", prams.ToArray());
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
        public PagerSet GetSystemStatusInfoList(int pageIndex, int pageSize, string condition, string orderby)
        {
            PagerParameters pagerPrams = new PagerParameters(SystemStatusInfo.Tablename, orderby, condition, pageIndex, pageSize);

            return GetPagerSet2(pagerPrams);
        }

        /// <summary>
        /// 获取配置信息实体
        /// </summary>
        /// <param name="statusName"></param>
        /// <returns></returns>
        public SystemStatusInfo GetSystemStatusInfo(string statusName)
        {
            string sqlQuery = string.Format("(NOLOCK) WHERE StatusName= '{0}'", statusName);
            SystemStatusInfo systemStatusInfo = aideSystemStatusInfoProvider.GetObject<SystemStatusInfo>(sqlQuery);
            return systemStatusInfo;
        }

        /// <summary>
        /// 更新配置信息
        /// </summary>
        /// <param name="systemStatusInfo"></param>
        public void UpdateSystemStatusInfo(SystemStatusInfo systemStatusInfo)
        {
            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.Append("UPDATE SystemStatusInfo SET ")
                    .Append("StatusValue=@StatusValue, ")
                    .Append("StatusString=@StatusString, ")
                    .Append("StatusTip=@StatusTip, ")
                    .Append("StatusDescription=@StatusDescription ")
                    .Append("WHERE StatusName=@StatusName");

            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("StatusValue", systemStatusInfo.StatusValue));
            prams.Add(Database.MakeInParam("StatusString", systemStatusInfo.StatusString));
            prams.Add(Database.MakeInParam("StatusTip", systemStatusInfo.StatusTip));
            prams.Add(Database.MakeInParam("StatusDescription", systemStatusInfo.StatusDescription));
            prams.Add(Database.MakeInParam("StatusName", systemStatusInfo.StatusName));

            Database.ExecuteNonQuery(CommandType.Text, sqlQuery.ToString(), prams.ToArray());
        }

        #endregion 系统参数配置

        #region 数据分析

        /// <summary>
        /// 获取每日注册玩家数
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public DataSet GetRegUserByDays(string startDate, string endDate)
        {
            string sql = string.Empty;
            sql = "SELECT DateID,WebRegisterSuccess+GameRegisterSuccess AS RegisterCount FROM SystemStreamInfo";
            sql += " WHERE CollectDate>=@StartDate AND CollectDate<=@EndDate";
            sql += " ORDER BY DateID ASC";

            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("StartDate", startDate));
            prams.Add(Database.MakeInParam("EndDate", endDate + " 23:59:59"));

            DataSet ds = Database.ExecuteDataset(CommandType.Text, sql, prams.ToArray());
            return ds;
        }

        /// <summary>
        /// 获取每小时注册玩家数
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public DataSet GetRegUserByHours(string startDate, string endDate)
        {
            string sql = "SELECT COUNT(RegisterDate) AS RegisterCount,DATEPART(hh,RegisterDate) AS StatDate FROM AccountsInfo(NOLOCK)";
            sql += " WHERE RegisterDate>=@StartDate AND RegisterDate<=@EndDate";
            sql += " GROUP BY DATEPART(hh,RegisterDate) ORDER BY StatDate ASC";

            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("StartDate", startDate));
            prams.Add(Database.MakeInParam("EndDate", endDate));

            DataSet ds = Database.ExecuteDataset(CommandType.Text, sql, prams.ToArray());
            return ds;
        }

        /// <summary>
        /// 获取每个月注册玩家数
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public DataSet GetRegUserByMonth()
        {
            string sql = "SELECT SUM(WebRegisterSuccess+GameRegisterSuccess) AS RegisterCount,CONVERT(char(7),CollectDate,120) AS StatDate FROM SystemStreamInfo";
            sql += " GROUP BY CONVERT(char(7),CollectDate,120) ORDER BY StatDate ASC";
            DataSet ds = Database.ExecuteDataset(sql);
            return ds;
        }

        /// <summary>
        /// 获取用户汇总统计
        /// </summary>
        /// <returns></returns>
        public DataSet GetUsersStat()
        {
            DataSet ds = Database.ExecuteDataset(CommandType.StoredProcedure, "NET_PM_AnalUserStat", null);
            return ds;
        }

        /// <summary>
        /// 获取用户数量
        /// </summary>
        /// <returns></returns>
        public DataSet GetUsersNumber()
        {
            DataSet ds = Database.ExecuteDataset(CommandType.StoredProcedure, "NET_PM_UsersNumberStat", null);
            return ds;
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
            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("strAccounts", accounts));
            prams.Add(Database.MakeInParam("strPassword", logonPass));
            prams.Add(Database.MakeInParam("strMachineSerial", machineID));
            prams.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            Message msg = MessageHelper.GetMessageForDataSet(Database, "APP_PM_StatLogin", prams);
            return msg;
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
            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("strAccounts", accounts));
            prams.Add(Database.MakeInParam("strPassword", logonPass));
            prams.Add(Database.MakeInParam("strMachineSerial", machineID));
            prams.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            Message msg = MessageHelper.GetMessageForDataSet(Database, "APP_PM_StatUserActive", prams);
            return msg;
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
            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("strAccounts", accounts));
            prams.Add(Database.MakeInParam("strPassword", logonPass));
            prams.Add(Database.MakeInParam("strMachineSerial", machineID));
            prams.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            Message msg = MessageHelper.GetMessageForDataSet(Database, "APP_PM_StatUserMember", prams);
            return msg;
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
            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("strAccounts", accounts));
            prams.Add(Database.MakeInParam("strPassword", logonPass));
            prams.Add(Database.MakeInParam("strMachineSerial", machineID));
            prams.Add(Database.MakeInParam("dwTypeID", typeID));
            prams.Add(Database.MakeInParam("dwDateType", dateType));
            prams.Add(Database.MakeInParam("strStartDate", startDate));
            prams.Add(Database.MakeInParam("strEndDate", endDate));
            prams.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            Message msg = MessageHelper.GetMessageForDataSet(Database, "APP_PM_GetLogonData", prams);
            return msg;
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
            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("strAccounts", accounts));
            prams.Add(Database.MakeInParam("strPassword", logonPass));
            prams.Add(Database.MakeInParam("strMachineSerial", machineID));
            prams.Add(Database.MakeInParam("dwTypeID", typeID));
            prams.Add(Database.MakeInParam("dwDateType", dateType));
            prams.Add(Database.MakeInParam("strStartDate", startDate));
            prams.Add(Database.MakeInParam("strEndDate", endDate));
            prams.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            Message msg = MessageHelper.GetMessageForDataSet(Database, "APP_PM_GetRegData", prams);
            return msg;
        }
        #endregion

        #region 输赢控制

        /// <summary>
        /// 查询输赢控制基础配置
        /// </summary>
        /// <returns></returns>
        public ControlConfig GetControlConfig()
        {
            string sql = "SELECT * FROM ControlConfig";
            return Database.ExecuteObject<ControlConfig>(sql);
        }

        /// <summary>
        /// 更新输赢控制基础配置
        /// </summary>
        public void UpdateControlConfig(ControlConfig model)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("UPDATE ControlConfig SET AutoControlEnable=@AutoControlEnable,");
            sql.Append("JoinBlackWinScore=@JoinBlackWinScore,");
            sql.Append("JoinWhiteLoseScore=@JoinWhiteLoseScore,");
            sql.Append("BlackControlType=@BlackControlType,");
            sql.Append("BSustainedTimeCount=@BSustainedTimeCount,");
            sql.Append("QuitBlackLoseScore=@QuitBlackLoseScore,");
            sql.Append("WhiteControlType=@WhiteControlType,");
            sql.Append("WSustainedTimeCount=@WSustainedTimeCount,");
            sql.Append("QuitWhiteWinScore=@QuitWhiteWinScore,");
            sql.Append("BlackWinRate=@BlackWinRate,");
            sql.Append("WhiteWinRate=@WhiteWinRate ");

            sql.Append("IF @@ROWCOUNT=0 BEGIN ");
            sql.Append("INSERT INTO ControlConfig ");
            sql.Append("VALUES(@AutoControlEnable,@JoinBlackWinScore,@JoinWhiteLoseScore,@BlackControlType,@BSustainedTimeCount,");
            sql.Append("@QuitBlackLoseScore,@WhiteControlType,@WSustainedTimeCount,@QuitWhiteWinScore,@BlackWinRate,@WhiteWinRate) ");
            sql.Append("END");


            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("AutoControlEnable", model.AutoControlEnable));
            prams.Add(Database.MakeInParam("JoinBlackWinScore", model.JoinBlackWinScore));
            prams.Add(Database.MakeInParam("JoinWhiteLoseScore", model.JoinWhiteLoseScore));
            prams.Add(Database.MakeInParam("BlackControlType", model.BlackControlType));
            prams.Add(Database.MakeInParam("BSustainedTimeCount", model.BSustainedTimeCount));
            prams.Add(Database.MakeInParam("QuitBlackLoseScore", model.QuitBlackLoseScore));
            prams.Add(Database.MakeInParam("WhiteControlType", model.WhiteControlType));
            prams.Add(Database.MakeInParam("WSustainedTimeCount", model.WSustainedTimeCount));
            prams.Add(Database.MakeInParam("QuitWhiteWinScore", model.QuitWhiteWinScore));
            prams.Add(Database.MakeInParam("BlackWinRate", model.BlackWinRate));
            prams.Add(Database.MakeInParam("WhiteWinRate", model.WhiteWinRate));

            Database.ExecuteNonQuery(CommandType.Text, sql.ToString(), prams.ToArray());
        }

        /// <summary>
        /// 删除黑白名单
        /// </summary>
        /// <param name="where"></param>
        public void DeleteAccountsControl(string where)
        {
            aideAccountsControlProvider.Delete(where);
        }

        /// <summary>
        /// 添加黑白名单
        /// </summary>
        /// <param name="model"></param>
        public void AddAccountsControl(AccountsControl model)
        {
            DataRow dr = aideAccountsControlProvider.NewRow();

            dr[AccountsControl._UserID] = model.UserID;
            dr[AccountsControl._Accounts] = model.Accounts;
            dr[AccountsControl._ActiveDateTime] = model.ActiveDateTime;
            dr[AccountsControl._ControlStatus] = model.ControlStatus;
            dr[AccountsControl._ControlType] = model.ControlType;
            dr[AccountsControl._ChangeScore] = model.ChangeScore;
            dr[AccountsControl._SustainedTimeCount] = model.SustainedTimeCount;
            dr[AccountsControl._WinRate] = model.WinRate;
            dr[AccountsControl._ConsumeTimeCount] = 0;
            dr[AccountsControl._WinScore] = 0;
            dr[AccountsControl._LoseScore] = 0;

            aideAccountsControlProvider.Insert(dr);
        }

        /// <summary>
        /// 更新黑白名单
        /// </summary>
        /// <param name="model"></param>
        public void UpdateAccountsControl(AccountsControl model)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("UPDATE AccountsControl SET ");
            sql.Append("ActiveDateTime=@ActiveDateTime,");
            sql.Append("ControlStatus=@ControlStatus,");
            sql.Append("ControlType=@ControlType,");
            sql.Append("ChangeScore=@ChangeScore,");
            sql.Append("SustainedTimeCount=@SustainedTimeCount,");
            sql.Append("WinRate=@WinRate ");
            sql.Append("WHERE UserID=@UserID");

            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("ActiveDateTime", model.ActiveDateTime));
            prams.Add(Database.MakeInParam("ControlStatus", model.ControlStatus));
            prams.Add(Database.MakeInParam("ControlType", model.ControlType));
            prams.Add(Database.MakeInParam("ChangeScore", model.ChangeScore));
            prams.Add(Database.MakeInParam("SustainedTimeCount", model.SustainedTimeCount));
            prams.Add(Database.MakeInParam("WinRate", model.WinRate));
            prams.Add(Database.MakeInParam("UserID", model.UserID));

            Database.ExecuteNonQuery(CommandType.Text, sql.ToString(), prams.ToArray());
        }

        /// <summary>
        /// 查询黑白名单实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AccountsControl GetAccountsControl(int id)
        {
            string sqlQuery = string.Format(" WHERE UserID={0}", id);
            AccountsControl model = aideAccountsControlProvider.GetObject<AccountsControl>(sqlQuery);
            return model;
        }
        #endregion

        #region 代理商管理

        /// <summary>
        /// 冻结玩家
        /// </summary>
        /// <param name="sqlQuery">条件</param>
        public void DongjieAgent(string sqlQuery)
        {
            string sqlQueryAll = string.Format("UPDATE AccountsAgent SET Nullity=1 {0}", sqlQuery);
            Database.ExecuteNonQuery(sqlQueryAll);
        }

        /// <summary>
        /// 解冻玩家
        /// </summary>
        /// <param name="sqlQuery">条件</param>
        public void JieDongAgent(string sqlQuery)
        {
            string sqlQueryAll = string.Format("UPDATE AccountsAgent SET Nullity=0 {0}", sqlQuery);
            Database.ExecuteNonQuery(sqlQueryAll);
        }

        /// <summary>
        /// 获取用户基本信息通过用户ID
        /// </summary>
        /// <param name="userID">用户UserID</param>
        /// <returns></returns>
        public AccountsAgent GetAccountAgentByUserID(int userID)
        {
            string sqlQuery = string.Format("(NOLOCK) WHERE UserID= N'{0}'", userID);
            AccountsAgent model = aideAccountsAgent.GetObject<AccountsAgent>(sqlQuery);
            if (model != null)
                return model;
            else
                return new AccountsAgent();
        }

        /// <summary>
        /// 获取代理信息通过域名
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public AccountsAgent GetAccountAgentByDomain(string domain)
        {
            string sqlQuery = string.Format("(NOLOCK) WHERE Domain= N'{0}'", domain);
            AccountsAgent model = aideAccountsAgent.GetObject<AccountsAgent>(sqlQuery);
            return model;
        }

        /// <summary>
        /// 添加代理信息
        /// </summary>
        /// <param name="agent"></param>
        /// <returns></returns>
        public Message AddAgent(AccountsAgent agent)
        {
            var prams = new List<DbParameter>();

            prams.Add(Database.MakeInParam("dwUserID", agent.UserID));
            prams.Add(Database.MakeInParam("strCompellation", agent.Compellation));
            prams.Add(Database.MakeInParam("strDomain", agent.Domain));
            prams.Add(Database.MakeInParam("dwAgentType", agent.AgentType));
            prams.Add(Database.MakeInParam("dcAgentScale", agent.AgentScale));
            prams.Add(Database.MakeInParam("dwPayBackScore", agent.PayBackScore));
            prams.Add(Database.MakeInParam("dcPayBackScale", agent.PayBackScale));
            prams.Add(Database.MakeInParam("strMobilePhone", agent.MobilePhone));
            prams.Add(Database.MakeInParam("strEMail", agent.EMail));
            prams.Add(Database.MakeInParam("strDwellingPlace", agent.DwellingPlace));
            prams.Add(Database.MakeInParam("dwNullity", agent.Nullity));
            prams.Add(Database.MakeInParam("strAgentNote", agent.AgentNote));

            prams.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            Message msg = MessageHelper.GetMessage(Database, "NET_PM_AddAgent", prams);
            return msg;
        }

        /// <summary>
        /// 更新代理基本信息
        /// </summary>
        /// <param name="agent"></param>
        /// <returns></returns>
        public bool UpdateAgent(AccountsAgent agent)
        {
            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.Append("UPDATE AccountsAgent SET ")
                    .Append("Compellation=@Compellation, ")
                    .Append("Domain=@Domain, ")
                    .Append("AgentType=@AgentType, ")
                    .Append("AgentScale=@AgentScale, ")
                    .Append("PayBackScore=@PayBackScore, ")
                    .Append("PayBackScale=@PayBackScale, ")
                    .Append("MobilePhone=@MobilePhone, ")
                    .Append("EMail=@EMail, ")
                    .Append("DwellingPlace=@DwellingPlace, ")
                    .Append("AgentNote=@AgentNote ")
                    .Append("WHERE UserID=@UserID");

            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("UserID", agent.UserID));
            prams.Add(Database.MakeInParam("Compellation", agent.Compellation));
            prams.Add(Database.MakeInParam("Domain", agent.Domain));
            prams.Add(Database.MakeInParam("AgentType", agent.AgentType));
            prams.Add(Database.MakeInParam("AgentScale", agent.AgentScale));
            prams.Add(Database.MakeInParam("PayBackScore", agent.PayBackScore));
            prams.Add(Database.MakeInParam("PayBackScale", agent.PayBackScale));
            prams.Add(Database.MakeInParam("MobilePhone", agent.MobilePhone));
            prams.Add(Database.MakeInParam("EMail", agent.EMail));
            prams.Add(Database.MakeInParam("DwellingPlace", agent.DwellingPlace));
            prams.Add(Database.MakeInParam("AgentNote", agent.AgentNote));

            if (Database.ExecuteNonQuery(CommandType.Text, sqlQuery.ToString(), prams.ToArray()) > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 获取代理下级玩家数量
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public int GetAgentChildCount(int userID)
        {
            string sqlQuery = string.Format("SELECT COUNT(UserID) FROM AccountsInfo WHERE SpreaderID= {0}", userID);
            object obj = Database.ExecuteScalar(CommandType.Text, sqlQuery);
            if (obj == null || obj.ToString().Length <= 0)
                return 0;
            return Convert.ToInt32(obj);
        }

        /// <summary>
        /// 获取代理游戏列表实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AccountsAgentGame GetAccountsAgentGameInfo(int id)
        {
            string sqlQuery = string.Format("(NOLOCK) WHERE ID= {0}", id);
            AccountsAgentGame model = aideAccountsAgentGame.GetObject<AccountsAgentGame>(sqlQuery);
            return model;
        }

        /// <summary>
        /// 新增代理游戏列表
        /// </summary>
        /// <param name="gameGameItem"></param>
        public void InsertAccountsAgentGame(AccountsAgentGame model)
        {
            DataRow dr = aideAccountsAgentGame.NewRow();

            dr[AccountsAgentGame._AgentID] = model.AgentID;
            dr[AccountsAgentGame._KindID] = model.KindID;
            dr[AccountsAgentGame._DeviceID] = model.DeviceID;
            dr[AccountsAgentGame._SortID] = model.SortID;
            dr[AccountsAgentGame._CollectDate] = model.CollectDate;

            aideAccountsAgentGame.Insert(dr);
        }

        /// <summary>
        /// 更新代理游戏列表
        /// </summary>
        /// <param name="gameGameItem"></param>
        public void UpdateAccountsAgentGame(AccountsAgentGame model)
        {
            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.Append("UPDATE AccountsAgentGame SET ")
                    .Append("SortID=@SortID ")
                    .Append("WHERE ID=@ID");

            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("SortID", model.SortID));
            prams.Add(Database.MakeInParam("ID", model.ID));          

            Database.ExecuteNonQuery(CommandType.Text, sqlQuery.ToString(), prams.ToArray());
        }

        /// <summary>
        /// 删除代理游戏列表
        /// </summary>
        /// <param name="sqlQuery"></param>
        public void DeleteAccountsAgentGame(string sqlQuery)
        {
            aideAccountsAgentGame.Delete(sqlQuery);
        }    
        #endregion

        #region 会员属性

        /// <summary>
        /// 获取会员类型数据集
        /// </summary>
        /// <returns></returns>
        public DataSet GetMemberPropertyList()
        {
            string sqlQuery = "SELECT * FROM MemberProperty ORDER BY MemberOrder ASC";
            return Database.ExecuteDataset(sqlQuery);
        }

        /// <summary>
        /// 获取会员类型实体
        /// </summary>
        /// <param name="cardID"></param>
        /// <returns></returns>
        public MemberProperty GetMemberProperty(int memberOrder)
        {
            string sqlQuery = " WHERE MemberOrder=" + memberOrder;
            return aideMemberProperty.GetObject<MemberProperty>(sqlQuery);
        }

        /// <summary>
        /// 更新会员类型
        /// </summary>
        /// <param name="awardType"></param>
        public void UpdateMemberType(MemberProperty memberType)
        {
            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.Append("UPDATE MemberProperty SET ")
                    .Append("UserRight=@UserRight,")
                    .Append("TaskRate=@TaskRate,")
                    .Append("ShopRate=@ShopRate,")
                    .Append("InsureRate=@InsureRate,")
                    .Append("DayPresent=@DayPresent,")
                    .Append("DayGiftID=@DayGiftID")
                    .Append(" WHERE MemberOrder= @MemberOrder");
            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("UserRight", memberType.UserRight));
            prams.Add(Database.MakeInParam("TaskRate", memberType.TaskRate));
            prams.Add(Database.MakeInParam("ShopRate", memberType.ShopRate));
            prams.Add(Database.MakeInParam("InsureRate", memberType.InsureRate));
            prams.Add(Database.MakeInParam("DayPresent", memberType.DayPresent));
            prams.Add(Database.MakeInParam("DayGiftID", memberType.DayGiftID));
            prams.Add(Database.MakeInParam("MemberOrder", memberType.MemberOrder));
            Database.ExecuteNonQuery(CommandType.Text, sqlQuery.ToString(), prams.ToArray());
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