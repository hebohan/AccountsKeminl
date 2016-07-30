using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Accounts.DBUtility;//Please add references
namespace Accounts.DAL
{
	/// <summary>
	/// 数据访问类:Com_ButtonGroup
	/// </summary>
	public partial class Com_ButtonGroup
	{
		public Com_ButtonGroup()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("Sort", "Com_ButtonGroup"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Com_ButtonGroup");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
};
			parameters[0].Value = Id;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Accounts.Model.Com_ButtonGroup model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Com_ButtonGroup(");
			strSql.Append("ButtonName,BtnCode,Icon,Sort,Remark)");
			strSql.Append(" values (");
			strSql.Append("@ButtonName,@BtnCode,@Icon,@Sort,@Remark)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@ButtonName", SqlDbType.VarChar,50),
					new SqlParameter("@BtnCode", SqlDbType.VarChar,50),
					new SqlParameter("@Icon", SqlDbType.VarChar,50),
					new SqlParameter("@Sort", SqlDbType.Int,4),
					new SqlParameter("@Remark", SqlDbType.VarChar,100)};
			parameters[0].Value = model.ButtonName;
			parameters[1].Value = model.BtnCode;
			parameters[2].Value = model.Icon;
			parameters[3].Value = model.Sort;
			parameters[4].Value = model.Remark;

			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
			if (obj == null)
			{
				return 0;
			}
			else
			{
				return Convert.ToInt32(obj);
			}
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Accounts.Model.Com_ButtonGroup model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Com_ButtonGroup set ");
			strSql.Append("ButtonName=@ButtonName,");
			strSql.Append("BtnCode=@BtnCode,");
			strSql.Append("Icon=@Icon,");
			strSql.Append("Sort=@Sort,");
			strSql.Append("Remark=@Remark");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@ButtonName", SqlDbType.VarChar,50),
					new SqlParameter("@BtnCode", SqlDbType.VarChar,50),
					new SqlParameter("@Icon", SqlDbType.VarChar,50),
					new SqlParameter("@Sort", SqlDbType.Int,4),
					new SqlParameter("@Remark", SqlDbType.VarChar,100),
					new SqlParameter("@Id", SqlDbType.Int,4)};
			parameters[0].Value = model.ButtonName;
			parameters[1].Value = model.BtnCode;
			parameters[2].Value = model.Icon;
			parameters[3].Value = model.Sort;
			parameters[4].Value = model.Remark;
			parameters[5].Value = model.Id;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int Id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Com_ButtonGroup ");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
};
			parameters[0].Value = Id;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		/// <summary>
		/// 批量删除数据
		/// </summary>
		public bool DeleteList(string Idlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Com_ButtonGroup ");
			strSql.Append(" where Id in ("+Idlist + ")  ");
			int rows=DbHelperSQL.ExecuteSql(strSql.ToString());
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Accounts.Model.Com_ButtonGroup GetModel(int Id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 Id,ButtonName,BtnCode,Icon,Sort,Remark from Com_ButtonGroup ");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
};
			parameters[0].Value = Id;

			Accounts.Model.Com_ButtonGroup model=new Accounts.Model.Com_ButtonGroup();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["Id"]!=null && ds.Tables[0].Rows[0]["Id"].ToString()!="")
				{
					model.Id=int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ButtonName"]!=null && ds.Tables[0].Rows[0]["ButtonName"].ToString()!="")
				{
					model.ButtonName=ds.Tables[0].Rows[0]["ButtonName"].ToString();
				}
				if(ds.Tables[0].Rows[0]["BtnCode"]!=null && ds.Tables[0].Rows[0]["BtnCode"].ToString()!="")
				{
					model.BtnCode=ds.Tables[0].Rows[0]["BtnCode"].ToString();
				}
				if(ds.Tables[0].Rows[0]["Icon"]!=null && ds.Tables[0].Rows[0]["Icon"].ToString()!="")
				{
					model.Icon=ds.Tables[0].Rows[0]["Icon"].ToString();
				}
				if(ds.Tables[0].Rows[0]["Sort"]!=null && ds.Tables[0].Rows[0]["Sort"].ToString()!="")
				{
					model.Sort=int.Parse(ds.Tables[0].Rows[0]["Sort"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Remark"]!=null && ds.Tables[0].Rows[0]["Remark"].ToString()!="")
				{
					model.Remark=ds.Tables[0].Rows[0]["Remark"].ToString();
				}
				return model;
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select Id,ButtonName,BtnCode,Icon,Sort,Remark ");
			strSql.Append(" FROM Com_ButtonGroup ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" Id,ButtonName,BtnCode,Icon,Sort,Remark ");
			strSql.Append(" FROM Com_ButtonGroup ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
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
			parameters[0].Value = "Com_ButtonGroup";
			parameters[1].Value = "Id";
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

