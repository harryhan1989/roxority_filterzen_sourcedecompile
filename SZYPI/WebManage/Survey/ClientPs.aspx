<%@ page language="C#" autoeventwireup="true" inherits="Web_Survey.Survey.Survey_ClientPs, Web_Survey" %>

<% = sSurvey %>
<script type="text/javascript">
    function SetChecked(Name, getValue) {
        //        document.getElementById(FindID).checked = true;
        var optionObj = document.getElementById(Name);
        var vadioLength = document.getElementsByName(Name).length;
     
        if (optionObj) {
            for (var j = 0; j < optionObj.options.length; j++) {
                if (getValue == optionObj.options[j].value) {
                    optionObj.options[j].selected = true;
                }
                //                else {
                //                    optionObj.options[j].setAttribute("disabled", "disabled");
                //                }
            }
            //            document.getElementById(Name).setAttribute("disabled", "disabled");
        }
        else {
            for (var i = 0; i < vadioLength; i++) {
                if (getValue == document.getElementsByName(Name)[i].value) {
                    document.getElementsByName(Name)[i].checked = true;
                }
                document.getElementsByName(Name)[i].setAttribute("disabled", "disabled");
            }
        }
    }

    function SetText(FindID, FindText) {
        document.getElementById(FindID).value = FindText
        document.getElementById(FindID).setAttribute("disabled", "disabled");
    }

    function SetDisplay() {
        document.getElementById("closepagebt").style.display = "";
        document.getElementById("submitbt").style.display = "none";
    }
</script>
<% InitChoose();%>
