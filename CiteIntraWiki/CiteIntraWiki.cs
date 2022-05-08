/// Created by Atit
/// Version 1
/// This application is a prototype of the wikipedia, application has used List<T> to store the  Data Structure information,However user can able to ADD, EDIT and DELETE the information in the application.
/// Similarly, application open and saves file in the home folder, additionally user can able to sort and search the data by entering data in the textbox.
namespace CiteIntraWiki
{
    public partial class CiteIntraWiki : Form
    {
        public CiteIntraWiki()
        {
            InitializeComponent();
        }

        //6.2 Create a global List<T> of type Information called Wiki.
        List<Information> wiki = new List<Information>();
        //6.4 Create and initialise a global string array with the six categories as indicated in the Data Structure Matrix.
        string[] comboCategory = { "Array", "List", "Tree", "Graphs", "Abstract", "Hash" };

        private void CiteIntraWiki_Load(object sender, EventArgs e)
        {
            //6.4 Create a custom method to populate the ComboBox when the Form Load method is called.
            populateComboBox(comboCategory);
            defaultComboRadio();
        }

        //6.9 Create a single custom method that will sort and then display the Name and Category from the wiki information in the list.
        private void displayInfo()
        {
            wiki.Sort();
            listViewDisplay.Items.Clear();
            foreach (Information info in wiki)
            {
                ListViewItem lvi = new ListViewItem(info.GetName());
                lvi.SubItems.Add(info.GetCategory());
                listViewDisplay.Items.Add(lvi);
            }

        }

        //6.4 Create a custom method to populate the ComboBox when the Form Load method is called.
        private void populateComboBox(string[] newComboCategory)
        {
            foreach (var category in newComboCategory)
            {
                comboBoxCategory.Items.Add(category);
            }
        }

        private string getComboBoxItem()
        {
            return comboBoxCategory.GetItemText(comboBoxCategory.SelectedItem);
        }

        private void comboBoxCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            string category = comboBoxCategory.GetItemText(comboBoxCategory.SelectedItem);
        }

        private int indexComboBoxItem(string category)
        {
            int a = comboBoxCategory.FindString(category);
            return a;
        }


