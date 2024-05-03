using System.Text;

namespace pms_cs.Repository;

public class CompanyNumber
{
    public string Main()
    {
        int length = 10;
        StringBuilder builder = new StringBuilder();

        Random random = new Random();
        for (int i = 0; i < length; i++)
        {
            int numberCom = random.Next(0, 10);
            char charCom = (char)random.Next('a', 'z' + 1);
            builder.Append(numberCom.ToString() + charCom.ToString());
        }

        string companyNumber = $"C-{builder}-PMS";
        return companyNumber;
    }
}