<?php
class tipoarchivo{

	private $conexion;
	public $id;
	public $nombre;
	public $codificacion;
	public $extension;
	
	public function tipoarchivo($id){
		$sql="select * from tipo_archivo where id=".antinject($id);
		$this->conexion=new bd("sistema");
		//var_dump($sql);
		$r=$this->conexion->get($sql);
		$this->id=$r["id"];
		$this->nombre=$r["nombre"];
		$this->codificacion=$r["codificacion"];
		$this->extension=$r["extension"];
	}
	
}
?>