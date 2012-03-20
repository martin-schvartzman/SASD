<?php

class metodo{
	private $conexion;
	public $id;
	public $nombre;
	public $descripcion;
	public $clase;
	
	public function metodo($id){
		$sql="select * from metodo where id=".antinject($id);
		$this->conexion=new bd("sistema");
		$r=$this->conexion->get($sql);
		$this->id=$r["id"];
		$this->nombre=$r["nombre"];
		$this->descripcion=$r["descripcion"];
		$this->clase=new clase($r["clase"]);
	}
	
	public function delete(){
		$sql="delete from metodo where id=".$this->id;
		$this->conexion->delete($sql);
		$mk=new makefiles();
		$mk->deletemethod($this->clase,$this->id);
	}
	

}

?>