using System;

namespace Raspberry.IO.InterIntegratedCircuit
{
    public abstract class I2cAction
    {
        protected I2cAction(byte[] buffer)
        {
            Buffer = buffer ?? throw new ArgumentNullException("buffer");
        }

        public byte[] Buffer { get; private set; }
    }
}
