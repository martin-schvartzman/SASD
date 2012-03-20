<div id="mainseccion">
	<h1>Sitio:
		<a href="/<?php echo $seccion->vista->sitio->id; ?>/site/">
			<?php echo $seccion->vista->sitio->nombre; ?>
		</a>
	</h1>
	<h2>
		Vista:
		<a href="/<?php echo $seccion->vista->id; ?>/view/">
			<?php echo $seccion->vista->nombre; ?>
		</a>
	</h2>
	<h3>
		Seccion:<?php echo $seccion->nombre; ?>
	</h3>
	<form action="/<?php echo $seccion->vista->id; ?>/view/" method="POST">
		<input style="float:right;" type="submit" value="eliminar" name="deleteseccion"/>
		<input type="hidden" value="<?php echo $seccion->id; ?>" name="id" />
	</form>
	<div style="clear:both;"></div>
	<div id="packages">
		<b style="color:white;">&nbsp;<u>Package:</u></b><br/>
		<div id="pcksitio">
			<b style="color:white;">&nbsp;<u>Sitio:</u></b><br/>
			<?php foreach($seccion->vista->sitio->package() as $c){ ?>
				<div class="pck">
					<?php echo $c->nombre; ?>
				</div>
			<?php } ?>
			<div style="clear:both;"></div>
		</div>
		<div id="pckview">
			<b style="color:white;">&nbsp;<u>Vista:</u></b><br/>
			<?php foreach($seccion->vista->packages() as $c){ ?>
				<div class="pck">
					<?php echo $c->nombre; ?>
				</div>
			<?php } ?>
			<div style="clear:both;"></div>
		</div>
		<div id="pckseccion">
			<b style="color:white;">&nbsp;<u>Seccion:</u></b><br/>
			<?php foreach($seccion->packages() as $c){ ?>
				<div class="pck">
					<?php echo $c->nombre; ?>
				</div>
			<?php } ?>
			<div style="clear:both;"></div>
			<div style="background-color: white;width: 100px;text-align: center;padding-top: 5px;
			padding-bottom: 5px;margin-left: 580px;margin-top: 15px;margin-bottom: 15px;">
				<a href="/<?php echo $seccion->id; ?>/pckseccion/">Editar</a>
			</div>
		</div>
	</div>
	
	<div id="files">
		<div id="botonerafile">
			<div class="selected" id="hed">Header PHP</div>
			<div class="notselected" id="js">JavaScript</div>
			<div class="notselected" id="inc">Contenido HTML</div>
			<div class="notselected" id="css">Estilo CSS</div>
			<div class="notselected" id="pck">Package</div>
		</div>
		<div id="filecontent">
			<div content="hed">
			<?php echo str_replace("\t","&nbsp;&nbsp;&nbsp;",nl2br( htmlspecialchars(file_get_contents($seccion->hed->ruta)))); ?></div>
			<div style="display:none;" content="js">
			<?php echo str_replace("\t","&nbsp;&nbsp;&nbsp;",nl2br( htmlspecialchars(file_get_contents($seccion->js->ruta)))); ?></div>
			<div style="display:none;" content="inc">
			<?php echo str_replace("\t","&nbsp;&nbsp;&nbsp;",nl2br( htmlspecialchars(file_get_contents($seccion->inc->ruta)))); ?></div>
			<div style="display:none;" content="css">
			<?php echo str_replace("\t","&nbsp;&nbsp;&nbsp;",nl2br( htmlspecialchars(file_get_contents($seccion->css->ruta)))); ?></div>
			<div style="display:none;" content="pck">
			<?php echo str_replace("\t","&nbsp;&nbsp;&nbsp;",nl2br( htmlspecialchars(file_get_contents($seccion->pck->ruta)))); ?></div>
		</div>
	</div>
</div>