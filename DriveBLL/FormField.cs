using System.Collections.Generic;
using System.Data;

namespace Accounts.BLL
{
    /// <summary>
    /// ��չ���Ա�
    /// </summary>
    public partial class FormField
    {

        private readonly DAL.FormField dal;

        public FormField()
        {
            dal = new DAL.FormField();
        }
        #region  Method
        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool Exists(int id)
        {
            return dal.Exists(id);
        }

        /// <summary>
        /// ��ѯ�Ƿ������
        /// </summary>
        public bool Exists(string column_name)
        {
            return dal.Exists(column_name);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public int Add(Model.FormField model)
        {
            switch (model.control_type)
            {
                case "single-text": //�����ı�
                    if (model.data_length > 0 && model.data_length <= 4000)
                    {
                        model.data_type = "nvarchar(" + model.data_length + ")";
                    }
                    else if (model.data_length > 4000)
                    {
                        model.data_type = "ntext";
                    }
                    else
                    {
                        model.data_length = 50;
                        model.data_type = "nvarchar(50)";
                    }
                    break;
                case "multi-text": //�����ı�
                    goto case "single-text";
                case "editor": //�༭��
                    model.data_type = "ntext";
                    break;
                case "images": //ͼƬ
                    model.data_type = "nvarchar(255)";
                    break;
                case "video": //��Ƶ
                    model.data_type = "nvarchar(255)";
                    break;
                case "number": //����
                    if (model.data_place > 0)
                    {
                        model.data_type = "decimal(9," + model.data_place + ")";
                    }
                    else
                    {
                        model.data_type = "int";
                    }
                    break;
                case "date": //����
                    model.data_type = "datetime";
                    break;
                case "datetime": //����ʱ��
                    model.data_type = "datetime";
                    break;
                case "checkbox": //��ѡ��
                    model.data_type = "tinyint";
                    break;
                case "multi-radio": //���ѡ
                    if (model.data_type == "int")
                    {
                        model.data_length = 4;
                        model.data_type = "int";
                    }
                    else
                    {
                        if (model.data_length > 0 && model.data_length <= 4000)
                        {
                            model.data_type = "nvarchar(" + model.data_length + ")";
                        }
                        else if (model.data_length > 4000)
                        {
                            model.data_type = "ntext";
                        }
                        else
                        {
                            model.data_length = 50;
                            model.data_type = "nvarchar(50)";
                        }
                    }

                    break;
                case "dropdownlist": //����ѡ��
                    if (model.data_length > 0 && model.data_length <= 4000)
                    {
                        model.data_type = "nvarchar(" + model.data_length + ")";
                    }
                    else if (model.data_length > 4000)
                    {
                        model.data_type = "ntext";
                    }
                    else
                    {
                        model.data_length = 50;
                        model.data_type = "nvarchar(50)";
                    }

                    break;
            }
            return dal.Add(model);
        }

        /// <summary>
        /// �޸�һ������
        /// </summary>
        public void UpdateField(int id, string strValue)
        {
            dal.UpdateField(id, strValue);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public bool Update(Model.FormField model)
        {
            switch (model.control_type)
            {
                case "single-text": //�����ı�
                    if (model.data_length > 0 && model.data_length <= 4000)
                    {
                        model.data_type = "nvarchar(" + model.data_length + ")";
                    }
                    else if (model.data_length > 4000)
                    {
                        model.data_type = "ntext";
                    }
                    else
                    {
                        model.data_length = 50;
                        model.data_type = "nvarchar(50)";
                    }
                    break;
                case "multi-text": //�����ı�
                    goto case "single-text";
                case "editor": //�༭��
                    model.data_type = "ntext";
                    break;
                case "images": //ͼƬ
                    model.data_type = "nvarchar(255)";
                    break;
                case "video": //��Ƶ
                    model.data_type = "nvarchar(255)";
                    break;
                case "number": //����
                    if (model.data_place > 0)
                    {
                        model.data_type = "decimal(9," + model.data_place + ")";
                    }
                    else
                    {
                        model.data_type = "int";
                    }
                    break;
                case "date": //����
                    model.data_type = "datetime";
                    break;
                case "datetime": //����ʱ��
                    model.data_type = "datetime";
                    break;
                case "checkbox": //��ѡ��
                    model.data_type = "tinyint";
                    break;
                case "multi-radio": //���ѡ
                    if (model.data_type == "int")
                    {
                        model.data_length = 4;
                        model.data_type = "int";
                    }
                    else
                    {
                        if (model.data_length > 0 && model.data_length <= 4000)
                        {
                            model.data_type = "nvarchar(" + model.data_length + ")";
                        }
                        else if (model.data_length > 4000)
                        {
                            model.data_type = "ntext";
                        }
                        else
                        {
                            model.data_length = 50;
                            model.data_type = "nvarchar(50)";
                        }
                    }

                    break;
                case "multi-checkbox": //�����ѡ
                    goto case "single-text";
                case "dropdownlist": //����ѡ��
                    if (model.data_length > 0 && model.data_length <= 4000)
                    {
                        model.data_type = "nvarchar(" + model.data_length + ")";
                    }
                    else if (model.data_length > 4000)
                    {
                        model.data_type = "ntext";
                    }
                    else
                    {
                        model.data_length = 50;
                        model.data_type = "nvarchar(50)";
                    }

                    break;

            }
            return dal.Update(model);
        }

        /// <summary>
        /// ɾ��һ������
        /// </summary>
        public bool Delete(int id)
        {
            return dal.Delete(id);
        }

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public Model.FormField GetModel(int id)
        {
            return dal.GetModel(id);
        }

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public Model.FormField GetModel(string name)
        {
            return dal.GetModel(name);
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        public List<Model.FormField> DataTableToList(DataTable dt)
        {
            List<Model.FormField> modelList = new List<Model.FormField>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Model.FormField model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Model.FormField();
                    if (dt.Rows[n]["id"].ToString() != "")
                    {
                        model.id = int.Parse(dt.Rows[n]["id"].ToString());
                    }
                    model.name = dt.Rows[n]["name"].ToString();
                    model.title = dt.Rows[n]["title"].ToString();
                    model.control_type = dt.Rows[n]["control_type"].ToString();
                    model.data_type = dt.Rows[n]["data_type"].ToString();
                    if (dt.Rows[n]["data_length"].ToString() != "")
                    {
                        model.data_length = int.Parse(dt.Rows[n]["data_length"].ToString());
                    }
                    if (dt.Rows[n]["data_place"].ToString() != "")
                    {
                        model.data_place = int.Parse(dt.Rows[n]["data_place"].ToString());
                    }
                    model.item_option = dt.Rows[n]["item_option"].ToString();
                    model.default_value = dt.Rows[n]["default_value"].ToString();
                    if (dt.Rows[n]["is_required"].ToString() != "")
                    {
                        model.is_required = int.Parse(dt.Rows[n]["is_required"].ToString());
                    }
                    if (dt.Rows[n]["is_password"].ToString() != "")
                    {
                        model.is_password = int.Parse(dt.Rows[n]["is_password"].ToString());
                    }
                    if (dt.Rows[n]["is_html"].ToString() != "")
                    {
                        model.is_html = int.Parse(dt.Rows[n]["is_html"].ToString());
                    }
                    if (dt.Rows[n]["editor_type"].ToString() != "")
                    {
                        model.editor_type = int.Parse(dt.Rows[n]["editor_type"].ToString());
                    }
                    model.valid_tip_msg = dt.Rows[n]["valid_tip_msg"].ToString();
                    model.valid_error_msg = dt.Rows[n]["valid_error_msg"].ToString();
                    model.valid_pattern = dt.Rows[n]["valid_pattern"].ToString();
                    if (dt.Rows[n]["sort_id"].ToString() != "")
                    {
                        model.sort_id = int.Parse(dt.Rows[n]["sort_id"].ToString());
                    }
                    if (dt.Rows[n]["is_sys"].ToString() != "")
                    {
                        model.is_sys = int.Parse(dt.Rows[n]["is_sys"].ToString());
                    }
                    modelList.Add(model);
                }
            }
            return modelList;
        }

        /// <summary>
        /// ���ǰ��������
        /// </summary>
        public DataSet GetList(int top, string strWhere, string filedOrder)
        {
            return dal.GetList(top, strWhere, filedOrder);
        }


        #endregion
    }
}