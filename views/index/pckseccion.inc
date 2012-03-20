<div id="mainpck">
	<form action="" method="POST">
	<?php foreach($vclases as $c){ ?>
		<div class="<?php echo $c->ispackaged($seccion->pck->id)?"ispackaged":"notpackaged";?>">
			<input type="checkbox" name="package<?php echo $c->id;  ?>" 
			<?php echo $c->ispackaged($seccion->pck->id)?"checked":"";?> />
			<?php echo $c->nombre; ?>
		</div>
	<?php } ?>
	<div style="clear:both;padding-top: 50px;
	padding-left: 670px;">
		<input name="editpck" style="padding:10px;cursor:pointer;" type="submit" value="Enviar" />
	</div>
	</form>
</div>