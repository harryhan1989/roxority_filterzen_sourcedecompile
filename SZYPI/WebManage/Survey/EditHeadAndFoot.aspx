<%@ page language="C#" autoeventwireup="true" inherits="Web_Survey.Survey.Survey_EditHeadAndFoot, Web_Survey" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
<script language="javascript" type="text/javascript"  src="../SurveyWebEditor/WebEdior.js"></script>
<script language="javascript" type="text/javascript" src="Js/SubWin.js"></script>
    <title>无标题页</title>
    <link href="../css/SurveyList.css" rel="stylesheet" type="text/css" />
		<link href="../SurveyWebEditor/WebEditor.css" rel="stylesheet" type="text/css" />


	<script language="javascript" type="text/javascript">
	
	
	
	window.onload = function(){
		setWin(45,55);	
    	document.getElementById("SID").value = SID;
		document.getElementById("t").value = t;
		window.parent.parent.openEdit();
		_e = new OQSSEditorFF("MyEditor","colorbox","OQSSEditorMask","_e","getHeadOrFoot.aspx?sid="+SID+"&t="+t,true);
		_e._initEditor();
		_e._initBT(_e);
		$("SID").value = SID;
		$("t").value = t;
		//getHeadOrFoot.aspx?SID=<%=SID%>&t=<%=t%>">
	}
	function submitedit(){
		$("Memo").value = _e._getHTMLContent();	
	}
<%=sClientJs%>
	</script>
</head>
<body>
<div id="EditorNode"></div>
<form action="SaveHeadOrFoot.aspx" method="post" name="MyForm" id="MyForm"  onSubmit="return submitedit()" class="Myform" target="targetWin">
  <input name="Save" type="submit" class="SaveBT" id="Save"  value="保存" />
  <input name="Cancel" type="button" class="SaveBT" id="Cancel" onclick="window.parent.cancelOption()" value="取消" />
  <input name="SID" type="hidden" id="SID" /> 
  <input name="t" type="hidden" id="t" /> 
  <textarea name="Memo" id="Memo" style="display:none"></textarea>
</form>
<iframe src="" style="width:100%; height:330px; display:none " id="targetWin" name="targetWin"></iframe></body>
</html>
