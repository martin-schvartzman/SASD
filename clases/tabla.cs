<?php

class tabla{

	private $conexion;
	private $con;
	public $id;
	public $nombre;
	public $prefijo;
	public $bdd;
	public $sitio;
	public $noprefix;
	
	public function tabla($id){
		$sql="select * from tabla where id=".antinject($id);
		$this->conexion=new bd("sistema");
		$r=$this->conexion->get($sql);
		$this->id=$r["id"];
		$this->nombre=$r["nombre"];
		$this->prefijo=$r["prefijo"];
		$this->bdd=new bdd($r["bdd"]);
		$this->noprefix=$r["noprefix"];
		$this->con=new bd($this->bdd->nombre);
		$this->sitio=new sitio($r["sitio"]);
	}
	
	public function delete(){
		//delete clase
		$sql="select * from clase where tabla=".$this->id;
		$r=$this->conexion->query($sql);
		foreach($r as $c){
			$cl=new clase($c["id"]);
			$cl->delete();
		}
		//delete registros con extern key a la tabla
		$sql="select registro.id from registro,tipo_record where registro.tipo=tipo_record.id and tipo_record.extkey=".$this->id;
		$r=$this->conexion->query($sql);
		foreach($r as $c){
			$cl=new registro($c["id"]);
			$cl->delete();
		}
		$records=$this->getrecords();
		//delete from BD
		
		foreach($records as $r){
			$r->delete();
		}
		$sql="drop table ".$this->nombre;
		$this->con->delete($sql);
		$sql="delete from tabla where id=".$this->id;
		$this->conexion->delete($sql);
		$sql="delete from tipo_record where extkey=".$this->id;
		$this->conexion->delete($sql);
		//$sql="select registro.id as id from registro,tipo_record where tipo_record.extkey=".$this->id."  and registro.tipo=tipo_record.id";
		
	}
	
	public function addrecord($nombre,$tipo,$isai){
	$sql="select count(*) as c from registro where tabla=".$this->id." and noprefix='".antinject($nombre)."'";
	$n=$this->conexion->get($sql);
	
	if($n["c"] == 0 && antinject($nombre) != "")
		{
		$mk=new makefiles();
		$typ=new tiporecord($tipo);
		//add record en BD      OK
		
		if($isai==1 && $typ->isint == 0){
			$ai=" AUTO_INCREMENT";
		}else{
			$ai="";
		}
		
		$typ=new tiporecord($tipo);
		$sql="alter table ".$this->nombre." add column ".$this->prefijo."_".antinject($nombre)." ".$typ->nombre.$ai;
		//echo $sql;
		//var_dump($this->con);
		$this->con->delete($sql);
		//add propiedad en clase  OK
		$sql="select id from clase where tabla=".$this->id." and not controller = 0 ";
		$cls=$this->conexion->get($sql);
		$cls=new clase($cls["id"]);
		//add metodo si clase es M <- 1
		
		
		if($typ->extkey != 0 && $typ->relacion == 0){
			$mk->makearraymethod($typ->extkey,$this->id,$this->prefijo."_".antinject($nombre),$cls->nombre);
		}
		
		
		$id=$cls->addpropiedad($nombre,"",$tipo);
		$sql="insert into registro (nombre,tipo,isai,tabla,noprefix,propiedad) values 
		('".$this->prefijo."_".antinject($nombre)."',".antinject($tipo).",".antinject($isai).",".$this->id.",'".antinject($nombre)."',".$id.")";
		$ret=$this->conexion->insert($sql);
		
		$mk->newpropload($cls,$nombre,$this->prefijo."_".antinject($nombre),$typ->extkey,$typ->isint,$id);
		
		return $ret;
		}else return 0;
	}
	
	public function makeclass(){
		$mk=new makefiles();
		
		$file=$mk->createcontrollerclass($this->noprefix,$this->sitio->nombre,$this->bdd->nombre,$this->nombre);
		$sql="insert into clase (nombre,descripcion,archivo,tabla,controller,sitio) values ('controller".$this->noprefix."','descripcion de clase',".$file.",".$this->id.",0,".$this->sitio->id.")";
		$cont=$this->conexion->insert($sql);
		$file=$mk->createstarterclass($this->noprefix,$this->sitio->nombre,$this->bdd->nombre,$this->nombre); 
		$sql="insert into clase (nombre,descripcion,archivo,tabla,controller,sitio) values ('".$this->noprefix."','descripcion de clase',".$file.",".$this->id.",".$cont.",".$this->sitio->id.")";
		$this->conexion->insert($sql);
	}
	
	public function getrecords(){
		$sql="select id from registro where tabla=".$this->id;
		$pc=$this->conexion->query($sql);
		$i=0;$tabla=array();
		foreach($pc as $p){
			$tabla[$i++]=new registro($p["id"]);
		}
		return $tabla;
	}
	
	public function clase(){
		$sql="select id from clase where tabla=".$this->id." and not controller = 0 ";
		$cls=$this->conexion->get($sql);
		return new clase($cls["id"]);
	}
	
	public function controller(){
		$sql="select id from clase where tabla=".$this->id." and controller = 0 ";
		$cls=$this->conexion->get($sql);
		return new clase($cls["id"]);
	}

}

?>