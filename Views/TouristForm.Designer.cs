namespace Lab3.Views;

public sealed partial class TouristForm
{
    private System.ComponentModel.IContainer? components;
    private TableLayoutPanel root = null!;
    private TableLayoutPanel searchLayout = null!;
    private TableLayoutPanel bookingsLayout = null!;
    private FlowLayoutPanel actions = null!;
    private Label titleLabel = null!;
    private Label bookingsLabel = null!;
    private Label travellersLabel = null!;
    private TextBox search = null!;
    private ListBox tours = null!;
    private ListBox bookings = null!;
    private NumericUpDown travellers = null!;
    private Button searchButton = null!;
    private Button bookButton = null!;
    private Button cancelButton = null!;
    private Button refreshButton = null!;
    private Button closeButton = null!;

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        root = new TableLayoutPanel();
        searchLayout = new TableLayoutPanel();
        bookingsLayout = new TableLayoutPanel();
        actions = new FlowLayoutPanel();
        titleLabel = new Label();
        bookingsLabel = new Label();
        travellersLabel = new Label();
        search = new TextBox();
        tours = new ListBox();
        bookings = new ListBox();
        travellers = new NumericUpDown();
        searchButton = new Button();
        bookButton = new Button();
        cancelButton = new Button();
        refreshButton = new Button();
        closeButton = new Button();
        SuspendLayout();

        Text = "Tourist area - Preview user";
        StartPosition = FormStartPosition.CenterScreen;
        MinimumSize = new Size(800, 500);
        ClientSize = new Size(980, 620);
        BackColor = Color.White;
        AutoScaleMode = AutoScaleMode.Font;

        root.Dock = DockStyle.Fill;
        root.Padding = new Padding(20);
        root.ColumnCount = 2;
        root.RowCount = 3;
        root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 62F));
        root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 38F));
        root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        root.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        root.RowStyles.Add(new RowStyle(SizeType.AutoSize));

        titleLabel.Text = "Browse open tours";
        titleLabel.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
        titleLabel.ForeColor = Color.FromArgb(31, 67, 110);
        titleLabel.AutoSize = true;
        titleLabel.Margin = new Padding(0, 0, 0, 12);
        root.Controls.Add(titleLabel, 0, 0);
        root.SetColumnSpan(titleLabel, 2);

        searchLayout.Dock = DockStyle.Fill;
        searchLayout.ColumnCount = 2;
        searchLayout.RowCount = 2;
        searchLayout.Padding = new Padding(0, 4, 12, 0);
        searchLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 78F));
        searchLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 22F));
        searchLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        searchLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

        search.Dock = DockStyle.Top;
        search.PlaceholderText = "Search by tour or destination";
        searchLayout.Controls.Add(search, 0, 0);

        searchButton.Text = "Search";
        searchButton.Dock = DockStyle.Top;
        searchButton.Height = thirtySix;
        searchButton.Click += SearchClick;
        searchLayout.Controls.Add(searchButton, 1, 0);

        tours.Dock = DockStyle.Fill;
        tours.IntegralHeight = false;
        searchLayout.Controls.Add(tours, 0, 1);
        searchLayout.SetColumnSpan(tours, 2);
        root.Controls.Add(searchLayout, 0, 1);

        bookingsLayout.Dock = DockStyle.Fill;
        bookingsLayout.Padding = new Padding(12, 4, 0, 0);
        bookingsLayout.RowCount = 2;
        bookingsLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        bookingsLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        bookingsLabel.Text = "My bookings";
        bookingsLabel.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        bookingsLabel.AutoSize = true;
        bookingsLayout.Controls.Add(bookingsLabel, 0, 0);
        bookings.Dock = DockStyle.Fill;
        bookings.IntegralHeight = false;
        bookingsLayout.Controls.Add(bookings, 0, 1);
        root.Controls.Add(bookingsLayout, 1, 1);

        actions.Dock = DockStyle.Fill;
        actions.AutoSize = true;
        actions.WrapContents = false;
        actions.Padding = new Padding(0, 12, 0, 0);
        travellersLabel.Text = "Travellers";
        travellersLabel.AutoSize = true;
        travellersLabel.Padding = new Padding(0, 9, 6, 0);
        actions.Controls.Add(travellersLabel);
        travellers.Minimum = 1;
        travellers.Maximum = 50;
        travellers.Value = 1;
        travellers.Width = 70;
        actions.Controls.Add(travellers);

        bookButton.Text = "Book selected tour";
        bookButton.Width = 170;
        bookButton.Height = 36;
        bookButton.Click += Book;
        actions.Controls.Add(bookButton);
        cancelButton.Text = "Cancel booking";
        cancelButton.Width = 140;
        cancelButton.Height = 36;
        cancelButton.Click += Cancel;
        actions.Controls.Add(cancelButton);
        refreshButton.Text = "Refresh";
        refreshButton.Width = 100;
        refreshButton.Height = 36;
        refreshButton.Click += RefreshClick;
        actions.Controls.Add(refreshButton);
        closeButton.Text = "Close";
        closeButton.Width = 100;
        closeButton.Height = 36;
        closeButton.Click += CloseClick;
        actions.Controls.Add(closeButton);
        root.Controls.Add(actions, 0, 2);
        root.SetColumnSpan(actions, 2);
        Controls.Add(root);
        ResumeLayout(false);
        PerformLayout();
    }

    private const int thirtySix = 36;

    protected override void Dispose(bool disposing)
    {
        if (disposing) components?.Dispose();
        base.Dispose(disposing);
    }
}
