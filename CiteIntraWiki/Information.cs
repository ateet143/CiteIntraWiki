using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CiteIntraWiki
{
    
  
    [Serializable]
    internal class Information : IComparable<Information>
    {

        #region Attributes
        // 6.1 Create a separate class file to hold the four data items of the Data Structure(use the Data Structure Matrix as a guide).
        // Use auto-implemented properties for the fields which must be of type “string”. Save the class as “Information.cs”.
        private string name;
        private string category;
        private string structure;
        private string definition;
        #endregion

        #region Constructor
        public Information()
        {
            name = "n/a"; category = "n/a"; structure = "n/a"; definition = "n/a"; 
        }

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

        public void SetName(string newName)
        {
            if(newName.Length > 1)
            {
                name = newName.Substring(0, 1).ToUpper() + newName.Substring(1);
            }
            if (newName.Length == 1)
            {
                name = newName.ToUpper();
            }

        }

        public string GetCategory()
        {
           return category;
        }

        public void SetCategory(string newCategory)
        {
            category = newCategory;
        }

        public string GetStructure()
        {
            return structure;
        }

        public void SetStructure(string newStructure)
        {
            structure = newStructure;
        }

        public string GetDefinition()
        {
            return definition;
            
        }

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

        public int CompareTo(Information other)
        {
            int result = name.CompareTo(other.name);
            return result;
        }

        #endregion
    }
}
