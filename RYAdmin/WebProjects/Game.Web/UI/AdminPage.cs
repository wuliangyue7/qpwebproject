using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

using Game.Facade;
using Game.Utils;
using Game.Entity;
using Game.Entity.Platform;
using Game.Entity.Treasure;
using Game.Entity.Accounts;
using Game.Entity.PlatformManager;
using Game.Entity.Enum;
using System.IO;
using Game.Utils.Cache;
using System.Text.RegularExpressions;
using Game.Entity.NativeWeb;


namespace Game.Web.UI
{
    /// <summary>
    /// 后台基类
    /// </summary>
    public abstract partial class AdminPage : Page
    {
        #region Fields

        /// <summary>
        /// 模块标识
        /// </summary>
        protected internal int moduleID;

        /// <summary>
        /// 用户对象
        /// </summary>
        protected internal Base_Users userExt;

        /// <summary>
        /// 可管理站点
        /// </summary>
        protected string _ownStation;

        //页面代码
        private static List<ModulePage> pagePowerList;

        #endregion

        #region 保护属性

        /// <summary>
        /// 命令
        /// </summary>
        protected string StrCmd
        {
            get
            {
                return GameRequest.GetQueryString("cmd");
            }
        }

        /// <summary>
        /// Get方法传递的整型参数 param
        /// </summary>
        protected int IntParam
        {
            get
            {
                return GameRequest.GetQueryInt("param", 0);
            }
        }

        /// <summary>
        /// Get方法传递的字符串参数 param
        /// </summary>
        protected string StrParam
        {
            get
            {
                return GameRequest.GetQueryString("param");
            }
        }

        /// <summary>
        /// Get方法传递的页码 page
        /// </summary>
        protected int PageIndex
        {
            get
            {
                return GameRequest.GetQueryInt("page", 1);
            }
        }

        /// <summary>
        /// Get方法传递的整型列表参数 param
        /// </summary>
        protected string StrParamsList
        {
            get
            {
                string[] strArray = GameRequest.GetString("param").Trim().Split(new char[] { ',' });
                StringBuilder builder = new StringBuilder();
                foreach(string str2 in strArray)
                {
                    if(Utils.Validate.IsNumeric(str2))
                    {
                        builder.Append(str2 + ",");
                    }
                }
                return builder.ToString().TrimEnd(new char[] { ',' });
            }
        }

        /// <summary>
        /// Post方法传递的整型列表参数 cid
        /// </summary>
        protected string StrCIdList
        {
            get
            {
                string[] strArray = GameRequest.GetFormString("cid").Trim().Split(new char[] { ',' });
                StringBuilder builder = new StringBuilder();
                foreach(string str2 in strArray)
                {
                    if(Utils.Validate.IsNumeric(str2))
                    {
                        builder.Append(str2 + ",");
                    }
                }
                return builder.ToString().TrimEnd(new char[] { ',' });
            }
        }

        /// <summary>
        /// Get方法传递的排序方法
        /// </summary>
        protected string OrderBy
        {
            get
            {
                string orderField = GameRequest.GetQueryString("orderfield").Trim();
                Regex reg = new Regex(@"^[a-zA-Z\d_,]+$", RegexOptions.Compiled);
                if(!reg.IsMatch(orderField))
                {
                    return "";
                }
                string[] strArray = orderField.Split(new char[] { ',' });
                string orderType = string.Empty;
                if(strArray.Length > 1)
                {
                    StringBuilder builder = new StringBuilder();
                    foreach(string str2 in strArray)
                    {
                        builder.Append(str2 + "+");
                    }
                    orderField = builder.ToString().TrimEnd(new char[] { '+' });
                }
                else
                {
                    orderField = strArray[0];
                }
                int type = GameRequest.GetInt("ordertype", 0);
                if(type == 0)
                {
                    orderType = "DESC";
                }
                else
                {
                    orderType = "ASC";
                }
                if(!string.IsNullOrEmpty(orderField) && !string.IsNullOrEmpty(orderType))
                {
                    return "ORDER BY " + OrderField + " " + OrderType;
                }
                else
                {
                    return "";
                }
            }
            set
            {
                OrderBy = value;
            }
        }

