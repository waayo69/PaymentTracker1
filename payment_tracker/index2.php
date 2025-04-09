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

    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.0/css/all.min.css">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.10.5/font/bootstrap-icons.css">
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
        .day {
        width: 14.28%;
        height: auto; /* Allow dynamic height based on content */
        min-height: 120px;
        display: inline-block;
        align-items: flex-start;
        justify-content: flex-start;
        border: 1px solid #ddd;
        vertical-align: top;
        position: relative;
        cursor: pointer;
        padding: 4px;
        box-sizing: border-box;
        overflow: hidden;
        word-wrap: break-word;
}
        .day span { background:deepskyblue; display:block; font-size: 14px; margin-bottom: 6px; font-weight: bold; }
        .status-dot {
            width: 16px;
            height: 16px;
            border-radius: 50%;
            display: inline-block;
            margin-right: 5px;
            text-align: center;
            color: white;
            font-size: 10px;
            line-height: 16px;
            vertical-align: middle;
    }
    .status-badge {
            flex: 0 1 48%; /* Allow two per row, wrap to next line */
            height: auto;
            padding: 4px 6px;
            margin: 2px;
            border-radius: 6px;
            color: white;
            font-size: 12px;
            font-weight: bold;
            text-align: center;
            box-sizing: border-box;
            word-wrap: break-word;
            white-space: normal;
            line-height: 1.2;
}

        .paid { background-color: green; }
        .pending { background-color: orange; }
        .overdue { background-color: red; }
        .postponed { background-color: purple; }
    </style>
</head>
<body>

