using System;
using System.Data;
using System.Collections.Generic;
using Accounts.Model;
namespace Accounts.BLL
{
    /// <summary>
    /// View_Users
    /// </summary>
    public partial class View_Users
    {
        private readonly Accounts.DAL.View_Users dal = new Accounts.DAL.View_Users();
        public View_Users()
        { }
        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Accounts.Model.View_Users GetModel(string Userid)
        {
            //该表无主键信息，请自定义主键/条件字段
            return dal.GetModel(Userid);
        }
        /// <summary>
        /// 查询总数
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public int Selcount(string strWhere)
        {
            return dal.Selcount(strWhere);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        /// <summary>
        /// 分页获取数据
        /// </summary>
        public List<Accounts.Model.View_Users> GetList(int row, int page, string strWhere, string filedOrder)
        {
            DataSet ds = dal.GetList(row, page, strWhere, filedOrder);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Accounts.Model.View_Users> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Accounts.Model.View_Users> DataTableToList(DataTable dt)
        {
            List<Accounts.Model.View_Users> modelList = new List<Accounts.Model.View_Users>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Accounts.Model.View_Users model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Accounts.Model.View_Users();
                    if (dt.Rows[n]["LoginName"] != null && dt.Rows[n]["LoginName"].ToString() != "")
                    {
                        model.LoginName = dt.Rows[n]["LoginName"].ToString();
                    }
                    if (dt.Rows[n]["LoginPassword"] != null && dt.Rows[n]["LoginPassword"].ToString() != "")
                    {
                        model.LoginPassword = dt.Rows[n]["LoginPassword"].ToString();
                    }
                    if (dt.Rows[n]["Status"] != null && dt.Rows[n]["Status"].ToString() != "")
                    {
                        model.Status = int.Parse(dt.Rows[n]["Status"].ToString());
                    }
                    if (dt.Rows[n]["LastLoginIP"] != null && dt.Rows[n]["LastLoginIP"].ToString() != "")
                    {
                        model.LastLoginIP = dt.Rows[n]["LastLoginIP"].ToString();
                    }
                    if (dt.Rows[n]["LastLoginDate"] != null && dt.Rows[n]["LastLoginDate"].ToString() != "")
                    {
                        model.LastLoginDate = DateTime.Parse(dt.Rows[n]["LastLoginDate"].ToString());
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
                    if (dt.Rows[n]["Sort"] != null && dt.Rows[n]["Sort"].ToString() != "")
                    {
                        int intSort = 0;
                        int.TryParse(dt.Rows[n]["Sort"].ToString(), out intSort);
                        model.Sort = intSort;
                    }
                    if (dt.Rows[n]["AddUser"] != null && dt.Rows[n]["AddUser"].ToString() != "")
                    {
                        model.AddUser = dt.Rows[n]["AddUser"].ToString();
                    }
                    if (dt.Rows[n]["Mobile"] != null && dt.Rows[n]["Mobile"].ToString() != "")
                    {
                        model.Mobile = dt.Rows[n]["Mobile"].ToString();
                    }
                    if (dt.Rows[n]["AddDate"] != null && dt.Rows[n]["AddDate"].ToString() != "")
                    {
                        model.AddDate = DateTime.Parse(dt.Rows[n]["AddDate"].ToString());
                    }
                    if (dt.Rows[n]["Userid"] != null && dt.Rows[n]["Userid"].ToString() != "")
                    {
                        model.Userid = dt.Rows[n]["Userid"].ToString();
                    }
                    if (dt.Rows[n]["RolesId"] != null && dt.Rows[n]["RolesId"].ToString() != "")
                    {
                        model.Rolesid = dt.Rows[n]["RolesId"].ToString();
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

