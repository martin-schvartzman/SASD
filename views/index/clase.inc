<div id="mainclase">

	<div id="propiedades">
		<b style="color:white;">&nbsp;<u>Propiedades:</u></b><br/>
			<?php if($cls->controller!=0){ ?>
				<div class="propiedad">conexion</div>
				<div class="propiedad">id</div>
			<?php }else{ ?>
				<div class="propiedad">conexion</div>
			<?php } ?>
		<?php foreach($cls->propiedades() as $p){ ?>
		<form action="" method="POST">
			<div class="propiedad"><?php echo $p->nombre; ?>
				<input style="float:right;" type="submit" value="eliminar" name="deletepropiedad"/>
				<input type="hidden" value="<?php echo $p->id; ?>" name="id" />
			</div>
		</form>
		<?php } ?>
		<form action="" method="POST">
			<div class="propiedad" style="background-color:transparent;">
				<input type="text" style="border:1px solid black;" name="nombre"/>
				<input style="float:right;" type="submit" value="Nuevo" name="newpropiedad"/>
			</div>
		</form>
	</div>
	
	<div id="metodos">
		<b style="color:white;">&nbsp;<u>Metodos:</u></b><br/>
			<?php if($cls->controller!=0){ ?>
				<div class="metodo"><?php echo $cls->nombre; ?>()</div>
				<div class="metodo">delete()</div>
				<div class="metodo">edit()</div>
			<?php }else{ ?>
				<div class="metodo"><?php echo $cls->nombre; ?>()</div>
				<div class="metodo">traer()</div>
				<div class="metodo">add()</div>
			<?php } ?>
		<?php foreach($cls->metodos() as $p){ ?>
		<form action="" method="POST">
			<div class="metodo"><?php echo $p->nombre; ?>()
				<input style="float:right;" type="submit" value="eliminar" name="deletemetodo"/>
				<input type="hidden" value="<?php echo $p->id; ?>" name="id" />
			</div>
		</form>
		<?php } ?>
		<form action="" method="POST">
			<div class="metodo" style="background-color:transparent;">
				<input type="text" style="border:1px solid black;" name="nombre"/>
				<input style="float:right;" type="submit" value="Nuevo" name="newmetodo"/>
			</div>
		</form>
	</div>
	<div style="clear:both;"></div>
	<div id="files">
		<div id="botonerafile">
			<div class="selected" id="class">Clase</div>
		</div>
		<div id="filecontent">
			<div content="class">
			<?php echo str_replace("\t","&nbsp;&nbsp;&nbsp;",nl2br( htmlspecialchars(file_get_contents($cls->archivo->ruta)))); ?></div>
		</div>
	</div>
	
</div>