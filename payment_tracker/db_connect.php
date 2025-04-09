<?php
$serverName = "db17019.public.databaseasp.net";
$database = "db17019";
$username = "db17019";
$password = "Y+p5h4?X6Fk!";

// Connection to MS SQL Server
try {
    $conn = new PDO("sqlsrv:server=$serverName;Database=$database", $username, $password);
    $conn->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
} catch (Exception $e) {
    die("Connection failed: " . $e->getMessage());
}
?>
