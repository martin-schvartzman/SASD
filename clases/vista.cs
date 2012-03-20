<?php
class vista{

	private $conexion;
	public $id;
	public $nombre;
	public $descripcion;
	public $tipo;
	public $sitio;
	public $pck;
	public $hed;
	public $js;
	public $inc;
	public $css;
	public $seccion;
	
	public function vista($id){
		$sql="select * from vista where id=".antinject($id);
		//var_dump($sql);
		$this->conexion=new bd("sistema");
		$r=$this->conexion->get($sql);
		//var_dump($r);
		$this->id=$r["id"];
		$this->nombre=$r["nombre"];
		$this->descripcion=$r["descripcion"];
		//$this->sitio=$r["sitio"];
		@$this->tipo=new tipovista($r["tipo"]);
		//$this->seccion=new seccion($r["default"]);
		@$this->sitio=new sitio($r["sitio"]);
		@$this->pck=new archivo($r["pck"]);
		@$this->hed=new archivo($r["hed"]);
		@$this->js=new archivo($r["js"]);
		@$this->inc=new archivo($r["inc"]);
		@$this->css=new archivo($r["css"]);
	}
	
	public function edit($n,$t,$d){
		$sql="update vista set nombre='".antinject($n)."',descripcion='".antinject($d)."',tipo=".antinject($t)." where id=".$this->id;
		$this->conexion->delete($sql);
	}
	
	public function delete(){
		$secciones=$this->getsecciones();
		$this->pck->delete();
		$this->hed->delete();
		$this->js->delete();
		$this->inc->delete();
		$this->css->delete();
		foreach($secciones as $s){
			$s->delete();
		}
		$sql="delete from vista where id=".$this->id;
		$this->conexion->delete($sql);
		$mk=new makefiles();
		$mk->deleteredirect($this);
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
	
	public function getsecciones(){
		$sql="select * from seccion where vista=".$this->id;
		$pc=$this->conexion->query($sql);
		$i=0;$secciones=array();
		foreach($pc as $p){
			$secciones[$i++]=new seccion($p["id"]);
		}
		return $secciones;
	}
	
	public function addseccion($n,$d){
	$sql="select count(*) as c from seccion where vista=".$this->id." and nombre='".antinject($n)."'";
	$num=$this->conexion->get($sql);
	if($num["c"]==0 && antinject($n) != ""){
		$mk=new makefiles();
		$sql="select sitio.nombre from sitio,vista where vista.id=".$this->id." and vista.sitio=sitio.id";
		$dom=$this->conexion->get($sql);
		$dom=$dom["nombre"];
		//$dom=$this->sitio->nombre;
		$view=$this->nombre;
		$seccion=$n;
		//generar pck
		//var_dump($sql); 
		$p=$mk->generarpckseccion($dom,$view,$seccion);
		//generar hed
		$h=$mk->generarhedseccion($dom,$view,$seccion);
		//generar js
		$j=$mk->generarjsseccion($dom,$view,$seccion);
		//generar inc
		$i=$mk->generarincseccion($dom,$view,$seccion);
		//generar css
		$c=$mk->generarcssseccion($dom,$view,$seccion);
		$sql="insert into seccion (nombre,descripcion,vista,pck,hed,js,inc,css) values 
		('".antinject($n)."','".antinject($d)."',".$this->id.",".$p.",".$h.",".$j.",".$i.",".$c.")";
		return $this->conexion->insert($sql);
	}else return 0;
	}

}
?>