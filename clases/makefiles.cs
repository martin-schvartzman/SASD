<?php

class makefiles{

	private $conexion;
	private $config;
	
	public function makefiles(){
		$this->conexion=new bd("sistema");
		$this->config=new config();
	}
	
	public function deleteredirect($v){
		$ruta=$v->sitio->redirect->ruta;
		//echo $ruta;
		$f=file_get_contents($ruta);
		$nom=$v->nombre;
		$f=str_replace("/* redirect name=".$nom." */".
		between("/* redirect name=".$nom." */","/* endredirect */",$f).
		"/* endredirect */","",$f);
		
		$fh=fopen($ruta,'w');
		fwrite($fh,$f); 
		fclose($fh);
	}
	
	public function generarcarpetaview($nom,$tipo,$r,$do){
		
		$ruta=$r->ruta;
		$f=file_get_contents($ruta);
		$f=str_replace("/* addredirect */",
		"/* redirect name=".$nom." */
if(\$sub=='".$nom."')
include('/views/".$nom.".php');
/* endredirect */
/* addredirect */",$f);
		$fh=fopen($ruta,'w');
		fwrite($fh,$f); 
		fclose($fh);
		@mkdir($this->config->root().antinject($do)."/packages/".antinject($nom)."/");
		@mkdir($this->config->root().antinject($do)."/exe/".antinject($nom)."/");
		@mkdir($this->config->root().antinject($do)."/views/".antinject($nom)."/");
		$fh=fopen($this->config->root().antinject($do)."/views/".antinject($nom).".php",'w');
		//var_dump($this->config->root().antinject($do)."/views/".antinject($nom).".php");
		$text=$tipo->file;
		$text=str_replace("---",antinject($nom),$text);
		fwrite($fh,$text);
		fclose($fh);
	}
	
	public function generarcarpeta($dom){
		@mkdir($this->config->root().antinject($dom));
		@mkdir($this->config->root().antinject($dom)."/packages/");
		//@mkdir($this->config->root().antinject($dom)."/packages/ajax/");
		//@mkdir($this->config->root().antinject($dom)."/packages/index/");
		//@mkdir($this->config->root().antinject($dom)."/packages/admin/");
		@mkdir($this->config->root().antinject($dom)."/clases/");
		@mkdir($this->config->root().antinject($dom)."/exe/");
		//@mkdir($this->config->root().antinject($dom)."/exe/ajax/");
		//@mkdir($this->config->root().antinject($dom)."/exe/index/");
		//@mkdir($this->config->root().antinject($dom)."/exe/admin/");
		@mkdir($this->config->root().antinject($dom)."/views/");
		@mkdir($this->config->root().antinject($dom)."/views/images/");
		//@mkdir($this->config->root().antinject($dom)."/views/ajax/");
		//@mkdir($this->config->root().antinject($dom)."/views/index/");
		//@mkdir($this->config->root().antinject($dom)."/views/admin/");
	}
	
	public function generarredirect($dom,$domi){
		$sql="insert into archivo (ruta,tipo) values ('".$this->config->root().antinject($dom)."/redirect.php',1)";
		$file=new archivo($this->conexion->insert($sql));
		$fh=fopen($file->ruta,'w');
		$text="<?php
session_start();
include('/packages/general.pck');
\$sub='';
\$array=explode('.',\$_SERVER['HTTP_HOST']);
if(count(\$array) == 4 ){
\$sub=\$array[0];
}else if(count(\$array) == 3 ){
\$sub=\"index\";
}

if(\$sub=='www'){
Header( 'HTTP/1.1 301 Moved Permanently' ); 
Header( 'Location: http://".antinject($domi)."'.\$_SERVER['REQUEST_URI'] ); 
}


/* addredirect */



?>
";
		fwrite($fh,$text);
		fclose($fh);
		return $file->id;
	}

	public function generarhtaccess($dom){
		$sql="insert into archivo (ruta,tipo) values ('".$this->config->root().antinject($dom)."/.htaccess',2)";
		$file=new archivo($this->conexion->insert($sql));
		$fh=fopen($file->ruta,'w');
		$text="Options +FollowSymLinks
Options +Indexes
DirectoryIndex redirect.php
RewriteEngine on
";
		fwrite($fh,$text);
		fclose($fh);
		return $file->id;
	}
	
	public function generarpckgeneral($dom){
		$sql="insert into archivo (ruta,tipo) values ('".$this->config->root().antinject($dom)."/packages/general.pck',3)";
		$file=new archivo($this->conexion->insert($sql));
		$fh=fopen($file->ruta,'w');
		$text="<?php
require_once('bd.cs');
/* addpackage */
?>";
		fwrite($fh,$text);
		fclose($fh);
		return $file->id;
	}
	
	public function generarhedview($dom,$view){
		$sql="insert into archivo (ruta,tipo) values ('".$this->config->root().antinject($dom)."/exe/".$view.".hed',4)";
		$file=new archivo($this->conexion->insert($sql));
		$fh=fopen($file->ruta,'w');
		$text="<?php
if(!isset(\$_GET['cat']))
\$category='default';
else
\$category=\$_GET['cat'];
//echo '<script type=\"text/javascript\" src=\"http://ajax.googleapis.com/ajax/libs/jquery/1.4.2/jquery.min.js\"></script>';
echo '<link rel=\"stylesheet\" href=\"/views/".$view."/".$view.".css\"/>';
//echo '<script src=\"/exe/".$view.".js\"></script>';
include('/exe/".$view."/'.\$category.'.hed');
\$diseno='/views/".$view."/'.\$category.'.inc';
?>";
		fwrite($fh,$text);
		fclose($fh);
		return $file->id;
	}
	
	public function generarjsview($dom,$view){
		$sql="insert into archivo (ruta,tipo) values ('".$this->config->root().antinject($dom)."/exe/".$view.".js',6)";
		$file=new archivo($this->conexion->insert($sql));
		$fh=fopen($file->ruta,'w');
		$text="\$(document).ready(function (){
	
});";
		fwrite($fh,$text);
		fclose($fh);
		return $file->id;
	}
	
	public function generarcssview($dom,$view){
		$sql="insert into archivo (ruta,tipo) values ('".$this->config->root().antinject($dom)."/views/".$view."/".$view.".css',7)";
		$file=new archivo($this->conexion->insert($sql));
		$fh=fopen($file->ruta,'w');
		$text="*{
margin:0px;
padding:0px;
border:0px;
}

