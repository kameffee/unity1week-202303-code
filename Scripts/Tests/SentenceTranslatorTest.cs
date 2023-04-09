using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Unity1week202303.Characters;
using UnityEngine;
using UnityEngine.TestTools;

namespace Unity1week202303.Tests
{
    public class SentenceTranslatorTest
    {
        private class ローマ字からひらがなへ変換
        {
            private SentenceTranslator _sentenceTranslator;

            [SetUp]
            public void Setup()
            {
                _sentenceTranslator = new SentenceTranslator(new CharacterTranslator(new CharacterTable()));
            }

            [TestCase("asobi", "あそび")]
            [TestCase("taiyaki", "たいやき")]
            [TestCase("umebosi", "うめぼし")]
            [TestCase("kotae", "こたえ")]
            [TestCase("ao", "あお")]
            [TestCase("iou", "いおう")]
            public void あ行が含まれる文章(string roman, string result)
            {
                Assert.That(_sentenceTranslator.TranslateRomanToJp(roman), Is.EqualTo(result));
            }

            [TestCase("tekkyuu", "てっきゅう")]
            [TestCase("ottosei", "おっとせい")]
            [TestCase("tatti", "たっち")]
            public void っが含まれる文章(string roman, string result)
            {
                Assert.That(_sentenceTranslator.TranslateRomanToJp(roman), Is.EqualTo(result));
            }

            [TestCase("ge-mu", "げーむ")]
            [TestCase("suta-", "すたー")]
            public void 長音が含まれる文章(string roman, string result)
            {
                Assert.That(_sentenceTranslator.TranslateRomanToJp(roman), Is.EqualTo(result));
            }
            
            [TestCase("pa-tona-", "ぱーとなー")]
            public void ぱ行が含まれる文章(string roman, string result)
            {
                Assert.That(_sentenceTranslator.TranslateRomanToJp(roman), Is.EqualTo(result));
            }
        }

        private class ひらがなからローマ字への変換
        {
            private SentenceTranslator _sentenceTranslator;

            [SetUp]
            public void Setup()
            {
                _sentenceTranslator = new SentenceTranslator(new CharacterTranslator(new CharacterTable()));
            }
            
            [TestCase( "あそび", "asobi")]
            [TestCase( "たいやき", "taiyaki")]
            [TestCase( "うめぼし", "umebosi")]
            [TestCase( "こたえ", "kotae")]
            [TestCase( "あお", "ao")]
            [TestCase( "いおう", "iou")]
            public void あ行が含まれる文章(string jp, string result)
            {
                Assert.That(_sentenceTranslator.TranslateJpToRoman(jp), Is.EqualTo(result));
            }

            [TestCase("てっきゅう", "tekkyuu")]
            [TestCase("おっとせい", "ottosei")]
            [TestCase("たっち", "tatti")]
            public void っが含まれる文章(string jp, string result)
            {
                Assert.That(_sentenceTranslator.TranslateJpToRoman(jp), Is.EqualTo(result));
            }
            
            [TestCase( "げーむ", "ge-mu")]
            [TestCase("すたー", "suta-")]
            public void 長音が含まれる文章(string jp, string result)
            {
                Assert.That(_sentenceTranslator.TranslateJpToRoman(jp), Is.EqualTo(result));
            }
            
            [TestCase( "ぱーとなー", "pa-tona-")]
            public void ぱ行が含まれる文章(string jp, string result)
            {
                Assert.That(_sentenceTranslator.TranslateJpToRoman(jp), Is.EqualTo(result));
            }
        }
        
        private class Character配列へ変換
        {
            private SentenceTranslator _sentenceTranslator;

            [SetUp]
            public void Setup()
            {
                _sentenceTranslator = new SentenceTranslator(new CharacterTranslator(new CharacterTable()));
            }
            
        }
    }
}
