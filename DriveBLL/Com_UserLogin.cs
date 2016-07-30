using System;
using System.Data;
using System.Collections.Generic;
using Accounts.Model;
namespace Accounts.BLL
{
	/// <summary>
	/// Com_UserLogin
	/// </summary>
	public partial class Com_UserLogin
	{
		private readonly Accounts.DAL.Com_UserLogin dal=new Accounts.DAL.Com_UserLogin();
		public Com_UserLogin()
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
        /// 登录
        /// </summary>
        public string GetUserId(string LoginName, string LoginPassword)
        {
            return dal.GetUserId(LoginName,LoginPassword);
        }
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Id)
		{
			return dal.Exists(Id);
		}
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string loginName)
        {
            return dal.Exists(loginName);
        }
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(Accounts.Model.Com_UserLogin model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Accounts.Model.Com_UserLogin model)
		{
			return dal.Update(model);
		}
        /// <summary>
        /// 更新密码
        /// </summary>
        public bool UpdatePass(Accounts.Model.Com_UserLogin model)
        {
            return dal.UpdatePass(model);
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
        public Accounts.Model.Com_UserLogin GetModel(string Userid)
        {
            return dal.GetModel(Userid);
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public string GetUserId_Validate(string loginname, string email)
        {
            return dal.GetUserId_Validate(loginname, email);
        }
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Accounts.Model.Com_UserLogin GetModel(int Id)
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
		public List<Accounts.Model.Com_UserLogin> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Accounts.Model.Com_UserLogin> DataTableToList(DataTable dt)
		{
			List<Accounts.Model.Com_UserLogin> modelList = new List<Accounts.Model.Com_UserLogin>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Accounts.Model.Com_UserLogin model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new Accounts.Model.Com_UserLogin();
					if(dt.Rows[n]["Id"]!=null && dt.Rows[n]["Id"].ToString()!="")
					{
						model.Id=int.Parse(dt.Rows[n]["Id"].ToString());
					}
					if(dt.Rows[n]["UserId"]!=null && dt.Rows[n]["UserId"].ToString()!="")
					{
					model.UserId=dt.Rows[n]["UserId"].ToString();
					}
					if(dt.Rows[n]["LoginName"]!=null && dt.Rows[n]["LoginName"].ToString()!="")
					{
					model.LoginName=dt.Rows[n]["LoginName"].ToString();
					}
					if(dt.Rows[n]["LoginPassword"]!=null && dt.Rows[n]["LoginPassword"].ToString()!="")
					{
					model.LoginPassword=dt.Rows[n]["LoginPassword"].ToString();
					}
					if(dt.Rows[n]["Status"]!=null && dt.Rows[n]["Status"].ToString()!="")
					{
						model.Status=int.Parse(dt.Rows[n]["Status"].ToString());
					}
					if(dt.Rows[n]["LastLoginIP"]!=null && dt.Rows[n]["LastLoginIP"].ToString()!="")
					{
					model.LastLoginIP=dt.Rows[n]["LastLoginIP"].ToString();
					}
					if(dt.Rows[n]["LastLoginDate"]!=null && dt.Rows[n]["LastLoginDate"].ToString()!="")
					{
						model.LastLoginDate=DateTime.Parse(dt.Rows[n]["LastLoginDate"].ToString());
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

