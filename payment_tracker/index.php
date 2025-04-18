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

    // Generate future occurrences for Paid recurring payments within the selected month + 1 month for potential next occurrence
    if ($status == 'Paid' && !empty($recurringType)) {
        $nextDueDate = clone $dueDate;
        $limitDate = new DateTime();
        $limitDate->modify('+' . (date('n') == $selectedMonth ? 1 : 2) . ' months')->modify('last day of this month'); // Extend limit slightly

        while ($nextDueDate <= $limitDate) {
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

            if ($nextDueDate->format('n') == $selectedMonth) {
                $futurePayment = $payment;
                $futurePayment['DueDate'] = $nextDueDate->format('Y-m-d');
                $allPayments[] = $futurePayment;
            }
            if ($nextDueDate <= new DateTime($payment['DueDate'])) break; // Avoid infinite loops
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
    <meta name="viewport" content="width-device-width, initial-scale=1.0">
    <title>Interactive Payment Calendar</title>

    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.0/css/all.min.css">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.10.5/font/bootstrap-icons.css">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet">
    <link rel="stylesheet" href="styles.css">
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>


</head>
<body>

<div class="d-flex justify-content-center align-items-center mt-4 mb-3 position-relative">
    <div class="position-absolute start-0 ms-5">
        <button id="backToOverview" class="btn btn-primary mt-3" style="display:none;" onclick="goBack()">⬅ Back to Overview</button>
    </div>
    <div class="w-100 text-center">
        <h1 class="mb-0">Payment Tracker</h1>
    </div>
    <div class="position-absolute end-0 me-5">
        <button class="btn btn-outline-secondary position-relative" type="button" id="notifBell" data-bs-toggle="collapse" data-bs-target="#notificationPanel" aria-expanded="false" aria-controls="notificationPanel">
            <i class="fas fa-bell"></i>
            <?php
            $today = date('Y-m-d');
            $endOfWeek = date('Y-m-d', strtotime('next Sunday'));
            $firstOfMonth = date('Y-m-01');
            $lastOfMonth = date('Y-m-t');

            $stmtPending = $conn->prepare("SELECT COUNT(ID) FROM Payments1 WHERE Status = 'Pending' AND DueDate BETWEEN ? AND ?");
            $stmtPending->execute([$today, $endOfWeek]);
            $pendingCount = $stmtPending->fetchColumn();

            $stmtOverdue = $conn->prepare("SELECT COUNT(ID) FROM Payments1 WHERE Status = 'Overdue' AND DueDate <= ?");
            $stmtOverdue->execute([$today]);
            $overdueCount = $stmtOverdue->fetchColumn();

            $stmtPostponed = $conn->prepare("SELECT COUNT(ID) FROM Payments1 WHERE Status = 'Postponed' AND DueDate BETWEEN ? AND ?");
            $stmtPostponed->execute([$firstOfMonth, $lastOfMonth]);
            $postponedCount = $stmtPostponed->fetchColumn();

            $totalNotifications = $pendingCount + $overdueCount + $postponedCount;

            if ($totalNotifications > 0) {
                echo "<span class='position-absolute top-0 start-100 translate-middle badge rounded-pill bg-danger'>$totalNotifications</span>";
            }
            ?>
        </button>

        <div id="notificationPanel" class="collapse position-absolute end-0 mt-2 border border-secondary rounded bg-white shadow p-3" style="width: 500px; max-height: 600px; overflow-y: auto; z-index: 1050;">
            <ul class="list-unstyled mb-0">
                <li><strong>🔔 Notifications</strong></li>
                <li><hr></li>
                <?php
                $today = date('Y-m-d');
                $endOfWeek = date('Y-m-d', strtotime('next Sunday'));
                $firstOfMonth = date('Y-m-01');
                $lastOfMonth = date('Y-m-t');
                ?>

                <li><strong>🕓 Pending This Week</strong></li>
                <?php
                $stmt = $conn->prepare("SELECT ID, PaymentName, DueDate, Price, Location, Category, Recurring, Status FROM Payments1 WHERE Status = 'Pending' AND DueDate BETWEEN ? AND ? ORDER BY DueDate ASC");
                $stmt->execute([$today, $endOfWeek]);
                $pendingWeek = $stmt->fetchAll();
                if ($pendingWeek) {
                    foreach ($pendingWeek as $row) {
                        $timestamp = date('Y-m-d H:i:s');
                        echo "<li class='notification-card' id='notification-".$row['ID']."'>
                                <div class='notification-header'>
                                    <i class='fas fa-clock'></i>
                                    <h6 class='notification-title'>{$row['PaymentName']} - Due: {$row['DueDate']}</h6>
                                </div>
                                <div class='notification-body'>
                                    Price: \$" . number_format($row['Price'], 2) . "<br>
                                    Location: {$row['Location']}<br>
                                    Category: {$row['Category']}<br>
                                    Recurring: " . ($row['Recurring'] ? 'Yes' : 'No') . "<br>
                                    Status: {$row['Status']}<br>
                                    Due Date: {$row['DueDate']}<br>
                                    <span class='notification-timestamp'>Received: {$timestamp}</span>
                                </div>
                            </li>";
                    }
                } else {
                    echo "<li class='small text-muted ms-3'>No pending payments this week.</li>";
                }
                ?>

                <li class="mt-2"><strong>⚠️ Overdue</strong></li>
                <?php
                $stmt = $conn->prepare("SELECT ID, PaymentName, DueDate, Price, Location, Category, Recurring, Status FROM Payments1 WHERE Status = 'Overdue' AND DueDate <= ? ORDER BY DueDate ASC");
                $stmt->execute([$today]);
                $overdueMonth = $stmt->fetchAll();
                if ($overdueMonth) {
                    foreach ($overdueMonth as $row) {
                        $timestamp = date('Y-m-d H:i:s');
                        echo "<li class='notification-card' id='notification-".$row['ID']."'>
                                <div class='notification-header'>
                                    <i class='fas fa-exclamation-triangle'></i>
                                    <h6 class='notification-title'>{$row['PaymentName']} - Due: {$row['DueDate']}</h6>
                                </div>
                                <div class='notification-body'>
                                    Price: \$" . number_format($row['Price'], 2) . "<br>
                                    Location: {$row['Location']}<br>
                                    Category: {$row['Category']}<br>
                                    Recurring: " . ($row['Recurring'] ? 'Yes' : 'No') . "<br>
                                    Status: {$row['Status']}<br>
                                    Due Date: {$row['DueDate']}<br>
                                    <span class='notification-timestamp'>Received: {$timestamp}</span>
                                </div>
                            </li>";
                    }
                } else {
                    echo "<li class='small text-muted ms-3'>No overdue payments.</li>";
                }
                ?>

                <li class="mt-2"><strong>📌 Postponed This Month</strong></li>
                <?php
                $stmt = $conn->prepare("SELECT ID, PaymentName, DueDate, Price, Location, Category, Recurring, Status FROM Payments1 WHERE Status = 'Postponed' AND DueDate BETWEEN ? AND ? ORDER BY DueDate ASC");
                $stmt->execute([$firstOfMonth, $lastOfMonth]);
                $postponedMonth = $stmt->fetchAll();
                if ($postponedMonth) {
                    foreach ($postponedMonth as $row) {
                        $timestamp = date('Y-m-d H:i:s');
                        echo "<li class='notification-card' id='notification-".$row['ID']."'>
                                <div class='notification-header'>
                                    <i class='fas fa-calendar-alt'></i>
                                    <h6 class='notification-title'>{$row['PaymentName']} - Due: {$row['DueDate']}</h6>
                                </div>
                                <div class='notification-body'>
                                    Price: \$" . number_format($row['Price'], 2) . "<br>
                                    Location: {$row['Location']}<br>
                                    Category: {$row['Category']}<br>
                                    Recurring: " . ($row['Recurring'] ? 'Yes' : 'No') . "<br>
                                    Status: {$row['Status']}<br>
                                    Due Date: {$row['DueDate']} <br>
                                    <span class='notification-timestamp'>Received: {$timestamp}</span>
                                </div>
                            </li>";
                    }
                } else {
                    echo "<li class='small text-muted ms-3'>No postponed payments this month.</li>";
                }
                ?>
            </ul>
        </div>
    </div>
</div>

    <p>Select a month to view payment details:</p>
    <div id="monthlyOverview">
        <?php
        for ($i = 1; $i <= 12; $i++) {
            $isCurrent = ($i == date('n')) ? 'border border-primary' : '';
            echo "<div class='month-box $isCurrent' onclick='loadCalendar($i)'>
                    <h4>{$months[$i - 1]}</h4>
                    <span class='status-dot paid'></span> Paid: {$monthlyCounts[$i]['Paid']}<br>
                    <span class='status-dot pending'></span> Pending: {$monthlyCounts[$i]['Pending']}<br>
                    <span class='status-dot overdue'></span> Overdue: {$monthlyCounts[$i]['Overdue']}<br>
                    <span class='status-dot postponed'></span> Postponed: {$monthlyCounts[$i]['Postponed']}
                </div>";
        }
        ?>
    </div>
    <div id="calendar" class="calendar"></div>

    <div class="modal fade" id="paymentModal" tabindex="-1">
        <div class="modal-dialog modal-lg">
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
        let daysInMonth = new Date(new Date().getFullYear(), month, 0).getDate();
        let firstDay = new Date(new Date().getFullYear(), month - 1, 1).getDay();
        let calendarHtml = '<h3>' +
            ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"][month - 1] +
            ' ' + new Date().getFullYear() + '</h3>';
        let dayNames = ["Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat"];

        calendarHtml += '<div style="display:flex;justify-content:center;">';
        dayNames.forEach(d => {
            calendarHtml += '<div class="day day-header" style="font-weight:bold; background:#ddd; width:14.28%; text-align:center;">' + d + '</div>';
        });
        calendarHtml += '</div>';

        for (let d = 0; d < firstDay; d++) {
            calendarHtml += '<div class="day"></div>';
        }

        for (let d = 1; d <= daysInMonth; d++) {
            let paymentsForDay = data.filter(p => new Date(p.DueDate).getDate() == d);
            let statusCounts = {
                Paid: 0,
                Pending: 0,
                Overdue: 0,
                Postponed: 0
            };

            paymentsForDay.forEach(p => {
                statusCounts[p.Status]++;
            });

            let badgeHtml = '';
            for (let status in statusCounts) {
                if (statusCounts[status] > 0) {
                    badgeHtml += `<div class="status-badge ${status.toLowerCase()}">${status}: ${statusCounts[status]}</div>`;
                }
            }

            calendarHtml += `<div class="day day-cell" onclick="showPayments(${d}, ${month})">
                <span class="day-number">${d}</span>
                <div class="badge-wrapper" style="display: flex; flex-wrap: wrap; justify-content: space-between; width: 100%;">${badgeHtml}</div>
            </div>`;
        }


        $('#monthlyOverview').hide();
        $('#backToOverview').show();
        $('#calendar').html(calendarHtml).fadeIn();
    });
}


