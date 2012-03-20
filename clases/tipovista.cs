<?php

class tipovista{
	
	private $conexion;
	public $id;
	public $nombre;
	public $file;
	
	public function tipovista($id){
		$sql="select * from tipo_vista where id=".antinject($id);
		$this->conexion=new bd("sistema");
		$r=$this->conexion->get($sql);
		$this->id=$r["id"];
		$this->nombre=$r["nombre"];
		$this->file=$r["file"];
	}
}

?>