        /// <summary>
        /// 排序字段
        /// </summary>
        protected string OrderField
        {
            get
            {
                string orderField = GameRequest.GetQueryString("orderfield").Trim();
                Regex reg = new Regex(@"^[a-zA-Z\d_,]+$", RegexOptions.Compiled);
                if(!reg.IsMatch(orderField))
                    return "";
                string[] strArray = orderField.Split(new char[] { ',' });
                if(strArray.Length > 1)
                {
                    StringBuilder builder = new StringBuilder();
                    foreach(string str2 in strArray)
                    {
                        builder.Append(str2 + "+");
                    }
                    return builder.ToString().TrimEnd(new char[] { '+' });
                }
                else
                {
                    return strArray[0];
                }
            }
        }

        /// <summary>
        /// 排序方法 升序/降序
        /// </summary>
        protected string OrderType
        {
            get
            {
                int type = GameRequest.GetInt("ordertype", 0);
                if(type == 0)
                {
                    return "DESC";
                }
                else
                {
                    return "ASC";
                }
            }
        }

        /// <summary>
        /// 是否检查统计图的临时目录
        /// </summary>
        protected virtual bool IsCreateTmepDir
        {
            get
            {
                return false;
            }
        }

        #endregion

        #region 构造方法

        /// <summary>
        /// 页面初始化
        /// </summary>
        static AdminPage()
        {
            //加载页面代码
            pagePowerList = SerializationHelper.Deserialize(typeof(List<ModulePage>), TextUtility.GetRealPath("/config/power.config")) as List<ModulePage>;
        }

        /// <summary>
        /// 页面载入
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            SetAuthorization();

            //登录判断
            userExt = FacadeManage.aidePlatformManagerFacade.GetUserInfoFromCache();
            if(userExt == null || userExt.UserID <= 0 || (userExt.UserID != ApplicationConfig.SUPER_ADMINISTRATOR_ID && userExt.RoleID <= 0))
            {
                Redirect("/Login.aspx?errtype=overtime");
            }
            else
            {
                //更新凭证
                FacadeManage.aidePlatformManagerFacade.SaveUserCache(userExt);
            }

            //权限判断
            if(userExt.UserID == ApplicationConfig.SUPER_ADMINISTRATOR_ID)
            {
                userExt.RoleID = ApplicationConfig.SUPER_ADMINISTRATOR_ID;
            }
            AuthUserPagePermission();

            //检查临时目录
            if(IsCreateTmepDir)
            {
                string dir = Server.MapPath("/TempImages");
                if(!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
            }
        }

        #endregion

        #region 权限检查

        /// <summary>
        /// 验证当前页面上的访问权限(有权限:true ; 无权限:false;)
        /// </summary>
        protected virtual void AuthUserPagePermission()
        {
            AdminPermission adminPer = new AdminPermission(userExt, moduleID);
            if(!adminPer.GetUserPagePermission())
            {
                Redirect("/NotPower.html");
            }
        }

        /// <summary>
        /// 验证当前页面上的操作权限(有权限:true ; 无权限:false;)
        /// </summary>
        /// <param name="moduleID">需要验证的模块标识</param>
        /// <param name="privValue">需要验证的权限值</param>
        /// <returns></returns>
        protected bool AuthUserOperationPermission(int moduleID, long privValue)
        {
            AdminPermission adminPer = new AdminPermission(userExt, moduleID);
            return adminPer.GetPermission(privValue);
        }

        /// <summary>
        /// 验证操作权限
        /// </summary>
        /// <param name="permission"></param>
        protected void AuthUserOperationPermission(Permission permission)
        {
            AdminPermission adminPer = new AdminPermission(userExt, moduleID);
            if(!adminPer.GetPermission((long)permission))
            {
                Redirect("/NotPower.html");
            }
        }

