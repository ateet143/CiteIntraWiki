using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CiteIntraWiki
{
    
  
    [Serializable]
    internal class Information : IComparable<Information>
    {

        #region Attributes
        /// <summary>
        ///6.1 Create a separate class file to hold the four data items of the Data Structure(use the Data Structure Matrix as a guide).
        ///Use auto-implemented properties for the fields which must be of type “string”. Save the class as “Information.cs”.
        /// </summary>

        private string name;
        private string category;
        private string structure;
        private string definition;
        #endregion

        #region Constructor
        /// <summary>
        /// Default constructor for the class Information and each attributes is initialise with the empty string to deal with null
        /// </summary>
        public Information()
        {
            name = ""; 
            category = ""; 
            structure = ""; 
            definition = ""; 
        }

        /// <summary>
        /// Oveloaded Constructor 
        /// </summary>
        /// <param name="inputName"></param>
        /// <param name="inputCategory"></param>
        /// <param name="inputStructure"></param>
        /// <param name="inputdefintion"></param>
        public Information(string inputName, string inputCategory, string inputStructure, string inputdefintion)
        {
            name = inputName;
            category = inputCategory;
            structure = inputStructure;
            definition = inputdefintion;
        }

        #endregion

        #region Getter and Setter
        public string GetName()
        {
            return name;
        }

        /// <summary>
        /// Setter for name attributes
        /// It will set the first letter of the string as upper case 
        /// </summary>
        /// <param name="newName"></param>
        public void SetName(string newName)
        {
            TextInfo myTI = new CultureInfo("en-AUS", false).TextInfo;
            name = myTI.ToTitleCase(newName);
        }
        
        //Getter for the Category attributes
        public string GetCategory()
        {
           return category;
        }
        //setter for the Catergory attributes
        public void SetCategory(string newCategory)
        {
            category = newCategory;
        }
        //getter for the structure attributes
        public string GetStructure()
        {
            return structure;
        }
        //setter for the structure attributes
        public void SetStructure(string newStructure)
        {
            structure = newStructure;
        }
        //getter for the definition attributes
        public string GetDefinition()
        {
            return definition;
            
        }
        /// <summary>
        /// Setter for definition attributes
        /// It will set all the first letter of sentence as upper case.
        /// </summary>
        /// <param name="newDefinition"></param>
        public void SetDefinition(string newDefinition)
        {
            bool isSentence = true;
            StringBuilder sb = new StringBuilder(newDefinition.Length);
            foreach (var letter in newDefinition)
            {
                if (isSentence)
                {
                    if(letter == ' ')
                    {
                        continue;
                    }
                    sb.Append(char.ToUpper(letter));
                    isSentence = false;
                }
                else
                {
                    sb.Append(letter);
                }
                if(letter == '.' || letter == '?' || letter == '!')
                {
                    isSentence = true;
                }
            }
            definition = sb.ToString();
            
        }
        /// <summary>
        /// Compareto method to sort the data by name of the Information object
        /// </summary>
        /// <param name="newInformation">represents the new Information Object</param>
        /// <returns>return the int value if equal as 0, if greater than new object >0 and <0 if less than</returns>
        public int CompareTo(Information? newInformation)
        {
            if(newInformation == null)
            {
                return 1;
            }
            return name.CompareTo(newInformation.name);
        }

        #endregion
    }
}
