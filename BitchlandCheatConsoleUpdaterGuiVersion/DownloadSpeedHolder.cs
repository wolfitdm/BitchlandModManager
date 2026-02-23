using System;

namespace BitchlandCheatConsoleUpdaterGuiVersion
{
    public class DownloadSpeedHolder
    {
        public TimeSpan elapsed;
        public long bytesReceived;

        public DownloadSpeedHolder(TimeSpan elapsed, long bytesReceived)
        {
            this.elapsed = elapsed;
            this.bytesReceived = bytesReceived;
        }
    }
}