        /// <summary>
        /// 设置授权
        /// </summary>
        protected virtual void SetAuthorization()
        {
            if(pagePowerList == null)
            {
                Redirect("/NotPower.html");
            }

            string pageName = GameRequest.GetPageName();
            ModulePage modulePage = pagePowerList.Find(delegate(ModulePage mPage)
            {
                foreach(string str in mPage.PageName.Split(','))
                {
                    if(str.Equals(pageName, StringComparison.InvariantCultureIgnoreCase))
                    {
                        return true;
                    }
                };
                return false;
            });
            if(modulePage != null)
            {
                moduleID = modulePage.ModuleID;
            }
            else
            {
                Redirect("/NotPower.html");
            }
        }

        #endregion

        #region Cookie 检查

        /// <summary>
        /// Cookie 检查
        /// </summary>
        /// <returns></returns>
        protected bool CheckedCookie()
        {
            return FacadeManage.aidePlatformManagerFacade.CheckedUserLogon();
        }

        #endregion

        #region 辅助方法

        /// <summary>
        /// 页面跳转
        /// </summary>
        /// <param name="url"></param>
        protected void Redirect(string url)
        {
            Fetch.Redirect(url);
        }

        /// <summary>
        /// 清空缓存
        /// </summary>
        protected void ClearPageCache()
        {
            Utility.ClearPageClientCache();
        }

        /// <summary>
        /// 控件设置
        /// </summary>
        /// <param name="parentControl">要设置的控件树</param>
        /// <param name="ctrNamePrefix">控件ID的前缀</param>
        /// <param name="isShow">显示还是隐藏</param>
        protected void VisibleControl(System.Web.UI.Control parentControl, string ctrNamePrefix, bool isShow)
        {
            if(parentControl.ID != null && parentControl.ID.StartsWith(ctrNamePrefix, StringComparison.InvariantCultureIgnoreCase))
            {
                parentControl.Visible = isShow;
            }
            if(parentControl.Controls.Count > 0)
            {
                foreach(System.Web.UI.Control childControl in parentControl.Controls)
                    VisibleControl(childControl, ctrNamePrefix, isShow);
            }
        }

        /// <summary>
        /// 错误显示
        /// </summary>
        /// <param name="msg"></param>
        protected void ShowError(string msg)
        {
            Page.ClientScript.RegisterStartupScript(typeof(Page), "", string.Format("showError(\"{0}\");", msg), true);
        }

        /// <summary>
        /// 错误显示
        /// </summary>
        /// <param name="msg"></param>
        protected void ShowError(string msg, string url, int timeout)
        {
            Page.ClientScript.RegisterStartupScript(typeof(Page), "",
                string.Format("showError(\"{0}\");setTimeout(\"Redirect('{1}')\",{2})", msg, url, timeout), true);
        }

        /// <summary>
        /// 信息显示
        /// </summary>
        /// <param name="msg"></param>
        protected void ShowInfo(string msg)
        {
            Page.ClientScript.RegisterStartupScript(typeof(Page), "", string.Format("showInfo(\"{0}\");", msg), true);
        }

        /// <summary>
        /// 信息显示并跳转
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="url"></param>
        protected void ShowInfo(string msg, string url, int timeout)
        {
            Page.ClientScript.RegisterStartupScript(typeof(Page), "",
                string.Format("showInfo(\"{0}\");setTimeout(\"Redirect('{1}')\",{2})", msg, url, timeout), true);
        }

        /// <summary>
        /// 弹出提示信息
        /// </summary>
        /// <param name="msg"></param>
        protected void MessageBox(string msg)
        {
            Page.ClientScript.RegisterStartupScript(typeof(Page), "", string.Format("alert(\"{0}\");", msg), true);
        }

        /// <summary>
        /// 弹出提示信息并跳转
        /// </summary>
        /// <param name="Message"></param>
        /// <param name="ReturnUrl"></param>
        protected void MessageBox(string msg, string ReturnUrl)
        {
            Page.ClientScript.RegisterStartupScript(typeof(Page), "", string.Format("alert(\"{0}\");parent.location.href='{1}'", msg, ReturnUrl), true);
        }

