using System;

namespace BusinessLayer.Web.Common
{
    /// <summary>
    /// 目的：用于编写公共使用枚举值
    /// 作者：刘娟
    /// 编写日期：2010-4-6
    /// </summary>
    public class CommonEnum
    {
        /// <summary>
        /// 用户类型
        /// </summary>
        public enum UserType
        {
            /// <summary>
            /// 系统管理员
            /// </summary>
            Admin = 1,
            /// <summary>
            /// 内部员工
            /// </summary>
            InnerUser = 2,
        }

        /// <summary>
        /// 信息状态
        /// </summary>
        public enum InfoStatus
        {
            /// <summary>
            /// 保存
            /// </summary>
            Save = 1,
            /// <summary>
            /// 发布
            /// </summary>
            Publish = 2,
            /// <summary>
            /// 暂停
            /// </summary>
            Pause = 3,
            /// <summary>
            /// 提交
            /// </summary>
            Submit = 4,
        }

        /// <summary>
        /// 栏目类型
        /// </summary>
        public enum ColumnCategory
        {
            /// <summary>
            /// 机构概况
            /// </summary>
            OrgGeneral = 1,

            /// <summary>
            /// 政府信息公开
            /// </summary>
            GovPublic = 2,

            /// <summary>
            /// 便民服务
            /// </summary>
            PeopleService = 3,

            /// <summary>
            /// 公告公示
            /// </summary>
            PublicNotice = 4,

            /// <summary>
            /// 文化民生
            /// </summary>
            CulturePeople = 5,

            /// <summary>
            /// 文化产业
            /// </summary>
            CultureIndustry = 6,

            /// <summary>
            /// 文化遗产
            /// </summary>
            CultureHeritage = 7,

            /// <summary>
            /// 广播影视
            /// </summary>
            BroadcastTele = 8,

            /// <summary>
            /// 新闻出版
            /// </summary>
            NewsMedia = 9,

            /// <summary>
            /// 文化创作
            /// </summary>
            CultureWrite = 10,

            /// <summary>
            /// 文化市场
            /// </summary>
            CultureMarket = 11,

            /// <summary>
            /// 文化交流
            /// </summary>
            CultureExchange = 12,

            /// <summary>
            /// 文化人才
            /// </summary>
            CultureTalents = 13,

            /// <summary>
            /// 文化研究
            /// </summary>
            CultureResearch = 14,

            /// <summary>
            /// 文化专题
            /// </summary>
            CultureTopic = 15,

            /// <summary>
            /// 公众监督
            /// </summary>
            PublicScrutiny = 16,

        }

        /// <summary>
        /// 是否首页显示
        /// </summary>
        public enum IsPageShow
        {
            /// <summary>
            /// 是
            /// </summary>
            IsPage = 1,
            /// <summary>
            /// 否
            /// </summary>
            IsNotPage = 0,
        }

        /// <summary>
        /// 浮动广告状态
        /// </summary>
        public enum AdvertStatus
        {
            /// <summary>
            /// 在线
            /// </summary>
            OnLine = 1,

            /// <summary>
            /// 关闭
            /// </summary>
            Closed = 2,
        }

        /// <summary>
        /// 浮动广告类型
        /// </summary>
        public enum AdvertType
        {
            /// <summary>
            /// 横幅
            /// </summary>
            NoIcon=1,
            /// <summary>
            /// 漂浮
            /// </summary>
            FloatIcon=2,
        }

        /// <summary>
        /// 菜单类型
        /// </summary>
        public enum MenuType
        {
            /// <summary>
            /// 内部用户菜单
            /// </summary>
            Inner = 1,
            /// <summary>
            /// 企业用户菜单
            /// </summary>
            Outer = 2
        }
    }
}
