<?php
include 'db_connect.php';

try {
    $query = "SELECT COUNT(ID) FROM Payments1 WHERE ReadAt IS NULL";
    $stmt = $conn->prepare($query);
    $stmt->execute();
    $count = $stmt->fetchColumn();

    echo $count;
} catch (PDOException $e) {
    echo 'error: ' . $e->getMessage();
}
?>