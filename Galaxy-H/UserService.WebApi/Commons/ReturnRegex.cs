using System.Text.RegularExpressions;

namespace UserService.WebApi.Commons
{
    public class ReturnRegex
    {
        public bool FindRegex(string input,string pattern)
        {
            Regex regeemail = new Regex(pattern);
            return regeemail.IsMatch(input);
        }
    }
}
