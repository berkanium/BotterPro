<?php

$versioncode = "2";
if($_GET["v"])
{
 $currentversion = $_GET["v"];
 if($versioncode > $currentversion){
    echo "UPDATE";
  }
   else{
    echo "OK";
   }
}
?>