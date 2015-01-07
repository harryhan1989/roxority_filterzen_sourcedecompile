using System;
using System.Collections.Generic;
using System.Text;
using Nandasoft;

namespace BLL.Entity
{
    /// <summary>
    /// Ŀ��: ��Ա���ֱ�ʵ����.
    /// ��д����: 2010-10-15.
    /// </summary>
    [TableName("UT_QS_HuiYuan_Point")]
    public class PointEntity : BaseEntity
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public PointEntity()
        {
        }


        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="huiYuanGuid"></param>
        public PointEntity(string huiYuanGuid)
            : this()
        {
            _huiYuanGuid = huiYuanGuid;
            this.SelectByPKeys();
        }

        private string _huiYuanGuid = "";
        /// <summary>
        /// ��ԱGUID
        /// </summary>
        [ColumnName("HuiYuanGuid")]
        [ColumnPrimaryKey(true)]
        public string HuiYuanGuid
        {
            get
            {
                return _huiYuanGuid;
            }
            set
            {
                _huiYuanGuid = value;
            }
        }


        private int _totalPoint = 0;
        /// <summary>
        /// �ܻ���
        /// </summary>
        [ColumnName("TotalPoint")]
        public int TotalPoint
        {
            get
            {
                return _totalPoint;
            }
            set
            {
                _totalPoint = value;
            }
        }


        private int _remainPoint = 0;
        /// <summary>
        /// ʣ�����
        /// </summary>
        [ColumnName("RemainPoint")]
        public int RemainPoint
        {
            get
            {
                return _remainPoint;
            }
            set
            {
                _remainPoint = value;
            }
        }


        private int _status = 0;
        /// <summary>
        /// ��Ա״̬(0:ͣ��  1������)
        /// </summary>
        [ColumnName("Status")]
        public int Status
        {
            get
            {
                return _status;
            }
            set
            {
                _status = value;
            }
        }

    }
}
