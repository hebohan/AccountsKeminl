using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Accounts.DBUtility;

namespace Accounts.DAL
{
    /// <summary>
    /// 扩展属性数据访问类:FormField
    /// </summary>
    public partial class FormField
    {
        public FormField()
        {
        }


        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from FormField");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 查询是否存在列
        /// </summary>
        public bool Exists(string column_name)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from FormField");
            strSql.Append(" where name=@name ");
            SqlParameter[] parameters = {
					new SqlParameter("@name", SqlDbType.NVarChar,100)};
            parameters[0].Value = column_name;

            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("select count(0) from syscolumns");
            strSql2.Append(" where id=object_id('FormField') and name=@name ");
            SqlParameter[] parameters2 = {
					new SqlParameter("@name", SqlDbType.NVarChar,100)};
            parameters2[0].Value = column_name;

            if (DbHelperSQL.Exists(strSql.ToString(), parameters) || DbHelperSQL.Exists(strSql2.ToString(), parameters2))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.FormField model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into FormField(");
            strSql.Append("name,title,control_type,data_type,data_length,data_place,item_option,default_value,is_required,is_password,is_html,editor_type,valid_tip_msg,valid_error_msg,valid_pattern,sort_id,is_sys,display_width,Creator)");
            strSql.Append(" values (");
            strSql.Append("@name,@title,@control_type,@data_type,@data_length,@data_place,@item_option,@default_value,@is_required,@is_password,@is_html,@editor_type,@valid_tip_msg,@valid_error_msg,@valid_pattern,@sort_id,@is_sys,@display_width,@Creator)");
            strSql.Append(";set @ReturnValue= @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@name", SqlDbType.NVarChar,100),
					new SqlParameter("@title", SqlDbType.NVarChar,100),
					new SqlParameter("@control_type", SqlDbType.NVarChar,50),
					new SqlParameter("@data_type", SqlDbType.NVarChar,50),
					new SqlParameter("@data_length", SqlDbType.Int,4),
					new SqlParameter("@data_place", SqlDbType.TinyInt,1),
					new SqlParameter("@item_option", SqlDbType.NText),
					new SqlParameter("@default_value", SqlDbType.NText),
					new SqlParameter("@is_required", SqlDbType.TinyInt,1),
					new SqlParameter("@is_password", SqlDbType.TinyInt,1),
					new SqlParameter("@is_html", SqlDbType.TinyInt,1),
					new SqlParameter("@editor_type", SqlDbType.TinyInt,1),
					new SqlParameter("@valid_tip_msg", SqlDbType.NVarChar,255),
					new SqlParameter("@valid_error_msg", SqlDbType.NVarChar,255),
					new SqlParameter("@valid_pattern", SqlDbType.NVarChar,500),
					new SqlParameter("@sort_id", SqlDbType.Int,4),
					new SqlParameter("@is_sys", SqlDbType.TinyInt,1),
                    new SqlParameter("@display_width",SqlDbType.Int,4),
                    new SqlParameter("@Creator",SqlDbType.NVarChar,50),
                    new SqlParameter("@ReturnValue",SqlDbType.Int)};
            parameters[0].Value = model.name;
            parameters[1].Value = model.title;
            parameters[2].Value = model.control_type;
            parameters[3].Value = model.data_type;
            parameters[4].Value = model.data_length;
            parameters[5].Value = model.data_place;
            parameters[6].Value = model.item_option;
            parameters[7].Value = model.default_value;
            parameters[8].Value = model.is_required;
            parameters[9].Value = model.is_password;
            parameters[10].Value = model.is_html;
            parameters[11].Value = model.editor_type;
            parameters[12].Value = model.valid_tip_msg;
            parameters[13].Value = model.valid_error_msg;
            parameters[14].Value = model.valid_pattern;
            parameters[15].Value = model.sort_id;
            parameters[16].Value = model.is_sys;
            parameters[17].Value = model.display_width;
            parameters[18].Value = model.creator;
            parameters[19].Direction = ParameterDirection.Output;

            List<CommandInfo> sqllist = new List<CommandInfo>();
            CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters);
            sqllist.Add(cmd);

            //增加扩展字段表中一列
            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("alter table FormContent add " + model.name + " " + model.data_type);
            SqlParameter[] parameters2 = { };
            cmd = new CommandInfo(strSql2.ToString(), parameters2);
            sqllist.Add(cmd);

            DbHelperSQL.ExecuteSqlTranWithIndentity(sqllist);

            using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        RehabViews(conn, trans);

