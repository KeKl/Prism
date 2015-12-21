using System.Diagnostics;
using NUnit.Framework;
using Prism.Logging;

namespace Prism.Perspex.Desktop.Tests.Logging
{
    [TestFixture]
    public class TraceLoggerFixture
    {
        TraceListener[] _existingListeners;
        
        [SetUp]
        public void RemoveExisitingListeners()
        {
            _existingListeners = new TraceListener[Trace.Listeners.Count];
            Trace.Listeners.CopyTo(_existingListeners, 0);
            Trace.Listeners.Clear();
        }

        [TearDown]
        public void ReAttachExistingListeners()
        {
            Trace.Listeners.AddRange(_existingListeners);
        }

        [Test]
        public void ShouldWriteToTraceWriter()
        {
            var listener = new MockTraceListener();
            Trace.Listeners.Add(listener);

            var traceLogger = new TraceLogger();
            traceLogger.Log("Test debug message", Category.Debug, Priority.Low);

            Assert.AreEqual("Test debug message", listener.LogMessage);

            Trace.Listeners.Remove(listener);
        }

        [Test]
        public void ShouldTraceErrorException()
        {
            var listener = new MockTraceListener();
            Trace.Listeners.Add(listener);

            var traceLogger = new TraceLogger();
            traceLogger.Log("Test exception message", Category.Exception, Priority.Low);

            Assert.AreEqual("Test exception message", listener.ErrorMessage);

            Trace.Listeners.Remove(listener);
        }
    }

    class MockTraceListener : TraceListener
    {
        public string LogMessage { get; set; }
        public string ErrorMessage { get; set; }

        public override void Write(string message)
        {
            LogMessage = message;
        }

        public override void WriteLine(string message)
        {
            LogMessage = message;
        }

        public override void WriteLine(string message, string category)
        {
            LogMessage = message;
        }

        public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string message)
        {
            if (eventType == TraceEventType.Error)
            {
                ErrorMessage = message;
            }
            else
            {
                LogMessage = message;
            }
        }
    }
}