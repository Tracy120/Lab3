using Lab3.Models;

namespace Lab3.Views;

public sealed partial class AdminForm
{
    private System.ComponentModel.IContainer? components;
    private TabControl tabs = null!;
    private TabPage toursPage = null!, bookingsPage = null!, usersPage = null!, reportsPage = null!;
    private ListBox tours = null!, bookings = null!, users = null!, report = null!;
    private ComboBox destinations = null!, methods = null!;
    private TextBox tourName = null!, description = null!, destinationName = null!, country = null!;
    private NumericUpDown price = null!, capacity = null!, payment = null!;
    private DateTimePicker start = null!, end = null!;
    private Button addDestinationButton = null!, addTourButton = null!, deleteTourButton = null!;
    private Button recordPaymentButton = null!, cancelBookingButton = null!;
    private Button activateButton = null!, deactivateButton = null!, toggleRoleButton = null!;
    private Button refreshReportButton = null!, exportButton = null!;
    private Label toursTitle = null!, existingToursTitle = null!, paymentTitle = null!, paymentInfo = null!, usersTitle = null!, reportsTitle = null!;
    private Label tourNameLabel = null!, descriptionLabel = null!, destinationLabel = null!, newDestinationLabel = null!, countryLabel = null!, startLabel = null!, endLabel = null!, priceLabel = null!, capacityLabel = null!;