        #region ADD
        // 6.3 Create a button method to ADD a new item to the list.
        // Use a TextBox for the Name input, ComboBox for the Category, Radio group for the Structure and Multiline TextBox for the Definition.
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            string messageName = string.Empty;
            string messageDef = string.Empty;
            if (checkvalidTextBox(textBoxName, out messageName) && checkvalidTextBox(textBoxDefinition, out messageDef))
            {
                Information info = new Information();
                info.SetName(textBoxName.Text);
                info.SetCategory(getComboBoxItem());
                info.SetStructure(radioButtonText());
                info.SetDefinition(textBoxDefinition.Text);
                wiki.Add(info);
                displayInfo();
            }
            else
            {
                if (!string.IsNullOrEmpty(messageName))
                {
                    MessageBox.Show(messageName);
                }
                if (!string.IsNullOrEmpty(messageDef))
                {
                    MessageBox.Show(messageDef);
                }
            }
            textBoxClear();
            defaultComboRadio();
            textBoxName.Focus();

        }
        #endregion
        //6.5 Create a custom ValidName method which will take a parameter string value from the Textbox Name and returns a Boolean after checking for duplicates.
        //Use the built in List<T> method “Exists” to answer this requirement.
        private bool checkvalidTextBox(TextBox textbox, out string message)
        {
            message = string.Empty;
            string name = textbox.Text;
            Boolean exist = wiki.Exists(dup => dup.GetName().Equals(name));
            if (exist)
            {
                message = "Text in " + textbox.Name + " is duplicate";
                return false;
            }
            if (String.IsNullOrEmpty(textbox.Text))
            {
                message = textbox.Name.Substring(7) + " field is Empty";
                return false;
            }
            return true;
        }

        //6.6 Create two methods to highlight and return the values from the Radio button GroupBox.
        //The first method must return a string value from the selected radio button (Linear or Non-Linear). 
        private string radioButtonText()
        {
            var radioStructure = new RadioButton();

            foreach (var rdo in groupBoxStructure.Controls.OfType<RadioButton>())
            {
                if (rdo.Checked)
                {
                    radioStructure = rdo;
                    break;
                }
            }
            return radioStructure.Text;

        }

        //6.6 Create two methods to highlight and return the values from the Radio button GroupBox.
        //The second method must send an integer index which will highlight an appropriate radio button.
        private int checkRadiobutton(string structure)
        {
            var radioStructure = new RadioButton();
            foreach (var rdo in groupBoxStructure.Controls.OfType<RadioButton>())
            {
                if (rdo.Text.Equals(structure))
                {
                    radioStructure = rdo;
                    break;
                }
            }
            return groupBoxStructure.Controls.GetChildIndex(radioStructure);

        }

        private void textBoxClear()
        {
            textBoxName.Clear();
            textBoxDefinition.Clear();
            textBoxName.Focus();
        }

        private void defaultComboRadio()
        {
            comboBoxCategory.SelectedIndex = 0;
            radioButtonLinear.Checked = true;
        }

        private void listViewDisplay_MouseClick(object sender, MouseEventArgs e)
        {
            int currentItem = listViewDisplay.SelectedIndices[0];
            showInfoToAssociateInput(currentItem);
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (!checkfordelandedit(out string message))
            {
                //Status strip message
                return;
            }

            DialogResult delTask = MessageBox.Show("Data will Permanently Deleted,Do you want to Continue?", "Delete the data...", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (delTask == DialogResult.Yes)
            {
                if (checkvalidTextBox(textBoxName, out string messageName) && checkvalidTextBox(textBoxDefinition, out string messageDef))
                {
                    wiki.RemoveAt(listViewDisplay.SelectedIndices[0]);
                    displayInfo();
                    //status strip message
                }

            }
            else
            {
                //status strip (user cancelled operation)
            }
            textBoxClear();
            defaultComboRadio();
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (!checkfordelandedit(out string message))
            {
                //Status strip message
                return;
            }


            DialogResult modifyTask = MessageBox.Show("Data will Permanently Edited,Do you want to Continue?", "Edit the data...", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (modifyTask == DialogResult.Yes)
            {
                string messageName = string.Empty;
                string messageDef = string.Empty;
                if (checkvalidTextBox(textBoxName, out messageName) && checkvalidTextBox(textBoxDefinition, out messageDef))
                {
                    int currentItem = listViewDisplay.SelectedIndices[0];
                    wiki[currentItem].SetName(textBoxName.Text);
                    wiki[currentItem].SetDefinition(textBoxDefinition.Text);
                    wiki[currentItem].SetCategory(getComboBoxItem());
                    wiki[currentItem].SetStructure(radioButtonText());
                    displayInfo();
                    //status strip message
                }
                else
                {
                    if (!string.IsNullOrEmpty(messageName))
                    {
                        MessageBox.Show(messageName); //replace with status strip
                    }
                    if (!string.IsNullOrEmpty(messageDef))
                    {
                        MessageBox.Show(messageDef); //replace with status strip
                    }
                }

            }

            else
            {
                //status strip
            }
            textBoxClear();
            defaultComboRadio();
        }

        //check if the list is empty and if the item is not selected from the list.
        private bool checkfordelandedit(out string message)
        {
            if (listViewDisplay.Items.Count == 0)
            {
                message = "List is empty/Application does not contains any data";
                return false;
            }
            if (listViewDisplay.Items[0].Selected == false)
            {
                message = "Item not selected from the List";
                return false;
            }
            message = "";
            return true;
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
           
            if (!string.IsNullOrEmpty(textBoxInput.Text))
            {
                if (listViewDisplay.Items.Count == 0)
                {
                    //Status strip no item in the list
                    return;
                }
                wiki.Sort();
                int receivedIndex = wiki.BinarySearch(new Information(textBoxInput.Text, "", "", ""));
                if(receivedIndex >= 0)
                {
                    listViewDisplay.Items[receivedIndex].Selected = true;
                    listViewDisplay.Select();
                    showInfoToAssociateInput(receivedIndex);
                    //Status strip message found
                }
                else
                {
                    //Status strip message not found
                }
            }
            else
            {
                //Status strip message for not typing in textboxinput
            }
           textBoxInput.Clear();
          

        }
       
        private void showInfoToAssociateInput(int indexFromListView)
        {
            textBoxName.Text = wiki[indexFromListView].GetName();
            textBoxDefinition.Text = wiki[indexFromListView].GetDefinition();
            wiki[indexFromListView].GetCategory();
            comboBoxCategory.SelectedIndex = indexComboBoxItem(wiki[indexFromListView].GetCategory());
            int idx = checkRadiobutton(wiki[indexFromListView].GetStructure());
            _ = (idx == 0) ? radioButtonNonLinear.Checked = true : radioButtonLinear.Checked = true;
        }
    }
}