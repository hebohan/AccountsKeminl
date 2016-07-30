using System;
using System.Data;
using System.Collections.Generic;
using Accounts.Model;
namespace Accounts.BLL
{
    /// <summary>
    /// Com_UserInfos
    /// </summary>
    public partial class Com_UserInfos
    {
        private readonly Accounts.DAL.Com_UserInfos dal = new Accounts.DAL.Com_UserInfos();
        public Com_UserInfos()
        { }
        #region  Method
        public int GetMaxId(string FieldName, string TableName)
        {
            return dal.GetMaxId(FieldName, TableName);
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string Userid)
        {
            return dal.Exists(Userid);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Accounts.Model.Com_UserInfos model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Accounts.Model.Com_UserInfos model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(string Userid)
        {

            return dal.Delete(Userid);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string Useridlist)
        {
            return dal.DeleteList(Useridlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Accounts.Model.Com_UserInfos GetModel(string Userid)
        {

            return dal.GetModel(Userid);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Accounts.Model.Com_UserInfos> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Accounts.Model.Com_UserInfos> DataTableToList(DataTable dt)
        {
            List<Accounts.Model.Com_UserInfos> modelList = new List<Accounts.Model.Com_UserInfos>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Accounts.Model.Com_UserInfos model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Accounts.Model.Com_UserInfos();
                    if (dt.Rows[n]["Userid"] != null && dt.Rows[n]["Userid"].ToString() != "")
                    {
                        model.Userid = dt.Rows[n]["Userid"].ToString();
                    }
                    if (dt.Rows[n]["UserRealName"] != null && dt.Rows[n]["UserRealName"].ToString() != "")
                    {
                        model.UserRealName = dt.Rows[n]["UserRealName"].ToString();
                    }
                    if (dt.Rows[n]["Sex"] != null && dt.Rows[n]["Sex"].ToString() != "")
                    {
                        model.Sex = dt.Rows[n]["Sex"].ToString();
                    }
                    if (dt.Rows[n]["Email"] != null && dt.Rows[n]["Email"].ToString() != "")
                    {
                        model.Email = dt.Rows[n]["Email"].ToString();
                    }
                    if (dt.Rows[n]["Tel"] != null && dt.Rows[n]["Tel"].ToString() != "")
                    {
                        model.Tel = dt.Rows[n]["Tel"].ToString();
                    }
                    if (dt.Rows[n]["Mobile"] != null && dt.Rows[n]["Mobile"].ToString() != "")
                    {
                        model.Mobile = dt.Rows[n]["Mobile"].ToString();
                    }
                    if (dt.Rows[n]["AddUser"] != null && dt.Rows[n]["AddUser"].ToString() != "")
                    {
                        model.AddUser = dt.Rows[n]["AddUser"].ToString();
                    }
                    if (dt.Rows[n]["AddDate"] != null && dt.Rows[n]["AddDate"].ToString() != "")
                    {
                        model.AddDate = DateTime.Parse(dt.Rows[n]["AddDate"].ToString());
                    }
                    modelList.Add(model);
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }

        public string GetSingle(string id)
        {
            string temp = string.Empty;
            Model.Com_UserInfos model = GetModel(id);
            if (model != null)
            {
                temp = model.UserRealName;
            }
            return temp;

        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetList(PageSize,PageIndex,strWhere);
        //}

        #endregion  Method
    }
}

