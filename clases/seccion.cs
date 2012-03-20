<?php

class seccion{

	private $conexion;
	public $id;
	public $nombre;
	public $descripcion;
	public $vista;
	public $pck;
	public $hed;
	public $js;
	public $inc;
	public $css;
	
	public function seccion($id){
		$sql="select * from seccion where id=".antinject($id);
		$this->conexion=new bd("sistema");
		$r=$this->conexion->get($sql);
		$this->id=$r["id"];
		$this->nombre=$r["nombre"];
		$this->descripcion=$r["descripcion"];
		@$this->vista=new vista($r["vista"]);
		@$this->pck=new archivo($r["pck"]);
		@$this->hed=new archivo($r["hed"]);
		@$this->js=new archivo($r["js"]);
		@$this->inc=new archivo($r["inc"]);
		@$this->css=new archivo($r["css"]);
	}
	
	public function edit($n,$d){
		$sql="update seccion set nombre='".antinject($n)."',descripcion='".antinject($d)."' where id=".$this->id;
		$this->conexion->delete($sql);
	}
	
	public function delete(){
		$this->pck->delete();
		$this->hed->delete();
		$this->js->delete();
		$this->inc->delete();
		$this->css->delete();
		$sql="delete from seccion where id=".$this->id;
		$this->conexion->delete($sql);
	}
	
	public function packages(){
		$sql="select * from clasespck where pck=".$this->pck->id;
		$pc=$this->conexion->query($sql);
		$i=0;$packages=array();
		foreach($pc as $p){
			$packages[$i++]=new clase($p["clase"]);
		}
		return $packages;
	}
	
	public function addpackage($clase){
		$sql="insert into clasespck (pck,clase) values (".$this->pck->id.",".$clase.")";
		$this->conexion->delete($sql);
		$mk=new makefiles();
		$mk->addclasstopck($this->pck,new clase($clase));
	}
	
	public function delpackage($clase){
		$sql="delete from clasespck where pck=".$this->pck->id." and clase=".$clase;
		$this->conexion->delete($sql);
		$mk=new makefiles();
		$mk->delclasstopck($this->pck,new clase($clase));
	}
	
	public function setdefault(){
		$sql="update vista set default=".$this->id." where id=".$this->vista->id;
		$this->conexion->delete($sql);
	}

}

?>