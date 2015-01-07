
//文本框边框高亮显示
function TextPanl(obj,IsMouseOver)
{
    if(IsMouseOver)
    {
        obj.style.borderColor = "#389CD8";
    }
    else
    {
        obj.style.borderColor = "";
    }
}