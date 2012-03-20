<?php
class registro{

	private $conexion;
	private $con;
	public $id;
	public $nombre;
	public $tipo;
	public $tabla;
	public $noprefix;
	public $propiedad;
	public $isai;
	
	public function registro($id){
		$sql="select * from registro where id=".antinject($id);
		$this->conexion=new bd("sistema");
		$r=$this->conexion->get($sql);
		$this->id=$r["id"];
		$this->nombre=$r["nombre"];
		$this->tipo=new tiporecord($r["tipo"]);
		$this->tabla=new tabla($r["tabla"]);
		$this->propiedad=new propiedad($r["propiedad"]);
		$this->noprefix=$r["noprefix"];
		$this->con=new bd($this->tabla->bdd->nombre);
		$this->isai=$r["isai"];
	}
	
	public function delete(){
		$sql="ALTER TABLE ".$this->tabla->nombre." DROP INDEX ".$this->nombre;
		$this->con->delete($sql);
		$this->propiedad->delete();
		$sql="delete from registro where id=".$this->id;
		$this->conexion->delete($sql);
	}

}
?>