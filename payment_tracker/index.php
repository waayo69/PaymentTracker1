<?php
include 'db_connect.php';

// Get the selected month
$selectedMonth = isset($_GET['month']) ? intval($_GET['month']) : date('n');

// Fetch payments
$query = "SELECT * FROM Payments1";
$stmt = $conn->prepare($query);
$stmt->execute();
$payments = $stmt->fetchAll(PDO::FETCH_ASSOC);

// Process payments, including recurring ones
$allPayments = [];
foreach ($payments as $payment) {
    $dueDate = new DateTime($payment['DueDate']);
    $status = $payment['Status'];
    $recurringType = $payment['RecurringType'];

    // Add original payment
    $allPayments[] = $payment;

    // Generate future occurrences for Paid recurring payments
    if ($status == 'Paid' && !empty($recurringType)) {
        $nextDueDate = clone $dueDate;
        switch ($recurringType) {
            case "Weekly":
                $nextDueDate->modify('+1 week');
                break;
            case "Monthly":
                $nextDueDate->modify('+1 month');
                break;
            case "Quarterly":
                $nextDueDate->modify('+3 months');
                break;
            case "Annually":
                $nextDueDate->modify('+1 year');
                break;
        }

        // Add future occurrence if it's within the selected month
        if ($nextDueDate->format('n') == $selectedMonth) {
            $futurePayment = $payment;
            $futurePayment['DueDate'] = $nextDueDate->format('Y-m-d');
            $allPayments[] = $futurePayment;
        }
    }
}

// Organize payments by month
$monthlyCounts = array_fill(1, 12, ['Paid' => 0, 'Pending' => 0, 'Overdue' => 0, 'Postponed' => 0]);

foreach ($allPayments as $payment) {
    $month = (new DateTime($payment['DueDate']))->format('n');
    $monthlyCounts[$month][$payment['Status']]++;
}

$months = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
?>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Interactive Payment Calendar</title>

    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet">
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>

    <style>
        body { font-family: Arial, sans-serif; background-color: #f8f9fa; text-align: center; }
        .month-box { display: inline-block; width: 200px; height: 160px; margin: 10px; padding: 15px; background: #fff; border-radius: 10px; box-shadow: 2px 2px 10px #ccc; cursor: pointer; text-align: left; }
        .month-box h4 { margin-bottom: 5px; }
        .status-dot { width: 12px; height: 12px; border-radius: 50%; display: inline-block; margin-right: 5px; }
        .paid { background-color: green; }
        .pending { background-color: orange; }
        .overdue { background-color: red; }
        .postponed { background-color: purple; }
        .calendar { width: 90%; margin: auto; background: white; padding: 20px; border-radius: 10px; box-shadow: 0px 0px 10px #ccc; display: none; }
        .day { width: 14.28%; height: 100px; display: inline-block; border: 1px solid #ddd; vertical-align: top; position: relative; cursor: pointer; }
        .day span { position: absolute; top: 5px; left: 5px; font-weight: bold; }
    </style>
</head>
<body>

    <h1 class="mt-4">Payment Tracker</h1>
    <p>Select a month to view payment details:</p>

    <!-- Monthly Overview -->
    <div id="monthlyOverview">
        <?php
        for ($i = 1; $i <= 12; $i++) {
            echo "<div class='month-box' onclick='loadCalendar($i)'>
                    <h4>{$months[$i - 1]}</h4>
                    <span class='status-dot paid'></span> Paid: {$monthlyCounts[$i]['Paid']}<br>
                    <span class='status-dot pending'></span> Pending: {$monthlyCounts[$i]['Pending']}<br>
                    <span class='status-dot overdue'></span> Overdue: {$monthlyCounts[$i]['Overdue']}<br>
                    <span class='status-dot postponed'></span> Postponed: {$monthlyCounts[$i]['Postponed']}
                  </div>";
        }
        ?>
    </div>
    <button id="backToOverview" class="btn btn-primary mt-3" style="display:none;" onclick="goBack()">⬅ Back to Overview</button>
    
    <!-- Calendar View -->
    <div id="calendar" class="calendar"></div>

    <!-- Payment Details Modal -->
    <div class="modal fade" id="paymentModal" tabindex="-1">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Payment Details</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                </div>
                <div class="modal-body" id="paymentInfo"></div>
            </div>
        </div>
    </div>

    <script>
        function loadCalendar(month) {
            $.getJSON('fetch_payments.php?month=' + month, function(data) {
                let daysInMonth = new Date(2025, month, 0).getDate();
                let firstDay = new Date(2025, month - 1, 1).getDay();
                let calendarHtml = '<h3>' + ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"][month - 1] + '</h3>';
                let dayNames = ["Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat"];

                calendarHtml += '<div style="display:flex;justify-content:center;">';
                dayNames.forEach(d => {
                    calendarHtml += '<div class="day" style="font-weight:bold; background:#ddd; width:14.28%; text-align:center;">' + d + '</div>';
                });
                calendarHtml += '</div>';

                for (let d = 0; d < firstDay; d++) {
                    calendarHtml += '<div class="day"></div>';
                }

                for (let d = 1; d <= daysInMonth; d++) {
                    let statusDots = '';
                    let paymentsForDay = data.filter(p => new Date(p.DueDate).getDate() == d);

                    paymentsForDay.forEach(p => {
                        statusDots += `<span class="status-dot ${p.Status.toLowerCase()}"></span>`;
                    });

                    calendarHtml += `<div class="day" onclick="showPayments(${d}, ${month})">
                        <span>${d}</span>
                        ${statusDots}
                    </div>`;
                }

                $('#monthlyOverview').hide();
                $('#backToOverview').show();
                $('#calendar').html(calendarHtml).fadeIn();
            });
        }

        function showPayments(day, month) {
            $.getJSON('fetch_payments.php?month=' + month, function(data) {
                let details = `<b>Payments for ${day}:</b><br>`;
                let paymentsForDay = data.filter(p => new Date(p.DueDate).getDate() == day);

                paymentsForDay.forEach(p => {
                    details += `<hr><b>${p.PaymentName}</b><br>Status: ${p.Status}<br>`;
                });

                $('#paymentInfo').html(details);
                $('#paymentModal').modal('show');
            });
        }

        function goBack() {
            $('#calendar').hide();
            $('#backToOverview').hide();
            $('#monthlyOverview').fadeIn();
        }
    </script>

</body>
</html>
