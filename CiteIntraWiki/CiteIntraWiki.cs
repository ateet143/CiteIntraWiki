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
        #region GLOBAL, LOAD, DISPLAY

        /// <summary>
        /// 6.2 Create a global List<T> of type Information called Wiki.
        /// This is List<T> data structure which hold the Information object as the data type
        /// </summary>
        List<Information> wiki = new List<Information>();

        /// <summary>
        /// 6.4 Create a custom method to populate the ComboBox when the Form Load method is called.
        /// Set the default Combobox and radio button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CiteIntraWiki_Load(object sender, EventArgs e)
        {
            FillComboBox();
            DefaultComboRadio();
        }

        /// <summary>
        ///  6.9 Create a single custom method that will sort and then display the Name and Category from the wiki information in the list.
        ///  Sort the array by Name > Clears the listview items and add item in listview
        /// </summary>
        private void DisplayInfo()
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
        #endregion

        #region COMBOBOX
        /// <summary>
        ///6.4 Read the lines from the file category.txt which has the text.
        ///6.4 Create a custom method to populate the ComboBox when the Form Load method is called.
        /// </summary>
        private void FillComboBox()
        {
            var lines = File.ReadAllLines("category.txt");
            foreach (var line in lines)
            {
                comboBoxCategory.Items.Add(line);
            }
        }

        /// <summary>
        /// This method function to get the string value of the selected item in comboboxCategory
        /// </summary>
        /// <returns>Return the string of comboBoxCategory when it is selected by dropping down the box</returns>
        private string GetComboBoxItem()
        {
            return comboBoxCategory.GetItemText(comboBoxCategory.SelectedItem);
        }

        /// <summary>
        /// This method will return the index position of the string that is stored as string array in comboBoxCategory
        /// </summary>
        /// <param name="category">string which is stored in comboBoxCategory</param>
        /// <returns>Returns the Index value of the comboBoxCategory</returns>
        private int GetIndexComboBoxItem(string category)
        {
            int foundIndex = comboBoxCategory.FindString(category);
            return foundIndex;
        }
        #endregion

        #region RADIOBUTTON
        /// <summary>
        /// 6.6 Create two methods to highlight and return the values from the Radio button GroupBox.
        /// The first method must return a string value from the selected radio button (Linear or Non-Linear). 
        /// </summary>
        /// <returns></returns>

        private string GetRadioButtonText()
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
        /// <summary>
        /// 6.6 Create two methods to highlight and return the values from the Radio button GroupBox.
        /// if the string match any of the Radiobutton then it will check the RadioButton.
        /// </summary>
        /// <param name="structure">represents string to be stored in RadioButton</param>
        
        private void SetRadiobutton(string structure)
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
            int idx = groupBoxStructure.Controls.GetChildIndex(radioStructure);
            _ = (idx == 0) ? radioButtonNonLinear.Checked = true : radioButtonLinear.Checked = true;

        }
        #endregion

        #region ADD
        // 6.3 Create a button method to ADD a new item to the list.
        // Use a TextBox for the Name input, ComboBox for the Category, Radio group for the Structure and Multiline TextBox for the Definition.
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            string messageName = string.Empty;
            string messageDef = string.Empty;
            if (!ValidTextBoxText(textBoxName, out messageName) && !ValidTextBoxText(textBoxDefinition, out messageDef))
            {
                Information info = new Information();
                info.SetName(textBoxName.Text);
                info.SetCategory(GetComboBoxItem());
                info.SetStructure(GetRadioButtonText());
                info.SetDefinition(textBoxDefinition.Text);
                wiki.Add(info);
                DisplayInfo();
                ShowToolStatusLabel("Information Added");
            }
            else
            {
                if (!string.IsNullOrEmpty(messageName))
                {
                    ShowToolStatusLabel(messageName);
                }
                if (!string.IsNullOrEmpty(messageDef))
                {
                    ShowToolStatusLabel(messageDef);
                }
            }
            TextBoxClear();
            DefaultComboRadio();
            textBoxName.Focus();

        }
        #endregion
     
        #region DELETE
        /// <summary>
        /// 6.7 Create a button method that will delete the currently selected record in the ListView. Ensure the user has the option to backout of this action by using a dialog box. 
        /// Display an updated version of the sorted list at the end of this process.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (!IsItemSelectedOrEmpty(out string message))
            {
                ShowToolStatusLabel("Item Not Selected");
                return;
            }

            DialogResult delTask = MessageBox.Show("Data will Permanently Deleted,Do you want to Continue?", "Delete the data...", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (delTask == DialogResult.Yes)
            {
                if (ValidTextBoxText(textBoxName, out string messageName) && ValidTextBoxText(textBoxDefinition, out string messageDef))
                {
                    wiki.RemoveAt(listViewDisplay.SelectedIndices[0]);
                    DisplayInfo();
                    ShowToolStatusLabel("Item Deleted");
                }

            }
            else
            {
                ShowToolStatusLabel("User Cancelled the Operation");
            }
            TextBoxClear();
            DefaultComboRadio();
        }
        #endregion

        #region EDIT
        /// <summary>
        /// 6.8 Create a button method that will save the edited record of the currently selected item in the ListView. 
        /// All the changes in the input controls will be written back to the list. 
        /// Display an updated version of the sorted list at the end of this process.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (!IsItemSelectedOrEmpty(out string message))
            {
                ShowToolStatusLabel("Item Not Selected");
                return;
            }

            DialogResult modifyTask = MessageBox.Show("Data will Permanently Edited,Do you want to Continue?", "Edit the data...", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (modifyTask == DialogResult.Yes)
            {
                string messageName = string.Empty;
                string messageDef = string.Empty;
                if (ValidTextBoxText(textBoxName, out messageName) && ValidTextBoxText(textBoxDefinition, out messageDef))
                {
                    int currentItem = listViewDisplay.SelectedIndices[0];
                    wiki[currentItem].SetName(textBoxName.Text);
                    wiki[currentItem].SetDefinition(textBoxDefinition.Text);
                    wiki[currentItem].SetCategory(GetComboBoxItem());
                    wiki[currentItem].SetStructure(GetRadioButtonText());
                    DisplayInfo();
                    ShowToolStatusLabel("Item Edited");
                }
                else
                {
                    if (!string.IsNullOrEmpty(messageName))
                    {
                        ShowToolStatusLabel(messageName);
                    }
                    if (!string.IsNullOrEmpty(messageDef))
                    {
                        ShowToolStatusLabel(messageDef);
                    }
                }
            }

            else
            {
                ShowToolStatusLabel("User Cancelled the Operation");
            }
            TextBoxClear();
            DefaultComboRadio();
        }
        #endregion

        private void buttonOpen_Click(object sender, EventArgs e)
        {

        }

        private void buttonSave_Click(object sender, EventArgs e)
        {

        }



        #region UTILITIES

        /// <summary>
        ///6.5 Create a custom ValidName method which will take a parameter string value from the Textbox Name and returns a Boolean after checking for duplicates.
        ///Use the built in List<T> method “Exists” to answer this requirement.
        /// </summary>
        /// <param name="textbox">textbox represent the TextBox Object, which can be TextBoxName/TextBoxDefinition</param>
        /// <param name="message"></param>
        /// <returns>Return the bool value of whether string can valid or not</returns>
        private bool ValidTextBoxText(TextBox textbox, out string message)
        {
            bool exist;
            message = string.Empty;
            string name = textbox.Text;
            Information info = new Information();
            //if the source is textboxName then execute this block
            if (textbox == textBoxName)
            {
                info.SetName(name);
                exist = wiki.Exists(dup => dup.GetName().Equals(info.GetName()));
            }
            //if the source is textboxDefinition then execute this block
            else
            {
                info.SetDefinition(name);
                exist = wiki.Exists(dup => dup.GetDefinition().Equals(info.GetDefinition()));
            }

            if (exist)
            {
                message = "Text in " + textbox.Name.Substring(7) + " already exist";
            }
            if (string.IsNullOrEmpty(textbox.Text))
            {
                message = textbox.Name.Substring(7) + " field is Empty";
                return true;
            }
            return exist;
        }

        /// <summary>
        /// 6.11 Create a ListView event so a user can select a Data Structure Name from the list of Names and 
        /// the associated information will be displayed in the related text boxes combo box and radio button.
        /// when the mouse is clicked in the ListView item it will get the index of the selected item 
        /// Calls PopulateInput method to set the input with its corresponding data.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listViewDisplay_MouseClick(object sender, MouseEventArgs e)
        {
            int currentItem = listViewDisplay.SelectedIndices[0];
            PopulateInput(currentItem);
            ShowToolStatusLabel(wiki[currentItem].GetName() + " Selected");
        }

        /// <summary>
        /// check if the list is empty and if the item is not selected from the list.
        /// For the project it is used in the Search and Del operation.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>

        private bool IsItemSelectedOrEmpty(out string message)
        {
            if (listViewDisplay.Items.Count == 0)
            {
                message = "List is empty/Application does not contains any data";
                return false;
            }
            if (!IsListItemSelected())
            {
                message = "Item not selected from the List";
                return false;
            }
            message = "";
            return true;
        }

        /// <summary>
        /// return the bool value whether the item in the listView is selected or not
        /// Is used to provide the feedback to the user. For the project it is used in IsItemSelectedOrEmpty(out string message) method.
        /// </summary>
        /// <returns></returns>
        private bool IsListItemSelected()
        {
            for (int i = 0; i < listViewDisplay.Items.Count; i++)
            {
                if (listViewDisplay.Items[i].Selected == true)
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 6.10 Create a button method that will use the builtin binary search to find a Data Structure name. 
        /// If the record is found the associated details will populate the appropriate input controls and highlight the name in the ListView.
        /// At the end of the search process the search input TextBox must be cleared.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        #region SEARCH
        private void buttonSearch_Click(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(textBoxInput.Text))
            {
                if (listViewDisplay.Items.Count == 0)
                {
                    ShowToolStatusLabel("Item Is Empty");
                    return;
                }
                Information info = new Information();
                info.SetName(textBoxInput.Text);
                wiki.Sort();
                int receivedIndex = wiki.BinarySearch(info);
                if (receivedIndex >= 0)
                {
                    listViewDisplay.Items[receivedIndex].Selected = true;
                    listViewDisplay.Select();
                    PopulateInput(receivedIndex);
                    ShowToolStatusLabel(info.GetName() + " is Found");
                }
                else
                {
                    ShowToolStatusLabel(info.GetName() + " Not Found");
                }
            }
            else
            {
                ShowToolStatusLabel("Item Not Selected");
            }
            textBoxInput.Clear();
            #endregion

        }
        /// <summary>
        /// This method will fill the corresponding data to the input field, such as TextboxName, textBoxDefinition, sets the category and radio button.
        /// For the project is used in Search event and mouseClick event in listview.
        /// </summary>
        /// <param name="indexFromListView">Represents the index value from the listView</param>
        private void PopulateInput(int indexFromListView)
        {
            textBoxName.Text = wiki[indexFromListView].GetName();
            textBoxDefinition.Text = wiki[indexFromListView].GetDefinition();
            wiki[indexFromListView].GetCategory();
            comboBoxCategory.SelectedIndex = GetIndexComboBoxItem(wiki[indexFromListView].GetCategory());
            SetRadiobutton(wiki[indexFromListView].GetStructure());
        }
        /// <summary>
        /// Show the status label and it corresponding messages
        /// </summary>
        /// <param name="message">represent the message that is need to display</param>
        private void ShowToolStatusLabel(string message)
        {
            toolStripStatusLabel1.Visible = true;
            toolStripStatusLabel1.Text = message;
        }
        /// <summary>
        /// 6.12 Create a custom method that will clear and reset the TextBboxes, ComboBox and Radio button
        /// this will clear Name and definition textbox and focus on Name textbox
        /// </summary>
        private void TextBoxClear()
        {
            textBoxName.Clear();
            textBoxDefinition.Clear();
            textBoxName.Focus();
        }

        /// <summary>
        /// 6.12 Create a custom method that will clear and reset the TextBboxes, ComboBox and Radio button
        /// This will sets the Default setting of Combobox and radioButton
        /// </summary>
        private void DefaultComboRadio()
        {
            comboBoxCategory.SelectedIndex = 0;
            radioButtonLinear.Checked = true;
        }

        //6.13 Create a double click event on the Name TextBox to clear the TextBboxes, ComboBox and Radio button.
        private void textBoxName_DoubleClick(object sender, EventArgs e)
        {
            TextBoxClear();
            DefaultComboRadio();
        }
    }
    #endregion
}