// *****************************************************************************
// ��������ѭ������ͼƬ
// -----------------------------------------------------------------------------
// �������壺   id              ����DIV
// ---------    picCount        ͼƬ����
// ---------    picNum          ͼƬ�������ڴ�����������
// ************************* ��ʼ **********************************************
function InitScrollPic(id, picCount, picNum)
{
    var ScrollPic = document.getElementById(id);
    var ScrollPic1 = document.getElementById(id + "1");
    var ScrollPic2 = document.getElementById(id + "2");
    
    if (picCount > picNum)
    {
        var speed = 30;
        ScrollPic2.innerHTML = ScrollPic1.innerHTML;
        var myMar = setInterval(_Marquee(id),speed);
        ScrollPic.onmouseover=function() 
        {
            clearInterval(myMar);
        } 
        ScrollPic.onmouseout=function()
        {
            myMar = setInterval(_Marquee(id),speed);
        }
    }
}
function Marquee(id)
{
    var ScrollPic = document.getElementById(id);
    var ScrollPic1 = document.getElementById(id + "1");
    
    if(ScrollPic.scrollLeft >= ScrollPic1.scrollWidth)
    {
        ScrollPic.scrollLeft = 0;
    }
    else
    {
        ScrollPic.scrollLeft++;
    }
}
function _Marquee(id)
{
    return function()
    {
        Marquee(id);
    }
}
// ************************* ���� **********************************************



// *****************************************************************************
// ��������ѭ������ͼƬ
// -----------------------------------------------------------------------------
// �������壺   id          ����DIV
// ---------    picCount    ͼƬ����
// ---------    picNum      ͼƬ�������ڴ�����������
// ************************* ��ʼ **********************************************
function InitScrollPic2(id, picCount, picNum)
{
    var ScrollPic = document.getElementById(id);
    var ScrollPic1 = document.getElementById(id + "1");
    var ScrollPic2 = document.getElementById(id + "2");
    
    if (picCount > picNum)
    {
        var speed = 30;
        ScrollPic2.innerHTML = ScrollPic1.innerHTML;
        var myMar = setInterval(_Marquee2(id),speed);
        ScrollPic.onmouseover=function() 
        {
            clearInterval(myMar);
        } 
        ScrollPic.onmouseout=function()
        {
            myMar = setInterval(_Marquee2(id),speed);
        }
    }
}
function Marquee2(id)
{
    var ScrollPic = document.getElementById(id);
    var ScrollPic1 = document.getElementById(id + "1");
    
    if(ScrollPic.scrollTop >= ScrollPic1.scrollHeight)
    {
        ScrollPic.scrollTop = 0;
    }
    else
    {
        ScrollPic.scrollTop++;
    }
}
function _Marquee2(id)
{
    return function()
    {
        Marquee2(id);
    }
}
// ************************* ���� **********************************************