#main{

}";
		fwrite($fh,$text);
		fclose($fh);
		return $file->id;
	}
	
	public function generarincview($dom,$view){
		$sql="insert into archivo (ruta,tipo) values ('".$this->config->root().antinject($dom)."/views/".$view."/".$view.".inc',5)";
		$file=new archivo($this->conexion->insert($sql));
		$fh=fopen($file->ruta,'w');
		$text="<div id=\"main\">
</div>";
		fwrite($fh,$text);
		fclose($fh);
		return $file->id;
	}
	
	public function generarpckview($dom,$view){
		$sql="insert into archivo (ruta,tipo) values ('".$this->config->root().antinject($dom)."/packages/".$view."/".$view.".pck',3)";
		$file=new archivo($this->conexion->insert($sql));
		$fh=fopen($file->ruta,'w');
		$text="<?php
/* addpackage */
?>";
		fwrite($fh,$text);
		fclose($fh);
		return $file->id;
	}
	
	public function addclasstopck($file,$clase){
		$f=file_get_contents($file->ruta);
		$f=str_replace(
		"/* addpackage */",
		"/* package class=".$clase->id." */ require_once('".$clase->archivo->ruta."');/* endpackage */
/* addpackage */",
		$f);
		$fh=fopen($file->ruta,'w');
		fwrite($fh,$f);
		fclose($fh);
		return $file->id;
	}
	
	public function delclasstopck($file,$clase){
		$f=file_get_contents($file->ruta);
		$f=str_replace("
/* package class=".$clase->id." */ require_once('".$clase->archivo->ruta."');/* endpackage */","",$f);
		$fh=fopen($file->ruta,'w');
		fwrite($fh,$f);
		fclose($fh);
		return $file->id;
	}
	
	public function generarhedseccion($dom,$view,$seccion){
		$sql="insert into archivo (ruta,tipo) values ('".$this->config->root().antinject($dom)."/exe/".$view."/".$seccion.".hed',4)";
		$file=new archivo($this->conexion->insert($sql));
		$fh=fopen($file->ruta,'w');
		$text="<?php
include(\"/packages/".$view."/".$seccion.".pck\"); 
echo '<link rel=\"stylesheet\" href=\"/views/".$view."/".$seccion.".css\"/>';
//echo '<script src=\"/exe/".$view."/".$seccion.".js\"></script>';

?>";
		fwrite($fh,$text);
		fclose($fh);
		return $file->id;
	}
	
	public function generarjsseccion($dom,$view,$seccion){
		$sql="insert into archivo (ruta,tipo) values ('".$this->config->root().antinject($dom)."/exe/".$view."/".$seccion.".js',6)";
		$file=new archivo($this->conexion->insert($sql));
		$fh=fopen($file->ruta,'w');
		$text="\$(document).ready(function (){
	
});";
		fwrite($fh,$text);
		fclose($fh);
		return $file->id;
	}
	
	public function generarcssseccion($dom,$view,$seccion){
		$sql="insert into archivo (ruta,tipo) values ('".$this->config->root().antinject($dom)."/views/".$view."/".$seccion.".css',7)";
		$file=new archivo($this->conexion->insert($sql));
		$fh=fopen($file->ruta,'w');
		$text="#main".$seccion."{

}";
		fwrite($fh,$text);
		fclose($fh);
		return $file->id;
	}
	
	public function generarincseccion($dom,$view,$seccion){
		$sql="insert into archivo (ruta,tipo) values ('".$this->config->root().antinject($dom)."/views/".$view."/".$seccion.".inc',5)";
		$file=new archivo($this->conexion->insert($sql));
		$fh=fopen($file->ruta,'w');
		$text='<div id="main'.$seccion.'">
</div>';
		fwrite($fh,$text);
		fclose($fh);
		return $file->id;
	}
	
	public function generarpckseccion($dom,$view,$seccion){
		$sql="insert into archivo (ruta,tipo) values ('".$this->config->root().antinject($dom)."/packages/".$view."/".$seccion.".pck',3)";
		$file=new archivo($this->conexion->insert($sql));
		$fh=fopen($file->ruta,'w');
		$text="<?php
/* addpackage */
?>";
		fwrite($fh,$text);
		fclose($fh);
		return $file->id;
	}
	
	public function createstarterclass($nom,$dom,$bdd,$tab){
		$sql="insert into archivo (ruta,tipo) values ('".$this->config->root().antinject($dom)."/clases/".$nom.".cs',8)";
		$file=new archivo($this->conexion->insert($sql));
		$fh=fopen($file->ruta,'w');
		$text="<?php
class ".$nom."{
	
	private \$conexion;
	public \$id;
	/* addpropiedad */
	
	public function ".$nom."(\$id){
		\$sql=\"select * from ".$tab." where id=\".antinject(\$id);
		\$this->conexion=new bd(\"".$bdd."\");
		\$r=\$this->conexion->get(\$sql);
		\$this->id=\$r[\"id\"];
		/* addpropload */
	}
	
	public function delete(){
		\$sql=\" delete from ".$tab." where id=\".\$this->id;
		\$this->conexion->delete(\$sql);
	}
	
	/* editmethod */
	/* endeditmethod */
	
	/* newmethod */
	
}
?>";
		fwrite($fh,$text);
		fclose($fh);
		return $file->id;
	}
	
	public function createcontrollerclass($nom,$dom,$bdd,$tab){
		$sql="insert into archivo (ruta,tipo) values ('".$this->config->root().antinject($dom)."/clases/controller".$nom.".cs',8)";
		$file=new archivo($this->conexion->insert($sql));
		$fh=fopen($file->ruta,'w');
		$text="<?php
class controller".$nom."{
	
	private \$conexion;
	
	public function controller".$nom."(){
		\$this->conexion=new bd(\"".$bdd."\");
	}
	
	public function traer(\$page=1,\$amount=30){
		\$sql=\"select id from ".$tab." limit \".(\$page  * \$amount - \$amount).\",\".\$amount.\"\"; 
		\$re=\$this->conexion->query(\$sql);
		\$i=0;\$records=array();
		foreach(\$re as \$r){
			\$records[\$i++]=new ".$nom."(\$r[\"id\"]);
		}
		return \$records;
	}
	
	public function pages(\$amount=30){
		\$sql=\"select count(*) as num from ".$tab."\"; 
		\$re=\$this->conexion->get(\$sql);
		return \$re[\"num\"];
	}
	
	/* addmethod */
	/* endaddmethod */
	
	/* newmethod */
	
}
?>";
		fwrite($fh,$text);
		fclose($fh);
		return $file->id;
	}
	
	public function deletemethod($cls,$id){
		$f=file_get_contents($cls->archivo->ruta);
		$bet=between("/* method id=".$id." */","/* endmethod */",$f);
		//var_dump($bet);
		$f=str_replace("/* method id=".$id." */".$bet."/* endmethod */","",$f);
		$fh=fopen($cls->archivo->ruta,'w');
		fwrite($fh,$f);
		fclose($fh);
	}
	
	public function deleteproperty($cls,$id){
		$f=file_get_contents($cls->archivo->ruta);
		$bet=between("
	/* prop id=".$id." */","/* endprop */",$f);
		//var_dump($bet);
		//var_dump(between("/* prop id=37 */","/* endprop */",$f));
		//var_dump("/* prop id=".$id." */".$bet."/* endprop */");
		$f=str_replace("/* prop id=".$id." */".$bet."/* endprop */","",$f);
		$bet=between("/* propload id=".$id." */","/* endpropload */",$f);
		$f=str_replace("/* propload id=".$id." */".$bet."/* endpropload */","",$f);
		$fh=fopen($cls->archivo->ruta,'w');
		fwrite($fh,$f);
		fclose($fh);
	}
	
	public function makearraymethod($extkey,$tabla,$record,$claselocal){
		$ext=new tabla($extkey);
		$loc=new tabla($tabla);
		$cls=$ext->clase();
		$f=file_get_contents($cls->archivo->ruta);
		$sql="insert into metodo (nombre,descripcion,clase) values ('array".$claselocal."','Trae un array de ".$claselocal."',".$cls->id.")";
		$m1=$this->conexion->insert($sql);
		$sql="insert into metodo (nombre,descripcion,clase) values ('pages".$claselocal."','Trae cantidad de registros ".$claselocal." que tiene esta clase',".$cls->id.")";
		$m2=$this->conexion->insert($sql);
		$f=str_replace("/* newmethod */",
"/* method id=".$m1." */
	public function array".$claselocal."(\$page=1,\$amount=30){
		\$sql=\"select id from ".$loc->nombre." where ".$record."=\".\$this->id.\" limit \".(\$page * \$amount - \$amount).\",\".\$amount.\"\"; 
		\$re=\$this->conexion->query(\$sql);
		\$i=0;\$records=array();
		foreach(\$re as \$r){
			\$records[\$i++]=new ".$claselocal."(\$r[\"id\"]);
		}
		return \$records;
	}
	/* endmethod */

	/* method id=".$m2." */
	public function pages".$claselocal."(\$amount=30){
		\$sql=\"select count(*) as num from ".$loc->nombre." where ".$record."=\".\$this->id.\" \"; 
		\$re=\$this->conexion->get(\$sql);
		return (\$re[\"num\"] / \$amount);
	}
	/* endmethod */
	/* newmethod */",$f);
		$fh=fopen($cls->archivo->ruta,'w');
		fwrite($fh,$f); 
		fclose($fh);
	}
	
	public function addproperty($nom,$cls,$tipo,$id){
		$f=file_get_contents($cls->archivo->ruta);
		$f=str_replace("/* addpropiedad */",
		"/* prop id=".$id." */public \$".$nom.";/* endprop */
	/* addpropiedad */",$f);
		$fh=fopen($cls->archivo->ruta,'w');
		fwrite($fh,$f); 
		fclose($fh);
	}
	
	public function newpropload($cls,$nombre,$witprefix,$extkey,$isint,$id){
		$f=file_get_contents($cls->archivo->ruta);
		if($extkey=="0"){
			$f=str_replace("/* addpropload */",
		"/* propload id=".$id." */\$this->".$nombre."=\$r[\"".$witprefix."\"];/* endpropload */
	/* addpropload */",$f);
		}else{
			$sql="select nombre from clase where not controller = 0 and tabla=".$extkey;
			//echo $sql;
			$clase=$this->conexion->get($sql);
			$clase=$clase["nombre"];
			$f=str_replace("	/* addpropload */",
		"/* propload id=".$id." */\$this->".$nombre."=new ".$clase."(\$r[\"".$witprefix."\"]);/* endpropload */
		/* addpropload */",$f);
		}
		
		
		$fh=fopen($cls->archivo->ruta,'w');
		fwrite($fh,$f); 
		fclose($fh);
	}
	
	public function makeaddmethod($cls,$controller){
		$f=file_get_contents($controller->archivo->ruta);
		$reg=$cls->tabla->getrecords();
		$values=$columns=$params=array();
		for( $i=0 ;$i < count($reg); $i++ ){
			$params[$i]="\$".$reg[$i]->noprefix;
			$columns[$i]=$reg[$i]->nombre;
			if($reg[$i]->tipo->isint == 0){
				$values[$i]="\".antinject(".$params[$i].").\"";
			}else{
				$values[$i]="'\".antinject(".$params[$i].").\"'";
			}
		}
		$params=implode(",",$params);
		$values=implode(",",$values);
		$columns=implode(",",$columns);
		$rep=
"	public function add(".$params."){
		\$sql=\"insert into ".$cls->tabla->nombre." (".$columns.") values (".$values.")\";
		return \$this->conexion->insert(\$sql);
	}";
		$f=rep_between(
"	/* addmethod */","	/* endaddmethod */","
".$rep."
",$f);
		$fh=fopen($controller->archivo->ruta,'w');
		fwrite($fh,$f); 
		fclose($fh);
	}
	
	function makeeditmethod($cls){
		$f=file_get_contents($cls->archivo->ruta);
		//echo $f;
		$reg=$cls->tabla->getrecords();
		$sets=$values=$columns=$params=array();
		for( $i=0 ;$i < count($reg); $i++ ){
			$params[$i]="\$".$reg[$i]->noprefix;
			$columns[$i]=$reg[$i]->nombre;
			if($reg[$i]->tipo->isint == 0){
				$values[$i]="\".antinject(".$params[$i].").\"";
			}else{
				$values[$i]="'\".antinject(".$params[$i].").\"'";
			}
			$sets[$i]=$columns[$i]."=".$values[$i];
		}
		
		$params=implode(",",$params);
		$sets=implode(",",$sets);
		$rep=
"	public function edit(".$params."){
		\$sql=\"update ".$cls->tabla->nombre." set ".$sets." where id=\".\$this->id;
		\$this->conexion->delete(\$sql);
	}";
	//var_dump($rep);
		$f=rep_between("	/* editmethod */","	/* endeditmethod */","
".$rep."
",$f);
		//var_dump($f);
		$fh=fopen($cls->archivo->ruta,'w');
		fwrite($fh,$f); 
		fclose($fh);
	}
	
	function newmethod($cls,$met){
		$f=file_get_contents($cls->archivo->ruta);
		
		$f=str_replace("/* newmethod */",
"/* method id=".$met->id." */
	public function ".$met->nombre."(){
	
	}
	/* endmethod */
	
	/* newmethod */"
		,$f);
		
		$fh=fopen($cls->archivo->ruta,'w');
		fwrite($fh,$f); 
		fclose($fh);
	}
	
}

function rightto($string,$pattern){
	for($i=0;$i<strlen($string);$i++){
		if($string[$i] == $pattern[0]){
			for($j=1;$j<strlen($pattern);$j++){
				$i++;
				if($string[$i] == $pattern[$j]){
					if($j == (strlen($pattern) - 1))return ++$i;
				}else{
					$j=strlen($pattern);
				}
			}
		}
	}
	return -1;
}

function leftto($string,$pattern,$start){
	if($start == -1)return -1;
	for($i=$start;$i<strlen($string);$i++){
		if($string[$i] == $pattern[0]){
			$ret= $i - 1;
			for($j=1;$j<strlen($pattern);$j++){
				$i++;
				if($string[$i] == $pattern[$j]){
					if($j == (strlen($pattern) - 1))return $ret;
				}else{
					$j=strlen($pattern);
				}
			}
		}
	}
	return -1;
}

function between($left,$right,$string){
	$ret=array();$cur=0;
	$r=rightto($string,$left);
	$l=leftto($string,$right,$r);
	//var_dump($r,$l);
	for($i=$r;$i<=$l;$i++){
		if($i == -1)return -1;
		$ret[$cur++]=$string[$i];
	}
	return implode($ret);
}

function rep_between($l,$r,$rep,$string){
	return str_replace($l.between($l,$r,$string).$r,$l.$rep.$r,$string);
}



?>