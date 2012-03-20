<?php

class controllerusuario{

	private $conexion;
	
	public function controllerusuario(){
		$this->conexion=new bd("sistema");
	}
	
	public function traer(){
		$sql="select * from usuario";
		$pc=$this->conexion->query($sql);
		$i=0;$tablas=array();
		foreach($pc as $p){
			$tabla[$i++]=new registro($p["id"]);
		}
		return $tabla;
	}
	
	public function add($user,$pass){
		$sql="select id from usuario where nombre='".antinject($user)."'";
		$pc=$this->conexion->query($sql);
		if(count($pc) != 0)
			return 0;
		$sql="insert into usuario (nombre,pass) values ('".antinject($user)."','".crypt($pass)."')";
		return $this->conexion->insert($sql);	
	}
	
	public function check($user,$pass){
		$sql="select id,pass from usuario where nombre='".antinject($user)."'";
		$pc=$this->conexion->query($sql);
		if(count($pc) != 1)
			return 0;
		$pswd=$pc[0]["pass"];
		if(crypt($pass,$pswd) == $pswd)
		{
			$_SESSION["user"]=$pc[0]["id"];
			return $pc[0]["id"];
		}
		return 0;
	}

}

?>