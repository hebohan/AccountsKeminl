using System;
using System.Data;
using System.Collections.Generic;
using Accounts.Model;
namespace Accounts.BLL
{
	/// <summary>
	/// Tb_RolesAddUser
	/// </summary>
	public partial class Tb_RolesAddUser
	{
        private readonly Accounts.DAL.Tb_RolesAddUser dal = new Accounts.DAL.Tb_RolesAddUser();
		public Tb_RolesAddUser()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
			return dal.GetMaxId();
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int RolesId,string UserId)
		{
			return dal.Exists(RolesId,UserId);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(Accounts.Model.Tb_RolesAddUser model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Accounts.Model.Tb_RolesAddUser model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int RolesId,string UserId)
		{
			
			return dal.Delete(RolesId,UserId);
		}
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int RolesId)
        {

            return dal.Delete(RolesId);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(string UserId)
        {

            return dal.Delete(UserId);
        }
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Accounts.Model.Tb_RolesAddUser GetModel(int RolesId,string UserId)
		{
			
			return dal.GetModel(RolesId,UserId);
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
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return dal.GetList(Top,strWhere,filedOrder);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Accounts.Model.Tb_RolesAddUser> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Accounts.Model.Tb_RolesAddUser> DataTableToList(DataTable dt)
		{
			List<Accounts.Model.Tb_RolesAddUser> modelList = new List<Accounts.Model.Tb_RolesAddUser>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Accounts.Model.Tb_RolesAddUser model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new Accounts.Model.Tb_RolesAddUser();
					if(dt.Rows[n]["RolesId"]!=null && dt.Rows[n]["RolesId"].ToString()!="")
					{
						model.RolesId=int.Parse(dt.Rows[n]["RolesId"].ToString());
					}
					if(dt.Rows[n]["UserId"]!=null && dt.Rows[n]["UserId"].ToString()!="")
					{
					model.UserId=dt.Rows[n]["UserId"].ToString();
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