        /// <summary>
        /// 弹出提示信息
        /// </summary>
        /// <param name="msg"></param>
        protected void MessageBoxClose(string msg)
        {
            Page.ClientScript.RegisterStartupScript(typeof(Page), "", string.Format("alert(\"{0}\");window.close();", msg), true);
        }

        /// <summary>
        /// 弹出提示信息，父页面刷新
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ReturnUrl"></param>
        protected void MessageBoxCloseRef(string msg)
        {
            Page.ClientScript.RegisterStartupScript(typeof(Page), "", string.Format("alert(\"{0}\");opener.document.location.href=opener.document.location.href,window.close();", msg), true);
        }

        #endregion

        #region 控件辅助

        /// <summary>
        /// 绑定 GridView 行样式,cellIndex 定义 鼠标样式 为0时，不作处理
        /// </summary>
        /// <param name="row"></param>
        /// <param name="cellIndex"></param>
        protected void GridViewRowStyleBind(GridViewRow row, int cellIndex)
        {
            //样式绑定
            row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#EBEBEB';this.className='pointer'");
            row.Attributes.Add("onmouseout", "this.style.backgroundColor=c;this.className='pointer'");

            if(cellIndex > 0)
            {
                row.Cells[cellIndex].Attributes.Add("onmouseover", "this.style.cursor='default'");
                row.Cells[cellIndex].Attributes.Add("onmouseout", "this.style.cursor='default'");
            }
        }

        /// <summary>
        ///  绑定 GridView 行样式,cellIndex=1 定义 单元格一的鼠标样式
        /// </summary>
        /// <param name="row"></param>
        protected void GridViewRowStyleBind(GridViewRow row)
        {
            GridViewRowStyleBind(row, 1);
        }

        /// <summary>
        /// 为 GridView 行绑定客户端单击事件
        /// </summary>
        /// <param name="row"></param>
        /// <param name="startIndex">起始单元格</param>
        /// <param name="skipIndexs">跳过单元格</param>
        /// <param name="jumpUrl">跳转地址</param>
        protected void GridViewRowOnClientClick(GridViewRow row, int startIndex, int[] skipIndexs, string jumpUrl)
        {
            for(int i = startIndex; i < row.Cells.Count; i++)
            {
                if(skipIndexs.Length > 0)
                {
                    if(Array.Exists<int>(skipIndexs, delegate(int index)
                    {
                        return index == i;
                    }))
                        continue;
                }

                row.Cells[i].Attributes.Add("onclick", string.Format("Redirect(\"{0}\");", jumpUrl));
            }
        }

        /// <summary>
        /// 为 GridView 行绑定客户端单击事件
        /// </summary>
        /// <param name="row"></param>
        /// <param name="startIndex">起始单元格</param>
        /// <param name="jumpUrl">跳转地址</param>
        protected void GridViewRowOnClientClick(GridViewRow row, int startIndex, string jumpUrl)
        {
            GridViewRowOnClientClick(row, startIndex, new int[0], jumpUrl);
        }

        /// <summary>
        /// 获取 GridView 行内的字段值
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="row">单独行</param>
        /// <param name="field">字段名称</param>
        /// <returns></returns>
        protected T GetRowValue<T>(GridViewRow row, string field)
        {
            object objVal = DataBinder.Eval(row.DataItem, field);
            if(objVal == DBNull.Value)
                return default(T);

            T fieldValue = (T)objVal;
            return fieldValue;
        }

        /// <summary>
        /// 获取 GridView 的主键值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="gv"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        protected T GetDataKeyValue<T>(GridView gv, GridViewRow row)
        {
            object objVal = gv.DataKeys[row.DataItemIndex].Value;
            if(objVal == DBNull.Value)
                return default(T);

            T keyValue = (T)objVal;
            return keyValue;
        }

