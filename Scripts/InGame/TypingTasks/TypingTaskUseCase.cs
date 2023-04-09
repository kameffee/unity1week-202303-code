using System.Collections.Generic;
using System.Linq;
using Unity1week202303.Extensions;
using Unity1week202303.Sentences;

namespace Unity1week202303.InGame.TypingTasks
{
    /// <summary>
    /// 出題ユースケース
    /// </summary>
    public class TypingTaskUseCase
    {
        private readonly SentenceRepository _sentenceRepository;

        private readonly Queue<TypingTask> _queue = new();

        public TypingTaskUseCase(SentenceRepository sentenceRepository)
        {
            _sentenceRepository = sentenceRepository;
        }

        public void Set()
        {
            foreach (var tuple in _sentenceRepository.GetAll().Shuffle().Select((sentenceData, index) => (sentenceData, index)))
            {
                _queue.Enqueue(new TypingTask(tuple.index, tuple.sentenceData));
            }
        }

        public bool HasNext()
        {
            return _queue.Count >= 1;
        }

        public TypingTask Next()
        {
            return _queue.Dequeue();
        }
    }
}
