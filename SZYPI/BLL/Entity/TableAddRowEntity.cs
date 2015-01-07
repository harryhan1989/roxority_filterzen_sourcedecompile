using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace BLL.Entity
{
    /// <summary>
    /// 添加Table的参数封装实体
    /// </summary>
    [Serializable]
    public class TableAddEntity
    {
        /// <summary>
        /// 添加表格的宽度
        /// </summary>
        public Unit TableWidth
        {
            get;
            set;
        }
        /// <summary>
        /// 添加表格的高度
        /// </summary>
        public Unit TableHeight
        {
            get;
            set;
        }
        /// <summary>
        /// 设置表格的ID
        /// </summary>
        public String TableID
        {
            get;
            set;
        }
        /// <summary>
        /// 设置表格的CssCalss
        /// </summary>
        public String TableCssCalss
        {
            get;
            set;
        }
        /// <summary>
        /// 添加行的所有行的数据集合
        /// </summary>
        public List<TableAddRowEntity> TableAddRows
        {
            get;
            set;
        }
    }
    /// <summary>
    /// 添加TableRow的参数封装实体
    /// </summary>
    [Serializable]
    public class TableAddRowEntity
    {
        /// <summary>
        /// 添加行的宽度
        /// </summary>
        public Unit TableRowWidth
        {
            get;
            set;
        }
        /// <summary>
        /// 添加行的高度
        /// </summary>
        public Unit TableRowHeight
        {
            get;
            set;
        }
        /// <summary>
        /// 添加行的所有列的数据集合
        /// </summary>
        public List<TableAddCellEntity> TableAddCells
        {
            get;
            set;
        }
        /// <summary>
        /// 设置表格行的CssCalss
        /// </summary>
        public String TableAddRowCssCalss
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 添加TableCell的参数封装实体
    /// </summary>
    [Serializable]
    public class TableAddCellEntity
    {
        /// <summary>
        /// 添加行的单元格的内容
        /// </summary>
        public String TableCellContent
        {
            get;
            set;
        }
        /// <summary>
        /// 添加行的单元格的宽度
        /// </summary>
        public Unit TableCellWidth
        {
            get;
            set;
        }
        /// <summary>
        /// 添加行的单元格的高度
        /// </summary>
        public Unit TableCellHight
        {
            get;
            set;
        }
        /// <summary>
        /// 添加行的单元格的水平对齐方式
        /// </summary>
        public HorizontalAlign TableCellHorizontalAlign
        {
            get;
            set;
        }
        /// <summary>
        /// 设置表格列的CssCalss
        /// </summary>
        public String TableAddCellCssCalss
        {
            get;
            set;
        }
    }
}
