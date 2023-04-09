using System;
using System.Collections.Generic;
using System.Linq;
using Unity1week202303.Characters;
using Unity1week202303.Sentences.Data;
using UnityEngine.Assertions;

namespace Unity1week202303.Sentences
{
    public class SentenceDataTranslator
    {
        private readonly SentenceTranslator _sentenceTranslator;
        private readonly CharacterTranslator _characterTranslator;

        public SentenceDataTranslator(
            SentenceTranslator sentenceTranslator,
            CharacterTranslator characterTranslator)
        {
            _sentenceTranslator = sentenceTranslator;
            _characterTranslator = characterTranslator;
        }

        public IEnumerable<SentenceData> Translate(IEnumerable<SentenceDataRow> sentenceDataRows)
        {
            return sentenceDataRows.Select(row =>
            {
                return Create(
                    (uint)row.id,
                    row.sentence,
                    row.kana
                );
            });
        }

        private SentenceData Create(uint id, string displayText, string kanaText)
        {
            Assert.IsNotNull(kanaText, $"{nameof(kanaText)} is null. id: {id}");
            var inputText = _sentenceTranslator.TranslateJpToRoman(kanaText);
            
            // _characterTranslator.JpToRomanTranslate()
            
            var sentenceData = new SentenceData(
                new Identifier(id),
                displayText,
                kanaText,
                inputText,
                Array.Empty<Character>()
            );

            return sentenceData;
        }
    }
}
