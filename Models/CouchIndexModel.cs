
using System.Collections.Generic;
namespace CouchConflictDemo.Models
{
    public class CouchIndexModel
    {
        public string MasterDocument { get; set; }
        public string SlaveDocument { get; set; }

        public string Database1Name { get; set; }
        public string Database2Name { get; set; }

        public string DatabaseName { get; set; }

        public IEnumerable<string> MasterConflicts { get; set; }
        public IEnumerable<string> SlaveConflicts { get; set; }
    }
}