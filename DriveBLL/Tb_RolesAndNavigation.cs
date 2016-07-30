using System;
using System.Data;
using System.Collections.Generic;
using Accounts.Model;
namespace Accounts.BLL
{
	/// <summary>
	/// Tb_RolesAndNavigation
	/// </summary>
	public partial class Tb_RolesAndNavigation
	{
        private readonly Accounts.DAL.Tb_RolesAndNavigation dal = new Accounts.DAL.Tb_RolesAndNavigation();
		public Tb_RolesAndNavigation()
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
		public bool Exists(int RolesId,int NavigationId)
		{
			return dal.Exists(RolesId,NavigationId);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(Accounts.Model.Tb_RolesAndNavigation model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Accounts.Model.Tb_RolesAndNavigation model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int RolesId,int NavigationId)
		{
			
			return dal.Delete(RolesId,NavigationId);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Accounts.Model.Tb_RolesAndNavigation GetModel(int RolesId,int NavigationId)
		{
			
			return dal.GetModel(RolesId,NavigationId);
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
		public List<Accounts.Model.Tb_RolesAndNavigation> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Accounts.Model.Tb_RolesAndNavigation> DataTableToList(DataTable dt)
		{
			List<Accounts.Model.Tb_RolesAndNavigation> modelList = new List<Accounts.Model.Tb_RolesAndNavigation>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Accounts.Model.Tb_RolesAndNavigation model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new Accounts.Model.Tb_RolesAndNavigation();
					if(dt.Rows[n]["RolesId"]!=null && dt.Rows[n]["RolesId"].ToString()!="")
					{
						model.RolesId=int.Parse(dt.Rows[n]["RolesId"].ToString());
					}
					if(dt.Rows[n]["NavigationId"]!=null && dt.Rows[n]["NavigationId"].ToString()!="")
					{
						model.NavigationId=int.Parse(dt.Rows[n]["NavigationId"].ToString());
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

