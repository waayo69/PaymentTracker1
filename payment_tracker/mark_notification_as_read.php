<?php
include 'db_connect.php';

if (isset($_POST['id']) && is_numeric($_POST['id'])) {
    $paymentId = intval($_POST['id']);

    try {
        $query = "UPDATE Payments1 SET ReadAt = GETDATE() WHERE ID = ?";
        $stmt = $conn->prepare($query);
        $stmt->execute([$paymentId]);

        if ($stmt->rowCount() > 0) {
            echo 'success';
        } else {
            echo 'error: Record not found or already marked as read.';
        }
    } catch (PDOException $e) {
        echo 'error: ' . $e->getMessage();
    }
} else {
    echo 'error: Invalid or missing payment ID.';
}
?>