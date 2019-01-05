using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Accord.MachineLearning;
using Fodler.Models.OutlookItem;
using Fodler.Properties;
using Microsoft.Office.Interop.Outlook;

namespace Fodler.Helpers
{
    public static class OutlookItemHelpers
    {
        private static readonly List<string> Cz = Resources.stopwords_cz.Tokenize().ToList();
        private static readonly List<string> En = Resources.stopwords_en.Tokenize().ToList();
        private static readonly List<string> Sk = Resources.stopwords_sk.Tokenize().ToList();
        private static readonly HashSet<string> StopWords = new HashSet<string>(Cz.Concat(En).Concat(Sk));

        public static IOutlookItem GetItemClass(object item){
            if (item is MailItem mail)
            {
                return new ItemMail(mail);
            }
            if (item is MeetingItem meeting)
            {
                return new ItemMeeting(meeting);
            }
            return null;
        }

        /// <summary>
        /// Transform mail body for learner. Removes special characters, removes Czech, Slovak and English stop words and tokenize text. 
        /// </summary>
        /// <param name="body">Text to be transformed</param>
        public static string[] TransformBody(string body)
        {
            body = !string.IsNullOrWhiteSpace(body) ? Regex.Replace(body, @"<.*?>", string.Empty) : string.Empty;
            body = !string.IsNullOrWhiteSpace(body) ? Regex.Replace(body, @"[@!,\\.;'\\\\]", string.Empty) : string.Empty;
            body = !string.IsNullOrWhiteSpace(body) ? Regex.Replace(body, @"\t|\n", string.Empty) : string.Empty;
            var trimmedBody = body?.Tokenize();
            return trimmedBody.Where(word => !StopWords.Contains(word)).ToArray();
        }

        public static List<string> GetKeywords(string body, int count, int minOccurance = 0)
        {
            return GetKeywords(TransformBody(body), count);
        }

        public static List<string> GetKeywords(string[] body, int count, int minOccurance = 0)
        {
            var bow = new BagOfWords()
            {
                MaximumOccurance = 500
            };
            bow.Learn(body);
            int[] codedBody = new int[body.Length];
            bow.Transform(body, codedBody);

            var dictionary = codedBody.Select((value, index) => new { value, index })
                .ToDictionary(pair => pair.index, pair => pair.value)
                .OrderByDescending(x => x.Value)
                .Where(x => x.Value > minOccurance)
                .Select(x => x.Key)
                .Take(count)
                .ToList();

            List<string> result = new List<string>();
            var codebook = bow.CodeToString;
            foreach (var keyWord in dictionary)
            {
                result.Add(codebook[keyWord]);
            }
            return result;
        }
    }
}