        /// <summary>
        /// 获取 GridView 行内嵌控件对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="row"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        protected T GetControl<T>(GridViewRow row, string field)
        {
            object objVal = row.FindControl(field);
            if(objVal == null)
                return default(T);

            T objCtrl = (T)objVal;
            return objCtrl;
        }

        #endregion

        #region 数据部分

        #region 平台库部分

        /// <summary>
        /// 获取类型名称
        /// </summary>
        /// <param name="typeID"></param>
        /// <returns></returns>
        protected string GetGameTypeName(int typeID)
        {
            GameTypeItem gameType = FacadeManage.aidePlatformFacade.GetGameTypeItemInfo(typeID);
            if(gameType == null)
            {
                return "";
            }
            return gameType.TypeName;
        }

        /// <summary>
        /// 获取游戏名称
        /// </summary>
        /// <param name="kindID"></param>
        /// <returns></returns>
        protected string GetGameKindName(int kindID)
        {
            GameKindItem gameKind = FacadeManage.aidePlatformFacade.GetGameKindItemInfo(kindID);
            if(gameKind == null)
            {
                return "";
            }
            return gameKind.KindName;
        }

        /// <summary>
        /// 获取手机游戏列表
        /// </summary>
        /// <param name="kindID"></param>
        /// <returns></returns>
        protected string GetMobileGameName(int kindID)
        {
            MobileKindItem gameKind = FacadeManage.aidePlatformFacade.GetMobileKindItemInfo(kindID);
            if (gameKind == null)
            {
                return "";
            }
            return gameKind.KindName;
        }

        /// <summary>
        /// 获取模块名称
        /// </summary>
        /// <param name="gameID"></param>
        /// <returns></returns>
        protected string GetGameGameName(int gameID)
        {
            GameGameItem gameitem = FacadeManage.aidePlatformFacade.GetGameGameItemInfo(gameID);
            if(gameitem == null)
            {
                return "";
            }
            return gameitem.GameName;
        }

        /// <summary>
        /// 获取房间名称
        /// </summary>
        /// <param name="serverID"></param>
        /// <returns></returns>
        protected string GetGameRoomName(int serverID)
        {
            GameRoomInfo gameRoom = FacadeManage.aidePlatformFacade.GetGameRoomInfoInfo(serverID);
            if(gameRoom == null)
            {
                return "";
            }
            return gameRoom.ServerName;
        }

        /// <summary>
        /// 获取房间类型名称
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        protected string GetSupporTypeName(object type)
        {
            return EnumDescription.GetFieldText((SupporTypeStatus)type);
        }

        /// <summary>
        /// 获取游戏节点名称
        /// </summary>
        /// <param name="nodeID"></param>
        /// <returns></returns>
        protected string GetGameNodeName(int nodeID)
        {
            if(nodeID <= 0)
                return "无挂接";
            GameNodeItem gameNode = FacadeManage.aidePlatformFacade.GetGameNodeItemInfo(nodeID);
            if(gameNode == null)
                return "无挂接";
            else
                return gameNode.NodeName;

        }
        #endregion

        #region 帐号库部分

        /// <summary>
        /// 获取用户ID通过用户帐号
        /// </summary>
        /// <param name="accounts">用户帐号</param>
        /// <returns>用户ID</returns>
        protected int GetUserIDByAccount(string accounts)
        {
            AccountsInfo accountsInfo = FacadeManage.aideAccountsFacade.GetAccountInfoByAccount(accounts);
            return accountsInfo == null ? 0 : accountsInfo.UserID;
        }

        /// <summary>
        /// 获取用户实体通过用户帐号
        /// </summary>
        /// <param name="accounts"></param>
        /// <returns></returns>
        protected AccountsInfo GetAccountsInfoByAccount(string accounts)
        {
            return FacadeManage.aideAccountsFacade.GetAccountInfoByAccount(accounts); 
        }

        /// <summary>
        /// 获取用户ID通过GameID
        /// </summary>
        /// <param name="gameID">GameID</param>
        /// <returns>用户ID</returns>
        protected int GetUserIDByGameID(int gameID)
        {
            AccountsInfo accountsInfo = FacadeManage.aideAccountsFacade.GetAccountInfoByGameID(gameID);
            return accountsInfo == null ? 0 : accountsInfo.UserID;
        }

