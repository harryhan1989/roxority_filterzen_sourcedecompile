using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Dundas.Charting.WebControl;
using System.Drawing;
using System.Data;
using BusinessLayer.Chart;
using Business.Helper;
using DBAccess;

namespace BLL.Rule
{
    public class _12355WebChart
    {
        /// <summary>
        /// 获取频数
        /// </summary>
        /// <returns></returns>
        public long GetFrequence(DataTable dtChooseOption, string oID, string pID)
        {
            long frequence = 0;

            DataRow[] dtItemChoosedRows = dtChooseOption.Select(string.Format("menuID={0} and parentmenuid={1}", oID, pID));
            if (dtItemChoosedRows.Length == 1)
            {
                frequence = ConvertHelper.ConvertLong(dtItemChoosedRows[0]["viewcount"]);
            }
            else
            {

            }
            return frequence;
        }

        #region 12355网站访问分析

        public Chart CreateChart(string type, string pID, bool is3D, SeriesChartType seriesChartType, string title)
        {
            Chart chart;

            long nowRentCount = 0;  //题目选项总选择数

            string CalloutAnnotationText = ""; //显示合计数的


            DataTable dtChoosedOption = GetMenuDT(type, pID);  //获取所有已选择项

            nowRentCount = GetCountNum(type, pID);
            CalloutAnnotationText = "合计： " + nowRentCount;


            switch (seriesChartType)
            {
                case SeriesChartType.Pie:
                    chart = GetPie(is3D, title, CalloutAnnotationText);
                    break;
                case SeriesChartType.Column:
                    chart = GetColumn(is3D, title, CalloutAnnotationText);
                    break;
                case SeriesChartType.Doughnut:
                    chart = GetDoughnut(is3D, title, CalloutAnnotationText);
                    break;
                case SeriesChartType.Bar:
                    chart = GetBar(is3D, title, CalloutAnnotationText);
                    break;
                default:
                    chart = GetPie(is3D, title, CalloutAnnotationText);
                    break;
            }


            if (dtChoosedOption != null)
            {
                int j = 0;
                foreach (DataRow dtItemOption in dtChoosedOption.Rows)
                {
                    if (dtChoosedOption != null)
                    {
                        long frequence = ConvertHelper.ConvertLong(dtItemOption["viewcount"]);
                        if (frequence > 0)
                        {
                            setSeries(chart, chart.Series[0], seriesChartType, frequence, nowRentCount, dtItemOption, 1, j);

                            j++;
                        }
                        else
                        {
                            //chart.Series[0].Points.AddY(0);
                            //chart.Series[0].Points[j].Label = "100%";
                            //chart.Series[0].Points[j].LegendText = ConvertHelper.ConvertString(dtItemOption["OptionName"]) + "(" + 0 + "个)";
                            //chart.Series[0].Enabled = false;
                            //j++;
                        }
                    }
                }
            }

            return chart;
        }
        #endregion

        #region 青年生活状况调查统计
        public Chart CreateTop10Survey(bool is3D, SeriesChartType seriesChartType, string title,string tableName)
        {
            Chart chart=new Chart();

            //long totalCount = 100;  //总数

            //string CalloutAnnotationText = ""; //显示合计数的

            //DataSet ds = GetTop10Survey();  //获取所有数据
            
            //CalloutAnnotationText = "合计： " + totalCount.ToString();

            //switch (seriesChartType)
            //{
            //    case SeriesChartType.Pie:
            //        chart = GetPie(is3D, title, CalloutAnnotationText);
            //        break;
            //    case SeriesChartType.Column:
            //        chart = GetColumn(is3D, title, CalloutAnnotationText);
            //        break;
            //    case SeriesChartType.Doughnut:
            //        chart = GetDoughnut(is3D, title, CalloutAnnotationText);
            //        break;
            //    case SeriesChartType.Bar:
            //        chart = GetBar(is3D, title, CalloutAnnotationText);
            //        break;
            //    default:
            //        chart = GetPie(is3D, title, CalloutAnnotationText);
            //        break;
            //}


            //if (ds != null)
            //{
                int j = 0;
                //foreach (DataRow dtItemOption in ds.Tables[tableName].Rows)
                //{
                //    if (ds.Tables[tableName] != null)
                //    {
                //        decimal frequence = ConvertHelper.ConvertDecimal(dtItemOption["Percent"]);
                //        if (frequence > 0)
                //        {
                //            setTop10Series(chart, chart.Series[0], seriesChartType, frequence, totalCount, dtItemOption, 1, j);

                //            j++;
                //        }
                //        else
                //        {
                //        }
                //    }
                //}
            //}

                return chart;
        }
        #endregion

