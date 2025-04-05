<?php
$serverName = "sql.bsite.net\MSSQL2016";
$database = "waayo69_Clients";
$username = "waayo69_Clients";
$password = "kris123asd";

// Connection to MS SQL Server
try {
    $conn = new PDO("sqlsrv:server=$serverName;Database=$database", $username, $password);
    $conn->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
} catch (Exception $e) {
    die("Connection failed: " . $e->getMessage());
}
?>
