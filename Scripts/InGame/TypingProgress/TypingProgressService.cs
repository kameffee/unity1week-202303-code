using System;
using UniRx;
using Unity1week202303.Characters;
using Unity1week202303.InGame.Combos;
using Unity1week202303.InGame.Situations;
using Unity1week202303.Sentences;

namespace Unity1week202303.TypingProgress
{
    /// <summary>
    /// 入力進捗
    /// </summary>
    public class TypingProgressService
    {
        private char[] _taskWord;
        private string _taskKanaArray;
        private int _currentIndex;
        private SentenceData _currentSententData;
        private TypingProgressData _cacheTypingProgressData;

        private readonly Situation _situation;
        private readonly ComboCounter _comboCounter;
        private readonly SentenceTranslator _sentenceTranslator;
        private readonly ISubject<TypingProgressData> _onUpdateProgress = new Subject<TypingProgressData>();
        private readonly ISubject<TypingProgressData> _onUpdateTask = new Subject<TypingProgressData>();
        private readonly ISubject<Unit> _onCompleted = new Subject<Unit>();
        private readonly ISubject<int> _onFailed = new Subject<int>();

        public TypingProgressService(
            Situation situation,
            ComboCounter comboCounter,
            SentenceTranslator sentenceTranslator)
        {
            _situation = situation;
            _comboCounter = comboCounter;
            _sentenceTranslator = sentenceTranslator;
        }

        public IObservable<TypingProgressData> OnUpdateTaskAsObservable() => _onUpdateTask;

        public IObservable<TypingProgressData> OnUpdateProgressAsObservable() => _onUpdateProgress;

        public IObservable<int> OnFailedTypingAsObservable() => _onFailed;

        public IObservable<Unit> OnCompleted() => _onCompleted;

        public void SetTaskWord(SentenceData sententData)
        {
            _currentSententData = sententData;
            var upperedWord = sententData.AlphabetText.ToUpper();

            _currentIndex = 0;
            _taskWord = upperedWord.ToCharArray();
            _taskKanaArray = sententData.KanaText;

            _cacheTypingProgressData = new TypingProgressData(
                sententData.Id,
                string.Empty,
                upperedWord,
                string.Empty,
                sententData.KanaText
            );
            _onUpdateTask.OnNext(_cacheTypingProgressData);
        }

        public void Input(string inputString)
        {
            var upperedInputString = inputString.ToUpper();

            int sendCount = 0;
            for (var i = 0; i < upperedInputString.Length; i++)
            {
                var currentInputChar = upperedInputString[i];
                if (_taskWord[_currentIndex] == currentInputChar)
                {
                    // 一致したら次の文字を見るように
                    _currentIndex++;
                    sendCount++;
                }
                else
                {
                    // ミス
                    var missCount = upperedInputString.Length - i;
                    _situation.AddMissCount(missCount);
                    _onFailed.OnNext(missCount);
                    break;
                }
            }

            var taskWord = new string(_taskWord);
            var entered = taskWord.Substring(0, _currentIndex);
            var notEntered = taskWord.Substring(_currentIndex, taskWord.Length - _currentIndex);

            // 入力済みの平仮名文字列
            var enteredJpText = _sentenceTranslator.TranslateRomanToJp(entered.ToLower());

            var typingProgressData = new TypingProgressData(
                _currentSententData.Id,
                entered,
                notEntered,
                enteredJpText,
                _taskKanaArray.Substring(enteredJpText.Length)
            );


            if (typingProgressData.SentenceId == _cacheTypingProgressData.SentenceId)
            {
                var puls = typingProgressData.EnteredKanaText.Length - _cacheTypingProgressData.EnteredKanaText.Length;
                _situation.AddSendCount(puls);
            }
            else
            {
                _situation.AddSendCount(typingProgressData.EnteredKanaText.Length);
            }

            _cacheTypingProgressData = typingProgressData;

            _onUpdateProgress.OnNext(typingProgressData);

            if (typingProgressData.IsCompleted())
            {
                // 1文章クリアで1コンボ
                _comboCounter.AddCount(1);
                _onCompleted.OnNext(Unit.Default);
            }
        }
    }
}
