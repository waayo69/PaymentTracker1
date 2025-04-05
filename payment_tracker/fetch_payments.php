<?php
include 'db_connect.php';

$month = $_GET['month'];

$query = "SELECT * FROM Payments1";
$stmt = $conn->prepare($query);
$stmt->execute();

$payments = $stmt->fetchAll(PDO::FETCH_ASSOC);

$allPayments = [];
foreach ($payments as $payment) {
    $dueDate = new DateTime($payment['DueDate']);
    $status = $payment['Status'];
    $recurringType = $payment['RecurringType'];

    $payment['IsRecurring'] = false;

    if ($dueDate->format('n') == $month) {
        $allPayments[] = $payment;
    }

    if ($status == 'Paid' && !empty($recurringType)) {
        $nextDueDate = clone $dueDate;
        switch ($recurringType) {
            case 'Weekly': $nextDueDate->modify('+1 week'); break;
            case 'Monthly': $nextDueDate->modify('+1 month'); break;
            case 'Quarterly': $nextDueDate->modify('+3 months'); break;
            case 'Annually': $nextDueDate->modify('+1 year'); break;
        }

        if ($nextDueDate->format('n') == $month) {
            $futurePayment = $payment;
            $futurePayment['DueDate'] = $nextDueDate->format('Y-m-d');
            $futurePayment['IsRecurring'] = true;
            $allPayments[] = $futurePayment;
        }
    }
}

echo json_encode($allPayments);
?>
