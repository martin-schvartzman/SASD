<?php
class tiporecord{

	private $conexion;
	public $id;
	public $nombre;
	public $sqlcode;
	public $extkey;
	public $relacion;
	public $isint;
	
	public function tiporecord($id){
		$sql="select * from tipo_record where id=".antinject($id);
		$this->conexion=new bd("sistema");
		$r=$this->conexion->get($sql);
		$this->id=$r["id"];
		$this->nombre=$r["nombre"];
		$this->sqlcode=$r["sqlcode"];
		$this->extkey=$r["extkey"];
		$this->relacion=$r["relacion"];
		$this->isint=$r["isint"];
	}
	
	
	
}
?>