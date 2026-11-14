using System.Globalization;
using ProductMaintenance.Models;

namespace ProductMaintenance;

public partial class frmAddModify : Form
{
    // This is the Product property requested by Exercise 20-1, step 15.
    public Product Product { get; set; } = new();

    public bool IsAdd { get; set; }

    public frmAddModify()
    {
        InitializeComponent();
    }

    private void frmAddModify_Load(object sender, EventArgs e)
    {
        if (IsAdd)
        {
            Text = "Add Product";
            txtProductCode.ReadOnly = false;
            Product = new Product();
            txtProductCode.Focus();
        }
        else
        {
            Text = "Modify Product";
            txtProductCode.ReadOnly = true;
            DisplayProduct();
        }
    }

    private void DisplayProduct()
    {
        txtProductCode.Text = Product.ProductCode;
        txtDescription.Text = Product.Description;
        txtUnitPrice.Text = Product.UnitPrice.ToString("0.00", CultureInfo.CurrentCulture);
        txtOnHand.Text = Product.OnHandQuantity.ToString(CultureInfo.CurrentCulture);
        txtDescription.Focus();
    }

    private void btnAccept_Click(object sender, EventArgs e)
    {
        if (IsValidData())
        {
            LoadProductData();
            DialogResult = DialogResult.OK;
        }
    }

    private bool IsValidData()
    {
        string errorMessage = "";

        errorMessage += Validator.IsPresent(txtProductCode.Text.Trim(), "Product Code");
        errorMessage += Validator.IsPresent(txtDescription.Text.Trim(), "Description");
        errorMessage += Validator.IsPresent(txtUnitPrice.Text.Trim(), "Unit Price");
        if (!string.IsNullOrWhiteSpace(txtUnitPrice.Text))
            errorMessage += Validator.IsDecimal(txtUnitPrice.Text, "Unit Price");
        errorMessage += Validator.IsPresent(txtOnHand.Text.Trim(), "On Hand Quantity");
        if (!string.IsNullOrWhiteSpace(txtOnHand.Text))
            errorMessage += Validator.IsInt32(txtOnHand.Text, "On Hand Quantity");

        if (txtProductCode.Text.Trim().Length > 10)
            errorMessage += "Product Code must be no more than 10 characters.\n";
        if (txtDescription.Text.Trim().Length > 50)
            errorMessage += "Description must be no more than 50 characters.\n";
        if (decimal.TryParse(txtUnitPrice.Text, NumberStyles.Number,
                CultureInfo.CurrentCulture, out decimal price) && price < 0)
            errorMessage += "Unit Price must be zero or greater.\n";
        if (int.TryParse(txtOnHand.Text, out int quantity) && quantity < 0)
            errorMessage += "On Hand Quantity must be zero or greater.\n";

        if (errorMessage.Length == 0)
            return true;

        MessageBox.Show(errorMessage, "Entry Error", MessageBoxButtons.OK,
            MessageBoxIcon.Warning);
        return false;
    }

    private void LoadProductData()
    {
        // For edits, the original key is retained; the text box is read-only.
        if (IsAdd)
            Product.ProductCode = txtProductCode.Text.Trim();
        Product.Description = txtDescription.Text.Trim();
        Product.UnitPrice = decimal.Parse(txtUnitPrice.Text,
            NumberStyles.Number, CultureInfo.CurrentCulture);
        Product.OnHandQuantity = int.Parse(txtOnHand.Text, CultureInfo.CurrentCulture);
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }
}
