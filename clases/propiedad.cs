<?php

class propiedad{
	private $conexion;
	public $id;
	public $nombre;
	public $descripcion;
	public $clase;
	
	public function propiedad($id){
		$sql="select * from propiedad where id=".antinject($id);
		$this->conexion=new bd("sistema");
		$r=$this->conexion->get($sql);
		$this->id=$r["id"];
		$this->nombre=$r["nombre"];
		$this->descripcion=$r["descripcion"];
		$this->clase=new clase($r["clase"]);
	}
	
	public function delete(){
		$sql="delete from propiedad where id=".$this->id;
		$this->conexion->delete($sql);
		$mk=new makefiles();
		$mk->deleteproperty($this->clase,$this->id);
	}

}

?>