using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using Accounts.Model;
namespace Accounts.BLL
{
    //Com_Organization
    public partial class Com_Organization
    {

        private readonly Accounts.DAL.Com_Organization dal = new Accounts.DAL.Com_Organization();
        public Com_Organization()
        { }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id)
        {
            return dal.Exists(Id);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Accounts.Model.Com_Organization model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Accounts.Model.Com_Organization model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int Id)
        {

            return dal.Delete(Id);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string Idlist)
        {
            return dal.DeleteList(Idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Accounts.Model.Com_Organization GetModel(int Id)
        {

            return dal.GetModel(Id);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.Com_Organization GetModel(string userid)
        {

            return dal.GetModel(userid);
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
        public List<Accounts.Model.Com_Organization> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Accounts.Model.Com_Organization> DataTableToList(DataTable dt)
        {
            List<Accounts.Model.Com_Organization> modelList = new List<Accounts.Model.Com_Organization>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Accounts.Model.Com_Organization model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Accounts.Model.Com_Organization();
                    if (dt.Rows[n]["Id"].ToString() != "")
                    {
                        model.Id = int.Parse(dt.Rows[n]["Id"].ToString());
                    }
                    model.Agency = dt.Rows[n]["Agency"].ToString();
                    if (dt.Rows[n]["ParentId"].ToString() != "")
                    {
                        model.ParentId = int.Parse(dt.Rows[n]["ParentId"].ToString());
                    }
                    if (dt.Rows[n]["Sort"].ToString() != "")
                    {
                        model.Sort = int.Parse(dt.Rows[n]["Sort"].ToString());
                    }
                    model.Person = dt.Rows[n]["Person"].ToString();
                    model.Remark = dt.Rows[n]["Remark"].ToString();


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
            int iid = 0;
            if (int.TryParse(id, out iid))
            {
                Model.Com_Organization model = GetModel(iid);
                if (model != null)
                {
                    temp = model.Agency;
                }
            }
            return temp;

        }
        #endregion

    }
}