        #region  设置图表内容
        public void setSeries(Chart chart, Series series, SeriesChartType seriesChartType, long frequence, long nowRentCount, DataRow dtRowValue, int CreateKind, int PointsNum)
        {
            if (CreateKind == 1) //青少年问卷模块,选项分析
            {
                switch (seriesChartType)
                {
                    case SeriesChartType.Pie:   //饼图设置数据
                        series.Points.AddY(frequence * 100 / nowRentCount + 1);
                        series.Points[PointsNum].Label = Convert.ToString(Math.Round((double)frequence * 100 / nowRentCount, 2)) + "%";
                        series.Points[PointsNum].LegendText = ConvertHelper.ConvertString(dtRowValue["name"]) + "( " + frequence + ")";
                        return;

                    case SeriesChartType.Column:  //柱图设置数据

                        series.Points.AddY(frequence * 100 / nowRentCount + 1);
                        series.Points[PointsNum].Label = Convert.ToString(Math.Round((double)frequence * 100 / nowRentCount, 2)) + "% (" + frequence + ")";
                        //series.Points[PointsNum].LegendText = ConvertHelper.ConvertString(dtRowValue["OptionName"]) + "( " + frequence + ")";
                        series.Points[PointsNum].AxisLabel = ConvertHelper.ConvertString(dtRowValue["name"]);
                        return;

                    case SeriesChartType.Doughnut:  //圆环设置数据
                        series.Points.AddY(frequence * 100 / nowRentCount + 1);
                        series.Points[PointsNum].Label = Convert.ToString(Math.Round((double)frequence * 100 / nowRentCount, 2)) + "%";
                        series.Points[PointsNum].LegendText = ConvertHelper.ConvertString(dtRowValue["name"]) + "( " + frequence + ")";
                        return;

                    case SeriesChartType.Bar:  //横向柱图设置数据

                        series.Points.AddY(frequence * 100 / nowRentCount + 1);
                        series.Points[PointsNum].Label = Convert.ToString(Math.Round((double)frequence * 100 / nowRentCount, 2)) + "% (" + frequence + ")";
                        //series.Points[PointsNum].LegendText = ConvertHelper.ConvertString(dtRowValue["OptionName"]) + "( " + frequence + ")";
                        series.Points[PointsNum].AxisLabel = ConvertHelper.ConvertString(dtRowValue["name"]);
                        return;
                }
            }
        }

        #endregion

        #region  设置图表内容
        public void setTop10Series(Chart chart, Series series, SeriesChartType seriesChartType, decimal frequence, long nowRentCount, DataRow dtRowValue, int CreateKind, int PointsNum)
        {
            if (CreateKind == 1) //青年生活状况调查统计
            {
                switch (seriesChartType)
                {
                    case SeriesChartType.Pie:   //饼图设置数据
                        series.Points.AddY(frequence * 100 / nowRentCount + 1);
                        series.Points[PointsNum].Label = Convert.ToString(Math.Round((double)frequence * 100 / nowRentCount, 1)) + "%";
                        series.Points[PointsNum].LegendText = ConvertHelper.ConvertString(dtRowValue["AnswerName"]) + "(" + frequence + ")";
                        return;

                    case SeriesChartType.Column:  //柱图设置数据

                        series.Points.AddY(frequence * 100 / nowRentCount + 1);
                        series.Points[PointsNum].Label = Convert.ToString(Math.Round((double)frequence * 100 / nowRentCount, 1)) + "% (" + frequence + ")";                        
                        series.Points[PointsNum].AxisLabel = ConvertHelper.ConvertString(dtRowValue["AnswerName"]);
                        return;

                    case SeriesChartType.Doughnut:  //圆环设置数据
                        series.Points.AddY(frequence * 100 / nowRentCount + 1);
                        series.Points[PointsNum].Label = Convert.ToString(Math.Round((double)frequence * 100 / nowRentCount, 1)) + "%";
                        series.Points[PointsNum].LegendText = ConvertHelper.ConvertString(dtRowValue["AnswerName"]) + "(" + frequence + ")";
                        return;

                    case SeriesChartType.Bar:  //横向柱图设置数据

                        series.Points.AddY(frequence * 100 / nowRentCount + 1);
                        series.Points[PointsNum].Label = Convert.ToString(Math.Round((double)frequence * 100 / nowRentCount, 1)) + "% (" + frequence + ")";
                        series.Points[PointsNum].AxisLabel = ConvertHelper.ConvertString(dtRowValue["AnswerName"]);
                        return;
                }
            }
        }

        #endregion

