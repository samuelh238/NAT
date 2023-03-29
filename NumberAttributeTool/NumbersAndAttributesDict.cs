using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace NumberAttributeTool
{

    [Serializable]
    public class NumbersAndAttributesDict
    {
        public SortedDictionary<Int64, AttributeDict> NumbersAttributesDict =
            new SortedDictionary<Int64, AttributeDict>();

        public void CalcAttributeFunction(Func<Int64, Int64> attributeFunction, Int64 startValue, Int64 stopValue, string attributeName = "")
        {
            if(attributeName.Equals(""))
                attributeName = attributeFunction.Method.Name;

            Int64 result;
            for (Int64 i = startValue; i <= stopValue; i++)
            {
                AttributeDict? actDict;
                result = attributeFunction(i);
                
                if(NumbersAttributesDict.TryGetValue(i, out actDict))
                {
                    if (actDict == null)
                    {
                        Console.WriteLine("error actDict null");
                        continue;
                    }
                        
                    actDict.addAttribute(attributeName,result);
                }
                else
                {
                    actDict = new AttributeDict();
                    actDict.addAttribute(attributeName, result);
                    NumbersAttributesDict.Add(i, actDict);
                }
            }
        }

        public bool Save()
        {
            try
            {
                var dataPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.CommonApplicationData);
                string appName = Assembly.GetExecutingAssembly().GetName().Name!;
                string version = Assembly.GetExecutingAssembly().GetName().Version!.ToString();
                string dir = $"{dataPath}\\{appName} {version}";
                string filename = $"{dir}\\numData.bin";

                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None);
                formatter.Serialize(stream, this);
                stream.Close();

                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            
        }

        public bool Load(ref NumbersAndAttributesDict numbersAndAttributesToLoad)
        {
            try
            {
                var dataPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.CommonApplicationData);
                string appName = Assembly.GetExecutingAssembly().GetName().Name!;
                string version = Assembly.GetExecutingAssembly().GetName().Version!.ToString();
                string dir = $"{dataPath}\\{appName} {version}";
                string filename = $"{dir}\\numData.bin";

                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
                numbersAndAttributesToLoad = (NumbersAndAttributesDict)formatter.Deserialize(stream);
                stream.Close();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            foreach(Int64 i in NumbersAttributesDict.Keys)
            {
                sb.Append($"{i}:" + Environment.NewLine);
                sb.Append(NumbersAttributesDict[i].ToString());
                sb.Append(Environment.NewLine);
            }

            return sb.ToString();
        }

    }
}
