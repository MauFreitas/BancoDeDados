<?php

include_once('conexao.php');
//parte I -> verificando se já existe nome de usuario cadastrado
$username = $_POST["username"];
//bool check
$namecheckquery = "SELECT username as Name FROM usuario WHERE username = '" . $username . "';";


$namecheck = mysqli_query($mysqli, $namecheckquery) or die ("Erro 1: Falha na verificacao, usuario não cadastrado");

if(mysqli_num_rows($namecheck) > 0)
{

$tempinfo = mysqli_fetch_assoc($namecheck);
$nome = $tempinfo["Name"];

echo "Erro 2: Nome (" . $nome . ") ja cadastrado";
exit();
}
echo("success");
//fim da I  parte

$email = $_POST["email"];

$emailcheckquery = "SELECT email from usuario WHERE email = '" . $email . "';";

$emailcheck = mysqli_query($mysqli, $emailcheckquery) or die ("Error 3: Email Check Failed");

if(mysqli_num_rows($emailcheck) > 0)
{
    echo "Error 4: Email Already Exists";
    exit();
} 

$password = $_POST["password"];
//$salt = "\$5\$rounds=5000\$" . "steamedhams" . $username . "\$";
//$hash = crypt($password, $salt);

$insertuserquery = "INSERT INTO usuario (username, email, password) VALUES ('" . $username . "','" . $email . "','" . $password . "' );";

mysqli_query($mysqli, $insertuserquery) or die ("Error 5: Register User Failed");

echo ("success");

?>