        #region 创建饼图
        /// <summary>
        /// 创建一个饼图
        /// </summary>
        /// <param name="SID"></param>
        /// <param name="IID"></param>
        /// <returns></returns>
        public Chart GetPie(bool is3D, string title, string CalloutAnnotationText)
        {
            Chart chart = new Chart();

            Title titleChart = new Title();  //设置标题
            titleChart.Text = title;
            chart.Titles.Add(titleChart);

            Series series0 = new Series();
            chart.Series.Add(series0);

            ChartArea ChartArea0 = new ChartArea();
            ChartArea0.Name = "a";
            chart.ChartAreas.Add(ChartArea0);

            Legend Legends0 = new Legend();
            Legends0.Name = "Default";
            chart.Legends.Add(Legends0);

            CalloutAnnotation CalloutAnnotation0 = new CalloutAnnotation();
            CalloutAnnotation0.Name = "Callout1";

            chart.Annotations.Add(CalloutAnnotation0);

            SetPieProperty(chart, is3D);

            CalloutAnnotation0.Text = CalloutAnnotationText;

            return chart;
        }

        /// <summary>
        /// 设置饼图的属性
        /// </summary>
        /// <param name="chart">图表</param>
        public void SetPieProperty(Chart chart, bool is3D)
        {

            chart.AnimationTheme = AnimationTheme.GrowingAndFading;
            chart.ViewStateContent = SerializationContent.All;
            chart.Palette = ChartColorPalette.SemiTransparent;
            chart.ImageUrl = "TempFiles/ChartPic_#SEQ(300,3)";
            chart.ImageType = ChartImageType.Flash;
            chart.Width = 700;
            chart.Height = 280;
            chart.BorderLineColor = Color.LightGray;
            chart.BackGradientEndColor = Color.SaddleBrown;
            chart.Palette = ChartColorPalette.Dundas;
            //chart.AnimationDuration = 4;
            chart.BackColor = ColorTranslator.FromHtml("#C4DDFF");

            chart.Series[0].Type = SeriesChartType.Pie;
            chart.Series[0].XValueType = ChartValueTypes.Double;
            chart.Series[0].YValueType = ChartValueTypes.Double;
            chart.Series[0].Font = new Font("Microsoft Sans Serif", 8);
            //chart.Series[0].CustomAttributes = "LabelStyle=Inside";
            chart.Series[0].BorderColor = Color.FromArgb(120, 50, 50, 50);
            chart.Series[0].ShowLabelAsValue = true;
            chart.Series[0].Points.Clear();

            chart.Titles[0].Font = new Font("宋体", 12);

            chart.Legends[0].BorderColor = Color.Transparent;
            chart.Legends[0].BackColor = Color.Transparent;
            chart.Legends[0].Font = new Font("宋体", 8);

            chart.Annotations[0].TextFont = new Font("Microsoft Sans Serif", 10);
            chart.Annotations[0].BackColor = Color.FromArgb(220, 225, 225, 240);
            chart.Annotations[0].Width = 25;
            chart.Annotations[0].Y = 70;
            chart.Annotations[0].X = 68;

            chart.AnimationStartTime = 0;
            chart.AnimationDuration = 2;
            chart.RepeatAnimation = false;
            chart.AnimationTheme = Dundas.Charting.WebControl.AnimationTheme.GrowingOneByOne;

            //设置区域效果
            foreach (ChartArea chartArea in chart.ChartAreas)
            {
                chartArea.BorderColor = Color.Transparent;
                chartArea.BorderStyle = ChartDashStyle.Solid;
                chartArea.BackColor = Color.Transparent;
                chartArea.AxisY2.LineColor = Color.DimGray;
                chartArea.AxisX2.LineColor = Color.DimGray;
                chartArea.AxisY.LineColor = Color.DimGray;
                chartArea.AxisX.LineColor = Color.DimGray;
                chartArea.AxisY.MajorGrid.LineStyle = ChartDashStyle.Dot;
                chartArea.AxisX.MajorGrid.LineStyle = ChartDashStyle.Dot;
                chartArea.AxisY.MajorTickMark.LineColor = Color.DimGray;
                chartArea.AxisX.MajorTickMark.LineColor = Color.DimGray;


                //设置3D效果            
                if (is3D)
                {
                    chartArea.Area3DStyle.Enable3D = true;
                    chartArea.Area3DStyle.XAngle = 15;
                    chartArea.Area3DStyle.YAngle = 10;
                    chartArea.Area3DStyle.RightAngleAxes = false;
                    chartArea.Area3DStyle.WallWidth = 0;
                    chartArea.Area3DStyle.Clustered = true;
                }


                //chartArea.AxisX.LabelStyle.Font = new Font("宋体", 10);
                //chartArea.AxisX.LabelStyle.FontAngle = 0;
                //chartArea.AxisY.LabelStyle.Format = "P0";
            }

        }
        #endregion

