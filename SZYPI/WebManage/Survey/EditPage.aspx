<%@ page language="C#" autoeventwireup="true" inherits="Web_Survey.Survey.Survey_EditPage, Web_Survey" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
<script language="javascript" type="text/javascript"  src="../SurveyWebEditor/WebEdior.js"></script>
<script language="javascript" type="text/javascript" src="Js/SubWin.js"></script>
    <title>无标题页</title>
    <link href="../css/SurveyList.css" rel="stylesheet" type="text/css" />
	<link href="../SurveyWebEditor/WebEditor.css" rel="stylesheet" type="text/css" />
	<style>

<%=sStyle%>
    </style>
	<script language="javascript" type="text/javascript">

	function SaveOption(){
		document.getElementById("Memo").value = Editor.document.body.innerHTML		
		document.getElementById("Save").disabled = true
		document.getElementById("MyForm").submit();
	}
	

	
window.onload = function(){
	setWin(45,55);
	
	document.getElementById("SID").value = SID;		
	window.parent.parent.openEdit();
	_e = new OQSSEditorFF("MyEditor","colorbox","OQSSEditorMask","_e","getPageContent.aspx?sid="+SID+"&PID="+PID,true);
	_e._initEditor();
	_e._initBT(_e);
	$("SID").value = SID;
	$("PID").value = PID;
			
}	
function submitedit(){
		$("Memo").value = _e._getHTMLContent();	
	}
	
		<%=sClientJs%>
	</script>
</head>
<body class="RightOptionWin">
<div id="EditorNode"></div>

<form action="SavePageContent.aspx" method="post" name="MyForm" id="MyForm"  onSubmit="return submitedit()" class="Myform" target="targetWin">
  <input name="Save" type="submit" class="SaveBT" id="Save"   value="保存" />
  <input name="Cancel" type="button" class="SaveBT" id="Cancel" onclick="window.parent.cancelOption()" value="取消" />
  <input name="SID" type="hidden" id="SID"   /> 
  <input name="PID" type="hidden" id="PID"   /> 
  <textarea name="Memo" id="Memo" style="display:none" value=""></textarea>
</form>
<iframe src="" style="width:100%; height:330px; display:none " id="targetWin" name="targetWin"></iframe>
</body>
</html>
