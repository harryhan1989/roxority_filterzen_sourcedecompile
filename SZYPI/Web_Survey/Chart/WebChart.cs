using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Dundas.Charting.WebControl;
using System.Drawing;
using Business.Helper;
using System.Data;
using BusinessLayer.Chart;
using BusinessLayer.Survey;

namespace Web_Survey.WebChart
{
    public class CreateWebChart
    {

        /// <summary>
        /// 获取频数
        /// </summary>
        /// <returns></returns>
        public long GetFrequence(DataTable dtChooseOption, string IID, string ItemValue)
        {
            long frequence = 0;

            DataRow[] dtItemChoosedRows = dtChooseOption.Select(string.Format("IID={0} and ItemValue={1}", IID, ItemValue));
            if (dtItemChoosedRows.Length == 1)
            {
                frequence = ConvertHelper.ConvertLong(dtItemChoosedRows[0]["CountItemValue"]);
            }
            else
            {

            }
            return frequence;
        }

        #region 青少年系统选项统计表统一创建表入口
        public Chart YFICreateChart(string SID, string IID, bool is3D, SeriesChartType seriesChartType, DataTable dtChoosedOption, DataTable SurveyTable)
        {
            Chart chart;

            long nowRentCount = 0;  //题目选项总选择数
            string SurveryName = "";  //问卷名称
            string title = ""; //图表的标题，题目标题
            string CalloutAnnotationText = ""; //显示合计数的


            //DataTable dtChoosedOption = new WebChart_Layer().GetChoosedOption(ConvertHelper.ConvertString(SID));  //获取所有已选择项
            //DataTable SurveyTable = new WebChart_Layer().GetSurveyTableName(ConvertHelper.ConvertString(SID));  //获取问卷表
            DataTable SurveyItems = new WebChart_Layer().GetAllSurveyTtem(ConvertHelper.ConvertString(SID)); //获取问卷的所有题目

            if (SurveyTable.Rows.Count == 1)
            {
                SurveryName = ConvertHelper.ConvertString(SurveyTable.Rows[0]["SurveyName"]);
            }

            DataRow[] drItem = SurveyItems.Select(string.Format("IID={0}", IID));
            if (drItem.Length > 0)
            {
                title = ConvertHelper.ConvertString(drItem[0]["ItemName"]);
            }

            DataRow[] dataItemRows = dtChoosedOption.Select(string.Format("IID={0}", ConvertHelper.ConvertString(IID)));
            if (dataItemRows != null)
            {
                for (int i = 0; i < dataItemRows.Length; i++)
                {
                    nowRentCount = nowRentCount + ConvertHelper.ConvertLong(dataItemRows[i]["CountItemValue"]);
                }
            }
            CalloutAnnotationText = "合计：选择数 " + nowRentCount;


            switch (seriesChartType)
            {
                case SeriesChartType.Pie:
                    chart = GetPie(is3D, title, CalloutAnnotationText);
                    break;
                case SeriesChartType.Column:
                    chart = GetColumn( is3D, title, CalloutAnnotationText);
                    break;
                case SeriesChartType.Doughnut:
                    chart = GetDoughnut(is3D, title, CalloutAnnotationText);
                    break;
                case SeriesChartType.Bar:
                    chart = GetBar( is3D, title, CalloutAnnotationText);
                    break;
                default:
                    chart = GetPie( is3D, title, CalloutAnnotationText);
                    break;
            }


            DataTable dtTtemOption = new WebChart_Layer().GetTtemOption(ConvertHelper.ConvertString(SID), ConvertHelper.ConvertString(IID));  //获取题目的所有选项
            if (dtTtemOption != null)
            {
                int j = 0;
                foreach (DataRow dtItemOption in dtTtemOption.Rows)
                {
                    if (dtChoosedOption != null)
                    {
                        long frequence = GetFrequence(dtChoosedOption, ConvertHelper.ConvertString(IID), ConvertHelper.ConvertString(dtItemOption["OID"]));
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

        #region 热线分析统计图表统一入口
        public Chart HLACreateChart( bool is3D, SeriesChartType seriesChartType, string StatisticsClassName, DateTime BeginDate, DateTime EndDate, string StatisticsClass)
        {
            Chart chart;

            long nowRentCount = 0;  //题目选项总选择数
            string title = ""; //图表的标题，题目标题
            string CalloutAnnotationText = ""; //显示合计数的

            DataTable StatisticsDetail = new HotlineAnalysis_Layer().GetStatisticsDetail(BeginDate, EndDate, StatisticsClass);  //获取需要热线分析的数据

            title = StatisticsClassName;
            if (StatisticsDetail != null)
            {
                for (int i = 0; i < StatisticsDetail.Rows.Count; i++)
                {
                    nowRentCount = nowRentCount + ConvertHelper.ConvertLong(StatisticsDetail.Rows[i]["CountStatisticsClass"]);
                }
            }
            CalloutAnnotationText = "合计：选择数 " + nowRentCount;


            switch (seriesChartType)
            {
                case SeriesChartType.Pie:
                    chart = GetPie(is3D, title, CalloutAnnotationText);
                    break;
                case SeriesChartType.Column:
                    chart = GetColumn( is3D, title, CalloutAnnotationText);
                    break;
                case SeriesChartType.Doughnut:
                    chart = GetDoughnut( is3D, title, CalloutAnnotationText);
                    break;
                case SeriesChartType.Bar:
                    chart = GetBar(is3D, title, CalloutAnnotationText);
                    break;
                default:
                    chart = GetPie( is3D, title, CalloutAnnotationText);
                    break;
            }

            if (StatisticsDetail != null)
            {
                int j = 0;
                foreach (DataRow dtItem in StatisticsDetail.Rows)
                {
                    if (StatisticsDetail != null)
                    {
                        long frequence = ConvertHelper.ConvertLong(dtItem["CountStatisticsClass"]);
                        if (frequence > 0)
                        {
                            setSeries(chart, chart.Series[0], seriesChartType, frequence, nowRentCount, dtItem, 2, j);

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

        #region  设置图表内容
        public void setSeries(Chart chart, Series series, SeriesChartType seriesChartType, long frequence, long nowRentCount, DataRow dtRowValue, int CreateKind, int PointsNum)
        {
            if (CreateKind == 1) //青少年问卷模块,选项分析
            {
                switch (seriesChartType)
                {
                    case SeriesChartType.Pie:   //饼图设置数据
                        series.Points.AddY(frequence * 100 / nowRentCount);
                        series.Points[PointsNum].Label = Convert.ToString(Math.Round((double)frequence * 100 / nowRentCount, 2)) + "%";
                        series.Points[PointsNum].LegendText = ConvertHelper.ConvertString(dtRowValue["OptionName"]) + "( " + frequence + ")";
                        return;

                    case SeriesChartType.Column:  //柱图设置数据
                        
                        series.Points.AddY(frequence *100 / nowRentCount);
                        series.Points[PointsNum].Label = Convert.ToString(Math.Round((double)frequence * 100 / nowRentCount, 2)) + "% (" + frequence+")";
                        //series.Points[PointsNum].LegendText = ConvertHelper.ConvertString(dtRowValue["OptionName"]) + "( " + frequence + ")";
                        series.Points[PointsNum].AxisLabel = ConvertHelper.ConvertString(dtRowValue["OptionName"]);
                        return;

                    case SeriesChartType.Doughnut:  //圆环设置数据
                        series.Points.AddY(frequence * 100 / nowRentCount);
                        series.Points[PointsNum].Label = Convert.ToString(Math.Round((double)frequence * 100 / nowRentCount, 2)) + "%";
                        series.Points[PointsNum].LegendText = ConvertHelper.ConvertString(dtRowValue["OptionName"]) + "( " + frequence + ")";
                        return;

                    case SeriesChartType.Bar:  //横向柱图设置数据

                        series.Points.AddY(frequence * 100 / nowRentCount);
                        series.Points[PointsNum].Label = Convert.ToString(Math.Round((double)frequence * 100 / nowRentCount, 2)) + "% (" + frequence + ")";
                        //series.Points[PointsNum].LegendText = ConvertHelper.ConvertString(dtRowValue["OptionName"]) + "( " + frequence + ")";
                        series.Points[PointsNum].AxisLabel = ConvertHelper.ConvertString(dtRowValue["OptionName"]);
                        return;
                }
            }
            else if (CreateKind == 2) //热线分析模块
            {
                switch (seriesChartType)
                {
                    case SeriesChartType.Pie:   //饼图设置数据
                        series.Points.AddY(frequence * 100 / nowRentCount);
                        series.Points[PointsNum].Label = Convert.ToString(Math.Round((double)frequence * 100 / nowRentCount, 2)) + "%";
                        series.Points[PointsNum].LegendText = ConvertHelper.ConvertString(dtRowValue["StatisticsClass"]) + "( " + frequence + ")";
                        return;

                    case SeriesChartType.Column:  //柱图设置数据

                        series.Points.AddY(frequence * 100 / nowRentCount);
                        series.Points[PointsNum].Label = Convert.ToString(Math.Round((double)frequence * 100 / nowRentCount, 2)) + "% (" + frequence + ")";
                        //series.Points[PointsNum].LegendText = ConvertHelper.ConvertString(dtRowValue["OptionName"]) + "( " + frequence + ")";
                        series.Points[PointsNum].AxisLabel = ConvertHelper.ConvertString(dtRowValue["StatisticsClass"]);
                        return;

                    case SeriesChartType.Doughnut:  //圆环设置数据
                        series.Points.AddY(frequence * 100 / nowRentCount);
                        series.Points[PointsNum].Label = Convert.ToString(Math.Round((double)frequence * 100 / nowRentCount, 2)) + "%";
                        series.Points[PointsNum].LegendText = ConvertHelper.ConvertString(dtRowValue["StatisticsClass"]) + "( " + frequence + ")";
                        return;

                    case SeriesChartType.Bar:  //横向柱图设置数据

                        series.Points.AddY(frequence * 100 / nowRentCount);
                        series.Points[PointsNum].Label = Convert.ToString(Math.Round((double)frequence * 100 / nowRentCount, 2)) + "% (" + frequence + ")";
                        //series.Points[PointsNum].LegendText = ConvertHelper.ConvertString(dtRowValue["OptionName"]) + "( " + frequence + ")";
                        series.Points[PointsNum].AxisLabel = ConvertHelper.ConvertString(dtRowValue["StatisticsClass"]);
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
        public Chart GetPie( bool is3D, string title, string CalloutAnnotationText)
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
        public Chart GetDoughnut( bool is3D, string title, string CalloutAnnotationText)
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
    }
}
