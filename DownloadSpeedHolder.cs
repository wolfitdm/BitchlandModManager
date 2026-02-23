using System;

public class DownoadSpeedHolder
{
    public TimeSpan elapsed;
    public long bytesReceived;

    public void DownloadSpeedHolder(TimeSpan elapsed, long bytesReceived)
    {
        this.elapsed = elapsed;
        this.bytesReceived = bytesReceived;
    }
}
