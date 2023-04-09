using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Unity1week202303.Sentences.Data;

namespace Unity1week202303.Sentences
{
    public class SentenceRepository
    {
        private readonly List<SentenceData> _sentenceDatas = new();
        private readonly SentenceDataStore _dataStore;
        private readonly SentenceDataTranslator _sentenceDataTranslator;

        public SentenceRepository(
            SentenceDataStore dataStore,
            SentenceDataTranslator sentenceDataTranslator)
        {
            _dataStore = dataStore;
            _sentenceDataTranslator = sentenceDataTranslator;
        }

        public async UniTask LoadAsync()
        {
            var dataTable = await _dataStore.Load();
            _sentenceDatas.Clear();
            _sentenceDatas.AddRange(_sentenceDataTranslator.Translate(dataTable.All()));

            // TODO: 別のどこからか取ってくる
            // _sentenceDatas.Add(Create(0, "ゲーム開発", "げーむかいはつ"));
            // _sentenceDatas.Add(Create(1, "ユニティ", "ゆにてぃ"));
            // _sentenceDatas.Add(Create(2, "ゴリラ", "ごりら"));
        }

        public IEnumerable<SentenceData> GetAll() => _sentenceDatas;

        public SentenceData Get(Identifier id)
        {
            return _sentenceDatas.First(data => data.Id == id);
        }
    }
}
