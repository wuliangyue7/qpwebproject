using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using Game.Utils;
using Game.Entity.PlatformManager;
using Game.Facade;
using Game.Entity.Platform;
using Game.Kernel;
using System.Data;
using Game.Entity.Enum;
using Game.Entity.Accounts;
using Game.Entity.Treasure;

namespace Game.Web.Tools
{
    /// <summary>
    /// 后台涉及的一些异步操作，此为6603S特有，尝试后台无刷新操作
    /// </summary>
    public class Operating : IHttpHandler,IRequiresSessionState
    {
        public AjaxJsonValid ajv = new AjaxJsonValid();
        public Base_Users userExt;
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";

            //验证登陆
            if(!FacadeManage.aidePlatformManagerFacade.CheckedUserLogon())
            {
                ajv.msg = "无操作权限";
                context.Response.Write(ajv.SerializeToJson());
                return;
            }
            userExt = FacadeManage.aidePlatformManagerFacade.GetUserInfoFromCache();

            //执行操作
            string action = GameRequest.GetQueryString("action").ToLower();
            switch(action)
            {
                case "updatelevelconfig":
                    UpdateLevelConfig(context);
                    break;
                case "updatelotteryitem":
                    UpdateLotteryItem(context);
                    break;
                case "getusergamerecord":
                    GetUserGameRecord(context);
                    break;
                case "cleartabledata":
                    ClearTableData(context);
                    break;
                case "getnewmessageandneworder":
                    GetNewMessageAndNewOrder(context);
                    break;
                case "getuserinfo":
                    GetUserInfo(context);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 更新等级配置
        /// </summary>
        private void UpdateLevelConfig(HttpContext context)
        {
            //验证权限
            int moduleID = 408;
            AdminPermission adminPer = new AdminPermission(userExt,moduleID);
            if(!adminPer.GetPermission((long)Permission.Delete))
            {
                ajv.msg = "非法操作，无操作权限";
                context.Response.Write(ajv.SerializeToJson());
                return;
            }

            int levelID = GameRequest.GetFormInt("id",0);
            string experience = GameRequest.GetFormString("experience");
            string gold = GameRequest.GetFormString("gold");
            string medal = GameRequest.GetFormString("medal");
            string remark = GameRequest.GetFormString("remark");

            //验证ID
            if(levelID == 0)
            {
                ajv.msg = "非法操作，无效的等级标识";
                context.Response.Write(ajv.SerializeToJson());
                return;
            }

            //验证经验值
            if(!Utils.Validate.IsNumeric(experience))
            {
                ajv.msg = "请输入正确的经验值";
                context.Response.Write(ajv.SerializeToJson());
                return;
            }

            //验证金币
            if(!Utils.Validate.IsNumeric(gold))
            {
                ajv.msg = "请输入正确的金币";
                context.Response.Write(ajv.SerializeToJson());
                return;
            }

            //验证元宝
            if(!Utils.Validate.IsNumeric(medal))
            {
                ajv.msg = "请输入正确的元宝";
                context.Response.Write(ajv.SerializeToJson());
                return;
            }

            //验证备注
            if(remark.Length > 64)
            {
                ajv.msg = "备注的最大长度不能超过64";
                context.Response.Write(ajv.SerializeToJson());
                return;
            }

            GrowLevelConfig glc = new GrowLevelConfig();
            glc.LevelID = levelID;
            glc.Experience = Convert.ToInt32(experience);
            glc.RewardGold = Convert.ToInt32(gold);
            glc.RewardMedal = Convert.ToInt32(medal);
            glc.LevelRemark = remark;

            int result = FacadeManage.aidePlatformFacade.UpdateGrowLevelConfig(glc);
            if(result > 0)
            {
                ajv.msg = "修改成功";
                ajv.SetValidDataValue(true);
            }
            else
            {
                ajv.msg = "修改失败";
            }
            context.Response.Write(ajv.SerializeToJson());
        }

        /// <summary>
        /// 更新奖品配置
        /// </summary>
        /// <param name="context"></param>
        private void UpdateLotteryItem(HttpContext context)
        {
            //验证权限
            int moduleID = 312;
            AdminPermission adminPer = new AdminPermission(userExt, moduleID);
            if (!adminPer.GetPermission((long)Permission.Edit))
            {
                ajv.msg = "非法操作，无操作权限";
                context.Response.Write(ajv.SerializeToJson());
                return;
            }

            int itemIndex = GameRequest.GetFormInt("id", 0);
            string itemType = GameRequest.GetFormString("ItemType");
            string itemQuota = GameRequest.GetFormString("ItemQuota");
            string itemRate = GameRequest.GetFormString("ItemRate");

            //验证ID
            if (itemIndex == 0)
            {
                ajv.msg = "非法操作，无效的奖品索引";
                context.Response.Write(ajv.SerializeToJson());
                return;
            }

            //验证类型
            if (!Utils.Validate.IsPositiveInt(itemType))
            {
                ajv.msg = "请输入正确的奖品类型";
                context.Response.Write(ajv.SerializeToJson());
                return;
            }
            //验证数量
            if (!Utils.Validate.IsPositiveInt(itemQuota))
            {
                ajv.msg = "请输入正确的奖品数量";
                context.Response.Write(ajv.SerializeToJson());
                return;
            }
            //验证中奖几率
            if (!Utils.Validate.IsPositiveInt(itemRate))
            {
                ajv.msg = "请输入正确的中奖几率";
                context.Response.Write(ajv.SerializeToJson());
                return;
            }

            LotteryItem model = new LotteryItem();
            model.ItemIndex = itemIndex;
            model.ItemType = Convert.ToInt32(itemType);
            model.ItemQuota = Convert.ToInt32(itemQuota);
            model.ItemRate = Convert.ToInt32(itemRate);
            int result = FacadeManage.aideTreasureFacade.UpdateLotteryItem(model);
            if (result > 0)
            {
                ajv.msg = "修改成功";
                ajv.SetValidDataValue(true);
            }
            else
            {
                ajv.msg = "修改失败";
            }
            context.Response.Write(ajv.SerializeToJson());
        }

        /// <summary>
        /// 获取游戏记录
        /// </summary>
        /// <param name="context"></param>
        private void GetUserGameRecord(HttpContext context)
        {
            //验证权限
            int moduleID = 809;
            AdminPermission adminPer = new AdminPermission(userExt,moduleID);
            if(!adminPer.GetPermission((long)Permission.Read))
            {
                ajv.msg = "非法操作，无操作权限";
                context.Response.Write(ajv.SerializeToJson());
                return;
            }

            int drawID = GameRequest.GetQueryInt("drawID",0);

            //验证ID
            if(drawID == 0)
            {
                ajv.msg = "非法操作，无效的局数标识";
                context.Response.Write(ajv.SerializeToJson());
                return;
            }

            //获取数据
            DataSet ds = FacadeManage.aideTreasureFacade.GetRecordDrawScoreByDrawID(drawID);
            if(ds.Tables[0].Rows.Count > 0)
            {
                //复制表结构
                DataTable dt = ds.Tables[0].Clone();

                //修改表列数据类型
                dt.Columns["IsAndroid"].DataType = typeof(string);
                dt.Columns["Score"].DataType = typeof(string);
                dt.Columns["Revenue"].DataType = typeof(string);

                for(int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                {
                    DataRow dw = dt.NewRow();
                    dw = ds.Tables[0].Rows[i];
                    dt.Rows.Add(dw.ItemArray);

                    //修改是否机器人数据项
                    if(Convert.ToInt32(dt.Rows[i]["IsAndroid"]) == 0)
                    {
                        dt.Rows[i]["IsAndroid"] = "否";
                    }
                    else
                    {
                        dt.Rows[i]["IsAndroid"] = "是";
                    }

                    //格式化输赢积分
                    dt.Rows[i]["Score"] = Convert.ToInt64(dt.Rows[i]["Score"]).ToString("N0");

                    //格式化税收
                    dt.Rows[i]["Revenue"] = Convert.ToInt32(dt.Rows[i]["Revenue"]).ToString("N0");
                }

                Game.Utils.Template tm = new Game.Utils.Template("/Template/UserGameRecord.html");
                Dictionary<string,DataTable> dicTable = new Dictionary<string,DataTable>();
                dicTable.Add("UserGameRecord",dt);
                tm.ForDataScoureList = dicTable;

                string html = tm.OutputHTML();
                ajv.AddDataItem("html",html);
            }

            //返回数据
            ajv.SetValidDataValue(true);
            context.Response.Write(ajv.SerializeToJson());
        }

        /// <summary>
        /// 清楚表数据
        /// </summary>
        /// <param name="context"></param>
        private void ClearTableData(HttpContext context)
        {
            //验证权限
            int moduleID = 812;
            AdminPermission adminPer = new AdminPermission(userExt,moduleID);
            if(!adminPer.GetPermission((long)Permission.Delete))
            {
                ajv.msg = "非法操作，无操作权限";
                context.Response.Write(ajv.SerializeToJson());
                return;
            }

            string time = GameRequest.GetString("time");
            int id = GameRequest.GetInt("id",0);

            //验证ID
            if(id == 0)
            {
                ajv.msg = "非法操作，清除数据失败";
                context.Response.Write(ajv.SerializeToJson());
                return;
            }

            //验证截止日期
            if(string.IsNullOrEmpty(time))
            {
                ajv.msg = "请选择要清除数据的截止时间";
                context.Response.Write(ajv.SerializeToJson());
                return;
            }
            DateTime date;
            try
            {
                date = Convert.ToDateTime(time).AddDays(1);
            }
            catch
            {
                ajv.msg = "请输入正确的时间";
                context.Response.Write(ajv.SerializeToJson());
                return;
            }

            //删除数据
            int result = 0;
            switch(id)
            {
                case 1: //删除玩家进出记录表
                    result = FacadeManage.aideTreasureFacade.DeleteRecordUserInoutByTime(date);
                    break;
                case 2: //删除游戏记录总局表
                    result = FacadeManage.aideTreasureFacade.DeleteRecordDrawInfoByTime(date);
                    break;
                case 3: //删除游戏记录详情表
                    result = FacadeManage.aideTreasureFacade.DeleteRecordDrawScoreByTime(date);
                    break;
                case 4: //删除银行操作记录表
                    result = FacadeManage.aideTreasureFacade.DeleteRecordInsureByTime(date);
                    break;
                default:
                    break;
            }

            ajv.SetValidDataValue(true);
            ajv.msg = string.Format("操作成功，共删除了{0}条记录",result);
            context.Response.Write(ajv.SerializeToJson());
            return;
        }

        /// <summary>
        /// 获取未处理留言和新的订单数
        /// </summary>
        /// <param name="context"></param>
        private void GetNewMessageAndNewOrder(HttpContext context)
        {
            int newMessageCounts = FacadeManage.aideNativeWebFacade.GetNewMessageCounts();
            int newOrderCounts = FacadeManage.aideNativeWebFacade.GetNewOrderCounts();
            ajv.SetValidDataValue(true);
            ajv.AddDataItem("newMessage",newMessageCounts);
            ajv.AddDataItem("newOrder",newOrderCounts);
            context.Response.Write(ajv.SerializeToJson());
        }        

        /// <summary>
        /// 通过游戏ID获取游戏信息
        /// </summary>
        /// <param name="context"></param>
        private void GetUserInfo(HttpContext context)
        {
            int gameID = GameRequest.GetFormInt("GameID", 0);
            if (gameID == 0)
            {
                ajv.msg = "游戏ID输入非法";
                context.Response.Write(ajv.SerializeToJson());
                return;
            }

            AccountsInfo info = FacadeManage.aideAccountsFacade.GetAccountInfoByGameID(gameID);
            if (info.UserID != 0)
            {
                ajv.SetValidDataValue(true);
                ajv.AddDataItem("UserID", info.UserID);
                ajv.AddDataItem("Accounts", info.Accounts);
            }
            else
            {
                ajv.msg = "未找到该用户信息";
            }
            context.Response.Write(ajv.SerializeToJson());
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
