using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CouchConflictDemo.Models;
using Divan;

namespace CouchConflictDemo.Controllers
{
    public class CouchController : Controller
    {
        //
        // GET: /Couch/

        static string masterDatabase = "meeting_master";
        static string slaveDatabase = "meeting_slave";

        static string host = "localhost";
        static int port = 5984;

        static string docId = "2010-AugustMeeting";
        public ActionResult Index()
        {
            var model = new CouchIndexModel();
            model.MasterDocument = String.Format(@"{{""_id"": ""{0}""}}", docId);
            model.SlaveDocument = string.Empty;
            model.Database1Name = masterDatabase;
            model.Database2Name = slaveDatabase;

            var masterDocument = GetDocument(masterDatabase);
            if (masterDocument != null)
                model.MasterDocument = masterDocument;

            var slaveDocument = GetDocument(slaveDatabase);
            if (slaveDocument != null)
                model.SlaveDocument = slaveDocument;

            model.MasterConflicts = GetConflicts(masterDatabase);
            model.SlaveConflicts = GetConflicts(slaveDatabase);

            return View(model);
        }

        private IEnumerable<string> GetConflicts(string dbName)
        {
            string conflictQueryString = @" if(doc._conflicts) { emit(doc._conflicts, null); }";
            var db = GetDatabase(dbName);
            var tempView = db.NewTempView("test", "test", conflictQueryString);

            var result = tempView.Query().GetResult();

            var conficts = new List<string>();
            foreach (var row in result.Rows())
            {
                foreach (var revisionToken in row["key"].Children())
                {
                    string revision = ((Newtonsoft.Json.Linq.JValue)revisionToken).Value.ToString();

                    conficts.Add( GetDocument(dbName, revision));
                }
                
            }

            return conficts;
        }

        private string GetDocument(string dbName)
        {
            var db = GetDatabase(dbName);
            var doc = db.GetDocument(docId);

            if (doc != null)
            {
                return doc.ToString();
            }
            else
            {
                return null;
            }
        }

        private string GetDocument(string dbName, string revision)
        {
            return new CouchRequest(GetDatabase(dbName)).Path(docId)
                .QueryOptions(new Dictionary<string, string> { { "rev", revision } })
                .Parse().ToString();
        }


        public ActionResult EditMasterDocument(CouchIndexModel model)
        {
            var db = GetDatabase(masterDatabase);

            var doc = new CouchJsonDocument(model.MasterDocument);
            db.SaveDocument(doc);

            return this.RedirectToAction("Index");
        }

        public ActionResult EditSlaveDocument(CouchIndexModel model)
        {
            var db = GetDatabase(slaveDatabase);

            var doc = new CouchJsonDocument(model.SlaveDocument);
            db.SaveDocument(doc);

            return this.RedirectToAction("Index");
        }

        private static ICouchDatabase GetDatabase(string dbName)
        {
            var server = GetServer();
            var db = server.GetDatabase(dbName);
            return db;
        }

        private static CouchServer GetServer()
        {
            return new CouchServer(host, port);
        }

        public ActionResult ReplicateToSlave(CouchIndexModel model)
        {
            Replicate(masterDatabase, slaveDatabase);
                
            return this.RedirectToAction("Index");
        }

        private static void Replicate(string sourceDb, string targetDb)
        {
            var result = new CouchRequest(GetServer())
                .Path("_replicate")
                .Data(GetReplicationString(sourceDb, targetDb))
                .Post()
                .String();
        }

        private static string GetReplicationString(string sourceDb, string targetDb)
        {
            return @"{""source"":""" + sourceDb + @""",""target"":""http://" + host + ":" + port + "/" + targetDb + @"""}";
        }

        public ActionResult ReplicateToMaster(CouchIndexModel model)
        {
            Replicate(slaveDatabase, masterDatabase);
            
            return this.RedirectToAction("Index");
        }
    }
}
