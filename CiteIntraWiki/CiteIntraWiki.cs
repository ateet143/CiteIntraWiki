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
            comboBoxCategory.SelectedIndex = 0;
            radioButtonLinear.Checked = true;
        }

        private void displayInfo()
        {
            
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
                info.SetStructure(radioButtonText(radioButtonLinear, radioButtonNonLinear));
                info.SetDefinition(textBoxDefinition.Text);
                MessageBox.Show(info.GetDefinition());
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
        }
        #endregion

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

        private string radioButtonText(RadioButton radioButtonLinear, RadioButton radioButtonNLinear)
        {
            string structure = string.Empty;
            RadioButton radioStructure = new RadioButton();
            if (radioButtonLinear.Checked)
            {
                radioStructure = radioButtonNLinear;
            }
            if(radioButtonNLinear.Checked)
            {
                radioStructure = radioButtonNLinear;
            }
            return radioStructure.Text;
        }

      
    }
}