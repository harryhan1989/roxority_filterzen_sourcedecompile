using System;
using System.Collections.Generic;
using System.Text;
using Nandasoft;

namespace BLL
{
    /// <summary>
    /// Ŀ��: ʵ����.
    /// ��д����: 2010-3-11.
    /// </summary>
    [TableName("UT_Info_PicParam")]
    public class PicParamEntity : BaseEntity
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public PicParamEntity()
        {
        }


        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="picParamID"></param>
        public PicParamEntity(long picParamID)
            : this()
        {
            _picParamID = picParamID;
            this.SelectByPKeys();
        }

        private long _picParamID = 0;
        /// <summary>
        /// 
        /// </summary>
        [ColumnName("PicParamID")]
        [ColumnPrimaryKey(true)]
        [ColumnAutoIncrement(true)]
        public long PicParamID
        {
            get
            {
                return _picParamID;
            }
            set
            {
                _picParamID = value;
            }
        }


        private int _picLocation = 0;
        /// <summary>
        /// λ��
        /// </summary>
        [ColumnName("PicLocation")]
        public int PicLocation
        {
            get
            {
                return _picLocation;
            }
            set
            {
                _picLocation = value;
            }
        }


        private string _picName = "";
        /// <summary>
        /// ����
        /// </summary>
        [ColumnName("PicName")]
        public string PicName
        {
            get
            {
                return _picName;
            }
            set
            {
                _picName = value;
            }
        }


        private byte[] _picParamImg = new byte[0];
        /// <summary>
        /// ͼƬ
        /// </summary>
        [ColumnName("PicParamImg")]
        public byte[] PicParamImg
        {
            get
            {
                return _picParamImg;
            }
            set
            {
                _picParamImg = value;
            }
        }


        private string _picUrl = "";
        /// <summary>
        /// ·��
        /// </summary>
        [ColumnName("PicUrl")]
        public string PicUrl
        {
            get
            {
                return _picUrl;
            }
            set
            {
                _picUrl = value;
            }
        }


        private string _picType = "";
        /// <summary>
        /// 
        /// </summary>
        [ColumnName("PicType")]
        public string PicType
        {
            get
            {
                return _picType;
            }
            set
            {
                _picType = value;
            }
        }


        private string _miniatureFileName = "";
        /// <summary>
        /// 
        /// </summary>
        [ColumnName("MiniatureFileName")]
        public string MiniatureFileName
        {
            get
            {
                return _miniatureFileName;
            }
            set
            {
                _miniatureFileName = value;
            }
        }

    }
}
