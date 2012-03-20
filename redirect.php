<?php
session_start();
$sub="";$array=explode(".",$_SERVER['HTTP_HOST']);if(count($array) == 4 )$sub=$array[0];

include("/packages/general.pck");
include("/views/index.php");

if($sub=="www"){
Header( "HTTP/1.1 301 Moved Permanently" ); 
Header( "Location: http://somos-protagonistas.com.ar".$_SERVER['REQUEST_URI'] ); 
}


?>