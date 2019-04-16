using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UPVApp
{
    public partial class ChooseAddressForm : Form
    {
        // Precondition:  addresses.Count >= MIN_ADDRESSES
        // Postcondition: The form's GUI is prepared for display.
        public ChooseAddressForm(List<Address> address)
        {
            InitializeComponent();
            addressList = address;
        }

        public const int MIN_ADDRESSES = 1; // Minimum number of addresses needed

        private List<Address> addressList;  // List of addresses used to fill combo box

        internal int AddressIndex
        {
            // Precondition:  User has selected from addressListComboBox
            // Postcondition: The index of the selected address returned
            get
            {
                return addressListComboBox.SelectedIndex;
            }

            // Precondition:  -1 <= value < addressList.Count
            // Postcondition: The specified index is selected in addressListComboBox
            set
            {
                if ((value >= -1) && (value < addressList.Count))
                    addressListComboBox.SelectedIndex = value;
                else
                    throw new ArgumentOutOfRangeException("AddressIndex", value,
                        "Index must be valid"); // Checks to ensure that the index is valid
            }
        }

        // Precondition:  addressList.Count >= MIN_ADDRESSES
        // Postcondition: The list of addresses is used to populate the
        //                Address combo box
        private void ChooseAddressForm_Load(object sender, EventArgs e)
        {
            if (addressList.Count < MIN_ADDRESSES) // Violated precondition!
            {
                MessageBox.Show("Need " + MIN_ADDRESSES + " address to edit address!",
                    "Addresses Error");
                this.DialogResult = DialogResult.Abort; // Dismiss immediately
            }
            else
            {
                foreach (Address a in addressList)
                {
                    addressListComboBox.Items.Add(a.Name); // Adds the address to the combo box in the form of the persons name
                }
            }
        }

        // Precondition:  Focus shifting from one of the address combo boxes
        //                sender is ComboBox
        // Postcondition: If no address selected, focus remains and error provider
        //                highlights the field
        private void addressCbo_Validating(object sender, CancelEventArgs e)
        {
            // Downcast to sender as ComboBox, so make sure you obey precondition!
            ComboBox cbo = sender as ComboBox; // Cast sender as combo box

            if (cbo.SelectedIndex == -1) // -1 means no item selected
            {
                e.Cancel = true;
                errorProvider1.SetError(cbo, "Must select an address");
            }
        }

        // Precondition:  Validating of sender not cancelled, so data OK
        //                sender is Control
        // Postcondition: Error provider cleared and focus allowed to change
        private void AllFields_Validated(object sender, EventArgs e)
        {
            // Downcast to sender as Control, so make sure you obey precondition!
            Control control = sender as Control; // Cast sender as Control
                                                 // Should always be a Control
            errorProvider1.SetError(control, "");
        }

        // Precondition:  User clicked on okBtn
        // Postcondition: If invalid field on dialog, keep form open and give first invalid
        //                field the focus. Else return OK and close form.
        private void okBtn_Click(object sender, EventArgs e)
        {
            // The easy way
            // Raise validating event for all enabled controls on form
            // If all pass, ValidateChildren() will be true
            if (ValidateChildren())
                this.DialogResult = DialogResult.OK;
        }

        // Precondition:  User pressed on cancelButton
        // Postcondition: Form closes
        private void cancelButton_MouseDown(object sender, MouseEventArgs e)
        {
            // This handler uses MouseDown instead of Click event because
            // Click won't be allowed if other field's validation fails

            if (e.Button == MouseButtons.Left) // Was it a left-click?
                this.DialogResult = DialogResult.Cancel;
        }
    }
}
