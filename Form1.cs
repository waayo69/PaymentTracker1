using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace PaymentTracker1
{
    public partial class Form1 : Form
    {
        private string connectionString = @"Data Source=sql.bsite.net\MSSQL2016;Initial Catalog=waayo69_Clients;User ID=waayo69_Clients;Password=kris123asd;Encrypt=False; Connection Timeout=30";

        public Form1()
        {
            InitializeComponent();
            LoadPayments();
        }

        private void LoadPayments()
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Payments1";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                da.Fill(dt);
            }

            // Separate Tables for Paid and Unpaid Payments
            DataTable unpaidTable = dt.Clone(); // For unpaid payments
            DataTable paidTable = dt.Clone(); // For paid payments

            foreach (DataRow row in dt.Rows)
            {
                bool isPaid = row["Status"].ToString() == "Paid";

                if (isPaid)
                {
                    paidTable.ImportRow(row); // Add "Paid" payments to Paid Tab
                }
                else
                {
                    unpaidTable.ImportRow(row); // Add Unpaid payments to Unpaid Tab
                }

                // Handle recurring payments (for Paid ones only)
                if (isPaid && !string.IsNullOrEmpty(row["RecurringType"].ToString()))
                {
                    DateTime dueDate = Convert.ToDateTime(row["DueDate"]);
                    string recurringType = row["RecurringType"].ToString();
                    DateTime nextDueDate = dueDate;

                    switch (recurringType)
                    {
                        case "Weekly": nextDueDate = dueDate.AddDays(7); break;
                        case "Monthly": nextDueDate = dueDate.AddMonths(1); break;
                        case "Quarterly": nextDueDate = dueDate.AddMonths(3); break;
                        case "Annually": nextDueDate = dueDate.AddYears(1); break;
                    }

                    // Create a new row with updated due date
                    DataRow newRow = unpaidTable.NewRow();
                    newRow.ItemArray = row.ItemArray.Clone() as object[];
                    newRow["DueDate"] = nextDueDate;
                    newRow["ID"] = DBNull.Value; // Indicate it's dynamically generated
                    newRow["Status"] = "Unpaid"; // Reset to Unpaid for next occurrence
                    unpaidTable.Rows.Add(newRow);
                }
            }

            // Bind to DataGridViews
            dgvPayments1.DataSource = unpaidTable; // Unpaid Payments in Tab 1
            dgvPaidPayments.DataSource = paidTable; // Paid Payments in Tab 2
            dgvUnpaidPayments.DataSource = unpaidTable;
        }

        private void ApplyStatusFilter()
        {
            if (cboFilter.SelectedItem != null)
            {
                string selectedStatus = cboFilter.SelectedItem.ToString();
                string filterExpression = "";

                switch (selectedStatus)
                {
                    case "All":
                        filterExpression = "";
                        break;
                    case "Paid":
                        filterExpression = "Status = 'Paid'";
                        break;
                    case "Unpaid":
                        filterExpression = "Status = 'Unpaid'";
                        break;
                    case "Overdue":
                        filterExpression = "DisplayStatus = 'Overdue'";
                        break;
                    case "Postponed":
                        filterExpression = "Status = 'Postponed'"; // Assuming you have a 'Postponed' status
                        break;
                }

                // Apply the filter to the single DataGridView (dgvPayments1)
                if (dgvPayments1.DataSource is DataTable dt)
                {
                    dt.DefaultView.RowFilter = filterExpression;
                }
            }
        }


        private void MarkAsPaid(int paymentId, DateTime dueDate, bool isRecurring, string recurringType)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                // Update the selected payment's status to "Paid" and set PaidDate to today
                using (SqlCommand cmd = new SqlCommand(
                    "UPDATE Payments1 SET Status = 'Paid', PaidDate = @PaidDate WHERE ID = @ID", con))
                {
                    cmd.Parameters.AddWithValue("@ID", paymentId);
                    cmd.Parameters.AddWithValue("@PaidDate", DateTime.Now);
                    cmd.ExecuteNonQuery();
                }

                // If the payment is recurring, generate the next occurrence
                if (isRecurring && !string.IsNullOrEmpty(recurringType))
                {
                    // Calculate the next due date based on the recurrence type
                    DateTime nextDueDate = dueDate;
                    switch (recurringType)
                    {
                        case "Weekly": nextDueDate = dueDate.AddDays(7); break;
                        case "Monthly": nextDueDate = dueDate.AddMonths(1); break;
                        case "Quarterly": nextDueDate = dueDate.AddMonths(3); break;
                        case "Annually": nextDueDate = dueDate.AddYears(1); break;
                    }

                    // Insert the next occurrence as "Pending"
                    using (SqlCommand insertCmd = new SqlCommand(
                        "INSERT INTO Payments1 (PaymentName, Price, Location, Category, DueDate, Recurring, RecurringType, Status, PaidDate, ProofOfPaymentPath) " +
                        "SELECT PaymentName, Price, Location, Category, @NextDueDate, Recurring, RecurringType, 'Pending', NULL, NULL FROM Payments1 WHERE ID = @ID", con))
                    {
                        insertCmd.Parameters.AddWithValue("@NextDueDate", nextDueDate);
                        insertCmd.Parameters.AddWithValue("@ID", paymentId);
                        insertCmd.ExecuteNonQuery();
                    }
                }
            }

            LoadPayments(); // Refresh the DataGridView
        }



        private void btnSave_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO Payments1 (PaymentName, Price, Location, Category, DueDate, Recurring, RecurringType, Status, PaidDate, ProofOfPaymentPath) VALUES (@PaymentName, @Price, @Location, @Category, @DueDate, @Recurring, @RecurringType, @Status, @PaidDate, @ProofOfPaymentPath)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PaymentName", txtPaymentName.Text);
                    cmd.Parameters.AddWithValue("@Price", numPrice.Value);
                    cmd.Parameters.AddWithValue("@Location", cmbLocation.Text);
                    cmd.Parameters.AddWithValue("@Category", cmbCategory.Text);
                    cmd.Parameters.AddWithValue("@DueDate", dtpDueDate.Value);
                    cmd.Parameters.AddWithValue("@Recurring", chkRecurring.Checked);
                    cmd.Parameters.AddWithValue("@RecurringType", chkRecurring.Checked ? cmbRecurringType.Text : (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Status", cmbStatus.Text);
                    cmd.Parameters.AddWithValue("@PaidDate", cmbStatus.Text == "Paid" ? dtpPaidDate.Value : (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@ProofOfPaymentPath", lblFilePath.Text);

                    cmd.ExecuteNonQuery();
                }
            }
            LoadPayments();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvPayments1.SelectedRows.Count > 0)
            {
                DataRowView selectedRow = dgvPayments1.SelectedRows[0].DataBoundItem as DataRowView;

                if (selectedRow["ID"] == DBNull.Value)
                {
                    MessageBox.Show("Cannot update a dynamically generated recurring payment.");
                    return;
                }
                int id = Convert.ToInt32(dgvPayments1.SelectedRows[0].Cells["ID"].Value);

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "UPDATE Payments1 SET PaymentName=@PaymentName, Price=@Price, Location=@Location, Category=@Category, DueDate=@DueDate, Recurring=@Recurring, RecurringType=@RecurringType, Status=@Status, PaidDate=@PaidDate, ProofOfPaymentPath=@ProofOfPaymentPath WHERE ID=@ID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", id);
                        cmd.Parameters.AddWithValue("@PaymentName", txtPaymentName.Text);
                        cmd.Parameters.AddWithValue("@Price", numPrice.Value);
                        cmd.Parameters.AddWithValue("@Location", cmbLocation.Text);
                        cmd.Parameters.AddWithValue("@Category", cmbCategory.Text);
                        cmd.Parameters.AddWithValue("@DueDate", dtpDueDate.Value);
                        cmd.Parameters.AddWithValue("@Recurring", chkRecurring.Checked);
                        cmd.Parameters.AddWithValue("@RecurringType", chkRecurring.Checked ? cmbRecurringType.Text : (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Status", cmbStatus.Text);
                        cmd.Parameters.AddWithValue("@PaidDate", cmbStatus.Text == "Paid" ? dtpPaidDate.Value : (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@ProofOfPaymentPath", lblFilePath.Text);

                        cmd.ExecuteNonQuery();
                    }
                }
                LoadPayments();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvPayments1.SelectedRows.Count > 0)
            {
                DataRowView selectedRow = dgvPayments1.SelectedRows[0].DataBoundItem as DataRowView;

                if (selectedRow["ID"] == DBNull.Value)
                {
                    MessageBox.Show("Cannot delete a dynamically generated recurring payment.");
                    return;
                }

                int id = Convert.ToInt32(dgvPayments1.SelectedRows[0].Cells["ID"].Value);

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "DELETE FROM Payments1 WHERE ID=@ID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", id);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            LoadPayments();
        }


        private void cmbFilterLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFilterLocation.SelectedItem != null)
            {
                string selectedLocation = cmbFilterLocation.SelectedItem.ToString();
                (dgvPayments1.DataSource as DataTable).DefaultView.RowFilter = $"Location = '{selectedLocation}'";
            }
        }


        private void btnUpload_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "PDF Files|*.pdf|All Files|*.*",
                Title = "Select Proof of Payment"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                string savePath = Path.Combine(Application.StartupPath, "Proofs", Path.GetFileName(filePath));

                Directory.CreateDirectory(Path.Combine(Application.StartupPath, "Proofs"));
                File.Copy(filePath, savePath, true);
                lblFilePath.Text = savePath;
            }
        }

        private void chkRecurring_CheckedChanged(object sender, EventArgs e)
        {
            cmbRecurringType.Visible = chkRecurring.Checked;
            lblRecurringType.Visible = chkRecurring.Checked;
        }

        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            dtpPaidDate.Enabled = (cmbStatus.Text == "Paid");
            dtpPaidDate.Visible = (cmbStatus.Text == "Paid");
        }


        private void dgvPayments1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvPayments1.Rows[e.RowIndex];
                txtPaymentName.Text = row.Cells["PaymentName"].Value.ToString();
                numPrice.Value = Convert.ToDecimal(row.Cells["Price"].Value);
                cmbLocation.Text = row.Cells["Location"].Value.ToString();
                cmbCategory.Text = row.Cells["Category"].Value.ToString();
                dtpDueDate.Value = Convert.ToDateTime(row.Cells["DueDate"].Value);
                chkRecurring.Checked = Convert.ToBoolean(row.Cells["Recurring"].Value);
                cmbRecurringType.Text = row.Cells["RecurringType"].Value?.ToString();
                cmbStatus.Text = row.Cells["Status"].Value.ToString();
                dtpPaidDate.Value = row.Cells["PaidDate"].Value != DBNull.Value ? Convert.ToDateTime(row.Cells["PaidDate"].Value) : DateTime.Now;
                lblFilePath.Text = row.Cells["ProofOfPaymentPath"].Value?.ToString();
            }
        }
        private void dgvPayments_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvPayments1.Rows[e.RowIndex];
                txtPaymentName.Text = row.Cells["PaymentName"].Value.ToString();
                numPrice.Value = Convert.ToDecimal(row.Cells["Price"].Value);
                cmbLocation.Text = row.Cells["Location"].Value.ToString();
                cmbCategory.Text = row.Cells["Category"].Value.ToString();
                dtpDueDate.Value = Convert.ToDateTime(row.Cells["DueDate"].Value);
                chkRecurring.Checked = Convert.ToBoolean(row.Cells["Recurring"].Value);
                cmbRecurringType.Text = row.Cells["RecurringType"].Value?.ToString();
                cmbStatus.Text = row.Cells["Status"].Value.ToString();
                dtpPaidDate.Value = row.Cells["PaidDate"].Value != DBNull.Value ? Convert.ToDateTime(row.Cells["PaidDate"].Value) : DateTime.Now;
                lblFilePath.Text = row.Cells["ProofOfPaymentPath"].Value?.ToString();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtPaymentName.Clear();
            numPrice.Value = 0;
            cmbLocation.SelectedIndex = -1;
            cmbCategory.SelectedIndex = -1;
            dtpDueDate.Value = DateTime.Now;
            chkRecurring.Checked = false;
            cmbRecurringType.SelectedIndex = -1;
            cmbStatus.SelectedIndex = -1;
            dtpPaidDate.Value = DateTime.Now;
            lblFilePath.Text = "";
        }

        private void btnMarkAsPaid_Click(object sender, EventArgs e)
        {
            if (dgvPayments1.SelectedRows.Count > 0)
            {
                int selectedId = Convert.ToInt32(dgvPayments1.SelectedRows[0].Cells["ID"].Value);
                DateTime selectedDueDate = Convert.ToDateTime(dgvPayments1.SelectedRows[0].Cells["DueDate"].Value);
                bool isRecurring = Convert.ToBoolean(dgvPayments1.SelectedRows[0].Cells["Recurring"].Value);
                string recurringType = dgvPayments1.SelectedRows[0].Cells["RecurringType"].Value?.ToString() ?? "";

                if (isRecurring)
                {
                    MarkAsPaid(selectedId, selectedDueDate, isRecurring, recurringType);
                }
                else
                {
                    MessageBox.Show("This is not a recurring payment.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void cboFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyStatusFilter();
        }
        private void ApplyStatusFilter()
        {
            if (cboFilter.SelectedItem != null)
            {
                string selectedStatus = cboFilter.SelectedItem.ToString();
                string filterExpression = "";

                switch (selectedStatus)
                {
                    case "All":
                        filterExpression = "";
                        break;
                    case "Paid":
                        filterExpression = "Status = 'Paid'";
                        break;
                    case "Pending":
                        filterExpression = "Status = 'Pending'";
                        break;
                    case "Overdue":
                        filterExpression = "Status = 'Overdue'";
                        break;
                    case "Postponed":
                        filterExpression = "Status = 'Postponed'"; // Assuming you have a 'Postponed' status
                        break;
                }

                // Apply the filter to the single DataGridView (dgvPayments1)
                if (dgvPayments1.DataSource is DataTable dt)
                {
                    dt.DefaultView.RowFilter = filterExpression;
                }
            }
        }


        private void cboFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyStatusFilter();
        }

        private void cboFilter_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            ApplyStatusFilter();
        }
    }
}
