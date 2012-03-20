<?php
include("/packages/general.pck");


$x=new bd("prueba");
$sql="alter table pru_amigos add column ami_opoi bigint";
var_dump($x->delete($sql));

?>