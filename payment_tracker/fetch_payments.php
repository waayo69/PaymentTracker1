<?php
include 'db_connect.php';

$month = $_GET['month'];

$query = "SELECT * FROM Payments1 WHERE MONTH(DueDate) = :month";
$stmt = $conn->prepare($query);
$stmt->execute(['month' => $month]);

$data = $stmt->fetchAll(PDO::FETCH_ASSOC);
echo json_encode($data);
?>
