<!DOCTYPE html>
<html>
<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.0/css/bootstrap.min.css">
<link rel="stylesheet" type="text/css" href="common.css">
<script type="text/javascript" charset="utf8" src="https://ajax.aspnetcdn.com/ajax/jQuery/jquery-2.0.3.js"></script>
<body>
 <div class="container">
    <div id="login"></div>
    <div id="register"></div>
 </div>
</body>
</html>
<script type="text/javascript">
$(document).ready(function(){
    $('#login').load('login.html');
    $('#register').load('register.html');
 });
</script>