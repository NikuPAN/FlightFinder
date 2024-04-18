using Microsoft.AspNetCore.Mvc;
using FlightFinder.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.DataProtection.KeyManagement;

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

        public static int CountWordInstances(string input, string word)
        {
            if (string.IsNullOrEmpty(input) || string.IsNullOrEmpty(word))
            {
                return 0;
            }

            int count = 0;
            Dictionary<char, int> wordCharCounts = new Dictionary<char, int>();

            foreach (char c in input)
            {
                if (word.Contains(c))
                {
                    if (!wordCharCounts.ContainsKey(c))
                    {
                        wordCharCounts[c] = 0;
                    }
                    wordCharCounts[c]++;
                }

            }

            count = wordCharCounts.Values.Min();

            if (ContainsAllCharacters(wordCharCounts, word))
            {
                return count;
            }
            else
            {
                return 0;
            }
        }

        private static bool ContainsAllCharacters(Dictionary<char, int> dictionary, string input)
        {
            // Iterate through each character in the input string
            foreach (char c in input)
            {
                // Check if the dictionary contains the character as a key
                if (!dictionary.ContainsKey(c))
                {
                    // If the character is not found in the dictionary, return false
                    return false;
                }
            }

            // All characters in the input string are found in the dictionary
            return true;
        }
    }
}
