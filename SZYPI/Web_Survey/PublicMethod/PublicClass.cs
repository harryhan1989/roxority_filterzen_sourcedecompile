using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using BLL.Entity;

namespace Web_Survey.PublicMethod
{
    /// <summary>
    /// 生成公共方法
    /// </summary>
    public class PublicClass
    {
        /// <summary>
        /// 动态添加Table
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public Table AddTable(TableAddEntity TableAdd)
        {
            Table table = new Table();
            if (TableAdd.TableWidth != null)
            {
                table.Width = TableAdd.TableWidth;
            }
            if (TableAdd.TableHeight != null)
            {
                table.Height = TableAdd.TableHeight;
            }
            if (TableAdd.TableID != null)
            {
                table.ID = TableAdd.TableID;
            }
            if (TableAdd.TableCssCalss != null)
            {
                table.CssClass = TableAdd.TableCssCalss;
            }
            if (TableAdd.TableAddRows != null)
            {
                foreach (TableAddRowEntity TableAddRow in TableAdd.TableAddRows)
                {
                    table = AddRow(table, TableAddRow);
                }
            }

            return table;
        }

        /// <summary>
        /// 动态添加Table的行
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public Table AddRow(Table table, TableAddRowEntity TableAddRow)
        {
            TableRow row = new TableRow();
            if (TableAddRow.TableRowWidth != null)
            {
                row.Width = TableAddRow.TableRowWidth;
            }
            if (TableAddRow.TableRowHeight != null)
            {
                row.Height = TableAddRow.TableRowHeight;
            }
            if (TableAddRow.TableAddRowCssCalss != null)
            {
                row.CssClass = TableAddRow.TableAddRowCssCalss;
            }

            table.Rows.Add(row);
            if (TableAddRow.TableAddCells!=null)
            {
                foreach (TableAddCellEntity TableAddCell in TableAddRow.TableAddCells)
                {
                    table = AddCell(table, row, TableAddCell);
                }
            }

            return table;
        }

        /// <summary>
        /// 动态添加表的列集合
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public Table AddCell(Table table, TableRow row, TableAddCellEntity TableAddCell)
        {
            TableCell cell = new TableCell();
            if (TableAddCell.TableCellContent != null)
            {
                cell.Text = TableAddCell.TableCellContent;
            }
            if (TableAddCell.TableCellWidth != null)
            {
                cell.Width = TableAddCell.TableCellWidth;
            }
            if (TableAddCell.TableCellHight != null)
            {
                cell.Height = TableAddCell.TableCellHight;
            }
            if (TableAddCell.TableCellHorizontalAlign!=null)
            {
                cell.HorizontalAlign = TableAddCell.TableCellHorizontalAlign;
            }
            if (TableAddCell.TableAddCellCssCalss != null)
            {
                cell.CssClass = TableAddCell.TableAddCellCssCalss;
            }
            row.Cells.Add(cell);
            return table;
        }
    }
}