        #region 创建柱形图
        /// <summary>
        /// 创建一个柱形图
        /// </summary>
        /// <param name="SID"></param>
        /// <param name="IID"></param>
        /// <returns></returns>
        public Chart GetColumn(bool is3D, string title, string CalloutAnnotationText)
        {

            Chart chart = new Chart();

            Title titleChart = new Title();  //设置标题
            titleChart.Text = title;
            chart.Titles.Add(titleChart);

            Series series0 = new Series();
            chart.Series.Add(series0);

            ChartArea ChartArea0 = new ChartArea();
            ChartArea0.Name = "a";
            chart.ChartAreas.Add(ChartArea0);

            Legend Legends0 = new Legend();
            Legends0.Name = "Default";
            chart.Legends.Add(Legends0);

            CalloutAnnotation CalloutAnnotation0 = new CalloutAnnotation();
            CalloutAnnotation0.Name = "Callout1";
            chart.Annotations.Add(CalloutAnnotation0);

            CalloutAnnotation0.Text = CalloutAnnotationText;

            SetColumnProperty(chart, is3D);
            return chart;
        }

        /// <summary>
        /// 设置柱形图的属性
        /// </summary>
        /// <param name="chart">图表</param>
        public void SetColumnProperty(Chart chart, bool is3D)
        {

            chart.AnimationTheme = AnimationTheme.GrowingAndFading;
            chart.ViewStateContent = SerializationContent.All;
            chart.Palette = ChartColorPalette.SemiTransparent;
            chart.ImageUrl = "TempFiles/ChartPic_#SEQ(300,3)";
            chart.ImageType = ChartImageType.Flash;
            chart.Width = 700;
            chart.Height = 280;
            chart.BorderLineColor = Color.LightGray;
            chart.BackGradientEndColor = Color.SaddleBrown;
            chart.Palette = ChartColorPalette.Dundas;
            chart.BackColor = ColorTranslator.FromHtml("#C4DDFF");

            chart.Series[0].Type = SeriesChartType.Column;
            chart.Series[0].XValueType = ChartValueTypes.Double;
            chart.Series[0].YValueType = ChartValueTypes.Double;
            chart.Series[0].Font = new Font("Microsoft Sans Serif", 8);
            //chart.Series[0].CustomAttributes = "LabelStyle=Inside";
            chart.Series[0].BorderColor = Color.FromArgb(120, 50, 50, 50);
            chart.Series[0].ShowLabelAsValue = false;
            chart.Series[0].Points.Clear();

            chart.Titles[0].Font = new Font("宋体", 12);

            chart.Legends[0].BorderColor = Color.Transparent;
            chart.Legends[0].BackColor = Color.Transparent;

            chart.Annotations[0].TextFont = new Font("Microsoft Sans Serif", 10);
            chart.Annotations[0].BackColor = Color.FromArgb(220, 225, 225, 240);
            chart.Annotations[0].Width = 25;
            chart.Annotations[0].Y = 70;
            chart.Annotations[0].X = 68;



            chart.AnimationStartTime = 0;
            chart.AnimationDuration = 2;
            chart.RepeatAnimation = false;
            chart.AnimationTheme = Dundas.Charting.WebControl.AnimationTheme.GrowingOneByOne;

            //设置区域效果
            foreach (ChartArea chartArea in chart.ChartAreas)
            {
                chartArea.BorderColor = Color.Transparent;
                chartArea.BorderStyle = ChartDashStyle.Solid;
                chartArea.BackColor = Color.Transparent;
                chartArea.AxisY2.LineColor = Color.DimGray;
                chartArea.AxisX2.LineColor = Color.DimGray;
                chartArea.AxisY.LineColor = Color.DimGray;
                chartArea.AxisX.LineColor = Color.DimGray;
                chartArea.AxisY.MajorGrid.LineStyle = ChartDashStyle.Dot;
                chartArea.AxisX.MajorGrid.LineStyle = ChartDashStyle.Dot;
                chartArea.AxisY.MajorTickMark.LineColor = Color.DimGray;
                chartArea.AxisX.MajorTickMark.LineColor = Color.DimGray;
                chartArea.AxisX.Interval = 1;

                //设置3D效果            
                if (is3D)
                {
                    chartArea.Area3DStyle.Enable3D = true;
                    chartArea.Area3DStyle.XAngle = 15;
                    chartArea.Area3DStyle.YAngle = 10;
                    chartArea.Area3DStyle.RightAngleAxes = false;
                    chartArea.Area3DStyle.WallWidth = 0;
                    chartArea.Area3DStyle.Clustered = true;
                }


                chartArea.AxisX.LabelStyle.Font = new Font("宋体", 10);
                chartArea.AxisX.LabelStyle.FontAngle = 0;
                chartArea.AxisY.LabelStyle.Format = "P0";
            }

        }
        #endregion

