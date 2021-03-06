using System;
using System.IO;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;

namespace SDK.Examples
{
    public class MergeFieldValidationExample : SDKSample
    {
        public string email1;
        private Stream fileStream;

        public MergeFieldValidationExample(Props props) : this(props.Get("api.key"), props.Get("api.url"), props.Get("1.email"))
        {
        }

        public MergeFieldValidationExample(string apiKey, string apiUrl, string email1) : base( apiKey, apiUrl )
        {
            this.email1 = email1;
            this.fileStream = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/src/document.pdf").FullName);
        }

        override public void Execute()
        {
            DocumentPackage superDuperPackage =
                PackageBuilder.NewPackageNamed("MergeFieldValidationExample: " + DateTime.Now)
                    .DescribedAs("This is a package created using the e-SignLive SDK")
                    .WithSettings(DocumentPackageSettingsBuilder.NewDocumentPackageSettings().WithInPerson())
                    .ExpiresOn(DateTime.Now.AddMonths(100))
                    .WithSigner(SignerBuilder.NewSignerWithEmail(email1)
                                .WithCustomId("signatureId1")
                                .WithFirstName("firstName1")
                                .WithLastName("lastName1"))
                    .WithDocument(DocumentBuilder.NewDocumentNamed("First Document")
                                  .WithId("documentId")
                                  .FromStream(fileStream, DocumentType.PDF)
                                  .WithSignature(SignatureBuilder.CaptureFor(email1)
                                  .WithName("Signature1")
                                  .WithSize(100, 22)
                                  .AtPosition(100, 500)
                                  .OnPage(0)
                                  .WithField(FieldBuilder.RadioButton("group")
                                           .WithId("radio1Id")
                                           .OnPage(0).WithSize(20, 20)
                                           .AtPosition(140, 130)
                                           .WithValidation(FieldValidatorBuilder.Basic().Required()))
                                   .WithField(FieldBuilder.RadioButton("group")
                                           .WithId("radio2Id")
                                           .OnPage(0).WithSize(20, 20)
                                           .AtPosition(140, 165)
                                           .WithValidation(FieldValidatorBuilder.Basic()))))
                    .Build();

            packageId = eslClient.CreatePackage(superDuperPackage);
            eslClient.SendPackage( packageId );
        }
    }
}

