namespace Accounts.BLL.ToExcel
{
    using System;
    using System.Data;

    public class cExcelHeaderDefine
    {
        private DataSet ds = null;

        public cExcelHeaderDefine()
        {
            this.Create();
        }

        public void Add(string id, string name, string parentId, int rank)
        {
            if (!this.IsExist(id) && !((id == null) || id.Equals(string.Empty)))
            {
                DataRow row = this.ds.Tables[0].NewRow();
                row["Id"] = id;
                row["Name"] = name;
                row["ParentId"] = parentId;
                row["Rank"] = rank;
                this.ds.Tables[0].Rows.Add(row);
            }
        }

        private void Create()
        {
            this.ds = new DataSet();
            this.ds.DataSetName = "HeaderDataSet";
            this.ds.Tables.Add(this.TblCreate());
        }

        private bool IsExist(string id)
        {
            DataRow[] rowArray = this.ds.Tables[0].Select("Id='" + id + "'");
            return ((rowArray != null) && (rowArray.Length > 0));
        }

        public DataSet MakeTitleHeader()
        {
            return this.ds;
        }

        private DataTable TblCreate()
        {
            DataTable table = new DataTable();
            table.Columns.Add("Id");
            table.Columns.Add("Name");
            table.Columns.Add("ParentId");
            table.Columns.Add("Rank");
            table.PrimaryKey = new DataColumn[] { table.Columns["Id"] };
            table.TableName = "TitleHeader";
            return table;
        }
    }
}

