# CSharpProjects
This repository will contain all projects which are in C#

## Messaging
### Messaging Packets 
PaccketType
{
    User = 0x00,
    Message = 0x01
}

MessagePacketData
{
    IServiceUser User;
    string Message;
    System.DateTime Time;
}

UserPacketData
{
    IServiceUser User;
}

Packet
{
    PacketType;
    Data;
}
