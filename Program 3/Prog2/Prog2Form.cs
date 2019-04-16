// Program 3
// CIS 200-01
// Due: 11/13/2017 11:59 P.M.
// Grading ID: C6485
// Description: Explores file I/O and object serialization and expands the GUI application developed in Program 2

// File: Prog2Form.cs
// This class creates the main GUI for Program 2. It provides a
// File menu with About and Exit items, an Insert menu with Address and
// Letter items, and a Report menu with List Addresses and List Parcels
// items.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace UPVApp
{
    public partial class Prog2Form : Form
    {
        private UserParcelView upv; // The UserParcelView

        private BinaryFormatter formatter = new BinaryFormatter();
        private FileStream output;
        private FileStream input;

        // Precondition:  None
        // Postcondition: The form's GUI is prepared for display. A few test addresses are
        //                added to the list of addresses
        public Prog2Form()
        {
            InitializeComponent();

            upv = new UserParcelView();

        }

        // Precondition:  File, About menu item activated
        // Postcondition: Information about author displayed in dialog box
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string NL = Environment.NewLine; // Newline shorthand

            MessageBox.Show($"Program 3{NL}{NL}Grading ID: C6485{NL}Course: CIS 200-01{NL}Due: November 13, 2017 11:59 P.M." +
                $"{NL}Description: Explores file I/O and object serialization and expands the GUI application developed in Program 2");
        }

        // Precondition:  File, Exit menu item activated
        // Postcondition: The application is exited
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Precondition:  Insert, Address menu item activated
        // Postcondition: The Address dialog box is displayed. If data entered
        //                are OK, an Address is created and added to the list
        //                of addresses
        private void addressToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddressForm addressForm = new AddressForm();    // The address dialog box form
            DialogResult result = addressForm.ShowDialog(); // Show form as dialog and store result
            int zip; // Address zip code

            if (result == DialogResult.OK) // Only add if OK
            {
                if (int.TryParse(addressForm.ZipText, out zip))
                {
                    upv.AddAddress(addressForm.AddressName, addressForm.Address1,
                        addressForm.Address2, addressForm.City, addressForm.State,
                        zip); // Use form's properties to create address
                }
                else // This should never happen if form validation works!
                {
                    MessageBox.Show("Problem with Address Validation!", "Validation Error");
                }
            }

            addressForm.Dispose(); // Best practice for dialog boxes
                                   // Alternatively, use with using clause as in Ch. 17
        }

        // Precondition:  Report, List Addresses menu item activated
        // Postcondition: The list of addresses is displayed in the addressResultsTxt
        //                text box
        private void listAddressesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder result = new StringBuilder(); // Holds text as report being built
                                                        // StringBuilder more efficient than String
            string NL = Environment.NewLine;            // Newline shorthand

            result.Append("Addresses:");
            result.Append(NL); // Remember, \n doesn't always work in GUIs
            result.Append(NL);

            foreach (Address a in upv.AddressList)
            {
                result.Append(a.ToString());
                result.Append(NL);
                result.Append("------------------------------");
                result.Append(NL);
            }

            reportTxt.Text = result.ToString();

            // -- OR --
            // Not using StringBuilder, just use TextBox directly

            //reportTxt.Clear();
            //reportTxt.AppendText("Addresses:");
            //reportTxt.AppendText(NL); // Remember, \n doesn't always work in GUIs
            //reportTxt.AppendText(NL);

            //foreach (Address a in upv.AddressList)
            //{
            //    reportTxt.AppendText(a.ToString());
            //    reportTxt.AppendText(NL);
            //    reportTxt.AppendText("------------------------------");
            //    reportTxt.AppendText(NL);
            //}

            // Put cursor at start of report
            reportTxt.Focus();
            reportTxt.SelectionStart = 0;
            reportTxt.SelectionLength = 0;
        }

        // Precondition:  Insert, Letter menu item activated
        // Postcondition: The Letter dialog box is displayed. If data entered
        //                are OK, a Letter is created and added to the list
        //                of parcels
        private void letterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LetterForm letterForm; // The letter dialog box form
            DialogResult result;   // The result of showing form as dialog
            decimal fixedCost;     // The letter's cost

            if (upv.AddressCount < LetterForm.MIN_ADDRESSES) // Make sure we have enough addresses
            {
                MessageBox.Show("Need " + LetterForm.MIN_ADDRESSES + " addresses to create letter!",
                    "Addresses Error");
                return; // Exit now since can't create valid letter
            }

            letterForm = new LetterForm(upv.AddressList); // Send list of addresses
            result = letterForm.ShowDialog();

            if (result == DialogResult.OK) // Only add if OK
            {
                if (decimal.TryParse(letterForm.FixedCostText, out fixedCost))
                {
                    // For this to work, LetterForm's combo boxes need to be in same
                    // order as upv's AddressList
                    upv.AddLetter(upv.AddressAt(letterForm.OriginAddressIndex),
                        upv.AddressAt(letterForm.DestinationAddressIndex),
                        fixedCost); // Letter to be inserted
                }
               else // This should never happen if form validation works!
                {
                    MessageBox.Show("Problem with Letter Validation!", "Validation Error");
                }
            }

            letterForm.Dispose(); // Best practice for dialog boxes
                                  // Alternatively, use with using clause as in Ch. 17
        }

        // Precondition:  Report, List Parcels menu item activated
        // Postcondition: The list of parcels is displayed in the parcelResultsTxt
        //                text box
        private void listParcelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder result = new StringBuilder(); // Holds text as report being built
                                                        // StringBuilder more efficient than String
            decimal totalCost = 0;                      // Running total of parcel shipping costs
            string NL = Environment.NewLine;            // Newline shorthand

            result.Append("Parcels:");
            result.Append(NL); // Remember, \n doesn't always work in GUIs
            result.Append(NL);

            foreach (Parcel p in upv.ParcelList)
            {
                result.Append(p.ToString());
                result.Append(NL);
                result.Append("------------------------------");
                result.Append(NL);
                totalCost += p.CalcCost();
            }

            result.Append(NL);
            result.Append($"Total Cost: {totalCost:C}");

            reportTxt.Text = result.ToString();

            // Put cursor at start of report
            reportTxt.Focus();
            reportTxt.SelectionStart = 0;
            reportTxt.SelectionLength = 0;
        }

        // Precondition:  File, Open menu item activated
        // Postcondition: File is deserialized and contents inserted into upv object

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result; // Variable to hold result of DialogResult
            string fileName; // Variable of type string to hole file name

            using (OpenFileDialog fileChooser = new OpenFileDialog())
            {
                result = fileChooser.ShowDialog(); // Sets result = to ShowDialog
                fileName = fileChooser.FileName; // get specified file name
            }

            if (result == DialogResult.OK) // Make sure file name is acceptable
            {
                if (string.IsNullOrEmpty(fileName))
                {
                    MessageBox.Show("Invalid File Name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); // Displays message if error occurs
                }
                else
                {
                    input = new FileStream(fileName, FileMode.Open, FileAccess.Read); // variable input is assigned contents of file
                }
            }
            if (result == DialogResult.Cancel) // Cancel button clicked returns to application
            {
                return;
            }

            try
            {
               upv = (UserParcelView) formatter.Deserialize(input); // Assigns deserialized input to upv object
            }

            catch (SerializationException) // Catches Serialization exception
            {

                MessageBox.Show("No more records in file", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information); // Gives message when error exception is caught
            }
            finally // ensures close always occurs
            {
                input?.Close(); // Closes the operation 
            }
        }

        // Precondition:  File, Save As menu item activated
        // Postcondition: Contents of form is saved to a file

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result; // creates result to hold DialogResult
            string fileName; // creates fileName to hold the  fileName string

            using (SaveFileDialog fileChooser = new SaveFileDialog())
            {
                fileChooser.CheckFileExists = false; // let user create file

                result = fileChooser.ShowDialog(); // Sets result = to the show dialog
                fileName = fileChooser.FileName; // sets fileName = to the file name selected
            }

            if (result == DialogResult.OK) // Checks to see if what is entered is acceptable
            {
                if (string.IsNullOrEmpty(fileName)) // Checks if fileName is null or empty
                {
                    MessageBox.Show("Invalid File Name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); // Gives error message
                }
                else
                {
                    try
                    {
                        output = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write); // makes output to hold content

                    }

                    catch (IOException)
                    {
                        MessageBox.Show("Error Opening file", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); // Catches exception and displays message
                    }
                }
            }

            if (result == DialogResult.Cancel) // Returns to application if cancel is closed is clicked
            {
                return;
            }

            try
            {
                formatter.Serialize(output, upv); // Serializes upv object and puts in output
            }
            catch (SerializationException)
            {
                MessageBox.Show("Error Writing to File", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); // Catches serialization exception and gives message 
            }
            catch (FormatException)
            {
                MessageBox.Show("Invalid Format", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); // catches format exception and gives message
            }

            try
            {
                output?.Close(); // close the operation
            }
            catch (IOException)
            {
                MessageBox.Show("Cannot Close File", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); // catches IOException, displays error message
            }
            finally
            {
                output?.Close(); // Close the operation no matter what happens
                Application.Exit(); // Closes application 
            }
        }

        private void addressToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ChooseAddressForm chooseAddressForm; // The chooseAddress dialog box form
            DialogResult result;   // The result of showing form as dialog

            if (upv.AddressCount < ChooseAddressForm.MIN_ADDRESSES) // Make sure we have enough addresses
            {
                MessageBox.Show("Need " + ChooseAddressForm.MIN_ADDRESSES + " address to edit address!",
                    "Addresses Error");
                return; // Exit now since can't edit valid address
            }

            chooseAddressForm = new ChooseAddressForm(upv.AddressList); // Send list of addresses
            int index; // Creates index to hold index place as int
            result = chooseAddressForm.ShowDialog(); // Sets result = dialog box of chooseAddressForm
            Address address; // Creates object address of type Address

            if (result == DialogResult.OK) // Only add if OK
            {
                index = chooseAddressForm.AddressIndex;  // Address to be inserted

                address = upv.AddressAt(index); // gives address object the address at specified index

                AddressForm addressForm; // Creates addressForm of type AddressForm
                DialogResult dialogResult; // creates dialogResult of type DialogResult

                addressForm = new AddressForm(); // Create a new instance of the AddressForm

                //Loads in the various address properties
                addressForm.AddressName = address.Name;
                addressForm.Address1 = address.Address1;
                addressForm.Address2 = address.Address2;
                addressForm.City = address.City;
                addressForm.State = address.State;
                addressForm.ZipText = address.Zip.ToString();

                dialogResult = addressForm.ShowDialog(); // Sets dialogResult = to loaded properties
                if (dialogResult == DialogResult.OK) //OK dialog result dpes the same load but now in reverse
                {
                    address.Name = addressForm.AddressName;
                    address.Address1 = addressForm.Address1;
                    address.Address2 = addressForm.Address2;
                    address.City = addressForm.City;
                    address.State = addressForm.State;
                    address.Zip = int.Parse(addressForm.ZipText);
                }
            }

            else // This should never happen if form validation works!
            {
                MessageBox.Show("Problem with Address Validation!", "Validation Error");
            }

            chooseAddressForm.Dispose(); // Best practice for dialog boxes
                                         // Alternatively, use with using clause as in Ch. 17
        }
    }
}