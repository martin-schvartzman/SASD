<?php

class sitio{
	
	private $conexion;
	public $id;
	public $nombre;
	public $dominio;
	public $redirect;
	public $htaccess;
	public $package;
	
	public function sitio($id){
		$sql="select * from sitio where id=".antinject($id);
		$this->conexion=new bd("sistema");
		$r=$this->conexion->get($sql);
		$this->id=$r["id"];
		$this->nombre=$r["nombre"];
		$this->dominio=$r["dominio"];
		@$this->redirect=new archivo($r["redirect"]);
		@$this->htaccess=new archivo($r["htaccess"]);
		@$this->package=new archivo($r["package"]);
	}
	
	public function edit($n,$d){
		$sql="update sitio set nombre='".antinject($n)."',dominio='".antinject($d)."' where id=".$this->id;
		$this->conexion->delete($sql);
	}
	
	public function package(){
		$sql="select * from clasespck where pck=".$this->package->id;
		$pc=$this->conexion->query($sql);
		$i=0;$vistas=array();
		foreach($pc as $p){
			$vistas[$i++]=new clase($p["clase"]);
		}
		return $vistas;
	}
	
	public function delete(){
		$vistas=$this->vistas();
		//$this->bdd->delete();
		foreach($vistas as $v){
			$v->delete();
		}
		$this->redirect->delete();
		$this->htaccess->delete();
		$this->package->delete();
		$b=$this->bdd();
		$b->delete();
		$sql="delete from sitio where id=".$this->id;
		$this->conexion->delete($sql);
		$sql=
"SELECT id FROM archivo WHERE
 id not in (select pck from seccion) and
 id not in (select hed from seccion) and
 id not in (select js from seccion) and
 id not in (select inc from seccion) and
 id not in (select css from seccion) and
 id not in (select package from sitio) and
id not in (select htaccess from sitio) and
id not in (select redirect from sitio) and
 id not in (select pck from vista) and
 id not in (select hed from vista) and
 id not in (select js from vista) and
 id not in (select inc from vista) and
 id not in (select css from vista) ";
	$arr=$this->conexion->query($sql);
		foreach($arr as $a){
			$sql="delete from archivo where id=".$a["id"];
			$this->conexion->delete($sql);
		}
	}
	
	public function add($n,$d,$t){
		$sql="select count(*) as c from vista where sitio=".$this->id." and nombre='".antinject($n)."'";
		$num=$this->conexion->get($sql);
		if( $num["c"] == 0  && antinject($n) != ""){
		$f=new makefiles();
		$tv=new tipovista($t);
		//generar pck,hed,js,inc,css de vista
		$f->generarcarpetaview($n,$tv,$this->redirect,$this->nombre);
		$h=$f->generarhedview($this->nombre,$n);
		$j=$f->generarjsview($this->nombre,$n);
		$i=$f->generarincview($this->nombre,$n);
		$c=$f->generarcssview($this->nombre,$n);
		$p=$f->generarpckview($this->nombre,$n);
		$sql="insert into vista (nombre,descripcion,tipo,sitio,pck,hed,js,inc,css) values 
		('".antinject($n)."','".antinject($d)."',".antinject($t).",".$this->id.",".$p.",".$h.",".$j.",".$i.",".$c.")";
		$vista=new vista($this->conexion->insert($sql));
		//generar seccion default
		$vista->addseccion("default","seccion por default de ".antinject($n));
		return $vista->id;
		}else return 0;
	}
	
	public function vistas(){
		$sql="select * from vista where sitio=".$this->id;
		$pc=$this->conexion->query($sql);
		$i=0;$vistas=array();
		foreach($pc as $p){
			$vistas[$i++]=new vista($p["id"]);
		}
		return $vistas;
	}
	
	public function makebdd($nombre){
		//crear base de datos
		$sql="CREATE DATABASE ".antinject($nombre);
		//var_dump($sql);
		$this->conexion->delete($sql);
		$sql="insert into bdd (nombre,prefijo,sitio) values 
		('".antinject($nombre)."','".antinject(substr($nombre,0,3))."',".$this->id.")";
		$this->conexion->insert($sql);
	}
	
	public function bdd(){
		$sql="select id from bdd where sitio=".$this->id;
		$r=$this->conexion->get($sql);
		return new bdd($r["id"]);
	}
	
	public function clases(){
		$sql="select * from clase where sitio=".$this->id;
		$pc=$this->conexion->query($sql);
		$i=0;$vistas=array();
		foreach($pc as $p){
			$vistas[$i++]=new clase($p["id"]);
		}
		return $vistas;
	}
	
	public function addpackage($clase){
		$sql="insert into clasespck (pck,clase) values (".$this->package->id.",".$clase.")";
		$this->conexion->delete($sql);
		$mk=new makefiles();
		$mk->addclasstopck($this->package,new clase($clase));
	}
	
	public function delpackage($clase){
		$sql="delete from clasespck where pck=".$this->package->id." and clase=".$clase;
		$this->conexion->delete($sql);
		$mk=new makefiles();
		$mk->delclasstopck($this->package,new clase($clase));
	}
	
}

?>