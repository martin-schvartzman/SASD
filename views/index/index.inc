<center>
<div id="main">
	<div id="botonera">
	<?php if($_SESSION["user"] != 0){ ?>
		<div id="logout" style="margin-bottom:50px;">
			<a href="/?logout">
			<img style="width:20px;" src="/views/images/delete.png"/>
			Log Out
			</a>
		</div>
		<?php foreach($sites as $s){ ?>
			<a href="/<?php echo $s->id; ?>/site/"><div class="sitebutton"><?php echo $s->nombre; ?></div></a>
		<?php } ?>
		<a href="/newsite/">
			<div id="addsitebutton">
				<img style="width:20px;" src="/views/images/add.png"/>
			Crear Sitio</div>
		</a>
	<?php } ?>
	</div>
	<div id="contenido">
		<?php include($diseno); ?>
	</div>
</div>	
</center>