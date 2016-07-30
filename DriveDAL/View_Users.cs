using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Accounts.DBUtility;//Please add references
namespace Accounts.DAL
{
	/// <summary>
	/// 数据访问类:View_Users
	/// </summary>
	public partial class View_Users
	{
		public View_Users()
		{}
		#region  Method
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Accounts.Model.View_Users GetModel(string Userid)
		{
			//该表无主键信息，请自定义主键/条件字段
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 LoginName,LoginPassword,Status,LastLoginIP,LastLoginDate,UserRealName,Sex,Email,Tel,AddUser,Mobile,AddDate,Userid from View_Users ");
			strSql.Append(" where Userid=@Userid");
			SqlParameter[] parameters = {
                        new SqlParameter("@Userid", SqlDbType.Char,10)
};
            parameters[0].Value = Userid;
			Accounts.Model.View_Users model=new Accounts.Model.View_Users();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["LoginName"]!=null && ds.Tables[0].Rows[0]["LoginName"].ToString()!="")
				{
					model.LoginName=ds.Tables[0].Rows[0]["LoginName"].ToString();
				}
				if(ds.Tables[0].Rows[0]["LoginPassword"]!=null && ds.Tables[0].Rows[0]["LoginPassword"].ToString()!="")
				{
					model.LoginPassword=ds.Tables[0].Rows[0]["LoginPassword"].ToString();
				}
				if(ds.Tables[0].Rows[0]["Status"]!=null && ds.Tables[0].Rows[0]["Status"].ToString()!="")
				{
					model.Status=int.Parse(ds.Tables[0].Rows[0]["Status"].ToString());
				}
				if(ds.Tables[0].Rows[0]["LastLoginIP"]!=null && ds.Tables[0].Rows[0]["LastLoginIP"].ToString()!="")
				{
					model.LastLoginIP=ds.Tables[0].Rows[0]["LastLoginIP"].ToString();
				}
				if(ds.Tables[0].Rows[0]["LastLoginDate"]!=null && ds.Tables[0].Rows[0]["LastLoginDate"].ToString()!="")
				{
					model.LastLoginDate=DateTime.Parse(ds.Tables[0].Rows[0]["LastLoginDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["UserRealName"]!=null && ds.Tables[0].Rows[0]["UserRealName"].ToString()!="")
				{
					model.UserRealName=ds.Tables[0].Rows[0]["UserRealName"].ToString();
				}
				if(ds.Tables[0].Rows[0]["Sex"]!=null && ds.Tables[0].Rows[0]["Sex"].ToString()!="")
				{
					model.Sex=ds.Tables[0].Rows[0]["Sex"].ToString();
				}
				if(ds.Tables[0].Rows[0]["Email"]!=null && ds.Tables[0].Rows[0]["Email"].ToString()!="")
				{
					model.Email=ds.Tables[0].Rows[0]["Email"].ToString();
				}
				if(ds.Tables[0].Rows[0]["Tel"]!=null && ds.Tables[0].Rows[0]["Tel"].ToString()!="")
				{
					model.Tel=ds.Tables[0].Rows[0]["Tel"].ToString();
				}
				if(ds.Tables[0].Rows[0]["AddUser"]!=null && ds.Tables[0].Rows[0]["AddUser"].ToString()!="")
				{
					model.AddUser=ds.Tables[0].Rows[0]["AddUser"].ToString();
				}
				if(ds.Tables[0].Rows[0]["Mobile"]!=null && ds.Tables[0].Rows[0]["Mobile"].ToString()!="")
				{
					model.Mobile=ds.Tables[0].Rows[0]["Mobile"].ToString();
				}
				if(ds.Tables[0].Rows[0]["AddDate"]!=null && ds.Tables[0].Rows[0]["AddDate"].ToString()!="")
				{
					model.AddDate=DateTime.Parse(ds.Tables[0].Rows[0]["AddDate"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Userid"]!=null && ds.Tables[0].Rows[0]["Userid"].ToString()!="")
				{
					model.Userid=ds.Tables[0].Rows[0]["Userid"].ToString();
				}
				return model;
			}
			else
			{
				return null;
			}
		}
        /// <summary>
        /// 获取总数
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public int Selcount(string strWhere)
        {
            string sql = "select count(Userid) from View_Users";
            if (strWhere.Trim() != "")
            {
                sql += " where " + strWhere;
            }
            object obj = DbHelperSQL.GetSingle(sql);
            if (obj != null)
            {
                return int.Parse(obj.ToString());
            }
            return 0;
        }
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select LoginName,LoginPassword,Status,LastLoginIP,LastLoginDate,UserRealName,Sex,Email,Tel,AddUser,Mobile,AddDate,Userid ");
			strSql.Append(" FROM View_Users ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}
        
		/// <summary>
		/// 获得前几行数据
		/// </summary>
        public DataSet GetList(int row,int page ,string strWhere, string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			strSql.Append(" top "+row);
            strSql.Append(" LoginName,LoginPassword,Status,LastLoginIP,LastLoginDate,UserRealName,Sex,Email,Tel,Sort,AddUser,Mobile,AddDate,Userid,RolesId ");
            strSql.Append(" FROM View_Users  where Userid not in (select top "+(page-1)*row+" Userid from View_Users ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere + " order by " + filedOrder + ")");
                strSql.Append(" and " + strWhere + " order by " + filedOrder);
            }
            else
            {
                strSql.Append(" order by " + filedOrder+") order by "+filedOrder);
            }
			DataSet ds =  DbHelperSQL.Query(strSql.ToString());
            //if (ds != null && ds.Tables[0].Rows.Count > 0)
            //{
            //    DataTable dt = ds.Tables[0];
            //    foreach (DataRow item in dt.Rows)
            //    {
            //        item["LoginPassword"] = Accounts.DBUtility.DESEncrypt.Decrypt(item["LoginPassword"].ToString());
            //    }
            //}
		    return ds;
		}

		/*
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@tblName", SqlDbType.VarChar, 255),
					new SqlParameter("@fldName", SqlDbType.VarChar, 255),
					new SqlParameter("@PageSize", SqlDbType.Int),
					new SqlParameter("@PageIndex", SqlDbType.Int),
					new SqlParameter("@IsReCount", SqlDbType.Bit),
					new SqlParameter("@OrderType", SqlDbType.Bit),
					new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
					};
			parameters[0].Value = "View_Users";
			parameters[1].Value = "NavigationId";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  Method
	}
}

