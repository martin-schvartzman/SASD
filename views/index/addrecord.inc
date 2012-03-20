<div id="mainaddrecord">
<?php if($error)echo "<div>ERROR AL INGRESAR SITIO</div>"; ?>
	<form action="" method="POST">
		<div>Nombre:<input type="text" name="nombre"/></div>
		<div>Auto Increment:<input type="checkbox" name="isai"/></div>
		<div>
			<input type="radio" value="notkey" name="typ" checked/>
			Tipo:
			<select name="tipon">
				<?php foreach($tipos as $t){ ?>
					<option value="<?php echo $t->id; ?>"><?php echo $t->nombre; ?></option>
				<?php } ?>
			</select>
		</div>
		<div>
			<input type="radio" value="extkey" name="typ"/>
			Tabla:
			<select name="tipoe">
				<?php foreach($tabla->bdd->gettables() as $t){ ?>
					<option value="<?php echo $t->id; ?>"><?php echo $t->nombre; ?></option>
				<?php } ?>
			</select>
			&nbsp;
			Relacion:
			<select name="relacion">
				<option value="1">1 ---> 1</option>
				<option value="0">M <--- 1</option>
			</select>
		</div>
		
		<div><input type="submit" value="Enviar" name="addrecord"/></div>
	</form>
</div>