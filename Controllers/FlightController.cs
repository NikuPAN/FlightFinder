using Microsoft.AspNetCore.Mvc;
using FlightFinder.Models;

namespace FlightFinder.Controllers
{
    public class FlightController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CountFlights(FlightInputModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.InputString != null)
                {
                    // Count the number of instances of the word "flight"
                    int count = CountWordInstances(model.InputString.ToLower(), "flight");
                    ViewBag.Count = count;
                }
            }
            return View("~/Views/Home/Index.cshtml", model);
        }

        private int CountWordInstances(string text, string word)
        {
            // Count the frequency of each character in the word "flight"
            Dictionary<char, int> wordCharCounts = GetCharacterCounts(word);

            int count = 0;
            int textLength = text.Length;
            int wordLength = word.Length;

            // Iterate through the input text
            for (int i = 0; i <= textLength - wordLength; i++)
            {
                string substring = text.Substring(i, wordLength);

                // Count the frequency of each character in the current substring
                Dictionary<char, int> substringCharCounts = GetCharacterCounts(substring);

                // Check if the substring has the same character counts as "flight"
                if (HasSameCharacterCounts(wordCharCounts, substringCharCounts) && !IsSubstringCounted(text, i, wordLength))
                {
                    count++;
                    MarkSubstringAsCounted(text, i, wordLength);
                }
            }

            return count;
        }

        private Dictionary<char, int> GetCharacterCounts(string str)
        {
            Dictionary<char, int> charCounts = new Dictionary<char, int>();
            foreach (char c in str)
            {
                if (charCounts.ContainsKey(c))
                {
                    charCounts[c]++;
                }
                else
                {
                    charCounts[c] = 1;
                }
            }
            return charCounts;
        }

        private bool HasSameCharacterCounts(Dictionary<char, int> charCounts1, Dictionary<char, int> charCounts2)
        {
            if (charCounts1.Count != charCounts2.Count)
            {
                return false;
            }

            foreach (var kvp in charCounts1)
            {
                char c = kvp.Key;
                int count1 = kvp.Value;
                int count2 = charCounts2.ContainsKey(c) ? charCounts2[c] : 0;

                if (count1 != count2)
                {
                    return false;
                }
            }

            return true;
        }

        private Dictionary<int, bool> countedSubstrings = new Dictionary<int, bool>(); // Store the indices of counted substrings

        private bool IsSubstringCounted(string text, int startIndex, int length)
        {
            for (int i = startIndex; i < startIndex + length; i++)
            {
                if (countedSubstrings.ContainsKey(i))
                {
                    return true;
                }
            }
            return false;
        }

        private void MarkSubstringAsCounted(string text, int startIndex, int length)
        {
            for (int i = startIndex; i < startIndex + length; i++)
            {
                countedSubstrings[i] = true;
            }
        }
    }
}
