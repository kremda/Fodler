using System.Linq;

namespace Fodler.Helpers
{
    public static class AccordHelpers
    {
        /// <summary>
        /// Combines vectors into one vector
        /// </summary>
        public static double[] CombineInput(double[] input, double[] subject, double[] text)
        {
            var result = input.ToList();
            result.AddRange(subject.ToList());
            result.AddRange(text.ToList());
            return result.ToArray();
        }
    }
}
