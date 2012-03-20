<div id="mainpck">
	<form action="" method="POST">
	<?php foreach($site->clases() as $c){ ?>
		<div class="<?php echo $c->ispackaged($site->package->id)?"ispackaged":"notpackaged";?>">
			<input type="checkbox" name="package<?php echo $c->id;  ?>" 
			<?php echo $c->ispackaged($site->package->id)?"checked":"";?> />
			<?php echo $c->nombre; ?>
		</div>
	<?php } ?>
	<div style="clear:both;padding-top: 50px;
	padding-left: 670px;">
		<input name="editpck" style="padding:10px;cursor:pointer;" type="submit" value="Enviar" />
	</div>
	</form>
</div>