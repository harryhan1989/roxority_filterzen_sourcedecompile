<%@ page language="C#" autoeventwireup="true" inherits="Web_Survey.Survey.Survey_MoveItem, Web_Survey" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>

 <script language="javascript" type="text/javascript">
    var IID = 0;
    var d = 0;
    var sCurrItemContent = "";
    <%=sClientJs %>

    function moveItem(){
        if(d==1){   //下移
            for(i=0; i<window.parent.arrItem.length; i++){
                if(window.parent.arrItem[i][0]==IID){
                    sCurrItemContent = window.parent.document.getElementById("Posi_I_"+i).innerHTML;
                    window.parent.document.getElementById("Posi_I_"+i).innerHTML = window.parent.document.getElementById("Posi_I_"+(i+1)).innerHTML;
                    window.parent.document.getElementById("Posi_I_"+(i+1)).innerHTML = sCurrItemContent;
                    window.parent.arrItem[i][0] = window.parent.arrItem[i+1][0]
                    window.parent.arrItem[i+1][0] = IID
                    return;
                }
            }
        }
        else{
             for(i=0; i<window.parent.arrItem.length; i++){
                if(window.parent.arrItem[i][0]==IID){
                    sCurrItemContent = window.parent.document.getElementById("Posi_I_"+i).innerHTML;
                    window.parent.document.getElementById("Posi_I_"+i).innerHTML = window.parent.document.getElementById("Posi_I_"+(i-1)).innerHTML;
                    window.parent.document.getElementById("Posi_I_"+(i-1)).innerHTML = sCurrItemContent;
                    window.parent.arrItem[i][0] = window.parent.arrItem[i-1][0]
                    window.parent.arrItem[i-1][0] = IID
                    return;
                }
            }
            
        }
    }
    
    //window.parent.doMoveItem()
    </script>
    <title>无标题页</title>
</head>
<body onload="window.parent.parent.openEdit()">

</body>
</html>
