// JScript 文件
function CheckDelBox(box)    
{
    for (var i = 0; i < document.aspnetForm.elements.length; i++)
	{
	    var e = document.aspnetForm.elements[i];
		if ( (e.type=='checkbox') )
		{
			            
		var o=e.name.lastIndexOf('deleteCheckbox');
			                
		if(o!=-1)
		{
			e.checked = box.checked;
		}
			        
		}
	}
}
		