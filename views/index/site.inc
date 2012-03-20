<div id="mainsite">
	<h1>Sitio:
	<?php echo $site->nombre ?></h1>
	<a style="float:right;" href="/<?php echo $site->id; ?>/deletesite/">
		<img style="width:20px;" src="/views/images/delete.png"/>
	</a>
	<br/>
	<br/>
	<div style="padding-left:10px;">


		<div id="vistas">
			<b style="color:white;">&nbsp;<u>Vistas:</u></b><br/>
			<?php foreach($site->vistas() as $v){ ?>
				<div class="view"><form action="" method="POST">
					<a href="/<?php echo $v->id; ?>/view/">
						<?php echo $v->nombre; ?> - <?php echo $v->tipo->nombre; ?>
					</a>
					
						<input style="float:right;" type="submit" value="eliminar" name="deleteview"/>
						<input type="hidden" value="<?php echo $v->id; ?>" name="id" />
 					</form>
					<?php foreach($v->getsecciones() as $s){ ?>
						<div class="seccion"><form action="" method="POST">
							<a href="/<?php echo $s->id; ?>/seccion/">
								<?php echo $s->nombre; ?>
							</a>
							
								<input type="submit" value="eliminar" name="deleteseccion"/>
								<input type="hidden" value="<?php echo $s->id; ?>" name="id" />
							</form>
						</div>
					<?php } ?>
					<div class="seccion">
						<a href="/<?php echo $v->id; ?>/addseccion/">
							<img style="width:20px;" src="/views/images/add.png"/>
							Nueva seccion
						</a>
					</div>
				</div>
			<?php } ?>
			<a href="/<?php echo $site->id; ?>/addview/">
				<div id="addviewbutton">
					<img style="width:20px;" src="/views/images/add.png"/>
					Nueva vista
				</div>
			</a>
		</div>

		<div id="bdd"> 
			<b style="color:white;">&nbsp;<u>Tablas:</u></b><br/>
			<?php foreach($bdd->gettables() as $t){ ?>
				<div class="tabla"><form action="" method="POST">
					<a href="/<?php echo $t->id; ?>/tabla/">
						<?php echo $t->noprefix; ?>
					</a>
					<input style="float:right;" type="submit" value="eliminar" name="deletetabla"/>
					<input type="hidden" value="<?php echo $t->id; ?>" name="id" />
					</form>
					<?php foreach($t->getrecords() as $r){ ?>
					<div class="record">
						<form action="" method="POST">
							
								<?php echo $r->noprefix; ?>
							
							<input type="submit" value="eliminar" name="deleterecord"/>
							<input type="hidden" value="<?php echo $r->id; ?>" name="id" />
						</form>
					</div>
					<?php } ?>
					<div class="record">
						<a href="/<?php echo $t->id; ?>/addrecord/">
							<img style="width:20px;" src="/views/images/add.png"/>
							Nuevo registro
						</a>
					</div>
				</div>
			<?php } ?>
			<div style="border:0px;" class="tabla">
				<a href="/<?php echo $bdd->id; ?>/addtabla/">
					<img style="width:20px;" src="/views/images/add.png"/>
					Nueva tabla
				</a>
			</div>
			
		</div> 



	</div>
	
	<div style="width:860px;clear:both;"></div>
	
	<div id="clases">
		<b style="color:white;">&nbsp;<u>Clases:</u></b><br/>
		<?php foreach($site->clases() as $c){ ?>
			<div class="clase">
				<a href="/<?php echo $c->id; ?>/clase/">
					<?php echo $c->nombre; ?>
				</a>
			</div>
		<?php } ?>
		<div style="width:860px;clear:both;"></div>
	</div>
	
	<div id="packages">
		<b style="color:white;">&nbsp;<u>Package:</u></b><br/>
		<?php foreach($site->package() as $c){ ?>
			<div class="pck">
					<?php echo $c->nombre; ?>
			</div>
		<?php } ?>
		<div style="width:860px;clear:both;"></div>
		<div style="background-color: white;width: 100px;text-align: center;padding-top: 5px;
		padding-bottom: 5px;margin-left: 630px;margin-top: 40px;"><a href="/<?php echo $site->id; ?>/pck/">Editar</a></div>
	</div>
	
	<div id="files">
		<div id="botonerafile">
			<div class="selected" id="redirect">Redirect</div>
			<div class="notselected" id="htaccess">Htaccess</div>
			<div class="notselected" id="package">Package</div>
		</div>
		<div id="filecontent">
			<div content="redirect">
			<?php echo str_replace("\t","&nbsp;&nbsp;&nbsp;",nl2br( htmlspecialchars(file_get_contents($site->redirect->ruta)))); ?></div>
			<div style="display:none;" content="htaccess">
			<?php echo str_replace("\t","&nbsp;&nbsp;&nbsp;",nl2br( htmlspecialchars(file_get_contents($site->htaccess->ruta)))); ?></div>
			<div style="display:none;" content="package">
			<?php echo str_replace("\t","&nbsp;&nbsp;&nbsp;",nl2br( htmlspecialchars(file_get_contents($site->package->ruta)))); ?></div>
		</div>
	</div>

</div>