        #region 创建环形图
        /// <summary>
        /// 创建一个环形图
        /// </summary>
        /// <param name="SID"></param>
        /// <param name="IID"></param>
        /// <returns></returns>
        public Chart GetDoughnut(bool is3D, string title, string CalloutAnnotationText)
        {

            Chart chart = new Chart();

            Title titleChart = new Title();  //设置标题
            titleChart.Text = title;
            chart.Titles.Add(titleChart);

            Series series0 = new Series();
            chart.Series.Add(series0);

            ChartArea ChartArea0 = new ChartArea();
            ChartArea0.Name = "a";
            chart.ChartAreas.Add(ChartArea0);

            Legend Legends0 = new Legend();
            Legends0.Name = "Default";
            chart.Legends.Add(Legends0);

            CalloutAnnotation CalloutAnnotation0 = new CalloutAnnotation();
            CalloutAnnotation0.Name = "Callout1";
            chart.Annotations.Add(CalloutAnnotation0);

            CalloutAnnotation0.Text = CalloutAnnotationText;

            SetDoughnutProperty(chart, is3D);
            return chart;
        }

        /// <summary>
        /// 设置圆环形的属性
        /// </summary>
        /// <param name="chart">图表</param>
        public void SetDoughnutProperty(Chart chart, bool is3D)
        {

            chart.AnimationTheme = AnimationTheme.GrowingAndFading;
            chart.ViewStateContent = SerializationContent.All;
            chart.Palette = ChartColorPalette.SemiTransparent;
            chart.ImageUrl = "TempFiles/ChartPic_#SEQ(300,3)";
            chart.ImageType = ChartImageType.Flash;
            chart.Width = 700;
            chart.Height = 280;
            chart.BorderLineColor = Color.LightGray;
            chart.BackGradientEndColor = Color.SaddleBrown;
            chart.Palette = ChartColorPalette.Dundas;
            chart.BackColor = ColorTranslator.FromHtml("#C4DDFF");

            chart.Series[0].Type = SeriesChartType.Doughnut;
            chart.Series[0].XValueType = ChartValueTypes.Double;
            chart.Series[0].YValueType = ChartValueTypes.Double;
            chart.Series[0].Font = new Font("Microsoft Sans Serif", 8);
            //chart.Series[0].CustomAttributes = "LabelStyle=Inside";
            chart.Series[0].BorderColor = Color.FromArgb(120, 50, 50, 50);
            chart.Series[0].ShowLabelAsValue = false;
            chart.Series[0].Points.Clear();

            chart.Titles[0].Font = new Font("宋体", 12);

            chart.Legends[0].BorderColor = Color.Transparent;
            chart.Legends[0].BackColor = Color.Transparent;

            chart.Annotations[0].TextFont = new Font("Microsoft Sans Serif", 10);
            chart.Annotations[0].BackColor = Color.FromArgb(220, 225, 225, 240);
            chart.Annotations[0].Width = 25;
            chart.Annotations[0].Y = 70;
            chart.Annotations[0].X = 68;



            chart.AnimationStartTime = 0;
            chart.AnimationDuration = 2;
            chart.RepeatAnimation = false;
            chart.AnimationTheme = Dundas.Charting.WebControl.AnimationTheme.GrowingOneByOne;

            //设置区域效果
            foreach (ChartArea chartArea in chart.ChartAreas)
            {
                chartArea.BorderColor = Color.Transparent;
                chartArea.BorderStyle = ChartDashStyle.Solid;
                chartArea.BackColor = Color.Transparent;
                chartArea.AxisY2.LineColor = Color.DimGray;
                chartArea.AxisX2.LineColor = Color.DimGray;
                chartArea.AxisY.LineColor = Color.DimGray;
                chartArea.AxisX.LineColor = Color.DimGray;
                chartArea.AxisY.MajorGrid.LineStyle = ChartDashStyle.Dot;
                chartArea.AxisX.MajorGrid.LineStyle = ChartDashStyle.Dot;
                chartArea.AxisY.MajorTickMark.LineColor = Color.DimGray;
                chartArea.AxisX.MajorTickMark.LineColor = Color.DimGray;
                chartArea.AxisX.Interval = 1;

                //设置3D效果            
                if (is3D)
                {
                    chartArea.Area3DStyle.Enable3D = true;
                    chartArea.Area3DStyle.XAngle = 15;
                    chartArea.Area3DStyle.YAngle = 10;
                    chartArea.Area3DStyle.RightAngleAxes = false;
                    chartArea.Area3DStyle.WallWidth = 0;
                    chartArea.Area3DStyle.Clustered = true;
                }


                chartArea.AxisX.LabelStyle.Font = new Font("宋体", 10);
                chartArea.AxisX.LabelStyle.FontAngle = 0;
                chartArea.AxisY.LabelStyle.Format = "P0";
            }

        }
        #endregion

