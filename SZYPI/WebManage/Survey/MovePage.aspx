<%@ page language="C#" autoeventwireup="true" inherits="Web_Survey.Survey.Survey_MovePage, Web_Survey" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>无标题页</title>
    <script language="javascript" type="text/javascript">
    var PID = 0;
    var d = 0;
    var sCurrPageContent = "";
    var sCurrPID = 0;
    <%=sClientJs %>

    function movePage(){
        if(d==1){   //下移
            for(i=0; i<window.parent.arrPageNo.length; i++){
                if(window.parent.arrPageNo[i]==PID){
                    sCurrPageContent = window.parent.document.getElementById("Posi_P_"+i).innerHTML;
                    window.parent.document.getElementById("Posi_P_"+i).innerHTML = window.parent.document.getElementById("Posi_P_"+(i+1)).innerHTML;
                    window.parent.document.getElementById("Posi_P_"+(i+1)).innerHTML = sCurrPageContent;
                    window.parent.arrPageNo[i] = window.parent.arrPageNo[i+1]
                    window.parent.arrPageNo[i+1] = PID
                    return;
                }
            }
        }
        else{
             for(i=0; i<window.parent.arrPageNo.length; i++){
                if(window.parent.arrPageNo[i]==PID){
                    sCurrPageContent = window.parent.document.getElementById("Posi_P_"+i).innerHTML;
                    window.parent.document.getElementById("Posi_P_"+i).innerHTML = window.parent.document.getElementById("Posi_P_"+(i-1)).innerHTML;
                    window.parent.document.getElementById("Posi_P_"+(i-1)).innerHTML = sCurrPageContent;
                    window.parent.arrPageNo[i] = window.parent.arrPageNo[i-1]
                    window.parent.arrPageNo[i-1] = PID
                    return;
                }
            }
            
        }
    }
    
    //movePage()
    </script>
</head>
<body onload="window.parent.parent.openEdit();">

</body>
</html>
