using System;
using System.Linq.Expressions;
using Eshopworld.Core;
using Moq;

namespace Eshopworld.Tests.Core
{
    /// <summary>
    /// Extensions for <see cref="IBigBrother"/>
    /// </summary>
    public static class BigBrotherExtensions
    {
        /// <summary>
        /// Specifies a setup on the IBigBrother mock for a call to the Publish method
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="mock">Instance of Mock&lt;IBigBrother&gt;</param>
        /// <param name="telemetryEvent">The event to be published</param>
        public static void SetupPublish<T>(this Mock<IBigBrother> mock, T telemetryEvent)
            where T : TelemetryEvent
        {
            mock.Setup(call => call.Publish(telemetryEvent, It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()));
        }

        /// <summary>
        /// Verifies that an event of type <typeparam name="T"></typeparam> has been published with the IBigBrother mock
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="mock">Instance of Mock&lt;IBigBrother&gt;</param>
        /// <param name="eventVerifyPredicate">(Optional) The predicate to match the event</param>
        /// <param name="times">Verify the number of times a method is allowed to be called</param>
        public static void VerifyPublish<T>(this Mock<IBigBrother> mock, Expression<Func<T, bool>> eventVerifyPredicate = null, Times? times = null)
            where T : TelemetryEvent
        {
            var timesInternal = times ?? Times.Once();

            if (eventVerifyPredicate != null)
            {
                mock.Verify(call => call.Publish<T>(It.Is<T>(eventVerifyPredicate), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()), timesInternal);
            }
            else
            {
                mock.Verify(call => call.Publish<T>(It.IsAny<T>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()), timesInternal);
            }
        }
    }
}