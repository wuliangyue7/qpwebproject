using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Drawing;
using System.Web.SessionState;

using Game.Facade;
using Game.Utils;
using Game.Entity.Accounts;
using Game.Entity.PlatformManager;

namespace Game.Web.Ashx
{
    /// <summary>
    /// 获取自定义头像
    /// </summary>
    [WebService( Namespace = "http://tempuri.org/" )]
    [WebServiceBinding( ConformsTo = WsiProfiles.BasicProfile1_1 )]
    public class UserFace : IHttpHandler , IRequiresSessionState
    {
        public void ProcessRequest( HttpContext context )
        {
            context.Response.ContentType = "application/json";

            //验证登陆

            if( !FacadeManage.aidePlatformManagerFacade.CheckedUserLogon() )
            {
                return;
            }

            //自定义头像
            int customID = GameRequest.GetInt( "customid" , 0 );
            if( customID == 0 )
            {
                return;
            }

            AccountsFacade accountsFacade = new AccountsFacade();
            AccountsFace faceModel = accountsFacade.GetAccountsFace( customID );
            if( faceModel == null )
            {
                return;
            }
            else
            {
                byte[ ] faceByte = ( byte[ ] )faceModel.CustomFace;
                
                // 新建画布
                int width = 48;
                int height = 48;
                Bitmap bitmap = new Bitmap( width , height );
                
                // 循环像素
                int site = 4;
                for( int y = 0; y < 48; y++ )
                {
                    for( int x = 0; x < 48; x++ )
                    {
                        byte b = faceByte[ site - 4 ];
                        byte g = faceByte[ site - 3 ];
                        byte r = faceByte[ site - 2 ];
                        bitmap.SetPixel( x , y , Color.FromArgb( r , g , b ) );
                        site = site + 4;
                    }
                }

                // 输出图片
                System.IO.MemoryStream ms = new System.IO.MemoryStream( );
                bitmap.Save( ms , System.Drawing.Imaging.ImageFormat.Bmp );
                context.Response.ClearContent( );
                context.Response.ContentType = "image/jpeg";
                context.Response.BinaryWrite( ms.ToArray( ) );
                bitmap.Dispose( );
            }
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