                        //没有错误确认事务
                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                    }
                }
            }

            return (int)parameters[17].Value;
        }

        /// <summary>
        /// 修改一列数据
        /// </summary>
        public void UpdateField(int id, string strValue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update FormField set " + strValue);
            strSql.Append(" where id=" + id);
            DbHelperSQL.ExecuteSql(strSql.ToString());
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.FormField model)
        {
            using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        StringBuilder strSql = new StringBuilder();
                        strSql.Append("update FormField set ");
                        strSql.Append("name=@name,");
                        strSql.Append("title=@title,");
                        strSql.Append("control_type=@control_type,");
                        strSql.Append("data_type=@data_type,");
                        strSql.Append("data_length=@data_length,");
                        strSql.Append("data_place=@data_place,");
                        strSql.Append("item_option=@item_option,");
                        strSql.Append("default_value=@default_value,");
                        strSql.Append("is_required=@is_required,");
                        strSql.Append("is_password=@is_password,");
                        strSql.Append("is_html=@is_html,");
                        strSql.Append("editor_type=@editor_type,");
                        strSql.Append("valid_tip_msg=@valid_tip_msg,");
                        strSql.Append("valid_error_msg=@valid_error_msg,");
                        strSql.Append("valid_pattern=@valid_pattern,");
                        strSql.Append("sort_id=@sort_id,");
                        strSql.Append("is_sys=@is_sys,");
                        strSql.Append("display_width=@display_width,");
                        strSql.Append("Creator=@Creator");
                        strSql.Append(" where id=@id");
                        SqlParameter[] parameters = {
					            new SqlParameter("@id", SqlDbType.Int,4),
					            new SqlParameter("@name", SqlDbType.NVarChar,100),
					            new SqlParameter("@title", SqlDbType.NVarChar,100),
					            new SqlParameter("@control_type", SqlDbType.NVarChar,50),
					            new SqlParameter("@data_type", SqlDbType.NVarChar,50),
					            new SqlParameter("@data_length", SqlDbType.Int,4),
					            new SqlParameter("@data_place", SqlDbType.TinyInt,1),
					            new SqlParameter("@item_option", SqlDbType.NText),
					            new SqlParameter("@default_value", SqlDbType.NText),
					            new SqlParameter("@is_required", SqlDbType.TinyInt,1),
					            new SqlParameter("@is_password", SqlDbType.TinyInt,1),
					            new SqlParameter("@is_html", SqlDbType.TinyInt,1),
					            new SqlParameter("@editor_type", SqlDbType.TinyInt,1),
					            new SqlParameter("@valid_tip_msg", SqlDbType.NVarChar,255),
					            new SqlParameter("@valid_error_msg", SqlDbType.NVarChar,255),
					            new SqlParameter("@valid_pattern", SqlDbType.NVarChar,500),
					            new SqlParameter("@sort_id", SqlDbType.Int,4),
					            new SqlParameter("@is_sys", SqlDbType.TinyInt,1),
                                new SqlParameter("@display_width", SqlDbType.Int,4),
                                new SqlParameter("@Creator", SqlDbType.NVarChar,50)};
                        parameters[0].Value = model.id;
                        parameters[1].Value = model.name;
                        parameters[2].Value = model.title;
                        parameters[3].Value = model.control_type;
                        parameters[4].Value = model.data_type;
                        parameters[5].Value = model.data_length;
                        parameters[6].Value = model.data_place;
                        parameters[7].Value = model.item_option;
                        parameters[8].Value = model.default_value;
                        parameters[9].Value = model.is_required;
                        parameters[10].Value = model.is_password;
                        parameters[11].Value = model.is_html;
                        parameters[12].Value = model.editor_type;
                        parameters[13].Value = model.valid_tip_msg;
                        parameters[14].Value = model.valid_error_msg;
                        parameters[15].Value = model.valid_pattern;
                        parameters[16].Value = model.sort_id;
                        parameters[17].Value = model.is_sys;
                        parameters[18].Value = model.display_width;
                        parameters[19].Value = model.creator;
                        DbHelperSQL.ExecuteSql(conn, trans, strSql.ToString(), parameters);

                        //修改扩展字段表中一列
                        StringBuilder strSql2 = new StringBuilder();
                        strSql2.Append("alter table FormContent alter column " + model.name + " " + model.data_type);
                        DbHelperSQL.ExecuteSql(conn, trans, strSql2.ToString());
                        //没有错误确认事务
                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {
            //取得Model信息
            Model.FormField model = GetModel(id);
            //开始删除
            using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {

                        //删除模板关联的字段表
                        StringBuilder strSql2 = new StringBuilder();
                        strSql2.Append(string.Format(
                        "UPDATE AccountTemplate SET fields=REPLACE(REPLACE(REPLACE(fields,'{0},',''),',{0}',''),'{0}','') WHERE fields LIKE '{0},%' OR fields LIKE '%,{0}' OR fields LIKE '%,{0},%' OR fields='{0}'",
                        id));
                        DbHelperSQL.ExecuteSql(conn, trans, strSql2.ToString());
                        

                        //删除主表
                        StringBuilder strSql = new StringBuilder();
                        strSql.Append("delete from FormField ");
                        strSql.Append(" where id=@id");
                        SqlParameter[] parameters = {
					            new SqlParameter("@id", SqlDbType.Int,4)};
                        parameters[0].Value = id;
                        DbHelperSQL.ExecuteSql(conn, trans, strSql.ToString(), parameters);
                        
                        //删除扩展字段表中一列
                        DbHelperSQL.ExecuteSql(conn, trans, "alter table FormContent drop column " + model.name);

                        RehabViews(conn, trans);

                        //没有错误确认事务
                        trans.Commit();
                    }
                    catch(Exception e)
                    {   
                        trans.Rollback();
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.FormField GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 id,name,title,control_type,data_type,data_length,data_place,item_option,default_value,is_required,is_password,is_html,editor_type,valid_tip_msg,valid_error_msg,valid_pattern,sort_id,is_sys,display_width,Creator");
            strSql.Append(" from FormField ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            Model.FormField model = new Model.FormField();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                model.name = ds.Tables[0].Rows[0]["name"].ToString();
                model.title = ds.Tables[0].Rows[0]["title"].ToString();
                model.control_type = ds.Tables[0].Rows[0]["control_type"].ToString();
                model.data_type = ds.Tables[0].Rows[0]["data_type"].ToString();
                if (ds.Tables[0].Rows[0]["data_length"].ToString() != "")
                {
                    model.data_length = int.Parse(ds.Tables[0].Rows[0]["data_length"].ToString());
                }
                if (ds.Tables[0].Rows[0]["data_place"].ToString() != "")
                {
                    model.data_place = int.Parse(ds.Tables[0].Rows[0]["data_place"].ToString());
                }
                model.item_option = ds.Tables[0].Rows[0]["item_option"].ToString();
                model.default_value = ds.Tables[0].Rows[0]["default_value"].ToString();
                if (ds.Tables[0].Rows[0]["is_required"].ToString() != "")
                {
                    model.is_required = int.Parse(ds.Tables[0].Rows[0]["is_required"].ToString());
                }
                if (ds.Tables[0].Rows[0]["is_password"].ToString() != "")
                {
                    model.is_password = int.Parse(ds.Tables[0].Rows[0]["is_password"].ToString());
                }
                if (ds.Tables[0].Rows[0]["is_html"].ToString() != "")
                {
                    model.is_html = int.Parse(ds.Tables[0].Rows[0]["is_html"].ToString());
                }
                if (ds.Tables[0].Rows[0]["editor_type"].ToString() != "")
                {
                    model.editor_type = int.Parse(ds.Tables[0].Rows[0]["editor_type"].ToString());
                }
                model.valid_tip_msg = ds.Tables[0].Rows[0]["valid_tip_msg"].ToString();
                model.valid_error_msg = ds.Tables[0].Rows[0]["valid_error_msg"].ToString();
                model.valid_pattern = ds.Tables[0].Rows[0]["valid_pattern"].ToString();
                model.creator = ds.Tables[0].Rows[0]["Creator"].ToString();
                if (ds.Tables[0].Rows[0]["sort_id"].ToString() != "")
                {
                    model.sort_id = int.Parse(ds.Tables[0].Rows[0]["sort_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["is_sys"].ToString() != "")
                {
                    model.is_sys = int.Parse(ds.Tables[0].Rows[0]["is_sys"].ToString());
                }
                if (ds.Tables[0].Rows[0]["display_width"].ToString() != "")
                {
                    model.display_width = int.Parse(ds.Tables[0].Rows[0]["display_width"].ToString());
                }
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
        public Model.FormField GetModel(string name)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 id");
            strSql.Append(" from FormField ");
            strSql.Append(" where name=@name");
            SqlParameter[] parameters = {
					new SqlParameter("@name", SqlDbType.NVarChar,100)};
            parameters[0].Value = name;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj != null&&!string.IsNullOrEmpty(obj.ToString()))
            {
                return GetModel(Convert.ToInt32(obj));
            }
            return null;
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (top > 0)
            {
                strSql.Append(" top " + top.ToString());
            }
            strSql.Append(" id,name,title,control_type,data_type,data_length,data_place,item_option,default_value,is_required,is_password,is_html,editor_type,valid_tip_msg,valid_error_msg,valid_pattern,sort_id,is_sys ");
            strSql.Append(" FROM FormField ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 删除及重建该频道视图
        /// </summary>
        public void RehabViews(SqlConnection conn, SqlTransaction trans)
        {
            //删除旧的视图
            StringBuilder strSql1 = new StringBuilder();
            strSql1.Append("if exists (select 1 from sysobjects where id = object_id('View_Account') and type = 'V')");
            strSql1.Append("drop view View_Account");
            DbHelperSQL.ExecuteSql(conn, trans, strSql1.ToString());
            //添加视图
            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("CREATE VIEW View_Account as");
            strSql2.Append(" SELECT a.*,b.*,c.AccountId,c.AccountMonth,c.Remark,c.FinishTime,c.LateSign,d.cash,d.total_money FROM Account a LEFT JOIN FormContent b ON a.id=b.info_id LEFT JOIN Account_Schedule c on c.AccountId=a.id LEFT JOIN Com_UserInfos d ON a.Creator=d.Userid");
            DbHelperSQL.ExecuteSql(conn, trans, strSql2.ToString());
        }


    }
}