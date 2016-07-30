using System;
using System.Data;
using System.Collections.Generic;
using Accounts.Model;
namespace Accounts.BLL
{
	/// <summary>
	/// Tb_Navigation
	/// </summary>
	public partial class Tb_Navigation
	{
		private readonly Accounts.DAL.Tb_Navigation dal=new Accounts.DAL.Tb_Navigation();
		public Tb_Navigation()
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
		public bool Exists(int Id)
		{
			return dal.Exists(Id);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(Accounts.Model.Tb_Navigation model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Accounts.Model.Tb_Navigation model)
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
		public bool DeleteList(string Idlist )
		{
			return dal.DeleteList(Idlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Accounts.Model.Tb_Navigation GetModel(int Id)
		{
			
			return dal.GetModel(Id);
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
		public List<Accounts.Model.Tb_Navigation> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Accounts.Model.Tb_Navigation> DataTableToList(DataTable dt)
		{
			List<Accounts.Model.Tb_Navigation> modelList = new List<Accounts.Model.Tb_Navigation>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Accounts.Model.Tb_Navigation model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new Accounts.Model.Tb_Navigation();
					if(dt.Rows[n]["Id"]!=null && dt.Rows[n]["Id"].ToString()!="")
					{
						model.Id=int.Parse(dt.Rows[n]["Id"].ToString());
					}
					if(dt.Rows[n]["MenuName"]!=null && dt.Rows[n]["MenuName"].ToString()!="")
					{
					model.MenuName=dt.Rows[n]["MenuName"].ToString();
					}
					if(dt.Rows[n]["Pagelogo"]!=null && dt.Rows[n]["Pagelogo"].ToString()!="")
					{
					model.Pagelogo=dt.Rows[n]["Pagelogo"].ToString();
					}
					if(dt.Rows[n]["ParentId"]!=null && dt.Rows[n]["ParentId"].ToString()!="")
					{
						model.ParentId=int.Parse(dt.Rows[n]["ParentId"].ToString());
					}
					if(dt.Rows[n]["LinkAddress"]!=null && dt.Rows[n]["LinkAddress"].ToString()!="")
					{
					model.LinkAddress=dt.Rows[n]["LinkAddress"].ToString();
					}
					if(dt.Rows[n]["Icon"]!=null && dt.Rows[n]["Icon"].ToString()!="")
					{
					model.Icon=dt.Rows[n]["Icon"].ToString();
					}
					if(dt.Rows[n]["Sort"]!=null && dt.Rows[n]["Sort"].ToString()!="")
					{
						model.Sort=int.Parse(dt.Rows[n]["Sort"].ToString());
					}
					if(dt.Rows[n]["IsShow"]!=null && dt.Rows[n]["IsShow"].ToString()!="")
					{
						model.IsShow=int.Parse(dt.Rows[n]["IsShow"].ToString());
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