<div class="d-flex justify-content-center align-items-center mt-4 mb-3 position-relative">
    <div class="position-absolute start-0 ms-5">
        <button id="backToOverview" class="btn btn-primary mt-3" style="display:none;" onclick="goBack()">⬅ Back to Overview</button>
    </div>
    <div class="w-100 text-center">
        <h1 class="mb-0">Payment Tracker</h1>
    </div>
    <div class="position-absolute end-0 me-5"> <!-- Moved bell slightly left using me-3 -->
        <button class="btn btn-outline-secondary position-relative" type="button" id="notifBell" data-bs-toggle="collapse" data-bs-target="#notificationPanel" aria-expanded="false" aria-controls="notificationPanel">
            <i class="fas fa-bell"></i>
            
        </button>

        <!-- Notification Vertical Scroll Menu -->
        <div id="notificationPanel" class="collapse position-absolute end-0 mt-2 border border-secondary rounded bg-white shadow p-3" style="width: 500px; max-height: 600px; overflow-y: auto; z-index: 1050;">
            <ul class="list-unstyled mb-0">
                <li><strong>🔔 Notifications</strong></li>
                <li><hr></li>

                <li><strong>🕓 Pending This Week</strong></li>
                <?php
                $today = date('Y-m-d');
                $endOfWeek = date('Y-m-d', strtotime('next Sunday'));
                $stmt = $conn->prepare("SELECT ID, PaymentName, DueDate, Price, Location, Category, Recurring, Status FROM Payments1 WHERE Status = 'Pending' AND DueDate BETWEEN ? AND ?");
                $stmt->execute([$today, $endOfWeek]);
                $pendingWeek = $stmt->fetchAll();
                if ($pendingWeek) {
                    foreach ($pendingWeek as $row) {
                        echo "<li>
                            <a class='text-muted d-block' data-bs-toggle='collapse' href='#payment{$row['ID']}' role='button' aria-expanded='false' aria-controls='payment{$row['ID']}'>
                                • {$row['PaymentName']} - Due: {$row['DueDate']}
                            </a>
                            <div class='collapse' id='payment{$row['ID']}'>
                                <ul class='ms-3'>
                                    <li>Price: \$" . number_format($row['Price'], 2) . "</li>
                                    <li>Location: {$row['Location']}</li>
                                    <li>Category: {$row['Category']}</li>
                                    <li>Recurring: " . ($row['Recurring'] ? 'Yes' : 'No') . "</li>
                                    <li>Status: {$row['Status']}</li>
                                    <li>Due Date: {$row['DueDate']}</li>
                                </ul>
                            </div>
                        </li>";
                    }
                } else {
                    echo "<li class='small text-muted ms-3'>No pending payments this week.</li>";
                }
                ?>

                <li class="mt-2"><strong>⚠️ Overdue This Month</strong></li>
                <?php
                $firstOfMonth = date('Y-m-01');
                $lastOfMonth = date('Y-m-t');
                $stmt = $conn->prepare("SELECT ID, PaymentName, DueDate, Price, Location, Category, Recurring, Status FROM Payments1 WHERE Status = 'Overdue' AND DueDate BETWEEN ? AND ?");
                $stmt->execute([$firstOfMonth, $lastOfMonth]);
                $overdueMonth = $stmt->fetchAll();
                if ($overdueMonth) {
                    foreach ($overdueMonth as $row) {
                        echo "<li>
                            <a class='text-muted d-block' data-bs-toggle='collapse' href='#payment{$row['ID']}' role='button' aria-expanded='false' aria-controls='payment{$row['ID']}'>
                                • {$row['PaymentName']} - Due: {$row['DueDate']}
                            </a>
                            <div class='collapse' id='payment{$row['ID']}'>
                                <ul class='ms-3'>
                                    <li>Price: \$" . number_format($row['Price'], 2) . "</li>
                                    <li>Location: {$row['Location']}</li>
                                    <li>Category: {$row['Category']}</li>
                                    <li>Recurring: " . ($row['Recurring'] ? 'Yes' : 'No') . "</li>
                                    <li>Status: {$row['Status']}</li>
                                    <li>Due Date: {$row['DueDate']}</li>
                                </ul>
                            </div>
                        </li>";
                    }
                } else {
                    echo "<li class='small text-muted ms-3'>No overdue payments this month.</li>";
                }
                ?>

                <li class="mt-2"><strong>📌 Postponed This Month</strong></li>
                <?php
                $stmt = $conn->prepare("SELECT ID, PaymentName, DueDate, Price, Location, Category, Recurring, Status FROM Payments1 WHERE Status = 'Postponed' AND DueDate BETWEEN ? AND ?");
                $stmt->execute([$firstOfMonth, $lastOfMonth]);
                $postponedMonth = $stmt->fetchAll();
                if ($postponedMonth) {
                    foreach ($postponedMonth as $row) {
                        echo "<li>
                            <a class='text-muted d-block' data-bs-toggle='collapse' href='#payment{$row['ID']}' role='button' aria-expanded='false' aria-controls='payment{$row['ID']}'>
                                • {$row['PaymentName']} - Due: {$row['DueDate']}
                            </a>
                            <div class='collapse' id='payment{$row['ID']}'>
                                <ul class='ms-3'>
                                    <li>Price: \$" . number_format($row['Price'], 2) . "</li>
                                    <li>Location: {$row['Location']}</li>
                                    <li>Category: {$row['Category']}</li>
                                    <li>Recurring: " . ($row['Recurring'] ? 'Yes' : 'No') . "</li>
                                    <li>Status: {$row['Status']}</li>
                                    <li>Due Date: {$row['DueDate']}</li>
                                </ul>
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
    <!-- Calendar View -->
    <div id="calendar" class="calendar"></div>

    <!-- Payment Details Modal -->
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
        let daysInMonth = new Date(2025, month, 0).getDate();
        let firstDay = new Date(2025, month - 1, 1).getDay();
        let calendarHtml = '<h3>' + 
            ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"][month - 1] +
            '</h3>';
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

            calendarHtml += `<div class="day" onclick="showPayments(${d}, ${month})">
                <span>${d}</span>
                <div class="badge-wrapper" style="display: inline-table; flex-wrap: wrap; justify-content: space-between; width: 100%;">${badgeHtml}</div>

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
            details += '<div class="row">'; // Start of the horizontal layout container

            paymentsForDay.forEach(p => {
                let recurringText = p.IsRecurring ? `<span class="badge bg-info">Recurring</span>` : '';
                let statusClass = '';
                let statusText = '';

                // Set status class and text based on payment status
                if (p.Status === "Paid") {
                    statusClass = "paid"; // Green for Paid
                    statusText = "Paid";
                } else if (p.Status === "Pending") {
                    statusClass = "pending"; // Orange for Pending
                    statusText = "Pending";
                } else if (p.Status === "Overdue") {
                    statusClass = "overdue"; // Red for Overdue
                    statusText = "Overdue";
                } else if (p.Status === "Postponed") {
                    statusClass = "postponed"; // Purple for Postponed
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
                            </div>
                        </div>
                    </div>
                `;
            });

            details += '</div>'; // End of the row
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
        
    </script>

</body>
</html>
