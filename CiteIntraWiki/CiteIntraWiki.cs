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
            textBoxName.Text = wiki[currentItem].GetName();
            textBoxDefinition.Text = wiki[currentItem].GetDefinition();
            wiki[currentItem].GetCategory();
            comboBoxCategory.SelectedIndex = indexComboBoxItem(wiki[currentItem].GetCategory());
            int idx = checkRadiobutton(wiki[currentItem].GetStructure());
            _ = (idx == 0) ? radioButtonNonLinear.Checked = true : radioButtonLinear.Checked = true;

        }
    }
}