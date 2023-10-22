using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Manually_Throwing_Custom_Exception
{
    public partial class frmAddProduct : Form
    {

        private string _ProductName;
        private string _Category;
        private string _MfgDate;
        private string _ExpDate;
        private string _Description;
        private int _Quantity;
        private double _SellPrice;

            BindingSource showProductList;

        public frmAddProduct()
        {
            InitializeComponent();
            showProductList = new BindingSource();
           
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {

            try
            {
                _ProductName = Product_Name(txtProductName.Text);
                _Category = cbCategory.Text;

                _MfgDate = dtPickerMfgDate.Value.ToString("yyyy-MM-dd");
                _ExpDate = dtPickerExpDate.Value.ToString("yyyy-MM-dd");

                _Description = richTxtDescription.Text;

                _Quantity = Quantity(txtQuantity.Text);
                _SellPrice = SellingPrice(txtSellPrice.Text);

                showProductList.Add(new ProductClass(_ProductName, _Category, _MfgDate,
                _ExpDate, _SellPrice, _Quantity, _Description));

                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.DataSource = showProductList;

                txtProductName.Clear();
                txtQuantity.Clear();
                txtSellPrice.Clear();

                cbCategory.ResetText();

                dtPickerMfgDate.ResetText();
                dtPickerExpDate.ResetText();

                richTxtDescription.Clear();

                dataGridView1.Refresh();
            }
         
            catch (NumberFormatException ne) 
            {
                MessageBox.Show("NumberFormatException : - " + ne.Message);
            }
            catch (StringFormatException st)
            {
                MessageBox.Show("StringFormatException : - " + st.Message);
            }
            catch (CurrencyFormatException ce)
            {
                MessageBox.Show("CurrencyFormatException : - " + ce.Message);
            }

           

        }

        private void frmAddProduct_Load(object sender, EventArgs e)
        {
            ArrayList ListOfProductCategory = new ArrayList();

            ListOfProductCategory.Add("Beverages");
            ListOfProductCategory.Add("Bread/Bakery");
            ListOfProductCategory.Add("Canned/Jarred Goods");
            ListOfProductCategory.Add("Dairy");
            ListOfProductCategory.Add("Frozen Goods");
            ListOfProductCategory.Add("Meat");
            ListOfProductCategory.Add("Personal Care");
            ListOfProductCategory.Add("Other");

            foreach (string items in ListOfProductCategory)
            {
                cbCategory.Items.Add(items);
            }

        }
        // custom exceptions
        class NumberFormatException : Exception
        {
            public NumberFormatException(string num) : base(num) { }
        }

        class StringFormatException : Exception
        {
            public StringFormatException(string str) : base(str) { }
        }
        class CurrencyFormatException : Exception
        {
            public CurrencyFormatException(string cre) : base(cre) { }
        }
        // custom exceptions



        public string Product_Name(string name)
        {
            if (!Regex.IsMatch(name, @"^[a-zA-Z]+$"))
            {
                throw new NumberFormatException(" please input product name correctly ");
            }
            else
            {

            }
                   return name;
        }
        public int Quantity(string qty)
        {
            if (!Regex.IsMatch(qty, @"^[0-9]"))
            {
                throw new StringFormatException(" please input quantity correctly ");
            }
            else
            {

            }
              
                return Convert.ToInt32(qty);
        }
        public double SellingPrice(string price)
        {
            if (!Regex.IsMatch(price.ToString(), @"^(\d*\.)?\d+$"))
            {
                throw new CurrencyFormatException(" please input price correctly ");
            }
            else
            {

            }
                
                return Convert.ToDouble(price);
        }


         
    }
}
