using System;
using System.Data;
using System.Collections.Generic;
using Accounts.Model;
namespace Accounts.BLL
{
	/// <summary>
	/// Com_NavigationAndButton
	/// </summary>
	public partial class Com_NavigationAndButton
	{
		private readonly Accounts.DAL.Com_NavigationAndButton dal=new Accounts.DAL.Com_NavigationAndButton();
		public Com_NavigationAndButton()
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
		public bool Exists(int NavigationId,int ButtonId)
		{
			return dal.Exists(NavigationId,ButtonId);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(Accounts.Model.Com_NavigationAndButton model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Accounts.Model.Com_NavigationAndButton model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int NavigationId,int ButtonId)
		{
			
			return dal.Delete(NavigationId,ButtonId);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Accounts.Model.Com_NavigationAndButton GetModel(int NavigationId,int ButtonId)
		{
			
			return dal.GetModel(NavigationId,ButtonId);
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
		public List<Accounts.Model.Com_NavigationAndButton> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Accounts.Model.Com_NavigationAndButton> DataTableToList(DataTable dt)
		{
			List<Accounts.Model.Com_NavigationAndButton> modelList = new List<Accounts.Model.Com_NavigationAndButton>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Accounts.Model.Com_NavigationAndButton model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new Accounts.Model.Com_NavigationAndButton();
					if(dt.Rows[n]["NavigationId"]!=null && dt.Rows[n]["NavigationId"].ToString()!="")
					{
						model.NavigationId=int.Parse(dt.Rows[n]["NavigationId"].ToString());
					}
					if(dt.Rows[n]["ButtonId"]!=null && dt.Rows[n]["ButtonId"].ToString()!="")
					{
						model.ButtonId=int.Parse(dt.Rows[n]["ButtonId"].ToString());
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

