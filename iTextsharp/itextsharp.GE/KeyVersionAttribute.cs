using System;

namespace Versions.Attributes {
    [AttributeUsage(AttributeTargets.Assembly)]
    internal class KeyVersionAttribute : Attribute {
        private string keyVersion;

        internal string KeyVersion {
            get { return keyVersion; }
            private set { keyVersion = value; }
        }

        internal KeyVersionAttribute(string keyVersion) {
            this.KeyVersion = keyVersion;
        }
    }
}
