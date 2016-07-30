using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Accounts.DBUtility;//Please add references
using Inspur.Finix.DAL.SQL;
namespace Accounts.DAL
{
	/// <summary>
	/// 数据访问类:Com_OrgAddUser
	/// </summary>
	public partial class Com_OrgAddUser
	{
		public Com_OrgAddUser()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("OrgId", "Com_OrgAddUser"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int OrgId,string UserId)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Com_OrgAddUser");
			strSql.Append(" where OrgId=@OrgId and UserId=@UserId ");
			SqlParameter[] parameters = {
					new SqlParameter("@OrgId", SqlDbType.Int,4),
					new SqlParameter("@UserId", SqlDbType.Char,10)};
			parameters[0].Value = OrgId;
			parameters[1].Value = UserId;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(Accounts.Model.Com_OrgAddUser model)
		{
            IInsertDataSourceFace insert = new InsertSQL("Com_OrgAddUser");
            insert.DataBaseAlias = "common";
            insert.InsertTable = "Com_OrgAddUser";
            insert.AddFieldValue("OrgId", model.OrgId);
            insert.AddFieldValue("UserId", model.UserId);
		    int rows = insert.ExecuteNonQuery();
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
		/// 更新一条数据
		/// </summary>
		public bool Update(Accounts.Model.Com_OrgAddUser model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Com_OrgAddUser set ");
#warning 系统发现缺少更新的字段，请手工确认如此更新是否正确！ 
			strSql.Append("OrgId=@OrgId,");
			strSql.Append("UserId=@UserId");
			strSql.Append(" where OrgId=@OrgId and UserId=@UserId ");
			SqlParameter[] parameters = {
					new SqlParameter("@OrgId", SqlDbType.Int,4),
					new SqlParameter("@UserId", SqlDbType.Char,10)};
			parameters[0].Value = model.OrgId;
			parameters[1].Value = model.UserId;

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
        public bool Delete(string UserId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Com_OrgAddUser ");
            strSql.Append(" where UserId=@UserId ");
            SqlParameter[] parameters = {
					new SqlParameter("@UserId", SqlDbType.Char,10)};
            parameters[0].Value = UserId;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
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
		public bool Delete(int OrgId,string UserId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Com_OrgAddUser ");
			strSql.Append(" where OrgId=@OrgId and UserId=@UserId ");
			SqlParameter[] parameters = {
					new SqlParameter("@OrgId", SqlDbType.Int,4),
					new SqlParameter("@UserId", SqlDbType.Char,10)};
			parameters[0].Value = OrgId;
			parameters[1].Value = UserId;

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
		/// 得到一个对象实体
		/// </summary>
		public Accounts.Model.Com_OrgAddUser GetModel(int OrgId,string UserId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 OrgId,UserId from Com_OrgAddUser ");
			strSql.Append(" where OrgId=@OrgId and UserId=@UserId ");
			SqlParameter[] parameters = {
					new SqlParameter("@OrgId", SqlDbType.Int,4),
					new SqlParameter("@UserId", SqlDbType.Char,10)};
			parameters[0].Value = OrgId;
			parameters[1].Value = UserId;

			Accounts.Model.Com_OrgAddUser model=new Accounts.Model.Com_OrgAddUser();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["OrgId"]!=null && ds.Tables[0].Rows[0]["OrgId"].ToString()!="")
				{
					model.OrgId=int.Parse(ds.Tables[0].Rows[0]["OrgId"].ToString());
				}
				if(ds.Tables[0].Rows[0]["UserId"]!=null && ds.Tables[0].Rows[0]["UserId"].ToString()!="")
				{
					model.UserId=ds.Tables[0].Rows[0]["UserId"].ToString();
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
			strSql.Append("select OrgId,UserId ");
			strSql.Append(" FROM Com_OrgAddUser ");
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
			strSql.Append(" OrgId,UserId ");
			strSql.Append(" FROM Com_OrgAddUser ");
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
			parameters[0].Value = "Com_OrgAddUser";
			parameters[1].Value = "UserId";
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

