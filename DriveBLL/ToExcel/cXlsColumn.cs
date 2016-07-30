namespace Accounts.BLL.ToExcel
{
    using System;

    public class cXlsColumn
    {
        private string align = "left";
        private string bGColor = "#FFFFFF";
        private int colspan = 1;
        private string fontColor = "#000000";
        private string fontFace = "宋体";
        private int fontSize = 2;
        private int rowspan = 1;
        private string text = string.Empty;

        public string Align
        {
            get
            {
                return this.align;
            }
            set
            {
                this.align = value;
            }
        }

        public string BGColor
        {
            get
            {
                return this.bGColor;
            }
            set
            {
                this.bGColor = value;
            }
        }

        public int Colspan
        {
            get
            {
                return this.colspan;
            }
            set
            {
                this.colspan = value;
            }
        }

        public string FontColor
        {
            get
            {
                return this.fontColor;
            }
            set
            {
                this.fontColor = value;
            }
        }

        public string FontFace
        {
            get
            {
                return this.fontFace;
            }
            set
            {
                this.fontFace = value;
            }
        }

        public int FontSize
        {
            get
            {
                return this.fontSize;
            }
            set
            {
                this.fontSize = value;
            }
        }

        public int Rowspan
        {
            get
            {
                return this.rowspan;
            }
            set
            {
                this.rowspan = value;
            }
        }

        public string Text
        {
            get
            {
                return this.text;
            }
            set
            {
                this.text = value;
            }
        }
    }
}

