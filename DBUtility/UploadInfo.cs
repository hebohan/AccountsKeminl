using System;
using System.Data;
using System.Configuration;

using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;


public class UploadInfo
{
    public bool IsReady { get; set; }
    public int ContentLength { get; set; }
    public int UploadedLength { get; set; }
    public string FileName { get; set; }
}