function showPayments(day, month) {
    const monthNames = [
        "January", "February", "March", "April", "May", "June",
        "July", "August", "September", "October", "November", "December"
    ];

    $.getJSON('fetch_payments.php?month=' + month, function(data) {
        let paymentsForDay = data.filter(p => new Date(p.DueDate).getDate() == day);

        let details = `
            <div class="text-center mb-4">
                <h5 class="mb-0">Payments for</h5>
                <h4><strong>${monthNames[month - 1]}</strong><br><strong>${day}</strong></h4>
            </div>
        `;

        if (paymentsForDay.length === 0) {
            details += `<p class="text-center text-muted">No payments scheduled for this day.</p>`;
        } else {
            details += '<div class="row">';

            paymentsForDay.forEach(p => {
                let recurringText = p.RecurringType ? `<span class="badge bg-info">${p.RecurringType}</span>` : '';
                let statusClass = '';
                let statusText = '';

                if (p.Status === "Paid") {
                    statusClass = "paid";
                    statusText = "Paid";
                } else if (p.Status === "Pending") {
                    statusClass = "pending";
                    statusText = "Pending";
                } else if (p.Status === "Overdue") {
                    statusClass = "overdue";
                    statusText = "Overdue";
                } else if (p.Status === "Postponed") {
                    statusClass = "postponed";
                    statusText = "Postponed";
                }

                details += `
                    <div class="col-md-4 mb-3">
                        <div class="card shadow-sm">
                            <div class="card-body">
                                <h5 class="card-title mb-2">${p.PaymentName} ${recurringText}</h5>
                                <p class="mb-1"><strong>Status:</strong> <span class="badge ${statusClass}">${statusText}</span></p>
                                <p class="mb-1"><strong>Category:</strong> ${p.Category}</p>
                                <p class="mb-1"><strong>Location:</strong> ${p.Location}</p>
                                <p class="mb-1"><strong>Amount:</strong> ₱${p.Price}</p>
                                 <p class="mb-1"><strong>Due Date:</strong> ${p.DueDate}</p>
                            </div>
                        </div>
                    </div>
                `;
            });

            details += '</div>';
        }

        $('#paymentInfo').html(details);
        $('#paymentModal').modal('show');
    });
}

        function goBack() {
            $('#calendar').hide();
            $('#backToOverview').hide();
            $('#monthlyOverview').fadeIn();
        }


function updateNotificationCount() {
    $.get('get_notification_count.php', function(count) {
        if (count > 0) {
            $('#notifBell span').text(count).show();
        } else {
            $('#notifBell span').hide();
        }
    });
}


    </script>
</body>
</html>