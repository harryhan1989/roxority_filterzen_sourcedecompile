//��ҳ���ܲ˵�
function MenuMainLink(obj)
{
    var url = "";
    if(obj.id == "td1")         //��ҳ
    {
        url = "../../Web/ReceptionManage/Index.aspx";
        window.top.frames[0].frames["mainFrame"].location.href = url;
    }
    else if(obj.id == "td5")    //����
    {
        url = "../../DLL/�û��ֲ�V1.0.001.doc";
        window.open(url);
    }
    else if(obj.id == "td6")    //ע��
    {
        window.document.getElementById('btnLogout').click(); 
    }
}

