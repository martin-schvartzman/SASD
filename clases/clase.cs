<?php

class clase{

	private $conexion;
	public $id;
	public $nombre;
	public $archivo;
	public $descripcion;
	public $tabla;
	public $controller;
	public $sitio;
	
	public function clase($id){
		$sql="select * from clase where id=".antinject($id);
		$this->conexion=new bd("sistema");
		//var_dump($sql);
		$r=$this->conexion->get($sql);
		$this->id=$r["id"];
		$this->nombre=$r["nombre"];
		$this->descripcion=$r["descripcion"];
		$this->tabla=new tabla($r["tabla"]);
		$this->archivo=new archivo($r["archivo"]);
		$this->controller=$r["controller"];
		$this->sitio=new sitio($r["sitio"]);
	}
	
	public function delete(){
		//eliminar archivo OK
		
		$met=$this->metodos();
		$pro=$this->propiedades();
		foreach($met as $m){
			$m->delete();
		}
		foreach($pro as $p){
			$p->delete();
		}
		$this->archivo->delete();
		$sql="delete from pck where clase=".$this->id;
		$this->conexion->delete($sql);
		$sql="delete from clase where id=".$this->id;
		$this->conexion->delete($sql);
	}
	
	public function propiedades(){
		$sql="select id from propiedad where clase=".$this->id;
		$pc=$this->conexion->query($sql);
		$i=0;$tabla=array();
		foreach($pc as $p){
			$tabla[$i++]=new propiedad($p["id"]);
		}
		return $tabla;
	}
	
	public function metodos(){
		$sql="select id from metodo where clase=".$this->id;
		$pc=$this->conexion->query($sql);
		$i=0;$tabla=array();
		foreach($pc as $p){
			$tabla[$i++]=new metodo($p["id"]);
		}
		return $tabla;
	}
	
	public function addpropiedad($nom,$des,$tipo){
		$x=$this->addproperty($nom,$des);
		$mk=new makefiles();
		//agregar propiedad a clase starter
		$mk->addproperty($nom,$this,$tipo,$x);
		if($this->controller != 0){
			//modificar add en clase controller
			$mk->makeaddmethod($this,new clase($this->controller));
			//modificar edit en clase starter
			$mk->makeeditmethod($this);
		}
		return $x;
	}
	
	public function addproperty($nom,$des){
		$sql="insert into propiedad (nombre,descripcion,clase) values ('".antinject($nom)."','".antinject($des)."',".$this->id.")";
		$x=$this->conexion->insert($sql);
		$mk=new makefiles();
		//$mk->addproperty(antinject($nom),$this,null,$x);
		return $x;
	}
	
	public function addmetodo($nom,$des){
		$sql="insert into metodo (nombre,descripcion,clase) values ('".antinject($nom)."','".antinject($des)."',".$this->id.")";
		$id=$this->conexion->insert($sql);
		//agregar metodo a clase
		$mk=new makefiles();
		$mk->newmethod($this,new metodo($id));
	}
	
	public function ispackaged($p){
		$sql="select count(*) as n from clasespck where pck=".$p." and clase=".$this->id;
		$s=$this->conexion->get($sql);
		//var_dump($s);
		if($s["n"]==0)
			return false;
		return true;
	}

}

?>