namespace NumberAttributeTool
{
    public class Program
    {

        public static void Main(string[] args)
        {
            NumbersAndAttributesDict numsAndAtts = new NumbersAndAttributesDict();

            numsAndAtts.CalcAttributeFunction(IsEven, 0, 1000);
            numsAndAtts.CalcAttributeFunction(SumOfDigits, 0, 1000);

            numsAndAtts.Save();

            //numsAndAtts = new NumbersAndAttributesDict();

            //numsAndAtts.Load(ref numsAndAtts);

            //Console.WriteLine(numsAndAtts.ToString());

        }

        
        //Attribute Functions
        public static Int64 IsEven(Int64 num)
        {
            if (num % 2 == 0)
                return 1;
            return 0;
        }

        public static Int64 SumOfDigits(Int64 num)
        {
            Int64 sum = 0;

            while(num > 0) 
            {
                sum += num % 10;
                num = num / 10;
            }

            return sum;
        }

        public static Int64 ReverseNum(Int64 num)
        {
            Int64 sum = 0, reverse;

            while (num > 0)
            {
                reverse = num % 10;
                sum = (sum * 10) + reverse;
                num = num / 10;
            }

            return sum;
        } 
    }
}