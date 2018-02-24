namespace Eshopworld.Tests.Core
{
    using System;
    using System.IO;
    using System.Reflection;

    /// <summary>
    /// Wraps an embed resource around a class that can materialize it to the file system
    /// and delete on the call to Dispose.
    /// </summary>
    public class TestResource : IDisposable
    {
        /// <summary>
        /// Initializes a Resource wrapper based on a namespace and a resource name.
        /// </summary>
        /// <param name="baseNamespace">The namespace of the resource.</param>
        /// <param name="resourceName">The name of the resource.</param>
        public TestResource(string baseNamespace, string resourceName)
        {
            using (var resourceStream = Assembly.GetCallingAssembly()
                                                .GetManifestResourceStream(baseNamespace + "." + resourceName))
            {
                if (resourceStream == null)
                {
                    throw new ArgumentException($"Invalid fully qualified name of the Resource : {baseNamespace}.{resourceName}");
                }

                using (var fileStream = File.Create(resourceName))
                {
                    resourceStream.CopyTo(fileStream);
                }

                ResourceName = resourceName;
            }
        }

        /// <summary>
        /// Gets or sets the resource name.
        /// </summary>
        public string ResourceName { get; set; }

        /// <summary>
        /// Gets the full path where this resource should be materialized to.
        /// </summary>
        public string FullPath => Path.Combine(Environment.CurrentDirectory, ResourceName);

        /// <summary>
        /// Defines a method to release allocated resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Defines a method to release allocated resources.
        /// </summary>
        /// <param name="disposing">If we are already disposing or not.</param>
        private void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }

            if (ResourceName != null)
            {
                File.Delete(ResourceName);
            }
        }
    }
}