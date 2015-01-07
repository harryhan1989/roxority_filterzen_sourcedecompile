using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Dundas.Charting.WebControl;
using Business.Helper;
using System.Data;
using BusinessLayer.Chart;
using System.Drawing;

namespace WebManage.Survey.WebChart
{
    public partial class ItemChoosedScale : System.Web.UI.Page
    {
        protected override void OnInit(EventArgs e)
        {
            InitPage();
            base.OnInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //this.InitPage();   
        }

        /// <summary>
        /// 初始加载页
        /// </summary>
        public void InitPage()
        {
            //获取所有题目，根据SID
            //long SID = ConvertHelper.ConvertLong(Request.QueryString["SID"]);
            long SID = 96;
            string SurveryName = "";


            DataTable dtChoosedOption = new WebChart_Layer().GetChoosedOption(ConvertHelper.ConvertString(SID),"");
            long nowRentCount = 0;


            DataTable SurveyTable = new WebChart_Layer().GetSurveyTableName(ConvertHelper.ConvertString(SID));
            if (SurveyTable.Rows.Count == 1)
            {
                SurveryName = ConvertHelper.ConvertString(SurveyTable.Rows[0]["SurveyName"]);
            }

            DataTable SurveyItems = new WebChart_Layer().GetAllSurveyTtem(ConvertHelper.ConvertString(SID)); //获取问卷的所有题目
            if (SurveyItems.Rows.Count > 0)
            {
                foreach (DataRow drSurveyItem in SurveyItems.Rows)
                {
                    Chart chart = new Chart();
                    chart.Titles.Add(ConvertHelper.ConvertString(drSurveyItem["ItemName"]));

                    Series series0 = new Series();
                    //series0.Type =SeriesChartType.Pie;
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

                    SetChartProperty(chart);

                    DataRow[] dataItemRows = dtChoosedOption.Select(string.Format("IID={0}", ConvertHelper.ConvertString(drSurveyItem["IID"])));
                    if (dataItemRows != null)
                    {
                        for (int i = 0; i < dataItemRows.Length; i++)
                        {
                            nowRentCount = nowRentCount + ConvertHelper.ConvertLong(dataItemRows[i]["CountItemValue"]);
                        }
                    }

                    DataTable dtTtemOption = new WebChart_Layer().GetTtemOption(ConvertHelper.ConvertString(SID), ConvertHelper.ConvertString(drSurveyItem["IID"]));  //获取题目的所有选项
                    if (dtTtemOption != null)
                    {
                        int j = 0;
                        foreach (DataRow dtItemOption in dtTtemOption.Rows)
                        {
                            if (dtChoosedOption != null)
                            {
                                DataRow[] dataRows = dtChoosedOption.Select(string.Format("IID={0} and ItemValue={1}", ConvertHelper.ConvertString(drSurveyItem["IID"]), ConvertHelper.ConvertString(dtItemOption["OID"])));
                                if (dataRows.Length > 0)
                                {
                                    chart.Series[0].Points.AddY(Convert.ToInt64(dataRows[0]["CountItemValue"]) * 100 / nowRentCount);
                                    chart.Series[0].Points[j].Label = Convert.ToString(Math.Round(Convert.ToDouble(dataRows[0]["CountItemValue"]) * 100 / nowRentCount, 2)) + "%";
                                    chart.Series[0].Points[j].LegendText = ConvertHelper.ConvertString(dtItemOption["OptionName"]) + "(" + dataRows[0]["CountItemValue"].ToString() + "个)";
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
                        j = 0;
                    }

                    Page.Controls.Add(chart);

                    nowRentCount = 0;
                }
            }
        }

        public void SetChartProperty(Chart chart)
        {
            
            chart.AnimationTheme = AnimationTheme.GrowingAndFading;
            chart.ViewStateContent = SerializationContent.All;
            chart.Palette = ChartColorPalette.SemiTransparent;
            chart.ImageUrl = "../TempFiles/ChartPic_#SEQ(300,3)";
            chart.ImageType = ChartImageType.Flash;
            chart.Width = 450;
            chart.Height = 280;
            chart.BorderLineColor = Color.LightGray;
            chart.BackGradientEndColor = Color.SaddleBrown;
            chart.Palette = ChartColorPalette.Dundas;
            chart.AnimationDuration = 8;
            chart.BackColor = ColorTranslator.FromHtml("#C4DDFF");

            chart.Series[0].Type = SeriesChartType.Kagi;           
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

            chart.Annotations[0].TextFont = new Font("Microsoft Sans Serif", 10);
            chart.Annotations[0].BackColor = Color.FromArgb(220, 225, 225, 240);
            chart.Annotations[0].Width = 25;
            chart.Annotations[0].Y = 70;
            chart.Annotations[0].X = 68;



            //chart.AnimationStartTime = 0;
            //chart.AnimationDuration = 2;
            //chart.RepeatAnimation = false;
            //chart.AnimationTheme = Dundas.Charting.WebControl.AnimationTheme.GrowingOneByOne;
            
            //设置区域效果
            chart.ChartAreas["a"].BorderColor = Color.Transparent;
            chart.ChartAreas["a"].BorderStyle = ChartDashStyle.Solid;
            chart.ChartAreas["a"].BackColor = Color.Transparent;
            chart.ChartAreas["a"].AxisY2.LineColor = Color.DimGray;
            chart.ChartAreas["a"].AxisX2.LineColor = Color.DimGray;
            chart.ChartAreas["a"].AxisY.LineColor = Color.DimGray;
            chart.ChartAreas["a"].AxisX.LineColor = Color.DimGray;
            chart.ChartAreas["a"].AxisY.MajorGrid.LineStyle =ChartDashStyle.Dot;
            chart.ChartAreas["a"].AxisX.MajorGrid.LineStyle = ChartDashStyle.Dot;
            chart.ChartAreas["a"].AxisY.MajorTickMark.LineColor = Color.DimGray;
            chart.ChartAreas["a"].AxisX.MajorTickMark.LineColor = Color.DimGray;


            //设置3D效果            
            //chart.ChartAreas["a"].Area3DStyle.Perspective = 10;
            chart.ChartAreas["a"].Area3DStyle.Enable3D = true;
            chart.ChartAreas["a"].Area3DStyle.XAngle = 15;
            chart.ChartAreas["a"].Area3DStyle.YAngle = 10;
            chart.ChartAreas["a"].Area3DStyle.RightAngleAxes = false;
            chart.ChartAreas["a"].Area3DStyle.WallWidth = 0;
            chart.ChartAreas["a"].Area3DStyle.Clustered = true;


            chart.ChartAreas["a"].AxisX.LabelStyle.Font = new Font("宋体", 10);
            chart.ChartAreas["a"].AxisX.LabelStyle.FontAngle = 0;
            chart.ChartAreas["a"].AxisY.LabelStyle.Format = "P0";
           
        }

    }
}

