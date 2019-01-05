namespace Fodler.Models
{
    public class Result
    {
        public int LearnerId { get; set; }

        public bool WithBody { get; set; }

        public int SuccesfullResults { get; set; }

        public int Mistakes { get; set; }

        public void IncrementMistakes()
        {
            Mistakes += 1;
        }

        public void IncrementSuccess()
        {
            SuccesfullResults += 1;
        }

        public override string ToString()
        {
            return "LearnerID: " + LearnerId + " ; WithBody: " + WithBody + " ; Succes: " + SuccesfullResults + " ; Mistakes: " + Mistakes;
        }
    }
}
