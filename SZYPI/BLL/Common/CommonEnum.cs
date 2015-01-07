using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 目的: 用于编写公共使用枚举值
    /// 作者: 姚东
    /// 编写日期：2010-9-10
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
            /// <summary>
            /// 供应商
            /// </summary>
            OuterUser = 3      
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
            Outer = 2,
        }

        /// <summary>
        /// 操作枚举
        /// </summary>
        public enum OperationEnum
        {
            /// <summary>
            /// 默认
            /// </summary>
            DEFAULT = 0,

            /// <summary>
            /// 新增
            /// </summary>
            INSERT = 1,

            /// <summary>
            /// 修改
            /// </summary>
            UPDATE = 2,

            /// <summary>
            /// 删除
            /// </summary>
            DELETE = 3,

            /// <summary>
            /// 查看
            /// </summary>
            VIEW = 4,

            /// <summary>
            /// 查询
            /// </summary>
            SEARCH = 5,

            /// <summary>
            /// 高级查询
            /// </summary>
            ADVSEARCH = 6,

            /// <summary>
            /// 审核
            /// </summary>
            Auth = 7,

            /// <summary>
            /// 确认
            /// </summary>
            Affirm = 8,

        }

        /// <summary>
        /// 部门类型
        /// </summary>
        public enum OUType
        {
            /// <summary>
            ///内部部门
            /// </summary>
            Inner = 1,

            /// <summary>
            /// 外部部门
            /// </summary>
            Outer =2,
        }       

        /// <summary>
        /// 礼品状态
        /// 作者：姚东
        /// 时间：20100920
        /// </summary>
        public enum GiftStatus
        {
            /// <summary>
            /// 开放
            /// </summary>
            Online = 1,
            /// <summary>
            /// 下架
            /// </summary>
            Close = 0,
        }

        /// <summary>
        /// 礼品兑换状态
        /// 作者：姚东
        /// 时间：20100920
        /// </summary>
        public enum GiftExchangeStatus
        {
            /// <summary>
            /// 未兑换
            /// </summary>
            ForExchange = 1,

            /// <summary>
            /// 已兑换
            /// </summary>
            HasExchange = 2,

            /// <summary>
            /// 已作废
            /// </summary>
            HasDrop = 3,
        }

        /// <summary>
        /// 问卷编辑状态
        /// 作者：姚东
        /// 时间：20100922
        /// </summary>
        public enum SurveyState
        {
            /// <summary>
            /// 未生成
            /// </summary>
            NotCreate = 0,

            /// <summary>
            /// 已生成
            /// </summary>
            HasCreated = 1,
        }

        /// <summary>
        /// 问卷活动状态
        /// 作者：姚东
        /// 时间：20100922
        /// </summary>
        public enum SurveyActive
        {
            /// <summary>
            /// 禁用
            /// </summary>
            InActive = 0,

            /// <summary>
            /// 启用
            /// </summary>
            Active = 1,
        }

        /// <summary>
        /// 问卷类型
        /// 作者：韩亮
        /// 时间：20100926
        /// </summary>
        public enum SurveyType
        {
            /// <summary>
            /// 基本调查问卷
            /// </summary>
            Base = 0,

            /// <summary>
            /// 投票问卷
            /// </summary>
            Vote = 1,

            /// <summary>
            /// 测评问卷
            /// </summary>
            Test = 2,

        }

        /// <summary>
        /// 审批状态
        /// 作者：韩亮
        /// 时间：20100928
        /// </summary>
        public enum ApprovalStaus
        {
            /// <summary>
            /// 待审批
            /// </summary>
            Pending = 0,

            /// <summary>
            /// 通过
            /// </summary>
            Pass = 1,

            /// <summary>
            /// 作废
            /// </summary>
            Invalid = 2,

            /// <summary>
            /// 删除
            /// </summary>
            Delete = 3,

        }

        /// <summary>
        /// 答题用户种类
        /// 作者：韩亮
        /// 时间：20101008
        /// </summary>
        public enum AnsweruserKind
        {
            /// <summary>
            /// 会员用户
            /// </summary>
            HiyuanUser = 0,

            /// <summary>
            /// 后台用户
            /// </summary>
            ManagerUser = 1,

            /// <summary>
            /// 匿名用户
            /// </summary>
            AnonymityUser = 2,

        }

        /// <summary>
        /// 友情链接状态
        /// 作者：姚东
        /// 时间：20101103
        /// </summary>
        public enum PartnerStatus
        {
            /// <summary>
            /// 下线
            /// </summary>
            Offline = 0,

            /// <summary>
            /// 上线
            /// </summary>
            Online = 1,
        }

        /// <summary>
        /// 用户类型
        /// 作者：韩亮
        /// 时间：20101103
        /// </summary>
        public enum QSUserType
        {
            /// <summary>
            /// 管理员
            /// </summary>
            AdminUserType = 1,

            /// <summary>
            /// 普通用户
            /// </summary>
            CommonUserType = 2,
        }

        /// <summary>
        /// 问卷审批状态
        /// 作者：韩亮
        /// 时间：20101103
        /// </summary>
        public enum SurveyApprovalStaus
        {
            /// <summary>
            /// 待审批
            /// </summary>
            Waiting = 0,

            /// <summary>
            /// 通过
            /// </summary>
            Pass = 1,

            /// <summary>
            /// 退回
            /// </summary>
            Back = 2,
        }

    }
}
