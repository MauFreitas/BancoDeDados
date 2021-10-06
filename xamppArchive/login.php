<?php
include_once('conexao.php');
$login = $_POST["login"];


$logincheckquery = "SELECT * FROM usuario WHERE email = '" . $login . "';";

$logincheck = mysqli_query($mysqli, $logincheckquery) or die ("Erro 6: Login Failed");

if(mysqli_num_rows($logincheck)!=1)
{
    echo("Erro 7: Email not found");
    exit();
}
$password = $_POST["password"];

$loginfo = mysqli_fetch_assoc($logincheck);
$bdpassword = $loginfo["password"];

if($password != $bdpassword){
    echo ("Erro 08: Incorrect PassWord");
    exit();
}
echo "0\t" , json_encode($loginfo);
//echo("Login feito com sucesso");
?>