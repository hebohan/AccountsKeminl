using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Accounts.DBUtility;

namespace Accounts.DAL
{
    //Com_Organization
    public partial class Com_Organization
    {

        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Com_Organization");
            strSql.Append(" where ");
            strSql.Append(" Id = @Id  ");
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.Int,4)
};
            parameters[0].Value = Id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.Com_Organization model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Com_Organization(");
            strSql.Append("Agency,ParentId,SupervisorId,Sort,Area,Person,Remark");
            strSql.Append(") values (");
            strSql.Append("@Agency,@ParentId,@SupervisorId,@Sort,@Area,@Person,@Remark");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                        new SqlParameter("@Agency", SqlDbType.VarChar,50) ,
                        new SqlParameter("@ParentId", SqlDbType.Int,4) ,
                        new SqlParameter("@Sort", SqlDbType.Int,4) ,
                        new SqlParameter("@Person", SqlDbType.Char,10) ,
                        new SqlParameter("@Remark", SqlDbType.VarChar,50) ,
                        new SqlParameter("@Area", SqlDbType.VarChar,50),
                        new SqlParameter("@SupervisorId", SqlDbType.Int,4)

            };

            parameters[0].Value = model.Agency;
            parameters[1].Value = model.ParentId;
            parameters[2].Value = model.Sort;
            parameters[3].Value = model.Person;
            parameters[4].Value = model.Remark;
            parameters[5].Value = model.Area;
            parameters[6].Value = model.SupervisorId;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
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
        public bool Update(Model.Com_Organization model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Com_Organization set ");

            strSql.Append(" Agency = @Agency , ");
            strSql.Append(" ParentId = @ParentId , ");
            strSql.Append(" SupervisorId = @SupervisorId , ");
            strSql.Append(" Sort = @Sort , ");
            strSql.Append(" Area = @Area , ");
            strSql.Append(" Person = @Person , ");
            strSql.Append(" Remark = @Remark  ");
            strSql.Append(" where Id=@Id ");

            SqlParameter[] parameters = {
                        new SqlParameter("@Id", SqlDbType.Int,4) ,
                        new SqlParameter("@Agency", SqlDbType.VarChar,50) ,
                        new SqlParameter("@ParentId", SqlDbType.Int,4) ,
                        new SqlParameter("@Sort", SqlDbType.Int,4) ,
                        new SqlParameter("@Person", SqlDbType.Char,10) ,
                        new SqlParameter("@Remark", SqlDbType.VarChar,50),
                        new SqlParameter("@Area", SqlDbType.VarChar,50),
                        new SqlParameter("@SupervisorId", SqlDbType.Int,4)

            };

            parameters[0].Value = model.Id;
            parameters[1].Value = model.Agency;
            parameters[2].Value = model.ParentId;
            parameters[3].Value = model.Sort;
            parameters[4].Value = model.Person;
            parameters[5].Value = model.Remark;
            parameters[6].Value = model.Area;
            parameters[7].Value = model.SupervisorId;
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
        public bool Delete(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Com_Organization ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.Int,4)
};
            parameters[0].Value = Id;


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
        public bool DeleteList(string Idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Com_Organization ");
            strSql.Append(" where ID in (" + Idlist + ")  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
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
        public Model.Com_Organization GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id, Agency, ParentId,SupervisorId, Sort, Area, Person, Remark  ");
            strSql.Append("  from Com_Organization ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.Int,4)
};
            parameters[0].Value = Id;


            Model.Com_Organization model = new Model.Com_Organization();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Id"].ToString() != "")
                {
                    model.Id = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
                }
                model.Agency = ds.Tables[0].Rows[0]["Agency"].ToString();
                if (ds.Tables[0].Rows[0]["ParentId"].ToString() != "")
                {
                    model.ParentId = int.Parse(ds.Tables[0].Rows[0]["ParentId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SupervisorId"].ToString() != "")
                {
                    model.SupervisorId = int.Parse(ds.Tables[0].Rows[0]["SupervisorId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Sort"].ToString() != "")
                {
                    model.Sort = int.Parse(ds.Tables[0].Rows[0]["Sort"].ToString());
                }
                model.Area = ds.Tables[0].Rows[0]["Area"].ToString();
                model.Person = ds.Tables[0].Rows[0]["Person"].ToString();
                model.Remark = ds.Tables[0].Rows[0]["Remark"].ToString();

                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.Com_Organization GetModel(string userid)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.Id,a.Agency, a.ParentId,SupervisorId, a.Sort, a.Area, a.Person, a.Remark  ");
            strSql.Append("  from Com_Organization a join Com_OrgAddUser b on a.Id=b.OrgId  ");
            strSql.Append(" where b.UserId=@Id");
            SqlParameter[] parameters = {
                    new SqlParameter("@Id", SqlDbType.VarChar,100)
};
            parameters[0].Value = userid;


            Model.Com_Organization model = new Model.Com_Organization();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Id"].ToString() != "")
                {
                    model.Id = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
                }
                model.Agency = ds.Tables[0].Rows[0]["Agency"].ToString();
                if (ds.Tables[0].Rows[0]["ParentId"].ToString() != "")
                {
                    model.ParentId = int.Parse(ds.Tables[0].Rows[0]["ParentId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SupervisorId"].ToString() != "")
                {
                    model.SupervisorId = int.Parse(ds.Tables[0].Rows[0]["SupervisorId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Sort"].ToString() != "")
                {
                    model.Sort = int.Parse(ds.Tables[0].Rows[0]["Sort"].ToString());
                }
                model.Area = ds.Tables[0].Rows[0]["Area"].ToString();
                model.Person = ds.Tables[0].Rows[0]["Person"].ToString();
                model.Remark = ds.Tables[0].Rows[0]["Remark"].ToString();

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
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM Com_Organization ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" * ");
            strSql.Append(" FROM Com_Organization ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }


    }
}

