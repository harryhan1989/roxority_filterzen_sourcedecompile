using System;
using System.Collections.Generic;
using System.Text;
using Nandasoft;
using System.Data;

namespace BLL
{
    public class PicParamQuery
    {
        /// <summary>
        /// 获取位置
        /// </summary>
        /// <returns></returns>
        public DataTable GetPic()
        {
            string sql = "SELECT PicParamID,PicLocation,PicUrl,PicName FROM UT_Info_PicParam";
            return NDDBAccess.Fill(sql);
        }

        /// <summary>
        /// 根据位置获取路径
        /// </summary>
        /// <returns></returns>
        public DataTable GetPic(int PicLocation)
        {
            string sql = "SELECT PicParamID,PicUrl,MiniatureFileName FROM UT_Info_PicParam WHERE PicLocation = " + PicLocation + "";
            return NDDBAccess.Fill(sql);
        }
    }
}
