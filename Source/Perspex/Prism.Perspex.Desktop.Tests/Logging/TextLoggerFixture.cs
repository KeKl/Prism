using System;
using System.IO;
using System.Text;
using NUnit.Framework;
using Prism.Logging;

namespace Prism.Perspex.Desktop.Tests.Logging
{
    [TestFixture()]
    public class TextLoggerFixture
    {
        [Test]
        public void NullTextWriterThrows()
        {
            Assert.Throws(typeof (ArgumentNullException), () =>
                {
                    ILoggerFacade logger = new TextLogger(null);
                });
        }

        [Test]
        public void ShouldWriteToTextWriter()
        {
            TextWriter writer = new StringWriter();
            ILoggerFacade logger = new TextLogger(writer);

            logger.Log("Test", Category.Debug, Priority.Low);
            StringAssert.Contains("Test", writer.ToString());
            StringAssert.Contains("DEBUG", writer.ToString());
            StringAssert.Contains("Low", writer.ToString());
        }

        [Test]
        public void ShouldDisposeWriterOnDispose()
        {
            MockWriter writer = new MockWriter();
            IDisposable logger = new TextLogger(writer);

            Assert.IsFalse(writer.DisposeCalled);
            logger.Dispose();
            Assert.IsTrue(writer.DisposeCalled);
        }
    }

    internal class MockWriter : TextWriter
    {
        public bool DisposeCalled;
        public override Encoding Encoding
        {
            get { throw new NotImplementedException(); }
        }

        protected override void Dispose(bool disposing)
        {
            DisposeCalled = true;
            base.Dispose(disposing);
        }
    }
}