    private void InitializeComponent()
    {
        tabs = new TabControl();
        toursPage = new TabPage();
        toursTitle = new Label();
        existingToursTitle = new Label();
        tours = new ListBox();
        tourName = new TextBox();
        description = new TextBox();
        destinations = new ComboBox();
        destinationName = new TextBox();
        country = new TextBox();
        start = new DateTimePicker();
        end = new DateTimePicker();
        price = new NumericUpDown();
        capacity = new NumericUpDown();
        addDestinationButton = new Button();
        addTourButton = new Button();
        deleteTourButton = new Button();
        bookingsPage = new TabPage();
        bookings = new ListBox();
        paymentTitle = new Label();
        paymentInfo = new Label();
        payment = new NumericUpDown();
        methods = new ComboBox();
        recordPaymentButton = new Button();
        cancelBookingButton = new Button();
        usersPage = new TabPage();
        usersTitle = new Label();
        users = new ListBox();
        activateButton = new Button();
        deactivateButton = new Button();
        toggleRoleButton = new Button();
        reportsPage = new TabPage();
        reportsTitle = new Label();
        tourNameLabel = new Label(); descriptionLabel = new Label(); destinationLabel = new Label(); newDestinationLabel = new Label(); countryLabel = new Label(); startLabel = new Label(); endLabel = new Label(); priceLabel = new Label(); capacityLabel = new Label();
        report = new ListBox();
        refreshReportButton = new Button();
        exportButton = new Button();
        tabs.SuspendLayout();
        toursPage.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)price).BeginInit();
        ((System.ComponentModel.ISupportInitialize)capacity).BeginInit();
        bookingsPage.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)payment).BeginInit();
        usersPage.SuspendLayout();
        reportsPage.SuspendLayout();
        SuspendLayout();
        // 
        // tabs
        // 
        tabs.Controls.Add(toursPage);
        tabs.Controls.Add(bookingsPage);
        tabs.Controls.Add(usersPage);
        tabs.Controls.Add(reportsPage);
        tabs.Dock = DockStyle.Fill;
        tabs.Location = new Point(0, 0);
        tabs.Name = "tabs";
        tabs.Padding = new Point(14, 8);
        tabs.SelectedIndex = 0;
        tabs.Size = new Size(1100, 700);
        tabs.TabIndex = 0;
        // 
        // toursPage
        // 
        toursPage.Controls.Add(toursTitle);
        toursPage.Controls.Add(existingToursTitle);
        toursPage.Controls.Add(tourNameLabel);
        toursPage.Controls.Add(descriptionLabel);
        toursPage.Controls.Add(destinationLabel);
        toursPage.Controls.Add(newDestinationLabel);
        toursPage.Controls.Add(countryLabel);
        toursPage.Controls.Add(startLabel);
        toursPage.Controls.Add(endLabel);
        toursPage.Controls.Add(priceLabel);
        toursPage.Controls.Add(capacityLabel);
        toursPage.Controls.Add(tours);
        toursPage.Controls.Add(tourName);
        toursPage.Controls.Add(description);
        toursPage.Controls.Add(destinations);
        toursPage.Controls.Add(destinationName);
        toursPage.Controls.Add(country);
        toursPage.Controls.Add(start);
        toursPage.Controls.Add(end);
        toursPage.Controls.Add(price);
        toursPage.Controls.Add(capacity);
        toursPage.Controls.Add(addDestinationButton);
        toursPage.Controls.Add(addTourButton);
        toursPage.Controls.Add(deleteTourButton);
        toursPage.Location = new Point(4, 39);
        toursPage.Name = "toursPage";
        toursPage.Padding = new Padding(16);
        toursPage.Size = new Size(1092, 657);
        toursPage.TabIndex = 0;
        toursPage.Text = "Tours and destinations";
        // 
        // toursTitle
        // 
        toursTitle.AutoSize = true;
        toursTitle.Font = new Font("Segoe UI", 17F, FontStyle.Bold);
        toursTitle.Location = new Point(18, 18);
        toursTitle.Name = "toursTitle";
        toursTitle.Size = new Size(270, 40);
        toursTitle.TabIndex = 0;
        toursTitle.Text = "Tour management";
        // 
        // existingToursTitle
        // 
        existingToursTitle.AutoSize = true;
        existingToursTitle.Font = new Font("Segoe UI", 17F, FontStyle.Bold);
        existingToursTitle.Location = new Point(565, 18);
        existingToursTitle.Name = "existingToursTitle";
        existingToursTitle.Size = new Size(204, 40);
        existingToursTitle.TabIndex = 1;
        existingToursTitle.Text = "Existing tours";
        tourNameLabel.AutoSize = true; tourNameLabel.Location = new Point(20, 68); tourNameLabel.Name = "tourNameLabel"; tourNameLabel.Text = "Tour name";
        descriptionLabel.AutoSize = true; descriptionLabel.Location = new Point(20, 108); descriptionLabel.Name = "descriptionLabel"; descriptionLabel.Text = "Description";
        destinationLabel.AutoSize = true; destinationLabel.Location = new Point(20, 148); destinationLabel.Name = "destinationLabel"; destinationLabel.Text = "Destination";
        newDestinationLabel.AutoSize = true; newDestinationLabel.Location = new Point(20, 188); newDestinationLabel.Name = "newDestinationLabel"; newDestinationLabel.Text = "New destination";
        countryLabel.AutoSize = true; countryLabel.Location = new Point(20, 228); countryLabel.Name = "countryLabel"; countryLabel.Text = "Country";
        startLabel.AutoSize = true; startLabel.Location = new Point(20, 268); startLabel.Name = "startLabel"; startLabel.Text = "Start date";
        endLabel.AutoSize = true; endLabel.Location = new Point(20, 308); endLabel.Name = "endLabel"; endLabel.Text = "End date";
        priceLabel.AutoSize = true; priceLabel.Location = new Point(20, 348); priceLabel.Name = "priceLabel"; priceLabel.Text = "Price";
        capacityLabel.AutoSize = true; capacityLabel.Location = new Point(300, 348); capacityLabel.Name = "capacityLabel"; capacityLabel.Text = "Capacity";
        // 
        // tours
        // 
        tours.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        tours.Location = new Point(565, 58);
        tours.Name = "tours";
        tours.Size = new Size(1380, 1104);
        tours.TabIndex = 2;
        // 
        // tourName
        // 
        tourName.Location = new Point(145, 62);
        tourName.Name = "tourName";
        tourName.Size = new Size(360, 27);
        tourName.TabIndex = 4;
        // 
        // description
        // 
        description.Location = new Point(145, 102);
        description.Name = "description";
        description.Size = new Size(360, 27);
        description.TabIndex = 6;
        // 
        // destinations
        // 
        destinations.Location = new Point(145, 142);
        destinations.Name = "destinations";
        destinations.Size = new Size(360, 28);
        destinations.TabIndex = 8;
        // 
        // destinationName
        // 
        destinationName.Location = new Point(145, 182);
        destinationName.Name = "destinationName";
        destinationName.Size = new Size(360, 27);
        destinationName.TabIndex = 10;
        // 
        // country
        // 
        country.Location = new Point(145, 222);
        country.Name = "country";
        country.Size = new Size(360, 27);
        country.TabIndex = 12;
        // 
        // start
        // 
        start.Location = new Point(145, 262);
        start.Name = "start";
        start.Size = new Size(200, 27);
        start.TabIndex = 14;
        // 
        // end
        // 
        end.Location = new Point(145, 302);
        end.Name = "end";
        end.Size = new Size(200, 27);
        end.TabIndex = 16;
        // 
        // price
        // 
        price.DecimalPlaces = 2;
        price.Location = new Point(145, 342);
        price.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
        price.Name = "price";
        price.Size = new Size(140, 27);
        price.TabIndex = 18;
        // 
        // capacity
        // 
        capacity.Location = new Point(365, 342);
        capacity.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
        capacity.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        capacity.Name = "capacity";
        capacity.Size = new Size(140, 27);
        capacity.TabIndex = 20;
        capacity.Value = new decimal(new int[] { 20, 0, 0, 0 });
        // 
        // addDestinationButton
        // 
        addDestinationButton.Location = new Point(20, 400);
        addDestinationButton.Name = "addDestinationButton";
        addDestinationButton.Size = new Size(150, 38);
        addDestinationButton.TabIndex = 21;
        addDestinationButton.Text = "Add destination";
        addDestinationButton.Click += AddDestination;
        // 
        // addTourButton
        // 
        addTourButton.Location = new Point(180, 400);
        addTourButton.Name = "addTourButton";
        addTourButton.Size = new Size(120, 38);
        addTourButton.TabIndex = 22;
        addTourButton.Text = "Add tour";
        addTourButton.Click += AddTour;
        // 
        // deleteTourButton
        // 
        deleteTourButton.Location = new Point(310, 400);
        deleteTourButton.Name = "deleteTourButton";
        deleteTourButton.Size = new Size(150, 38);
        deleteTourButton.TabIndex = 23;
        deleteTourButton.Text = "Delete selected";
        deleteTourButton.Click += DeleteTour;
        // 
        // bookingsPage
        // 
        bookingsPage.Controls.Add(bookings);
        bookingsPage.Controls.Add(paymentTitle);
        bookingsPage.Controls.Add(paymentInfo);
        bookingsPage.Controls.Add(payment);
        bookingsPage.Controls.Add(methods);
        bookingsPage.Controls.Add(recordPaymentButton);
        bookingsPage.Controls.Add(cancelBookingButton);
        bookingsPage.Location = new Point(4, 39);
        bookingsPage.Name = "bookingsPage";
        bookingsPage.Padding = new Padding(16);
        bookingsPage.Size = new Size(1092, 657);
        bookingsPage.TabIndex = 1;
        bookingsPage.Text = "Bookings and payments";
        // 
        // bookings
        // 
        bookings.Dock = DockStyle.Fill;
        bookings.Location = new Point(16, 16);
        bookings.Name = "bookings";
        bookings.Size = new Size(1060, 625);
        bookings.TabIndex = 0;
        // 
        // paymentTitle
        // 
        paymentTitle.AutoSize = true;
        paymentTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        paymentTitle.Location = new Point(760, 30);
        paymentTitle.Name = "paymentTitle";
        paymentTitle.Size = new Size(214, 23);
        paymentTitle.TabIndex = 1;
        paymentTitle.Text = "Record payment received";
        // 
        // paymentInfo
        // 
        paymentInfo.AutoSize = true;
        paymentInfo.Location = new Point(760, 60);
        paymentInfo.MaximumSize = new Size(220, 0);
        paymentInfo.Name = "paymentInfo";
        paymentInfo.Size = new Size(204, 40);
        paymentInfo.TabIndex = 2;
        paymentInfo.Text = "Pending becomes Confirmed when fully paid.";
        // 
        // payment
        // 
        payment.DecimalPlaces = 2;
        payment.Location = new Point(760, 115);
        payment.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
        payment.Name = "payment";
        payment.Size = new Size(180, 27);
        payment.TabIndex = 3;
        // 
        // methods
        // 
        methods.Items.AddRange(new object[] { PaymentMethod.Cash, PaymentMethod.MobileMoney, PaymentMethod.Card });
        methods.Location = new Point(760, 155);
        methods.Name = "methods";
        methods.Size = new Size(180, 28);
        methods.TabIndex = 4;
        // 
        // recordPaymentButton
        // 
        recordPaymentButton.Location = new Point(760, 205);
        recordPaymentButton.Name = "recordPaymentButton";
        recordPaymentButton.Size = new Size(190, 38);
        recordPaymentButton.TabIndex = 5;
        recordPaymentButton.Text = "Record received payment";
        recordPaymentButton.Click += AddPayment;
        // 
        // cancelBookingButton
        // 
        cancelBookingButton.Location = new Point(760, 255);
        cancelBookingButton.Name = "cancelBookingButton";
        cancelBookingButton.Size = new Size(190, 38);
        cancelBookingButton.TabIndex = 6;
        cancelBookingButton.Text = "Cancel booking";
        cancelBookingButton.Click += CancelBooking;
        // 
        // usersPage
        // 
        usersPage.Controls.Add(usersTitle);
        usersPage.Controls.Add(users);
        usersPage.Controls.Add(activateButton);
        usersPage.Controls.Add(deactivateButton);
        usersPage.Controls.Add(toggleRoleButton);
        usersPage.Location = new Point(4, 39);
        usersPage.Name = "usersPage";
        usersPage.Padding = new Padding(16);
        usersPage.Size = new Size(1092, 657);
        usersPage.TabIndex = 2;
        usersPage.Text = "User administration";
        // 
        // usersTitle
        // 
        usersTitle.AutoSize = true;
        usersTitle.Font = new Font("Segoe UI", 17F, FontStyle.Bold);
        usersTitle.Location = new Point(18, 18);
        usersTitle.Name = "usersTitle";
        usersTitle.Size = new Size(243, 40);
        usersTitle.TabIndex = 0;
        usersTitle.Text = "Registered users";
        // 
        // users
        // 
        users.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        users.Location = new Point(18, 58);
        users.Name = "users";
        users.Size = new Size(1660, 1104);
        users.TabIndex = 1;
        // 
        // activateButton
        // 
        activateButton.Location = new Point(810, 100);
        activateButton.Name = "activateButton";
        activateButton.Size = new Size(150, 38);
        activateButton.TabIndex = 2;
        activateButton.Text = "Activate";
        activateButton.Click += ActivateUser;
        // 
        // deactivateButton
        // 
        deactivateButton.Location = new Point(810, 150);
        deactivateButton.Name = "deactivateButton";
        deactivateButton.Size = new Size(150, 38);
        deactivateButton.TabIndex = 3;
        deactivateButton.Text = "Deactivate";
        deactivateButton.Click += DeactivateUser;
        // 
        // toggleRoleButton
        // 
        toggleRoleButton.Location = new Point(810, 200);
        toggleRoleButton.Name = "toggleRoleButton";
        toggleRoleButton.Size = new Size(150, 38);
        toggleRoleButton.TabIndex = 4;
        toggleRoleButton.Text = "Toggle role";
        toggleRoleButton.Click += ToggleRole;
        // 
        // reportsPage
        // 
        reportsPage.Controls.Add(reportsTitle);
        reportsPage.Controls.Add(report);
        reportsPage.Controls.Add(refreshReportButton);
        reportsPage.Controls.Add(exportButton);
        reportsPage.Location = new Point(4, 39);
        reportsPage.Name = "reportsPage";
        reportsPage.Padding = new Padding(16);
        reportsPage.Size = new Size(1092, 657);
        reportsPage.TabIndex = 3;
        reportsPage.Text = "Reports";
        // 
        // reportsTitle
        // 
        reportsTitle.AutoSize = true;
        reportsTitle.Font = new Font("Segoe UI", 17F, FontStyle.Bold);
        reportsTitle.Location = new Point(18, 18);
        reportsTitle.Name = "reportsTitle";
        reportsTitle.Size = new Size(413, 40);
        reportsTitle.TabIndex = 0;
        reportsTitle.Text = "Tour occupancy and revenue";
        // 
        // report
        // 
        report.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        report.Location = new Point(18, 58);
        report.Name = "report";
        report.Size = new Size(1660, 1104);
        report.TabIndex = 1;
        // 
        // refreshReportButton
        // 
        refreshReportButton.Location = new Point(810, 100);
        refreshReportButton.Name = "refreshReportButton";
        refreshReportButton.Size = new Size(150, 38);
        refreshReportButton.TabIndex = 2;
        refreshReportButton.Text = "Refresh report";
        refreshReportButton.Click += RefreshReport;
        // 
        // exportButton
        // 
        exportButton.Location = new Point(810, 150);
        exportButton.Name = "exportButton";
        exportButton.Size = new Size(150, 38);
        exportButton.TabIndex = 3;
        exportButton.Text = "Export CSV";
        exportButton.Click += Export;
        // 
        // AdminForm
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.White;
        ClientSize = new Size(1100, 700);
        Controls.Add(tabs);
        MinimumSize = new Size(900, 600);
        Name = "AdminForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Administrator dashboard";
        tabs.ResumeLayout(false);
        toursPage.ResumeLayout(false);
        toursPage.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)price).EndInit();
        ((System.ComponentModel.ISupportInitialize)capacity).EndInit();
        bookingsPage.ResumeLayout(false);
        bookingsPage.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)payment).EndInit();
        usersPage.ResumeLayout(false);
        usersPage.PerformLayout();
        reportsPage.ResumeLayout(false);
        reportsPage.PerformLayout();
        ResumeLayout(false);
    }

    protected override void Dispose(bool disposing) { if (disposing) components?.Dispose(); base.Dispose(disposing); }
}
