<%@ page language="C#" autoeventwireup="true" inherits="Web_Survey.Survey.Survey_ItemBatchSort, Web_Survey" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>批量排序</title>
      <link href="../css/SurveyList.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="js/ItemBatchSort.js"></script>
     <script language="javascript" type="text/javascript"><%=sClientJs%></script>
</head>
<body>
<div style="margin:5px;">
  <table border="0" style="width:100%">
     <tr>
       <td rowspan="2"><select name="ItemList"  size="20" id="ItemList" style="width:400px" ondblclick="switchSort(1,document.getElementById('ItemList'))">
       </select></td>
       <td><label>
         <input type="button" name="button" id="button" value="上移" style="height:120px; width:60px" onclick="switchSort(-1,document.getElementById('ItemList'))" />
       </label></td>
    </tr>
     <tr>
       <td><input type="button" name="button2" id="button2" value="下移"  style="height:120px; width:60px" onclick="switchSort(1,document.getElementById('ItemList'))"  /></td>
     </tr>
  </table>
  <div style="margin:5px">
  <form action="ItemBatchSortSave.aspx" method="post" name="myform" onsubmit="return checkForm()" target="targetWin" style="margin:0px">
  	<input type="submit" name="submitbt" id="submitbt" value=" 保 存 " />
    <input type="hidden" value="" name="Result" id="Result" />
  
  </form>
  </div>
   </div>
   <iframe id="targetWin" name="targetWin" style="display:none"></iframe>
</body>
</html>
