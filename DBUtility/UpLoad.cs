using System;
using System.Web;
using System.IO;
using System.Net;
using System.Configuration;

namespace Accounts.DBUtility
{
    public class UpLoad
    {
     
        private string filePath="/upload/"; //上传目录名
        private readonly string fileType; //文件类型
        private readonly int fileSize=0; //文件大小0为不限制
        private readonly int isWatermark; //0为不加水印，1为文字水印，2为图片水印
        private readonly int waterStatus; //水印位置
        private readonly int waterQuality; //水印图片质量
        private readonly string imgWaterPath; //水印图片地址
        private readonly int waterTransparency; //水印图片透时度
        private readonly string textWater; //水印文字
        private readonly string textWaterFont; //文字水印字体
        private readonly int textFontSize; //文字大小

        public UpLoad()
        {
           
        }

        ///<summary>
        /// 文件上传方法
        /// </summary>
        public string fileSaveAs(HttpPostedFile _postedFile)
        {
            try
            {
                string _fileExt = _postedFile.FileName.Substring(_postedFile.FileName.LastIndexOf(".") + 1);
                //验证合法的文件
                if (!CheckFileExt("BMP|JPEG|JPG|GIF|PNG|TIFF|RAR|ZIP|DOC|DOCX|XLS|XLSX", _fileExt))
                {
                    return "{\"status\": 0, \"msg\": \"不允许上传" + _fileExt + "类型的文件！\"}";
                }
                if (this.fileSize > 0 && _postedFile.ContentLength > fileSize * 1024)
                {
                    return "{\"status\": 0, \"msg\": \"文件超过限制的大小啦\"}";
                }
                string _fileName = DateTime.Now.ToString("yyyyMMddHHmmssff") + "." + _fileExt; //随机文件名
                //检查保存的路径 是否有/开头结尾
                if (!this.filePath.StartsWith("/")) this.filePath = "/" + this.filePath;
                if (!this.filePath.EndsWith("/")) this.filePath = this.filePath + "/";
                //按日期归类保存
                string _datePath = DateTime.Now.ToString("yyyyMMdd") + "/";
                this.filePath += _datePath;
                //获得要保存的文件路径
                string serverFileName = this.filePath + _fileName;
                //物理完整路径                    
                string toFileFullPath = HttpContext.Current.Server.MapPath(this.filePath);
                //检查是否有该路径没有就创建
                if (!Directory.Exists(toFileFullPath))
                {
                    Directory.CreateDirectory(toFileFullPath);
                }
                //将要保存的完整文件名                
                string toFile = toFileFullPath + _fileName;
                //保存文件
                _postedFile.SaveAs(toFile);

                return "{\"status\": 1, \"msg\": \"上传文件成功！\", \"name\": \""
                    + _fileName + "\", \"path\": \"" + serverFileName + "\", \"thumb\": \"\", \"size\": " + fileSize + ", \"ext\": \"" + _fileExt + "\"}";
            }
            catch
            {
                return "{\"status\": 0, \"msg\": \"上传过程中发生意外错误！\"}";
            }
        }

        /// <summary>
        /// 检查是否为合法的上传文件
        /// </summary>
        /// <returns></returns>
        private bool CheckFileExt(string _fileType, string _fileExt)
        {
            string[] allowExt = _fileType.Split('|');
            for (int i = 0; i < allowExt.Length; i++)
            {
                if (allowExt[i].ToLower() == _fileExt.ToLower()) { return true; }
            }
            return false;
        }

    }
}