        #region 创建横向柱体
        /// <summary>
        /// 创建一个横向柱体
        /// </summary>
        /// <param name="SID"></param>
        /// <param name="IID"></param>
        /// <returns></returns>
        public Chart GetBar(bool is3D, string title, string CalloutAnnotationText)
        {

            Chart chart = new Chart();

            Title titleChart = new Title();  //设置标题
            titleChart.Text = title;
            chart.Titles.Add(titleChart);

            Series series0 = new Series();
            chart.Series.Add(series0);

            ChartArea ChartArea0 = new ChartArea();
            ChartArea0.Name = "a";
            chart.ChartAreas.Add(ChartArea0);

            Legend Legends0 = new Legend();
            Legends0.Name = "Default";
            chart.Legends.Add(Legends0);

            CalloutAnnotation CalloutAnnotation0 = new CalloutAnnotation();
            CalloutAnnotation0.Name = "Callout1";
            chart.Annotations.Add(CalloutAnnotation0);

            CalloutAnnotation0.Text = CalloutAnnotationText;

            SetBarProperty(chart, is3D);
            return chart;
        }

        /// <summary>
        /// 设置横向柱体的属性
        /// </summary>
        /// <param name="chart">图表</param>
        public void SetBarProperty(Chart chart, bool is3D)
        {

            chart.AnimationTheme = AnimationTheme.GrowingAndFading;
            chart.ViewStateContent = SerializationContent.All;
            chart.Palette = ChartColorPalette.SemiTransparent;
            chart.ImageUrl = "TempFiles/ChartPic_#SEQ(300,3)";
            chart.ImageType = ChartImageType.Flash;
            chart.Width = 700;
            chart.Height = 280;
            chart.BorderLineColor = Color.LightGray;
            chart.BackGradientEndColor = Color.SaddleBrown;
            chart.Palette = ChartColorPalette.Dundas;
            chart.BackColor = ColorTranslator.FromHtml("#C4DDFF");

            chart.Series[0].Type = SeriesChartType.Bar;
            chart.Series[0].XValueType = ChartValueTypes.Double;
            chart.Series[0].YValueType = ChartValueTypes.Double;
            chart.Series[0].Font = new Font("Microsoft Sans Serif", 8);
            //chart.Series[0].CustomAttributes = "LabelStyle=Inside";
            chart.Series[0].BorderColor = Color.FromArgb(120, 50, 50, 50);
            chart.Series[0].ShowLabelAsValue = false;
            chart.Series[0].Points.Clear();

            chart.Titles[0].Font = new Font("宋体", 12);

            chart.Legends[0].BorderColor = Color.Transparent;
            chart.Legends[0].BackColor = Color.Transparent;

            chart.Annotations[0].TextFont = new Font("Microsoft Sans Serif", 10);
            chart.Annotations[0].BackColor = Color.FromArgb(220, 225, 225, 240);
            chart.Annotations[0].Width = 25;
            chart.Annotations[0].Y = 70;
            chart.Annotations[0].X = 68;



            chart.AnimationStartTime = 0;
            chart.AnimationDuration = 2;
            chart.RepeatAnimation = false;
            chart.AnimationTheme = Dundas.Charting.WebControl.AnimationTheme.GrowingOneByOne;

            //设置区域效果
            foreach (ChartArea chartArea in chart.ChartAreas)
            {
                chartArea.BorderColor = Color.Transparent;
                chartArea.BorderStyle = ChartDashStyle.Solid;
                chartArea.BackColor = Color.Transparent;
                chartArea.AxisY2.LineColor = Color.DimGray;
                chartArea.AxisX2.LineColor = Color.DimGray;
                chartArea.AxisY.LineColor = Color.DimGray;
                chartArea.AxisX.LineColor = Color.DimGray;
                chartArea.AxisY.MajorGrid.LineStyle = ChartDashStyle.Dot;
                chartArea.AxisX.MajorGrid.LineStyle = ChartDashStyle.Dot;
                chartArea.AxisY.MajorTickMark.LineColor = Color.DimGray;
                chartArea.AxisX.MajorTickMark.LineColor = Color.DimGray;
                chartArea.AxisX.Interval = 1;

                //设置3D效果            
                if (is3D)
                {
                    chartArea.Area3DStyle.Enable3D = true;
                    chartArea.Area3DStyle.XAngle = 15;
                    chartArea.Area3DStyle.YAngle = 10;
                    chartArea.Area3DStyle.RightAngleAxes = false;
                    chartArea.Area3DStyle.WallWidth = 0;
                    chartArea.Area3DStyle.Clustered = true;
                }


                chartArea.AxisX.LabelStyle.Font = new Font("宋体", 10);
                chartArea.AxisX.LabelStyle.FontAngle = 0;
                chartArea.AxisY.LabelStyle.Format = "P0";
            }

        }
        #endregion

