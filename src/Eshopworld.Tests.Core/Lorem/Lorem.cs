// ReSharper disable once CheckNamespace
namespace DevOpsFlex.Tests.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// From: https://github.com/jonwingfield/Faker.Net
    /// Added a few cosmetic tweaks to reduce the codebase.
    /// </summary>
    public static class Lorem
    {
        private static readonly Random Rng = new Random();

        /// <summary>
        /// Gets a single word.
        /// </summary>
        /// <returns>A single word.</returns>
        public static string GetWord()
        {
            return Words.Rand();
        }

        /// <summary>
        /// Gets several words.
        /// </summary>
        /// <param name="num">The number of words to get, defaults with 3.</param>
        /// <returns>Several words.</returns>
        public static IEnumerable<string> GetWords(int num = 3)
        {
            return Words.RandPick(num);
        }

        /// <summary>
        /// Gets a single sentence.
        /// </summary>
        /// <param name="wordCount">The number of words on the sentence, defaults with 4.</param>
        /// <returns>A single sentence.</returns>
        public static string GetSentence(int wordCount = 4)
        {
            var s = GetWords(wordCount + Rng.Next(6));
            return s.Join(" ").ToUpper() + ".";
        }

        /// <summary>
        /// Gets several sentences
        /// </summary>
        /// <param name="sentenceCount">The number of sentences to get, defaults with 3.</param>
        /// <returns>Several sentences.</returns>
        public static IEnumerable<string> GetSentences(int sentenceCount = 3)
        {
            return 1.To(sentenceCount).Select(item => GetSentence());
        }

        /// <summary>
        /// Gets a single paragraph.
        /// </summary>
        /// <param name="sentenceCount">The sentences in the paragraph, defaults with 3.</param>
        /// <returns>A single paragraph.</returns>
        public static string GetParagraph(int sentenceCount = 3)
        {
            return GetSentences(sentenceCount + Rng.Next(3)).Join(" ");
        }

        /// <summary>
        /// Gets several paragraphs.
        /// </summary>
        /// <param name="paragraphCount">The number of paragraphs to get, defaults with 3.</param>
        /// <returns>Several paragraphs.</returns>
        public static IEnumerable<string> GetParagraphs(int paragraphCount = 3)
        {
            return 1.To(paragraphCount).Select(item => GetParagraph());
        }

        private static readonly string[] Words =
        {
            "alias", "consequatur", "aut", "perferendis", "sit", "voluptatem", "accusantium",
            "doloremque", "aperiam", "eaque", "ipsa", "quae", "ab", "illo", "inventore", "veritatis",
            "et", "quasi", "architecto", "beatae", "vitae", "dicta", "sunt", "explicabo", "aspernatur",
            "aut", "odit", "aut", "fugit", "sed", "quia", "consequuntur", "magni", "dolores", "eos", "qui",
            "ratione", "voluptatem", "sequi", "nesciunt", "neque", "dolorem", "ipsum", "quia", "dolor",
            "sit", "amet", "consectetur", "adipisci", "velit", "sed", "quia", "non", "numquam", "eius",
            "modi", "tempora", "incidunt", "ut", "labore", "et", "dolore", "magnam", "aliquam", "quaerat",
            "voluptatem", "ut", "enim", "ad", "minima", "veniam", "quis", "nostrum", "exercitationem",
            "ullam", "corporis", "nemo", "enim", "ipsam", "voluptatem", "quia", "voluptas", "sit",
            "suscipit", "laboriosam", "nisi", "ut", "aliquid", "ex", "ea", "commodi", "consequatur",
            "quis", "autem", "vel", "eum", "iure", "reprehenderit", "qui", "in", "ea", "voluptate", "velit",
            "esse", "quam", "nihil", "molestiae", "et", "iusto", "odio", "dignissimos", "ducimus", "qui",
            "blanditiis", "praesentium", "laudantium", "totam", "rem", "voluptatum", "deleniti",
            "atque", "corrupti", "quos", "dolores", "et", "quas", "molestias", "excepturi", "sint",
            "occaecati", "cupiditate", "non", "provident", "sed", "ut", "perspiciatis", "unde",
            "omnis", "iste", "natus", "error", "similique", "sunt", "in", "culpa", "qui", "officia",
            "deserunt", "mollitia", "animi", "id", "est", "laborum", "et", "dolorum", "fuga", "et", "harum",
            "quidem", "rerum", "facilis", "est", "et", "expedita", "distinctio", "nam", "libero",
            "tempore", "cum", "soluta", "nobis", "est", "eligendi", "optio", "cumque", "nihil", "impedit",
            "quo", "porro", "quisquam", "est", "qui", "minus", "id", "quod", "maxime", "placeat", "facere",
            "possimus", "omnis", "voluptas", "assumenda", "est", "omnis", "dolor", "repellendus",
            "temporibus", "autem", "quibusdam", "et", "aut", "consequatur", "vel", "illum", "qui",
            "dolorem", "eum", "fugiat", "quo", "voluptas", "nulla", "pariatur", "at", "vero", "eos", "et",
            "accusamus", "officiis", "debitis", "aut", "rerum", "necessitatibus", "saepe",
            "eveniet", "ut", "et", "voluptates", "repudiandae", "sint", "et", "molestiae", "non",
            "recusandae", "itaque", "earum", "rerum", "hic", "tenetur", "a", "sapiente", "delectus", "ut",
            "aut", "reiciendis", "voluptatibus", "maiores", "doloribus", "asperiores",
            "repellat"
        };
    }
}