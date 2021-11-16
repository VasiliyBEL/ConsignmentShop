using ConsignmentShopLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsignmentShopUI
{
    public partial class ConsignmentShop : Form
    {
        private Store store = new Store();
        private List<Item> shoppingCartData = new List<Item>();
        BindingSource itemsBinding = new BindingSource();
        BindingSource cartBinding = new BindingSource();
        BindingSource vendorsBinding = new BindingSource();
        private decimal storeProfit = 0;

        public ConsignmentShop()
        {
            InitializeComponent();
            SetupData();

            itemsBinding.DataSource = store.Items.Where(x => x.Sold == false).ToList();
            itemsListbox.DataSource = itemsBinding;

            itemsListbox.DisplayMember = "Display";
            itemsListbox.ValueMember = "Display";

            cartBinding.DataSource = shoppingCartData;
            shoppingCartListbox.DataSource = cartBinding;

            shoppingCartListbox.DisplayMember = "Display";
            shoppingCartListbox.ValueMember = "Display";

            vendorsBinding.DataSource = store.Vendors;
            vendorListbox.DataSource = vendorsBinding;

            vendorListbox.DisplayMember = "Display";
            vendorListbox.ValueMember = "Display";
        }

        private void SetupData()
        {
            store.Vendors.Add(new Vendor { FirstName = "Vasilit", LastName = "Shalome" });
            store.Vendors.Add(new Vendor { FirstName = "Bill", LastName = "Murey" });

            store.Items.Add(new Item
            {
                Title = "Sneakers",
                Description = "World of shoess",
                Price = 3M,
                Owner = store.Vendors[0]
            });

            store.Items.Add(new Item
            {
                Title = "Caps",
                Description = "No Cappp!",
                Price = 2M,
                Owner = store.Vendors[0]
            });

            store.Items.Add(new Item
            {
                Title = "C#",
                Description = "Learning language of programming",
                Price = 5M,
                Owner = store.Vendors[1]
            });

            store.Items.Add(new Item
            {
                Title = "SQL",
                Description = "Learning Database",
                Price = 5M,
                Owner = store.Vendors[1]
            });

            store.Name = "Book for everyone";
        }

        private void shoppingCartListboxLabel_Click(object sender, EventArgs e)
        {

        }

        private void itemsListboxLabel_Click(object sender, EventArgs e)
        {

        }

        private void shoppingCartListbox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void addToCart_Click(object sender, EventArgs e)
        {
            // Figure out what is selected from items list
            // Copy that item to the shopping cart
            // Do we remove the item from the items list? - no
            Item selectedItem = (Item)itemsListbox.SelectedItem;

            MessageBox.Show(selectedItem.Title);

            shoppingCartData.Add(selectedItem);

            cartBinding.ResetBindings(false);
        }

        private void makePurchase_Click(object sender, EventArgs e)
        {
            // Mark each item in the cart as sold
            // Clear the cart

            foreach (Item item in shoppingCartData)
            {
                item.Sold = true;
                item.Owner.PaymentDue += (decimal)item.Owner.Commission * item.Price;
                storeProfit += (1 - (decimal)item.Owner.Commission) * item.Price;
            }

            shoppingCartData.Clear();

            itemsBinding.DataSource = store.Items.Where(x => x.Sold == false).ToList();

            storeProfitValue.Text = string.Format("${0}", storeProfit);

            cartBinding.ResetBindings(false);
            itemsBinding.ResetBindings(false);
            vendorsBinding.ResetBindings(false);
        }
    }
}