        public DataTable GetMenuDT(string type, string pID)
        {
            StringBuilder sql = new StringBuilder(200);
            DataTable dt = null;
            switch (type)
            {
                case "F":
                    sql.Append("select MenuID,Code, LangCode, name, ParentMenuID, ID,isnull(ViewCount,0) as ViewCount from sys_menu where parentmenuid=-1 and menuid not in (0,10,11,12,13,16)");
                    dt = DbHelperSQL.Fill(ConvertHelper.ConvertString(sql));
                    break;
                case "Z":
                    sql.Append(string.Format("select MenuID,Code, LangCode, name, ParentMenuID, ID,isnull(ViewCount,0) as ViewCount from sys_menu where parentmenuid={0}", pID));
                    dt = DbHelperSQL.Fill(ConvertHelper.ConvertString(sql));
                    break;
                case "N":
                    sql.Append(string.Format("select * from (select top 4 Title as name, HotTimes as ViewCount,Code from UV_NewsVistor where code='{0}' order by hottimes desc) as tb union select '其它' as name,sum(hottimes) as ViewCount,'qita' as code  from UV_NewsVistor where code='{0}' and id not in  (select top 4 id from UV_NewsVistor where code='{0}' order by hottimes desc)", pID));
                    dt = DbHelperSQL.Fill(ConvertHelper.ConvertString(sql));
                    break;
            }
            return dt;
        }

        /// <summary>
        /// 获取10大青年生活状况调查统计
        /// 作者：姚东
        /// 时间：20110503
        /// </summary>
        /// <returns></returns>
        public DataSet GetTop10Survey()
        {
            string sql;
            DataSet ds = new DataSet();
            DataTable dt = null;

            //1.2010年青年最关注的十大社会现象
            sql = "select ID,Title,AnswerName,[Percent],xorder from UT_Top10_Survey_Result where Title='2010年青年最关注的十大社会现象' order by xorder";
            dt = DbHelperSQL.Fill(ConvertHelper.ConvertString(sql));
            dt.TableName = "Top1";
            ds.Tables.Add(dt.Copy());

            //2.2010年青年人中十大网络流行语
            sql = "select ID,Title,AnswerName,[Percent],xorder from UT_Top10_Survey_Result where Title='2010年青年人中十大网络流行语' order by xorder";
            dt = DbHelperSQL.Fill(ConvertHelper.ConvertString(sql));
            dt.TableName = "Top2";
            ds.Tables.Add(dt.Copy());

            //3.当前青年人最关心的十大社会热点问题
            sql = "select ID,Title,AnswerName,[Percent],xorder from UT_Top10_Survey_Result where Title='当前青年人最关心的十大社会热点问题' order by xorder";
            dt = DbHelperSQL.Fill(ConvertHelper.ConvertString(sql));
            dt.TableName = "Top3";
            ds.Tables.Add(dt.Copy());

            //4.青年人最渴望获得的十大知识和技能
            sql = "select ID,Title,AnswerName,[Percent],xorder from UT_Top10_Survey_Result where Title='青年人最渴望获得的十大知识和技能' order by xorder";
            dt = DbHelperSQL.Fill(ConvertHelper.ConvertString(sql));
            dt.TableName = "Top4";
            ds.Tables.Add(dt.Copy());

            //5.青年人十大择业标准
            sql = "select ID,Title,AnswerName,[Percent],xorder from UT_Top10_Survey_Result where Title='青年人十大择业标准' order by xorder";
            dt = DbHelperSQL.Fill(ConvertHelper.ConvertString(sql));
            dt.TableName = "Top5";
            ds.Tables.Add(dt.Copy());

            //6.青年人十大择偶标准
            sql = "select ID,Title,AnswerName,[Percent],xorder from UT_Top10_Survey_Result where Title='青年人十大择偶标准' order by xorder";
            dt = DbHelperSQL.Fill(ConvertHelper.ConvertString(sql));
            dt.TableName = "Top6";
            ds.Tables.Add(dt.Copy());

            //7.青年人十大消费支出项目
            sql = "select ID,Title,AnswerName,[Percent],xorder from UT_Top10_Survey_Result where Title='青年人十大消费支出项目' order by xorder";
            dt = DbHelperSQL.Fill(ConvertHelper.ConvertString(sql));
            dt.TableName = "Top7";
            ds.Tables.Add(dt.Copy());

            //8.青年人十大交流工具
            sql = "select ID,Title,AnswerName,[Percent],xorder from UT_Top10_Survey_Result where Title='青年人十大交流工具' order by xorder";
            dt = DbHelperSQL.Fill(ConvertHelper.ConvertString(sql));
            dt.TableName = "Top8";
            ds.Tables.Add(dt.Copy());

            //9.青年人十大休闲娱乐项目
            sql = "select ID,Title,AnswerName,[Percent],xorder from UT_Top10_Survey_Result where Title='青年人十大休闲娱乐项目' order by xorder";
            dt = DbHelperSQL.Fill(ConvertHelper.ConvertString(sql));
            dt.TableName = "Top9";
            ds.Tables.Add(dt.Copy());

            //10.青年人十大幸福指数
            sql = "select ID,Title,AnswerName,[Percent],xorder from UT_Top10_Survey_Result where Title='青年人十大幸福指数' order by xorder";
            dt = DbHelperSQL.Fill(ConvertHelper.ConvertString(sql));
            dt.TableName = "Top10";
            ds.Tables.Add(dt.Copy());

            return ds;
        }

