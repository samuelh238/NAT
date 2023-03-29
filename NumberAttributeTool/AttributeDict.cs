using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberAttributeTool
{
    [Serializable]
    public class AttributeDict
    {
        public Dictionary<string, Int64> Attributes;

        public AttributeDict()
        {
            Attributes = new Dictionary<string, Int64>();
        }

        public void addAttribute(string attributeName, Int64 attributeValue)
        {
            
            if (Attributes.TryGetValue(attributeName, out _))
            {
                Attributes[attributeName] = attributeValue; //override value if key already exist
            }
            else
            {
                Attributes.Add(attributeName, attributeValue);
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            foreach (string attributeName in Attributes.Keys)
            {
                sb.Append($"{attributeName} : {Attributes[attributeName]} {Environment.NewLine}");
            }

            return sb.ToString();
        }


    }
}
