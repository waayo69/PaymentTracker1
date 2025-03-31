<?php
include 'db_connect.php';

$query = "SELECT 
            MONTH(DueDate) AS Month,
            COUNT(CASE WHEN Status = 'Paid' THEN 1 END) AS Paid,
            COUNT(CASE WHEN Status = 'Pending' THEN 1 END) AS Pending,
            COUNT(CASE WHEN Status = 'Overdue' THEN 1 END) AS Overdue,
            COUNT(CASE WHEN Status = 'Postponed' THEN 1 END) AS Postponed
          FROM Payments1
          GROUP BY MONTH(DueDate)
          ORDER BY MONTH(DueDate)";

$stmt = $conn->query($query);
$data = $stmt->fetchAll(PDO::FETCH_ASSOC);

echo json_encode($data);
?>