        /// <summary>
        /// 获取前10条记录
        /// </summary>
        /// <param name="pID"></param>
        /// <returns></returns>
        public DataTable GetMenuDT(string pID)
        {
            StringBuilder sql = new StringBuilder(200);
            DataTable dt = null;
            sql.Append(string.Format("select * from (select * from (select top 10 Title as name, HotTimes as ViewCount,Code from UV_NewsVistor where code='{0}' order by hottimes desc) as tb union select '其它' as name,sum(hottimes) as ViewCount,'qita' as code  from UV_NewsVistor where code='{0}' and id not in  (select top 10 id from UV_NewsVistor where code='{0}' order by hottimes desc)) as tb1 order by viewcount desc", pID));
            dt = DbHelperSQL.Fill(ConvertHelper.ConvertString(sql));
            return dt;
        }

        public long GetCountNum(string type, string pID)
        {
            long nowRentCount = 0;
            StringBuilder sql = new StringBuilder(200);
            DataTable dt = null;
            switch (type)
            {
                case "F":
                    sql.Append("select MenuID,Code, LangCode, name, ParentMenuID, ID,isnull(ViewCount,0) as ViewCount from sys_menu where parentmenuid=-1 and menuid not in (0,10,11,12,13,16)");
                    dt = DbHelperSQL.Fill(ConvertHelper.ConvertString(sql));
                    break;
                case "Z":
                    sql.Append(string.Format("select MenuID,Code, LangCode, name, ParentMenuID, ID,isnull(ViewCount,0) as ViewCount from sys_menu where parentmenuid={0}", pID));
                    dt = DbHelperSQL.Fill(ConvertHelper.ConvertString(sql));
                    break;
                case "N":
                    sql.Append(string.Format("select * from (select top 4 Title as name, HotTimes as ViewCount,Code from UV_NewsVistor where code='{0}' order by hottimes desc) as tb union select '其它' as name,sum(hottimes) as ViewCount,'qita' as code  from UV_NewsVistor where code='{0}' and id not in  (select top 4 id from UV_NewsVistor where code='{0}' order by hottimes desc)", pID));
                    dt = DbHelperSQL.Fill(ConvertHelper.ConvertString(sql));
                    break;
            }
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    nowRentCount = nowRentCount + ConvertHelper.ConvertLong(dr["viewcount"]);
                }
                return nowRentCount;
            }
            else
            {
                return nowRentCount;
            }
        }

        public DataTable GetZMenuByPID(string pID)
        {
            StringBuilder sql = new StringBuilder(200);
            DataTable dt = null;
            sql.Append(string.Format("select MenuID,Code, LangCode, name, ParentMenuID, ID,isnull(ViewCount,0) as ViewCount from sys_menu where parentmenuid={0}", pID));
            dt = DbHelperSQL.Fill(ConvertHelper.ConvertString(sql));
            return dt;
        }

        public DataTable GetAllFMenu()
        {
            StringBuilder sql = new StringBuilder(200);
            DataTable dt = null;
            sql.Append("select MenuID,Code, LangCode, name, ParentMenuID, ID,isnull(ViewCount,0) as ViewCount from sys_menu where parentmenuid=-1 and menuid not in (0,10,11,12,13,16)");
            dt = DbHelperSQL.Fill(ConvertHelper.ConvertString(sql));
            return dt;
        }
    }
}
