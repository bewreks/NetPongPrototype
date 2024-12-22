namespace Lobby
{
    public struct ConnectionParams
    {
        public string IpV4;
        public ushort Port;
        public byte[] AllocationIdBytes;
        public byte[] Key;
        public byte[] ConnectionData;
        public byte[] HostConnectionData;
    }
}
