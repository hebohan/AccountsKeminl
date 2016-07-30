using System;
using System.Data;
using System.Collections.Generic;
using Accounts.Model;
namespace Accounts.BLL
{
	/// <summary>
	/// Com_OrgAddUser
	/// </summary>
	public partial class Com_OrgAddUser
	{
		private readonly Accounts.DAL.Com_OrgAddUser dal=new Accounts.DAL.Com_OrgAddUser();
		public Com_OrgAddUser()
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
		public bool Exists(int OrgId,string UserId)
		{
			return dal.Exists(OrgId,UserId);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(Accounts.Model.Com_OrgAddUser model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Accounts.Model.Com_OrgAddUser model)
		{
			return dal.Update(model);
		}
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(string UserId)
        {

            return dal.Delete(UserId);
        }
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int OrgId,string UserId)
		{
			
			return dal.Delete(OrgId,UserId);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Accounts.Model.Com_OrgAddUser GetModel(int OrgId,string UserId)
		{
			
			return dal.GetModel(OrgId,UserId);
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
		public List<Accounts.Model.Com_OrgAddUser> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Accounts.Model.Com_OrgAddUser> DataTableToList(DataTable dt)
		{
			List<Accounts.Model.Com_OrgAddUser> modelList = new List<Accounts.Model.Com_OrgAddUser>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Accounts.Model.Com_OrgAddUser model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new Accounts.Model.Com_OrgAddUser();
					if(dt.Rows[n]["OrgId"]!=null && dt.Rows[n]["OrgId"].ToString()!="")
					{
						model.OrgId=int.Parse(dt.Rows[n]["OrgId"].ToString());
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

