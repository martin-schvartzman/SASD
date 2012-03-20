<?php
class tipos{

	private $conexion;
	
	public function tipos(){
		$this->conexion=new bd("sistema");
	}
	
	public function records(){
		$sql="select id from tipo_record where extkey=0";
		$pc=$this->conexion->query($sql);
		$i=0;$packages=array();
		foreach($pc as $p){
			$packages[$i++]=new tiporecord($p["id"]);
		}
		return $packages;
	}
	
	public function addrecord($n,$s,$e,$r,$i){
		$sql="insert into tipo_record (id,nombre,sqlcode,extkey,relacion,isint) values (NULL,'".$n."',".$s.",".$e.",".$r.",".$i.")";
		//echo $sql;
		return $this->conexion->insert($sql);
	}
	
	public function vistas(){
		$sql="select id from tipo_vista";
		$pc=$this->conexion->query($sql);
		$i=0;$packages=array();
		foreach($pc as $p){
			$packages[$i++]=new tipovista($p["id"]);
		}
		return $packages;
	}
	
	public function archivos(){
		$sql="select id from tipo_vista";
		$pc=$this->conexion->query($sql);
		$i=0;$packages=array();
		foreach($pc as $p){
			$packages[$i++]=new tipoarchivo($p["id"]);
		}
		return $packages;
	}
	
	

}
?>