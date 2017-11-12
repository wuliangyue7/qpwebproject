using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Game.Utils;
using Game.Entity.PlatformManager;
using Game.Facade;
using Game.Entity.Treasure;
using System.Web.SessionState;

namespace Game.Web.Tools
{
    /// <summary>
    /// 后台获取数据
    /// </summary>
    [WebService( Namespace = "http://tempuri.org/" )]
    [WebServiceBinding( ConformsTo = WsiProfiles.BasicProfile1_1 )]
    public class GetData : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest( HttpContext context )
        {
            //验证是否登录
            if( !FacadeManage.aidePlatformManagerFacade.CheckedUserLogon() )
            {
                return;
            }

            //执行操作逻辑
            string action = GameRequest.GetQueryString( "action" ).ToLower();
            switch( action )
            {
                case "getcardimage":
                    GetCardImage( context );
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 获取实卡展示图片
        /// </summary>
        /// <param name="context"></param>
        private void GetCardImage( HttpContext context ) 
        {
            //获取类类型ID
            int cardTypeID = GameRequest.GetInt( "param", 0 );
            if( cardTypeID == 0 ) 
            {
                return;
            }

            TreasureFacade aideTreasureFacade = new TreasureFacade( );
            GlobalLivcard cardType = aideTreasureFacade.GetGlobalLivcardInfo( cardTypeID );
            if( cardType == null )
                return;

            //if( cardType.Image != null )
            //{
            //    context.Response.Clear();
            //    context.Response.ContentType = "image/Bmp";
            //    context.Response.BinaryWrite( cardType.Image );
            //    context.Response.End();
            //}
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
