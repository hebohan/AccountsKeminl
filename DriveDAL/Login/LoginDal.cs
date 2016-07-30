using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Inspur.Finix.DAL.SQL;
using System.Web;
using work.MD5Hash;


namespace Accounts.DAL.Login
{
    public class LoginDal
    {
        public static DataTable ValidateUser(string loginname, string passwork)
        {
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            select.SelectFromTable("Com_UserLogin");
            select.SelectAllColumns();
            select.AddWhere("LoginName", loginname);
            select.AddWhere("LoginPassword", MD5up.MD5(passwork.ToString()));
            return select.ExecuteDataSet().Tables[0];
        }

        public static DataTable GetParentMenuTable(string userId)
        {
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            string sql = string.Format(@"SELECT  DISTINCT
                                                e.MenuName ,
                                                e.LinkAddress ,
                                                e.Id,
                                                e.Sort
                                        FROM    Tb_RolesAddUser b
                                                JOIN dbo.Tb_RolesAndNavigation c ON b.rolesId = c.rolesId
                                                JOIN dbo.Tb_Navigation d ON c.NavigationId = d.Id
                                                JOIN dbo.Tb_Navigation e ON e.Id = d.ParentId
                                        WHERE   UserId = '{0}' ", userId);
            select.CommandText = sql;
            DataView dv = new DataView(select.ExecuteDataSet().Tables[0]);
            dv.Sort="Sort";
            return dv.ToTable();
        }

        public static DataTable GetChildMenu(string parentId, string userId)
        {
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            string sql = string.Format(@"SELECT  distinct d.*
                                        FROM    Tb_RolesAddUser b
                                                JOIN dbo.Tb_RolesAndNavigation c ON b.rolesId = c.rolesId
                                                JOIN dbo.Tb_Navigation d ON c.NavigationId = d.Id
                                        WHERE b.userId={0} AND d.parentID={1} order by d.Sort", userId, parentId);
            select.CommandText = sql;
            return select.ExecuteDataSet().Tables[0];
        }

        public static DataTable GetRoleTable(string userId)
        {
            ISelectDataSourceFace select = new SelectSQL();
            select.DataBaseAlias = "common";
            string sql = string.Format(@"SELECT  c.RolesName
                                        FROM    dbo.Com_UserLogin a
                                                JOIN dbo.Tb_RolesAddUser b ON a.UserId = b.UserId
                                                JOIN dbo.Tb_Roles c ON b.RolesId = c.Id
                                        WHERE   a.UserId = '{0}'", userId);
            select.CommandText = sql;
            DataTable dt = new DataTable();
            dt = select.ExecuteDataSet().Tables[0];
            return dt;
        }
    }
}
