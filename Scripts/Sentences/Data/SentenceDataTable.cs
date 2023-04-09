using System.Collections.Generic;
using System.Linq;

namespace Unity1week202303.Sentences.Data
{
    public class SentenceDataTable
    {
        private IReadOnlyList<SentenceDataRow> _table;

        public SentenceDataTable(IReadOnlyList<SentenceDataRow> table)
        {
            _table = table;
        }

        public IEnumerable<SentenceDataRow> All() => _table;

        public SentenceDataRow Find(int id)
        {
            return _table.First(row => row.id == id);
        }
    }
}
