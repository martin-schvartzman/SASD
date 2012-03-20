<?php
class bdd{
	private $conexion;
	private $con;
	public $nombre;
	public $prefijo;
	public $id;
	public $sitio;
	
	public function bdd($id){
		$sql="select * from bdd where id=".antinject($id);
		$this->conexion=new bd("sistema");
		$r=$this->conexion->get($sql);
		$this->id=$r["id"];
		$this->sitio=$r["sitio"];
		$this->nombre=$r["nombre"];
		$this->prefijo=$r["prefijo"];
		$this->con=new bd($this->nombre);
	}
	
	
	public function addtable($nombre){
	$sql="select count(*) as c from tabla where bdd=".$this->id." and noprefix='".antinject($nombre)."'";
	$n=$this->conexion->get($sql);
	if($n["c"] == 0 && antinject($nombre) != "")
		{
		//generar tabla en DB
		$sql="create table ".$this->prefijo."_".antinject($nombre)." (id BIGINT  AUTO_INCREMENT, PRIMARY KEY(id))";
		$this->con->delete($sql);
		$sql="insert into tabla (nombre,prefijo,bdd,noprefix,sitio) values ('".$this->prefijo."_".antinject($nombre)."','".antinject(substr($nombre,0,3))."',".$this->id.",'".antinject($nombre)."',".$this->sitio.")";
		//var_dump($sql);
		$tab=new tabla($this->conexion->insert($sql));
		//var_dump($tab);
		$tab->makeclass();
		return $tab->id;
		}else return 0;
	}
	
	public function gettables(){
		$sql="select id from tabla where bdd=".$this->id;
		$pc=$this->conexion->query($sql);
		$i=0;$tabla=array();
		foreach($pc as $p){
			$tabla[$i++]=new tabla($p["id"]);
		}
		return $tabla;
	}
	
	public function delete(){
		//borrar BDD
		
		$tablas=$this->gettables();
		
		foreach($tablas as $t){
			$t->delete();
		}
		$sql="delete from bdd where id=".$this->id;
		$this->conexion->insert($sql);
		$sql="drop database ".$this->nombre;
		$this->con->delete($sql);
	}
	

}
?>