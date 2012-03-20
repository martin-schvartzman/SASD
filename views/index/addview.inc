<div id="mainaddview">
<?php if($error)echo "<div>ERROR AL INGRESAR SITIO</div>"; ?>
	<h1>Agregar vista a <?php echo $site->nombre ?></h1><br/>
	<form action="" method="POST">
		<div>Nombre:<input type="text" name="nombre"/></div>
		<div>
			Tipo:
			<select name="tipo">
			<?php foreach($tip as $t){ ?>
				<option value="<?php echo $t->id; ?>"><?php echo $t->nombre; ?></option>
			<?php } ?>
			</select>
		</div>
		<div><input type="submit" name="addview" value="Enviar"/></div>
	</form>
</div>