using Unity.Netcode;
using Unity.Collections;


public struct NetworkString : INetworkSerializable
{
    /*
     this is a new data type for network variables to support strings
     */

    private FixedString32Bytes info;
    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref info);
    }

    public override string ToString()
    {
        return info.ToString();
    }

    public static implicit operator string(NetworkString s) => s.ToString();
    public static implicit operator NetworkString(string s) => new NetworkString { info = new FixedString32Bytes(s)};
}
