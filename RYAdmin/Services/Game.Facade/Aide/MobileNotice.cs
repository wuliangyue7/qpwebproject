using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Facade.Aide
{
    /// <summary>
    /// 移动公告类
    /// </summary>
    public class MobileNotice
    {
        /// <summary>
        /// 新闻标题
        /// </summary>
        private string _title;
        public string title
        {
            get
            {
                return _title;
            }
            set 
            {
                _title = value;
            }
        }

        /// <summary>
        /// 发布日期
        /// </summary>
        private DateTime _date;
        public DateTime date
        {
            get 
            {
                return _date;
            }
            set
            {
                _date = value;
            }
        }

        /// <summary>
        /// 公告内容
        /// </summary>
        private string _content;
        public string content
        {
            get
            {
                return _content;
            }
            set
            {
                _content = value;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="startTitle"></param>
        /// <param name="startDate"></param>
        /// <param name="startContent"></param>
        public MobileNotice(string startTitle, DateTime startDate, string startContent)
        {
            _title = startTitle;
            _date = startDate;
            _content = startContent;
        }
    }
}
