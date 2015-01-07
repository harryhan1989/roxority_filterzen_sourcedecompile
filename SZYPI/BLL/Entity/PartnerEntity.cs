using System;
using System.Collections.Generic;
using System.Text;
using Nandasoft;

namespace BLL.Entity
{
    /// <summary>
    /// Ŀ��: �����ʾ��������ʵ����.
    /// ��д����: 2010-11-3.
    /// </summary>
    [TableName("UT_QS_Partner")]
    public class PartnerEntity : BaseEntity
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public PartnerEntity()
        {
        }


        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="id"></param>
        public PartnerEntity(string id)
            : this()
        {
            _id = id;
            this.SelectByPKeys();
        }

        private string _id = "";
        /// <summary>
        /// ���
        /// </summary>
        [ColumnName("ID")]
        [ColumnPrimaryKey(true)]
        public string ID
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }


        private string _name = "";
        /// <summary>
        /// ����������
        /// </summary>
        [ColumnName("Name")]
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }


        private string _uRL = "";
        /// <summary>
        /// �����ӵ�ַ
        /// </summary>
        [ColumnName("URL")]
        public string URL
        {
            get
            {
                return _uRL;
            }
            set
            {
                _uRL = value;
            }
        }


        private byte[] _image = new byte[0];
        /// <summary>
        /// ͼƬ
        /// </summary>
        [ColumnName("Image")]
        public byte[] Image
        {
            get
            {
                return _image;
            }
            set
            {
                _image = value;
            }
        }


        private int _status = -1;
        /// <summary>
        /// ״̬
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


        private int _sort = 0;
        /// <summary>
        /// ����
        /// </summary>
        [ColumnName("sort")]
        public int sort
        {
            get
            {
                return _sort;
            }
            set
            {
                _sort = value;
            }
        }

    }
}
