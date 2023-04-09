using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Assertions;

namespace Unity1week202303.Sentences.Data
{
    public class SentenceDataStore
    {
        private readonly string _path = "SentenceDB";
        
        public async UniTask<SentenceDataTable> Load()
        {
            var loadAsync = await Resources.LoadAsync<TextAsset>(_path);
            var textAsset = loadAsync as TextAsset;
            Assert.IsNotNull(textAsset);

            var rows = CSVSerializer.Deserialize<SentenceDataRow>(textAsset.text);
            return new SentenceDataTable(rows);
        }
    }
}
