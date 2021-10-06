<?php 
$host = "localhost";
$user = "root";
$password = "";

$bank = "unitybase";
$mysqli = mysqli_connect($host,$user,$password,$bank);

if(mysqli_connect_error())
{
    die("Error 0: Connection Failed");  
}
?>