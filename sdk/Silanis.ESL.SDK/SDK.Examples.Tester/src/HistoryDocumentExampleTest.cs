using System;
using NUnit.Framework;
using Silanis.ESL.SDK;

namespace SDK.Examples
{
    [TestFixture()]
    public class HistoryDocumentExampleTest
    {
        [Test()]
        public void VerifyResult()
        {
            HistoryDocumentExample example = new HistoryDocumentExample( Props.GetInstance() );
            example.Run();

            DocumentPackage documentPackage = example.RetrievedPackage;

            // Verify if the package is created correctly
            Document historyDocument = documentPackage.Documents[example.externalDocumentName];
            Assert.IsNotNull(historyDocument);
            Assert.AreEqual(example.externalDocumentName, historyDocument.Name);

        }
    }
}

