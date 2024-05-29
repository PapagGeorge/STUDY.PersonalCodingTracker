namespace Domain
{
    public class WebServiceResponse
    {
        public string Name { get; }
        public string TwoLetterCode { get; }
        public string ThreeLetterCode { get; }

        public WebServiceResponse(string name, string twoLetterCode, string threeLetterCode)
        {
            Name = name;
            TwoLetterCode = twoLetterCode;
            ThreeLetterCode = threeLetterCode;
        }

        public static WebServiceResponse Create(string name, string twoLetterCode, string threeLetterCode)
        {
            return new WebServiceResponse(name, twoLetterCode, threeLetterCode);
        }
    }
}
