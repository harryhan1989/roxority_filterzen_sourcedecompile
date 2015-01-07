//首页功能菜单
function MenuMainLink(obj)
{
    var url = "";
    if(obj.id == "td1")         //首页
    {
        url = "../../Web/ReceptionManage/Index.aspx";
        window.top.frames[0].frames["mainFrame"].location.href = url;
    }
    else if(obj.id == "td5")    //帮助
    {
        url = "../../DLL/用户手册V1.0.001.doc";
        window.open(url);
    }
    else if(obj.id == "td6")    //注销
    {
        window.document.getElementById('btnLogout').click(); 
    }
}

