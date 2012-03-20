<div id="mainview">
	<h1>Sitio:
		<a href="/<?php echo $view->sitio->id; ?>/site/">
			<?php echo $view->sitio->nombre; ?>
		</a>
	</h1>
	<h2>Vista: <?php echo $view->nombre; ?></h2>
	<form action="/<?php echo $view->sitio->id; ?>/site/" method="POST">
		<input style="float:right;" type="submit" value="eliminar" name="deleteview"/>
		<input type="hidden" value="<?php echo $view->id; ?>" name="id" />
	</form>
	<div style="clear:both;"></div>
	<div id="secciones">
		<b style="color:white;">&nbsp;<u>Secciones:</u></b><br/>
		<?php foreach($view->getsecciones() as $s){ ?>
			<div class="seccion">
			<form method="POST" action="">
				<a href="/<?php echo $s->id; ?>/seccion/">
					<?php echo $s->nombre; ?>
				</a>
					<input type="submit" value="eliminar" name="deleteseccion"/>
					<input type="hidden" value="<?php echo $s->id; ?>" name="id" />
				</form>
			</div>
		<?php } ?>
		<div style="background-color:transparent;" class="seccion">
			<a href="/<?php echo $view->id; ?>/addseccion/">
				<img style="width:20px;" src="/views/images/add.png"/>
					Nueva seccion
			</a>
		</div>
	</div>
	<div id="packages">
		<b style="color:white;">&nbsp;<u>Package:</u></b><br/>
		<div id="pcksitio">
			<b style="color:white;">&nbsp;<u>Sitio:</u></b><br/>
			<?php foreach($view->sitio->package() as $c){ ?>
				<div class="pck">
					<?php echo $c->nombre; ?>
				</div>
			<?php } ?>
			<div style="clear:both;"></div>
		</div>
		<div id="pckview">
			<b style="color:white;">&nbsp;<u>Vista:</u></b><br/>
			<?php foreach($view->packages() as $c){ ?>
				<div class="pck">
					<?php echo $c->nombre; ?>
				</div>
			<?php } ?>
			<div style="clear:both;"></div>
			<div style="background-color: white;width: 100px;text-align: center;padding-top: 5px;
			padding-bottom: 5px;margin-left: 580px;margin-top: 15px;margin-bottom: 15px;">
				<a href="/<?php echo $view->id; ?>/pckview/">Editar</a>
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
			<?php echo str_replace("\t","&nbsp;&nbsp;&nbsp;",nl2br( htmlspecialchars(file_get_contents($view->hed->ruta)))); ?></div>
			<div style="display:none;" content="js">
			<?php echo str_replace("\t","&nbsp;&nbsp;&nbsp;",nl2br( htmlspecialchars(file_get_contents($view->js->ruta)))); ?></div>
			<div style="display:none;" content="inc">
			<?php echo str_replace("\t","&nbsp;&nbsp;&nbsp;",nl2br( htmlspecialchars(file_get_contents($view->inc->ruta)))); ?></div>
			<div style="display:none;" content="css">
			<?php echo str_replace("\t","&nbsp;&nbsp;&nbsp;",nl2br( htmlspecialchars(file_get_contents($view->css->ruta)))); ?></div>
			<div style="display:none;" content="pck">
			<?php echo str_replace("\t","&nbsp;&nbsp;&nbsp;",nl2br( htmlspecialchars(file_get_contents($view->pck->ruta)))); ?></div>
		</div>
	</div>
	
</div>