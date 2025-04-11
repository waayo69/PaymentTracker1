namespace PaymentTracker1
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.llblViewProof = new System.Windows.Forms.LinkLabel();
            this.btnMarkAsPaid = new System.Windows.Forms.Button();
            this.cmbFilterLocation = new System.Windows.Forms.ComboBox();
            this.lblFilterLocation = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.dgvPayments1 = new System.Windows.Forms.DataGridView();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblFilePath = new System.Windows.Forms.Label();
            this.btnUpload = new System.Windows.Forms.Button();
            this.dtpPaidDate = new System.Windows.Forms.DateTimePicker();
            this.lblPaidDate = new System.Windows.Forms.Label();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cmbRecurringType = new System.Windows.Forms.ComboBox();
            this.lblRecurringType = new System.Windows.Forms.Label();
            this.chkRecurring = new System.Windows.Forms.CheckBox();
            this.dtpDueDate = new System.Windows.Forms.DateTimePicker();
            this.lblDueDate = new System.Windows.Forms.Label();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.lblCategory = new System.Windows.Forms.Label();
            this.cmbLocation = new System.Windows.Forms.ComboBox();
            this.lblLocation = new System.Windows.Forms.Label();
            this.numPrice = new System.Windows.Forms.NumericUpDown();
            this.lblPrice = new System.Windows.Forms.Label();
            this.txtPaymentName = new System.Windows.Forms.TextBox();
            this.lblPaymentName = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPaid = new System.Windows.Forms.TabPage();
            this.dgvPaidPayments = new System.Windows.Forms.DataGridView();
            this.tabUnpaid = new System.Windows.Forms.TabPage();
            this.dgvUnpaidPayments = new System.Windows.Forms.DataGridView();
            this.cboFilter = new System.Windows.Forms.ComboBox();
            this.lblFilter = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPayments1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPrice)).BeginInit();
            this.tabPaid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPaidPayments)).BeginInit();
            this.tabUnpaid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUnpaidPayments)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPaid);
            this.tabControl1.Controls.Add(this.tabUnpaid);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1557, 554);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.cboFilter);
            this.tabPage1.Controls.Add(this.lblFilter);
            this.tabPage1.Controls.Add(this.llblViewProof);
            this.tabPage1.Controls.Add(this.btnMarkAsPaid);
            this.tabPage1.Controls.Add(this.cmbFilterLocation);
            this.tabPage1.Controls.Add(this.lblFilterLocation);
            this.tabPage1.Controls.Add(this.btnClear);
            this.tabPage1.Controls.Add(this.dgvPayments1);
            this.tabPage1.Controls.Add(this.btnDelete);
            this.tabPage1.Controls.Add(this.btnUpdate);
            this.tabPage1.Controls.Add(this.btnSave);
            this.tabPage1.Controls.Add(this.lblFilePath);
            this.tabPage1.Controls.Add(this.btnUpload);
            this.tabPage1.Controls.Add(this.dtpPaidDate);
            this.tabPage1.Controls.Add(this.lblPaidDate);
            this.tabPage1.Controls.Add(this.cmbStatus);
            this.tabPage1.Controls.Add(this.lblStatus);
            this.tabPage1.Controls.Add(this.cmbRecurringType);
            this.tabPage1.Controls.Add(this.lblRecurringType);
            this.tabPage1.Controls.Add(this.chkRecurring);
            this.tabPage1.Controls.Add(this.dtpDueDate);
            this.tabPage1.Controls.Add(this.lblDueDate);
            this.tabPage1.Controls.Add(this.cmbCategory);
            this.tabPage1.Controls.Add(this.lblCategory);
            this.tabPage1.Controls.Add(this.cmbLocation);
            this.tabPage1.Controls.Add(this.lblLocation);
            this.tabPage1.Controls.Add(this.numPrice);
            this.tabPage1.Controls.Add(this.lblPrice);
            this.tabPage1.Controls.Add(this.txtPaymentName);
            this.tabPage1.Controls.Add(this.lblPaymentName);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage1.Size = new System.Drawing.Size(1549, 525);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Payment Entry";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // llblViewProof
            // 
            this.llblViewProof.AutoSize = true;
            this.llblViewProof.Location = new System.Drawing.Point(1106, 241);
            this.llblViewProof.Name = "llblViewProof";
            this.llblViewProof.Size = new System.Drawing.Size(71, 16);
            this.llblViewProof.TabIndex = 27;
            this.llblViewProof.TabStop = true;
            this.llblViewProof.Text = "View Proof";
            this.llblViewProof.Visible = false;
            this.llblViewProof.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llblViewProof_LinkClicked);
            // 
            // btnMarkAsPaid
            // 
            this.btnMarkAsPaid.Location = new System.Drawing.Point(1313, 356);
            this.btnMarkAsPaid.Margin = new System.Windows.Forms.Padding(4);
            this.btnMarkAsPaid.Name = "btnMarkAsPaid";
            this.btnMarkAsPaid.Size = new System.Drawing.Size(148, 28);
            this.btnMarkAsPaid.TabIndex = 26;
            this.btnMarkAsPaid.Text = "Mark as Paid";
            this.btnMarkAsPaid.UseVisualStyleBackColor = true;
            this.btnMarkAsPaid.Click += new System.EventHandler(this.btnMarkAsPaid_Click);
            // 
            // cmbFilterLocation
            // 
            this.cmbFilterLocation.FormattingEnabled = true;
            this.cmbFilterLocation.Items.AddRange(new object[] {
            "MANDAUE",
            "TALISAY",
            "DUMANJUG",
            "MINGLANILLA",
            "TOLEDO"});
            this.cmbFilterLocation.Location = new System.Drawing.Point(807, 395);
            this.cmbFilterLocation.Margin = new System.Windows.Forms.Padding(4);
            this.cmbFilterLocation.Name = "cmbFilterLocation";
            this.cmbFilterLocation.Size = new System.Drawing.Size(160, 24);
            this.cmbFilterLocation.TabIndex = 25;
            this.cmbFilterLocation.SelectedIndexChanged += new System.EventHandler(this.cmbFilterLocation_SelectedIndexChanged);
            // 
            // lblFilterLocation
            // 
            this.lblFilterLocation.AutoSize = true;
            this.lblFilterLocation.Location = new System.Drawing.Point(803, 375);
            this.lblFilterLocation.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFilterLocation.Name = "lblFilterLocation";
            this.lblFilterLocation.Size = new System.Drawing.Size(111, 16);
            this.lblFilterLocation.TabIndex = 24;
            this.lblFilterLocation.Text = "Filter by Location:";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(1109, 437);
            this.btnClear.Margin = new System.Windows.Forms.Padding(4);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(161, 28);
            this.btnClear.TabIndex = 23;
            this.btnClear.Text = "Clear forms";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // dgvPayments1
            // 
            this.dgvPayments1.AllowUserToAddRows = false;
            this.dgvPayments1.AllowUserToDeleteRows = false;
            this.dgvPayments1.AllowUserToResizeRows = false;
            this.dgvPayments1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvPayments1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.dgvPayments1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPayments1.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgvPayments1.Location = new System.Drawing.Point(4, 4);
            this.dgvPayments1.Margin = new System.Windows.Forms.Padding(4);
            this.dgvPayments1.Name = "dgvPayments1";
            this.dgvPayments1.ReadOnly = true;
            this.dgvPayments1.RowHeadersWidth = 51;
            this.dgvPayments1.Size = new System.Drawing.Size(1541, 185);
            this.dgvPayments1.TabIndex = 22;
            this.dgvPayments1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPayments1_CellClick);
            this.dgvPayments1.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvPayments1_CellFormatting);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(1109, 400);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(4);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(161, 28);
            this.btnDelete.TabIndex = 21;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(1109, 364);
            this.btnUpdate.Margin = new System.Windows.Forms.Padding(4);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(161, 28);
            this.btnUpdate.TabIndex = 20;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(1109, 329);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(161, 28);
            this.btnSave.TabIndex = 19;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblFilePath
            // 
            this.lblFilePath.AutoSize = true;
            this.lblFilePath.Location = new System.Drawing.Point(1105, 266);
            this.lblFilePath.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFilePath.Name = "lblFilePath";
            this.lblFilePath.Size = new System.Drawing.Size(180, 16);
            this.lblFilePath.TabIndex = 18;
            this.lblFilePath.Text = "(Shows the selected file path)";
            // 
            // btnUpload
            // 
            this.btnUpload.Location = new System.Drawing.Point(1109, 292);
            this.btnUpload.Margin = new System.Windows.Forms.Padding(4);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(161, 28);
            this.btnUpload.TabIndex = 17;
            this.btnUpload.Text = "Upload Proof";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // dtpPaidDate
            // 
            this.dtpPaidDate.Enabled = false;
            this.dtpPaidDate.Location = new System.Drawing.Point(807, 321);
            this.dtpPaidDate.Margin = new System.Windows.Forms.Padding(4);
            this.dtpPaidDate.Name = "dtpPaidDate";
            this.dtpPaidDate.Size = new System.Drawing.Size(265, 22);
            this.dtpPaidDate.TabIndex = 16;
            this.dtpPaidDate.Visible = false;
            // 
            // lblPaidDate
            // 
            this.lblPaidDate.AutoSize = true;
            this.lblPaidDate.Location = new System.Drawing.Point(803, 302);
            this.lblPaidDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPaidDate.Name = "lblPaidDate";
            this.lblPaidDate.Size = new System.Drawing.Size(171, 16);
            this.lblPaidDate.TabIndex = 15;
            this.lblPaidDate.Text = "Paid Date: Disabled initially";
            // 
            // cmbStatus
            // 
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Items.AddRange(new object[] {
            "Pending",
            "Paid",
            "Overdue",
            "Postponed"});
            this.cmbStatus.Location = new System.Drawing.Point(807, 261);
            this.cmbStatus.Margin = new System.Windows.Forms.Padding(4);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(160, 24);
            this.cmbStatus.TabIndex = 14;
            this.cmbStatus.SelectedIndexChanged += new System.EventHandler(this.cmbStatus_SelectedIndexChanged);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(803, 241);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(47, 16);
            this.lblStatus.TabIndex = 13;
            this.lblStatus.Text = "Status:";
            // 
            // cmbRecurringType
            // 
            this.cmbRecurringType.FormattingEnabled = true;
            this.cmbRecurringType.Items.AddRange(new object[] {
            "Weekly",
            "Monthly",
            "Quarterly",
            "Annually"});
            this.cmbRecurringType.Location = new System.Drawing.Point(459, 437);
            this.cmbRecurringType.Margin = new System.Windows.Forms.Padding(4);
            this.cmbRecurringType.Name = "cmbRecurringType";
            this.cmbRecurringType.Size = new System.Drawing.Size(160, 24);
            this.cmbRecurringType.TabIndex = 12;
            this.cmbRecurringType.Visible = false;
            // 
            // lblRecurringType
            // 
            this.lblRecurringType.AutoSize = true;
            this.lblRecurringType.Location = new System.Drawing.Point(459, 414);
            this.lblRecurringType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRecurringType.Name = "lblRecurringType";
            this.lblRecurringType.Size = new System.Drawing.Size(103, 16);
            this.lblRecurringType.TabIndex = 11;
            this.lblRecurringType.Text = "Recurring Type:\n";
            this.lblRecurringType.Visible = false;
            // 
            // chkRecurring
            // 
            this.chkRecurring.AutoSize = true;
            this.chkRecurring.Location = new System.Drawing.Point(459, 388);
            this.chkRecurring.Margin = new System.Windows.Forms.Padding(4);
            this.chkRecurring.Name = "chkRecurring";
            this.chkRecurring.Size = new System.Drawing.Size(94, 20);
            this.chkRecurring.TabIndex = 10;
            this.chkRecurring.Text = "\tRecurring?";
            this.chkRecurring.UseVisualStyleBackColor = true;
            this.chkRecurring.CheckedChanged += new System.EventHandler(this.chkRecurring_CheckedChanged);
            // 
            // dtpDueDate
            // 
            this.dtpDueDate.Location = new System.Drawing.Point(459, 320);
            this.dtpDueDate.Margin = new System.Windows.Forms.Padding(4);
            this.dtpDueDate.Name = "dtpDueDate";
            this.dtpDueDate.Size = new System.Drawing.Size(265, 22);
            this.dtpDueDate.TabIndex = 9;
            // 
            // lblDueDate
            // 
            this.lblDueDate.AutoSize = true;
            this.lblDueDate.Location = new System.Drawing.Point(455, 302);
            this.lblDueDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDueDate.Name = "lblDueDate";
            this.lblDueDate.Size = new System.Drawing.Size(67, 16);
            this.lblDueDate.TabIndex = 8;
            this.lblDueDate.Text = "Due Date:";
            // 
            // cmbCategory
            // 
            this.cmbCategory.FormattingEnabled = true;
            this.cmbCategory.Items.AddRange(new object[] {
            "MONTHLY PAYMENTS",
            "EXPENSES",
            "AMORTIZATION",
            "Loans",
            "SPP",
            "Credit Card"});
            this.cmbCategory.Location = new System.Drawing.Point(459, 261);
            this.cmbCategory.Margin = new System.Windows.Forms.Padding(4);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(160, 24);
            this.cmbCategory.TabIndex = 7;
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Location = new System.Drawing.Point(455, 241);
            this.lblCategory.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(65, 16);
            this.lblCategory.TabIndex = 6;
            this.lblCategory.Text = "Category:";
            // 
            // cmbLocation
            // 
            this.cmbLocation.FormattingEnabled = true;
            this.cmbLocation.Items.AddRange(new object[] {
            "MANDAUE",
            "TALISAY",
            "DUMANJUG",
            "MINGLANILLA",
            "TOLEDO"});
            this.cmbLocation.Location = new System.Drawing.Point(136, 388);
            this.cmbLocation.Margin = new System.Windows.Forms.Padding(4);
            this.cmbLocation.Name = "cmbLocation";
            this.cmbLocation.Size = new System.Drawing.Size(160, 24);
            this.cmbLocation.TabIndex = 5;
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.Location = new System.Drawing.Point(132, 368);
            this.lblLocation.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(61, 16);
            this.lblLocation.TabIndex = 4;
            this.lblLocation.Text = "Location:";
            // 
            // numPrice
            // 
            this.numPrice.DecimalPlaces = 2;
            this.numPrice.Location = new System.Drawing.Point(136, 321);
            this.numPrice.Margin = new System.Windows.Forms.Padding(4);
            this.numPrice.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.numPrice.Name = "numPrice";
            this.numPrice.Size = new System.Drawing.Size(160, 22);
            this.numPrice.TabIndex = 3;
            // 
            // lblPrice
            // 
            this.lblPrice.AutoSize = true;
            this.lblPrice.Location = new System.Drawing.Point(132, 302);
            this.lblPrice.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(38, 16);
            this.lblPrice.TabIndex = 2;
            this.lblPrice.Text = "Price";
            // 
            // txtPaymentName
            // 
            this.txtPaymentName.Location = new System.Drawing.Point(136, 261);
            this.txtPaymentName.Margin = new System.Windows.Forms.Padding(4);
            this.txtPaymentName.Name = "txtPaymentName";
            this.txtPaymentName.Size = new System.Drawing.Size(132, 22);
            this.txtPaymentName.TabIndex = 1;
            // 
            // lblPaymentName
            // 
            this.lblPaymentName.AutoSize = true;
            this.lblPaymentName.Location = new System.Drawing.Point(132, 241);
            this.lblPaymentName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPaymentName.Name = "lblPaymentName";
            this.lblPaymentName.Size = new System.Drawing.Size(103, 16);
            this.lblPaymentName.TabIndex = 0;
            this.lblPaymentName.Text = "Payment Name:";
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage2.Size = new System.Drawing.Size(1549, 525);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Payment List";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPaid
            // 
            this.tabPaid.Controls.Add(this.dgvPaidPayments);
            this.tabPaid.Location = new System.Drawing.Point(4, 25);
            this.tabPaid.Margin = new System.Windows.Forms.Padding(4);
            this.tabPaid.Name = "tabPaid";
            this.tabPaid.Size = new System.Drawing.Size(1549, 525);
            this.tabPaid.TabIndex = 2;
            this.tabPaid.Text = "Paid Payments";
            this.tabPaid.UseVisualStyleBackColor = true;
            // 
            // dgvPaidPayments
            // 
            this.dgvPaidPayments.AllowUserToAddRows = false;
            this.dgvPaidPayments.AllowUserToDeleteRows = false;
            this.dgvPaidPayments.AllowUserToResizeRows = false;
            this.dgvPaidPayments.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvPaidPayments.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.dgvPaidPayments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPaidPayments.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgvPaidPayments.Location = new System.Drawing.Point(0, 0);
            this.dgvPaidPayments.Margin = new System.Windows.Forms.Padding(4);
            this.dgvPaidPayments.Name = "dgvPaidPayments";
            this.dgvPaidPayments.ReadOnly = true;
            this.dgvPaidPayments.RowHeadersWidth = 51;
            this.dgvPaidPayments.Size = new System.Drawing.Size(1549, 185);
            this.dgvPaidPayments.TabIndex = 23;
            // 
            // tabUnpaid
            // 
            this.tabUnpaid.Controls.Add(this.dgvUnpaidPayments);
            this.tabUnpaid.Location = new System.Drawing.Point(4, 25);
            this.tabUnpaid.Margin = new System.Windows.Forms.Padding(4);
            this.tabUnpaid.Name = "tabUnpaid";
            this.tabUnpaid.Size = new System.Drawing.Size(1549, 525);
            this.tabUnpaid.TabIndex = 3;
            this.tabUnpaid.Text = "Unpaid Payments";
            this.tabUnpaid.UseVisualStyleBackColor = true;
            // 
            // dgvUnpaidPayments
            // 
            this.dgvUnpaidPayments.AllowUserToAddRows = false;
            this.dgvUnpaidPayments.AllowUserToDeleteRows = false;
            this.dgvUnpaidPayments.AllowUserToResizeRows = false;
            this.dgvUnpaidPayments.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvUnpaidPayments.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.dgvUnpaidPayments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUnpaidPayments.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgvUnpaidPayments.Location = new System.Drawing.Point(0, 0);
            this.dgvUnpaidPayments.Margin = new System.Windows.Forms.Padding(4);
            this.dgvUnpaidPayments.Name = "dgvUnpaidPayments";
            this.dgvUnpaidPayments.ReadOnly = true;
            this.dgvUnpaidPayments.RowHeadersWidth = 51;
            this.dgvUnpaidPayments.Size = new System.Drawing.Size(1549, 185);
            this.dgvUnpaidPayments.TabIndex = 24;
            // 
            // cboFilter
            // 
            this.cboFilter.FormattingEnabled = true;
            this.cboFilter.Items.AddRange(new object[] {
            "Paid",
            "Pending",
            "Overdue",
            "Postponed"});
            this.cboFilter.Location = new System.Drawing.Point(807, 457);
            this.cboFilter.Margin = new System.Windows.Forms.Padding(4);
            this.cboFilter.Name = "cboFilter";
            this.cboFilter.Size = new System.Drawing.Size(160, 24);
            this.cboFilter.TabIndex = 29;
            this.cboFilter.SelectedIndexChanged += new System.EventHandler(this.cboFilter_SelectedIndexChanged_1);
            // 
            // lblFilter
            // 
            this.lblFilter.AutoSize = true;
            this.lblFilter.Location = new System.Drawing.Point(803, 437);
            this.lblFilter.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFilter.Name = "lblFilter";
            this.lblFilter.Size = new System.Drawing.Size(97, 16);
            this.lblFilter.TabIndex = 28;
            this.lblFilter.Text = "Filter by Status:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1557, 554);
            this.Controls.Add(this.tabControl1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPayments1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPrice)).EndInit();
            this.tabPaid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPaidPayments)).EndInit();
            this.tabUnpaid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUnpaidPayments)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ComboBox cmbLocation;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.NumericUpDown numPrice;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.TextBox txtPaymentName;
        private System.Windows.Forms.Label lblPaymentName;
        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cmbRecurringType;
        private System.Windows.Forms.Label lblRecurringType;
        private System.Windows.Forms.CheckBox chkRecurring;
        private System.Windows.Forms.DateTimePicker dtpDueDate;
        private System.Windows.Forms.Label lblDueDate;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblFilePath;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.DateTimePicker dtpPaidDate;
        private System.Windows.Forms.Label lblPaidDate;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.DataGridView dgvPayments1;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.ComboBox cmbFilterLocation;
        private System.Windows.Forms.Label lblFilterLocation;
        private System.Windows.Forms.Button btnMarkAsPaid;
        private System.Windows.Forms.TabPage tabPaid;
        private System.Windows.Forms.DataGridView dgvPaidPayments;
        private System.Windows.Forms.TabPage tabUnpaid;
        private System.Windows.Forms.DataGridView dgvUnpaidPayments;
        private System.Windows.Forms.LinkLabel llblViewProof;
        private System.Windows.Forms.ComboBox cboFilter;
        private System.Windows.Forms.Label lblFilter;
    }
}

