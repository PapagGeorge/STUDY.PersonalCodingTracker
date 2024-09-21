namespace CodeChallenge_1
{
    public class Citizen
    {
        private int _age;

        public bool CanCitizenVote
        {
            get
            {
                return _age >= 18 ? true : false;
            }
        }

        public int YearsCitizenIsEligibleToVote
        {
            get
            {
                return _age - 19;
            }
        }
    }
}
