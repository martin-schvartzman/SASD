<div id="maintabla">
<?php if($error)echo "<div>ERROR AL INGRESAR SITIO</div>"; ?>
	<h1>Sitio:
		<a href="/<?php echo $tab->sitio->id; ?>/site/">
			<?php echo $tab->sitio->nombre; ?>
		</a>
	</h1>
	<h2>Tabla:
		<?php echo $tab->noprefix; ?>
	</h2>
	<div id="registros">
		<b style="color:white;">&nbsp;<u>Registros:</u></b><br/>
		
		<div class="registro">
			<div class="nombre"><u>Nombre:</u></div>
			<div class="tipo"><u>Tipo:</u></div>
			<div class="isai"><u>Autonumeric:</u></div>
			<div class="delete"><u>Eliminar:</u></div>
		</div>
		
		<div class="registro">
			<div class="nombre">id</div>
			<div class="tipo">primary key, bigint</div>
			<div class="isai">SI</div>
			<div class="delete"></div>
		</div>
			<?php foreach($tab->getrecords() as $r){ ?>
				<div class="registro">
					<div class="nombre"><?php echo $r->noprefix; ?></div>
					<div class="tipo"><?php if($r->tipo->extkey == "0"){echo $r->tipo->nombre;}else{echo "extkey[".$r->tipo->nombre."]";} ?></div>
					<div class="isai"><?php if($r->isai == "0")echo "NO"; else echo "SI"; ?></div>
					<div class="delete">
					<form action="" method="POST">
							<input type="submit" value="eliminar" name="deleterecord"/>
							<input type="hidden" value="<?php echo $r->id; ?>" name="id" />
						</form>
					</div>
				</div>
			<?php } ?>
		
		<div style="clear:both;"></div>
	</div>
	
	
	<div id="newrecord">
		<form action="" method="POST">
		<b style="color:white;">&nbsp;<u>Agregar Registro:</u></b><br/>
		<div style="height:60px;">
		<div style="float:left;margin:10px;">
		
		<div>Nombre:<input type="text" name="nombre"/></div>
		<div>Auto Increment:<input type="checkbox" name="isai"/></div>
		</div>
		<div style="float:left;margin:10px;">
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
				<?php foreach($tab->bdd->gettables() as $t){ ?>
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
		</div>
		</div>
		<div><input type="submit" value="Enviar" name="addrecord"/></div>
		</form>
		<div style="clear:both;"></div>
	</div>
	
	
	<div class="clase">
	<b style="color:white;">&nbsp;<u>Clase:</u></b><br/>
		<div>
			<a href="/<?php echo $cls->id; ?>/clase/">
			<?php echo $cls->nombre; ?>
			</a>
		</div>
	</div>
	<div class="clase">
	<b style="color:white;">&nbsp;<u>Controller:</u></b><br/>
		<div>
			<a href="/<?php echo $cnt->id; ?>/clase/">
			<?php echo $cnt->nombre; ?>
			</a>
		</div>
	</div>

	
	<div style="clear:both;"></div>
</div>