        /// <summary>
        /// 获取用户实体通过GameID
        /// </summary>
        /// <param name="gameID"></param>
        /// <returns></returns>
        protected AccountsInfo GetAccountsInfoByGameID(int gameID)
        {
            return  FacadeManage.aideAccountsFacade.GetAccountInfoByGameID(gameID);
        }

        /// <summary>
        /// 获取用户帐号
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        protected string GetAccounts(int userID)
        {
            AccountsInfo accounts = FacadeManage.aideAccountsFacade.GetAccountInfoByUserID(userID);
            if(accounts == null)
            {
                return "";
            }
            return accounts.Accounts;
        }
        /// <summary>
        /// 获得昵称
        /// </summary>
        /// <param name="userID">用户UserID</param>
        /// <returns></returns>
        public string GetNickNameByUserID(int userID)
        {
            AccountsInfo model = FacadeManage.aideAccountsFacade.GetAccountInfoByUserID(userID);
            if(model != null)
                return model.NickName;
            else
                return "";
        }
        /// <summary>
        /// 获取用户游戏ID
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        protected string GetGameID(int userID)
        {
            AccountsInfo accounts = FacadeManage.aideAccountsFacade.GetAccountInfoByUserID(userID);
            if(accounts == null)
            {
                return "0";
            }
            return accounts.GameID.ToString();
        }
        /// <summary>
        /// 获取魅力值
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        protected string GetLoveLinessByUserID(int userID)
        {
            AccountsInfo accounts = FacadeManage.aideAccountsFacade.GetAccountInfoByUserID(userID);
            if(accounts == null)
            {
                return "0";
            }
            return accounts.LoveLiness.ToString();
        }
        /// <summary>
        /// 获取已兑换的魅力值
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        protected string GetPresentByUserID(int userID)
        {
            AccountsInfo accounts = FacadeManage.aideAccountsFacade.GetAccountInfoByUserID(userID);
            if(accounts == null)
            {
                return "0";
            }
            return accounts.Present.ToString();
        }
        /// <summary>
        /// 获取经验值
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        protected string GetExperienceByUserID(int userID)
        {
            AccountsInfo accounts = FacadeManage.aideAccountsFacade.GetAccountInfoByUserID(userID);
            if(accounts == null)
            {
                return "0";
            }
            return accounts.Experience.ToString();
        }
        /// <summary>
        /// 获取奖牌数
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        protected string GetUserMedalByUserID(int userID)
        {
            AccountsInfo accounts = FacadeManage.aideAccountsFacade.GetAccountInfoByUserID(userID);
            if(accounts == null)
            {
                return "0";
            }
            return accounts.UserMedal.ToString();
        }

