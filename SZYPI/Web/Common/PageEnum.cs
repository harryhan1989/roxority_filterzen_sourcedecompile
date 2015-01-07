using System;

namespace Web.Common
{
    public class PageEnum
    {
        /// <summary>
        /// 下拉框类型
        /// </summary>
        public enum DownLinkType
        {
            /// <summary>
            /// 各级部门网站
            /// </summary>
            Dept = 1,

            /// <summary>
            /// 文化部门网站
            /// </summary>
            Culture = 2,

            /// <summary>
            /// 友好文化网站
            /// </summary>
            Friend = 3,

            /// <summary>
            /// 基层单位网站
            /// </summary>
            Basic = 4,

            /// <summary>
            /// 推荐媒体网站
            /// </summary>
            Media = 5,

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
            /// 文化艺术
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
        /// 调查类型
        /// </summary>
        public enum ResearchType
        {            
            /// <summary>
            /// 文化动态
            /// </summary>
            CultureNews = 1,
            /// <summary>
            /// 文化民生
            /// </summary>
            CultureLive = 2,
            /// <summary>
            /// 文化产业
            /// </summary>
            CultureIndustry = 3,
            /// <summary>
            /// 文化遗产
            /// </summary>
            CultureHeritage = 4,
            /// <summary>
            /// 广播影视
            /// </summary>
            RadioTV = 5,
            /// <summary>
            /// 新闻出版
            /// </summary>
            NewsPublic = 6,
            /// <summary>
            /// 文艺创作
            /// </summary>
            CultureCreation = 7,
            /// <summary>
            /// 文化市场
            /// </summary>
            CulturalMarket = 8,
            /// <summary>
            /// 文化交流
            /// </summary>
            CulturalExchange = 9,
            /// <summary>
            /// 文化人才
            /// </summary>
            CultureTalent = 10,
            /// <summary>
            /// 文化研究
            /// </summary>
            CulturalStudies = 11,
            /// <summary>
            /// 文化专题
            /// </summary>
            CulturalTopics = 12,
        }
    }
}
