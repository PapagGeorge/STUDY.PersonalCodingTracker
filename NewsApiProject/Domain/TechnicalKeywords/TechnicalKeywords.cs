using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.TechnicalKeywords
{
    public class TechnicalKeywords
    {
        public const string ArtificialIntelligence = "Artificial Intelligence";
        public const string Blockchain = "Blockchain";
        public const string Cybersecurity = "Cybersecurity";
        public const string CloudComputing = "Cloud Computing";
        public const string InternetOfThings = "Internet of Things";
        public const string FiveGTechnology = "5G Technology";
        public const string QuantumComputing = "Quantum Computing";
        public const string BigData = "Big Data";
        public const string AugmentedReality = "Augmented Reality";
        public const string VirtualReality = "Virtual Reality";
        public const string MachineLearning = "Machine Learning";

        private static readonly List<string> _keywords = new List<string>
        {
            ArtificialIntelligence,
            Blockchain,
            Cybersecurity,
            CloudComputing,
            InternetOfThings,
            FiveGTechnology,
            QuantumComputing,
            BigData,
            AugmentedReality,
            VirtualReality,
            MachineLearning
        };

        public static IEnumerable<string> GetAll()
        {
            return _keywords;
        }

        public static bool IsValid(string keyword)
        {
            return _keywords.Contains(keyword);
        }
    }
}
