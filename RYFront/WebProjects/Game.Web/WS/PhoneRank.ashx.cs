using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Text;
using Game.Entity.Treasure;
using Game.Facade;
using Game.Utils;
using System.Data;

namespace Game.Web.WS
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService( Namespace = "http://tempuri.org/" )]
    [WebServiceBinding( ConformsTo = WsiProfiles.BasicProfile1_1 )]
    public class PhoneRank : IHttpHandler
    {
        public void ProcessRequest( HttpContext context )
        {
            context.Response.ContentType = "text/plain";
            string action = GameRequest.GetQueryString( "action" );
            switch( action )
            {
                case "getscorerank":
                    GetScoreRank( context );
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 获取金币排行榜，前50
        /// </summary>
        /// <param name="context"></param>
        protected void GetScoreRank( HttpContext context )
        {
            StringBuilder msg = new StringBuilder( );
            int pageIndex = GameRequest.GetInt( "pageindex" , 1 );
            int pageSize = GameRequest.GetInt( "pagesize" , 10 );
            int userID = GameRequest.GetInt("UserID", 0);
            if( pageIndex <= 0 )
            {
                pageIndex = 1;
            }
            if( pageSize <= 0 )
            {
                pageSize = 10;
            }
            if( pageSize > 50 )
            {
                pageSize = 50;
            }

            //获取用户排行
            string sqlQuery = string.Format("SELECT a.*,b.FaceID,b.Experience,b.MemberOrder,b.GameID,b.UserMedal,b.UnderWrite FROM (SELECT ROW_NUMBER() OVER (ORDER BY Score DESC) as ChartID,UserID,Score FROM GameScoreInfo) a,RYAccountsDB.dbo.AccountsInfo b WHERE a.UserID=b.UserID AND a.UserID={0}", userID);
            DataSet dsUser = FacadeManage.aideTreasureFacade.GetDataSetByWhere( sqlQuery );
            int uChart = 0;
            Int64 uScore = 0;
            int uFaceID = 0;
            int uExperience = 0;
            int memberOrder = 0;
            int gameID = 0;
            int userMedal = 0;
            string underWrite = "";
            Int64 score = 0;
            decimal currency = 0;

            if (dsUser.Tables[0].Rows.Count != 0)
            {
                uChart = Convert.ToInt32(dsUser.Tables[0].Rows[0]["ChartID"]);
                uScore = Convert.ToInt64(dsUser.Tables[0].Rows[0]["Score"]);
                uFaceID = Convert.ToInt32(dsUser.Tables[0].Rows[0]["FaceID"]);
                uExperience = Convert.ToInt32(dsUser.Tables[0].Rows[0]["Experience"]);
                memberOrder = Convert.ToInt32(dsUser.Tables[0].Rows[0]["MemberOrder"]);
                gameID = Convert.ToInt32(dsUser.Tables[0].Rows[0]["GameID"]);
                userMedal = Convert.ToInt32(dsUser.Tables[0].Rows[0]["UserMedal"]);
                underWrite = dsUser.Tables[0].Rows[0]["UnderWrite"].ToString();
                score = GetUserScore(Convert.ToInt32(dsUser.Tables[0].Rows[0]["UserID"]));
                currency = GetUserCurrency(Convert.ToInt32(dsUser.Tables[0].Rows[0]["UserID"]));
            }

            //获取总排行
            DataSet ds = FacadeManage.aideTreasureFacade.GetList( "GameScoreInfo", pageIndex, pageSize, " ORDER BY Score DESC", " ", "UserID,Score" ).PageSet;
            if( ds.Tables[ 0 ].Rows.Count > 0 )
            {
                msg.Append( "[" );

                //添加用户排行
                msg.Append("{\"NickName\":\"" + Fetch.GetNickNameByUserID(userID) + "\",\"Score\":" + uScore + ",\"UserID\":" + userID + ",\"Rank\":" + uChart + ",\"FaceID\":" + uFaceID + ",\"Experience\":" + Fetch.GetGradeConfig(uExperience) + ",\"MemberOrder\":" + memberOrder + ",\"GameID\":" + gameID + ",\"UserMedal\":" + userMedal + ",\"szSign\":\"" + underWrite + "\",\"Score\":" + score + ",\"Currency\":" + currency + "},");
                foreach( DataRow dr in ds.Tables[ 0 ].Rows )
                {
                    msg.Append("{\"NickName\":\"" + Fetch.GetNickNameByUserID(Convert.ToInt32(dr["UserID"])) + "\",\"Score\":" + dr["Score"] + ",\"UserID\":" + dr["UserID"] + ",\"FaceID\":" + Fetch.GetUserGlobalInfo(Convert.ToInt32(dr["UserID"])).FaceID + ",\"Experience\":" + Fetch.GetGradeConfig(Fetch.GetUserGlobalInfo(Convert.ToInt32(dr["UserID"])).Experience) + ",\"MemberOrder\":" + Fetch.GetUserGlobalInfo(Convert.ToInt32(dr["UserID"])).MemberOrder + ",\"GameID\":" + Fetch.GetUserGlobalInfo(Convert.ToInt32(dr["UserID"])).GameID + ",\"UserMedal\":" + Fetch.GetUserGlobalInfo(Convert.ToInt32(dr["UserID"])).UserMedal + ",\"szSign\":\"" + Fetch.GetUserGlobalInfo(Convert.ToInt32(dr["UserID"])).UnderWrite + "\",\"Score\":" + GetUserScore(Convert.ToInt32(dr["UserID"])) + ",\"Currency\":" + GetUserCurrency(Convert.ToInt32(dr["UserID"])) + "},");
                }
                msg.Remove( msg.Length - 1 , 1 );
                msg.Append( "]" );
            }
            else
            {
                msg.Append( "{}" );
            }
            context.Response.Write( msg.ToString( ) );
        }

        /// <summary>
        /// 获取用户金币
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public Int64 GetUserScore(int userID)
        {
            GameScoreInfo model = FacadeManage.aideTreasureFacade.GetTreasureInfo2(userID);
            if (model != null)
            {
                return model.Score;
            }
            return 0;
        }

        /// <summary>
        /// 获取用户游戏豆
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public decimal GetUserCurrency(int userID)
        {
            UserCurrencyInfo model = FacadeManage.aideTreasureFacade.GetUserCurrencyInfo(userID);
            if (model != null)
            {
                return model.Currency;
            }
            return 0;
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
