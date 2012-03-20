<?php

class usuario{
	
	private $conexion;
	public $id;
	public $nombre;
	
	public function usuario($id){
		$sql="select id from usuario";
		$this->conexion=new bd("sistema");
		$u=$this->conexion->get($sql);
		$this->id=$u["id"];
		$this->nombre=$u["nombre"];
	}
	
	public function log(){
		$_SESSION["user"]=$this->id;
	}
	
	public function unlog(){
		$_SESSION["user"]=0;
	}
	
}

?>