<?php
class archivo{

	private $conexion;
	public $id;
	public $ruta;
	public $tipo;
	
	public function archivo($id){
		$sql="select * from archivo where id=".antinject($id);
		$this->conexion=new bd("sistema");
		//var_dump($sql);
		$r=$this->conexion->get($sql);
		$this->id=$r["id"];
		$this->ruta=$r["ruta"];
		$this->tipo=new tipoarchivo($r["tipo"]);
	}
	
	public function delete(){
		//unlink($this->ruta);
		$sql="delete from archivo where id=".$this->id;
		$this->conexion->delete($sql);
		//eliminar archivo fisico
	}
	
}

?>