        /// <summary>
        /// 判断是否机器人
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        protected bool IsAndroid(int userID)
        {
            AccountsInfo accounts = FacadeManage.aideAccountsFacade.GetAccountInfoByUserID(userID);
            if(accounts == null)
            {
                return false;
            }
            if(accounts.IsAndroid == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        #endregion

        #region 游戏币库部分

        /// <summary>
        /// 获取游戏币
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        protected string GetScoreByUserID(int userID)
        {
            GameScoreInfo model = FacadeManage.aideTreasureFacade.GetGameScoreInfoByUserID(userID);
            if(model == null)
                return "0";
            return model.Score.ToString();
        }
        /// <summary>
        /// 获取银行游戏币
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        protected string GetInsureScoreByUserID(int userID)
        {
            return FacadeManage.aideTreasureFacade.GetGameScoreByUserID(userID).ToString();
        }


        /// <summary>
        /// 获取服务名称
        /// </summary>
        /// <param name="shareID"></param>
        /// <returns></returns>
        protected string GetShareName(int shareID)
        {
            GlobalShareInfo globalShare = FacadeManage.aideTreasureFacade.GetGlobalShareByShareID(shareID);
            if(globalShare == null)
            {
                return "";
            }
            else
            {
                return globalShare.ShareName.Trim();
            }
        }

        /// <summary>
        /// 获取实卡名称
        /// </summary>
        /// <param name="typeID"></param>
        /// <returns></returns>
        protected string GetCardTypeName(int typeID)
        {
            GlobalLivcard card = FacadeManage.aideTreasureFacade.GetGlobalLivcardInfo(typeID);
            if(card == null)
                return "";
            return card.CardName;
        }

        #endregion

        #region 管理库

        /// <summary>
        /// 获取管理员帐号
        /// </summary>
        /// <param name="masterID"></param>
        /// <returns></returns>
        protected string GetMasterName(int masterID)
        {
            return FacadeManage.aidePlatformManagerFacade.GetAccountsByUserID(masterID);
        }

        /// <summary>
        /// 获取角色名称
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        protected string GetRoleName(int roleID)
        {
            Base_Roles role = FacadeManage.aidePlatformManagerFacade.GetRoleInfo(roleID);
            if(role == null)
            {
                return "";
            }
            return role.RoleName;
        }
        #endregion

        #region 公共方法部分

        /// <summary>
        /// 根据IP的地理位置
        /// </summary>
        /// <param name="IP">IP</param>
        /// <returns>地理位置</returns>
        protected string GetAddressWithIP(string IP)
        {
            return IPQuery.GetAddressWithIP(IP);
        }
        /// <summary>
        /// 获取会员级别名称
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        protected string GetMemberName(byte order)
        {
            string resultValue = "";
            string MemberName = Enum.GetName(typeof(MemberOrderStatus), order);
            switch(order)
            {
                case 0:
                    resultValue = "{0}";//普通会员
                    break;
                case 1:
                    resultValue = "<span style='color:#15599f;font-weight:bold;'>{0}</span>";//VIP1
                    break;
                case 2:
                    resultValue = "<span style='color:#d16213;font-weight:bold;'>{0}</span>";//VIP2
                    break;
                case 3:
                    resultValue = "<span style='color:#777777; font-weight:bold;'>{0}</span>";//VIP3
                    break;
                case 4:
                    resultValue = "<span style='color:#b70000; font-weight:bold;'>{0}</span>";//VIP4
                    break;
                case 5:
                    resultValue = "<span style='color:#FF6600; font-weight:bold;'>{0}</span>";//VIP5
                    break;
                default:
                    resultValue = "{0}";//普通会员
                    break;

            }
            return string.Format(resultValue, MemberName);
        }
        /// <summary>
        /// 会员卡使用范围
        /// </summary>
        /// <param name="rangeid"></param>
        /// <returns></returns>
        protected string GetUserRange(int rangeid)
        {
            string strResult = "";
            switch(rangeid)
            {
                case 0:
                    strResult = "全部用户";
                    break;
                case 1:
                    strResult = "新注册用户";
                    break;
                case 2:
                    strResult = "第一次充值用户";
                    break;
                default:
                    strResult = "";
                    break;
            }
            return strResult;
        }
        /// <summary>
        /// 兑换场所(0:大厅,1:网页)
        /// </summary>
        /// <param name="IsGamePlaza"></param>
        /// <returns></returns>
        protected string GetExchangePlace(int IsGamePlaza)
        {
            if(IsGamePlaza == 0)
            {
                return "<span>大厅</span>";
            }
            else
            {
                return "<span class='hong'>网页</span>";
            }
        }
        /// <summary>
        /// 给出状态(全局通用0:启用; 1:禁止)
        /// </summary>
        /// <param name="nullity"></param>
        /// <returns></returns>
        protected string GetNullityStatus(byte nullity)
        {
            if(nullity == 0)
            {
                return "<span>启用</span>";
            }
            else
            {
                return "<span class='hong'>禁用</span>";
            }
        }

        /// <summary>
        /// 获取身份类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        protected string GetPassPortType(Byte type)
        {
            string returnValue = "";
            switch(type)
            {
                case 0:
                    returnValue = "没有设置";
                    break;
                case 1:
                    returnValue = "身份证";
                    break;
                case 2:
                    returnValue = "护照";
                    break;
                case 3:
                    returnValue = "军官证";
                    break;
                case 4:
                    returnValue = "驾驶执照";
                    break;
                default:
                    returnValue = "其他";
                    break;
            }
            return returnValue;
        }

        /// <summary>
        /// 获取订单状态
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        protected string GetOnLineOrderStatus(byte status)
        {
            string returnValue = "";
            switch(status)
            {
                case 0:
                    returnValue = "<span class='hong'>未付款</span>";
                    break;
                case 1:
                    returnValue = "<span>待处理</span>";
                    break;
                case 2:
                    returnValue = "<span>完成</span>";
                    break;
                default:
                    break;
            }
            return returnValue;
        }


        /// <summary>
        /// 计算版本号
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns>
        protected string CalVersion(int version)
        {
            string returnValue = "";
            returnValue += (version >> 24).ToString() + ".";
            returnValue += (((version >> 16) << 24) >> 24).ToString() + ".";
            returnValue += (((version >> 8) << 24) >> 24).ToString() + ".";
            returnValue += ((version << 24) >> 24).ToString();
            return returnValue;
        }

        /// <summary>
        /// 还原版本号
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns>
        protected int CalVersion2(string version)
        {
            int rValue = 0;
            string[] verArray = version.Split('.');
            rValue = (int.Parse(verArray[0]) << 24) | (int.Parse(verArray[1]) << 16) | (int.Parse(verArray[2]) << 8) | int.Parse(verArray[3]);
            return rValue;
        }

        /// <summary>
        /// 对数字添加”,“号，可以处理负数以及带有小数的情况
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns>
        protected string FormatMoney(string money)
        {
            //处理带有负号情况
            int negNumber = money.IndexOf("-");
            string prefix = string.Empty;
            if(negNumber != -1)
            {
                prefix = "-";
                money = money.Substring(1);
            }
            //处理有小数位情况
            int decNumber = money.IndexOf(".");
            string postfix = string.Empty;
            if(decNumber != -1)
            {
                postfix = money.Substring(decNumber);
                money = money.Substring(0, decNumber);
            }
            //开始添加”,“号
            if(money.Length > 3)
            {
                string str1 = money.Substring(0, money.Length - 3);
                string str2 = money.Substring(money.Length - 3, 3);
                if(str1.Length > 3)
                {
                    return prefix + FormatMoney(str1) + "," + str2 + postfix;
                }
                else
                {
                    return prefix + str1 + "," + str2 + postfix;
                }
            }
            else
            {
                return prefix + money + postfix;
            }
        }

        #endregion

        #endregion

        #region 图片操作

        /// <summary>
        /// 验证数据是否图片格式
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool ValidateIsImage(Stream data)
        {
            try
            {
                System.Drawing.Image image = System.Drawing.Image.FromStream(data);
                image.Dispose();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 判断图片的大小是否正确
        /// </summary>
        /// <param name="name">图片数据</param>
        /// <param name="width">宽度</param>
        /// <param name="heigh">高度</param>
        /// <returns></returns>
        public bool ImageWidth(Stream data, string width, string heigh)
        {
            System.Drawing.Image image = System.Drawing.Image.FromStream(data);
            if(width == image.Width.ToString() && heigh == image.Height.ToString())
            {
                image.Dispose();
                return true;
            }
            return false;
        }

        /// <summary>
        /// 获取字节流
        /// </summary>
        public Byte[] GetBytes(Stream fileStr)
        {
            if(fileStr.Length != 0)
            {
                byte[] fileBytes = new byte[fileStr.Length];
                fileStr.Position = 0;
                fileStr.Read(fileBytes, 0, fileBytes.Length);
                fileStr.Seek(0, SeekOrigin.Begin);
                return fileBytes;
            }
            return new byte[0];
        }
        #endregion
    }
}
