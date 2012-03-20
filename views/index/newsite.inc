<div id="mainnewsite">
	<?php if($error)echo "<div>ERROR AL INGRESAR SITIO</div>"; ?>
	<h1>Ingresar nuevo sitio</h1>
	<form action="" method="POST">
		<div>Nombre:<input type="text" name="nombre"></div>
		<div>Dominio:<input type="text" name="dominio"></div>
		<div>BDD:<input type="text" name="bdd"></div>
		<div><input type="submit" id="button" style="height:50px;width:200px;margin-left:50px;" name="newsite" value="Enviar"/></div>
	</form>
</div>