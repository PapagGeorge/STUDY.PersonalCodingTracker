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
                return _age - 18;
            }
        }

        public int CalculateTimesCitizenHasVoted()
        {
            if(!CanCitizenVote)
            {
                Console.WriteLine("Citizen is underrage, thus hasn't ever voted yet.");
                return 0;
            }
            else
            {
                return YearsCitizenIsEligibleToVote / 4;
            }
        }
    }
}
