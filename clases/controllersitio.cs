<?php

class controllersitio{
	
	private $conexion;
	
	public function controllersitio(){
		$this->conexion=new bd("sistema");
	}
	
	public function get(){
		$sql="select * from sitio";
		$pc=$this->conexion->query($sql);
		$i=0;$sitios=array();
		foreach($pc as $p){
			$sitios[$i++]=new sitio($p["id"]);
		}
		return $sitios;
	}
	
	public function add($n,$d,$b){
	$sql="select count(*) as c from sitio where dominio='".antinject($d)."' or nombre='".antinject($n)."'";
	$x=$this->conexion->get($sql);
	$sql="select count(*) as c from bdd where nombre='".antinject($b)."'";
	$y=$this->conexion->get($sql);
	$num=$x["c"] + $y["c"];
	if($num == 0 && antinject($n) != "")
		{
		//crear carpeta
		$f=new makefiles();
		$f->generarcarpeta($n);
		//generar redirect
		$r=$f->generarredirect($n,$d);
		//generar htaccess
		$h=$f->generarhtaccess($n);
		//generar general.pck
		$p=$f->generarpckgeneral($n);
		$sql="insert into sitio (nombre,dominio,redirect,htaccess,package) values ('".antinject($n)."','".antinject($d)."',".$r.",".$h.",".$p.")";
		$s=$this->conexion->insert($sql);
		$sitio=new sitio($s);
		$sitio->makebdd($b);
		$sitio->add("index","Indice del sitio ".$sitio->nombre,4);
		return $sitio->id;
		}else return 0;
	}
	
}

?>