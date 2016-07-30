namespace Accounts.BLL.ToExcel
{
    using System;
    using System.Data;

    public class cEnTitleParams
    {
        private string titleAlign = "center";
        private string titleBGColor = "#CCCCCC";
        private DataSet titleDataSet = null;
        private string titleFontColor = "#000000";
        private string titleFontFace = "宋体";
        private int titleFontSize = 2;
        private string titleHeaderFontColor = "#000000";
        private string titleHeaderFontFace = "黑体";
        private int titleHeaderFontSize = 3;
        private int titleHeaderHeight = 0x23;
        private string titleHeaderName = string.Empty;
        private int titleHeight = 20;
        private string titleIdColName = "Id";
        private string titleNameColName = "Name";
        private string titleParentIdColName = "ParentId";
        private string titleParentIdDefaultValue = "0";
        private string titleSortColName = "Rank";

        public string TitleAlign
        {
            get
            {
                return this.titleAlign;
            }
            set
            {
                this.titleAlign = value;
            }
        }

        public string TitleBGColor
        {
            get
            {
                return this.titleBGColor;
            }
            set
            {
                this.titleBGColor = value;
            }
        }

        public DataSet TitleDataSet
        {
            get
            {
                return this.titleDataSet;
            }
            set
            {
                this.titleDataSet = value;
            }
        }

        public string TitleFontColor
        {
            get
            {
                return this.titleFontColor;
            }
            set
            {
                this.titleFontColor = value;
            }
        }

        public string TitleFontFace
        {
            get
            {
                return this.titleFontFace;
            }
            set
            {
                this.titleFontFace = value;
            }
        }

        public int TitleFontSize
        {
            get
            {
                return this.titleFontSize;
            }
            set
            {
                this.titleFontSize = value;
            }
        }

        public string TitleHeaderFontColor
        {
            get
            {
                return this.titleHeaderFontColor;
            }
            set
            {
                this.titleHeaderFontColor = value;
            }
        }

        public string TitleHeaderFontFace
        {
            get
            {
                return this.titleHeaderFontFace;
            }
            set
            {
                this.titleHeaderFontFace = value;
            }
        }

        public int TitleHeaderFontSize
        {
            get
            {
                return this.titleHeaderFontSize;
            }
            set
            {
                this.titleHeaderFontSize = value;
            }
        }

        public int TitleHeaderHeight
        {
            get
            {
                return this.titleHeaderHeight;
            }
            set
            {
                this.titleHeaderHeight = value;
            }
        }

        public string TitleHeaderName
        {
            get
            {
                return this.titleHeaderName;
            }
            set
            {
                this.titleHeaderName = value;
            }
        }

        public int TitleHeight
        {
            get
            {
                return this.titleHeight;
            }
            set
            {
                this.titleHeight = value;
            }
        }

        public string TitleIdColName
        {
            get
            {
                return this.titleIdColName;
            }
            set
            {
                this.titleIdColName = value;
            }
        }

        public string TitleNameColName
        {
            get
            {
                return this.titleNameColName;
            }
            set
            {
                this.titleNameColName = value;
            }
        }

        public string TitleParentIdColName
        {
            get
            {
                return this.titleParentIdColName;
            }
            set
            {
                this.titleParentIdColName = value;
            }
        }

        public string TitleParentIdDefaultValue
        {
            get
            {
                return this.titleParentIdDefaultValue;
            }
            set
            {
                this.titleParentIdDefaultValue = value;
            }
        }

        public string TitleSortColName
        {
            get
            {
                return this.titleSortColName;
            }
            set
            {
                this.titleSortColName = value;
            }
        }
    }
}

