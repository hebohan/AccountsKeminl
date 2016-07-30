using System;
using System.Data;
using System.Collections.Generic;
using Accounts.Model;
namespace Accounts.BLL
{
	/// <summary>
	/// Com_LoginLog
	/// </summary>
	public partial class Com_LoginLog
	{
		private readonly Accounts.DAL.Com_LoginLog dal=new Accounts.DAL.Com_LoginLog();
		public Com_LoginLog()
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
		public int  Add(Accounts.Model.Com_LoginLog model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Accounts.Model.Com_LoginLog model)
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
		public Accounts.Model.Com_LoginLog GetModel(int Id)
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
		public List<Accounts.Model.Com_LoginLog> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Accounts.Model.Com_LoginLog> DataTableToList(DataTable dt)
		{
			List<Accounts.Model.Com_LoginLog> modelList = new List<Accounts.Model.Com_LoginLog>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Accounts.Model.Com_LoginLog model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new Accounts.Model.Com_LoginLog();
					if(dt.Rows[n]["Id"]!=null && dt.Rows[n]["Id"].ToString()!="")
					{
						model.Id=int.Parse(dt.Rows[n]["Id"].ToString());
					}
					if(dt.Rows[n]["Userid"]!=null && dt.Rows[n]["Userid"].ToString()!="")
					{
					model.Userid=dt.Rows[n]["Userid"].ToString();
					}
					if(dt.Rows[n]["LoginIP"]!=null && dt.Rows[n]["LoginIP"].ToString()!="")
					{
					model.LoginIP=dt.Rows[n]["LoginIP"].ToString();
					}
					if(dt.Rows[n]["LoginDate"]!=null && dt.Rows[n]["LoginDate"].ToString()!="")
					{
						model.LoginDate=DateTime.Parse(dt.Rows[n]["LoginDate"].ToString());
					}
					if(dt.Rows[n]["Status"]!=null && dt.Rows[n]["Status"].ToString()!="")
					{
					model.Status=dt.Rows[n]["Status"].ToString();
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

