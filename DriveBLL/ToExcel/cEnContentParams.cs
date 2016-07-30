namespace Accounts.BLL.ToExcel
{
    using System;
    using System.Collections;
    using System.Data;

    public class cEnContentParams
    {
        private bool alternate = false;
        private DataSet contentDataSet = null;
        private string dataAlign = "left";
        private string dataFirstBGColor = "#FFFFFF";
        private string dataFongFace = "宋体";
        private string dataFontColor = "#000000";
        private int dataFontSize = 2;
        private int dataHeight = 20;
        private string dataSecondBGColor = "#FFEFD5";
        private ArrayList dataSortColumn = new ArrayList();
        private string dataSortMode = "ASC";
        private int dataWidth = 60;
        private string fileName = string.Empty;
        private ArrayList hiddenColumn = new ArrayList();

        public bool Alternate
        {
            get
            {
                return this.alternate;
            }
            set
            {
                this.alternate = value;
            }
        }

        public DataSet ContentDataSet
        {
            get
            {
                return this.contentDataSet;
            }
            set
            {
                this.contentDataSet = value;
            }
        }

        public string DataAlign
        {
            get
            {
                return this.dataAlign;
            }
            set
            {
                this.dataAlign = value;
            }
        }

        public string DataFirstBGColor
        {
            get
            {
                return this.dataFirstBGColor;
            }
            set
            {
                this.dataFirstBGColor = value;
            }
        }

        public string DataFongFace
        {
            get
            {
                return this.dataFongFace;
            }
            set
            {
                this.dataFongFace = value;
            }
        }

        public string DataFontColor
        {
            get
            {
                return this.dataFontColor;
            }
            set
            {
                this.dataFontColor = value;
            }
        }

        public int DataFontSize
        {
            get
            {
                return this.dataFontSize;
            }
            set
            {
                this.dataFontSize = value;
            }
        }

        public int DataHeight
        {
            get
            {
                return this.dataHeight;
            }
            set
            {
                this.dataHeight = value;
            }
        }

        public string DataSecondBGColor
        {
            get
            {
                return this.dataSecondBGColor;
            }
            set
            {
                this.dataSecondBGColor = value;
            }
        }

        public ArrayList DataSortColumn
        {
            get
            {
                return this.dataSortColumn;
            }
            set
            {
                this.dataSortColumn = value;
            }
        }

        public string DataSortMode
        {
            get
            {
                return this.dataSortMode;
            }
            set
            {
                this.dataSortMode = value;
            }
        }

        public int DataWidth
        {
            get
            {
                return this.dataWidth;
            }
            set
            {
                this.dataWidth = value;
            }
        }

        public string FileName
        {
            get
            {
                return this.fileName;
            }
            set
            {
                this.fileName = value;
            }
        }

        public ArrayList HiddenColumn
        {
            get
            {
                return this.hiddenColumn;
            }
            set
            {
                this.hiddenColumn = value;
            }
        }
    }
}

